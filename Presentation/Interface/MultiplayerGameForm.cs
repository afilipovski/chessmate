using ChessMate.Domain;
using ChessMate.Domain.Positions;
using ChessMate.Presentation.AlphaBeta;
using ChessMate.Presentation.GraphicsRendering;
using ChessMate.Presentation.Interface;
using ChessMate.Service.Implementation;
using ChessMate.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using ChessMate.Presentation.Controllers;

namespace ChessMate.Presentation.Interface
{
    public partial class MultiplayerGameForm : Form
    {
        private readonly MultiplayerGameController _gameController;

        public MultiplayerGameForm(bool whitePov, MultiplayerGame multiplayerGame)
        {
            InitializeComponent();
			DoubleBuffered = true;
            _gameController = new MultiplayerGameController(this, whitePov, multiplayerGame);
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            _gameController.PaintForm(e);
        }

        private void Form2_Resize_1(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form2_ResizeEnd(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            _gameController.SubmitPlayerClick(e.X, e.Y);
        }

		private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
		}
	}
}
