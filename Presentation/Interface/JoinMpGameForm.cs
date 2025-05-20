using ChessMate.Domain;
using ChessMate.Service.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessMate.Presentation.Controllers;
using ChessMate.Presentation.Controllers.Implementation;
using ChessMate.Presentation.Controllers.Interface;
using ChessMate.Service.Interface;

namespace ChessMate.Presentation.Interface
{
    public partial class JoinMpGameForm : Form
    {
        private readonly IJoinMultiplayerController _controller;

        public JoinMpGameForm()
        {
            InitializeComponent();
            label1.Text = MultiplayerService.Username;
            Icon = new Icon($"{Application.StartupPath}\\Presentation\\Images\\form_icon.ico");
            _controller = new JoinMultiplayerController(this);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await _controller.ValidateForm(codeTxtBx.Text);
        }

        /// <summary>
        /// Determines whether the user hasn't entered a join code.
        /// </summary>
        /// <returns>A boolean value on whether the user hasn't entered a join code.</returns>
        public bool IsJoinCodeNull() => string.IsNullOrEmpty(codeTxtBx.Text);

        private void button1_Click_1(object sender, EventArgs e)
        {
            var form = new UserRegistrationForm();
            Hide();
            form.ShowDialog();
        }
    }
}
