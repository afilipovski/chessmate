using ChessMate.AlphaBeta;
using ChessMate.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace ChessMate
{
    public partial class Form1 : Form
    {
        public GameState GameState { get; set; } = new GameState();
        public bool InGame { get; set; } = false;

        readonly AIMoveOverlay aimo = new AIMoveOverlay();
        public string SavedGamePath { get; set; } = null;
        public bool Dirty { get; set; } = false;


        public Form1()
        {
            InitializeComponent();
			this.DoubleBuffered = true;
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
            GameState.Board.DrawTiles(e.Graphics);
            foreach (Board sb in GameState.successiveBoards)
            {
                sb.NewPos.Draw(e.Graphics);
            }
            if (!GameState.Board.WhiteTurn)
            {
                Console.WriteLine("draw overlay");
                aimo.Draw(e.Graphics);
            }
        }

        private void Form1_Resize_1(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //Height -= Height % 8;
            Invalidate();
        }

        //Opponent o = new Opponent(OpponentDifficulty.EASY);

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Board newBoard = GameState.Board.Click(new Position(e.X, e.Y), GameState.successiveBoards);

            if (!ReferenceEquals(GameState.Board, newBoard)) { 
                Dirty = true;
                UpdateTitle();
            }

            GameState.Board = newBoard;

            Invalidate();
            this.Refresh();

            //AI MOVE
            if (GameState.Board.WhiteTurn == false)
            {
                //Console.WriteLine($"Player Move:\n{Board}");
                Board aiMove = GameState.o.Move(GameState.Board);

                //Console.WriteLine(aiMove);
                if (aiMove != null)
                {
                    GameState.Board = aiMove;
                    //Console.WriteLine($"AI Move:\n{Board}");
                    if (GameState.Board.NoPossibleMoves())
                    {
                        if (GameState.Board.KingIsInCheck(true))
                            FormUtils.ShowDefeatDialog(() => this.Close());
                        else
                            FormUtils.ShowPlayerStalemateDialog(() => this.Close());
                    }
                }
                else //ai didn't generate move
                {
                    if (GameState.Board.KingIsInCheck(false))
                        FormUtils.ShowVictoryDialog(() => this.Close());
                    else
                        FormUtils.ShowAIStalemateDialog(() => this.Close());
                }
            }

            Invalidate();
        }

		private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
		{

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
			FileStream stream = File.OpenWrite(SavedGamePath);
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(stream, GameState);
			stream.Dispose();
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
                    BinaryFormatter bf = new BinaryFormatter();
                    GameState = bf.Deserialize(ofd.OpenFile()) as GameState;
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
