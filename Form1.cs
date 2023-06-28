using ChessMate.AlphaBeta;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ChessMate.Interface;

namespace ChessMate
{
    public partial class Form1 : Form
    {
        public Board Board { get; set; }
        public List<Board> successiveBoards { get; set; } = new List<Board>();
        
        bool ALPHA_BETA_DEBUG = false;

        AIMoveOverlay aimo = new AIMoveOverlay();

        public Form1()
        {
            InitializeComponent();
            if (ALPHA_BETA_DEBUG)
            {
                Board = Board.TwoRookBoard();
            }
            else
            {
                Board = new Board();
            }
            this.DoubleBuffered = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Board.HEIGHT = ClientSize.Height / 8;
            Board.WIDTH = Width / 8;
            Board.OFFSET = (Width - Height) / 2;
            Board.DrawTiles(e.Graphics);
            foreach (Board sb in successiveBoards)
            {
                sb.NewPos.Draw(e.Graphics);
            }
            if (!Board.WhiteTurn)
                aimo.Draw(e.Graphics);
        }

        private void Form1_Resize_1(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            Height -= Height % 8;
            Invalidate();
        }

        Opponent o = new Opponent(OpponentDifficulty.EASY);

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Board = Board.Click(new Position(e.X, e.Y), successiveBoards);
            Invalidate();

            //AI MOVE
            if (Board.WhiteTurn == false)
            {
				//Console.WriteLine($"Player Move:\n{Board}");
				Board aiMove = o.Move(Board);
                //Console.WriteLine(aiMove);
                if (aiMove != null)
                {
                    Board = aiMove;
                    //Console.WriteLine($"AI Move:\n{Board}");
                    if (Board.NoPossibleMoves())
                    {
                        if (Board.KingIsInCheck(true))
                            FormUtils.ShowDefeatDialog(() => this.Close());
                        else
                            FormUtils.ShowPlayerStalemateDialog(() => this.Close());
                    }
                }
                else //ai didn't generate move
                {
                    if (Board.KingIsInCheck(false))
                        FormUtils.ShowVictoryDialog(() => this.Close());
                    else
                        FormUtils.ShowAIStalemateDialog(() => this.Close());
                }
            }

            Invalidate();
        }
    }
}
