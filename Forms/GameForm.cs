using AdvancedTicTacToeWinForms.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedTicTacToeWinForms
{
    public partial class GameForm : Form
    {
        private Button[] gridButtons;

        private bool isXTurn = true;
        private bool isVsComputer = false;
        private bool isGameActive = true;
        private bool isTournamentMatch = false;
        private bool resultShown = false;
        private bool isAiThinking = false;

        private string player1Name = "Player X";
        private string player2Name = "Player O";
        private string tossWinner = "";

        private int xWins = 0;
        private int oWins = 0;
        private int draws = 0;

        private string aiDifficulty = "Easy";
        private Random random = new Random();

        private Action<string> tournamentWinnerCallback;
        private Action tournamentDrawCallback;
        private Action tournamentBackCallback;

        private Timer animationTimer;
        private Timer matchTimer;

        private float glowPhase;
        private int elapsedSeconds;

        private Panel topPanel;
        private Panel leftPanel;
        private Panel rightPanel;
        private Panel boardPanel;
        private Panel bottomPanel;
        private Panel resultOverlay;

        private Label lblEdition;
        private Label lblMatchType;
        private Label lblRound;
        private Label lblTimer;
        private Label lblAIInfo;
        private Label lblTurnIndicator;
        private Label lblTossInfo;
        private Label lblBoardHeader;
        private Label lblBoardSub;
        private Label lblFooter;

        private Label lblXName;
        private Label lblOName;
        private Label lblXSymbol;
        private Label lblOSymbol;
        private Label lblXScore;
        private Label lblOScore;
        private Label lblDrawScore;

        private Label lblResultTitle;
        private Label lblResultSub;
        private Label lblResultScore;

        private Button btnResultReset;
        private Button btnResultBack;

        private Point resultBoxLocation;
        private Size resultBoxSize;

        private int winningA = -1;
        private int winningB = -1;
        private int winningC = -1;

        private Color backgroundColor = Color.FromArgb(7, 10, 16);
        private Color panelColor = Color.FromArgb(14, 19, 28);
        private Color panelColor2 = Color.FromArgb(19, 25, 36);
        private Color boardCellColor = Color.FromArgb(15, 22, 32);
        private Color buttonColor = Color.FromArgb(23, 30, 42);
        private Color hoverColor = Color.FromArgb(35, 47, 65);

        private Color textColor = Color.FromArgb(235, 240, 255);
        private Color accentColor = Color.FromArgb(80, 170, 255);
        private Color accentBright = Color.FromArgb(135, 210, 255);

        private Color aiColor = Color.FromArgb(255, 92, 105);
        private Color aiBright = Color.FromArgb(255, 140, 150);

        private Color drawColor = Color.FromArgb(255, 190, 80);
        private Color greenColor = Color.FromArgb(80, 220, 150);
        private Color secondaryColor = Color.FromArgb(125, 140, 165);

        private Color borderColor = Color.FromArgb(42, 56, 76);

        public GameForm()
        {
            InitializeComponent();

            DoubleBuffered = true;

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);

            InitializeGameGrid();
            ApplyGamingTheme();
            CreatePremiumInterface();
            StartAnimation();
            StartMatchTimer();

            Load += GameForm_Load;
            Resize += GameForm_Resize;
            FormClosed += GameForm_FormClosed;
        }

        public GameForm(string p1, string p2) : this()
        {
            isVsComputer = false;
            isTournamentMatch = false;

            player1Name =
                string.IsNullOrWhiteSpace(p1)
                    ? "Player X"
                    : p1;

            player2Name =
                string.IsNullOrWhiteSpace(p2)
                    ? "Player O"
                    : p2;

            Random rand = new Random();

            tossWinner =
                rand.Next(0, 2) == 0
                    ? player1Name
                    : player2Name;

            isXTurn =
                tossWinner == player1Name;

            UpdateLabels();
        }

        public GameForm(
            string p1,
            string p2,
            Action<string> onWinner,
            Action onDraw,
            Action onBack) : this()
        {
            isVsComputer = false;
            isTournamentMatch = true;

            player1Name =
                string.IsNullOrWhiteSpace(p1)
                    ? "Player X"
                    : p1;

            player2Name =
                string.IsNullOrWhiteSpace(p2)
                    ? "Player O"
                    : p2;

            tournamentWinnerCallback = onWinner;
            tournamentDrawCallback = onDraw;
            tournamentBackCallback = onBack;

            Random rand = new Random();

            tossWinner =
                rand.Next(0, 2) == 0
                    ? player1Name
                    : player2Name;

            isXTurn =
                tossWinner == player1Name;

            UpdateLabels();
        }

        public GameForm(
            string p1,
            string p2,
            string winner,
            bool vsAI = false,
            string difficulty = "Easy") : this()
        {
            isVsComputer = vsAI;
            isTournamentMatch = false;

            player1Name =
                string.IsNullOrWhiteSpace(p1)
                    ? "Player X"
                    : p1;

            player2Name =
                string.IsNullOrWhiteSpace(p2)
                    ? (vsAI ? "Computer" : "Player O")
                    : p2;

            tossWinner =
                string.IsNullOrWhiteSpace(winner)
                    ? player1Name
                    : winner;

            aiDifficulty =
                string.IsNullOrWhiteSpace(difficulty)
                    ? "Easy"
                    : difficulty;

            isXTurn =
                tossWinner == player1Name;

            UpdateLabels();
        }

        private void ApplyGamingTheme()
        {
            BackColor = backgroundColor;
            ForeColor = textColor;

            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;

            StartPosition =
                FormStartPosition.CenterScreen;

            MinimumSize =
                new Size(1080, 720);

            Text =
                "Advanced Tic Tac Toe";

            lblTitle.Visible = false;
            lblStatus.Visible = false;
            lblToss.Visible = false;
            lblScoreboardHeader.Visible = false;
            lblXWins.Visible = false;
            lblOWins.Visible = false;
            lblDraws.Visible = false;

            StyleGridButton(btn1);
            StyleGridButton(btn2);
            StyleGridButton(btn3);
            StyleGridButton(btn4);
            StyleGridButton(btn5);
            StyleGridButton(btn6);
            StyleGridButton(btn7);
            StyleGridButton(btn8);
            StyleGridButton(btn9);

            StyleSideButton(btnHistory);
            StyleSideButton(btnLeaderboard);

            StylePrimaryButton(btnReset);
            StyleSecondaryButton(btnBack);

            btnHistory.Text =
                "MATCH HISTORY";

            btnLeaderboard.Text =
                "LEADERBOARD";

            btnReset.Text =
                "RESET ROUND";

            btnBack.Text =
                "EXIT MATCH";

            Paint += GameForm_Paint;
        }

        private void CreatePremiumInterface()
        {
            topPanel =
                new Panel();

            topPanel.BackColor =
                Color.Transparent;

            Controls.Add(topPanel);

            lblEdition =
                CreateLabel(
                    "TACTICAL EDITION // 03",
                    9F,
                    FontStyle.Bold,
                    secondaryColor);

            lblMatchType =
                CreateLabel(
                    "MATCH // PLAYER VS PLAYER",
                    9F,
                    FontStyle.Bold,
                    accentColor);

            lblRound =
                CreateLabel(
                    "ROUND // 01",
                    9F,
                    FontStyle.Bold,
                    secondaryColor);

            lblTimer =
                CreateLabel(
                    "TIME // 00:00",
                    10F,
                    FontStyle.Bold,
                    greenColor);

            lblAIInfo =
                CreateLabel(
                    "LOCAL CORE // ONLINE",
                    9F,
                    FontStyle.Bold,
                    accentColor);

            lblTurnIndicator =
                CreateLabel(
                    "",
                    12F,
                    FontStyle.Bold,
                    accentBright);

            lblTossInfo =
                CreateLabel(
                    "",
                    9F,
                    FontStyle.Bold,
                    drawColor);

            lblBoardHeader =
                CreateLabel(
                    "BATTLE GRID",
                    10F,
                    FontStyle.Bold,
                    secondaryColor);

            lblBoardSub =
                CreateLabel(
                    "MAKE YOUR MOVE",
                    8F,
                    FontStyle.Bold,
                    Color.FromArgb(
                        95,
                        110,
                        135));

            lblFooter =
     new Label();

            lblFooter.Text =
                "DEVELOPED BY SYED HUSSAIN ABBAS";

            lblFooter.Font =
                new Font(
                    "Segoe UI",
                    8.5F,
                    FontStyle.Bold);

            lblFooter.ForeColor =
                Color.FromArgb(
                    105,
                    120,
                    145);

            lblFooter.BackColor =
                backgroundColor;

            lblFooter.AutoSize =
                false;

            lblFooter.TextAlign =
                ContentAlignment.MiddleCenter;

            Controls.Add(lblEdition);
            Controls.Add(lblMatchType);
            Controls.Add(lblRound);
            Controls.Add(lblTimer);
            Controls.Add(lblAIInfo);
            Controls.Add(lblTurnIndicator);
            Controls.Add(lblTossInfo);
            Controls.Add(lblBoardHeader);
            Controls.Add(lblBoardSub);
            Controls.Add(lblFooter);

            leftPanel =
                CreatePanel();

            rightPanel =
                CreatePanel();

            boardPanel =
                CreatePanel();

            bottomPanel =
                new Panel();

            bottomPanel.BackColor =
                backgroundColor;

            Controls.Add(leftPanel);
            Controls.Add(rightPanel);
            Controls.Add(boardPanel);
            Controls.Add(bottomPanel);

            CreatePlayerCard(
                leftPanel,
                true,
                out lblXName,
                out lblXSymbol,
                out lblXScore);

            CreatePlayerCard(
                rightPanel,
                false,
                out lblOName,
                out lblOSymbol,
                out lblOScore);

            lblDrawScore =
                CreateLabel(
                    "DRAWS // 00",
                    9F,
                    FontStyle.Bold,
                    drawColor);

            rightPanel.Controls.Add(
                lblDrawScore);

            boardPanel.Controls.Add(
                lblBoardHeader);

            boardPanel.Controls.Add(
                lblBoardSub);

            foreach (Button button in gridButtons)
                boardPanel.Controls.Add(button);

            bottomPanel.Controls.Add(
                btnHistory);

            bottomPanel.Controls.Add(
                btnLeaderboard);

            bottomPanel.Controls.Add(
                btnReset);

            bottomPanel.Controls.Add(
                btnBack);

            CreateResultOverlay();

            UpdateMatchTypeText();
            UpdatePlayerCards();
            UpdateLayout();
        }

        private Label CreateLabel(
            string text,
            float size,
            FontStyle style,
            Color color)
        {
            Label label =
                new Label();

            label.Text =
                text;

            label.Font =
                new Font(
                    "Segoe UI",
                    size,
                    style);

            label.ForeColor =
                color;

            label.BackColor =
                Color.Transparent;

            label.AutoSize =
                false;

            label.TextAlign =
                ContentAlignment.MiddleCenter;

            return label;
        }

        private Panel CreatePanel()
        {
            Panel panel =
                new Panel();

            panel.BackColor =
                panelColor;

            panel.BorderStyle =
                BorderStyle.FixedSingle;

            return panel;
        }

        private void CreatePlayerCard(
            Control parent,
            bool isX,
            out Label nameLabel,
            out Label symbolLabel,
            out Label scoreLabel)
        {
            Panel card =
                new Panel();

            card.BackColor =
                panelColor2;

            card.BorderStyle =
                BorderStyle.FixedSingle;

            card.Tag =
                isX ? "X_CARD" : "O_CARD";

            parent.Controls.Add(card);

            symbolLabel =
                CreateLabel(
                    isX ? "X" : "O",
                    28F,
                    FontStyle.Bold,
                    isX
                        ? accentBright
                        : aiBright);

            nameLabel =
                CreateLabel(
                    isX
                        ? player1Name.ToUpper()
                        : player2Name.ToUpper(),
                    10F,
                    FontStyle.Bold,
                    textColor);

            scoreLabel =
                CreateLabel(
                    "00",
                    24F,
                    FontStyle.Bold,
                    isX
                        ? accentColor
                        : aiColor);

            Label roleLabel =
                CreateLabel(
                    isX
                        ? "PLAYER X"
                        : "PLAYER O",
                    8F,
                    FontStyle.Bold,
                    secondaryColor);

            card.Controls.Add(symbolLabel);
            card.Controls.Add(nameLabel);
            card.Controls.Add(roleLabel);
            card.Controls.Add(scoreLabel);
        }

        private void CreateResultOverlay()
        {
            resultOverlay =
                new Panel();

            resultOverlay.BackColor =
                Color.FromArgb(
                    230,
                    5,
                    8,
                    14);

            resultOverlay.Visible =
                false;

            resultOverlay.Paint +=
                ResultOverlay_Paint;

            lblResultTitle =
                CreateLabel(
                    "VICTORY",
                    32F,
                    FontStyle.Bold,
                    accentBright);

            lblResultSub =
                CreateLabel(
                    "",
                    11F,
                    FontStyle.Bold,
                    textColor);

            lblResultScore =
                CreateLabel(
                    "",
                    11F,
                    FontStyle.Bold,
                    secondaryColor);

            btnResultReset =
                CreateOverlayButton(
                    "PLAY AGAIN",
                    true);

            btnResultBack =
                CreateOverlayButton(
                    "RETURN",
                    false);

            btnResultReset.Click +=
                ResultReset_Click;

            btnResultBack.Click +=
                ResultBack_Click;

            resultOverlay.Controls.Add(
                lblResultTitle);

            resultOverlay.Controls.Add(
                lblResultSub);

            resultOverlay.Controls.Add(
                lblResultScore);

            resultOverlay.Controls.Add(
                btnResultReset);

            resultOverlay.Controls.Add(
                btnResultBack);

            Controls.Add(resultOverlay);

            resultOverlay.BringToFront();
        }

        private Button CreateOverlayButton(
            string text,
            bool primary)
        {
            Button button =
                new Button();

            button.Text =
                text;

            button.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            button.ForeColor =
                Color.White;

            button.BackColor =
                primary
                    ? Color.FromArgb(
                        32,
                        115,
                        195)
                    : buttonColor;

            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderSize =
                1;

            button.FlatAppearance.BorderColor =
                primary
                    ? accentColor
                    : borderColor;

            button.Size =
                new Size(
                    155,
                    42);

            button.Cursor =
                Cursors.Hand;

            button.TabStop =
                false;

            button.UseVisualStyleBackColor =
                false;

            button.MouseEnter +=
                OverlayButton_MouseEnter;

            button.MouseLeave +=
                OverlayButton_MouseLeave;

            return button;
        }

        private void UpdateLayout()
        {
            int width =
                ClientSize.Width;

            int height =
                ClientSize.Height;

            if (width <= 1 ||
                height <= 1)
                return;

            int margin = 32;
            int headerHeight = 100;
            int bottomHeight = 65;
            int contentTop = 130;

            int contentBottom =
                height -
                bottomHeight -
                25;

            int contentHeight =
                contentBottom -
                contentTop;

            int sideWidth = 220;
            int gap = 25;

            int availableBoardWidth =
                width -
                margin * 2 -
                sideWidth * 2 -
                gap * 2;

            int boardSize =
                Math.Min(
                    availableBoardWidth,
                    contentHeight);

            boardSize =
                Math.Min(
                    boardSize,
                    500);

            boardSize =
                Math.Max(
                    boardSize,
                    360);

            int totalWidth =
                sideWidth * 2 +
                boardSize +
                gap * 2;

            int startX =
                (width -
                 totalWidth) / 2;

            if (startX < margin)
                startX = margin;

            lblEdition.Location =
                new Point(
                    35,
                    24);

            lblEdition.Size =
                new Size(
                    255,
                    28);

            lblMatchType.Location =
                new Point(
                    35,
                    57);

            lblMatchType.Size =
                new Size(
                    255,
                    24);

            lblRound.Location =
                new Point(
                    margin + 270,
                    52);

            lblRound.Size =
                new Size(
                    120,
                    25);

            lblTimer.Location =
                new Point(
                    width -
                    margin -
                    160,
                    25);

            lblTimer.Size =
                new Size(
                    160,
                    28);

            lblAIInfo.Location =
                new Point(
                    width -
                    margin -
                    250,
                    52);

            lblAIInfo.Size =
                new Size(
                    250,
                    25);

            lblTurnIndicator.Location =
                new Point(
                    width / 2 -
                    230,
                    25);

            lblTurnIndicator.Size =
                new Size(
                    460,
                    32);

            lblTossInfo.Location =
                new Point(
                    width / 2 -
                    260,
                    56);

            lblTossInfo.Size =
                new Size(
                    520,
                    25);

            leftPanel.Location =
                new Point(
                    startX,
                    contentTop);

            leftPanel.Size =
                new Size(
                    sideWidth,
                    boardSize);

            boardPanel.Location =
                new Point(
                    startX +
                    sideWidth +
                    gap,
                    contentTop);

            boardPanel.Size =
                new Size(
                    boardSize,
                    boardSize);

            rightPanel.Location =
                new Point(
                    startX +
                    sideWidth +
                    gap +
                    boardSize +
                    gap,
                    contentTop);

            rightPanel.Size =
                new Size(
                    sideWidth,
                    boardSize);

            lblBoardHeader.Location =
                new Point(
                    0,
                    8);

            lblBoardHeader.Size =
                new Size(
                    boardSize,
                    22);

            lblBoardSub.Location =
                new Point(
                    0,
                    31);

            lblBoardSub.Size =
                new Size(
                    boardSize,
                    20);

            int gridTop = 58;

            int gridSize =
                Math.Min(
                    boardSize - 24,
                    boardSize -
                    gridTop -
                    12);

            int cellSize =
                gridSize / 3;

            cellSize =
                Math.Max(
                    100,
                    cellSize);

            gridSize =
                cellSize * 3;

            int gridX =
                (boardSize -
                 gridSize) / 2;

            int gridY =
                gridTop;

            for (int i = 0;
                 i < gridButtons.Length;
                 i++)
            {
                int row =
                    i / 3;

                int col =
                    i % 3;

                gridButtons[i].Location =
                    new Point(
                        gridX +
                        col * cellSize,
                        gridY +
                        row * cellSize);

                gridButtons[i].Size =
                    new Size(
                        cellSize - 2,
                        cellSize - 2);
            }

            LayoutPlayerCard(
                leftPanel,
                "X_CARD",
                lblXName,
                lblXSymbol,
                lblXScore);

            LayoutPlayerCard(
                rightPanel,
                "O_CARD",
                lblOName,
                lblOSymbol,
                lblOScore);

            lblDrawScore.Location =
                new Point(
                    10,
                    300);

            lblDrawScore.Size =
                new Size(
                    rightPanel.Width - 20,
                    36);

            bottomPanel.Location =
                new Point(
                    margin,
                    height -
                    bottomHeight -
                    5);

            bottomPanel.Size =
                new Size(
                    width -
                    margin * 2,
                    bottomHeight);

            int buttonWidth = 155;
            int buttonGap = 12;
            int buttonY = 8;

            btnHistory.Location =
                new Point(
                    0,
                    buttonY);

            btnLeaderboard.Location =
                new Point(
                    buttonWidth +
                    buttonGap,
                    buttonY);

            btnReset.Location =
                new Point(
                    bottomPanel.Width -
                    buttonWidth * 2 -
                    buttonGap,
                    buttonY);

            btnBack.Location =
                new Point(
                    bottomPanel.Width -
                    buttonWidth,
                    buttonY);

            lblFooter.Location =
                new Point(
                    width / 2 -
                    250,
                    height -
                    48);

            lblFooter.Size =
                new Size(
                    500,
                    20);

            if (resultOverlay != null)
            {
                resultOverlay.Location =
                    new Point(
                        0,
                        0);

                resultOverlay.Size =
                    new Size(
                        width,
                        height);

                LayoutResultOverlay();
            }

            lblEdition.BringToFront();
            lblMatchType.BringToFront();
            lblRound.BringToFront();
            lblTimer.BringToFront();
            lblAIInfo.BringToFront();
            lblTurnIndicator.BringToFront();
            lblTossInfo.BringToFront();
            lblFooter.BringToFront();
        }

        private void LayoutPlayerCard(
            Panel parent,
            string cardTag,
            Label nameLabel,
            Label symbolLabel,
            Label scoreLabel)
        {
            Panel card = null;

            foreach (Control control
                     in parent.Controls)
            {
                if (control is Panel &&
                    control.Tag != null &&
                    control.Tag.ToString() ==
                    cardTag)
                {
                    card =
                        control as Panel;

                    break;
                }
            }

            if (card == null)
                return;

            card.Location =
                new Point(
                    10,
                    105);

            card.Size =
                new Size(
                    parent.Width - 20,
                    180);

            foreach (Control control
                     in card.Controls)
            {
                if (!(control is Label))
                    continue;

                Label label =
                    control as Label;

                if (label == symbolLabel)
                {
                    label.Location =
                        new Point(
                            20,
                            10);

                    label.Size =
                        new Size(
                            card.Width - 40,
                            55);
                }
                else if (label == nameLabel)
                {
                    label.Location =
                        new Point(
                            20,
                            65);

                    label.Size =
                        new Size(
                            card.Width - 40,
                            32);
                }
                else if (label == scoreLabel)
                {
                    label.Location =
                        new Point(
                            20,
                            125);

                    label.Size =
                        new Size(
                            card.Width - 40,
                            40);
                }
                else
                {
                    label.Location =
                        new Point(
                            20,
                            100);

                    label.Size =
                        new Size(
                            card.Width - 40,
                            22);
                }
            }
        }

        private void LayoutResultOverlay()
        {
            if (resultOverlay == null)
                return;

            int boxWidth = 620;
            int boxHeight = 280;

            int x =
                (resultOverlay.Width -
                 boxWidth) / 2;

            int y =
                (resultOverlay.Height -
                 boxHeight) / 2;

            resultBoxLocation =
                new Point(
                    x,
                    y);

            resultBoxSize =
                new Size(
                    boxWidth,
                    boxHeight);

            lblResultTitle.Location =
                new Point(
                    x + 60,
                    y + 38);

            lblResultTitle.Size =
                new Size(
                    boxWidth - 120,
                    55);

            lblResultSub.Location =
                new Point(
                    x + 40,
                    y + 105);

            lblResultSub.Size =
                new Size(
                    boxWidth - 80,
                    35);

            lblResultScore.Location =
                new Point(
                    x + 40,
                    y + 145);

            lblResultScore.Size =
                new Size(
                    boxWidth - 80,
                    30);

            btnResultReset.Location =
                new Point(
                    x + 145,
                    y + 205);

            btnResultBack.Location =
                new Point(
                    x + 320,
                    y + 205);
        }

        private void StartAnimation()
        {
            animationTimer =
                new Timer();

            animationTimer.Interval =
                35;

            animationTimer.Tick +=
                AnimationTimer_Tick;

            animationTimer.Start();
        }

        private void StartMatchTimer()
        {
            matchTimer =
                new Timer();

            matchTimer.Interval =
                1000;

            matchTimer.Tick +=
                MatchTimer_Tick;

            matchTimer.Start();
        }

        private void MatchTimer_Tick(
            object sender,
            EventArgs e)
        {
            if (!isGameActive)
                return;

            elapsedSeconds++;

            int minutes =
                elapsedSeconds / 60;

            int seconds =
                elapsedSeconds % 60;

            if (lblTimer != null)
            {
                lblTimer.Text =
                    $"TIME // {minutes:00}:{seconds:00}";
            }
        }

        private void AnimationTimer_Tick(
            object sender,
            EventArgs e)
        {
            glowPhase +=
                0.045F;

            if (glowPhase >
                Math.PI * 2)
            {
                glowPhase = 0;
            }

            Invalidate(new Rectangle(
                0,
                0,
                ClientSize.Width,
                ClientSize.Height - 55)); ;
        }

        private void GameForm_Paint(
            object sender,
            PaintEventArgs e)
        {
            if (ClientSize.Width <= 1 ||
                ClientSize.Height <= 1)
                return;

            Graphics g =
                e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            Rectangle area =
                ClientRectangle;

            if (area.Width <= 1 ||
                area.Height <= 1)
                return;

            using (LinearGradientBrush brush =
                new LinearGradientBrush(
                    area,
                    Color.FromArgb(
                        5,
                        8,
                        14),
                    Color.FromArgb(
                        15,
                        21,
                        31),
                    90F))
            {
                g.FillRectangle(
                    brush,
                    area);
            }

            int width =
                ClientSize.Width;

            int height =
                ClientSize.Height;

            using (Pen gridPen =
                new Pen(
                    Color.FromArgb(
                        11,
                        17,
                        26),
                    1))
            {
                for (int x = 0;
                     x < width;
                     x += 32)
                {
                    g.DrawLine(
                        gridPen,
                        x,
                        0,
                        x,
                        height);
                }

                for (int y = 0;
                     y < height;
                     y += 32)
                {
                    g.DrawLine(
                        gridPen,
                        0,
                        y,
                        width,
                        y);
                }
            }

            int glowAlpha =
                12 +
                (int)(
                    Math.Sin(glowPhase) *
                    5);

            int glowSize =
                Math.Min(
                    width,
                    height) / 2;

            using (SolidBrush glow =
                new SolidBrush(
                    Color.FromArgb(
                        glowAlpha,
                        35,
                        120,
                        200)))
            {
                g.FillEllipse(
                    glow,
                    width / 2 -
                    glowSize / 2,
                    150,
                    glowSize,
                    glowSize);
            }

            using (Pen borderPen =
                new Pen(
                    Color.FromArgb(
                        35,
                        49,
                        70),
                    1))
            {
                g.DrawRectangle(
                    borderPen,
                    18,
                    18,
                    width - 36,
                    height - 36);
            }

            using (Pen separatorPen =
      new Pen(
          Color.FromArgb(
              35,
              49,
              70),
          1))
            {
                g.DrawLine(
                    separatorPen,
                    32,
                    105,
                    width - 32,
                    105);
            }

            DrawWinningLine(g);
        }

        private void DrawWinningLine(
            Graphics g)
        {
            if (winningA == -1 ||
                winningB == -1 ||
                winningC == -1)
                return;

            if (winningA >= gridButtons.Length ||
                winningB >= gridButtons.Length ||
                winningC >= gridButtons.Length)
                return;

            Point p1 =
                GetButtonCenter(
                    gridButtons[winningA]);

            Point p2 =
                GetButtonCenter(
                    gridButtons[winningC]);

            Color lineColor =
                gridButtons[winningA].Text ==
                "X"
                    ? accentBright
                    : aiBright;

            using (Pen glow =
                new Pen(
                    Color.FromArgb(
                        45,
                        lineColor.R,
                        lineColor.G,
                        lineColor.B),
                    10))
            {
                g.DrawLine(
                    glow,
                    p1,
                    p2);
            }

            using (Pen line =
                new Pen(
                    lineColor,
                    4))
            {
                g.DrawLine(
                    line,
                    p1,
                    p2);
            }
        }

        private Point GetButtonCenter(
            Button button)
        {
            Point location =
                button.PointToScreen(
                    Point.Empty);

            Point local =
                PointToClient(
                    location);

            return new Point(
                local.X +
                button.Width / 2,
                local.Y +
                button.Height / 2);
        }

        private void ResultOverlay_Paint(
            object sender,
            PaintEventArgs e)
        {
            Graphics g =
                e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            if (resultBoxSize.Width <= 0)
                return;

            int pulse =
                18 +
                (int)(
                    Math.Sin(glowPhase) *
                    7);

            Rectangle glowRectangle =
                new Rectangle(
                    resultBoxLocation.X - 20,
                    resultBoxLocation.Y - 20,
                    resultBoxSize.Width + 40,
                    resultBoxSize.Height + 40);

            using (SolidBrush glow =
                new SolidBrush(
                    Color.FromArgb(
                        pulse,
                        60,
                        150,
                        230)))
            {
                g.FillRectangle(
                    glow,
                    glowRectangle);
            }

            using (SolidBrush box =
                new SolidBrush(
                    Color.FromArgb(
                        250,
                        13,
                        18,
                        28)))
            {
                g.FillRectangle(
                    box,
                    new Rectangle(
                        resultBoxLocation,
                        resultBoxSize));
            }

            using (Pen border =
                new Pen(
                    lblResultTitle.ForeColor,
                    1))
            {
                g.DrawRectangle(
                    border,
                    new Rectangle(
                        resultBoxLocation,
                        resultBoxSize));
            }
        }

        private void GameForm_Resize(
            object sender,
            EventArgs e)
        {
            if (!IsHandleCreated)
                return;

            if (ClientSize.Width <= 1 ||
                ClientSize.Height <= 1)
                return;

            UpdateLayout();
            Invalidate();
        }

        private void StyleGridButton(
            Button button)
        {
            button.Text =
                "";

            button.Font =
                new Font(
                    "Segoe UI",
                    35F,
                    FontStyle.Bold);

            button.ForeColor =
                textColor;

            button.BackColor =
                boardCellColor;

            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderColor =
                Color.FromArgb(
                    47,
                    65,
                    88);

            button.FlatAppearance.BorderSize =
                1;

            button.Cursor =
                Cursors.Hand;

            button.TabStop =
                false;

            button.UseVisualStyleBackColor =
                false;

            button.MouseEnter +=
                GridButton_MouseEnter;

            button.MouseLeave +=
                GridButton_MouseLeave;
        }

        private void StyleSideButton(
            Button button)
        {
            button.Size =
                new Size(
                    155,
                    42);

            button.Font =
                new Font(
                    "Segoe UI",
                    8.5F,
                    FontStyle.Bold);

            button.ForeColor =
                textColor;

            button.BackColor =
                buttonColor;

            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderColor =
                borderColor;

            button.FlatAppearance.BorderSize =
                1;

            button.Cursor =
                Cursors.Hand;

            button.TabStop =
                false;

            button.UseVisualStyleBackColor =
                false;

            button.MouseEnter +=
                SideButton_MouseEnter;

            button.MouseLeave +=
                SideButton_MouseLeave;
        }

        private void StylePrimaryButton(
            Button button)
        {
            button.Size =
                new Size(
                    155,
                    42);

            button.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            button.ForeColor =
                Color.White;

            button.BackColor =
                Color.FromArgb(
                    32,
                    115,
                    195);

            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderColor =
                accentColor;

            button.FlatAppearance.BorderSize =
                1;

            button.Cursor =
                Cursors.Hand;

            button.TabStop =
                false;

            button.UseVisualStyleBackColor =
                false;

            button.MouseEnter +=
                ActionButton_MouseEnter;

            button.MouseLeave +=
                ActionButton_MouseLeave;
        }

        private void StyleSecondaryButton(
            Button button)
        {
            button.Size =
                new Size(
                    155,
                    42);

            button.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            button.ForeColor =
                textColor;

            button.BackColor =
                buttonColor;

            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderColor =
                borderColor;

            button.FlatAppearance.BorderSize =
                1;

            button.Cursor =
                Cursors.Hand;

            button.TabStop =
                false;

            button.UseVisualStyleBackColor =
                false;

            button.MouseEnter +=
                ActionButton_MouseEnter;

            button.MouseLeave +=
                ActionButton_MouseLeave;
        }

        private void GridButton_MouseEnter(
            object sender,
            EventArgs e)
        {
            if (sender is Button button &&
                button.Text == "" &&
                isGameActive &&
                !isAiThinking)
            {
                button.BackColor =
                    hoverColor;

                button.FlatAppearance.BorderColor =
                    accentColor;
            }
        }

        private void GridButton_MouseLeave(
            object sender,
            EventArgs e)
        {
            if (sender is Button button &&
                button.Text == "")
            {
                button.BackColor =
                    boardCellColor;

                button.FlatAppearance.BorderColor =
                    Color.FromArgb(
                        47,
                        65,
                        88);
            }
        }

        private void SideButton_MouseEnter(
            object sender,
            EventArgs e)
        {
            if (sender is Button button)
            {
                button.BackColor =
                    hoverColor;

                button.FlatAppearance.BorderColor =
                    accentColor;
            }
        }

        private void SideButton_MouseLeave(
            object sender,
            EventArgs e)
        {
            if (sender is Button button)
            {
                button.BackColor =
                    buttonColor;

                button.FlatAppearance.BorderColor =
                    borderColor;
            }
        }

        private void ActionButton_MouseEnter(
            object sender,
            EventArgs e)
        {
            if (sender is Button button)
            {
                button.BackColor =
                    hoverColor;

                button.FlatAppearance.BorderColor =
                    accentBright;
            }
        }

        private void ActionButton_MouseLeave(
            object sender,
            EventArgs e)
        {
            if (sender is Button button)
            {
                if (button == btnReset)
                {
                    button.BackColor =
                        Color.FromArgb(
                            32,
                            115,
                            195);

                    button.FlatAppearance.BorderColor =
                        accentColor;
                }
                else
                {
                    button.BackColor =
                        buttonColor;

                    button.FlatAppearance.BorderColor =
                        borderColor;
                }
            }
        }

        private void OverlayButton_MouseEnter(
            object sender,
            EventArgs e)
        {
            if (sender is Button button)
            {
                button.BackColor =
                    hoverColor;

                button.FlatAppearance.BorderColor =
                    accentBright;
            }
        }

        private void OverlayButton_MouseLeave(
            object sender,
            EventArgs e)
        {
            if (sender is Button button)
            {
                if (button == btnResultReset)
                {
                    button.BackColor =
                        Color.FromArgb(
                            32,
                            115,
                            195);

                    button.FlatAppearance.BorderColor =
                        accentColor;
                }
                else
                {
                    button.BackColor =
                        buttonColor;

                    button.FlatAppearance.BorderColor =
                        borderColor;
                }
            }
        }

        private void InitializeGameGrid()
        {
            gridButtons =
                new Button[]
                {
                btn1,
                btn2,
                btn3,
                btn4,
                btn5,
                btn6,
                btn7,
                btn8,
                btn9
                };

            foreach (Button btn
                     in gridButtons)
            {
                if (btn != null)
                {
                    btn.Text =
                        "";

                    btn.Enabled =
                        true;
                }
            }
        }

        private async void GameForm_Load(
            object sender,
            EventArgs e)
        {
            UpdateLabels();

            if (isVsComputer &&
                !isXTurn &&
                isGameActive)
            {
                SetGridEnabled(false);
                isAiThinking = true;

                lblBoardSub.Text =
                    "AI CORE // CALCULATING";

                await Task.Delay(350);

                if (!IsDisposed &&
                    isGameActive)
                {
                    MakeComputerMove();
                }

                isAiThinking = false;

                if (isGameActive)
                    SetGridEnabled(true);
            }
        }

        private void UpdateMatchTypeText()
        {
            if (lblMatchType == null)
                return;

            if (isVsComputer)
            {
                lblMatchType.Text =
                    "MATCH // PLAYER VS AI";

                lblAIInfo.Text =
                    $"AI CORE // {aiDifficulty.ToUpper()}";
            }
            else if (isTournamentMatch)
            {
                lblMatchType.Text =
                    "MATCH // TOURNAMENT";

                lblAIInfo.Text =
                    "TOURNAMENT CORE // ONLINE";
            }
            else
            {
                lblMatchType.Text =
                    "MATCH // PLAYER VS PLAYER";

                lblAIInfo.Text =
                    "LOCAL CORE // ONLINE";
            }
        }

        private void UpdatePlayerCards()
        {
            if (lblXName != null)
                lblXName.Text =
                    player1Name.ToUpper();

            if (lblOName != null)
                lblOName.Text =
                    player2Name.ToUpper();

            if (lblXScore != null)
                lblXScore.Text =
                    xWins.ToString("00");

            if (lblOScore != null)
                lblOScore.Text =
                    oWins.ToString("00");

            if (lblDrawScore != null)
                lblDrawScore.Text =
                    $"DRAWS // {draws:00}";
        }

        private void UpdateLabels()
        {
            UpdateMatchTypeText();
            UpdatePlayerCards();

            if (lblTossInfo != null)
            {
                lblTossInfo.Text =
                    $"TOSS WINNER // {tossWinner.ToUpper()}";
            }

            SetCurrentTurnLabel();
        }

        private void SetCurrentTurnLabel()
        {
            if (lblTurnIndicator == null ||
                !isGameActive)
                return;

            string currentPlayer =
                isXTurn
                    ? player1Name
                    : player2Name;

            string symbol =
                isXTurn
                    ? "X"
                    : "O";

            lblTurnIndicator.Text =
                $"TURN // {currentPlayer.ToUpper()}   [{symbol}]";

            lblTurnIndicator.ForeColor =
                isXTurn
                    ? accentBright
                    : aiBright;

            if (lblBoardSub != null)
            {
                lblBoardSub.Text =
                    isXTurn
                        ? "X // MAKE YOUR MOVE"
                        : "O // MAKE YOUR MOVE";
            }
        }

        private async void HandleButtonClick(
            Button clickedButton)
        {
            if (!isGameActive ||
                isAiThinking ||
                clickedButton == null ||
                clickedButton.Text != "")
                return;

            clickedButton.Text =
                isXTurn
                    ? "X"
                    : "O";

            clickedButton.ForeColor =
                isXTurn
                    ? accentBright
                    : aiBright;

            clickedButton.BackColor =
                isXTurn
                    ? Color.FromArgb(
                        20,
                        45,
                        70)
                    : Color.FromArgb(
                        60,
                        25,
                        35);

            clickedButton.FlatAppearance.BorderColor =
                isXTurn
                    ? accentBright
                    : aiBright;

            if (CheckWinner())
                return;

            isXTurn =
                !isXTurn;

            SetCurrentTurnLabel();

            if (isVsComputer &&
                !isXTurn &&
                isGameActive)
            {
                SetGridEnabled(false);
                isAiThinking = true;

                lblBoardSub.Text =
                    "AI CORE // CALCULATING";

                await Task.Delay(350);

                if (!IsDisposed &&
                    isGameActive)
                {
                    MakeComputerMove();
                }

                isAiThinking = false;

                if (isGameActive)
                    SetGridEnabled(true);
            }
        }

        private void MakeComputerMove()
        {
            if (!isGameActive)
                return;

            int bestMove =
                -1;

            if (aiDifficulty == "Easy")
            {
                int[] emptyIndices =
                    GetEmptyIndices();

                if (emptyIndices.Length > 0)
                {
                    bestMove =
                        emptyIndices[
                            random.Next(
                                emptyIndices.Length)];
                }
            }
            else if (aiDifficulty == "Medium")
            {
                bestMove =
                    FindBestMove("O");

                if (bestMove == -1)
                    bestMove =
                        FindBestMove("X");

                if (bestMove == -1)
                {
                    int[] emptyIndices =
                        GetEmptyIndices();

                    if (emptyIndices.Length > 0)
                    {
                        bestMove =
                            emptyIndices[
                                random.Next(
                                    emptyIndices.Length)];
                    }
                }
            }
            else
            {
                bestMove =
                    FindBestHardMove();
            }

            if (bestMove == -1)
                return;

            gridButtons[bestMove].Text =
                "O";

            gridButtons[bestMove].ForeColor =
                aiBright;

            gridButtons[bestMove].BackColor =
                Color.FromArgb(
                    60,
                    25,
                    35);

            gridButtons[bestMove].FlatAppearance.BorderColor =
                aiBright;

            if (CheckWinner())
                return;

            isXTurn =
                true;

            SetCurrentTurnLabel();
        }

        private int FindBestHardMove()
        {
            int bestScore =
                int.MinValue;

            int bestMove =
                -1;

            int[] emptyIndices =
                GetEmptyIndices();

            foreach (int move
                     in emptyIndices)
            {
                gridButtons[move].Text =
                    "O";

                int score =
                    Minimax(
                        false,
                        int.MinValue,
                        int.MaxValue);

                gridButtons[move].Text =
                    "";

                if (score > bestScore)
                {
                    bestScore =
                        score;

                    bestMove =
                        move;
                }
            }

            return bestMove;
        }

        private int Minimax(
            bool isMaximizing,
            int alpha,
            int beta)
        {
            if (HasWinningCombination("O"))
                return 10;

            if (HasWinningCombination("X"))
                return -10;

            int[] emptyIndices =
                GetEmptyIndices();

            if (emptyIndices.Length == 0)
                return 0;

            if (isMaximizing)
            {
                int bestScore =
                    int.MinValue;

                foreach (int move
                         in emptyIndices)
                {
                    gridButtons[move].Text =
                        "O";

                    int score =
                        Minimax(
                            false,
                            alpha,
                            beta);

                    gridButtons[move].Text =
                        "";

                    bestScore =
                        Math.Max(
                            bestScore,
                            score);

                    alpha =
                        Math.Max(
                            alpha,
                            bestScore);

                    if (beta <= alpha)
                        break;
                }

                return bestScore;
            }

            int minimizingScore =
                int.MaxValue;

            foreach (int move
                     in emptyIndices)
            {
                gridButtons[move].Text =
                    "X";

                int score =
                    Minimax(
                        true,
                        alpha,
                        beta);

                gridButtons[move].Text =
                    "";

                minimizingScore =
                    Math.Min(
                        minimizingScore,
                        score);

                beta =
                    Math.Min(
                        beta,
                        minimizingScore);

                if (beta <= alpha)
                    break;
            }

            return minimizingScore;
        }

        private bool HasWinningCombination(
            string symbol)
        {
            int[,] winPatterns =
            {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 0, 4, 8 },
            { 2, 4, 6 }
        };

            for (int i = 0;
                 i < 8;
                 i++)
            {
                int a =
                    winPatterns[i, 0];

                int b =
                    winPatterns[i, 1];

                int c =
                    winPatterns[i, 2];

                if (gridButtons[a].Text ==
                        symbol &&
                    gridButtons[b].Text ==
                        symbol &&
                    gridButtons[c].Text ==
                        symbol)
                {
                    return true;
                }
            }

            return false;
        }

        private int FindBestMove(
            string symbol)
        {
            int[,] winPatterns =
            {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 0, 4, 8 },
            { 2, 4, 6 }
        };

            for (int i = 0;
                 i < 8;
                 i++)
            {
                int a =
                    winPatterns[i, 0];

                int b =
                    winPatterns[i, 1];

                int c =
                    winPatterns[i, 2];

                if (gridButtons[a].Text ==
                        symbol &&
                    gridButtons[b].Text ==
                        symbol &&
                    gridButtons[c].Text ==
                        "")
                {
                    return c;
                }

                if (gridButtons[a].Text ==
                        symbol &&
                    gridButtons[c].Text ==
                        symbol &&
                    gridButtons[b].Text ==
                        "")
                {
                    return b;
                }

                if (gridButtons[b].Text ==
                        symbol &&
                    gridButtons[c].Text ==
                        symbol &&
                    gridButtons[a].Text ==
                        "")
                {
                    return a;
                }
            }

            return -1;
        }

        private int[] GetEmptyIndices()
        {
            List<int> list =
                new List<int>();

            for (int i = 0;
                 i < gridButtons.Length;
                 i++)
            {
                if (gridButtons[i].Text == "")
                    list.Add(i);
            }

            return list.ToArray();
        }

        private bool CheckWinner()
        {
            int[,] winPatterns =
            {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 0, 4, 8 },
            { 2, 4, 6 }
        };

            for (int i = 0;
                 i < 8;
                 i++)
            {
                int a =
                    winPatterns[i, 0];

                int b =
                    winPatterns[i, 1];

                int c =
                    winPatterns[i, 2];

                if (!string.IsNullOrEmpty(
                        gridButtons[a].Text) &&
                    gridButtons[a].Text ==
                        gridButtons[b].Text &&
                    gridButtons[b].Text ==
                        gridButtons[c].Text)
                {
                    isGameActive =
                        false;

                    winningA =
                        a;

                    winningB =
                        b;

                    winningC =
                        c;

                    string winningSymbol =
                        gridButtons[a].Text;

                    string winnerName =
                        winningSymbol == "X"
                            ? player1Name
                            : player2Name;

                    string loserName =
                        winningSymbol == "X"
                            ? player2Name
                            : player1Name;

                    if (winningSymbol == "X")
                        xWins++;
                    else
                        oWins++;

                    HighlightWinningCells(
                        a,
                        b,
                        c,
                        winningSymbol);

                    UpdatePlayerCards();

                    SaveMatchToHistory(
                        $"{winnerName} won against {loserName}");

                    if (isTournamentMatch)
                    {
                        BeginInvoke(
                            new Action(() =>
                            {
                                tournamentWinnerCallback?.Invoke(
                                    winnerName);

                                Close();
                            }));

                        return true;
                    }

                    FileManager.UpdatePlayerStatistics(
                        player1Name,
                        player2Name,
                        winnerName);

                    ShowResultOverlay(
                        true,
                        winnerName,
                        winningSymbol);

                    Invalidate();

                    return true;
                }
            }

            if (GetEmptyIndices().Length == 0)
            {
                isGameActive =
                    false;

                draws++;

                UpdatePlayerCards();

                SaveMatchToHistory(
                    $"{player1Name} vs {player2Name} - Draw");

                if (isTournamentMatch)
                {
                    tournamentDrawCallback?.Invoke();

                    BeginInvoke(
                        new Action(() =>
                        {
                            Close();
                        }));

                    return true;
                }

                FileManager.UpdatePlayerStatistics(
                    player1Name,
                    player2Name,
                    "Draw");

                ShowResultOverlay(
                    false,
                    "",
                    "");

                return true;
            }

            return false;
        }

        private void HighlightWinningCells(
            int a,
            int b,
            int c,
            string symbol)
        {
            int[] cells =
            {
            a,
            b,
            c
        };

            foreach (int index
                     in cells)
            {
                gridButtons[index].BackColor =
                    symbol == "X"
                        ? Color.FromArgb(
                            25,
                            75,
                            110)
                        : Color.FromArgb(
                            90,
                            35,
                            45);

                gridButtons[index].FlatAppearance.BorderColor =
                    symbol == "X"
                        ? accentBright
                        : aiBright;

                gridButtons[index].FlatAppearance.BorderSize =
                    2;
            }
        }

        private void ShowResultOverlay(
            bool victory,
            string winner,
            string symbol)
        {
            if (resultShown)
                return;

            resultShown =
                true;

            matchTimer?.Stop();
            animationTimer?.Stop();

            if (victory)
            {
                lblResultTitle.Text =
                    "VICTORY";

                lblResultTitle.ForeColor =
                    symbol == "X"
                        ? accentBright
                        : aiBright;

                lblResultSub.Text =
                    $"{winner.ToUpper()} TAKES THE MATCH";

                lblResultScore.Text =
                    $"FINAL SCORE   {xWins:00}  —  {oWins:00}";
            }
            else
            {
                lblResultTitle.Text =
                    "DRAW";

                lblResultTitle.ForeColor =
                    drawColor;

                lblResultSub.Text =
                    "NO WINNER // PERFECTLY CONTESTED";

                lblResultScore.Text =
                    $"FINAL SCORE   {xWins:00}  —  {oWins:00}   //   DRAWS {draws:00}";
            }

            resultOverlay.Visible =
                true;

            resultOverlay.BringToFront();

            LayoutResultOverlay();
        }

        private void ResultReset_Click(
            object sender,
            EventArgs e)
        {
            resultOverlay.Visible =
                false;

            resultShown =
                false;

            animationTimer?.Start();

            winningA =
                -1;

            winningB =
                -1;

            winningC =
                -1;

            ResetRound();

            resultOverlay.SendToBack();

            Invalidate();
        }

        private void ResultBack_Click(
            object sender,
            EventArgs e)
        {
            resultOverlay.Visible =
                false;

            btnBack_Click(
                sender,
                e);
        }

        private void ResetRound()
        {
            isGameActive =
                true;

            isAiThinking =
                false;

            resultShown =
                false;

            winningA =
                -1;

            winningB =
                -1;

            winningC =
                -1;

            InitializeGameGrid();

            elapsedSeconds =
                0;

            if (lblTimer != null)
            {
                lblTimer.Text =
                    "TIME // 00:00";
            }

            isXTurn =
                isTournamentMatch
                    ? true
                    : tossWinner ==
                      player1Name;

            if (matchTimer != null)
                matchTimer.Start();

            UpdateLabels();

            if (isVsComputer &&
                !isXTurn &&
                isGameActive)
            {
                SetGridEnabled(false);
                isAiThinking = true;

                lblBoardSub.Text =
                    "AI CORE // CALCULATING";

                MakeComputerMove();

                isAiThinking = false;

                if (isGameActive)
                    SetGridEnabled(true);
            }

            Invalidate();
        }

        private void SaveMatchToHistory(
            string matchRecord)
        {
            if (isTournamentMatch)
            {
                FileManager.SaveTournamentMatch(
                    matchRecord);
            }
            else
            {
                FileManager.SaveMatch(
                    matchRecord);
            }
        }

        private void SetGridEnabled(
            bool enabled)
        {
            foreach (Button btn
                     in gridButtons)
            {
                if (btn != null &&
                    btn.Text == "")
                {
                    btn.Enabled =
                        enabled;
                }
            }
        }

        private void btnReset_Click(
            object sender,
            EventArgs e)
        {
            resultOverlay.Visible =
                false;

            ResetRound();
        }

        private void btnBack_Click(
            object sender,
            EventArgs e)
        {
            if (isTournamentMatch)
            {
                tournamentBackCallback?.Invoke();
                Close();
                return;
            }

            if (isVsComputer)
            {
                AISetupForm setup =
                    new AISetupForm();

                setup.Show();
                Close();
                return;
            }

            PlayerSetupForm playerSetup =
                new PlayerSetupForm();

            playerSetup.Show();
            Close();
        }

        private void btnHistory_Click(
            object sender,
            EventArgs e)
        {
            Hide();

            HistoryForm history =
                new HistoryForm(this);

            history.Show();
        }

        private void btnLeaderboard_Click(
            object sender,
            EventArgs e)
        {
            Hide();

            LeaderboardForm leaderboard =
                new LeaderboardForm(this);

            leaderboard.Show();
        }

        private void btn1_Click(
            object sender,
            EventArgs e)
        {
            HandleButtonClick(btn1);
        }

        private void btn2_Click(
            object sender,
            EventArgs e)
        {
            HandleButtonClick(btn2);
        }

        private void btn3_Click(
            object sender,
            EventArgs e)
        {
            HandleButtonClick(btn3);
        }

        private void btn4_Click(
            object sender,
            EventArgs e)
        {
            HandleButtonClick(btn4);
        }

        private void btn5_Click(
            object sender,
            EventArgs e)
        {
            HandleButtonClick(btn5);
        }

        private void btn6_Click(
            object sender,
            EventArgs e)
        {
            HandleButtonClick(btn6);
        }

        private void btn7_Click(
            object sender,
            EventArgs e)
        {
            HandleButtonClick(btn7);
        }

        private void btn8_Click(
            object sender,
            EventArgs e)
        {
            HandleButtonClick(btn8);
        }

        private void btn9_Click(
            object sender,
            EventArgs e)
        {
            HandleButtonClick(btn9);
        }

        private void GameForm_FormClosed(
            object sender,
            FormClosedEventArgs e)
        {
            animationTimer?.Stop();
            matchTimer?.Stop();
        }

        private void lblStatus_Click(
            object sender,
            EventArgs e)
        {
        }

        private void lblToss_Click(
            object sender,
            EventArgs e)
        {
        }
    }

}