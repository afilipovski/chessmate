using ChessMate.Domain.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Domain
{
    public class Move
    {
        public Position PositionFrom { get; set; }
        public Position PositionTo { get; set; }
        public Type PieceType { get; set; }
    }
}
