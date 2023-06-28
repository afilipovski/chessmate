using ChessMate.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessMate.AlphaBeta
{
    public enum OpponentDifficulty
    {
        EASY = 0,
        MEDIUM = 1,
        HARD = 2
    }
    
    public class Opponent
    {
        static Random r = new Random();

        public Opponent(OpponentDifficulty difficulty)
        {
            Difficulty = difficulty;
        }

        public OpponentDifficulty Difficulty { get; set; }

        private struct Node
        {
            public Board board;
            public int value;

            public Node(Board board, int value)
            {
                this.board = board;
                this.value = value;
            }
        }
        public Board Move(Board board)
        {
            //Console.WriteLine("Evaluating root: " + (board.WhiteTurn ? "maximizing" : "minimizing"));
            List<Node> nodes = new List<Node>();
            int pivot_value = board.WhiteTurn ? -EvaluationUtils.INFTY : EvaluationUtils.INFTY;
            foreach (Board move in board.Successor()) {
                int value = EvaluationUtils.alphabeta_init(move, (int)Difficulty, board.WhiteTurn);
                pivot_value = board.WhiteTurn ? Math.Max(pivot_value, value) : Math.Min(pivot_value,value);
                nodes.Add(new Node(move, value));
            }
            //Console.WriteLine($"Best AI score: {pivot_value}\n");
            List<Node> eligibleMoves = nodes.FindAll(n => n.value == pivot_value);
            //     Console.Write($"eligibleMoves.Count = {eligibleMoves.Count}");
            if (eligibleMoves.Count > 0)
            {
                Board next = eligibleMoves[r.Next(eligibleMoves.Count)].board;
                Position newPos = next.NewPos;

                //next.PieceByPosition[newPos] = new Rook(newPos, false);
                return next;
            }
            return null; //returns null if there are no possible moves.
        }
    }
}
