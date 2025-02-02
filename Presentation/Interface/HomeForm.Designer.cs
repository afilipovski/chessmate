namespace ChessMate.Presentation.Interface
{
    partial class HomeForm
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
            this.title = new System.Windows.Forms.Label();
            this.aiOpponentBtn = new System.Windows.Forms.Button();
            this.multiplayerBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(0, 64);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(453, 37);
            this.title.TabIndex = 0;
            this.title.Text = "ChessMate";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // aiOpponentBtn
            // 
            this.aiOpponentBtn.BackColor = System.Drawing.Color.White;
            this.aiOpponentBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiOpponentBtn.Location = new System.Drawing.Point(167, 160);
            this.aiOpponentBtn.Name = "aiOpponentBtn";
            this.aiOpponentBtn.Size = new System.Drawing.Size(119, 42);
            this.aiOpponentBtn.TabIndex = 2;
            this.aiOpponentBtn.Text = "AI Opponent";
            this.aiOpponentBtn.UseVisualStyleBackColor = false;
            this.aiOpponentBtn.Click += new System.EventHandler(this.aiOpponentBtn_Click);
            // 
            // multiplayerBtn
            // 
            this.multiplayerBtn.BackColor = System.Drawing.Color.White;
            this.multiplayerBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiplayerBtn.Location = new System.Drawing.Point(167, 237);
            this.multiplayerBtn.Name = "multiplayerBtn";
            this.multiplayerBtn.Size = new System.Drawing.Size(119, 42);
            this.multiplayerBtn.TabIndex = 3;
            this.multiplayerBtn.Text = "Multiplayer";
            this.multiplayerBtn.UseVisualStyleBackColor = false;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(453, 358);
            this.Controls.Add(this.multiplayerBtn);
            this.Controls.Add(this.aiOpponentBtn);
            this.Controls.Add(this.title);
            this.Name = "HomeForm";
            this.Text = "ChessMate - Home";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button aiOpponentBtn;
        private System.Windows.Forms.Button multiplayerBtn;
    }
}