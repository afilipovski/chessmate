using ChessMate.Domain;
using ChessMate.Service.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ChessMate.Service.Implementation
{
    public class MultiplayerService : IMultiplayerService
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

        public async Task Move(string username, string joinCode, Move move, Action<MultiplayerGame> callback)
        {
            var body = new Dictionary<string, string>
            {
                { "username", username },
                { "join_code", joinCode },
                { "from", move.PositionFrom.ToString() },
                { "to", move.PositionTo.ToString() },
                { "promotion", move.ShouldConvertToQueen ? "q" : null }
            };

            var json = JsonConvert.SerializeObject(body);
            await LongPollingForMove(json, callback);
        }

        private async Task LongPollingForMove(string jsonContent, Action<MultiplayerGame> callback)
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                try
                {
                    var response = await MakePostRequest("/game/move", new StringContent(jsonContent), _cts);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonData = await response.Content.ReadAsStringAsync();
                        callback.Invoke(new MultiplayerGame(jsonData));
                    }

                    // Simulate long polling: Wait before sending another request
                    await Task.Delay(5000, _cts.Token);
                }
                catch (TaskCanceledException) { break; } // Stop if cancellation is requested
                catch (Exception ex)
                {
                    Console.WriteLine($"Long polling error: {ex.Message}");
                    await Task.Delay(5000); // Retry delay
                }
            }
        }

        public void CancelMove()
        {
            _cts.Cancel();
        }
    }
}
