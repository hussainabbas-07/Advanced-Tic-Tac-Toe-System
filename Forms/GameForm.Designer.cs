using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedTicTacToeWinForms
{
    partial class GameForm
    {
        private IContainer components = null;

    protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblStatus = new Label();
            this.lblToss = new Label();
            this.lblScoreboardHeader = new Label();
            this.lblXWins = new Label();
            this.lblOWins = new Label();
            this.lblDraws = new Label();
            this.btn1 = new Button();
            this.btn2 = new Button();
            this.btn3 = new Button();
            this.btn4 = new Button();
            this.btn5 = new Button();
            this.btn6 = new Button();
            this.btn7 = new Button();
            this.btn8 = new Button();
            this.btn9 = new Button();
            this.btnReset = new Button();
            this.btnBack = new Button();
            this.btnHistory = new Button();
            this.btnLeaderboard = new Button();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(330, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(320, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ADVANCED TIC TAC TOE";

            this.lblStatus.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblStatus.ForeColor = Color.DeepSkyBlue;
            this.lblStatus.Location = new Point(290, 80);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(400, 30);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Player X Turn";
            this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            this.lblStatus.Click += new EventHandler(this.lblStatus_Click);

            this.lblToss.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblToss.ForeColor = Color.Gold;
            this.lblToss.Location = new Point(290, 115);
            this.lblToss.Name = "lblToss";
            this.lblToss.Size = new Size(400, 25);
            this.lblToss.TabIndex = 2;
            this.lblToss.Text = "Toss Winner: -";
            this.lblToss.TextAlign = ContentAlignment.MiddleCenter;
            this.lblToss.Click += new EventHandler(this.lblToss_Click);

            this.lblScoreboardHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblScoreboardHeader.ForeColor = Color.White;
            this.lblScoreboardHeader.Location = new Point(55, 145);
            this.lblScoreboardHeader.Name = "lblScoreboardHeader";
            this.lblScoreboardHeader.Size = new Size(180, 30);
            this.lblScoreboardHeader.TabIndex = 3;
            this.lblScoreboardHeader.Text = "SCOREBOARD";
            this.lblScoreboardHeader.TextAlign = ContentAlignment.MiddleCenter;

            this.lblXWins.BackColor = Color.FromArgb(25, 35, 50);
            this.lblXWins.BorderStyle = BorderStyle.FixedSingle;
            this.lblXWins.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblXWins.ForeColor = Color.DeepSkyBlue;
            this.lblXWins.Location = new Point(55, 185);
            this.lblXWins.Name = "lblXWins";
            this.lblXWins.Size = new Size(180, 45);
            this.lblXWins.TabIndex = 4;
            this.lblXWins.Text = "X Wins : 0";
            this.lblXWins.TextAlign = ContentAlignment.MiddleCenter;

            this.lblOWins.BackColor = Color.FromArgb(25, 35, 50);
            this.lblOWins.BorderStyle = BorderStyle.FixedSingle;
            this.lblOWins.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblOWins.ForeColor = Color.Crimson;
            this.lblOWins.Location = new Point(55, 240);
            this.lblOWins.Name = "lblOWins";
            this.lblOWins.Size = new Size(180, 45);
            this.lblOWins.TabIndex = 5;
            this.lblOWins.Text = "O Wins : 0";
            this.lblOWins.TextAlign = ContentAlignment.MiddleCenter;

            this.lblDraws.BackColor = Color.FromArgb(25, 35, 50);
            this.lblDraws.BorderStyle = BorderStyle.FixedSingle;
            this.lblDraws.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblDraws.ForeColor = Color.Gold;
            this.lblDraws.Location = new Point(55, 295);
            this.lblDraws.Name = "lblDraws";
            this.lblDraws.Size = new Size(180, 45);
            this.lblDraws.TabIndex = 6;
            this.lblDraws.Text = "Draws : 0";
            this.lblDraws.TextAlign = ContentAlignment.MiddleCenter;

            this.btn1.BackColor = Color.FromArgb(25, 35, 50);
            this.btn1.FlatAppearance.BorderColor = Color.FromArgb(60, 70, 90);
            this.btn1.FlatAppearance.BorderSize = 2;
            this.btn1.FlatStyle = FlatStyle.Flat;
            this.btn1.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            this.btn1.ForeColor = Color.White;
            this.btn1.Location = new Point(335, 160);
            this.btn1.Name = "btn1";
            this.btn1.Size = new Size(90, 90);
            this.btn1.TabIndex = 7;
            this.btn1.UseVisualStyleBackColor = false;
            this.btn1.Click += new EventHandler(this.btn1_Click);

            this.btn2.BackColor = Color.FromArgb(25, 35, 50);
            this.btn2.FlatAppearance.BorderColor = Color.FromArgb(60, 70, 90);
            this.btn2.FlatAppearance.BorderSize = 2;
            this.btn2.FlatStyle = FlatStyle.Flat;
            this.btn2.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            this.btn2.ForeColor = Color.White;
            this.btn2.Location = new Point(435, 160);
            this.btn2.Name = "btn2";
            this.btn2.Size = new Size(90, 90);
            this.btn2.TabIndex = 8;
            this.btn2.UseVisualStyleBackColor = false;
            this.btn2.Click += new EventHandler(this.btn2_Click);

            this.btn3.BackColor = Color.FromArgb(25, 35, 50);
            this.btn3.FlatAppearance.BorderColor = Color.FromArgb(60, 70, 90);
            this.btn3.FlatAppearance.BorderSize = 2;
            this.btn3.FlatStyle = FlatStyle.Flat;
            this.btn3.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            this.btn3.ForeColor = Color.White;
            this.btn3.Location = new Point(535, 160);
            this.btn3.Name = "btn3";
            this.btn3.Size = new Size(90, 90);
            this.btn3.TabIndex = 9;
            this.btn3.UseVisualStyleBackColor = false;
            this.btn3.Click += new EventHandler(this.btn3_Click);

            this.btn4.BackColor = Color.FromArgb(25, 35, 50);
            this.btn4.FlatAppearance.BorderColor = Color.FromArgb(60, 70, 90);
            this.btn4.FlatAppearance.BorderSize = 2;
            this.btn4.FlatStyle = FlatStyle.Flat;
            this.btn4.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            this.btn4.ForeColor = Color.White;
            this.btn4.Location = new Point(335, 260);
            this.btn4.Name = "btn4";
            this.btn4.Size = new Size(90, 90);
            this.btn4.TabIndex = 10;
            this.btn4.UseVisualStyleBackColor = false;
            this.btn4.Click += new EventHandler(this.btn4_Click);

            this.btn5.BackColor = Color.FromArgb(25, 35, 50);
            this.btn5.FlatAppearance.BorderColor = Color.FromArgb(60, 70, 90);
            this.btn5.FlatAppearance.BorderSize = 2;
            this.btn5.FlatStyle = FlatStyle.Flat;
            this.btn5.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            this.btn5.ForeColor = Color.White;
            this.btn5.Location = new Point(435, 260);
            this.btn5.Name = "btn5";
            this.btn5.Size = new Size(90, 90);
            this.btn5.TabIndex = 11;
            this.btn5.UseVisualStyleBackColor = false;
            this.btn5.Click += new EventHandler(this.btn5_Click);

            this.btn6.BackColor = Color.FromArgb(25, 35, 50);
            this.btn6.FlatAppearance.BorderColor = Color.FromArgb(60, 70, 90);
            this.btn6.FlatAppearance.BorderSize = 2;
            this.btn6.FlatStyle = FlatStyle.Flat;
            this.btn6.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            this.btn6.ForeColor = Color.White;
            this.btn6.Location = new Point(535, 260);
            this.btn6.Name = "btn6";
            this.btn6.Size = new Size(90, 90);
            this.btn6.TabIndex = 12;
            this.btn6.UseVisualStyleBackColor = false;
            this.btn6.Click += new EventHandler(this.btn6_Click);

            this.btn7.BackColor = Color.FromArgb(25, 35, 50);
            this.btn7.FlatAppearance.BorderColor = Color.FromArgb(60, 70, 90);
            this.btn7.FlatAppearance.BorderSize = 2;
            this.btn7.FlatStyle = FlatStyle.Flat;
            this.btn7.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            this.btn7.ForeColor = Color.White;
            this.btn7.Location = new Point(335, 360);
            this.btn7.Name = "btn7";
            this.btn7.Size = new Size(90, 90);
            this.btn7.TabIndex = 13;
            this.btn7.UseVisualStyleBackColor = false;
            this.btn7.Click += new EventHandler(this.btn7_Click);

            this.btn8.BackColor = Color.FromArgb(25, 35, 50);
            this.btn8.FlatAppearance.BorderColor = Color.FromArgb(60, 70, 90);
            this.btn8.FlatAppearance.BorderSize = 2;
            this.btn8.FlatStyle = FlatStyle.Flat;
            this.btn8.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            this.btn8.ForeColor = Color.White;
            this.btn8.Location = new Point(435, 360);
            this.btn8.Name = "btn8";
            this.btn8.Size = new Size(90, 90);
            this.btn8.TabIndex = 14;
            this.btn8.UseVisualStyleBackColor = false;
            this.btn8.Click += new EventHandler(this.btn8_Click);

            this.btn9.BackColor = Color.FromArgb(25, 35, 50);
            this.btn9.FlatAppearance.BorderColor = Color.FromArgb(60, 70, 90);
            this.btn9.FlatAppearance.BorderSize = 2;
            this.btn9.FlatStyle = FlatStyle.Flat;
            this.btn9.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            this.btn9.ForeColor = Color.White;
            this.btn9.Location = new Point(535, 360);
            this.btn9.Name = "btn9";
            this.btn9.Size = new Size(90, 90);
            this.btn9.TabIndex = 15;
            this.btn9.UseVisualStyleBackColor = false;
            this.btn9.Click += new EventHandler(this.btn9_Click);

            this.btnHistory.BackColor = Color.FromArgb(25, 35, 50);
            this.btnHistory.FlatAppearance.BorderColor = Color.DeepSkyBlue;
            this.btnHistory.FlatAppearance.BorderSize = 1;
            this.btnHistory.FlatStyle = FlatStyle.Flat;
            this.btnHistory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnHistory.ForeColor = Color.DeepSkyBlue;
            this.btnHistory.Location = new Point(735, 185);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new Size(170, 42);
            this.btnHistory.TabIndex = 16;
            this.btnHistory.Text = "MATCH HISTORY";
            this.btnHistory.UseVisualStyleBackColor = false;
            this.btnHistory.Click += new EventHandler(this.btnHistory_Click);

            this.btnLeaderboard.BackColor = Color.FromArgb(25, 35, 50);
            this.btnLeaderboard.FlatAppearance.BorderColor = Color.Gold;
            this.btnLeaderboard.FlatAppearance.BorderSize = 1;
            this.btnLeaderboard.FlatStyle = FlatStyle.Flat;
            this.btnLeaderboard.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLeaderboard.ForeColor = Color.Gold;
            this.btnLeaderboard.Location = new Point(735, 240);
            this.btnLeaderboard.Name = "btnLeaderboard";
            this.btnLeaderboard.Size = new Size(170, 42);
            this.btnLeaderboard.TabIndex = 17;
            this.btnLeaderboard.Text = "LEADERBOARD";
            this.btnLeaderboard.UseVisualStyleBackColor = false;
            this.btnLeaderboard.Click += new EventHandler(this.btnLeaderboard_Click);

            this.btnReset.BackColor = Color.DeepSkyBlue;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = FlatStyle.Flat;
            this.btnReset.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnReset.ForeColor = Color.FromArgb(15, 18, 25);
            this.btnReset.Location = new Point(335, 485);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new Size(140, 42);
            this.btnReset.TabIndex = 18;
            this.btnReset.Text = "RESET GAME";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new EventHandler(this.btnReset_Click);

            this.btnBack.BackColor = Color.FromArgb(25, 35, 50);
            this.btnBack.FlatAppearance.BorderColor = Color.Crimson;
            this.btnBack.FlatAppearance.BorderSize = 1;
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnBack.ForeColor = Color.Crimson;
            this.btnBack.Location = new Point(485, 485);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(140, 42);
            this.btnBack.TabIndex = 19;
            this.btnBack.Text = "BACK";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(15, 18, 25);
            this.ClientSize = new Size(980, 650);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnLeaderboard);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.btn9);
            this.Controls.Add(this.btn8);
            this.Controls.Add(this.btn7);
            this.Controls.Add(this.btn6);
            this.Controls.Add(this.btn5);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.lblDraws);
            this.Controls.Add(this.lblOWins);
            this.Controls.Add(this.lblXWins);
            this.Controls.Add(this.lblScoreboardHeader);
            this.Controls.Add(this.lblToss);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Advanced Tic Tac Toe";
            this.Load += new EventHandler(this.GameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblTitle;
        private Label lblStatus;
        private Label lblToss;
        private Label lblScoreboardHeader;
        private Label lblXWins;
        private Label lblOWins;
        private Label lblDraws;
        private Button btn1;
        private Button btn2;
        private Button btn3;
        private Button btn4;
        private Button btn5;
        private Button btn6;
        private Button btn7;
        private Button btn8;
        private Button btn9;
        private Button btnReset;
        private Button btnBack;
        private Button btnHistory;
        private Button btnLeaderboard;
    }
}
