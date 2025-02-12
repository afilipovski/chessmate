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
using ChessMate.Presentation.Controllers.Implementation;
using ChessMate.Presentation.Controllers.Interface;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ChessMate.Presentation.Interface
{
    public partial class MultiplayerGameForm : Form
    {
        private readonly IMultiplayerGameController _gameController;

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="whitePov">The piece color of the player.</param>
        /// <param name="multiplayerGame">The game the player is playing.</param>
        public MultiplayerGameForm(bool whitePov, MultiplayerGame multiplayerGame)
        {
            InitializeComponent();
			DoubleBuffered = true;
            Icon = new Icon($"{Application.StartupPath}\\Presentation\\Images\\form_icon.ico");
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
            _gameController.QuitGame(e);
		}

        /// <summary>
        /// Resizes and repositions the game data group box.
        /// </summary>
        /// <param name="xPadding">A horizontal padding.</param>
        /// <param name="yPadding">A vertical padding.</param>
        public void ResizeGroupBox(int xPadding, int yPadding)
        {
            gameInfoGrpBx.Width = Width - 2 * xPadding - 17;
            gameInfoGrpBx.Left = xPadding;
            gameInfoGrpBx.Top = (yPadding - gameInfoGrpBx.Height) / 2;
        }

        /// <summary>
        /// Sets the text in the opponent textbox.
        /// </summary>
        /// <param name="username">A user's username.</param>
        public void SetOpponentName(string username)
        {
            opponentTxtBx.Text = username;
        }

        /// <summary>
        /// Sets the text in the code textbox.
        /// </summary>
        /// <param name="joinCode">A multiplayer game's join code.</param>
        public void SetJoinCode(string joinCode)
        {
            codeTxtBx.Text = joinCode;
        }
    }
}
