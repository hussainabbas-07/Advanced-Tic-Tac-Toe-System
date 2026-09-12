using AdvancedTicTacToeWinForms.Classes;
using AdvancedTicTacToeWinForms.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AdvancedTicTacToeWinForms
{
    public partial class BracketForm : Form
    {
        private Tournament tournament;
        private Panel bracketPanel;
        private Label lblTitle;
        private Label lblTournamentInfo;
        private Label lblLive;
        private Label lblFooter;
        private Label lblEdition;
        private Label lblStatus;
        private Button btnBack;
        private Timer animationTimer;

        private Panel resultOverlay;
        private Label lblResultEdition;
        private Label lblResultTitle;
        private Label lblResultChampion;
        private Label lblResultSub;
        private Label lblResultLine;
        private Button btnViewBracket;
        private Button btnReturnTournament;

        private readonly Dictionary<int, Button> matchButtons =
            new Dictionary<int, Button>();

        private readonly Dictionary<int, Panel> matchCards =
            new Dictionary<int, Panel>();

        private readonly Dictionary<int, Point> cardCenters =
            new Dictionary<int, Point>();

        private float animationOffset;
        private int pulseValue;
        private bool pulseIncreasing = true;

        private Color BackgroundColor =
            Color.FromArgb(7, 10, 16);

        private Color BackgroundSecondary =
            Color.FromArgb(13, 18, 28);

        private Color PanelColor =
            Color.FromArgb(11, 16, 25);

        private Color CardColor =
            Color.FromArgb(17, 23, 34);

        private Color CardHoverColor =
            Color.FromArgb(24, 33, 47);

        private Color BorderColor =
            Color.FromArgb(43, 56, 75);

        private Color AccentColor =
            Color.FromArgb(65, 155, 255);

        private Color AccentBright =
            Color.FromArgb(105, 190, 255);

        private Color SuccessColor =
            Color.FromArgb(55, 215, 135);

        private Color GoldColor =
            Color.FromArgb(245, 195, 75);

        private Color MutedColor =
            Color.FromArgb(105, 118, 138);

        private Color TextColor =
            Color.FromArgb(235, 240, 248);

        public BracketForm(Tournament tournament)
        {
            this.tournament = tournament;

            BuildBracketUI();
            BuildBracket();

            animationTimer = new Timer();
            animationTimer.Interval = 35;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void BuildBracketUI()
        {
            Text = "Tournament Bracket";
            ClientSize = new Size(1450, 850);
            MinimumSize = new Size(1150, 700);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = true;
            MinimizeBox = true;
            BackColor = BackgroundColor;
            DoubleBuffered = true;

            Paint += BracketForm_Paint;
            Resize += BracketForm_Resize;

            lblEdition = new Label();
            lblEdition.Text = "TACTICAL EDITION // 05";
            lblEdition.Font =
                new Font("Consolas", 9F, FontStyle.Bold);
            lblEdition.ForeColor =
                Color.FromArgb(100, 125, 155);
            lblEdition.BackColor =
                Color.Transparent;
            lblEdition.TextAlign =
                ContentAlignment.MiddleLeft;
            Controls.Add(lblEdition);

            lblTitle = new Label();
            lblTitle.Text = "TOURNAMENT BRACKET";
            lblTitle.Font =
                new Font("Segoe UI", 27F, FontStyle.Bold);
            lblTitle.ForeColor =
                TextColor;
            lblTitle.BackColor =
                Color.Transparent;
            lblTitle.TextAlign =
                ContentAlignment.MiddleLeft;
            Controls.Add(lblTitle);

            lblTournamentInfo = new Label();
            lblTournamentInfo.Text =
                string.IsNullOrWhiteSpace(
                    tournament?.TournamentName)
                    ? "CHAMPIONSHIP"
                    : tournament.TournamentName.ToUpper();

            lblTournamentInfo.Font =
                new Font("Consolas", 10F, FontStyle.Bold);

            lblTournamentInfo.ForeColor =
                AccentBright;

            lblTournamentInfo.BackColor =
                Color.Transparent;

            lblTournamentInfo.TextAlign =
                ContentAlignment.MiddleLeft;

            Controls.Add(lblTournamentInfo);

            lblLive = new Label();
            lblLive.Text = "●  LIVE BRACKET";
            lblLive.Font =
                new Font("Consolas", 9F, FontStyle.Bold);
            lblLive.ForeColor =
                SuccessColor;
            lblLive.BackColor =
                Color.Transparent;
            lblLive.TextAlign =
                ContentAlignment.MiddleCenter;
            Controls.Add(lblLive);

            lblStatus = new Label();
            lblStatus.Text = "SYSTEM READY";
            lblStatus.Font =
                new Font("Consolas", 8F, FontStyle.Bold);
            lblStatus.ForeColor =
                MutedColor;
            lblStatus.BackColor =
                Color.Transparent;
            lblStatus.TextAlign =
                ContentAlignment.MiddleRight;
            Controls.Add(lblStatus);

            bracketPanel = new Panel();
            bracketPanel.BackColor =
                PanelColor;
            bracketPanel.BorderStyle =
                BorderStyle.None;
            bracketPanel.AutoScroll = true;
            bracketPanel.DoubleBuffered(true);
            bracketPanel.Paint += BracketPanel_Paint;
            Controls.Add(bracketPanel);

            btnBack = new Button();
            btnBack.Text = "RETURN TO TOURNAMENT";
            btnBack.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBack.ForeColor =
                TextColor;
            btnBack.BackColor =
                Color.FromArgb(25, 32, 44);
            btnBack.FlatStyle =
                FlatStyle.Flat;
            btnBack.FlatAppearance.BorderColor =
                Color.FromArgb(60, 73, 94);
            btnBack.FlatAppearance.BorderSize = 1;
            btnBack.Cursor =
                Cursors.Hand;

            btnBack.Click += BtnBack_Click;
            btnBack.MouseEnter += BtnBack_MouseEnter;
            btnBack.MouseLeave += BtnBack_MouseLeave;
            btnBack.MouseDown += BtnBack_MouseDown;
            btnBack.MouseUp += BtnBack_MouseUp;

            Controls.Add(btnBack);

            Label topLine = new Label();
            topLine.Text =
                "KNOCKOUT SERIES  //  ELIMINATION CONTROL";
            topLine.Font =
                new Font("Consolas", 8F, FontStyle.Bold);
            topLine.ForeColor =
                Color.FromArgb(80, 100, 125);
            topLine.BackColor =
                Color.Transparent;
            topLine.TextAlign =
                ContentAlignment.MiddleRight;
            topLine.Name = "lblTopLine";
            Controls.Add(topLine);

            Label windowClose =
                CreateWindowButton("×");

            windowClose.Name =
                "windowClose";

            windowClose.Click +=
                (s, e) => Close();

            Controls.Add(windowClose);

            Label windowMax =
                CreateWindowButton("□");

            windowMax.Name =
                "windowMax";

            windowMax.Click +=
                (s, e) =>
                {
                    WindowState =
                        WindowState ==
                        FormWindowState.Maximized
                            ? FormWindowState.Normal
                            : FormWindowState.Maximized;
                };

            Controls.Add(windowMax);

            Label windowMin =
                CreateWindowButton("—");

            windowMin.Name =
                "windowMin";

            windowMin.Click +=
                (s, e) =>
                {
                    WindowState =
                        FormWindowState.Minimized;
                };

            Controls.Add(windowMin);

            lblFooter = new Label();
            lblFooter.Text =
                "DEVELOPED BY SYED HUSSAIN ABBAS";
            lblFooter.Font =
                new Font("Consolas", 8F, FontStyle.Bold);
            lblFooter.ForeColor =
                Color.FromArgb(82, 96, 116);
            lblFooter.BackColor =
                BackgroundColor;
            lblFooter.TextAlign =
                ContentAlignment.MiddleCenter;
            Controls.Add(lblFooter);

            BuildResultOverlay();

            UpdateLayout();
        }

        private Label CreateWindowButton(string text)
        {
            Label label = new Label();

            label.Text = text;
            label.Font =
                new Font(
                    "Segoe UI",
                    11F,
                    FontStyle.Regular);

            label.ForeColor =
                Color.FromArgb(
                    135,
                    150,
                    170);

            label.BackColor =
                Color.Transparent;

            label.TextAlign =
                ContentAlignment.MiddleCenter;

            label.Cursor =
                Cursors.Hand;

            label.MouseEnter +=
                (s, e) =>
                {
                    label.ForeColor =
                        Color.White;
                };

            label.MouseLeave +=
                (s, e) =>
                {
                    label.ForeColor =
                        Color.FromArgb(
                            135,
                            150,
                            170);
                };

            return label;
        }

        private void BuildResultOverlay()
        {
            resultOverlay = new Panel();

            resultOverlay.BackColor =
                Color.FromArgb(
                    16,
                    21,
                    31);

            resultOverlay.BorderStyle =
                BorderStyle.FixedSingle;

            resultOverlay.Visible =
                false;

            resultOverlay.Paint +=
                ResultOverlay_Paint;

            Controls.Add(
                resultOverlay);

            lblResultEdition = new Label();
            lblResultEdition.Text =
                "TOURNAMENT COMPLETE  //  FINAL";
            lblResultEdition.Font =
                new Font(
                    "Consolas",
                    8F,
                    FontStyle.Bold);
            lblResultEdition.ForeColor =
                Color.FromArgb(
                    110,
                    125,
                    150);
            lblResultEdition.BackColor =
                Color.Transparent;
            lblResultEdition.TextAlign =
                ContentAlignment.MiddleCenter;
            resultOverlay.Controls.Add(
                lblResultEdition);

            lblResultTitle = new Label();
            lblResultTitle.Text =
                "CHAMPION";
            lblResultTitle.Font =
                new Font(
                    "Segoe UI",
                    30F,
                    FontStyle.Bold);
            lblResultTitle.ForeColor =
                GoldColor;
            lblResultTitle.BackColor =
                Color.Transparent;
            lblResultTitle.TextAlign =
                ContentAlignment.MiddleCenter;
            resultOverlay.Controls.Add(
                lblResultTitle);

            lblResultChampion = new Label();
            lblResultChampion.Text =
                "PLAYER";
            lblResultChampion.Font =
                new Font(
                    "Segoe UI",
                    24F,
                    FontStyle.Bold);
            lblResultChampion.ForeColor =
                TextColor;
            lblResultChampion.BackColor =
                Color.Transparent;
            lblResultChampion.TextAlign =
                ContentAlignment.MiddleCenter;
            lblResultChampion.AutoEllipsis =
                true;
            resultOverlay.Controls.Add(
                lblResultChampion);

            lblResultSub = new Label();
            lblResultSub.Text =
                "TOURNAMENT WINNER";
            lblResultSub.Font =
                new Font(
                    "Consolas",
                    9F,
                    FontStyle.Bold);
            lblResultSub.ForeColor =
                AccentBright;
            lblResultSub.BackColor =
                Color.Transparent;
            lblResultSub.TextAlign =
                ContentAlignment.MiddleCenter;
            resultOverlay.Controls.Add(
                lblResultSub);

            lblResultLine = new Label();
            lblResultLine.Text =
                "━━━━━━━━━━━━━━━━━━━━━━━━";
            lblResultLine.Font =
                new Font(
                    "Consolas",
                    9F);
            lblResultLine.ForeColor =
                Color.FromArgb(
                    55,
                    75,
                    100);
            lblResultLine.BackColor =
                Color.Transparent;
            lblResultLine.TextAlign =
                ContentAlignment.MiddleCenter;
            resultOverlay.Controls.Add(
                lblResultLine);

            btnViewBracket = new Button();
            btnViewBracket.Text =
                "VIEW BRACKET";
            btnViewBracket.Font =
                new Font(
                    "Segoe UI",
                    8.5F,
                    FontStyle.Bold);
            btnViewBracket.ForeColor =
                Color.White;
            btnViewBracket.BackColor =
                Color.FromArgb(
                    28,
                    82,
                    140);
            btnViewBracket.FlatStyle =
                FlatStyle.Flat;
            btnViewBracket.FlatAppearance.BorderSize =
                0;
            btnViewBracket.Cursor =
                Cursors.Hand;
            btnViewBracket.Click +=
                BtnViewBracket_Click;
            resultOverlay.Controls.Add(
                btnViewBracket);

            btnReturnTournament = new Button();
            btnReturnTournament.Text =
                "RETURN TO TOURNAMENT";
            btnReturnTournament.Font =
                new Font(
                    "Segoe UI",
                    8.5F,
                    FontStyle.Bold);
            btnReturnTournament.ForeColor =
                TextColor;
            btnReturnTournament.BackColor =
                Color.FromArgb(
                    28,
                    34,
                    45);
            btnReturnTournament.FlatStyle =
                FlatStyle.Flat;
            btnReturnTournament.FlatAppearance.BorderColor =
                Color.FromArgb(
                    60,
                    73,
                    94);
            btnReturnTournament.FlatAppearance.BorderSize =
                1;
            btnReturnTournament.Cursor =
                Cursors.Hand;
            btnReturnTournament.Click +=
                BtnReturnTournament_Click;
            resultOverlay.Controls.Add(
                btnReturnTournament);
        }

        private void LayoutResultOverlay()
        {
            if (resultOverlay == null)
                return;

            int width =
                Math.Min(
                    620,
                    ClientSize.Width - 80);

            int height = 390;

            if (ClientSize.Height < 650)
                height = 350;

            int x =
                (ClientSize.Width - width) / 2;

            int y =
                (ClientSize.Height - height) / 2;

            resultOverlay.SetBounds(
                x,
                y,
                width,
                height);

            lblResultEdition.SetBounds(
                25,
                25,
                width - 50,
                22);

            lblResultTitle.SetBounds(
                25,
                58,
                width - 50,
                55);

            lblResultChampion.SetBounds(
                25,
                122,
                width - 50,
                48);

            lblResultSub.SetBounds(
                25,
                174,
                width - 50,
                25);

            lblResultLine.SetBounds(
                25,
                207,
                width - 50,
                22);

            btnViewBracket.SetBounds(
                width / 2 - 190,
                height - 67,
                175,
                38);

            btnReturnTournament.SetBounds(
                width / 2 + 15,
                height - 67,
                175,
                38);
        }

        private void ResultOverlay_Paint(
            object sender,
            PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            int width =
                resultOverlay.ClientSize.Width;

            int height =
                resultOverlay.ClientSize.Height;

            using (Pen border =
                new Pen(
                    Color.FromArgb(
                        80,
                        65,
                        155,
                        255),
                    1))
            {
                g.DrawRectangle(
                    border,
                    0,
                    0,
                    width - 1,
                    height - 1);
            }

            using (SolidBrush glow =
                new SolidBrush(
                    Color.FromArgb(
                        20,
                        245,
                        195,
                        75)))
            {
                g.FillEllipse(
                    glow,
                    width / 2 - 170,
                    -115,
                    340,
                    210);
            }

            using (Pen line =
                new Pen(
                    Color.FromArgb(
                        90,
                        245,
                        195,
                        75),
                    1))
            {
                g.DrawLine(
                    line,
                    40,
                    235,
                    width - 40,
                    235);
            }
        }

        private void UpdateLayout()
        {
            if (ClientSize.Width <= 1 ||
                ClientSize.Height <= 1)
                return;

            int width =
                ClientSize.Width;

            int height =
                ClientSize.Height;

            Label topLine =
                Controls.Find(
                    "lblTopLine",
                    false)
                .FirstOrDefault()
                as Label;

            Label windowClose =
                Controls.Find(
                    "windowClose",
                    false)
                .FirstOrDefault()
                as Label;

            Label windowMax =
                Controls.Find(
                    "windowMax",
                    false)
                .FirstOrDefault()
                as Label;

            Label windowMin =
                Controls.Find(
                    "windowMin",
                    false)
                .FirstOrDefault()
                as Label;

            lblEdition.SetBounds(
                42,
                22,
                250,
                22);

            lblTitle.SetBounds(
                42,
                45,
                Math.Max(
                    500,
                    width - 500),
                48);

            lblTournamentInfo.SetBounds(
                44,
                91,
                500,
                25);

            if (topLine != null)
            {
                topLine.SetBounds(
                    width - 520,
                    23,
                    430,
                    22);
            }

            if (windowMin != null)
            {
                windowMin.SetBounds(
                    width - 135,
                    15,
                    40,
                    30);
            }

            if (windowMax != null)
            {
                windowMax.SetBounds(
                    width - 95,
                    15,
                    40,
                    30);
            }

            if (windowClose != null)
            {
                windowClose.SetBounds(
                    width - 55,
                    15,
                    40,
                    30);
            }

            lblLive.SetBounds(
                width - 235,
                72,
                170,
                28);

            lblStatus.SetBounds(
                width - 330,
                102,
                265,
                22);

            int panelTop = 130;

            int panelBottom =
                height - 92;

            if (panelBottom <
                panelTop + 320)
            {
                panelBottom =
                    panelTop + 320;
            }

            bracketPanel.SetBounds(
                30,
                panelTop,
                Math.Max(
                    700,
                    width - 60),
                panelBottom - panelTop);

            btnBack.SetBounds(
                42,
                height - 61,
                220,
                38);

            lblFooter.SetBounds(
                width / 2 - 250,
                height - 55,
                500,
                25);

            LayoutResultOverlay();

            Invalidate();
        }

        private void BracketForm_Resize(
            object sender,
            EventArgs e)
        {
            UpdateLayout();

            if (tournament != null &&
                resultOverlay != null &&
                !resultOverlay.Visible)
            {
                RefreshBracket();
            }
        }

        private void BracketForm_Paint(
            object sender,
            PaintEventArgs e)
        {
            if (ClientSize.Width <= 1 ||
                ClientSize.Height <= 1)
                return;

            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            using (LinearGradientBrush brush =
                new LinearGradientBrush(
                    ClientRectangle,
                    BackgroundColor,
                    BackgroundSecondary,
                    45F))
            {
                g.FillRectangle(
                    brush,
                    ClientRectangle);
            }

            int width =
                ClientSize.Width;

            int height =
                ClientSize.Height;

            int offset =
                (int)(animationOffset % 42);

            using (Pen gridPen =
                new Pen(
                    Color.FromArgb(
                        10,
                        65,
                        90,
                        120),
                    1))
            {
                for (int x = 0;
                     x < width;
                     x += 42)
                {
                    g.DrawLine(
                        gridPen,
                        x,
                        120 + offset,
                        x,
                        height);
                }

                for (int y = 120;
                     y < height;
                     y += 42)
                {
                    g.DrawLine(
                        gridPen,
                        0,
                        y + offset,
                        width,
                        y + offset);
                }
            }

            using (SolidBrush glow =
                new SolidBrush(
                    Color.FromArgb(
                        16,
                        60,
                        145,
                        255)))
            {
                g.FillEllipse(
                    glow,
                    width / 2 - 360,
                    -210,
                    720,
                    390);
            }

            using (Pen topLine =
                new Pen(
                    Color.FromArgb(
                        80 + pulseValue,
                        AccentColor.R,
                        AccentColor.G,
                        AccentColor.B),
                    1.5F))
            {
                g.DrawLine(
                    topLine,
                    42,
                    119,
                    width - 42,
                    119);
            }

            using (Pen bottomLine =
                new Pen(
                    Color.FromArgb(
                        55,
                        65,
                        155,
                        255),
                    1))
            {
                g.DrawLine(
                    bottomLine,
                    42,
                    height - 75,
                    width - 42,
                    height - 75);
            }
        }

        private void BracketPanel_Paint(
            object sender,
            PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            using (Pen border =
                new Pen(
                    BorderColor,
                    1))
            {
                g.DrawRectangle(
                    border,
                    0,
                    0,
                    Math.Max(
                        0,
                        bracketPanel.ClientSize.Width - 1),
                    Math.Max(
                        0,
                        bracketPanel.ClientSize.Height - 1));
            }

            using (Pen grid =
                new Pen(
                    Color.FromArgb(
                        9,
                        70,
                        90,
                        120),
                    1))
            {
                for (int x = 0;
                     x < bracketPanel.ClientSize.Width;
                     x += 48)
                {
                    g.DrawLine(
                        grid,
                        x,
                        0,
                        x,
                        bracketPanel.ClientSize.Height);
                }

                for (int y = 0;
                     y < bracketPanel.ClientSize.Height;
                     y += 48)
                {
                    g.DrawLine(
                        grid,
                        0,
                        y,
                        bracketPanel.ClientSize.Width,
                        y);
                }
            }

            DrawBracketConnections(g);
        }

        private void DrawBracketConnections(
            Graphics g)
        {
            if (cardCenters.Count == 0 ||
                tournament == null)
                return;

            int rounds =
                GetNumberOfRounds(
                    tournament.Players.Count);

            for (int round = 1;
                 round < rounds;
                 round++)
            {
                List<TournamentMatch> currentMatches =
                    tournament.GetRoundMatches(
                        round);

                List<TournamentMatch> nextMatches =
                    tournament.GetRoundMatches(
                        round + 1);

                for (int i = 0;
                     i < currentMatches.Count;
                     i++)
                {
                    int nextIndex =
                        i / 2;

                    if (nextIndex >=
                        nextMatches.Count)
                        continue;

                    TournamentMatch current =
                        currentMatches[i];

                    TournamentMatch next =
                        nextMatches[nextIndex];

                    if (!cardCenters.ContainsKey(
                            current.MatchNumber) ||
                        !cardCenters.ContainsKey(
                            next.MatchNumber))
                        continue;

                    Point currentCenter =
                        cardCenters[
                            current.MatchNumber];

                    Point nextCenter =
                        cardCenters[
                            next.MatchNumber];

                    int startX =
                        currentCenter.X + 115;

                    int endX =
                        nextCenter.X - 115;

                    if (endX <= startX)
                        continue;

                    int midX =
                        startX +
                        (endX - startX) / 2;

                    using (Pen line =
                        new Pen(
                            Color.FromArgb(
                                75,
                                70,
                                105,
                                145),
                            1.4F))
                    {
                        g.DrawLine(
                            line,
                            startX,
                            currentCenter.Y,
                            midX,
                            currentCenter.Y);

                        g.DrawLine(
                            line,
                            midX,
                            currentCenter.Y,
                            midX,
                            nextCenter.Y);

                        g.DrawLine(
                            line,
                            midX,
                            nextCenter.Y,
                            endX,
                            nextCenter.Y);
                    }
                }
            }
        }

        private void AnimationTimer_Tick(
            object sender,
            EventArgs e)
        {
            animationOffset +=
                0.28F;

            if (animationOffset >= 42)
                animationOffset = 0;

            if (pulseIncreasing)
            {
                pulseValue++;

                if (pulseValue >= 35)
                    pulseIncreasing = false;
            }
            else
            {
                pulseValue--;

                if (pulseValue <= 0)
                    pulseIncreasing = true;
            }

            lblLive.ForeColor =
                Color.FromArgb(
                    165 + pulseValue,
                    215,
                    135);

            if (!resultOverlay.Visible)
            {
                bracketPanel.Invalidate();

                Invalidate(
                    new Rectangle(
                        0,
                        0,
                        ClientSize.Width,
                        Math.Min(
                            125,
                            ClientSize.Height)));
            }
        }

        private void BuildBracket()
        {
            if (tournament == null ||
                tournament.Players == null ||
                tournament.Players.Count < 2)
                return;

            bracketPanel.Controls.Clear();

            matchButtons.Clear();
            matchCards.Clear();
            cardCenters.Clear();

            tournament.Matches.Clear();

            List<string> players =
                new List<string>(
                    tournament.Players);

            int bracketSize = 1;

            while (bracketSize < players.Count)
                bracketSize *= 2;

            while (players.Count < bracketSize)
                players.Add("BYE");

            int rounds =
                GetNumberOfRounds(
                    tournament.Players.Count);

            CreateBracketMatches(
                players,
                rounds);

            SyncBracketState();

            DrawBracket(rounds);

            UpdateStatus();
        }

        private int GetNumberOfRounds(
            int playerCount)
        {
            if (playerCount <= 1)
                return 1;

            int rounds = 0;
            int size = 1;

            while (size < playerCount)
            {
                size *= 2;
                rounds++;
            }

            return rounds;
        }

        private void CreateBracketMatches(
            List<string> players,
            int rounds)
        {
            int matchNumber = 1;

            int firstRoundMatches =
                players.Count / 2;

            for (int i = 0;
                 i < firstRoundMatches;
                 i++)
            {
                tournament.Matches.Add(
                    new TournamentMatch
                    {
                        MatchNumber =
                            matchNumber,
                        RoundNumber =
                            1,
                        Player1 =
                            players[i * 2],
                        Player2 =
                            players[i * 2 + 1],
                        Winner =
                            ""
                    });

                matchNumber++;
            }

            int previousMatchCount =
                firstRoundMatches;

            for (int round = 2;
                 round <= rounds;
                 round++)
            {
                int currentMatchCount =
                    previousMatchCount / 2;

                for (int i = 0;
                     i < currentMatchCount;
                     i++)
                {
                    tournament.Matches.Add(
                        new TournamentMatch
                        {
                            MatchNumber =
                                matchNumber,
                            RoundNumber =
                                round,
                            Player1 =
                                "TBD",
                            Player2 =
                                "TBD",
                            Winner =
                                ""
                        });

                    matchNumber++;
                }

                previousMatchCount =
                    currentMatchCount;
            }
        }

        private void SyncBracketState()
        {
            if (tournament == null)
                return;

            int totalRounds =
                GetNumberOfRounds(
                    tournament.Players.Count);

            for (int round = 1;
                 round <= totalRounds;
                 round++)
            {
                List<TournamentMatch> currentMatches =
                    tournament.GetRoundMatches(
                        round);

                for (int i = 0;
                     i < currentMatches.Count;
                     i++)
                {
                    TournamentMatch match =
                        currentMatches[i];

                    if (match.Player1 == "BYE" &&
                        match.Player2 != "BYE" &&
                        match.Player2 != "TBD" &&
                        !match.IsCompleted)
                    {
                        match.Winner =
                            match.Player2;
                    }

                    if (match.Player2 == "BYE" &&
                        match.Player1 != "BYE" &&
                        match.Player1 != "TBD" &&
                        !match.IsCompleted)
                    {
                        match.Winner =
                            match.Player1;
                    }
                }

                if (round >= totalRounds)
                    continue;

                List<TournamentMatch> nextMatches =
                    tournament.GetRoundMatches(
                        round + 1);

                for (int i = 0;
                     i < nextMatches.Count;
                     i++)
                {
                    TournamentMatch nextMatch =
                        nextMatches[i];

                    int firstIndex =
                        i * 2;

                    TournamentMatch previousOne =
                        firstIndex <
                        currentMatches.Count
                            ? currentMatches[
                                firstIndex]
                            : null;

                    TournamentMatch previousTwo =
                        firstIndex + 1 <
                        currentMatches.Count
                            ? currentMatches[
                                firstIndex + 1]
                            : null;

                    string playerOne =
                        GetAdvanceValue(
                            previousOne);

                    string playerTwo =
                        GetAdvanceValue(
                            previousTwo);

                    nextMatch.Player1 =
                        playerOne;

                    nextMatch.Player2 =
                        playerTwo;

                    if (nextMatch.IsCompleted)
                        continue;

                    if (nextMatch.Player1 == "BYE" &&
                        nextMatch.Player2 != "BYE" &&
                        nextMatch.Player2 != "TBD")
                    {
                        nextMatch.Winner =
                            nextMatch.Player2;
                    }
                    else if (
                        nextMatch.Player2 == "BYE" &&
                        nextMatch.Player1 != "BYE" &&
                        nextMatch.Player1 != "TBD")
                    {
                        nextMatch.Winner =
                            nextMatch.Player1;
                    }
                }
            }
        }

        private string GetAdvanceValue(
            TournamentMatch match)
        {
            if (match == null)
                return "TBD";

            if (!string.IsNullOrWhiteSpace(
                match.Winner))
                return match.Winner;

            if (match.Player1 == "BYE" &&
                match.Player2 == "BYE")
                return "BYE";

            return "TBD";
        }

        private void DrawBracket(
            int rounds)
        {
            bracketPanel.Controls.Clear();

            matchButtons.Clear();
            matchCards.Clear();
            cardCenters.Clear();

            int columnWidth =
                rounds <= 3
                    ? 285
                    : 260;

            int cardWidth =
                225;

            int cardHeight =
                154;

            int leftMargin =
                35;

            int topMargin =
                62;

            int maxBottom = 0;

            for (int round = 1;
                 round <= rounds;
                 round++)
            {
                List<TournamentMatch> matches =
                    tournament.GetRoundMatches(
                        round);

                int x =
                    leftMargin +
                    (round - 1) *
                    columnWidth;

                Label roundLabel =
                    CreateRoundLabel(
                        GetRoundName(
                            round,
                            rounds),
                        x,
                        16,
                        cardWidth);

                bracketPanel.Controls.Add(
                    roundLabel);

                int spacing;

                if (matches.Count <= 1)
                {
                    spacing = 260;
                }
                else
                {
                    int available =
                        Math.Max(
                            560,
                            bracketPanel.ClientSize.Height -
                            125);

                    spacing =
                        Math.Max(
                            178,
                            available /
                            matches.Count);
                }

                for (int i = 0;
                     i < matches.Count;
                     i++)
                {
                    TournamentMatch match =
                        matches[i];

                    int y;

                    if (round == 1)
                    {
                        y =
                            topMargin +
                            i * spacing;
                    }
                    else
                    {
                        List<TournamentMatch> previous =
                            tournament.GetRoundMatches(
                                round - 1);

                        int firstIndex =
                            i * 2;

                        if (firstIndex + 1 <
                                previous.Count &&
                            cardCenters.ContainsKey(
                                previous[firstIndex]
                                    .MatchNumber) &&
                            cardCenters.ContainsKey(
                                previous[firstIndex + 1]
                                    .MatchNumber))
                        {
                            int y1 =
                                cardCenters[
                                    previous[firstIndex]
                                        .MatchNumber].Y;

                            int y2 =
                                cardCenters[
                                    previous[firstIndex + 1]
                                        .MatchNumber].Y;

                            y =
                                (y1 + y2) / 2 -
                                cardHeight / 2;
                        }
                        else
                        {
                            y =
                                topMargin +
                                i * spacing;
                        }
                    }

                    if (y < 50)
                        y = 50;

                    Panel card =
                        CreateMatchCard(
                            match,
                            x,
                            y,
                            cardWidth,
                            cardHeight);

                    bracketPanel.Controls.Add(
                        card);

                    cardCenters[
                        match.MatchNumber] =
                        new Point(
                            x + cardWidth / 2,
                            y + cardHeight / 2);

                    maxBottom =
                        Math.Max(
                            maxBottom,
                            y +
                            cardHeight +
                            45);
                }
            }

            int requiredWidth =
                leftMargin +
                rounds * columnWidth +
                80;

            int requiredHeight =
                Math.Max(
                    maxBottom,
                    bracketPanel.ClientSize.Height);

            bracketPanel.AutoScrollMinSize =
                new Size(
                    requiredWidth,
                    requiredHeight);

            bracketPanel.Invalidate();
        }

        private Label CreateRoundLabel(
            string text,
            int x,
            int y,
            int width)
        {
            Label label =
                new Label();

            label.Text =
                text.ToUpper();

            label.Font =
                new Font(
                    "Consolas",
                    9F,
                    FontStyle.Bold);

            label.ForeColor =
                text == "FINAL"
                    ? GoldColor
                    : AccentBright;

            label.BackColor =
                Color.Transparent;

            label.TextAlign =
                ContentAlignment.MiddleCenter;

            label.SetBounds(
                x,
                y,
                width,
                28);

            return label;
        }

        private string GetRoundName(
            int round,
            int totalRounds)
        {
            if (round == totalRounds)
                return "FINAL";

            if (round ==
                totalRounds - 1)
                return "SEMIFINALS";

            if (round ==
                totalRounds - 2)
                return "QUARTER FINALS";

            int players =
                (int)Math.Pow(
                    2,
                    totalRounds -
                    round +
                    1);

            return "ROUND OF " +
                   players;
        }

        private Panel CreateMatchCard(
            TournamentMatch match,
            int x,
            int y,
            int width,
            int height)
        {
            Panel card =
                new Panel();

            card.SetBounds(
                x,
                y,
                width,
                height);

            bool isFinal =
                match.RoundNumber ==
                GetNumberOfRounds(
                    tournament.Players.Count);

            card.BackColor =
                isFinal
                    ? Color.FromArgb(
                        25,
                        24,
                        31)
                    : CardColor;

            card.BorderStyle =
                BorderStyle.FixedSingle;

            card.Cursor =
                IsMatchReady(match)
                    ? Cursors.Hand
                    : Cursors.Default;

            Label matchLabel =
                new Label();

            matchLabel.Text =
                "MATCH " +
                match.MatchNumber
                    .ToString("00");

            matchLabel.Font =
                new Font(
                    "Consolas",
                    8F,
                    FontStyle.Bold);

            matchLabel.ForeColor =
                Color.FromArgb(
                    105,
                    120,
                    140);

            matchLabel.BackColor =
                Color.Transparent;

            matchLabel.SetBounds(
                12,
                8,
                100,
                20);

            Label statusLabel =
                new Label();

            statusLabel.Text =
                GetMatchStatus(
                    match);

            statusLabel.Font =
                new Font(
                    "Consolas",
                    7F,
                    FontStyle.Bold);

            statusLabel.ForeColor =
                GetStatusColor(
                    match);

            statusLabel.BackColor =
                Color.Transparent;

            statusLabel.TextAlign =
                ContentAlignment.MiddleRight;

            statusLabel.SetBounds(
                105,
                8,
                width - 117,
                20);

            Label playerOne =
                CreatePlayerLabel(
                    GetPlayerDisplay(
                        match.Player1),
                    match,
                    true);

            playerOne.SetBounds(
                14,
                34,
                width - 28,
                25);

            Label versus =
                new Label();

            versus.Text =
                "VS";

            versus.Font =
                new Font(
                    "Consolas",
                    7F,
                    FontStyle.Bold);

            versus.ForeColor =
                Color.FromArgb(
                    85,
                    100,
                    120);

            versus.BackColor =
                Color.Transparent;

            versus.TextAlign =
                ContentAlignment.MiddleCenter;

            versus.SetBounds(
                width / 2 - 20,
                57,
                40,
                18);

            Label playerTwo =
                CreatePlayerLabel(
                    GetPlayerDisplay(
                        match.Player2),
                    match,
                    false);

            playerTwo.SetBounds(
                14,
                76,
                width - 28,
                25);

            Button playButton =
                new Button();

            string buttonText;

            if (match.IsCompleted)
            {
                buttonText =
                    "RESULT RECORDED";
            }
            else if (
                match.Player1 == "BYE" ||
                match.Player2 == "BYE")
            {
                buttonText =
                    "AUTO ADVANCE";
            }
            else if (
                match.Player1 == "TBD" ||
                match.Player2 == "TBD")
            {
                buttonText =
                    "WAITING";
            }
            else
            {
                buttonText =
                    "PLAY MATCH";
            }

            playButton.Text =
                buttonText;

            playButton.Font =
                new Font(
                    "Segoe UI",
                    7.5F,
                    FontStyle.Bold);

            playButton.ForeColor =
                Color.White;

            playButton.BackColor =
                GetButtonColor(
                    match);

            playButton.FlatStyle =
                FlatStyle.Flat;

            playButton.FlatAppearance.BorderSize =
                0;

            playButton.Cursor =
                IsMatchReady(match)
                    ? Cursors.Hand
                    : Cursors.Default;

            playButton.Tag =
                match.MatchNumber;

            playButton.SetBounds(
                12,
                106,
                width - 24,
                25);

            playButton.MouseEnter +=
                MatchButton_MouseEnter;

            playButton.MouseLeave +=
                MatchButton_MouseLeave;

            playButton.MouseDown +=
                MatchButton_MouseDown;

            playButton.MouseUp +=
                MatchButton_MouseUp;

            playButton.Click +=
                PlayButton_Click;

            Panel resultBar =
                new Panel();

            resultBar.SetBounds(
                12,
                134,
                width - 24,
                17);

            resultBar.BackColor =
                GetResultBarColor(
                    match);

            resultBar.BorderStyle =
                BorderStyle.None;

            Label resultLabel =
                new Label();

            resultLabel.Text =
                GetResultText(
                    match);

            resultLabel.Font =
                new Font(
                    "Consolas",
                    6.5F,
                    FontStyle.Bold);

            resultLabel.ForeColor =
                Color.White;

            resultLabel.BackColor =
                Color.Transparent;

            resultLabel.TextAlign =
                ContentAlignment.MiddleCenter;

            resultLabel.SetBounds(
                3,
                0,
                resultBar.Width - 6,
                resultBar.Height);

            resultBar.Controls.Add(
                resultLabel);

            card.Controls.Add(
                matchLabel);

            card.Controls.Add(
                statusLabel);

            card.Controls.Add(
                playerOne);

            card.Controls.Add(
                versus);

            card.Controls.Add(
                playerTwo);

            card.Controls.Add(
                playButton);

            card.Controls.Add(
                resultBar);

            card.MouseEnter +=
                Card_MouseEnter;

            card.MouseLeave +=
                Card_MouseLeave;

            matchButtons[
                match.MatchNumber] =
                playButton;

            matchCards[
                match.MatchNumber] =
                card;

            return card;
        }

        private Label CreatePlayerLabel(
            string text,
            TournamentMatch match,
            bool first)
        {
            Label label =
                new Label();

            label.Text =
                text;

            label.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            bool winner =
                match.IsCompleted &&
                string.Equals(
                    match.Winner,
                    text,
                    StringComparison.OrdinalIgnoreCase);

            label.ForeColor =
                winner
                    ? SuccessColor
                    : text == "TBD"
                        ? Color.FromArgb(
                            95,
                            108,
                            128)
                        : text == "BYE"
                            ? Color.FromArgb(
                                130,
                                140,
                                155)
                            : TextColor;

            label.BackColor =
                Color.Transparent;

            label.TextAlign =
                first
                    ? ContentAlignment.MiddleLeft
                    : ContentAlignment.MiddleRight;

            label.AutoEllipsis =
                true;

            return label;
        }

        private string GetPlayerDisplay(
            string player)
        {
            if (string.IsNullOrWhiteSpace(
                player))
                return "TBD";

            return player.ToUpper();
        }

        private string GetMatchStatus(
            TournamentMatch match)
        {
            if (match.IsCompleted)
                return "COMPLETED";

            if (match.Player1 == "BYE" ||
                match.Player2 == "BYE")
                return "AUTO ADVANCE";

            if (match.Player1 == "TBD" ||
                match.Player2 == "TBD")
                return "LOCKED";

            return "READY";
        }

        private Color GetStatusColor(
            TournamentMatch match)
        {
            if (match.IsCompleted)
                return SuccessColor;

            if (match.Player1 == "BYE" ||
                match.Player2 == "BYE")
                return GoldColor;

            if (match.Player1 == "TBD" ||
                match.Player2 == "TBD")
                return MutedColor;

            return AccentBright;
        }

        private string GetResultText(
            TournamentMatch match)
        {
            if (!match.IsCompleted)
                return "NO RESULT";

            if (match.RoundNumber ==
                GetNumberOfRounds(
                    tournament.Players.Count))
            {
                return "CHAMPION  //  " +
                       match.Winner.ToUpper();
            }

            return "WINNER  //  " +
                   match.Winner.ToUpper();
        }

        private Color GetResultBarColor(
            TournamentMatch match)
        {
            if (!match.IsCompleted)
            {
                return Color.FromArgb(
                    24,
                    31,
                    43);
            }

            if (match.RoundNumber ==
                GetNumberOfRounds(
                    tournament.Players.Count))
            {
                return Color.FromArgb(
                    125,
                    92,
                    28);
            }

            return Color.FromArgb(
                25,
                105,
                72);
        }

        private Color GetButtonColor(
            TournamentMatch match)
        {
            if (match.IsCompleted)
            {
                if (match.RoundNumber ==
                    GetNumberOfRounds(
                        tournament.Players.Count))
                {
                    return Color.FromArgb(
                        105,
                        78,
                        25);
                }

                return Color.FromArgb(
                    25,
                    88,
                    63);
            }

            if (match.Player1 == "BYE" ||
                match.Player2 == "BYE")
            {
                return Color.FromArgb(
                    90,
                    70,
                    28);
            }

            if (match.Player1 == "TBD" ||
                match.Player2 == "TBD")
            {
                return Color.FromArgb(
                    35,
                    42,
                    55);
            }

            return Color.FromArgb(
                28,
                82,
                140);
        }

        private bool IsMatchReady(
            TournamentMatch match)
        {
            return match != null &&
                   !match.IsCompleted &&
                   match.Player1 != "TBD" &&
                   match.Player2 != "TBD";
        }

        private void Card_MouseEnter(
            object sender,
            EventArgs e)
        {
            Panel card =
                sender as Panel;

            if (card == null)
                return;

            int matchNumber =
                matchCards
                    .FirstOrDefault(
                        x => x.Value == card)
                    .Key;

            if (matchNumber <= 0)
                return;

            TournamentMatch match =
                tournament.Matches
                    .FirstOrDefault(
                        x =>
                            x.MatchNumber ==
                            matchNumber);

            if (match == null)
                return;

            if (IsMatchReady(match))
            {
                card.BackColor =
                    CardHoverColor;
            }
        }

        private void Card_MouseLeave(
            object sender,
            EventArgs e)
        {
            Panel card =
                sender as Panel;

            if (card == null)
                return;

            int matchNumber =
                matchCards
                    .FirstOrDefault(
                        x => x.Value == card)
                    .Key;

            if (matchNumber <= 0)
                return;

            TournamentMatch match =
                tournament.Matches
                    .FirstOrDefault(
                        x =>
                            x.MatchNumber ==
                            matchNumber);

            if (match == null)
                return;

            bool isFinal =
                match.RoundNumber ==
                GetNumberOfRounds(
                    tournament.Players.Count);

            card.BackColor =
                isFinal
                    ? Color.FromArgb(
                        25,
                        24,
                        31)
                    : CardColor;
        }

        private void MatchButton_MouseEnter(
            object sender,
            EventArgs e)
        {
            Button button =
                sender as Button;

            if (button == null)
                return;

            int matchNumber =
                Convert.ToInt32(
                    button.Tag);

            TournamentMatch match =
                tournament.Matches
                    .FirstOrDefault(
                        x =>
                            x.MatchNumber ==
                            matchNumber);

            if (match == null)
                return;

            if (match.IsCompleted)
            {
                button.BackColor =
                    match.RoundNumber ==
                    GetNumberOfRounds(
                        tournament.Players.Count)
                        ? Color.FromArgb(
                            150,
                            110,
                            35)
                        : Color.FromArgb(
                            31,
                            125,
                            82);
            }
            else if (IsMatchReady(match))
            {
                button.BackColor =
                    Color.FromArgb(
                        45,
                        115,
                        190);
            }
        }

        private void MatchButton_MouseLeave(
            object sender,
            EventArgs e)
        {
            Button button =
                sender as Button;

            if (button == null)
                return;

            int matchNumber =
                Convert.ToInt32(
                    button.Tag);

            TournamentMatch match =
                tournament.Matches
                    .FirstOrDefault(
                        x =>
                            x.MatchNumber ==
                            matchNumber);

            if (match == null)
                return;

            button.BackColor =
                GetButtonColor(
                    match);
        }

        private void MatchButton_MouseDown(
            object sender,
            MouseEventArgs e)
        {
            Button button =
                sender as Button;

            if (button == null)
                return;

            int matchNumber =
                Convert.ToInt32(
                    button.Tag);

            TournamentMatch match =
                tournament.Matches
                    .FirstOrDefault(
                        x =>
                            x.MatchNumber ==
                            matchNumber);

            if (IsMatchReady(match))
            {
                button.BackColor =
                    Color.FromArgb(
                        20,
                        65,
                        115);
            }
        }

        private void MatchButton_MouseUp(
            object sender,
            MouseEventArgs e)
        {
            MatchButton_MouseEnter(
                sender,
                EventArgs.Empty);
        }

        private void PlayButton_Click(
            object sender,
            EventArgs e)
        {
            Button button =
                sender as Button;

            if (button == null)
                return;

            int matchNumber =
                Convert.ToInt32(
                    button.Tag);

            TournamentMatch match =
                tournament.Matches
                    .FirstOrDefault(
                        x =>
                            x.MatchNumber ==
                            matchNumber);

            if (match == null ||
                match.IsCompleted)
                return;

            if (match.Player1 == "BYE")
            {
                CompleteTournamentMatch(
                    match,
                    match.Player2);

                return;
            }

            if (match.Player2 == "BYE")
            {
                CompleteTournamentMatch(
                    match,
                    match.Player1);

                return;
            }

            if (!IsMatchReady(match))
                return;

            Hide();

            GameForm gameForm =
                new GameForm(
                    match.Player1,
                    match.Player2,
                    winner =>
                    {
                        CompleteTournamentMatch(
                            match,
                            winner);
                    },
                    () =>
                    {
                        Show();
                        BringToFront();
                    },
                    () =>
                    {
                        Show();
                        BringToFront();
                    });

            gameForm.Show();
        }

        private void CompleteTournamentMatch(
            TournamentMatch match,
            string winner)
        {
            if (match == null ||
                string.IsNullOrWhiteSpace(
                    winner))
                return;

            match.Winner =
                winner.Trim();

            SyncBracketState();

            int totalRounds =
                GetNumberOfRounds(
                    tournament.Players.Count);

            if (match.RoundNumber ==
                totalRounds)
            {
                tournament.SetChampion(
                    match.Winner);

                FileManager.SaveTournamentResult(
                    tournament.TournamentName,
                    match.Winner);

                RefreshBracket();

                Show();

                BringToFront();

                ShowChampionOverlay(
                    match.Winner);

                return;
            }

            RefreshBracket();

            Show();

            BringToFront();
        }

        private void ShowChampionOverlay(
            string winner)
        {
            if (string.IsNullOrWhiteSpace(
                winner))
                return;

            lblResultChampion.Text =
                winner.ToUpper();

            lblResultTitle.Text =
                "CHAMPION";

            lblResultSub.Text =
                "TOURNAMENT WINNER  //  FINAL MATCH";

            resultOverlay.Visible =
                true;

            resultOverlay.BringToFront();

            animationTimer.Stop();

            LayoutResultOverlay();

            Invalidate();
        }

        private void BtnViewBracket_Click(
            object sender,
            EventArgs e)
        {
            resultOverlay.Visible =
                false;

            animationTimer.Start();

            RefreshBracket();

            bracketPanel.Invalidate();
            Invalidate();
        }

        private void BtnReturnTournament_Click(
            object sender,
            EventArgs e)
        {
            Form existingForm =
                Application.OpenForms
                    .Cast<Form>()
                    .FirstOrDefault(
                        f =>
                            f is TournamentForm);

            if (existingForm != null)
            {
                existingForm.Show();
                existingForm.BringToFront();
            }
            else
            {
                TournamentForm tournamentForm =
                    new TournamentForm();

                tournamentForm.Show();
            }

            Close();
        }

        private void RefreshBracket()
        {
            if (tournament == null ||
                tournament.Players == null ||
                tournament.Players.Count < 2)
                return;

            SyncBracketState();

            int rounds =
                GetNumberOfRounds(
                    tournament.Players.Count);

            DrawBracket(rounds);

            UpdateStatus();

            bracketPanel.Invalidate();
            Invalidate();
        }

        private void UpdateStatus()
        {
            if (tournament == null)
                return;

            int totalRounds =
                GetNumberOfRounds(
                    tournament.Players.Count);

            TournamentMatch finalMatch =
                tournament.GetRoundMatches(
                    totalRounds)
                .FirstOrDefault();

            if (finalMatch != null &&
                finalMatch.IsCompleted)
            {
                lblStatus.Text =
                    "CHAMPIONSHIP COMPLETE";

                lblStatus.ForeColor =
                    GoldColor;

                lblLive.Text =
                    "●  CHAMPION CROWNED";

                lblLive.ForeColor =
                    GoldColor;

                return;
            }

            int completed =
                tournament.Matches
                    .Count(
                        x => x.IsCompleted);

            int total =
                tournament.Matches.Count;

            lblStatus.Text =
                "MATCH PROGRESS  //  " +
                completed +
                " / " +
                total;

            lblStatus.ForeColor =
                MutedColor;

            lblLive.Text =
                "●  LIVE BRACKET";

            lblLive.ForeColor =
                SuccessColor;
        }

        private void BtnBack_MouseEnter(
            object sender,
            EventArgs e)
        {
            btnBack.BackColor =
                Color.FromArgb(
                    38,
                    49,
                    66);
        }

        private void BtnBack_MouseLeave(
            object sender,
            EventArgs e)
        {
            btnBack.BackColor =
                Color.FromArgb(
                    25,
                    32,
                    44);
        }

        private void BtnBack_MouseDown(
            object sender,
            MouseEventArgs e)
        {
            btnBack.BackColor =
                Color.FromArgb(
                    18,
                    24,
                    34);
        }

        private void BtnBack_MouseUp(
            object sender,
            MouseEventArgs e)
        {
            btnBack.BackColor =
                Color.FromArgb(
                    38,
                    49,
                    66);
        }

        private void BtnBack_Click(
            object sender,
            EventArgs e)
        {
            Form existingForm =
                Application.OpenForms
                    .Cast<Form>()
                    .FirstOrDefault(
                        f =>
                            f is TournamentForm);

            if (existingForm != null)
            {
                existingForm.Show();
                existingForm.BringToFront();
            }
            else
            {
                TournamentForm tournamentForm =
                    new TournamentForm();

                tournamentForm.Show();
            }

            Close();
        }

        protected override void OnFormClosed(
            FormClosedEventArgs e)
        {
            if (animationTimer != null)
            {
                animationTimer.Stop();
                animationTimer.Dispose();
            }

            base.OnFormClosed(e);
        }

        private void BracketForm_Load(
            object sender,
            EventArgs e)
        {
        }
    }

    public static class ControlExtensions
    {
        public static void DoubleBuffered(
            this Control control,
            bool enable)
        {
            typeof(Control)
                .GetProperty(
                    "DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(
                    control,
                    enable,
                    null);
        }
    }
}