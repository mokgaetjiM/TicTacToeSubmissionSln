using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeRendererLib.Enums;
using TicTacToeRendererLib.Renderer;



    namespace TicTacToeSubmissionConole
    {
        public class TicTacToe
        {
        // Renderer for drawing the board on the console
        private TicTacToeConsoleRenderer _boardRenderer;
            private PlayerEnum?[,] _board;
            private PlayerEnum _currentPlayer;

        // Counter to track the number of moves played
        private int _movesCount;

            public TicTacToe()
            {
                _boardRenderer = new TicTacToeConsoleRenderer(10, 6);
                _board = new PlayerEnum?[3, 3];
            // Player X starts the game
            _currentPlayer = PlayerEnum.X;
                _movesCount = 0;
                InitializeGame();
            }
        // Initializes the game by rendering the board and resetting variables

        private void InitializeGame()
            {
                _boardRenderer.Render();
                _board = new PlayerEnum?[3, 3];
                _currentPlayer = PlayerEnum.X;
            // Reset the move count
            _movesCount = 0;
            }

            public void Run()
            {
                while (true)
                {
                    PlayTurn();
                    if (CheckWin())
                    {
                        DisplayResult($"Player {_currentPlayer} wins!");
                        if (AskForNewGame()) InitializeGame();
                        else break;
                    }
                    else if (_movesCount == 9)
                    {
                        DisplayResult("It's a draw!");
                        if (AskForNewGame()) InitializeGame();
                        else break;
                    }
                    SwitchPlayer();
                }
            }
        // Main loop to run the game

        private void PlayTurn()
            {
                int row, column;
                do
                {
                    Console.SetCursorPosition(2, 19);
                    Console.Write($"Player {_currentPlayer}       ");
                    Console.SetCursorPosition(2, 20);
                Console.Write("Please Enter Row (from  0 to 2):");
                    row = GetValidInput();
                    Console.SetCursorPosition(2, 22);
                    Console.Write("Please Enter Column (from  0 to 2): ");
                    column = GetValidInput();
                } while (!IsValidMove(row, column));

                _board[row, column] = _currentPlayer;
                _boardRenderer.AddMove(row, column, _currentPlayer, true);
                _movesCount++;
            }

            private int GetValidInput()
            {
                int input;
                while (!int.TryParse(Console.ReadLine(), out input) || input < 0 || input > 2)
                {
                    Console.SetCursorPosition(2, 24);
                    Console.Write("Invalid input. Please enter a number between 0 and 2.");
                    Console.SetCursorPosition(2, 25);
                    Console.Write("Try again: ");
                }
                Console.SetCursorPosition(2, 24);
                Console.Write("                                                        ");
                Console.SetCursorPosition(2, 25);
                Console.Write("                                                        ");
                return input;
            }
        //check for valid moves
            private bool IsValidMove(int row, int column)
            {
                if (_board[row, column].HasValue)
                {
                    Console.SetCursorPosition(2, 24);
                    Console.Write("This cell is already occupied. Try again.");
                    return false;
                }
                return true;
            }
        // Check if the current player has won
        private bool CheckWin()
            {
                // Check rows, columns, and diagonals
                for (int i = 0; i < 3; i++)
                {
                    if (_board[i, 0] == _currentPlayer && _board[i, 1] == _currentPlayer && _board[i, 2] == _currentPlayer) return true;
                    if (_board[0, i] == _currentPlayer && _board[1, i] == _currentPlayer && _board[2, i] == _currentPlayer) return true;
                }
                if (_board[0, 0] == _currentPlayer && _board[1, 1] == _currentPlayer && _board[2, 2] == _currentPlayer) return true;
                if (_board[0, 2] == _currentPlayer && _board[1, 1] == _currentPlayer && _board[2, 0] == _currentPlayer) return true;

                return false;
            }
        //switching players

            private void SwitchPlayer()
            {
                _currentPlayer = _currentPlayer == PlayerEnum.X ? PlayerEnum.O : PlayerEnum.X;
            }

            private void DisplayResult(string message)
            {
                Console.SetCursorPosition(2, 24);
                Console.Write(message);
            }
        //asking for a new game/start
            private bool AskForNewGame()
            {
                Console.SetCursorPosition(2, 26);
                Console.Write("Do you want to play again? (Y/N): ");
                var response = Console.ReadLine().Trim().ToUpper();
                return response == "Y";
            }
        }
    }

