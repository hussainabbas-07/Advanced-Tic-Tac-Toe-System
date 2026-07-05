using AdvancedTicTacToeGame.Interfaces;
using AdvancedTicTacToeGame.Utilities;
using System;
using System.Collections.Generic;

namespace AdvancedTicTacToeGame.Classes
{
    public class MatchHistory : ISaveable
    {
        public void Save()
        {
            FileManager.SaveHistory(matches);
        }

        public void Load()
        {
            matches = FileManager.LoadHistory();
        }

        private List<string> matches;

        public MatchHistory()
        {
            matches = new List<string>();
        }

        public void AddMatch(string record)
        {
            matches.Add(record);
        }

        public void DisplayHistory()
        {
            ConsoleUI.ShowTitle("MATCH HISTORY");

            if (matches.Count == 0)
            {
                Console.WriteLine("No matches played yet.");
                return;
            }

            foreach (string match in matches)
            {
                Console.WriteLine(match);
            }
        }

        public List<string> GetMatches()
        {
            return matches;
        }
    }
}
