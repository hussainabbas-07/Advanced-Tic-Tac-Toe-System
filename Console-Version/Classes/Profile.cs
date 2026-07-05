using System;
using AdvancedTicTacToeGame.Interfaces;

namespace AdvancedTicTacToeGame.Classes
{
    public class Profile : IStatistics
    {
        public int GetTotalGames()
        {
            return Wins + Losses + Draws;
        }

        public double GetWinPercentage()
        {
            if (GetTotalGames() == 0)
            {
                return 0;
            }

            return (double)Wins / GetTotalGames() * 100;
        }

        private string playerName;
        private int wins;
        private int losses;
        private int draws;

        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        public int Wins
        {
            get { return wins; }
            set { wins = value; }
        }

        public int Losses
        {
            get { return losses; }
            set { losses = value; }
        }

        public int Draws
        {
            get { return draws; }
            set { draws = value; }
        }

        public int TotalGames
        {
            get { return wins + losses + draws; }
        }

        public Profile(string playerName)
        {
            PlayerName = playerName;
            Wins = 0;
            Losses = 0;
            Draws = 0;
        }

        public void AddWin()
        {
            Wins++;
        }

        public void AddLoss()
        {
            Losses++;
        }

        public void AddDraw()
        {
            Draws++;
        }

        public void DisplayProfile()
        {
            Console.WriteLine("\n===== PLAYER PROFILE =====");

            Console.WriteLine($"Name        : {PlayerName}");
            Console.WriteLine($"Wins        : {Wins}");
            Console.WriteLine($"Losses      : {Losses}");
            Console.WriteLine($"Draws       : {Draws}");
            Console.WriteLine($"Total Games : {TotalGames}");
        }
    }
}
