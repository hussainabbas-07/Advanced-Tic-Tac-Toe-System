using System;

namespace AdvancedTicTacToeWinForms.Classes
{
    public class PlayerStatistics
    {
        public string PlayerName { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

        public int TotalGames
        {
            get
            {
                return Wins + Losses + Draws;
            }
        }

        public double WinRate
        {
            get
            {
                if (TotalGames == 0)
                    return 0;

                return (double)Wins / TotalGames * 100;
            }
        }

        public PlayerStatistics()
        {
        }

        public PlayerStatistics(string playerName)
        {
            PlayerName = playerName;
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
    }
}
