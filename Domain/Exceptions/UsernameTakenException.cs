using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Domain.Exceptions
{
    public class UsernameTakenException : Exception
    {
        public UsernameTakenException(string username) : base($"User '{username}' already has a game in progress!")
        {
        }
    }
}
