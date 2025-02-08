using ChessMate.Domain.Positions;
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
        public string PlayerUsername { get; set; }
        public string Username1 { get; set; }
        public string Username2 { get; set; }
        public bool WhiteTurn { get; set; }
        public Move LastMove { get; set; }
        public string OpponentUsername => Username1 == PlayerUsername ? Username2 : Username1;



        public MultiplayerGame()
        {
        }

        public MultiplayerGame(string apiResponse, string playerUsername)
        {
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiResponse);

            try
            {
                JoinCode = dictionary["join_code"] as string;
                Username1 = dictionary["username1"] as string;
                string whiteTurnString = dictionary["white_move"].ToString();
                WhiteTurn = whiteTurnString == "1" || whiteTurnString == "true";
                Username2 = dictionary.ContainsKey("username2") ? dictionary["username2"] as string : String.Empty;
                LastMove = dictionary.ContainsKey("last_move_from") && dictionary["last_move_from"] != null ? new Move
                {
                    PositionFrom = new Position(dictionary["last_move_from"] as string),
                    PositionTo = new Position(dictionary["last_move_to"] as string),
                } : null;
            }
            catch (Exception e)
            {
                throw new Exception($@"
                    Error parsing multiplayer game from API response.
                    {apiResponse}
                ", e);
            }

            PlayerUsername = playerUsername;
        }
    }
}
