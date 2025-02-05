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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeForm));
            this.aiOpponentBtn = new System.Windows.Forms.Button();
            this.multiplayerBtn = new System.Windows.Forms.Button();
            this.aboutBtn = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // aiOpponentBtn
            // 
            this.aiOpponentBtn.BackColor = System.Drawing.Color.White;
            this.aiOpponentBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aiOpponentBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiOpponentBtn.Location = new System.Drawing.Point(285, 371);
            this.aiOpponentBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.aiOpponentBtn.Name = "aiOpponentBtn";
            this.aiOpponentBtn.Size = new System.Drawing.Size(178, 65);
            this.aiOpponentBtn.TabIndex = 2;
            this.aiOpponentBtn.Text = "AI Opponent";
            this.aiOpponentBtn.UseVisualStyleBackColor = false;
            this.aiOpponentBtn.Click += new System.EventHandler(this.aiOpponentBtn_Click);
            // 
            // multiplayerBtn
            // 
            this.multiplayerBtn.BackColor = System.Drawing.Color.White;
            this.multiplayerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.multiplayerBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiplayerBtn.Location = new System.Drawing.Point(285, 491);
            this.multiplayerBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.multiplayerBtn.Name = "multiplayerBtn";
            this.multiplayerBtn.Size = new System.Drawing.Size(178, 65);
            this.multiplayerBtn.TabIndex = 3;
            this.multiplayerBtn.Text = "Multiplayer";
            this.multiplayerBtn.UseVisualStyleBackColor = false;
            this.multiplayerBtn.Click += new System.EventHandler(this.multiplayerBtn_Click);
            // 
            // aboutBtn
            // 
            this.aboutBtn.BackColor = System.Drawing.Color.White;
            this.aboutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aboutBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutBtn.Location = new System.Drawing.Point(285, 251);
            this.aboutBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.aboutBtn.Name = "aboutBtn";
            this.aboutBtn.Size = new System.Drawing.Size(178, 65);
            this.aboutBtn.TabIndex = 4;
            this.aboutBtn.Text = "About";
            this.aboutBtn.UseVisualStyleBackColor = false;
            this.aboutBtn.Click += new System.EventHandler(this.aboutBtn_Click);
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(0, 103);
            this.title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(748, 57);
            this.title.TabIndex = 0;
            this.title.Text = "ChessMate";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(750, 689);
            this.Controls.Add(this.aboutBtn);
            this.Controls.Add(this.multiplayerBtn);
            this.Controls.Add(this.aiOpponentBtn);
            this.Controls.Add(this.title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "HomeForm";
            this.Text = "ChessMate - Home";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button aiOpponentBtn;
        private System.Windows.Forms.Button multiplayerBtn;
        private System.Windows.Forms.Button aboutBtn;
        private System.Windows.Forms.Label title;
    }
}