using ChessMate.Domain;
using ChessMate.Domain.Pieces;
using ChessMate.Service.Implementation;
using ChessMate.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Presentation.AlphaBeta
{
    public class EvaluationUtils
    {
        public const int Infty = 10000;
        private static readonly IBoardService BoardService = AiBoardService.Instance;

        //high value favors white
        public static int EvaluateBoard(Board board)
        {
            int value = 0;
            board.PieceByPosition.Values.ToList().ForEach(piece =>
            {
                
                if (piece == null)
                    return;

                int local = 0;
                if (piece is Bishop) local = 30;
                else if (piece is King) local = 900;
                else if (piece is Queen) local = 90;
                else if (piece is Rook) local = 50;
                else if (piece is Knight) local = 30;
                else local = 10;

                value += piece.White ? local : -local;
            });
            return value;
        }

        public static int AlphabetaInit(Board node, int depth, bool maximisingPlayer)
        {
            return Alphabeta(node, depth, -Infty, Infty, maximisingPlayer);
        }

        static int Alphabeta(Board node, int depth, int alpha, int beta, bool maximisingPlayer)
        {
            List<Board> children = BoardService.GenerateSuccessiveStates(node);

            if (depth == 0 || children.Count == 0)
                return EvaluateBoard(node);
            if (maximisingPlayer)
            {
                int value = -Infty;
                foreach (Board child in children)
                {
                    value = Math.Max(value, Alphabeta(child, depth - 1, alpha, beta, false));
                    if (value > beta)
                        break;
                    alpha = Math.Max(alpha, value);
                }
                return value;
            }
            else
            {
                int value = Infty;
                foreach (Board child in children) {
                    value = Math.Min(value, Alphabeta(child, depth - 1, alpha, beta, true));
                    if (value < alpha)
                        break;
                    beta = Math.Min(beta, value);
                }
                return value;
            }
        }
    }
}
