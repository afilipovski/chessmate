using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public MultiplayerGame()
        {
        }

        public MultiplayerGame(string apiResponse)
        {
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiResponse);

            JoinCode = dictionary["join_code"] as string;
            Username1 = dictionary["username1"] as string;
            WhiteTurn = (bool)dictionary["white_move"];
            Username2 = dictionary.ContainsKey("username2") ? dictionary["username2"] as string : String.Empty;
        }
    }
}
