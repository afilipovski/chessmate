using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Presentation;
using ChessMate.Service.Interface;

namespace ChessMate.Service.Implementation
{
    public class GameStateService : IGameStateService
    {
        public void SaveToFile(GameState gameState, string filePath)
        {
            FileStream stream = File.OpenWrite(filePath);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, gameState);
            stream.Dispose();
        }

        public GameState LoadFromFile(Stream fileStream)
        {
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(fileStream) as GameState;
        }
    }
}
