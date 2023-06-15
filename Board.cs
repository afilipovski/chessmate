using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate
{
    public class Board
    {
        public Dictionary<Position, Piece> PieceByPosition { get; set; }
        //public Position[,] positions { get; set; } = new Position[8,8];
        public bool WhiteTurn { get; set; }
        public static int WIDTH { get; set; }
        public static int HEIGHT { get; set; }

        public Board()
        {
            PieceByPosition = new Dictionary<Position, Piece>();
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; j++)
                {
                    //positions[i,j] = new Position(i, j);
                    PieceByPosition.Add(new Position(i, j), null);
                }
            }
            for (int i = 0; i < 8; ++i)
            {
                Position pos = new Position(i, 1);
                PieceByPosition[pos] = new Pawn(pos, false);
                pos = new Position(i, 6);
                PieceByPosition[pos] = new Pawn(pos, true);
            }
            for (int i = 0; i < 8; i += 7)
            {
                Position pos = new Position(i, 0);
                PieceByPosition[pos] = new Pawn(pos, false);

            }
        }

        public List<Board> Successor()
        {
            List<Board> res = new List<Board>();

            PieceByPosition.Values.ToList()
                .Where(piece => piece.White == WhiteTurn).ToList()
                .ForEach(piece =>
                {
                    res.Concat(piece.PossibleMoves(this));
                });

            return res;
        }
        public bool IsOccupied(Position position)
        {
            return PieceByPosition.ContainsKey(position);
        }

        public void DrawTiles(Graphics g, int height, int width)
        {
            HEIGHT = height;
            WIDTH = width;
            Position[] pos = PieceByPosition.Keys.ToArray();
            for (int i = 0; i < 64; ++i)
            {
                //for (int j = 0; j < 8; j++)
                //{
                //   positions[i,j].Draw(g, Height / 8, Width / 8);
                //}
                pos[i].Draw(g);
            }
            foreach(Piece piece in PieceByPosition.Values) 
            {
                if (piece == null) continue;
                piece.Draw(g);
            }
        }

    }
}
