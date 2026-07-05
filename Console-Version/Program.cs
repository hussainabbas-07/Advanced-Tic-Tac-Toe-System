using System;
using AdvancedTicTacToeGame.Classes;
using AdvancedTicTacToeGame.Utilities;
using AdvancedTicTacToeGame.Enums;

namespace AdvancedTicTacToeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleUI.ShowTitle("Welcome");

            ConsoleUI.ShowDeveloperCredits();

            ConsoleUI.PressAnyKey();

            bool exit = false;

            while (!exit)
            {
                Console.Clear();

                ConsoleUI.ShowTitle("ADVANCED TIC TAC TOE SYSTEM");

                ConsoleUI.ShowMenuOption(1, "Player vs Player");
                ConsoleUI.ShowMenuOption(2, "Player vs AI");
                ConsoleUI.ShowMenuOption(3, "Tournament Mode");
                ConsoleUI.ShowMenuOption(4, "Match History");
                ConsoleUI.ShowMenuOption(5, "Leaderboard");
                ConsoleUI.ShowMenuOption(6, "Exit");

                ConsoleUI.ShowDivider();

                Console.Write("\nEnter Choice: ");

                int choice;

                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    ConsoleUI.ShowError("Please enter a valid number: ");
                }
                switch (choice)
                {
                    case 1:
                        {

                            Console.Write("\nEnter Player 1 Name: ");
                            string p1Name = Console.ReadLine();

                            Console.Write("Enter Player 2 Name: ");
                            string p2Name = Console.ReadLine();

                            Player player1 = new HumanPlayer(p1Name, 'X');
                            Player player2 = new HumanPlayer(p2Name, 'O');

                            Game game = new Game(player1, player2);

                            game.DisplayGameInfo();
                            game.StartGame();

                            Leaderboard leaderboard = new Leaderboard();

                            MatchHistory history = new MatchHistory();

                            history.Load();

                            if (game.IsDraw())
                            {
                                leaderboard.RecordDraw(player1.Name);
                                leaderboard.RecordDraw(player2.Name);

                                history.AddMatch(
                                $"[{DateTime.Now:dd-MM-yyyy hh:mm tt}] {player1.Name} vs {player2.Name} - Draw");
                            }
                            else
                            {
                                Player winner = game.GetWinner();

                                leaderboard.RecordWin(winner.Name);

                                Player loser;

                                if (winner == player1)
                                {
                                    loser = player2;
                                }
                                else
                                {
                                    loser = player1;
                                }

                                leaderboard.RecordLoss(loser.Name);

                                history.AddMatch($"[{DateTime.Now:dd-MM-yyyy hh:mm tt}] {winner.Name} defeated {loser.Name}");
                            }

                            history.Save();
                            break;
                        }

                    case 2:

                        Console.Write("\nEnter Your Name: ");
                        string playerName = Console.ReadLine();

                        Console.WriteLine("\nSelect Difficulty:");
                        Console.WriteLine("1. Easy");
                        Console.WriteLine("2. Medium");
                        Console.WriteLine("3. Hard");

                        Console.Write("\nChoice: ");

                        int diffChoice;

                        while (!int.TryParse(Console.ReadLine(), out diffChoice))
                        {
                            ConsoleUI.ShowError("Please enter a valid number: ");
                        }

                        Difficulty difficulty;

                        switch (diffChoice)
                        {
                            case 1:
                                difficulty = Difficulty.Easy;
                                break;

                            case 2:
                                difficulty = Difficulty.Medium;
                                break;

                            case 3:
                                difficulty = Difficulty.Hard;
                                break;

                            default:
                                difficulty = Difficulty.Easy;
                                break;
                        }

                        Player human = new HumanPlayer(playerName, 'X');

                        Player ai = new AIPlayer("Computer", 'O', difficulty);

                        Game aiGame = new Game(human, ai);

                        aiGame.DisplayGameInfo();
                        aiGame.StartGame();

                        break;

                    case 3:

                        Tournament tournament = new Tournament();

                        tournament.StartTournament();

                        break;

                    case 4:
                        {

                            MatchHistory history = new MatchHistory();

                            history.Load();

                            history.DisplayHistory();

                            break;
                        }
                    case 5:

                        Leaderboard lb = new Leaderboard();

                        lb.DisplayLeaderboard();

                        break;

                    case 6:

                        exit = true;

                        ConsoleUI.ShowSuccess("\nThank You For Playing!");

                        break;

                    default:

                        ConsoleUI.ShowError("\nInvalid Choice!");

                        break;
                }

                if (!exit)
                {
                    ConsoleUI.PressAnyKey();
                }
            }
        }
    }
}