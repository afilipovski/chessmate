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

namespace ChessMate.Presentation
{
    public partial class Form1 : Form
    {
        public GameState GameState { get; set; } = new GameState();
        private string SavedGamePath { get; set; } = null;
        public bool Dirty { get; set; } = false;
        private readonly IBoardService _boardService = new BoardService();
        private readonly Drawer _drawer = new Drawer();
        private readonly IGameStateService _gameStateService = new GameStateService();

        public Form1()
        {
            InitializeComponent();
			DoubleBuffered = true;
			GenerateGame();
            Dirty = false;
            UpdateTitle();
        }

        public void GenerateGame()
        {
			GameState.o = new Opponent(OpponentDifficulty.EASY);

			GameState.Board = new Board();

            SavedGamePath = null;

            Dirty = false;
            UpdateTitle();
            Checkmarks();

            Invalidate();
		}

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Board.TILE_SIDE = (ClientSize.Height - Board.OFFSET_Y) / 8;
            Board.OFFSET_X = (ClientSize.Width - 8 * Board.TILE_SIDE) / 2;
            _drawer.DrawChessBoardForm(GameState, e.Graphics);
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
            Board newBoard = _boardService.GetSuccessorStateForClickedPosition(new Position(e.X, e.Y), GameState.Board, GameState.successiveBoards);

            if (!ReferenceEquals(GameState.Board, newBoard)) { 
                Dirty = true;
                UpdateTitle();
            }

            GameState.Board = newBoard;

            Invalidate();
            GameState.SetCheckPosition();
            Refresh();

            //AI MOVE
            if (GameState.Board.WhiteTurn == false)
            {
                Board aiMove = GameState.o.Move(GameState.Board);

                if (aiMove != null)
                {
                    GameState.Board = aiMove;
                    if (_boardService.PossibleMovesNotExisting(GameState.Board))
                    {
                        if (_boardService.IsKingInCheck(GameState.Board, true))
                            FormUtils.ShowDefeatDialog(() => newToolStripMenuItem_Click(null, EventArgs.Empty));
                        else
                            FormUtils.ShowPlayerStalemateDialog(() => newToolStripMenuItem_Click(null, EventArgs.Empty));
                    }
                }
                else //ai didn't generate move
                {
                    if (_boardService.IsKingInCheck(GameState.Board, false))
                        FormUtils.ShowVictoryDialog(() => newToolStripMenuItem_Click(null, EventArgs.Empty));
                    else
                        FormUtils.ShowAIStalemateDialog(() => newToolStripMenuItem_Click(null, EventArgs.Empty));
                }
            }

            GameState.SetCheckPosition();

            Refresh();
        }

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
            if (UnsavedChangesAbort())
                return;
            GenerateGame();
		}

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SavedGamePath is null) { 
                saveAsToolStripMenuItem_Click(sender, e);
                return;
            }
			_gameStateService.SaveToFile(GameState, SavedGamePath);
            Dirty = false;
            UpdateTitle();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
            if (SaveAs())
                saveToolStripMenuItem_Click(sender, e);
		}

        private bool SaveAs()
        {
			SaveFileDialog sfd = new SaveFileDialog
			{
				AddExtension = true,
				DefaultExt = "sav",
				Filter = "Saved Games (*.sav)|*.sav"
			};
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				SavedGamePath = sfd.FileName;
                this.Text = $"ChessMate: {Path.GetFileNameWithoutExtension(SavedGamePath)}";
                return true;
			}
			return false;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
            if (UnsavedChangesAbort())
                return;
			OpenFileDialog ofd = new OpenFileDialog
			{
				DefaultExt = "sav",
				Filter = "Saved Games (*.sav)|*.sav"
			};
			if (ofd.ShowDialog() == DialogResult.OK)
            {
				try
				{
                    GameState = _gameStateService.LoadFromFile(ofd.OpenFile());
                    SavedGamePath = ofd.FileName;
                    Dirty = false;
                    UpdateTitle();
                    Invalidate();
                }
                catch (Exception)
				{
                    MessageBox.Show("The file is either corrupted or not a ChessMate savegame.", "Loading failed");
                }
            }

		}

        private void UpdateTitle()
        {
            Text = $"ChessMate";
            if (SavedGamePath != null)
                Text += $" - {Path.GetFileNameWithoutExtension(SavedGamePath)}";
            if (Dirty)
                Text += "*";
        }

        private bool UnsavedChangesAbort()
        {
            if (!Dirty)
                return false;
			if (MessageBox.Show("Do you want to save your game?", "Unsaved progress", MessageBoxButtons.YesNo)
				== DialogResult.No)
				return false;
			saveToolStripMenuItem_Click(null, EventArgs.Empty);
			if (!Dirty)
				return false;
            return true;
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
            this.Close();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
            if (UnsavedChangesAbort())
                e.Cancel = true;
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
            AboutForm af = new AboutForm();
            af.ShowDialog();
		}

		private void easyToolStripMenuItem_Click(object sender, EventArgs e)
		{
            GameState.o.Difficulty = OpponentDifficulty.EASY;
            Checkmarks();
		}

		private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
		{
            GameState.o.Difficulty = OpponentDifficulty.MEDIUM;
            Checkmarks();
		}

		private void hardToolStripMenuItem_Click(object sender, EventArgs e)
		{
            GameState.o.Difficulty = OpponentDifficulty.HARD;
            Checkmarks();
		}

        private void Checkmarks()
        {
            easyToolStripMenuItem.Checked = mediumToolStripMenuItem.Checked = hardToolStripMenuItem.Checked = false;
            switch (GameState.o.Difficulty)
            {
                case OpponentDifficulty.EASY: easyToolStripMenuItem.Checked = true; break;
                case OpponentDifficulty.MEDIUM: mediumToolStripMenuItem.Checked = true; break;
                case OpponentDifficulty.HARD: hardToolStripMenuItem.Checked = true; break;
            }
        }
	}
}
