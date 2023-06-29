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

        bool ALPHA_BETA_DEBUG = false;

        AIMoveOverlay aimo = new AIMoveOverlay();
        public string SavedGamePath { get; set; } = null;


        public Form1()
        {
            InitializeComponent();
			this.DoubleBuffered = true;
			SetForm();
            InGame = true;
        }

        public void SetForm()
        {
			ChooseDifficultyForm form = new ChooseDifficultyForm();
			DialogResult dr = form.ShowDialog();
			if (dr != DialogResult.OK && !InGame)
				System.Environment.Exit(0);

			GameState.o = new Opponent(form.ChosenDifficulty);

			if (ALPHA_BETA_DEBUG)
			{
				GameState.Board = Board.TwoRookBoard();
			}
			else
			{
				GameState.Board = new Board();
			}
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
            GameState.Board = GameState.Board.Click(new Position(e.X, e.Y), GameState.successiveBoards);
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
            SetForm();
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
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
            if (saveAs())
                saveToolStripMenuItem_Click(sender, e);
		}

        private bool saveAs()
        {
			SaveFileDialog sfd = new SaveFileDialog();
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				SavedGamePath = sfd.FileName;
                this.Text = $"ChessMate: {SavedGamePath}";
                return true;
			}
			return false;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                BinaryFormatter bf = new BinaryFormatter();
                GameState = bf.Deserialize(ofd.OpenFile()) as GameState;
                Invalidate();
            }

		}
	}
}
