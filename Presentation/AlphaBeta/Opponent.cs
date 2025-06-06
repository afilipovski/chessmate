﻿using ChessMate.Domain;
using ChessMate.Domain.Positions;
using ChessMate.Service.Implementation;
using ChessMate.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessMate.Presentation.AlphaBeta
{
    /// <summary>
    /// AI Opponent
    /// </summary>
    public class Opponent
    {
        private readonly IBoardService _boardService = AiBoardService.Instance;
        static readonly Random R = new Random();

        /// <summary>
        /// Initializes the AI opponent.
        /// </summary>
        /// <param name="difficulty">An AI difficulty.</param>
        public Opponent(OpponentDifficulty difficulty)
        {
            Difficulty = difficulty;
        }

        public OpponentDifficulty Difficulty { get; set; }

        private struct Node
        {
            public Board Board;
            public int Value;

            public Node(Board board, int value)
            {
                this.Board = board;
                this.Value = value;
            }
        }

        /// <summary>
        /// Chooses a sufficient successor state as a move.
        /// </summary>
        /// <param name="board">A board state.</param>
        /// <returns>A board state.</returns>
        public Board Move(Board board)
        {
            List<Node> nodes = new List<Node>();
            int pivotValue = board.WhiteTurn ? -EvaluationUtils.Infty : EvaluationUtils.Infty;
            foreach (Board move in _boardService.GenerateSuccessiveStates(board)) {
                int value = EvaluationUtils.AlphabetaInit(move, (int)Difficulty, board.WhiteTurn);
                pivotValue = board.WhiteTurn ? Math.Max(pivotValue, value) : Math.Min(pivotValue,value);
                nodes.Add(new Node(move, value));
            }
            List<Node> eligibleMoves = nodes.FindAll(n => n.Value == pivotValue);
            if (eligibleMoves.Count > 0)
            {
                Board next = eligibleMoves[R.Next(eligibleMoves.Count)].Board;
                Position newPos = next.NewPos;
                return next;
            }
            return null; //returns null if there are no possible moves.
        }
    }
}
