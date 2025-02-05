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

namespace ChessMate.Presentation.Interface
{
    public partial class Form3 : Form
    {
        public MultiplayerService multiplayerService;
        public Form3()
        {
            InitializeComponent();
            multiplayerService = new MultiplayerService();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            if (username == "")
            {
                MessageBox.Show("Please enter a username.", "Error");
                return;
            }

            string joinCode = textBox2.Text;
            if (joinCode != "" && joinCode.Length != 5)
            {
                MessageBox.Show("Please enter a valid join code");
                return;
            }

            if (string.IsNullOrEmpty(joinCode))
            {
                var response = await multiplayerService.CreateGame(username);
                joinCode = response.JoinCode;
            }

            Hide();
            bool whitePov = string.IsNullOrEmpty(textBox2.Text);
            Form2 multiplayerGame = new Form2(whitePov);
            multiplayerGame.ShowDialog();
            Show();

            await multiplayerService.LeaveGame(username, joinCode);
        }
    }
}
