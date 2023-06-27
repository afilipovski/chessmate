using ChessMate.Pieces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

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
        public Position NewPos { get; set; }

        // copy constructor
        public Board(Board board)
        {
            PieceByPosition = new Dictionary<Position, Piece>();
            WhiteTurn = !board.WhiteTurn;
            foreach (Position key in board.PieceByPosition.Keys)
            {
                PieceByPosition[key] = board.PieceByPosition[key];
            }
        }

        // copy 2
        public Board(Board b, Position posOld, Position posNew, Piece p) : this(b)
        {
            NewPos = new GreenPosition(posNew);
            PieceByPosition[posNew] = p;
            PieceByPosition[posOld] = null;
        }

        public static bool IsInBoard(Position p)
        {
            return p.X >= 0 && p.Y >= 0 && p.X <= 7 && p.Y <= 7;
        }

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
                Debug.WriteLine(PieceByPosition[pos].Position.X + " " + PieceByPosition[pos].Position.Y);
                pos = new Position(i, 7);
                PieceByPosition[pos] = new Bishop(pos, true);
                Debug.WriteLine(PieceByPosition[pos].Position.X + " " + PieceByPosition[pos].Position.Y);

            }

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
                Console.WriteLine($"{piece} {piece.Position} is eligible");
                List<Board> moves = piece.PossibleMoves(this);
                //Console.WriteLine($"moves.Count = {moves.Count}");
                foreach (Board move in moves)
                {
                    res.Add(move);
                }
            }
            Console.WriteLine($"res.Count = {res.Count}");
            return res;
        }

        public bool IsOccupied(Position position)
        {
            Debug.WriteLine("POSITION " + position.X + " " + position.Y);
            return PieceByPosition[position] != null;
        }

        public void DrawTiles(Graphics g, int height, int width, int offset)
        {
            HEIGHT = height;
            WIDTH = width;
            OFFSET = offset;
            Position[] pos = PieceByPosition.Keys.ToArray();
            for (int i = 0; i < 64; ++i)
            {
                pos[i].Draw(g);
            }
            foreach (Piece piece in PieceByPosition.Values)
            {
                if (piece == null) continue;
                piece.Draw(g);
            }
        }

        public Board Click(Position p, List<GreenPosition> greenPositions)
        {
            Position clickedPosition = new Position((p.X - OFFSET) / HEIGHT, p.Y / HEIGHT);
            //if (!WhiteTurn || clickedPosition.X > 7 || clickedPosition.Y > 7) return this;

            Piece clickedPiece = PieceByPosition[clickedPosition];
            if (clickedPiece == null || !clickedPiece.White)
            {
                if (CurrentClickedPiece == null) return this;
                Board newBoard = CurrentClickedPiece.PossibleMoves(this).Find(board => 
                    board.PieceByPosition[clickedPosition] == CurrentClickedPiece);

                //impossible move, i.e. unclicked the currently selected piece
                if (newBoard == null)
                {
                    CurrentClickedPiece = null;
                    return this;
                }

                CurrentClickedPiece.Position = clickedPosition;
                CurrentClickedPiece = null;
                WhiteTurn = false;
                return newBoard;
            }
            else if(CurrentClickedPiece == null || clickedPiece.White)
            {
                CurrentClickedPiece = clickedPiece;
                CurrentClickedPiece.PossibleMoves(this).ForEach(board => { greenPositions.Add(new GreenPosition(board.NewPos)); });
            }

            return this;

        }

        public string ToString()
        {
            return $"Board: {WhiteTurn}";
        }

    }
}
