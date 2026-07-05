using AdvancedTicTacToeGame.Utilities;
using System;

namespace AdvancedTicTacToeGame.Classes
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

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.ResetColor();

            isDraw = false;
        }

        public void DisplayGameInfo()
        {
            ConsoleUI.ShowTitle("TIC TAC TOE");

            player1.DisplayInfo();
            player2.DisplayInfo();

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("\n╔══════════════════════════════╗");
            Console.WriteLine($"║ Toss Winner: {currentPlayer.Name.PadRight(15)}║");
            Console.WriteLine("╚══════════════════════════════╝");

            Console.ResetColor();

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

            ConsoleUI.ShowSuccess("Game Started Successfully!");

            while (!gameOver)
            {
                DisplayBoard();

                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine($"Current Turn: {currentPlayer.Name} ({currentPlayer.Symbol})");

                Console.ResetColor();

                ConsoleUI.ShowDivider();

                Console.WriteLine();

                int move = currentPlayer.MakeMove();

                if (!board.IsPositionAvailable(move))
                {
                    ConsoleUI.ShowWarning($"Position {move} is already occupied!");
                    continue;
                }

                board.PlaceMove(move, currentPlayer.Symbol);

                if (board.CheckWinner(currentPlayer.Symbol))
                {
                    DisplayBoard();

                    ConsoleUI.ShowWinner(currentPlayer.Name);

                    gameOver = true;
                }
                else if (board.IsDraw())
                {
                    DisplayBoard();

                    ConsoleUI.ShowDraw();

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
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(
            $"\n➡ Next Turn: {currentPlayer.Name}"
            );

            Console.ResetColor();
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

        public bool IsDraw()
        {
            return isDraw;
        }
    }
}
