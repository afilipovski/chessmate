using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Domain;
using ChessMate.Presentation;

namespace ChessMate.Service.Interface
{
    public interface IGameStateService
    {
        void SaveToFile(GameState gameState, string filePath);
        GameState LoadFromFile(Stream fileStream);
    }
}
