using ChessMate.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.AlphaBeta
{
    public class EvaluationUtils
    {
        public const int INFTY = 10000;


        //high value favors white
        public static int evaluateBoard(Board board)
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

        public static int alphabeta_init(Board node, int depth, bool maximisingPlayer)
        {
            return alphabeta(node, depth, -INFTY, INFTY, maximisingPlayer);
        }

        static int alphabeta(Board node, int depth, int alpha, int beta, bool maximisingPlayer)
        {
            List<Board> children = node.Successor();

            if (depth == 0 || children.Count == 0)
                return evaluateBoard(node);
            if (maximisingPlayer)
            {
                int value = -INFTY;
                foreach (Board child in children)
                {
                    value = Math.Max(value, alphabeta(child, depth - 1, alpha, beta, false));
                    if (value > beta)
                        break;
                    alpha = Math.Max(alpha, value);
                }
                return value;
            }
            else
            {
                int value = INFTY;
                foreach (Board child in children) {
                    value = Math.Min(value, alphabeta(child, depth - 1, alpha, beta, true));
                    if (value < alpha)
                        break;
                    beta = Math.Min(beta, value);
                }
                return value;
            }
        }

        /*
                 function alphabeta(node, depth, α, β, maximizingPlayer) is
                    if depth == 0 or node is terminal then
                        return the heuristic value of node
                    if maximizingPlayer then
                        value := −∞
                        for each child of node do
                            value := max(value, alphabeta(child, depth − 1, α, β, FALSE))
                            if value > β then
                                break (* β cutoff *)
                            α := max(α, value)
                        return value
                    else
                        value := +∞
                        for each child of node do
                            value := min(value, alphabeta(child, depth − 1, α, β, TRUE))
                            if value < α then
                                break (* α cutoff *)
                            β := min(β, value)
                    return value
         */
    }
}
