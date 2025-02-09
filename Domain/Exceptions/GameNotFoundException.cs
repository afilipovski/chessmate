using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Domain.Exceptions
{
    public class GameNotFoundException : Exception
    {
        public GameNotFoundException(string joinCode) : base($"Couldn't find a game with the join code '{joinCode}'!")
        {
        }
    }
}
