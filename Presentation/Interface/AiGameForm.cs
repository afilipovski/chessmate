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

namespace ChessMate.Presentation.Interface
{
    public partial class AiGameForm : Form
    {
        private readonly IAiGameController _gameController;

        public AiGameForm()
        {
            InitializeComponent();
			DoubleBuffered = true;
            Icon = new Icon($"{Application.StartupPath}\\Presentation\\Images\\form_icon.ico");
            _gameController = new AiGameController(this);
            _gameController.GenerateGame();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            _gameController.PaintForm(e);
        }

        private void Form1_Resize_1(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            _gameController.SubmitPlayerClick(e.X, e.Y);
        }

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
            _gameController.NewGame();
		}

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _gameController.SaveGame();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
            _gameController.SaveGameAs();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
            _gameController.OpenGame();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
            Close();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _gameController.ExitGame(e);
		}

		private void easyToolStripMenuItem_Click(object sender, EventArgs e)
		{
            _gameController.SetDifficulty(OpponentDifficulty.Easy);
		}

		private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
		{
            _gameController.SetDifficulty(OpponentDifficulty.Medium);
		}

		private void hardToolStripMenuItem_Click(object sender, EventArgs e)
		{
            _gameController.SetDifficulty(OpponentDifficulty.Hard);
		}

        public void Checkmarks()
        {
            easyToolStripMenuItem.Checked = mediumToolStripMenuItem.Checked = hardToolStripMenuItem.Checked = false;
            switch (_gameController.GetDifficulty())
            {
                case OpponentDifficulty.Easy: easyToolStripMenuItem.Checked = true; break;
                case OpponentDifficulty.Medium: mediumToolStripMenuItem.Checked = true; break;
                case OpponentDifficulty.Hard: hardToolStripMenuItem.Checked = true; break;
            }
        }
	}
}
