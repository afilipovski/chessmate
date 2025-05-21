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
using System.Windows.Forms;
using System.CodeDom;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ChessMate.Service.Implementation
{
    public class MultiplayerService : SingletonBase<MultiplayerService>, IMultiplayerService
    {
        public static string Username { get; set; }
        public static string Password { get; set; }

        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://chess.filipovski.net")
            
        };

        private static async Task<HttpResponseMessage> MakePostRequest(string requestUri, HttpContent httpContent)
        {
            var token = await GetToken();
            httpContent.Headers.Add("X-CSRF-TOKEN", token);

            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Username}:{Password}"));
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);

            return await httpClient.PostAsync(requestUri, httpContent);
        }

        public async Task Register(string username, string password, string confirmPassword)
        {
            if (username == "" || password == "" || confirmPassword == "")
            {
                MessageBox.Show("All registration fields are required");
                return;
            }
            if (string.Compare(password, confirmPassword, StringComparison.Ordinal) != 0)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }
            

            var queryParams = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password },
                { "password_confirmation", confirmPassword }
            };

            var queryString = new FormUrlEncodedContent(queryParams);

            var response = await MakePostRequest("/register",queryString);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                MessageBox.Show("User already exists");
                return;
            }
            else if (response.StatusCode != HttpStatusCode.Created)
            {
                MessageBox.Show("Failed to register");
                return;
            }
        }

        private static async Task<string> GetToken()
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
            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Username}:{Password}"));
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);

            var response = await httpClient.GetAsync($"/game?username={username}");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                MessageBox.Show("Invalid credentials");
                throw new Exception();
            }

            var stringResponse = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new GameNotFoundException("Your opponent has forfeit the game. You win!");

            return new MultiplayerGame(stringResponse, username);
        }

        public async Task ValidateCredentials()
        {
            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Username}:{Password}"));
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);

            var response = await httpClient.GetAsync($"/auth/check");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Wrong credentials");
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
                    throw new GameNotFoundException($"Couldn't find a game with the join code '{joinCode}'!");
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
