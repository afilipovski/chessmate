﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ChessMate.Domain.Pieces;
using ChessMate.Domain.Positions;
using ChessMate.Presentation.AlphaBeta;
using ChessMate.Presentation.Controllers.Interface;
using ChessMate.Presentation.GraphicsRendering;
using ChessMate.Presentation.Interface;
using ChessMate.Service.Implementation;
using ChessMate.Service.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace ChessMate.Presentation.Controllers.Implementation
{
    public class MultiplayerGameController : IMultiplayerGameController
    {
        public GameState GameState { get; set; }
        private readonly IBoardService _boardService;
        private readonly IMultiplayerService _multiplayerService = MultiplayerService.Instance;
        private readonly Drawer _drawer;
        private readonly MultiplayerGameForm _form;
        private bool _hasOpponentForfeit;
        private bool _hasCurrentUserForfeit;
        private readonly bool _whitePov;
        private MultiplayerGame _multiplayerGame;

        public MultiplayerGameController(MultiplayerGameForm form, bool whitePov, MultiplayerGame multiplayerGame)
        {
            _form = form;
            this._whitePov = whitePov;
            this._drawer = new Drawer(whitePov);
            this._boardService = new MultiplayerBoardService(whitePov);
            this._multiplayerGame = multiplayerGame;
            _hasOpponentForfeit = false;
            _hasCurrentUserForfeit = false;

            GenerateGame();

            GetUsernames().ContinueWith(t =>
            {
                _form.SetOpponentName(!_whitePov ? _multiplayerGame.Username1 : _multiplayerGame.Username2);
                _form.Invalidate();
            });
            if (!whitePov)
            {
                ConsumeOpponentMove();
            }
        }

        private async Task ConsumeOpponentMove()
        {
            while (_whitePov != GameState.Board.WhiteTurn && !_hasCurrentUserForfeit)
            {
                MultiplayerGame game;
                
                try
                {
                    game = await _multiplayerService.GetMultiplayerGame(_multiplayerGame.PlayerUsername);
                }
                catch (GameNotFoundException ex)
                {
                    _hasOpponentForfeit = true;
                    UserInteractionUtils.ShowMessage(ex.Message, "Forfeit", _form.Close);
                    return;
                }

                if (game.WhiteTurn == _whitePov)
                {
                    Piece clickedPiece = GameState.Board.PieceByPosition[game.LastMove.PositionFrom];
                    GameState.Board = clickedPiece.PossibleMoves(GameState.Board)
                        .Single(b => b.NewPos.Equals(game.LastMove.PositionTo));
                    GameState.Board.WhiteTurn = _whitePov;

                    if (_boardService.PossibleMovesNotExisting(GameState.Board))
                    {
                        if (_boardService.IsKingInCheck(GameState.Board, _whitePov))
                            UserInteractionUtils.ShowMessage("You are in checkmate.", "Defeat", _form.Close);
                        else
                            UserInteractionUtils.ShowMessage("You are in stalemate.", "Stalemate", _form.Close);
                    }

                    _form.Invalidate();
                    return;
                }
                await Task.Delay(1000);
            }
        }

        private void GenerateGame()
        {
            GameState = new GameState();
            _form.SetJoinCode(_multiplayerGame.JoinCode);
            _form.Invalidate();
        }

        private async Task GetUsernames()
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
            Board.OffsetY = 120;
            Board.TileSide = (_form.ClientSize.Height - Board.OffsetY) / 8;
            Board.OffsetX = (_form.ClientSize.Width - 8 * Board.TileSide) / 2;
            _form.ResizeGroupBox(Board.OffsetX, Board.OffsetY);
            _drawer.DrawChessBoard(GameState, e.Graphics);
        }

        public void QuitGame(FormClosingEventArgs e)
        {
            if (_hasOpponentForfeit)
                return;
            e.Cancel = true;
            Action callback = () =>
            {
                _hasCurrentUserForfeit = true;
                e.Cancel = false;
            };
            UserInteractionUtils.ShowConfirmDialog(
                string.IsNullOrEmpty(_multiplayerGame.Username2)
                    ? "Are you sure you want to exit the game?"
                    : "Are you sure you want to exit the game? You'll automatically forfeit!", 
                "Exit Game", 
                callback);
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
            
            TryPublishPlayerMove(newBoard);

            GameState.Board = newBoard;

            _form.Invalidate();
            GameState.CheckPosition = _boardService.GetColoredKingCheckPosition(GameState.Board);
            _form.Refresh();


            bool isOpponentTurn = GameState.Board.WhiteTurn != _whitePov;
            bool noMovesPossible = _boardService.PossibleMovesNotExisting(GameState.Board);
            if (isOpponentTurn && noMovesPossible)
            {
                if (_boardService.IsKingInCheck(GameState.Board, !_whitePov))
                    UserInteractionUtils.ShowMessage("Opponent is in checkmate.", "Victory", _form.Close);
                else
                    UserInteractionUtils.ShowMessage("Opponent is in stalemate.", "Stalemate", _form.Close);
            }

            GameState.CheckPosition = _boardService.GetColoredKingCheckPosition(GameState.Board);

            _form.Refresh();
        }

        private void TryPublishPlayerMove(Board newBoard)
        {
            bool playerMadeAMove = GameState.Board.WhiteTurn == _whitePov && newBoard.WhiteTurn != _whitePov;
            if (!playerMadeAMove)
                return;
            Piece currentClickedPiece = GameState.Board.CurrentClickedPiece;
            bool shouldPromoteToQueen = (newBoard.NewPos.Y == 0 && currentClickedPiece.White) ||
                                        (newBoard.NewPos.Y == 7 && !currentClickedPiece.White);
            var move = new Move
            {
                PositionFrom = currentClickedPiece.Position,
                PositionTo = newBoard.NewPos,
                ShouldConvertToQueen = shouldPromoteToQueen
            };
            _multiplayerService.Move(_multiplayerGame.PlayerUsername, _multiplayerGame.JoinCode, move).ContinueWith(t =>
            {
                ConsumeOpponentMove();
            });
        }
    }
}
