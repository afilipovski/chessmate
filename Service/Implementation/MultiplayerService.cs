using ChessMate.Domain;
using ChessMate.Service.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChessMate.Domain.Exceptions;

namespace ChessMate.Service.Implementation
{
    public class MultiplayerService : SingletonBase<MultiplayerService>, IMultiplayerService
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://chess.filipovski.net")
        };

        private static async Task<HttpResponseMessage> MakePostRequest(string requestUri, HttpContent httpContent, CancellationTokenSource cts=null)
        {
            var token = await GetToken();
            httpContent.Headers.Add("X-CSRF-TOKEN", token);
            cts = cts ?? new CancellationTokenSource();
            return await httpClient.PostAsync(requestUri, httpContent, cts.Token);
        }

        public static async Task<string> GetToken()
        {
            var response = await httpClient.GetAsync("/token");
            return await response.Content.ReadAsStringAsync();
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

            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new UsernameTakenException(username);

            return new MultiplayerGame(stringResponse, username);
        }

        public async Task<MultiplayerGame> GetMultiplayerGame(string username)
        {
            var response = await httpClient.GetAsync($"/game?username={username}");
            var stringResponse = await response.Content.ReadAsStringAsync();

            return new MultiplayerGame(stringResponse, username);
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

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    if (stringResponse.StartsWith("User"))
                        throw new UsernameTakenException(username);
                    throw new GameFullException();
                case HttpStatusCode.NotFound:
                    throw new GameNotFoundException(joinCode);
            }

            return new MultiplayerGame(stringResponse, username);
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

        public async Task Move(string username, string joinCode, Move move)
        {
            var body = new Dictionary<string, string>
            {
                { "username", username },
                { "join_code", joinCode },
                { "from", move.PositionFrom.ToString() },
                { "to", move.PositionTo.ToString() },
            };

            if (move.ShouldConvertToQueen)
            {
                body.Add("promotion", "q");
            }

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await MakePostRequest("/game/move", content);
        }
    }
}
