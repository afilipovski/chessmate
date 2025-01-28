using ChessMate.Domain;
using ChessMate.Domain.Positions;
using ChessMate.Presentation.AlphaBeta;
using ChessMate.Service.Implementation;
using ChessMate.Service.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Presentation
{
	[Serializable]
	public class GameState
	{
		public Board Board { get; set; }
		public List<Board> successiveBoards { get; set; } = new List<Board>();
		public Opponent o { get; set; }
		public ColoredPosition checkPosition { get; set; } = null;
        private readonly IBoardService _boardService = new BoardService();

        public GameState()
		{
			Board = new Board();
			successiveBoards = new List<Board>();
			o = new Opponent(OpponentDifficulty.EASY);
			checkPosition = null;
		}

		public void SetCheckPosition()
		{
			checkPosition = null;
			Position king = _boardService.GetKingPositionIfInCheck(Board, Board.WhiteTurn);
			if (king is null)
				return;
			checkPosition = new ColoredPosition(king, PositionColor.Red);
		}
    }
}
