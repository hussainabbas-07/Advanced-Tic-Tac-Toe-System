using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedTicTacToeWinForms.Classes
{
    public class Tournament
    {
        public string TournamentName { get; set; }

        public List<string> Players { get; set; }

        public List<TournamentMatch> Matches { get; set; }

        public string Champion { get; private set; }

        public Tournament()
        {
            Players = new List<string>();
            Matches = new List<TournamentMatch>();
        }

        public Tournament(string tournamentName)
        {
            TournamentName = tournamentName;
            Players = new List<string>();
            Matches = new List<TournamentMatch>();
        }
        public void AddPlayer(string playerName)
        {
            if (!string.IsNullOrWhiteSpace(playerName))
            {
                Players.Add(playerName.Trim());
            }
        }

        public void ShufflePlayers()
        {
            Random random = new Random();

            Players = Players
                .OrderBy(x => random.Next())
                .ToList();
        }
        public void GenerateFirstRound()
        {
            Matches.Clear();

            int matchNumber = 1;

            for (int i = 0; i < Players.Count; i += 2)
            {
                string player1 = Players[i];

                string player2 = (i + 1 < Players.Count)
                    ? Players[i + 1]
                    : "BYE";

                TournamentMatch match = new TournamentMatch
                {
                    MatchNumber = matchNumber,
                    RoundNumber = 1,
                    Player1 = player1,
                    Player2 = player2,
                    Winner = player2 == "BYE" ? player1 : ""
                };

                Matches.Add(match);

                matchNumber++;
            }
        }

        public List<TournamentMatch> GetRoundMatches(int round)
        {
            return Matches
                .Where(m => m.RoundNumber == round)
                .ToList();
        }

        public void SetWinner(int matchNumber, string winner)
        {
            TournamentMatch match = Matches
                .FirstOrDefault(m => m.MatchNumber == matchNumber);

            if (match != null)
            {
                match.Winner = winner;
            }
        }

        public void SetChampion(string champion)
        {
            Champion = champion;
        }
    }


    public class TournamentMatch
    {
        public int MatchNumber { get; set; }

        public int RoundNumber { get; set; }

        public string Player1 { get; set; }

        public string Player2 { get; set; }

        public string Winner { get; set; }

        public bool IsCompleted
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Winner);
            }
        }
    }
}
