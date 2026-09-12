using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace AdvancedTicTacToeWinForms
{
    public partial class PlayerSetupForm : Form
    {
        private readonly Color backgroundColor = Color.FromArgb(6, 9, 15);
        private readonly Color panelColor = Color.FromArgb(12, 17, 25);
        private readonly Color inputColor = Color.FromArgb(15, 21, 31);
        private readonly Color inputFocusColor = Color.FromArgb(21, 31, 44);

        private readonly Color accentColor = Color.FromArgb(55, 165, 255);
        private readonly Color accentBright = Color.FromArgb(105, 205, 255);
        private readonly Color redColor = Color.FromArgb(220, 72, 82);
        private readonly Color redBright = Color.FromArgb(255, 105, 112);

        private readonly Color textColor = Color.FromArgb(240, 244, 252);
        private readonly Color mutedColor = Color.FromArgb(105, 120, 140);

        private readonly HashSet<Button> hoveredButtons =
            new HashSet<Button>();

        private readonly Dictionary<Button, float> buttonOffsets =
            new Dictionary<Button, float>();

        private readonly Dictionary<Button, int> baseButtonX =
            new Dictionary<Button, int>();

        private Timer animationTimer;
        private float animationPhase;

        private PointF[] particles;
        private float[] particleSpeeds;

        private Label lblEdition;
        private Label lblHeroSub;
        private Label lblMatchType;
        private Label lblMatchSub;
        private Label lblStatus;
        private Label lblVersion;
        private Label lblFooter;

        private Panel setupPanel;
        private Panel accentLine;

        private Label lblX;
        private Label lblO;

        public PlayerSetupForm()
        {
            InitializeComponent();

            DoubleBuffered = true;

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);

            ApplyGamingTheme();
            CreateInterfaceLabels();
            CreateSetupPanel();
            SetupControls();
            CreateParticles();
            UpdateLayout();
            StartAnimation();

            Resize += PlayerSetupForm_Resize;
            FormClosed += PlayerSetupForm_FormClosed;
        }

        private void ApplyGamingTheme()
        {
            Text = "Player Setup";
            BackColor = backgroundColor;

            ClientSize = new Size(1200, 760);
            MinimumSize = new Size(1000, 650);

            StartPosition = FormStartPosition.CenterScreen;

            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;

            Paint += PlayerSetupForm_Paint;

            lblTitle.Text = "PLAYER\r\nSETUP";
            lblTitle.Font = new Font(
                "Segoe UI",
                29F,
                FontStyle.Bold);

            lblTitle.ForeColor = textColor;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.AutoSize = false;
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            lblTitle.Visible = true;
        }

        private void CreateInterfaceLabels()
        {
            lblEdition = CreateLabel(
                "TACTICAL EDITION  /  02",
                new Font(
                    "Consolas",
                    8.5F,
                    FontStyle.Bold),
                Color.FromArgb(78, 160, 220));

            lblHeroSub = CreateLabel(
                "CONFIGURE YOUR HEAD-TO-HEAD MATCH",
                new Font(
                    "Segoe UI",
                    9.5F,
                    FontStyle.Bold),
                accentBright);

            lblMatchType = CreateLabel(
                "PLAYER VS PLAYER",
                new Font(
                    "Segoe UI",
                    17F,
                    FontStyle.Bold),
                textColor);

            lblMatchSub = CreateLabel(
                "LOCAL HEAD-TO-HEAD  //  TWO PLAYERS",
                new Font(
                    "Consolas",
                    8.5F,
                    FontStyle.Bold),
                mutedColor);

            lblStatus = CreateLabel(
                "SYSTEM READY",
                new Font(
                    "Consolas",
                    8.5F,
                    FontStyle.Bold),
                Color.FromArgb(70, 210, 125));

            lblVersion = CreateLabel(
                "BUILD 1.0  //  PVP  //  C#  //  WINFORMS",
                new Font(
                    "Consolas",
                    8F,
                    FontStyle.Regular),
                Color.FromArgb(70, 85, 105));

            lblFooter = CreateLabel(
                "Developed by Syed Hussain Abbas",
                new Font(
                    "Segoe UI",
                    8.5F,
                    FontStyle.Regular),
                Color.FromArgb(105, 120, 142));

            Controls.Add(lblEdition);
            Controls.Add(lblHeroSub);
            Controls.Add(lblMatchType);
            Controls.Add(lblMatchSub);
            Controls.Add(lblStatus);
            Controls.Add(lblVersion);
            Controls.Add(lblFooter);
        }

        private Label CreateLabel(
            string text,
            Font font,
            Color color)
        {
            Label label = new Label();

            label.Text = text;
            label.Font = font;
            label.ForeColor = color;
            label.BackColor = Color.Transparent;
            label.AutoSize = false;
            label.TextAlign = ContentAlignment.MiddleLeft;

            return label;
        }

        private void CreateSetupPanel()
        {
            setupPanel = new Panel();

            setupPanel.BackColor = panelColor;
            setupPanel.BorderStyle = BorderStyle.None;

            setupPanel.Paint += SetupPanel_Paint;

            Controls.Add(setupPanel);

            setupPanel.Controls.Add(lblPlayer1);
            setupPanel.Controls.Add(txtPlayer1);
            setupPanel.Controls.Add(lblPlayer2);
            setupPanel.Controls.Add(txtPlayer2);
            setupPanel.Controls.Add(btnStart);
            setupPanel.Controls.Add(btnBack);

            accentLine = new Panel();

            accentLine.BackColor = accentColor;
            accentLine.Size = new Size(120, 2);

            setupPanel.Controls.Add(accentLine);

            lblX = new Label();

            lblX.Text = "X";
            lblX.Font = new Font(
                "Segoe UI",
                24F,
                FontStyle.Bold);

            lblX.ForeColor = accentBright;
            lblX.BackColor = Color.Transparent;
            lblX.Size = new Size(50, 50);
            lblX.TextAlign = ContentAlignment.MiddleCenter;

            setupPanel.Controls.Add(lblX);

            lblO = new Label();

            lblO.Text = "O";
            lblO.Font = new Font(
                "Segoe UI",
                24F,
                FontStyle.Bold);

            lblO.ForeColor = redBright;
            lblO.BackColor = Color.Transparent;
            lblO.Size = new Size(50, 50);
            lblO.TextAlign = ContentAlignment.MiddleCenter;

            setupPanel.Controls.Add(lblO);
        }

        private void SetupControls()
        {
            lblPlayer1.Text = "PLAYER 01";
            lblPlayer1.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);

            lblPlayer1.ForeColor = accentBright;
            lblPlayer1.BackColor = Color.Transparent;
            lblPlayer1.AutoSize = false;
            lblPlayer1.Size = new Size(390, 24);
            lblPlayer1.TextAlign = ContentAlignment.MiddleLeft;

            txtPlayer1.Font = new Font(
                "Segoe UI",
                11F);

            txtPlayer1.ForeColor = textColor;
            txtPlayer1.BackColor = inputColor;
            txtPlayer1.BorderStyle = BorderStyle.FixedSingle;
            txtPlayer1.Size = new Size(390, 38);
            txtPlayer1.Cursor = Cursors.IBeam;

            lblPlayer2.Text = "PLAYER 02";
            lblPlayer2.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);

            lblPlayer2.ForeColor = redBright;
            lblPlayer2.BackColor = Color.Transparent;
            lblPlayer2.AutoSize = false;
            lblPlayer2.Size = new Size(390, 24);
            lblPlayer2.TextAlign = ContentAlignment.MiddleLeft;

            txtPlayer2.Font = new Font(
                "Segoe UI",
                11F);

            txtPlayer2.ForeColor = textColor;
            txtPlayer2.BackColor = inputColor;
            txtPlayer2.BorderStyle = BorderStyle.FixedSingle;
            txtPlayer2.Size = new Size(390, 38);
            txtPlayer2.Cursor = Cursors.IBeam;

            ConfigurePrimaryButton(
                btnStart,
                "START MATCH");

            ConfigureSecondaryButton(
                btnBack,
                "BACK");

            txtPlayer1.Enter += TextBox_Enter;
            txtPlayer1.Leave += TextBox_Leave;

            txtPlayer2.Enter += TextBox_Enter;
            txtPlayer2.Leave += TextBox_Leave;

            txtPlayer1.KeyDown += PlayerTextBox_KeyDown;
            txtPlayer2.KeyDown += PlayerTextBox_KeyDown;
        }

        private void ConfigurePrimaryButton(
            Button button,
            string title)
        {
            button.Text = "";
            button.ForeColor = Color.Transparent;
            button.BackColor = Color.Transparent;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.UseVisualStyleBackColor = false;
            button.Cursor = Cursors.Hand;
            button.TabStop = false;
            button.Tag = title;

            buttonOffsets[button] = 0;

            button.Paint += PrimaryButton_Paint;
            button.MouseEnter += GamingButton_MouseEnter;
            button.MouseLeave += GamingButton_MouseLeave;
            button.MouseDown += GamingButton_MouseDown;
            button.MouseUp += GamingButton_MouseUp;
        }

        private void ConfigureSecondaryButton(
            Button button,
            string title)
        {
            button.Text = "";
            button.ForeColor = Color.Transparent;
            button.BackColor = Color.Transparent;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.UseVisualStyleBackColor = false;
            button.Cursor = Cursors.Hand;
            button.TabStop = false;
            button.Tag = title;

            buttonOffsets[button] = 0;

            button.Paint += SecondaryButton_Paint;
            button.MouseEnter += GamingButton_MouseEnter;
            button.MouseLeave += GamingButton_MouseLeave;
            button.MouseDown += GamingButton_MouseDown;
            button.MouseUp += GamingButton_MouseUp;
        }

        private void UpdateLayout()
        {
            int width = ClientSize.Width;
            int height = ClientSize.Height;

            int leftWidth =
                (int)(width * 0.48F);

            int rightX =
                leftWidth + 35;

            int rightWidth =
                Math.Max(
                    430,
                    width - rightX - 55);

            int heroWidth =
                Math.Max(
                    330,
                    leftWidth - 95);

            lblEdition.Location =
                new Point(
                    65,
                    48);

            lblEdition.Size =
                new Size(
                    heroWidth,
                    24);

            lblTitle.Location =
                new Point(
                    65,
                    112);

            lblTitle.Size =
                new Size(
                    heroWidth,
                    92);

            lblHeroSub.Location =
                new Point(
                    68,
                    218);

            lblHeroSub.Size =
                new Size(
                    heroWidth,
                    25);

            lblMatchType.Location =
                new Point(
                    rightX,
                    80);

            lblMatchType.Size =
                new Size(
                    rightWidth,
                    35);

            lblMatchSub.Location =
                new Point(
                    rightX + 2,
                    116);

            lblMatchSub.Size =
                new Size(
                    rightWidth,
                    22);

            int panelWidth =
                Math.Min(
                    540,
                    rightWidth);

            int panelHeight = 420;

            setupPanel.Size =
                new Size(
                    panelWidth,
                    panelHeight);

            setupPanel.Location =
                new Point(
                    rightX,
                    160);

            int contentWidth =
                panelWidth - 155;

            int contentX = 125;

            lblPlayer1.Location =
                new Point(
                    contentX,
                    85);

            lblPlayer1.Size =
                new Size(
                    contentWidth,
                    24);

            txtPlayer1.Location =
                new Point(
                    contentX,
                    114);

            txtPlayer1.Size =
                new Size(
                    contentWidth,
                    38);

            lblX.Location =
                new Point(
                    55,
                    109);

            lblPlayer2.Location =
                new Point(
                    contentX,
                    205);

            lblPlayer2.Size =
                new Size(
                    contentWidth,
                    24);

            txtPlayer2.Location =
                new Point(
                    contentX,
                    234);

            txtPlayer2.Size =
                new Size(
                    contentWidth,
                    38);

            lblO.Location =
                new Point(
                    55,
                    229);

            accentLine.Location =
                new Point(
                    contentX,
                    54);

            int buttonWidth =
                Math.Min(
                    185,
                    (contentWidth - 12) / 2);

            btnStart.Size =
                new Size(
                    buttonWidth,
                    52);

            btnBack.Size =
                new Size(
                    buttonWidth,
                    52);

            btnStart.Location =
                new Point(
                    contentX,
                    330);

            btnBack.Location =
                new Point(
                    contentX +
                    buttonWidth +
                    12,
                    330);

            SetButtonRegion(
                btnStart,
                11);

            SetButtonRegion(
                btnBack,
                11);

            baseButtonX[btnStart] =
                btnStart.Left;

            baseButtonX[btnBack] =
                btnBack.Left;

            buttonOffsets[btnStart] = 0;
            buttonOffsets[btnBack] = 0;

            lblStatus.Location =
                new Point(
                    65,
                    height - 72);

            lblStatus.Size =
                new Size(
                    220,
                    25);

            lblVersion.Location =
                new Point(
                    width - 360,
                    height - 72);

            lblVersion.Size =
                new Size(
                    295,
                    25);

            lblVersion.TextAlign =
                ContentAlignment.MiddleRight;

            lblFooter.Location =
                new Point(
                    65,
                    height - 42);

            lblFooter.Size =
                new Size(
                    320,
                    20);

            Invalidate();
            setupPanel.Invalidate();
        }

        private void CreateParticles()
        {
            Random random = new Random();

            particles = new PointF[42];
            particleSpeeds = new float[42];

            for (int i = 0;
                 i < particles.Length;
                 i++)
            {
                particles[i] =
                    new PointF(
                        random.Next(
                            0,
                            Math.Max(
                                1,
                                ClientSize.Width)),
                        random.Next(
                            0,
                            Math.Max(
                                1,
                                ClientSize.Height)));

                particleSpeeds[i] =
                    0.10F +
                    (float)random.NextDouble() *
                    0.25F;
            }
        }

        private void StartAnimation()
        {
            animationTimer = new Timer();

            animationTimer.Interval = 45;

            animationTimer.Tick +=
                AnimationTimer_Tick;

            animationTimer.Start();
        }

        private void AnimationTimer_Tick(
            object sender,
            EventArgs e)
        {
            animationPhase += 0.012F;

            if (animationPhase >
                Math.PI * 2)
            {
                animationPhase = 0;
            }

            for (int i = 0;
                 i < particles.Length;
                 i++)
            {
                particles[i] =
                    new PointF(
                        particles[i].X,
                        particles[i].Y +
                        particleSpeeds[i]);

                if (particles[i].Y >
                    ClientSize.Height)
                {
                    particles[i] =
                        new PointF(
                            particles[i].X,
                            -4);
                }
            }

            Button[] buttons =
            {
                btnStart,
                btnBack
            };

            foreach (Button button in buttons)
            {
                if (!buttonOffsets.ContainsKey(button))
                {
                    buttonOffsets[button] = 0;
                }

                float target =
                    hoveredButtons.Contains(button)
                        ? 5F
                        : 0F;

                float current =
                    buttonOffsets[button];

                current +=
                    (target - current) *
                    0.14F;

                buttonOffsets[button] =
                    current;

                if (baseButtonX.ContainsKey(button))
                {
                    button.Left =
                        baseButtonX[button] +
                        (int)current;
                }

                button.Invalidate();
            }

            Invalidate();
        }

        private void PlayerSetupForm_Paint(
            object sender,
            PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            g.TextRenderingHint =
                TextRenderingHint.ClearTypeGridFit;

            using (LinearGradientBrush background =
                new LinearGradientBrush(
                    ClientRectangle,
                    Color.FromArgb(5, 8, 13),
                    Color.FromArgb(10, 16, 24),
                    35F))
            {
                g.FillRectangle(
                    background,
                    ClientRectangle);
            }

            DrawTacticalGrid(g);
            DrawParticles(g);
            DrawHeroMark(g);
            DrawDecorativeLines(g);
            DrawTopBar(g);
            DrawBottomBar(g);
            DrawSystemIndicators(g);
        }

        private void DrawTacticalGrid(
            Graphics g)
        {
            using (Pen gridPen =
                new Pen(
                    Color.FromArgb(
                        11,
                        21,
                        31),
                    1F))
            {
                for (int x = 0;
                     x < ClientSize.Width;
                     x += 45)
                {
                    g.DrawLine(
                        gridPen,
                        x,
                        0,
                        x,
                        ClientSize.Height);
                }

                for (int y = 0;
                     y < ClientSize.Height;
                     y += 45)
                {
                    g.DrawLine(
                        gridPen,
                        0,
                        y,
                        ClientSize.Width,
                        y);
                }
            }

            using (Pen diagonalPen =
                new Pen(
                    Color.FromArgb(
                        14,
                        27,
                        39),
                    1F))
            {
                for (int i =
                         -ClientSize.Height;
                     i < ClientSize.Width;
                     i += 100)
                {
                    g.DrawLine(
                        diagonalPen,
                        i,
                        ClientSize.Height,
                        i + ClientSize.Height,
                        0);
                }
            }
        }

        private void DrawParticles(
            Graphics g)
        {
            float pulse =
                0.5F +
                0.5F *
                (float)Math.Sin(
                    animationPhase * 2);

            int alpha =
                25 +
                (int)(pulse * 18);

            using (SolidBrush brush =
                new SolidBrush(
                    Color.FromArgb(
                        alpha,
                        75,
                        160,
                        220)))
            {
                foreach (PointF particle
                         in particles)
                {
                    g.FillEllipse(
                        brush,
                        particle.X,
                        particle.Y,
                        2,
                        2);
                }
            }
        }

        private void DrawHeroMark(
            Graphics g)
        {
            int leftWidth =
                (int)(ClientSize.Width * 0.48F);

            int heroLeft = 68;

            int heroWidth =
                Math.Max(
                    330,
                    leftWidth - 95);

            int centerX =
                heroLeft +
                heroWidth / 2;

            int centerY =
                ClientSize.Height / 2 +
                35;

            int cell = 42;
            int gridSize = cell * 3;

            using (Pen glowPen =
                new Pen(
                    Color.FromArgb(
                        18,
                        55,
                        165,
                        255),
                    6F))
            {
                g.DrawRectangle(
                    glowPen,
                    centerX -
                    gridSize / 2 -
                    10,
                    centerY -
                    gridSize / 2 -
                    10,
                    gridSize + 20,
                    gridSize + 20);
            }

            using (Pen gridPen =
                new Pen(
                    Color.FromArgb(
                        45,
                        83,
                        112),
                    2F))
            {
                for (int i = 1;
                     i < 3;
                     i++)
                {
                    int x =
                        centerX -
                        gridSize / 2 +
                        i * cell;

                    int y =
                        centerY -
                        gridSize / 2 +
                        i * cell;

                    g.DrawLine(
                        gridPen,
                        x,
                        centerY -
                        gridSize / 2,
                        x,
                        centerY +
                        gridSize / 2);

                    g.DrawLine(
                        gridPen,
                        centerX -
                        gridSize / 2,
                        y,
                        centerX +
                        gridSize / 2,
                        y);
                }
            }

            using (Pen xPen =
                new Pen(
                    accentColor,
                    4F))
            {
                g.DrawLine(
                    xPen,
                    centerX - 48,
                    centerY - 48,
                    centerX - 18,
                    centerY - 18);

                g.DrawLine(
                    xPen,
                    centerX - 18,
                    centerY - 48,
                    centerX - 48,
                    centerY - 18);
            }

            using (Pen oPen =
                new Pen(
                    redBright,
                    4F))
            {
                g.DrawEllipse(
                    oPen,
                    centerX + 10,
                    centerY - 48,
                    30,
                    30);
            }

            using (Pen xPen =
                new Pen(
                    accentColor,
                    3F))
            {
                g.DrawLine(
                    xPen,
                    centerX + 10,
                    centerY + 18,
                    centerX + 40,
                    centerY + 48);

                g.DrawLine(
                    xPen,
                    centerX + 40,
                    centerY + 18,
                    centerX + 10,
                    centerY + 48);
            }

            using (Pen outerPen =
                new Pen(
                    Color.FromArgb(
                        28,
                        59,
                        82),
                    1F))
            {
                g.DrawRectangle(
                    outerPen,
                    centerX -
                    gridSize / 2 -
                    23,
                    centerY -
                    gridSize / 2 -
                    23,
                    gridSize + 46,
                    gridSize + 46);
            }

            using (Pen cornerPen =
                new Pen(
                    Color.FromArgb(
                        55,
                        145,
                        210),
                    2F))
            {
                int left =
                    centerX -
                    gridSize / 2 -
                    30;

                int top =
                    centerY -
                    gridSize / 2 -
                    30;

                int right =
                    centerX +
                    gridSize / 2 +
                    30;

                int bottom =
                    centerY +
                    gridSize / 2 +
                    30;

                g.DrawLine(
                    cornerPen,
                    left,
                    top,
                    left + 18,
                    top);

                g.DrawLine(
                    cornerPen,
                    left,
                    top,
                    left,
                    top + 18);

                g.DrawLine(
                    cornerPen,
                    right,
                    top,
                    right - 18,
                    top);

                g.DrawLine(
                    cornerPen,
                    right,
                    top,
                    right,
                    top + 18);

                g.DrawLine(
                    cornerPen,
                    left,
                    bottom,
                    left + 18,
                    bottom);

                g.DrawLine(
                    cornerPen,
                    left,
                    bottom,
                    left,
                    bottom - 18);

                g.DrawLine(
                    cornerPen,
                    right,
                    bottom,
                    right - 18,
                    bottom);

                g.DrawLine(
                    cornerPen,
                    right,
                    bottom,
                    right,
                    bottom - 18);
            }
        }

        private void DrawDecorativeLines(
            Graphics g)
        {
            int separatorX =
                (int)(
                    ClientSize.Width *
                    0.48F);

            using (Pen separatorPen =
                new Pen(
                    Color.FromArgb(
                        27,
                        49,
                        68),
                    1F))
            {
                g.DrawLine(
                    separatorPen,
                    separatorX,
                    90,
                    separatorX,
                    ClientSize.Height - 95);
            }

            using (Pen accentPen =
                new Pen(
                    Color.FromArgb(
                        60,
                        120,
                        175),
                    1F))
            {
                g.DrawLine(
                    accentPen,
                    65,
                    116,
                    285,
                    116);

                g.DrawLine(
                    accentPen,
                    65,
                    118,
                    175,
                    118);

                g.DrawLine(
                    accentPen,
                    ClientSize.Width - 465,
                    55,
                    ClientSize.Width - 65,
                    55);

                g.DrawLine(
                    accentPen,
                    ClientSize.Width - 465,
                    57,
                    ClientSize.Width - 280,
                    57);
            }
        }

        private void DrawTopBar(
            Graphics g)
        {
            using (Pen pen =
                new Pen(
                    Color.FromArgb(
                        23,
                        39,
                        53),
                    1F))
            {
                g.DrawLine(
                    pen,
                    40,
                    30,
                    ClientSize.Width - 40,
                    30);
            }

            using (SolidBrush brush =
                new SolidBrush(
                    Color.FromArgb(
                        50,
                        100,
                        135)))
            {
                g.FillRectangle(
                    brush,
                    40,
                    28,
                    80,
                    3);
            }
        }

        private void DrawBottomBar(
            Graphics g)
        {
            using (Pen pen =
                new Pen(
                    Color.FromArgb(
                        25,
                        40,
                        55),
                    1F))
            {
                g.DrawLine(
                    pen,
                    40,
                    ClientSize.Height - 90,
                    ClientSize.Width - 40,
                    ClientSize.Height - 90);
            }

            float pulse =
                0.5F +
                0.5F *
                (float)Math.Sin(
                    animationPhase * 2);

            using (SolidBrush brush =
                new SolidBrush(
                    Color.FromArgb(
                        55 +
                        (int)(pulse * 35),
                        65,
                        200,
                        125)))
            {
                g.FillEllipse(
                    brush,
                    40,
                    ClientSize.Height - 69,
                    6,
                    6);
            }
        }

        private void DrawSystemIndicators(
            Graphics g)
        {
            int y1 =
                ClientSize.Height - 165;

            int y2 =
                ClientSize.Height - 135;

            int y3 =
                ClientSize.Height - 105;

            using (SolidBrush greenBrush =
                new SolidBrush(
                    Color.FromArgb(
                        65,
                        210,
                        125)))
            {
                g.FillEllipse(
                    greenBrush,
                    48,
                    y1 + 7,
                    5,
                    5);

                g.FillEllipse(
                    greenBrush,
                    48,
                    y2 + 7,
                    5,
                    5);

                g.FillEllipse(
                    greenBrush,
                    48,
                    y3 + 7,
                    5,
                    5);
            }

            using (SolidBrush textBrush =
                new SolidBrush(
                    Color.FromArgb(
                        65,
                        150,
                        105)))
            using (Font font =
                new Font(
                    "Consolas",
                    7.5F,
                    FontStyle.Bold))
            {
                g.DrawString(
                    "ONLINE",
                    font,
                    textBrush,
                    175,
                    y1 + 2);

                g.DrawString(
                    "READY",
                    font,
                    textBrush,
                    175,
                    y2 + 2);

                g.DrawString(
                    "PVP ACTIVE",
                    font,
                    textBrush,
                    175,
                    y3 + 2);
            }
        }

        private void SetupPanel_Paint(
            object sender,
            PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            g.TextRenderingHint =
                TextRenderingHint.ClearTypeGridFit;

            using (SolidBrush background =
                new SolidBrush(panelColor))
            {
                g.FillRectangle(
                    background,
                    ClientRectangle);
            }

            using (Pen border =
                new Pen(
                    Color.FromArgb(
                        38,
                        54,
                        72),
                    1F))
            {
                g.DrawRectangle(
                    border,
                    0,
                    0,
                    setupPanel.Width - 1,
                    setupPanel.Height - 1);
            }

            using (Pen topAccent =
                new Pen(
                    accentColor,
                    2F))
            {
                g.DrawLine(
                    topAccent,
                    0,
                    0,
                    100,
                    0);
            }

            using (Pen bottomAccent =
                new Pen(
                    redColor,
                    2F))
            {
                g.DrawLine(
                    bottomAccent,
                    setupPanel.Width - 100,
                    setupPanel.Height - 1,
                    setupPanel.Width,
                    setupPanel.Height - 1);
            }

            using (Pen separator =
                new Pen(
                    Color.FromArgb(
                        26,
                        38,
                        52),
                    1F))
            {
                g.DrawLine(
                    separator,
                    125,
                    178,
                    setupPanel.Width - 30,
                    178);

                g.DrawLine(
                    separator,
                    125,
                    298,
                    setupPanel.Width - 30,
                    298);
            }

            using (Font infoFont =
                new Font(
                    "Consolas",
                    7.5F,
                    FontStyle.Bold))
            using (SolidBrush infoBrush =
                new SolidBrush(
                    Color.FromArgb(
                        80,
                        102,
                        125)))
            {
                g.DrawString(
                    "PLAYER IDENTITY",
                    infoFont,
                    infoBrush,
                    125,
                    68);

                g.DrawString(
                    "PLAYER IDENTITY",
                    infoFont,
                    infoBrush,
                    125,
                    188);
            }

            using (Font vsFont =
                new Font(
                    "Consolas",
                    8F,
                    FontStyle.Bold))
            using (SolidBrush vsBrush =
                new SolidBrush(
                    Color.FromArgb(
                        70,
                        90,
                        112)))
            {
                string text = "VS";

                SizeF size =
                    g.MeasureString(
                        text,
                        vsFont);

                g.DrawString(
                    text,
                    vsFont,
                    vsBrush,
                    setupPanel.Width / 2 -
                    size.Width / 2,
                    310);
            }
        }

        private void PrimaryButton_Paint(
            object sender,
            PaintEventArgs e)
        {
            if (!(sender is Button button))
                return;

            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            g.TextRenderingHint =
                TextRenderingHint.ClearTypeGridFit;

            bool hovered =
                hoveredButtons.Contains(button);

            string title =
                button.Tag as string;

            using (GraphicsPath path =
                CreateCutRectangle(
                    new Rectangle(
                        0,
                        0,
                        Math.Max(
                            1,
                            button.Width - 1),
                        Math.Max(
                            1,
                            button.Height - 1)),
                    11))
            {
                using (SolidBrush brush =
                    new SolidBrush(
                        hovered
                            ? Color.FromArgb(
                                24,
                                44,
                                64)
                            : Color.FromArgb(
                                15,
                                28,
                                42)))
                {
                    g.FillPath(
                        brush,
                        path);
                }

                using (Pen border =
                    new Pen(
                        hovered
                            ? accentBright
                            : Color.FromArgb(
                                48,
                                77,
                                103),
                        hovered
                            ? 1.5F
                            : 1F))
                {
                    g.DrawPath(
                        border,
                        path);
                }
            }

            using (SolidBrush accent =
                new SolidBrush(
                    hovered
                        ? accentBright
                        : accentColor))
            {
                g.FillRectangle(
                    accent,
                    0,
                    9,
                    3,
                    button.Height - 18);
            }

            using (SolidBrush textBrush =
                new SolidBrush(
                    hovered
                        ? Color.White
                        : textColor))
            using (Font font =
                new Font(
                    "Segoe UI",
                    9.5F,
                    FontStyle.Bold))
            {
                g.DrawString(
                    title,
                    font,
                    textBrush,
                    new PointF(
                        25,
                        16));
            }

            using (Pen arrowPen =
                new Pen(
                    hovered
                        ? accentBright
                        : Color.FromArgb(
                            70,
                            100,
                            125),
                    1.4F))
            {
                int x =
                    button.Width - 30;

                int y =
                    button.Height / 2;

                g.DrawLine(
                    arrowPen,
                    x,
                    y - 6,
                    x + 9,
                    y);

                g.DrawLine(
                    arrowPen,
                    x + 9,
                    y,
                    x,
                    y + 6);
            }
        }

        private void SecondaryButton_Paint(
            object sender,
            PaintEventArgs e)
        {
            if (!(sender is Button button))
                return;

            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            g.TextRenderingHint =
                TextRenderingHint.ClearTypeGridFit;

            bool hovered =
                hoveredButtons.Contains(button);

            string title =
                button.Tag as string;

            using (GraphicsPath path =
                CreateCutRectangle(
                    new Rectangle(
                        0,
                        0,
                        Math.Max(
                            1,
                            button.Width - 1),
                        Math.Max(
                            1,
                            button.Height - 1)),
                    11))
            {
                using (SolidBrush brush =
                    new SolidBrush(
                        hovered
                            ? Color.FromArgb(
                                23,
                                31,
                                43)
                            : Color.FromArgb(
                                12,
                                17,
                                25)))
                {
                    g.FillPath(
                        brush,
                        path);
                }

                using (Pen border =
                    new Pen(
                        hovered
                            ? Color.FromArgb(
                                80,
                                125,
                                160)
                            : Color.FromArgb(
                                35,
                                49,
                                66),
                        1F))
                {
                    g.DrawPath(
                        border,
                        path);
                }
            }

            using (SolidBrush textBrush =
                new SolidBrush(
                    hovered
                        ? Color.FromArgb(
                            215,
                            230,
                            245)
                        : Color.FromArgb(
                            175,
                            188,
                            205)))
            using (Font font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold))
            {
                g.DrawString(
                    title,
                    font,
                    textBrush,
                    new PointF(
                        25,
                        17));
            }

            using (Pen arrowPen =
                new Pen(
                    hovered
                        ? Color.FromArgb(
                            105,
                            205,
                            255)
                        : Color.FromArgb(
                            65,
                            85,
                            108),
                    1.2F))
            {
                int x =
                    button.Width - 29;

                int y =
                    button.Height / 2;

                g.DrawLine(
                    arrowPen,
                    x,
                    y - 5,
                    x + 8,
                    y);

                g.DrawLine(
                    arrowPen,
                    x + 8,
                    y,
                    x,
                    y + 5);
            }
        }

        private void GamingButton_MouseEnter(
            object sender,
            EventArgs e)
        {
            if (sender is Button button)
            {
                hoveredButtons.Add(button);
                button.Invalidate();
            }
        }

        private void GamingButton_MouseLeave(
            object sender,
            EventArgs e)
        {
            if (sender is Button button)
            {
                hoveredButtons.Remove(button);
                button.Invalidate();
            }
        }

        private void GamingButton_MouseDown(
            object sender,
            MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Invalidate();
            }
        }

        private void GamingButton_MouseUp(
            object sender,
            MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Invalidate();
            }
        }

        private void SetButtonRegion(
            Button button,
            int cut)
        {
            using (GraphicsPath path =
                CreateCutRectangle(
                    new Rectangle(
                        0,
                        0,
                        Math.Max(
                            1,
                            button.Width - 1),
                        Math.Max(
                            1,
                            button.Height - 1)),
                    cut))
            {
                button.Region =
                    new Region(path);
            }
        }

        private GraphicsPath CreateCutRectangle(
            Rectangle rectangle,
            int cut)
        {
            GraphicsPath path =
                new GraphicsPath();

            path.StartFigure();

            path.AddLine(
                rectangle.X + cut,
                rectangle.Y,
                rectangle.Right - cut,
                rectangle.Y);

            path.AddLine(
                rectangle.Right - cut,
                rectangle.Y,
                rectangle.Right,
                rectangle.Y + cut);

            path.AddLine(
                rectangle.Right,
                rectangle.Y + cut,
                rectangle.Right,
                rectangle.Bottom - cut);

            path.AddLine(
                rectangle.Right,
                rectangle.Bottom - cut,
                rectangle.Right - cut,
                rectangle.Bottom);

            path.AddLine(
                rectangle.Right - cut,
                rectangle.Bottom,
                rectangle.X + cut,
                rectangle.Bottom);

            path.AddLine(
                rectangle.X + cut,
                rectangle.Bottom,
                rectangle.X,
                rectangle.Bottom - cut);

            path.AddLine(
                rectangle.X,
                rectangle.Bottom - cut,
                rectangle.X,
                rectangle.Y + cut);

            path.AddLine(
                rectangle.X,
                rectangle.Y + cut,
                rectangle.X + cut,
                rectangle.Y);

            path.CloseFigure();

            return path;
        }

        private void TextBox_Enter(
            object sender,
            EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.BackColor =
                    inputFocusColor;

                textBox.ForeColor =
                    Color.White;
            }
        }

        private void TextBox_Leave(
            object sender,
            EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.BackColor =
                    inputColor;

                textBox.ForeColor =
                    textColor;
            }
        }

        private void PlayerTextBox_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnStart.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void PlayerSetupForm_Resize(
            object sender,
            EventArgs e)
        {
            UpdateLayout();
        }

        private void PlayerSetupForm_Load(
            object sender,
            EventArgs e)
        {
            txtPlayer1.Focus();
        }

        private void btnStart_Click(
            object sender,
            EventArgs e)
        {
            string player1 =
                txtPlayer1.Text.Trim();

            string player2 =
                txtPlayer2.Text.Trim();

            if (string.IsNullOrWhiteSpace(player1) ||
                string.IsNullOrWhiteSpace(player2))
            {
                MessageBox.Show(
                    "Please enter both player names.",
                    "Player Setup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.Equals(
                player1,
                player2,
                StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Player names must be different.",
                    "Player Setup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            GameForm game =
                new GameForm(
                    player1,
                    player2);

            game.Show();

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

        private void PlayerSetupForm_FormClosed(
            object sender,
            FormClosedEventArgs e)
        {
            if (animationTimer != null)
            {
                animationTimer.Stop();
                animationTimer.Dispose();
                animationTimer = null;
            }
        }

        private void lblTitle_Click(
            object sender,
            EventArgs e)
        {
        }
    }
}