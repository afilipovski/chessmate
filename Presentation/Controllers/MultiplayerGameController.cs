using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessMate.Domain;
using ChessMate.Domain.Exceptions;
using ChessMate.Domain.Positions;
using ChessMate.Presentation.AlphaBeta;
using ChessMate.Presentation.GraphicsRendering;
using ChessMate.Presentation.Interface;
using ChessMate.Service.Implementation;
using ChessMate.Service.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace ChessMate.Presentation.Controllers
{
    public class MultiplayerGameController
    {
        public GameState GameState { get; set; }

        private readonly IBoardService _boardService;
        private readonly IGameStateService _gameStateService = new GameStateService();
        private readonly IMultiplayerService _multiplayerService = new MultiplayerService();
        private Drawer _drawer;
        private readonly Form2 _form;

        private bool _whitePov;

        private MultiplayerGame _multiplayerGame;


        public MultiplayerGameController(Form2 form, bool whitePov, MultiplayerGame multiplayerGame)
        {
            _form = form;
            this._whitePov = whitePov;
            this._drawer = new Drawer(whitePov);
            this._boardService = new MultiplayerBoardService(whitePov);
            this._multiplayerGame = multiplayerGame;

            GetUsernames().ContinueWith(t =>
            {
                _form.Invalidate();
            });
        }

        public void GenerateGame()
        {
            GameState = new GameState();

            _form.Invalidate();
        }

        public async Task GetUsernames()
        {
            while (this._multiplayerGame == null || string.IsNullOrEmpty(_multiplayerGame.Username2))
            {
                MultiplayerGame multiplayerGame = await _multiplayerService.GetMultiplayerGame(_multiplayerGame.PlayerUsername);
                await Task.Delay(1000);
                this._multiplayerGame = multiplayerGame;
            }
        }

        public void PaintForm(PaintEventArgs e)
        {
            Board.TileSide = (_form.ClientSize.Height - Board.OffsetY) / 8;
            Board.OffsetX = (_form.ClientSize.Width - 8 * Board.TileSide) / 2;
            _drawer.DrawChessBoardForm(GameState, e.Graphics, _multiplayerGame);
        }

        public void NewGame()
        {
            GenerateGame();
        }

        public void SubmitPlayerClick(int x, int y)
        {
            int xBoard = (x - Board.OffsetX) / Board.TileSide;
            int yBoard = (y - Board.OffsetY) / Board.TileSide;

            var position = new Position(
                !_whitePov ? 7 - xBoard : xBoard,
                !_whitePov ? 7 - yBoard : yBoard
            );
            Board newBoard = _boardService.GetSuccessorStateForClickedPosition(position, GameState.Board, GameState.SuccessiveBoards);

            GameState.Board = newBoard;

            _form.Invalidate();
            GameState.CheckPosition = _boardService.GetColoredKingCheckPosition(GameState.Board);
            _form.Refresh();


            GameState.CheckPosition = _boardService.GetColoredKingCheckPosition(GameState.Board);

            _form.Refresh();
        }
    }
}
