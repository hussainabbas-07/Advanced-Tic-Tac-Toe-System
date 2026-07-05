using AdvancedTicTacToeGame.Utilities;
using System;

namespace AdvancedTicTacToeGame.Classes
{
    public class Tournament
    {
        private Leaderboard leaderboard;
        private MatchHistory history;

        public Tournament()
        {
            leaderboard = new Leaderboard();
            history = new MatchHistory();
            history.Load();
        }

        public void StartTournament()
        {
            Console.Write("Enter number of rounds: ");

            int rounds;

            while (!int.TryParse(Console.ReadLine(), out rounds) || rounds <= 0)
            {
                Console.Write("Enter a valid number of rounds: ");
            }

            Console.Write("Enter Player 1 Name: ");
            string name1 = Console.ReadLine();

            Console.Write("Enter Player 2 Name: ");
            string name2 = Console.ReadLine();

            Player player1 = new HumanPlayer(name1, 'X');
            Player player2 = new HumanPlayer(name2, 'O');

            for (int i = 1; i <= rounds; i++)
            {
                ConsoleUI.ShowTitle($"ROUND {i}");

                Game game = new Game(player1, player2);

                game.StartGame();

                if (game.IsDraw())
                {
                    leaderboard.RecordDraw(player1.Name);
                    leaderboard.RecordDraw(player2.Name);

                    history.AddMatch(
                        $"Round {i}: {player1.Name} vs {player2.Name} - Draw"
                    );
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

                    history.AddMatch(
                        $"Round {i}: {winner.Name} defeated {loser.Name}"
                    );
                }

                history.Save();
            }

            Console.WriteLine("\nTournament Finished!");

            leaderboard.Save();
        }
    }
}
