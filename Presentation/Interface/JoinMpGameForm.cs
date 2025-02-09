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
            _controller = new JoinMultiplayerController(this);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await _controller.ValidateForm(usernameTxtBx.Text, codeTxtBx.Text);
        }

        public bool IsJoinCodeNull() => string.IsNullOrEmpty(codeTxtBx.Text);
    }
}
