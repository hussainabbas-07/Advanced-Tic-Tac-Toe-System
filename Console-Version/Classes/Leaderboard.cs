using System;
using System.Collections.Generic;
using AdvancedTicTacToeGame.Interfaces;
using AdvancedTicTacToeGame.Utilities;

namespace AdvancedTicTacToeGame.Classes
{
    public class Leaderboard : ISaveable
    {
        public void Save()
        {
            List<string> data = new List<string>();

            foreach (Profile profile in profiles)
            {
                data.Add(
                    $"{profile.PlayerName},{profile.Wins},{profile.Losses},{profile.Draws}"
                );
            }

            FileManager.SaveLeaderboard(data);
        }

        public void Load()
        {
            TotalPlayers = 0;
            List<string> data = FileManager.LoadLeaderboard();

            profiles.Clear();

            foreach (string line in data)
            {
                string[] parts = line.Split(',');

                Profile profile = new Profile(parts[0]);

                profile.Wins = int.Parse(parts[1]);
                profile.Losses = int.Parse(parts[2]);
                profile.Draws = int.Parse(parts[3]);

                profiles.Add(profile);
                TotalPlayers++;
            }
        }

        private List<Profile> profiles;

        public static int TotalPlayers = 0;

        public Leaderboard()
        {
            profiles = new List<Profile>();
            Load();
        }

        public void AddProfile(Profile profile)
        {
            profiles.Add(profile);

            TotalPlayers++;
        }

        public void SortLeaderboard()
        {
            profiles.Sort((p1, p2) => p2.Wins.CompareTo(p1.Wins));
        }

        public void DisplayLeaderboard()
        {
            SortLeaderboard();

            ConsoleUI.ShowTitle("LEADERBOARD");

            Console.WriteLine($"Total Players: {TotalPlayers}\n");

            int rank = 1;

            foreach (Profile profile in profiles)
            {
                Console.WriteLine(
                    $"{rank}. {profile.PlayerName} | Wins: {profile.Wins} | Losses: {profile.Losses} | Draws: {profile.Draws}"
                );

                rank++;
            }
        }

        public Profile FindProfile(string playerName)
        {
            foreach (Profile profile in profiles)
            {
                if (profile.PlayerName == playerName)
                {
                    return profile;
                }
            }

            return null;
        }

        public void RecordWin(string winnerName)
        {
            Profile profile = FindProfile(winnerName);

            if (profile == null)
            {
                profile = new Profile(winnerName);

                profiles.Add(profile);

                TotalPlayers++;
            }

            profile.AddWin();

            Save();
        }

        public void RecordLoss(string loserName)
        {
            Profile profile = FindProfile(loserName);

            if (profile == null)
            {
                profile = new Profile(loserName);

                profiles.Add(profile);

                TotalPlayers++;
            }

            profile.AddLoss();

            Save();
        }

        public void RecordDraw(string playerName)
        {
            Profile profile = FindProfile(playerName);

            if (profile == null)
            {
                profile = new Profile(playerName);

                profiles.Add(profile);

                TotalPlayers++;
            }

            profile.AddDraw();

            Save();
        }

        public Profile GetTopPlayer()
        {
            if (profiles.Count == 0)
            {
                return null;
            }

            SortLeaderboard();

            return profiles[0];
        }
    }
}
