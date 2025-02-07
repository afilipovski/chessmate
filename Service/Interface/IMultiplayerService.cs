using ChessMate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Service.Interface
{
    public interface IMultiplayerService
    {
        Task<MultiplayerGame> GetMultiplayerGame(string username);
        Task<MultiplayerGame> CreateGame(string username);
        Task<MultiplayerGame> JoinGame(string username, string joinCode);
        Task Move(string username, string joinCode, Move move, Action<MultiplayerGame> callback);
        Task LeaveGame(string username, string joinCode);
        void CancelMove();
    }
}
