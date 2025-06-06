﻿namespace ChessMate.Presentation.Interface
{
    partial class UserRegistrationForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.logInPasswordField = new System.Windows.Forms.TextBox();
            this.logInUsernameField = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.registerConfirm = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.registerPassword = new System.Windows.Forms.TextBox();
            this.registerUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.logInButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logInPasswordField);
            this.groupBox1.Controls.Add(this.logInUsernameField);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 180);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log in";
            // 
            // logInPasswordField
            // 
            this.logInPasswordField.Location = new System.Drawing.Point(94, 94);
            this.logInPasswordField.Name = "logInPasswordField";
            this.logInPasswordField.PasswordChar = '*';
            this.logInPasswordField.Size = new System.Drawing.Size(205, 26);
            this.logInPasswordField.TabIndex = 6;
            // 
            // logInUsernameField
            // 
            this.logInUsernameField.Location = new System.Drawing.Point(94, 49);
            this.logInUsernameField.Name = "logInUsernameField";
            this.logInUsernameField.Size = new System.Drawing.Size(205, 26);
            this.logInUsernameField.TabIndex = 5;
            this.logInUsernameField.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.registerConfirm);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.registerPassword);
            this.groupBox2.Controls.Add(this.registerUsername);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(324, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 180);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Register";
            // 
            // registerConfirm
            // 
            this.registerConfirm.Location = new System.Drawing.Point(94, 132);
            this.registerConfirm.Name = "registerConfirm";
            this.registerConfirm.PasswordChar = '*';
            this.registerConfirm.Size = new System.Drawing.Size(182, 26);
            this.registerConfirm.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Confirm";
            // 
            // registerPassword
            // 
            this.registerPassword.Location = new System.Drawing.Point(94, 91);
            this.registerPassword.Name = "registerPassword";
            this.registerPassword.PasswordChar = '*';
            this.registerPassword.Size = new System.Drawing.Size(182, 26);
            this.registerPassword.TabIndex = 10;
            // 
            // registerUsername
            // 
            this.registerUsername.Location = new System.Drawing.Point(94, 48);
            this.registerUsername.Name = "registerUsername";
            this.registerUsername.Size = new System.Drawing.Size(182, 26);
            this.registerUsername.TabIndex = 9;
            this.registerUsername.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Password";
            // 
            // logInButton
            // 
            this.logInButton.Location = new System.Drawing.Point(129, 240);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(93, 48);
            this.logInButton.TabIndex = 2;
            this.logInButton.Text = "Log in";
            this.logInButton.UseVisualStyleBackColor = true;
            this.logInButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(82, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(483, 33);
            this.label6.TabIndex = 3;
            this.label6.Text = "Sign in / create a user to continue";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(418, 240);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 48);
            this.button2.TabIndex = 4;
            this.button2.Text = "Register";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // UserRegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 300);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.logInButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "UserRegistrationForm";
            this.Text = "UserRegistrationForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox logInPasswordField;
        private System.Windows.Forms.TextBox logInUsernameField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox registerConfirm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox registerPassword;
        private System.Windows.Forms.TextBox registerUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button logInButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
    }
}