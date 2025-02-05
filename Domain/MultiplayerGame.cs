using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Domain
{
    public class MultiplayerGame
    {
        public string JoinCode { get; set; }
        public string Username1 { get; set; }
        public string Username2 { get; set; }
        public bool WhiteTurn { get; set; }
        public Move LastMove { get; set; }
    }
}
