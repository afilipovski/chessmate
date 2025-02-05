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
        }

        private void aiOpponentBtn_Click(object sender, EventArgs e)
        {
            Form1 aiForm = new Form1();
            Hide();
            aiForm.ShowDialog();
            Show();
        }

        private void multiplayerBtn_Click(object sender, EventArgs e)
        {
            Form3 configureMultiplayerGameForm = new Form3();
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
