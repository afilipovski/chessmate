namespace ChessMate.Presentation.Interface
{
    partial class Form3
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
            this.components = new System.ComponentModel.Container();
            this.titleLbl = new System.Windows.Forms.Label();
            this.usernameLbl = new System.Windows.Forms.Label();
            this.usernameTxtBx = new System.Windows.Forms.TextBox();
            this.codeTxtBx = new System.Windows.Forms.TextBox();
            this.codeLbl = new System.Windows.Forms.Label();
            this.joinBtn = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // titleLbl
            // 
            this.titleLbl.AutoSize = true;
            this.titleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.titleLbl.Location = new System.Drawing.Point(22, 22);
            this.titleLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(327, 25);
            this.titleLbl.TabIndex = 0;
            this.titleLbl.Text = "Welcome to ChessMate Multiplayer!";
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.usernameLbl.Location = new System.Drawing.Point(19, 67);
            this.usernameLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(77, 17);
            this.usernameLbl.TabIndex = 1;
            this.usernameLbl.Text = "Username:";
            // 
            // usernameTxtBx
            // 
            this.usernameTxtBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.usernameTxtBx.Location = new System.Drawing.Point(162, 64);
            this.usernameTxtBx.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.usernameTxtBx.Name = "usernameTxtBx";
            this.usernameTxtBx.Size = new System.Drawing.Size(195, 23);
            this.usernameTxtBx.TabIndex = 2;
            // 
            // codeTxtBx
            // 
            this.codeTxtBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.codeTxtBx.Location = new System.Drawing.Point(162, 101);
            this.codeTxtBx.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.codeTxtBx.Name = "codeTxtBx";
            this.codeTxtBx.Size = new System.Drawing.Size(136, 23);
            this.codeTxtBx.TabIndex = 4;
            // 
            // codeLbl
            // 
            this.codeLbl.AutoSize = true;
            this.codeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.codeLbl.Location = new System.Drawing.Point(19, 102);
            this.codeLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.codeLbl.Name = "codeLbl";
            this.codeLbl.Size = new System.Drawing.Size(137, 17);
            this.codeLbl.TabIndex = 5;
            this.codeLbl.Text = "Join code (optional):";
            // 
            // joinBtn
            // 
            this.joinBtn.Location = new System.Drawing.Point(301, 101);
            this.joinBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.joinBtn.Name = "joinBtn";
            this.joinBtn.Size = new System.Drawing.Size(55, 22);
            this.joinBtn.TabIndex = 6;
            this.joinBtn.Text = "Join";
            this.joinBtn.UseVisualStyleBackColor = true;
            this.joinBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 142);
            this.Controls.Add(this.joinBtn);
            this.Controls.Add(this.codeLbl);
            this.Controls.Add(this.codeTxtBx);
            this.Controls.Add(this.usernameTxtBx);
            this.Controls.Add(this.usernameLbl);
            this.Controls.Add(this.titleLbl);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form3";
            this.Text = "ChessMate - Join Multiplayer Game";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.Label usernameLbl;
        private System.Windows.Forms.TextBox usernameTxtBx;
        private System.Windows.Forms.TextBox codeTxtBx;
        private System.Windows.Forms.Label codeLbl;
        private System.Windows.Forms.Button joinBtn;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}