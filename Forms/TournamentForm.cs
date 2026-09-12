using AdvancedTicTacToeWinForms.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AdvancedTicTacToeWinForms
{
    public class TournamentForm : Form
    {
        private List<TextBox> playerTextBoxes = new List<TextBox>();
        private List<Panel> playerCards = new List<Panel>();

        private Panel configPanel;
        private Panel rosterPanel;

        private Label lblEdition;
        private Label lblSystem;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblHeroText;
        private Label lblHeroStatus;

        private Label lblEvent;
        private TextBox txtTournamentName;
        private Label lblPlayers;
        private NumericUpDown numPlayers;
        private Label lblFormat;
        private Label lblFormatValue;

        private Label lblRosterTitle;
        private Label lblRosterSub;
        private Label lblRosterCount;

        private Button btnStart;
        private Button btnBack;

        private Button btnMinimize;
        private Button btnMaximize;
        private Button btnClose;

        private Label lblStatus;
        private Label lblFooter;

        private Timer animationTimer;
        private float glowPhase;
        private Random random = new Random();

        private List<PointF> particles = new List<PointF>();

        private Color backgroundColor = Color.FromArgb(5, 8, 14);
        private Color panelColor = Color.FromArgb(11, 16, 25);
        private Color cardColor = Color.FromArgb(14, 21, 32);
        private Color fieldColor = Color.FromArgb(18, 27, 40);

        private Color accentColor = Color.FromArgb(45, 135, 230);
        private Color accentBright = Color.FromArgb(105, 190, 255);
        private Color secondaryBlue = Color.FromArgb(70, 115, 170);

        public TournamentForm()
        {
            BuildTournamentUI();
        }

        private void BuildTournamentUI()
        {
            Text = "Tournament Mode";
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = true;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = backgroundColor;
            DoubleBuffered = true;

            for (int i = 0; i < 55; i++)
            {
                particles.Add(
                    new PointF(
                        random.Next(0, 1600),
                        random.Next(0, 1000)));
            }

            lblEdition = new Label();
            lblEdition.Text = "TACTICAL EDITION  //  05";
            lblEdition.Font = new Font(
                "Segoe UI",
                8.5F,
                FontStyle.Bold);
            lblEdition.ForeColor =
                Color.FromArgb(90, 120, 160);
            lblEdition.BackColor = Color.Transparent;
            lblEdition.AutoSize = false;
            lblEdition.TextAlign =
                ContentAlignment.MiddleLeft;
            Controls.Add(lblEdition);

            lblSystem = new Label();
            lblSystem.Text =
                "TOURNAMENT SYSTEM   //   READY";
            lblSystem.Font = new Font(
                "Consolas",
                8.5F,
                FontStyle.Bold);
            lblSystem.ForeColor =
                Color.FromArgb(80, 125, 165);
            lblSystem.BackColor = Color.Transparent;
            lblSystem.AutoSize = false;
            lblSystem.TextAlign =
                ContentAlignment.MiddleRight;
            Controls.Add(lblSystem);

            btnMinimize = CreateWindowButton("—");
            btnMaximize = CreateWindowButton("□");
            btnClose = CreateWindowButton("×");

            btnMinimize.Click += (s, e) =>
            {
                WindowState = FormWindowState.Minimized;
            };

            btnMaximize.Click += (s, e) =>
            {
                WindowState =
                    WindowState == FormWindowState.Maximized
                        ? FormWindowState.Normal
                        : FormWindowState.Maximized;

                UpdateLayout();
            };

            btnClose.Click += (s, e) =>
            {
                Close();
            };

            Controls.Add(btnMinimize);
            Controls.Add(btnMaximize);
            Controls.Add(btnClose);

            lblTitle = new Label();
            lblTitle.Text = "TOURNAMENT";
            lblTitle.Font = new Font(
                "Segoe UI",
                36F,
                FontStyle.Bold);
            lblTitle.ForeColor =
                Color.FromArgb(242, 246, 252);
            lblTitle.BackColor = Color.Transparent;
            lblTitle.AutoSize = false;
            lblTitle.TextAlign =
                ContentAlignment.MiddleLeft;
            Controls.Add(lblTitle);

            lblSubtitle = new Label();
            lblSubtitle.Text =
                "CHAMPIONSHIP CONTROL // KNOCKOUT SERIES";
            lblSubtitle.Font = new Font(
                "Segoe UI",
                9.5F,
                FontStyle.Bold);
            lblSubtitle.ForeColor = accentBright;
            lblSubtitle.BackColor = Color.Transparent;
            lblSubtitle.AutoSize = false;
            lblSubtitle.TextAlign =
                ContentAlignment.MiddleLeft;
            Controls.Add(lblSubtitle);

            lblHeroText = new Label();
            lblHeroText.Text =
                "BUILD YOUR BATTLE.\r\nREGISTER YOUR CHALLENGERS.\r\nCROWN THE CHAMPION.";
            lblHeroText.Font = new Font(
                "Segoe UI",
                15F,
                FontStyle.Bold);
            lblHeroText.ForeColor =
                Color.FromArgb(190, 202, 220);
            lblHeroText.BackColor = Color.Transparent;
            lblHeroText.AutoSize = false;
            lblHeroText.TextAlign =
                ContentAlignment.MiddleLeft;
            Controls.Add(lblHeroText);

            lblHeroStatus = new Label();
            lblHeroStatus.Text =
                "MATCH ENGINE  //  KNOCKOUT";
            lblHeroStatus.Font = new Font(
                "Consolas",
                8.5F,
                FontStyle.Bold);
            lblHeroStatus.ForeColor =
                Color.FromArgb(75, 115, 155);
            lblHeroStatus.BackColor = Color.Transparent;
            lblHeroStatus.AutoSize = false;
            Controls.Add(lblHeroStatus);

            configPanel = new Panel();
            configPanel.BackColor = panelColor;
            configPanel.Paint += ConfigPanel_Paint;
            Controls.Add(configPanel);

            lblEvent = new Label();
            lblEvent.Text = "EVENT NAME";
            lblEvent.Font = new Font(
                "Segoe UI",
                8F,
                FontStyle.Bold);
            lblEvent.ForeColor =
                Color.FromArgb(115, 140, 170);
            lblEvent.BackColor = Color.Transparent;
            configPanel.Controls.Add(lblEvent);

            txtTournamentName = new TextBox();
            txtTournamentName.Name =
                "txtTournamentName";
            txtTournamentName.Text =
                "TIC TAC TOE CHAMPIONSHIP";
            txtTournamentName.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold);
            txtTournamentName.ForeColor =
                Color.FromArgb(235, 240, 250);
            txtTournamentName.BackColor =
                fieldColor;
            txtTournamentName.BorderStyle =
                BorderStyle.FixedSingle;
            txtTournamentName.Enter +=
                TournamentName_Enter;
            txtTournamentName.Leave +=
                TournamentName_Leave;
            configPanel.Controls.Add(txtTournamentName);

            lblPlayers = new Label();
            lblPlayers.Text = "PLAYERS";
            lblPlayers.Font = new Font(
                "Segoe UI",
                8F,
                FontStyle.Bold);
            lblPlayers.ForeColor =
                Color.FromArgb(115, 140, 170);
            lblPlayers.BackColor = Color.Transparent;
            configPanel.Controls.Add(lblPlayers);

            numPlayers = new NumericUpDown();
            numPlayers.Name = "numPlayers";
            numPlayers.Minimum = 2;
            numPlayers.Maximum = 16;
            numPlayers.Value = 4;
            numPlayers.Font = new Font(
                "Segoe UI",
                12F,
                FontStyle.Bold);
            numPlayers.ForeColor =
                Color.FromArgb(235, 240, 250);
            numPlayers.BackColor =
                fieldColor;
            numPlayers.BorderStyle =
                BorderStyle.FixedSingle;
            configPanel.Controls.Add(numPlayers);

            lblFormat = new Label();
            lblFormat.Text = "FORMAT";
            lblFormat.Font = new Font(
                "Segoe UI",
                8F,
                FontStyle.Bold);
            lblFormat.ForeColor =
                Color.FromArgb(115, 140, 170);
            lblFormat.BackColor = Color.Transparent;
            configPanel.Controls.Add(lblFormat);

            lblFormatValue = new Label();
            lblFormatValue.Text = "KNOCKOUT";
            lblFormatValue.Font = new Font(
                "Consolas",
                9F,
                FontStyle.Bold);
            lblFormatValue.ForeColor =
                accentBright;
            lblFormatValue.BackColor =
                Color.Transparent;
            configPanel.Controls.Add(lblFormatValue);

            rosterPanel = new Panel();
            rosterPanel.BackColor = panelColor;
            rosterPanel.Paint += RosterPanel_Paint;
            Controls.Add(rosterPanel);

            lblRosterTitle = new Label();
            lblRosterTitle.Text = "PLAYER ROSTER";
            lblRosterTitle.Font = new Font(
                "Segoe UI",
                16F,
                FontStyle.Bold);
            lblRosterTitle.ForeColor =
                Color.FromArgb(238, 243, 252);
            lblRosterTitle.BackColor =
                Color.Transparent;
            rosterPanel.Controls.Add(lblRosterTitle);

            lblRosterSub = new Label();
            lblRosterSub.Text =
                "REGISTER COMPETITORS FOR THE CHAMPIONSHIP";
            lblRosterSub.Font = new Font(
                "Consolas",
                8F,
                FontStyle.Bold);
            lblRosterSub.ForeColor =
                Color.FromArgb(82, 108, 138);
            lblRosterSub.BackColor =
                Color.Transparent;
            rosterPanel.Controls.Add(lblRosterSub);

            lblRosterCount = new Label();
            lblRosterCount.Text = "04 / 16";
            lblRosterCount.Font = new Font(
                "Consolas",
                9F,
                FontStyle.Bold);
            lblRosterCount.ForeColor =
                accentBright;
            lblRosterCount.BackColor =
                Color.Transparent;
            lblRosterCount.AutoSize = false;
            lblRosterCount.TextAlign =
                ContentAlignment.MiddleRight;
            rosterPanel.Controls.Add(lblRosterCount);

            GeneratePlayerFields(4);

            btnStart = new Button();
            btnStart.Text = "START CHAMPIONSHIP";
            btnStart.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.BackColor = accentColor;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Cursor = Cursors.Hand;
            btnStart.Click += btnStart_Click;
            btnStart.Paint += StartButton_Paint;
            btnStart.MouseEnter += StartButton_MouseEnter;
            btnStart.MouseLeave += StartButton_MouseLeave;
            Controls.Add(btnStart);

            btnBack = new Button();
            btnBack.Text = "BACK";
            btnBack.Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold);
            btnBack.ForeColor =
                Color.FromArgb(215, 223, 235);
            btnBack.BackColor =
                Color.FromArgb(23, 30, 42);
            btnBack.FlatStyle =
                FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 1;
            btnBack.FlatAppearance.BorderColor =
                Color.FromArgb(57, 72, 94);
            btnBack.Cursor = Cursors.Hand;
            btnBack.Click += btnBack_Click;
            btnBack.MouseEnter += BackButton_MouseEnter;
            btnBack.MouseLeave += BackButton_MouseLeave;
            Controls.Add(btnBack);

            lblStatus = new Label();
            lblStatus.Text =
                "SYSTEM ONLINE   //   04 PLAYERS READY";
            lblStatus.Font = new Font(
                "Consolas",
                8F,
                FontStyle.Bold);
            lblStatus.ForeColor =
                Color.FromArgb(70, 105, 138);
            lblStatus.BackColor =
                backgroundColor;
            lblStatus.AutoSize = false;
            lblStatus.TextAlign =
                ContentAlignment.MiddleLeft;
            Controls.Add(lblStatus);

            lblFooter = new Label();
            lblFooter.Text =
                "DEVELOPED BY SYED HUSSAIN ABBAS";
            lblFooter.Font = new Font(
                "Segoe UI",
                8F,
                FontStyle.Bold);
            lblFooter.ForeColor =
                Color.FromArgb(85, 100, 125);
            lblFooter.BackColor =
                backgroundColor;
            lblFooter.AutoSize = false;
            lblFooter.TextAlign =
                ContentAlignment.MiddleCenter;
            Controls.Add(lblFooter);

            numPlayers.ValueChanged +=
                numPlayers_ValueChanged;

            animationTimer = new Timer();
            animationTimer.Interval = 35;
            animationTimer.Tick +=
                AnimationTimer_Tick;
            animationTimer.Start();

            Paint += TournamentForm_Paint;
            Resize += TournamentForm_Resize;

            UpdateLayout();
        }

        private Button CreateWindowButton(string text)
        {
            Button button = new Button();

            button.Text = text;
            button.Font = new Font(
                "Segoe UI",
                text == "×" ? 12F : 10F,
                FontStyle.Regular);
            button.ForeColor =
                Color.FromArgb(145, 160, 180);
            button.BackColor =
                backgroundColor;
            button.FlatStyle =
                FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.Size =
                new Size(34, 28);

            button.MouseEnter +=
                (s, e) =>
                {
                    button.ForeColor =
                        text == "×"
                            ? Color.FromArgb(235, 90, 100)
                            : Color.White;
                    button.BackColor =
                        Color.FromArgb(18, 25, 37);
                };

            button.MouseLeave +=
                (s, e) =>
                {
                    button.ForeColor =
                        Color.FromArgb(145, 160, 180);
                    button.BackColor =
                        backgroundColor;
                };

            return button;
        }

        private void UpdateLayout()
        {
            int width = ClientSize.Width;
            int height = ClientSize.Height;

            if (width <= 1 || height <= 1)
                return;

            int margin = Math.Max(
                42,
                width / 22);

            lblEdition.Location =
                new Point(margin, 22);
            lblEdition.Size =
                new Size(300, 24);

            lblSystem.Location =
                new Point(
                    width - margin - 290,
                    22);
            lblSystem.Size =
                new Size(205, 24);

            btnMinimize.Location =
                new Point(
                    width - margin - 102,
                    18);

            btnMaximize.Location =
                new Point(
                    width - margin - 68,
                    18);

            btnClose.Location =
                new Point(
                    width - margin - 34,
                    18);

            lblTitle.Location =
                new Point(margin, 57);
            lblTitle.Size =
                new Size(620, 60);

            lblSubtitle.Location =
                new Point(
                    margin + 2,
                    112);
            lblSubtitle.Size =
                new Size(560, 24);

            lblHeroText.Location =
                new Point(
                    margin,
                    150);
            lblHeroText.Size =
                new Size(390, 105);

            lblHeroStatus.Location =
                new Point(
                    margin,
                    252);
            lblHeroStatus.Size =
                new Size(350, 22);

            int configWidth =
                Math.Min(
                    530,
                    Math.Max(
                        450,
                        width / 3));

            configPanel.Location =
                new Point(
                    width - margin - configWidth,
                    58);

            configPanel.Size =
                new Size(
                    configWidth,
                    190);

            lblEvent.Location =
                new Point(24, 22);
            lblEvent.Size =
                new Size(180, 20);

            txtTournamentName.Location =
                new Point(24, 48);
            txtTournamentName.Size =
                new Size(
                    configWidth - 48,
                    34);

            int configColumn =
                configWidth / 3;

            lblPlayers.Location =
                new Point(
                    24,
                    105);

            numPlayers.Location =
                new Point(
                    24,
                    130);
            numPlayers.Size =
                new Size(85, 32);

            lblFormat.Location =
                new Point(
                    configColumn + 5,
                    105);

            lblFormatValue.Location =
                new Point(
                    configColumn + 5,
                    130);

            int rosterY = 285;

            rosterPanel.Location =
                new Point(
                    margin,
                    rosterY);

            rosterPanel.Size =
                new Size(
                    width - margin * 2,
                    Math.Max(
                        330,
                        height - rosterY - 145));

            lblRosterTitle.Location =
                new Point(26, 18);
            lblRosterTitle.Size =
                new Size(300, 32);

            lblRosterSub.Location =
                new Point(28, 49);
            lblRosterSub.Size =
                new Size(450, 20);

            lblRosterCount.Location =
                new Point(
                    rosterPanel.Width - 155,
                    22);
            lblRosterCount.Size =
                new Size(125, 25);

            LayoutPlayerFields();

            int buttonY =
                height - 91;

            btnStart.Size =
                new Size(235, 48);
            btnStart.Location =
                new Point(
                    width / 2 - 125,
                    buttonY);

            btnBack.Size =
                new Size(105, 44);
            btnBack.Location =
                new Point(
                    width / 2 + 120,
                    buttonY + 2);

            lblStatus.Location =
                new Point(
                    margin,
                    height - 42);
            lblStatus.Size =
                new Size(330, 20);

            lblFooter.Location =
                new Point(
                    width / 2 - 250,
                    height - 42);
            lblFooter.Size =
                new Size(500, 20);

            lblEdition.BringToFront();
            lblSystem.BringToFront();
            btnMinimize.BringToFront();
            btnMaximize.BringToFront();
            btnClose.BringToFront();
            lblTitle.BringToFront();
            lblSubtitle.BringToFront();
            lblHeroText.BringToFront();
            lblHeroStatus.BringToFront();
            btnStart.BringToFront();
            btnBack.BringToFront();
            lblStatus.BringToFront();
            lblFooter.BringToFront();
        }

        private void LayoutPlayerFields()
        {
            if (rosterPanel == null)
                return;

            int count = playerTextBoxes.Count;

            int availableWidth = rosterPanel.Width - 56;
            int columnGap = 26;

            int columnWidth =
                (availableWidth - columnGap) / 2;

            int startY = 82;

            int availableHeight =
                rosterPanel.Height - startY - 18;

            int rows =
                (count + 1) / 2;

            int rowHeight =
                rows > 0
                    ? availableHeight / rows
                    : 36;

            rowHeight =
                Math.Max(
                    30,
                    Math.Min(
                        rowHeight,
                        47));

            for (int i = 0;
                 i < playerTextBoxes.Count;
                 i++)
            {
                TextBox box =
                    playerTextBoxes[i];

                Panel card =
                    playerCards[i];

                int column = i % 2;
                int row = i / 2;

                int x =
                    28 +
                    column *
                    (columnWidth + columnGap);

                int y =
                    startY +
                    row * rowHeight;

                card.Location =
                    new Point(
                        x,
                        y);

                card.Size =
                    new Size(
                        columnWidth,
                        rowHeight - 7);

                box.Location =
                    new Point(
                        92,
                        4);

                box.Size =
                    new Size(
                        Math.Max(
                            150,
                            columnWidth - 102),
                        Math.Max(
                            20,
                            rowHeight - 15));

                box.BringToFront();
            }

        }
        private void GeneratePlayerFields(int count)
        {
            foreach (Panel card in playerCards.ToList())
            {
                card.Dispose();
            }

            playerCards.Clear();

            playerTextBoxes.Clear();

            for (int i = 0; i < count; i++)
            {
                Panel card =
                    new Panel();

                card.Name =
                    $"playerCard{i + 1}";

                card.BackColor =
                    cardColor;

                card.Paint +=
                    PlayerCard_Paint;

                card.Tag = i;

                rosterPanel.Controls.Add(card);
                playerCards.Add(card);

                Label number =
                    new Label();

                number.Name =
                    $"lblPlayer{i + 1}";

                number.Text =
                    $"{i + 1:00}";

                number.Font =
                    new Font(
                        "Consolas",
                        9F,
                        FontStyle.Bold);

                number.ForeColor =
                    i % 2 == 0
                        ? Color.FromArgb(
                            95,
                            170,
                            235)
                        : Color.FromArgb(
                            105,
                            130,
                            160);

                number.BackColor =
                    Color.Transparent;

                number.Location =
                    new Point(
                        17,
                        5);

                number.Size =
                    new Size(
                        55,
                        25);

                number.TextAlign =
                    ContentAlignment.MiddleLeft;

                card.Controls.Add(number);

                Label status =
                    new Label();

                status.Text = "READY";
                status.Font =
                    new Font(
                        "Consolas",
                        6.5F,
                        FontStyle.Bold);

                status.ForeColor =
                    Color.FromArgb(
                        65,
                        105,
                        140);

                status.BackColor =
                    Color.Transparent;

                status.AutoSize = false;

                status.TextAlign =
                    ContentAlignment.MiddleRight;

                status.Location =
                    new Point(
                        card.Width - 70,
                        5);

                status.Size =
                    new Size(
                        52,
                        22);

                status.Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right;

                card.Controls.Add(status);

                TextBox playerTextBox =
                    new TextBox();

                playerTextBox.Name =
                    $"txtPlayer{i + 1}";

                playerTextBox.Font =
                    new Font(
                        "Segoe UI",
                        9.5F,
                        FontStyle.Regular);

                playerTextBox.ForeColor =
                    Color.FromArgb(
                        235,
                        240,
                        250);

                playerTextBox.BackColor =
                    fieldColor;

                playerTextBox.BorderStyle =
                    BorderStyle.FixedSingle;

                playerTextBox.Enter +=
                    PlayerTextBox_Enter;

                playerTextBox.Leave +=
                    PlayerTextBox_Leave;

                card.Controls.Add(
                    playerTextBox);

                playerTextBoxes.Add(
                    playerTextBox);
            }

            LayoutPlayerFields();
        }

        private void PlayerCard_Paint(
            object sender,
            PaintEventArgs e)
        {
            Panel card =
                sender as Panel;

            if (card == null)
                return;

            Graphics g =
                e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            int index =
                card.Tag is int
                    ? (int)card.Tag
                    : 0;

            Color lineColor =
                index % 2 == 0
                    ? Color.FromArgb(
                        42,
                        85,
                        125)
                    : Color.FromArgb(
                        35,
                        60,
                        88);

            using (Pen borderPen =
                new Pen(
                    lineColor,
                    1))
            {
                g.DrawRectangle(
                    borderPen,
                    0,
                    0,
                    card.Width - 1,
                    card.Height - 1);
            }

            using (SolidBrush accentBrush =
                new SolidBrush(
                    index % 2 == 0
                        ? Color.FromArgb(
                            55,
                            145,
                            235)
                        : Color.FromArgb(
                            40,
                            95,
                            145)))
            {
                g.FillRectangle(
                    accentBrush,
                    0,
                    0,
                    3,
                    card.Height);
            }
        }

        private void PlayerTextBox_Enter(
            object sender,
            EventArgs e)
        {
            TextBox box =
                sender as TextBox;

            if (box == null)
                return;

            box.BackColor =
                Color.FromArgb(
                    24,
                    39,
                    57);

            Control parent =
                box.Parent;

            parent?.Invalidate();
        }

        private void PlayerTextBox_Leave(
            object sender,
            EventArgs e)
        {
            TextBox box =
                sender as TextBox;

            if (box == null)
                return;

            box.BackColor =
                fieldColor;

            Control parent =
                box.Parent;

            parent?.Invalidate();
        }

        private void TournamentName_Enter(
            object sender,
            EventArgs e)
        {
            txtTournamentName.BackColor =
                Color.FromArgb(
                    24,
                    39,
                    57);
        }

        private void TournamentName_Leave(
            object sender,
            EventArgs e)
        {
            txtTournamentName.BackColor =
                fieldColor;
        }

        private void numPlayers_ValueChanged(
            object sender,
            EventArgs e)
        {
            int playerCount =
                (int)numPlayers.Value;

            GeneratePlayerFields(
                playerCount);

            lblStatus.Text =
                $"SYSTEM ONLINE   //   {playerCount:00} PLAYERS READY";

            lblRosterCount.Text =
                $"{playerCount:00} / 16";

            lblFormatValue.Text =
                playerCount % 2 == 0
                    ? "KNOCKOUT"
                    : "KNOCKOUT + BYE";

            UpdateLayout();
        }

        private void btnStart_Click(
            object sender,
            EventArgs e)
        {
            string tournamentName =
                txtTournamentName.Text.Trim();

            if (string.IsNullOrWhiteSpace(
                tournamentName))
            {
                tournamentName =
                    "Tic Tac Toe Championship";
            }

            Tournament tournament =
                new Tournament(
                    tournamentName);

            HashSet<string> usedNames =
                new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase);

            foreach (TextBox textBox
                     in playerTextBoxes)
            {
                string playerName =
                    textBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(
                    playerName))
                {
                    MessageBox.Show(
                        "Please enter all player names.",
                        "Missing Player Name",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    textBox.Focus();
                    return;
                }

                if (usedNames.Contains(
                    playerName))
                {
                    MessageBox.Show(
                        $"Player name '{playerName}' is already used.",
                        "Duplicate Player",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    textBox.Focus();
                    return;
                }

                usedNames.Add(
                    playerName);

                tournament.AddPlayer(
                    playerName);
            }

            tournament.ShufflePlayers();
            tournament.GenerateFirstRound();

            BracketForm bracket =
                new BracketForm(
                    tournament);

            bracket.Show();
            Hide();
        }

        private void btnBack_Click(
            object sender,
            EventArgs e)
        {
            MainMenuForm menu =
                new MainMenuForm();

            menu.Show();
            Close();
        }

        private void StartButton_MouseEnter(
            object sender,
            EventArgs e)
        {
            btnStart.BackColor =
                Color.FromArgb(
                    65,
                    160,
                    245);
        }

        private void StartButton_MouseLeave(
            object sender,
            EventArgs e)
        {
            btnStart.BackColor =
                accentColor;
        }

        private void BackButton_MouseEnter(
            object sender,
            EventArgs e)
        {
            btnBack.BackColor =
                Color.FromArgb(
                    38,
                    50,
                    67);
        }

        private void BackButton_MouseLeave(
            object sender,
            EventArgs e)
        {
            btnBack.BackColor =
                Color.FromArgb(
                    23,
                    30,
                    42);
        }

        private void StartButton_Paint(
            object sender,
            PaintEventArgs e)
        {
            Button button =
                sender as Button;

            if (button == null)
                return;

            Graphics g =
                e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            using (Pen pen =
                new Pen(
                    accentBright,
                    1))
            {
                Point[] points =
                {
                new Point(0, 0),
                new Point(
                    button.Width - 16,
                    0),
                new Point(
                    button.Width,
                    16),
                new Point(
                    button.Width,
                    button.Height),
                new Point(
                    16,
                    button.Height),
                new Point(
                    0,
                    button.Height - 16)
            };

                g.DrawPolygon(
                    pen,
                    points);
            }
        }

        private void ConfigPanel_Paint(
            object sender,
            PaintEventArgs e)
        {
            Graphics g =
                e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            float pulse =
                (float)(
                    (Math.Sin(glowPhase) + 1) *
                    0.5);

            using (Pen borderPen =
                new Pen(
                    Color.FromArgb(
                        38,
                        55,
                        76),
                    1))
            {
                g.DrawRectangle(
                    borderPen,
                    0,
                    0,
                    configPanel.Width - 1,
                    configPanel.Height - 1);
            }

            using (Pen accentPen =
                new Pen(
                    Color.FromArgb(
                        60 +
                        (int)(pulse * 30),
                        145,
                        235),
                    2))
            {
                g.DrawLine(
                    accentPen,
                    0,
                    0,
                    125,
                    0);
            }

            using (Pen sidePen =
                new Pen(
                    Color.FromArgb(
                        35,
                        65,
                        100),
                    1))
            {
                g.DrawLine(
                    sidePen,
                    0,
                    0,
                    0,
                    configPanel.Height);
            }
        }

        private void RosterPanel_Paint(
            object sender,
            PaintEventArgs e)
        {
            Graphics g =
                e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            float pulse =
                (float)(
                    (Math.Sin(glowPhase) + 1) *
                    0.5);

            using (Pen borderPen =
                new Pen(
                    Color.FromArgb(
                        38,
                        55,
                        76),
                    1))
            {
                g.DrawRectangle(
                    borderPen,
                    0,
                    0,
                    rosterPanel.Width - 1,
                    rosterPanel.Height - 1);
            }

            using (Pen accentPen =
                new Pen(
                    Color.FromArgb(
                        55 +
                        (int)(pulse * 25),
                        145,
                        235),
                    2))
            {
                g.DrawLine(
                    accentPen,
                    0,
                    0,
                    155,
                    0);
            }

            using (Pen separatorPen =
                new Pen(
                    Color.FromArgb(
                        29,
                        42,
                        58),
                    1))
            {
                g.DrawLine(
                    separatorPen,
                    28,
                    70,
                    rosterPanel.Width - 28,
                    70);
            }
        }

        private void TournamentForm_Paint(
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

            using (LinearGradientBrush brush =
                new LinearGradientBrush(
                    ClientRectangle,
                    Color.FromArgb(
                        3,
                        6,
                        11),
                    Color.FromArgb(
                        10,
                        17,
                        27),
                    90F))
            {
                g.FillRectangle(
                    brush,
                    ClientRectangle);
            }

            using (Pen gridPen =
                new Pen(
                    Color.FromArgb(
                        10,
                        18,
                        28),
                    1))
            {
                for (
                    int x = 0;
                    x < Width;
                    x += 48)
                {
                    g.DrawLine(
                        gridPen,
                        x,
                        0,
                        x,
                        Height);
                }

                for (
                    int y = 0;
                    y < Height;
                    y += 48)
                {
                    g.DrawLine(
                        gridPen,
                        0,
                        y,
                        Width,
                        y);
                }
            }

            float pulse =
                (float)(
                    (Math.Sin(glowPhase) + 1) *
                    0.5);

            using (Pen topGlow =
                new Pen(
                    Color.FromArgb(
                        35 +
                        (int)(pulse * 35),
                        55,
                        105),
                    1))
            {
                g.DrawLine(
                    topGlow,
                    40,
                    145,
                    Width - 40,
                    145);
            }

            using (Pen heroLine =
                new Pen(
                    Color.FromArgb(
                        30,
                        60,
                        95),
                    1))
            {
                g.DrawLine(
                    heroLine,
                    40,
                    270,
                    Width - 40,
                    270);
            }

            foreach (PointF particle
                     in particles)
            {
                using (SolidBrush particleBrush =
                    new SolidBrush(
                        Color.FromArgb(
                            35,
                            95,
                            155)))
                {
                    g.FillEllipse(
                        particleBrush,
                        particle.X % Math.Max(
                            1,
                            Width),
                        particle.Y % Math.Max(
                            1,
                            Height),
                        2,
                        2);
                }
            }

            using (Pen borderPen =
                new Pen(
                    Color.FromArgb(
                        27,
                        40,
                        57),
                    1))
            {
                g.DrawRectangle(
                    borderPen,
                    18,
                    15,
                    Width - 36,
                    Height - 30);
            }

            using (Pen cornerPen =
                new Pen(
                    Color.FromArgb(
                        60,
                        145,
                        225),
                    2))
            {
                g.DrawLine(
                    cornerPen,
                    18,
                    15,
                    85,
                    15);

                g.DrawLine(
                    cornerPen,
                    18,
                    15,
                    18,
                    70);
            }
        }

        private void AnimationTimer_Tick(
            object sender,
            EventArgs e)
        {
            glowPhase += 0.045F;

            if (glowPhase >
                Math.PI * 2)
            {
                glowPhase = 0;
            }

            Invalidate(
                new Rectangle(
                    0,
                    0,
                    ClientSize.Width,
                    Math.Max(
                        1,
                        ClientSize.Height - 45)));
        }

        private void TournamentForm_Resize(
            object sender,
            EventArgs e)
        {
            if (ClientSize.Width <= 1 ||
                ClientSize.Height <= 1)
                return;

            UpdateLayout();
            Invalidate();
        }

        protected override void OnFormClosed(
            FormClosedEventArgs e)
        {
            animationTimer?.Stop();
            animationTimer?.Dispose();

            base.OnFormClosed(e);
        }

        private void TournamentForm_Load(
            object sender,
            EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TournamentForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "TournamentForm";
            this.Load += new System.EventHandler(this.TournamentForm_Load_1);
            this.ResumeLayout(false);

        }

        private void TournamentForm_Load_1(object sender, EventArgs e)
        {

        }
    }

}