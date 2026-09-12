using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace AdvancedTicTacToeWinForms
{
    public partial class MainMenuForm : Form
    {
        private readonly Color backgroundColor = Color.FromArgb(6, 9, 15);
        private readonly Color buttonColor = Color.FromArgb(15, 21, 31);
        private readonly Color buttonHoverColor = Color.FromArgb(23, 33, 47);
        private readonly Color accentColor = Color.FromArgb(55, 165, 255);
        private readonly Color accentBright = Color.FromArgb(105, 205, 255);
        private readonly Color textColor = Color.FromArgb(240, 244, 252);
        private readonly Color mutedColor = Color.FromArgb(105, 120, 140);
        private readonly Color dangerColor = Color.FromArgb(220, 72, 82);

        private readonly HashSet<Button> hoveredButtons = new HashSet<Button>();

        private readonly Dictionary<Button, float> buttonOffsets =
            new Dictionary<Button, float>();

        private readonly Dictionary<Button, int> baseButtonX =
            new Dictionary<Button, int>();

        private Timer animationTimer;
        private float animationPhase;

        private PointF[] particles;
        private float[] particleSpeeds;

        private Label lblEdition;
        private Label lblHeroTop;
        private Label lblHeroSub;
        private Label lblModeTitle;
        private Label lblModeSub;
        private Label lblStatus;
        private Label lblVersion;

        private Label lblEngine;
        private Label lblAI;
        private Label lblTournament;

        public MainMenuForm()
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
            CreateParticles();
            SetupButtons();
            UpdateLayout();
            StartAnimation();

            Resize += MainMenuForm_Resize;
        }

        private void ApplyGamingTheme()
        {
            Text = "Advanced Tic Tac Toe";
            BackColor = backgroundColor;

            ClientSize = new Size(1200, 760);
            MinimumSize = new Size(1000, 650);

            StartPosition = FormStartPosition.CenterScreen;

            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;

            Paint += MainMenuForm_Paint;

            lblTitle.Text = "ADVANCED\r\nTIC TAC TOE";
            lblTitle.Font = new Font(
                "Segoe UI",
                29F,
                FontStyle.Bold);

            lblTitle.ForeColor = textColor;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.AutoSize = false;
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            lblFooter.Text = "Developed by Syed Hussain Abbas";
            lblFooter.Font = new Font(
                "Segoe UI",
                8.5F,
                FontStyle.Regular);

            lblFooter.ForeColor =
                Color.FromArgb(105, 120, 142);

            lblFooter.BackColor = Color.Transparent;
            lblFooter.AutoSize = false;
            lblFooter.TextAlign =
                ContentAlignment.MiddleLeft;
        }

        private void CreateInterfaceLabels()
        {
            lblEdition = CreateLabel(
                "TACTICAL EDITION  /  01",
                new Font(
                    "Consolas",
                    8.5F,
                    FontStyle.Bold),
                Color.FromArgb(78, 160, 220));

            lblHeroTop = CreateLabel(
                "STRATEGY  •  PRECISION  •  COMPETITION",
                new Font(
                    "Consolas",
                    8.5F,
                    FontStyle.Bold),
                Color.FromArgb(105, 120, 142));

            lblHeroSub = CreateLabel(
                "A COMPETITIVE TIC TAC TOE EXPERIENCE",
                new Font(
                    "Segoe UI",
                    9.5F,
                    FontStyle.Bold),
                accentBright);

            lblModeTitle = CreateLabel(
                "SELECT GAME MODE",
                new Font(
                    "Segoe UI",
                    17F,
                    FontStyle.Bold),
                textColor);

            lblModeSub = CreateLabel(
                "CHOOSE YOUR BATTLE",
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
                "BUILD 1.0  //  C#  //  WINFORMS",
                new Font(
                    "Consolas",
                    8F,
                    FontStyle.Regular),
                Color.FromArgb(70, 85, 105));

            lblEngine = CreateLabel(
                "GAME ENGINE",
                new Font(
                    "Consolas",
                    8F,
                    FontStyle.Bold),
                Color.FromArgb(125, 140, 158));

            lblAI = CreateLabel(
                "AI CORE",
                new Font(
                    "Consolas",
                    8F,
                    FontStyle.Bold),
                Color.FromArgb(125, 140, 158));

            lblTournament = CreateLabel(
                "TOURNAMENT",
                new Font(
                    "Consolas",
                    8F,
                    FontStyle.Bold),
                Color.FromArgb(125, 140, 158));

            Controls.Add(lblEdition);
            Controls.Add(lblHeroTop);
            Controls.Add(lblHeroSub);
            Controls.Add(lblModeTitle);
            Controls.Add(lblModeSub);
            Controls.Add(lblStatus);
            Controls.Add(lblVersion);
            Controls.Add(lblEngine);
            Controls.Add(lblAI);
            Controls.Add(lblTournament);
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

        private void SetupButtons()
        {
            ConfigurePrimaryButton(
                btnPvP,
                "01",
                "PLAYER VS PLAYER",
                "LOCAL HEAD-TO-HEAD");

            ConfigurePrimaryButton(
                btnPvAI,
                "02",
                "PLAYER VS AI",
                "TACTICAL AI CHALLENGE");

            ConfigurePrimaryButton(
                btnTM,
                "03",
                "TOURNAMENT",
                "KNOCKOUT CHAMPIONSHIP");

            ConfigureSecondaryButton(
                btnMH,
                "MATCH HISTORY");

            ConfigureSecondaryButton(
                btnLB,
                "LEADERBOARD");

            ConfigureExitButton(btnEX);
        }

        private void ConfigurePrimaryButton(
            Button button,
            string number,
            string title,
            string subtitle)
        {
            button.Text = "";
            button.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);

            button.ForeColor = Color.Transparent;
            button.BackColor = buttonColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.UseVisualStyleBackColor = false;
            button.Cursor = Cursors.Hand;
            button.TabStop = false;

            button.Tag = new string[]
            {
                number,
                title,
                subtitle
            };

            buttonOffsets[button] = 0;

            button.Paint += GamingButton_Paint;
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
            button.BackColor =
                Color.FromArgb(12, 17, 25);

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.UseVisualStyleBackColor = false;
            button.Cursor = Cursors.Hand;
            button.TabStop = false;

            button.Tag = new string[]
            {
                title
            };

            buttonOffsets[button] = 0;

            button.Paint += SecondaryButton_Paint;
            button.MouseEnter += GamingButton_MouseEnter;
            button.MouseLeave += GamingButton_MouseLeave;
            button.MouseDown += GamingButton_MouseDown;
            button.MouseUp += GamingButton_MouseUp;
        }

        private void ConfigureExitButton(Button button)
        {
            button.Text = "";
            button.ForeColor = Color.Transparent;
            button.BackColor =
                Color.FromArgb(12, 16, 23);

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.UseVisualStyleBackColor = false;
            button.Cursor = Cursors.Hand;
            button.TabStop = false;

            buttonOffsets[button] = 0;

            button.Paint += ExitButton_Paint;
            button.MouseEnter += GamingButton_MouseEnter;
            button.MouseLeave += GamingButton_MouseLeave;
            button.MouseDown += GamingButton_MouseDown;
            button.MouseUp += GamingButton_MouseUp;
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
                button.Region = new Region(path);
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

        private void GamingButton_Paint(
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

            string[] data =
                button.Tag as string[];

            if (data == null)
                return;

            Color fill =
                hovered
                    ? buttonHoverColor
                    : buttonColor;

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
                    12))
            {
                using (SolidBrush brush =
                    new SolidBrush(fill))
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
                                40,
                                55,
                                74),
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
                    10,
                    3,
                    Math.Max(
                        1,
                        button.Height - 20));
            }

            using (SolidBrush numberBrush =
                new SolidBrush(
                    hovered
                        ? accentBright
                        : Color.FromArgb(
                            75,
                            110,
                            140)))
            {
                using (Font numberFont =
                    new Font(
                        "Consolas",
                        9F,
                        FontStyle.Bold))
                {
                    g.DrawString(
                        data[0],
                        numberFont,
                        numberBrush,
                        new PointF(
                            22,
                            12));
                }
            }

            using (SolidBrush titleBrush =
                new SolidBrush(textColor))
            {
                using (Font titleFont =
                    new Font(
                        "Segoe UI",
                        10.5F,
                        FontStyle.Bold))
                {
                    g.DrawString(
                        data[1],
                        titleFont,
                        titleBrush,
                        new PointF(
                            60,
                            8));
                }
            }

            using (SolidBrush subtitleBrush =
                new SolidBrush(
                    hovered
                        ? Color.FromArgb(
                            150,
                            180,
                            208)
                        : mutedColor))
            {
                using (Font subtitleFont =
                    new Font(
                        "Consolas",
                        7.2F,
                        FontStyle.Regular))
                {
                    g.DrawString(
                        data[2],
                        subtitleFont,
                        subtitleBrush,
                        new PointF(
                            60,
                            34));
                }
            }

            using (Pen arrowPen =
                new Pen(
                    hovered
                        ? accentBright
                        : Color.FromArgb(
                            68,
                            90,
                            115),
                    1.4F))
            {
                int arrowX =
                    button.Width - 31;

                int arrowY =
                    button.Height / 2;

                g.DrawLine(
                    arrowPen,
                    arrowX,
                    arrowY - 6,
                    arrowX + 9,
                    arrowY);

                g.DrawLine(
                    arrowPen,
                    arrowX + 9,
                    arrowY,
                    arrowX,
                    arrowY + 6);
            }

            if (hovered)
            {
                using (Pen glowPen =
                    new Pen(
                        Color.FromArgb(
                            45,
                            accentColor.R,
                            accentColor.G,
                            accentColor.B),
                        1F))
                {
                    g.DrawLine(
                        glowPen,
                        18,
                        button.Height - 5,
                        button.Width - 18,
                        button.Height - 5);
                }
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

            string[] data =
                button.Tag as string[];

            if (data == null)
                return;

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
                    10))
            {
                using (SolidBrush brush =
                    new SolidBrush(
                        hovered
                            ? Color.FromArgb(
                                21,
                                30,
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
                                70,
                                150,
                                210)
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
                        ? accentBright
                        : Color.FromArgb(
                            185,
                            195,
                            210)))
            {
                using (Font font =
                    new Font(
                        "Segoe UI",
                        8.5F,
                        FontStyle.Bold))
                {
                    g.DrawString(
                        data[0],
                        font,
                        textBrush,
                        new PointF(
                            22,
                            14));
                }
            }

            using (Pen arrowPen =
                new Pen(
                    hovered
                        ? accentBright
                        : Color.FromArgb(
                            65,
                            85,
                            108),
                    1.2F))
            {
                int arrowX =
                    button.Width - 28;

                int arrowY =
                    button.Height / 2;

                g.DrawLine(
                    arrowPen,
                    arrowX,
                    arrowY - 5,
                    arrowX + 8,
                    arrowY);

                g.DrawLine(
                    arrowPen,
                    arrowX + 8,
                    arrowY,
                    arrowX,
                    arrowY + 5);
            }
        }

        private void ExitButton_Paint(
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
                    10))
            {
                using (SolidBrush brush =
                    new SolidBrush(
                        hovered
                            ? Color.FromArgb(
                                34,
                                19,
                                24)
                            : Color.FromArgb(
                                12,
                                16,
                                23)))
                {
                    g.FillPath(
                        brush,
                        path);
                }

                using (Pen border =
                    new Pen(
                        hovered
                            ? Color.FromArgb(
                                235,
                                82,
                                92)
                            : Color.FromArgb(
                                90,
                                45,
                                54),
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
                            255,
                            130,
                            137)
                        : dangerColor))
            {
                using (Font font =
                    new Font(
                        "Segoe UI",
                        8.5F,
                        FontStyle.Bold))
                {
                    g.DrawString(
                        "EXIT SYSTEM",
                        font,
                        textBrush,
                        new PointF(
                            22,
                            14));
                }
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
                button.BackColor =
                    Color.FromArgb(
                        31,
                        44,
                        60);

                button.Invalidate();
            }
        }

        private void GamingButton_MouseUp(
            object sender,
            MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.BackColor =
                    hoveredButtons.Contains(button)
                        ? buttonHoverColor
                        : buttonColor;

                button.Invalidate();
            }
        }

        private void UpdateLayout()
        {
            int width = ClientSize.Width;
            int height = ClientSize.Height;

            int leftWidth =
                (int)(width * 0.52F);

            int rightX =
                leftWidth + 48;

            int rightWidth =
                Math.Max(
                    320,
                    width - rightX - 55);

            int heroTextWidth =
                Math.Max(
                    300,
                    leftWidth - 100);

            lblEdition.Location =
                new Point(
                    65,
                    48);

            lblEdition.Size =
                new Size(
                    heroTextWidth,
                    24);

            lblTitle.Location =
                new Point(
                    65,
                    112);

            lblTitle.Size =
                new Size(
                    heroTextWidth,
                    90);

            lblHeroTop.Location =
                new Point(
                    68,
                    205);

            lblHeroTop.Size =
                new Size(
                    heroTextWidth,
                    22);

            lblHeroSub.Location =
                new Point(
                    68,
                    237);

            lblHeroSub.Size =
                new Size(
                    heroTextWidth,
                    24);

            lblModeTitle.Location =
                new Point(
                    rightX,
                    82);

            lblModeTitle.Size =
                new Size(
                    rightWidth,
                    35);

            lblModeSub.Location =
                new Point(
                    rightX + 2,
                    116);

            lblModeSub.Size =
                new Size(
                    rightWidth,
                    22);

            int primaryWidth =
                Math.Min(
                    410,
                    rightWidth);

            int primaryHeight = 62;

            btnPvP.Size =
                new Size(
                    primaryWidth,
                    primaryHeight);

            btnPvAI.Size =
                new Size(
                    primaryWidth,
                    primaryHeight);

            btnTM.Size =
                new Size(
                    primaryWidth,
                    primaryHeight);

            int buttonX = rightX;

            btnPvP.Location =
                new Point(
                    buttonX,
                    160);

            btnPvAI.Location =
                new Point(
                    buttonX,
                    234);

            btnTM.Location =
                new Point(
                    buttonX,
                    308);

            SetButtonRegion(
                btnPvP,
                12);

            SetButtonRegion(
                btnPvAI,
                12);

            SetButtonRegion(
                btnTM,
                12);

            int secondaryWidth =
                Math.Max(
                    120,
                    (primaryWidth - 12) / 2);

            btnMH.Size =
                new Size(
                    secondaryWidth,
                    50);

            btnLB.Size =
                new Size(
                    secondaryWidth,
                    50);

            btnMH.Location =
                new Point(
                    buttonX,
                    395);

            btnLB.Location =
                new Point(
                    buttonX +
                    secondaryWidth +
                    12,
                    395);

            SetButtonRegion(
                btnMH,
                10);

            SetButtonRegion(
                btnLB,
                10);

            btnEX.Size =
                new Size(
                    primaryWidth,
                    48);

            btnEX.Location =
                new Point(
                    buttonX,
                    465);

            SetButtonRegion(
                btnEX,
                10);

            baseButtonX[btnPvP] =
                btnPvP.Left;

            baseButtonX[btnPvAI] =
                btnPvAI.Left;

            baseButtonX[btnTM] =
                btnTM.Left;

            baseButtonX[btnMH] =
                btnMH.Left;

            baseButtonX[btnLB] =
                btnLB.Left;

            baseButtonX[btnEX] =
                btnEX.Left;

            lblEngine.Location =
                new Point(
                    68,
                    height - 170);

            lblEngine.Size =
                new Size(
                    130,
                    22);

            lblAI.Location =
                new Point(
                    68,
                    height - 140);

            lblAI.Size =
                new Size(
                    130,
                    22);

            lblTournament.Location =
                new Point(
                    68,
                    height - 110);

            lblTournament.Size =
                new Size(
                    130,
                    22);

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
                    width - 285,
                    height - 72);

            lblVersion.Size =
                new Size(
                    220,
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

            Button[] buttons =
            {
                btnPvP,
                btnPvAI,
                btnTM,
                btnMH,
                btnLB,
                btnEX
            };

            foreach (Button button in buttons)
            {
                buttonOffsets[button] = 0;

                if (baseButtonX.ContainsKey(button))
                {
                    button.Left =
                        baseButtonX[button];
                }
            }

            Invalidate();
        }

        private void CreateParticles()
        {
            Random random =
                new Random();

            particles =
                new PointF[50];

            particleSpeeds =
                new float[50];

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
                    0.12F +
                    (float)random.NextDouble() *
                    0.4F;
            }
        }

        private void StartAnimation()
        {
            animationTimer =
                new Timer();

            animationTimer.Interval = 30;

            animationTimer.Tick +=
                AnimationTimer_Tick;

            animationTimer.Start();
        }

        private void AnimationTimer_Tick(
            object sender,
            EventArgs e)
        {
            animationPhase += 0.018F;

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
                            -5);
                }
            }

            Button[] buttons =
            {
                btnPvP,
                btnPvAI,
                btnTM,
                btnMH,
                btnLB,
                btnEX
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
                    0.18F;

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

        private void MainMenuForm_Paint(
            object sender,
            PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            using (LinearGradientBrush background =
                new LinearGradientBrush(
                    ClientRectangle,
                    Color.FromArgb(
                        5,
                        8,
                        13),
                    Color.FromArgb(
                        10,
                        16,
                        24),
                    35F))
            {
                g.FillRectangle(
                    background,
                    ClientRectangle);
            }

            DrawTacticalGrid(g);
            DrawParticles(g);
            DrawHeroEmblem(g);
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
                        15,
                        28,
                        40),
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

            using (SolidBrush brush =
                new SolidBrush(
                    Color.FromArgb(
                        30 +
                        (int)(pulse * 25),
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

        private void DrawHeroEmblem(
            Graphics g)
        {
            int leftWidth =
                (int)(ClientSize.Width * 0.52F);

            int heroTextLeft = 68;

            int heroTextWidth =
                Math.Max(
                    300,
                    leftWidth - 100);

            int centerX =
                heroTextLeft +
                heroTextWidth / 2;

            int centerY =
                ClientSize.Height / 2 +
                10;

            int cell = 54;
            int gridSize = cell * 3;

            using (Pen glowPen =
                new Pen(
                    Color.FromArgb(
                        20,
                        55,
                        165,
                        255),
                    7F))
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
                        55,
                        95,
                        125),
                    2F))
            {
                for (int i = 1;
                     i < 3;
                     i++)
                {
                    int offset =
                        centerX -
                        gridSize / 2 +
                        i * cell;

                    g.DrawLine(
                        gridPen,
                        offset,
                        centerY -
                        gridSize / 2,
                        offset,
                        centerY +
                        gridSize / 2);

                    int horizontal =
                        centerY -
                        gridSize / 2 +
                        i * cell;

                    g.DrawLine(
                        gridPen,
                        centerX -
                        gridSize / 2,
                        horizontal,
                        centerX +
                        gridSize / 2,
                        horizontal);
                }
            }

            using (Pen xPen =
                new Pen(
                    Color.FromArgb(
                        85,
                        180,
                        255),
                    5F))
            {
                g.DrawLine(
                    xPen,
                    centerX - 63,
                    centerY - 63,
                    centerX - 25,
                    centerY - 25);

                g.DrawLine(
                    xPen,
                    centerX - 25,
                    centerY - 63,
                    centerX - 63,
                    centerY - 25);
            }

            using (Pen oPen =
                new Pen(
                    Color.FromArgb(
                        215,
                        225,
                        240),
                    5F))
            {
                g.DrawEllipse(
                    oPen,
                    centerX + 20,
                    centerY - 63,
                    38,
                    38);
            }

            using (Pen xPen =
                new Pen(
                    Color.FromArgb(
                        85,
                        180,
                        255),
                    4F))
            {
                g.DrawLine(
                    xPen,
                    centerX + 20,
                    centerY + 25,
                    centerX + 58,
                    centerY + 63);

                g.DrawLine(
                    xPen,
                    centerX + 58,
                    centerY + 25,
                    centerX + 20,
                    centerY + 63);
            }

            using (Pen outerPen =
                new Pen(
                    Color.FromArgb(
                        32,
                        65,
                        88),
                    1F))
            {
                g.DrawRectangle(
                    outerPen,
                    centerX -
                    gridSize / 2 -
                    25,
                    centerY -
                    gridSize / 2 -
                    25,
                    gridSize + 50,
                    gridSize + 50);
            }

            using (Pen cornerPen =
                new Pen(
                    Color.FromArgb(
                        65,
                        155,
                        220),
                    2F))
            {
                int left =
                    centerX -
                    gridSize / 2 -
                    32;

                int top =
                    centerY -
                    gridSize / 2 -
                    32;

                int right =
                    centerX +
                    gridSize / 2 +
                    32;

                int bottom =
                    centerY +
                    gridSize / 2 +
                    32;

                g.DrawLine(
                    cornerPen,
                    left,
                    top,
                    left + 22,
                    top);

                g.DrawLine(
                    cornerPen,
                    left,
                    top,
                    left,
                    top + 22);

                g.DrawLine(
                    cornerPen,
                    right,
                    top,
                    right - 22,
                    top);

                g.DrawLine(
                    cornerPen,
                    right,
                    top,
                    right,
                    top + 22);

                g.DrawLine(
                    cornerPen,
                    left,
                    bottom,
                    left + 22,
                    bottom);

                g.DrawLine(
                    cornerPen,
                    left,
                    bottom,
                    left,
                    bottom - 22);

                g.DrawLine(
                    cornerPen,
                    right,
                    bottom,
                    right - 22,
                    bottom);

                g.DrawLine(
                    cornerPen,
                    right,
                    bottom,
                    right,
                    bottom - 22);
            }
        }

        private void DrawDecorativeLines(
            Graphics g)
        {
            int width =
                ClientSize.Width;

            int height =
                ClientSize.Height;

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
                    width - 465,
                    55,
                    width - 65,
                    55);

                g.DrawLine(
                    accentPen,
                    width - 465,
                    57,
                    width - 280,
                    57);
            }

            using (Pen separatorPen =
                new Pen(
                    Color.FromArgb(
                        27,
                        49,
                        68),
                    1F))
            {
                int separatorX =
                    (int)(
                        ClientSize.Width *
                        0.52F);

                g.DrawLine(
                    separatorPen,
                    separatorX,
                    90,
                    separatorX,
                    height - 95);
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
                        60 +
                        (int)(pulse * 40),
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
            {
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
                        "AVAILABLE",
                        font,
                        textBrush,
                        175,
                        y3 + 2);
                }
            }
        }

        private void MainMenuForm_Resize(
            object sender,
            EventArgs e)
        {
            UpdateLayout();
        }

        private void Form1_Load(
            object sender,
            EventArgs e)
        {
        }

        private void label1_Click(
            object sender,
            EventArgs e)
        {
        }

        private void lblTitle_Click(
            object sender,
            EventArgs e)
        {
        }

        private void btnEX_Click(
            object sender,
            EventArgs e)
        {
            if (animationTimer != null)
            {
                animationTimer.Stop();
            }

            Application.Exit();
        }

        private void btnPvP_Click(
            object sender,
            EventArgs e)
        {
            PlayerSetupForm setup =
                new PlayerSetupForm();

            setup.Show();
            Hide();
        }

        private void btnPvAI_Click(
            object sender,
            EventArgs e)
        {
            AISetupForm aiSetup =
                new AISetupForm();

            aiSetup.Show();
            Hide();
        }

        private void btnTM_Click(
            object sender,
            EventArgs e)
        {
            TournamentForm form =
                new TournamentForm();

            form.Show();
            Hide();
        }

        private void btnMH_Click(
            object sender,
            EventArgs e)
        {
            HistoryForm history =
                new HistoryForm();

            history.Show();
            Hide();
        }

        private void btnLB_Click(
            object sender,
            EventArgs e)
        {
            LeaderboardForm form =
                new LeaderboardForm();

            form.Show();
            Hide();
        }
    }
}
