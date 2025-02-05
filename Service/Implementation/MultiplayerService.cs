using ChessMate.Domain;
using ChessMate.Service.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChessMate.Service.Implementation
{
    public class MultiplayerService : IMultiplayerService
    {
        public static HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://chess.filipovski.net")
        };

        private static async Task<HttpResponseMessage> MakePostRequest(string requestUri, HttpContent httpContent = null)
        {
            var token = await GetToken();
            httpContent.Headers.Add("X-CSRF-TOKEN", token);
            return await httpClient.PostAsync(requestUri, httpContent);
        }

        public static async Task<string> GetToken()
        {
            var response = await httpClient.GetAsync("/token");
            return response.Content.ReadAsStringAsync().Result;
        }

        public async Task<MultiplayerGame> CreateGame(string username)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "username", username }
            };

            var queryString = new FormUrlEncodedContent(queryParams);

            var response = await MakePostRequest("/game", queryString);
            var stringResponse = await response.Content.ReadAsStringAsync();

            return new MultiplayerGame(stringResponse);
        }

        public async Task<MultiplayerGame> GetMultiplayerGame(string username)
        {
            var response = await httpClient.GetAsync($"/game?username={username}");
            var stringResponse = await response.Content.ReadAsStringAsync();

            return new MultiplayerGame(stringResponse);
        }

        public async Task<MultiplayerGame> JoinGame(string username, string joinCode)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "username", username },
                { "join_code", joinCode }
            };

            var queryString = new FormUrlEncodedContent(queryParams);

            var response = await MakePostRequest("/game/join", queryString);
            var stringResponse = await response.Content.ReadAsStringAsync();

            return new MultiplayerGame(stringResponse);
        }

        public async Task LeaveGame(string username, string joinCode)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "username", username },
                { "join_code", joinCode }
            };

            var queryString = new FormUrlEncodedContent(queryParams);

            await MakePostRequest("/game/leave", queryString);
        }

        public async Task<MultiplayerGame> Move(string username, string joinCode, Move move)
        {
            throw new NotImplementedException();
        }
    }
}
