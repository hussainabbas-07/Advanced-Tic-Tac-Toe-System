namespace AdvancedTicTacToeWinForms
{
    partial class MainMenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnPvP = new System.Windows.Forms.Button();
            this.btnPvAI = new System.Windows.Forms.Button();
            this.btnTM = new System.Windows.Forms.Button();
            this.btnMH = new System.Windows.Forms.Button();
            this.btnLB = new System.Windows.Forms.Button();
            this.btnEX = new System.Windows.Forms.Button();
            this.lblFooter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(20, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(760, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ADVANCED TIC TAC TOE";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // btnPvP
            // 
            this.btnPvP.BackColor = System.Drawing.Color.LightBlue;
            this.btnPvP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPvP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPvP.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPvP.Location = new System.Drawing.Point(275, 160);
            this.btnPvP.Name = "btnPvP";
            this.btnPvP.Size = new System.Drawing.Size(250, 45);
            this.btnPvP.TabIndex = 1;
            this.btnPvP.Text = "Player vs Player";
            this.btnPvP.UseVisualStyleBackColor = false;
            this.btnPvP.Click += new System.EventHandler(this.btnPvP_Click);
            // 
            // btnPvAI
            // 
            this.btnPvAI.BackColor = System.Drawing.Color.LightGreen;
            this.btnPvAI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPvAI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPvAI.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPvAI.Location = new System.Drawing.Point(275, 220);
            this.btnPvAI.Name = "btnPvAI";
            this.btnPvAI.Size = new System.Drawing.Size(250, 45);
            this.btnPvAI.TabIndex = 2;
            this.btnPvAI.Text = "Player vs AI";
            this.btnPvAI.UseVisualStyleBackColor = false;
            this.btnPvAI.Click += new System.EventHandler(this.btnPvAI_Click);
            // 
            // btnTM
            // 
            this.btnTM.BackColor = System.Drawing.Color.Khaki;
            this.btnTM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTM.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTM.Location = new System.Drawing.Point(275, 280);
            this.btnTM.Name = "btnTM";
            this.btnTM.Size = new System.Drawing.Size(250, 45);
            this.btnTM.TabIndex = 3;
            this.btnTM.Text = "Tournament Mode";
            this.btnTM.UseVisualStyleBackColor = false;
            this.btnTM.Click += new System.EventHandler(this.btnTM_Click);
            // 
            // btnMH
            // 
            this.btnMH.BackColor = System.Drawing.Color.PeachPuff;
            this.btnMH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMH.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMH.Location = new System.Drawing.Point(275, 340);
            this.btnMH.Name = "btnMH";
            this.btnMH.Size = new System.Drawing.Size(250, 45);
            this.btnMH.TabIndex = 4;
            this.btnMH.Text = "Match History";
            this.btnMH.UseVisualStyleBackColor = false;
            this.btnMH.Click += new System.EventHandler(this.btnMH_Click);
            // 
            // btnLB
            // 
            this.btnLB.BackColor = System.Drawing.Color.Plum;
            this.btnLB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLB.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLB.Location = new System.Drawing.Point(275, 400);
            this.btnLB.Name = "btnLB";
            this.btnLB.Size = new System.Drawing.Size(250, 45);
            this.btnLB.TabIndex = 5;
            this.btnLB.Text = "Leaderboard";
            this.btnLB.UseVisualStyleBackColor = false;
            this.btnLB.Click += new System.EventHandler(this.btnLB_Click);
            // 
            // btnEX
            // 
            this.btnEX.BackColor = System.Drawing.Color.LightCoral;
            this.btnEX.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEX.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEX.Location = new System.Drawing.Point(275, 460);
            this.btnEX.Name = "btnEX";
            this.btnEX.Size = new System.Drawing.Size(250, 45);
            this.btnEX.TabIndex = 6;
            this.btnEX.Text = "Exit";
            this.btnEX.UseVisualStyleBackColor = false;
            this.btnEX.Click += new System.EventHandler(this.btnEX_Click);
            // 
            // lblFooter
            // 
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFooter.ForeColor = System.Drawing.Color.DimGray;
            this.lblFooter.Location = new System.Drawing.Point(20, 530);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(760, 25);
            this.lblFooter.TabIndex = 7;
            this.lblFooter.Text = "Developed By Hussain and Anumta";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFooter.Click += new System.EventHandler(this.label1_Click);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.lblFooter);
            this.Controls.Add(this.btnEX);
            this.Controls.Add(this.btnLB);
            this.Controls.Add(this.btnMH);
            this.Controls.Add(this.btnTM);
            this.Controls.Add(this.btnPvAI);
            this.Controls.Add(this.btnPvP);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced Tic Tac Toe System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnPvP;
        private System.Windows.Forms.Button btnPvAI;
        private System.Windows.Forms.Button btnTM;
        private System.Windows.Forms.Button btnMH;
        private System.Windows.Forms.Button btnLB;
        private System.Windows.Forms.Button btnEX;
        private System.Windows.Forms.Label lblFooter;
    }
}

