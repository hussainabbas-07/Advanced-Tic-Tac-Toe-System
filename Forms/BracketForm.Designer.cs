namespace AdvancedTicTacToeWinForms
{
    partial class BracketForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BracketForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "BracketForm";
            this.Text = "BracketForm";
            this.Load += new System.EventHandler(this.BracketForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}