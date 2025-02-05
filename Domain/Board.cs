using ChessMate.Domain.Pieces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using ChessMate.Domain.Positions;

namespace ChessMate.Domain
{
    [Serializable]
    public class Board
    {
        public Dictionary<Position, Piece> PieceByPosition { get; set; }
        public bool WhiteTurn { get; set; } = true;
        public static int TileSide { get; set; }
        public static int OffsetX { get; set; }
        public static int OffsetY { get; set; } = 50;
        public Piece CurrentClickedPiece { get; set; }
        public ColoredPosition NewPos { get; set; }

        public int TurnNumber { get; set; } = 0;

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
            TurnNumber = board.TurnNumber + 1;
        }

        public Board(Board b, Position posOld, Position posNew, Piece p) : this(b)
        {
            p = p.Clone();
            NewPos = new ColoredPosition(posNew);
            PieceByPosition[posNew] = p;
            PieceByPosition[posOld] = null;
            PieceByPosition[posNew].Position = posNew;
        }

		public Board(Board b, Position posOld, ColoredPosition posNew, Piece p) : this(b, posOld, posNew as Position, p)
		{
            NewPos = posNew;
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
                Position pos = new Position(4, 0);
                PieceByPosition[pos] = new King(pos, false);
                pos = new Position(4, 7);
                PieceByPosition[pos] = new King(pos, true);
                pos = new Position(3, 0);
                PieceByPosition[pos] = new Queen(pos, false);
                pos = new Position(3, 7);
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

        public bool IsOccupied(Position position)
        {
            return PieceByPosition[position] != null;
        }
    }
}
