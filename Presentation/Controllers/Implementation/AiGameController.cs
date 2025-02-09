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
using ChessMate.Presentation.Controllers.Interface;
using ChessMate.Presentation.GraphicsRendering;
using ChessMate.Presentation.Interface;
using ChessMate.Service.Implementation;
using ChessMate.Service.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace ChessMate.Presentation.Controllers.Implementation
{
    public class AiGameController : IAiGameController
    {
        public GameState GameState { get; set; }
        public string SavedGamePath { get; set; }
        public bool Dirty { get; set; }
        private Opponent opponent;

        private readonly IBoardService _boardService = AiBoardService.Instance;
        private readonly IGameStateService _gameStateService = GameStateService.Instance;
        private readonly Drawer _drawer = new Drawer();
        private readonly AiGameForm _form;

        public AiGameController(AiGameForm form)
        {
            _form = form;
        }

        public void GenerateGame()
        {
            GameState = new GameState();
            opponent = new Opponent(GameState.OpponentDifficulty);
            SavedGamePath = null;
            Dirty = false;

            UpdateTitle();
            _form.Checkmarks();
            _form.Invalidate();
        }

        public void PaintForm(PaintEventArgs e)
        {
            Board.TileSide = (_form.ClientSize.Height - Board.OffsetY) / 8;
            Board.OffsetX = (_form.ClientSize.Width - 8 * Board.TileSide) / 2;
            _drawer.DrawChessBoardForm(GameState, e.Graphics);
        }

        public void NewGame()
        {
            if (UnsavedChangesAbort())
                return;
            GenerateGame();
        }

        public void SubmitPlayerClick(int x, int y)
        {
            int xBoard = (x - Board.OffsetX) / Board.TileSide;
            int yBoard = (y - Board.OffsetY) / Board.TileSide;

            Board newBoard = _boardService.GetSuccessorStateForClickedPosition(new Position(xBoard, yBoard), GameState.Board, GameState.SuccessiveBoards);

            if (!ReferenceEquals(GameState.Board, newBoard))
            {
                Dirty = true;
                UpdateTitle();
            }

            GameState.Board = newBoard;

            _form.Invalidate();
            GameState.CheckPosition = _boardService.GetColoredKingCheckPosition(GameState.Board);
            _form.Refresh();

            //AI MOVE
            if (GameState.Board.WhiteTurn == false)
            {
                Board aiMove = opponent.Move(GameState.Board);

                if (aiMove != null)
                {
                    GameState.Board = aiMove;
                    if (_boardService.PossibleMovesNotExisting(GameState.Board))
                    {
                        if (_boardService.IsKingInCheck(GameState.Board, true))
                            UserInteractionUtils.ShowMessage("You are in checkmate.", "Defeat", NewGame);
                        else
                            UserInteractionUtils.ShowMessage("You are in stalemate.", "Stalemate", NewGame);
                    }
                }
                else //ai didn't generate move
                {
                    if (_boardService.IsKingInCheck(GameState.Board, false))
                        UserInteractionUtils.ShowMessage("AI is in checkmate.", "Victory", NewGame);
                    else
                        UserInteractionUtils.ShowMessage("The AI is in stalemate.", "Stalemate", NewGame);
                }
            }

            GameState.CheckPosition = _boardService.GetColoredKingCheckPosition(GameState.Board);

            _form.Refresh();
        }

        public void SaveGame()
        {
            if (SavedGamePath is null)
            {
                SaveGameAs();
                return;
            }
            _gameStateService.SaveToFile(GameState, SavedGamePath);
            Dirty = false;
            UpdateTitle();
        }

        public void SaveGameAs()
        {
            if (ChooseSaveGamePath())
                SaveGame();
        }

        public void OpenGame()
        {
            if (UnsavedChangesAbort())
                return;
            OpenFileResult result;
            try
            {
                result = UserInteractionUtils.ShowOpenFileDialog();
            }
            catch (FileNotChosenException e)
            {
                return;
            }
            try
            {
                GameState = _gameStateService.LoadFromFile(result.File);
            }
            catch (Exception)
            {
                UserInteractionUtils.ShowMessage("The file is either corrupted or not a ChessMate savegame.", "Loading failed", () => {});
            }
            opponent = new Opponent(GameState.OpponentDifficulty);
            SavedGamePath = result.FilePath;
            Dirty = false;
            UpdateTitle();
            _form.Invalidate();
        }

        public void SetDifficulty(OpponentDifficulty difficulty)
        {
            opponent.Difficulty = GameState.OpponentDifficulty = difficulty;
            _form.Checkmarks();
        }

        public void ExitGame(FormClosingEventArgs e)
        {
            if (UnsavedChangesAbort())
                e.Cancel = true;
        }

        public OpponentDifficulty GetDifficulty()
        {
            return GameState.OpponentDifficulty;
        }

        private bool ChooseSaveGamePath()
        {
            string filename;
            try
            {
                filename = UserInteractionUtils.ShowSaveFileDialog();
            }
            catch (FilePathNotChosenException e)
            {
                return false;
            }
            SavedGamePath = filename;
            _form.Text = $"ChessMate: {Path.GetFileNameWithoutExtension(SavedGamePath)}";
            return true;
        }

        private bool UnsavedChangesAbort()
        {
            if (!Dirty)
                return false;
            if (!UserInteractionUtils.ShowConfirmDialog("Do you want to save your game ? ", "Unsaved progress", () => {}))
                return false;
            SaveGame();
            if (!Dirty)
                return false;
            return true;
        }

        private void UpdateTitle()
        {
            _form.Text = "ChessMate - AI Opponent";
            if (SavedGamePath != null)
                _form.Text += $" - {Path.GetFileNameWithoutExtension(SavedGamePath)}";
            if (Dirty)
                _form.Text += "*";
        }
    }
}
