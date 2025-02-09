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
using ChessMate.Service.Interface;

namespace ChessMate.Presentation.Interface
{
    public partial class Form3 : Form
    {
        private readonly IMultiplayerService _multiplayerService;

        public Form3()
        {
            InitializeComponent();
            _multiplayerService = MultiplayerService.Instance;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = usernameTxtBx.Text;
            if (username == "")
            {
                MessageBox.Show("Please enter a username.", "Error");
                return;
            }

            string joinCode = codeTxtBx.Text;
            if (joinCode != "" && joinCode.Length != 5)
            {
                MessageBox.Show("Please enter a valid join code");
                return;
            }

            MultiplayerGame response;
            if (string.IsNullOrEmpty(joinCode))
            {
                response = await _multiplayerService.CreateGame(username);
                joinCode = response.JoinCode;
            }
            else
            {
                response = await _multiplayerService.JoinGame(username, joinCode);
            }

            Hide();
            bool whitePov = string.IsNullOrEmpty(codeTxtBx.Text);
            Form2 multiplayerGame = new Form2(whitePov, response);
            multiplayerGame.ShowDialog();
            Show();

            await _multiplayerService.LeaveGame(username, joinCode);
        }
    }
}
