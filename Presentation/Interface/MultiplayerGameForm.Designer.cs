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
            this.gameInfoGrpBx = new System.Windows.Forms.GroupBox();
            this.codeTxtBx = new System.Windows.Forms.TextBox();
            this.opponentTxtBx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gameInfoGrpBx.SuspendLayout();
            this.SuspendLayout();
            // 
            // gameInfoGrpBx
            // 
            this.gameInfoGrpBx.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gameInfoGrpBx.Controls.Add(this.label2);
            this.gameInfoGrpBx.Controls.Add(this.label1);
            this.gameInfoGrpBx.Controls.Add(this.opponentTxtBx);
            this.gameInfoGrpBx.Controls.Add(this.codeTxtBx);
            this.gameInfoGrpBx.Location = new System.Drawing.Point(109, 155);
            this.gameInfoGrpBx.Name = "gameInfoGrpBx";
            this.gameInfoGrpBx.Size = new System.Drawing.Size(348, 72);
            this.gameInfoGrpBx.TabIndex = 1;
            this.gameInfoGrpBx.TabStop = false;
            this.gameInfoGrpBx.Text = "Game Information";
            // 
            // codeTxtBx
            // 
            this.codeTxtBx.Enabled = false;
            this.codeTxtBx.Location = new System.Drawing.Point(10, 40);
            this.codeTxtBx.Name = "codeTxtBx";
            this.codeTxtBx.Size = new System.Drawing.Size(100, 20);
            this.codeTxtBx.TabIndex = 0;
            // 
            // opponentTxtBx
            // 
            this.opponentTxtBx.Enabled = false;
            this.opponentTxtBx.Location = new System.Drawing.Point(142, 40);
            this.opponentTxtBx.Name = "opponentTxtBx";
            this.opponentTxtBx.Size = new System.Drawing.Size(100, 20);
            this.opponentTxtBx.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Join Code";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(140, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Opponent Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MultiplayerGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(616, 597);
            this.Controls.Add(this.gameInfoGrpBx);
            this.Name = "MultiplayerGameForm";
            this.Text = "ChessMate - Multiplayer Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.Form2_ResizeEnd);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form2_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseClick);
            this.Resize += new System.EventHandler(this.Form2_Resize_1);
            this.gameInfoGrpBx.ResumeLayout(false);
            this.gameInfoGrpBx.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gameInfoGrpBx;
        private System.Windows.Forms.TextBox codeTxtBx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox opponentTxtBx;
    }
}

