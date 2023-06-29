using ChessMate.AlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate
{
	[Serializable]
	public class GameState
	{
		public Board Board { get; set; }
		public List<Board> successiveBoards { get; set; } = new List<Board>();
		public Opponent o { get; set; }
	}
}
