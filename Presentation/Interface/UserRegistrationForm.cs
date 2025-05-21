using ChessMate.Presentation.Controllers.Implementation;
using ChessMate.Service.Implementation;
using ChessMate.Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessMate.Presentation.Interface
{
    public partial class UserRegistrationForm : Form
    {
        MultiplayerService _service;
        public UserRegistrationForm()
        {
            InitializeComponent();
            _service = MultiplayerService.Instance;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            MultiplayerService.Username = logInUsernameField.Text;
            MultiplayerService.Password = logInPasswordField.Text;
            try
            {
                await _service.ValidateCredentials();
                var form = new JoinMpGameForm();
                Hide();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong credentials");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            MultiplayerService.Username = registerUsername.Text;
            MultiplayerService.Password = registerPassword.Text;
            try
            {
                await _service.Register(registerUsername.Text, registerPassword.Text, registerConfirm.Text);
                var form = new JoinMpGameForm();
                Hide();
                form.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
