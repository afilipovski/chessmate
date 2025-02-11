using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Domain.Exceptions
{
    public class GameFullException : Exception
    {
        public GameFullException() : base("The game you are searching already has 2 players!")
        {
        }
    }
}
