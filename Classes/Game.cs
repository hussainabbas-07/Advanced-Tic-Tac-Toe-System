using AdvancedTicTacToeWinForms.Utilities;
using System;

namespace AdvancedTicTacToeWinForms.Classes
{
    public class Game
    {
        private Board board;

        private Player player1;
        private Player player2;

        private Player currentPlayer;

        private bool isDraw;

        public Game(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;

            board = new Board();

            Random toss = new Random();

            if (toss.Next(2) == 0)
            {
                currentPlayer = player1;
            }
            else
            {
                currentPlayer = player2;
            }


            isDraw = false;
        }

        public void DisplayGameInfo()
        {
            player1.DisplayInfo();
            player2.DisplayInfo();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nCurrent Turn: {currentPlayer.Name}\n");
            Console.ResetColor();
        }

        public void DisplayBoard()
        {
            board.DisplayBoard();
        }

        public void StartGame()
        {
            bool gameOver = false;

            while (!gameOver)
            {
                DisplayBoard();

                int move = currentPlayer.MakeMove();

                if (!board.IsPositionAvailable(move))
                {
                    continue;
                }

                board.PlaceMove(move, currentPlayer.Symbol);

                if (board.CheckWinner(currentPlayer.Symbol))
                {
                    DisplayBoard();
                    gameOver = true;
                }
                else if (board.IsDraw())
                {
                    DisplayBoard();
                    isDraw = true;

                    gameOver = true;
                }
                else
                {
                    SwitchPlayer();
                }
            }
        }

        private void SwitchPlayer()
        {
            if (currentPlayer == player1)
            {
                currentPlayer = player2;
            }
            else
            {
                currentPlayer = player1;
            }
        }

        public Player GetWinner()
        {
            if (isDraw)
            {
                return null;
            }

            return currentPlayer;
        }

        public Player GetPlayer1()
        {
            return player1;
        }

        public Player GetPlayer2()
        {
            return player2;
        }

        public Player GetCurrentPlayer()
        {
            return currentPlayer;
        }

        public bool IsDraw()
        {
            return isDraw;
        }
    }
}
