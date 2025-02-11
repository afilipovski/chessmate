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
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
            Icon = new Icon($"{Application.StartupPath}\\Presentation\\Images\\form_icon.ico");
        }

        private void aiOpponentBtn_Click(object sender, EventArgs e)
        {
            AiGameForm aiForm = new AiGameForm();
            Hide();
            aiForm.ShowDialog();
            Show();
        }

        private void multiplayerBtn_Click(object sender, EventArgs e)
        {
            JoinMpGameForm configureMultiplayerGameForm = new JoinMpGameForm();
            Hide();
            configureMultiplayerGameForm.ShowDialog();
            Show();
        }

        private void aboutBtn_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }
    }
}
