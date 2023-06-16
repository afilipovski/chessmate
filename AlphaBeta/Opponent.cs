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
        EASY = 2,
        MEDIUM = 3,
        HARD = 4
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
            List<Node> nodes = new List<Node>();
            int maxvalue = -EvaluationUtils.INFTY;
            foreach (Board move in board.Successor()) {
                int value = EvaluationUtils.alphabeta_init(move, (int)Difficulty, board.WhiteTurn);
                maxvalue = Math.Max(maxvalue, value);
                nodes.Add(new Node(move, value));
            }
            List<Node> eligibleMoves = nodes.FindAll(n => n.value == maxvalue);
            return eligibleMoves[r.Next(eligibleMoves.Count)].board;
        }
    }
}
