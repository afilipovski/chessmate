using ChessMate.AlphaBeta;
using ChessMate.Pieces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ChessMate
{
    public class Board
    {
        public Dictionary<Position, Piece> PieceByPosition { get; set; }
        public bool WhiteTurn { get; set; } = true;
        public static int WIDTH { get; set; }
        public static int HEIGHT { get; set; }
        public static int OFFSET { get; set; }
        public Piece CurrentClickedPiece { get; set; }
        public GreenPosition NewPos { get; set; }

        // copy constructor
        public Board(Board board)
        {
            PieceByPosition = new Dictionary<Position, Piece>();
            WhiteTurn = !board.WhiteTurn;
            foreach (Position key in board.PieceByPosition.Keys)
            {
                if (board.PieceByPosition[key] != null)
                    PieceByPosition[key] = board.PieceByPosition[key].Clone();
                else
                    PieceByPosition[key] = null;
            }
        }

        // copy 2
        public Board(Board b, Position posOld, Position posNew, Piece p) : this(b)
        {
            p = p.Clone();
            NewPos = new GreenPosition(posNew);
            PieceByPosition[posNew] = p;
            //PieceByPosition.Remove(posOld);
            PieceByPosition[posOld] = null;
            //MOZHDA
            PieceByPosition[posNew].Position = posNew;
        }

        public static bool IsInBoard(Position p)
        {
            return p.X >= 0 && p.Y >= 0 && p.X <= 7 && p.Y <= 7;
        }

        //Generates starting board setup.
        public Board()
        {
            PieceByPosition = new Dictionary<Position, Piece>();
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; j++)
                {
                    PieceByPosition.Add(new Position(i, j), null);
                }
            }
            {
                Position pos = new Position(3, 0);
                PieceByPosition[pos] = new King(pos, false);
                pos = new Position(3, 7);
                PieceByPosition[pos] = new King(pos, true);
                pos = new Position(4, 0);
                PieceByPosition[pos] = new Queen(pos, false);
                pos = new Position(4, 7);
                PieceByPosition[pos] = new Queen(pos, true);
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
                PieceByPosition[pos] = new Rook(pos, false);
                pos = new Position(i, 7);
                PieceByPosition[pos] = new Rook(pos, true);

            }
            for (int i = 1; i < 8; i += 5)
            {
                Position pos = new Position(i, 0);
                PieceByPosition[pos] = new Knight(pos, false);
                pos = new Position(i, 7);
                PieceByPosition[pos] = new Knight(pos, true);

            }
            for (int i = 2; i < 8; i += 3)
            {
                Position pos = new Position(i, 0);
                PieceByPosition[pos] = new Bishop(pos, false);
                pos = new Position(i, 7);
                PieceByPosition[pos] = new Bishop(pos, true);

            }

        }

        // Generate board for debugging.
        public static Board TwoRookBoard()
        {
            Board b = new Board();
            foreach (Position p in b.PieceByPosition.Keys.ToArray())
            {
                b.PieceByPosition[p] = null;
                if (p == new Position(1, 0))
                    b.PieceByPosition[p] = new King(p, false);
                if (p == new Position(2, 4))
                    b.PieceByPosition[p] = new King(p, true);
            }
            return b;
        }


        public List<Board> Successor()
        {
            List<Board> res = new List<Board>();

            /*           List<Piece> eligiblePieces = PieceByPosition.Values.ToList()
                           .Where(piece => piece.White == WhiteTurn).ToList();

                       Console.WriteLine(eligiblePieces.ToString());

                       eligiblePieces.ForEach(piece =>
                           {
                               res.Concat(piece.PossibleMoves(this));
                           }); */

            List<Piece> pieces = PieceByPosition.Values.ToList();
            foreach (Piece piece in pieces)
            {

                if (piece == null) continue;
                //                Console.WriteLine($"Testing {piece} {piece.Position}");
                if (piece.White != WhiteTurn)
                    continue;
                // Console.WriteLine($"{piece} {piece.Position} is eligible");
                List<Board> moves = piece.PossibleMoves(this);
                //Console.WriteLine($"moves.Count = {moves.Count}");
                foreach (Board move in moves)
                {
                    res.Add(move);
                }
            }
            //   Console.WriteLine($"res.Count = {res.Count}");
            return res;
        }

        public bool IsOccupied(Position position)
        {
            Debug.WriteLine("POSITION " + position.X + " " + position.Y);
            return PieceByPosition[position] != null;
        }

        public void DrawTiles(Graphics g)
        {
            Position[] pos = PieceByPosition.Keys.ToArray();
            for (int i = 0; i < 64; ++i)
            {
                new Position(i % 8, i / 8).Draw(g);
                //pos[i].Draw(g);
            }
            foreach (Position position in PieceByPosition.Keys)
            {
                if (PieceByPosition[position] == null) continue;
                PieceByPosition[position].Draw(g, position);
            }
        }

        public Board Click(Position p, List<Board> successiveStates)
        {
            Position clickedPosition = new Position((p.X - OFFSET) / HEIGHT, p.Y / HEIGHT);
            //if (!WhiteTurn || clickedPosition.X > 7 || clickedPosition.Y > 7) return this;

            Piece clickedPiece = PieceByPosition[clickedPosition];


            if (clickedPiece == null || !clickedPiece.White)
            {
                if (CurrentClickedPiece == null) return this;

                //impossible move, i.e. unclicked the currently selected piece
                foreach (Board ss in successiveStates)
                {
                    Console.WriteLine(ss.NewPos + " " + clickedPosition);
                }
                Console.WriteLine();
                if (!successiveStates.Any(ss => ss.NewPos.Equals(clickedPosition)))
                {
                    CurrentClickedPiece = null;
                    return this;
                }

                Board res = successiveStates.Find(ss => ss.NewPos.Equals(clickedPosition));
                successiveStates.Clear();
                return res;
            }
            else if (clickedPiece.White)
            {
                CurrentClickedPiece = clickedPiece;
                CurrentClickedPiece.PossibleMoves(this).ForEach(board => { successiveStates.Add(board); });
            }

            return this;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Turn: " + (WhiteTurn ? "White" : "Black"));
            foreach (Position position in PieceByPosition.Keys)
            {
                if (PieceByPosition[position] == null)
                    continue;
                sb.AppendLine($"{position}: {PieceByPosition[position]}");
            }
            sb.AppendLine("New pos: " + NewPos);
            sb.AppendLine("Evaluation: " + EvaluationUtils.evaluateBoard(this));

            return sb.ToString();
        }

    }
}
