namespace ChessMate.Presentation.Interface
{
    partial class MultiplayerGameForm
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
            this.SuspendLayout();
            // 
            // MultiplayerGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(616, 597);
            this.Name = "MultiplayerGameForm";
            this.Text = "ChessMate - Multiplayer Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.Form2_ResizeEnd);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form2_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseClick);
            this.Resize += new System.EventHandler(this.Form2_Resize_1);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

