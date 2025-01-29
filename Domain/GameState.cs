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

namespace ChessMate.Domain
{
	[Serializable]
	public class GameState
	{
		public Board Board { get; set; } = new Board();
        public List<Board> SuccessiveBoards { get; set; } = new List<Board>();
        public OpponentDifficulty OpponentDifficulty { get; set; } = OpponentDifficulty.Easy;
        public ColoredPosition CheckPosition { get; set; } = null;
    }
}
