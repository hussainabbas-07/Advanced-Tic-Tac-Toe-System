using AdvancedTicTacToeWinForms.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdvancedTicTacToeWinForms.Utilities
{
    public static class FileManager
    {
        private static string GetProjectDataFolder()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            if (baseDir.Contains(@"\bin\Debug") || baseDir.Contains(@"\bin\Release"))
            {
                string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\"));
                return Path.Combine(projectRoot, "Data");
            }

            return Path.Combine(baseDir, "Data");
        }

        private static string historyFilePath =
            Path.Combine(GetProjectDataFolder(), "History.txt");

        private static string leaderboardFilePath =
            Path.Combine(GetProjectDataFolder(), "Leaderboard.txt");

        private static string tournamentHistoryFilePath =
            Path.Combine(GetProjectDataFolder(), "TournamentHistory.txt");

        private static string tournamentLeaderboardFilePath =
            Path.Combine(GetProjectDataFolder(), "TournamentLeaderboard.txt");

        private static void EnsureFolderAndFilesExist()
        {
            try
            {
                string folder = GetProjectDataFolder();

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                if (!File.Exists(historyFilePath))
                {
                    using (File.Create(historyFilePath)) { }
                }

                if (!File.Exists(leaderboardFilePath))
                {
                    using (File.Create(leaderboardFilePath)) { }
                }

                if (!File.Exists(tournamentHistoryFilePath))
                {
                    using (File.Create(tournamentHistoryFilePath)) { }
                }

                if (!File.Exists(tournamentLeaderboardFilePath))
                {
                    using (File.Create(tournamentLeaderboardFilePath)) { }
                }
            }
            catch
            {
            }
        }

        public static void SaveHistory(List<string> matches)
        {
            try
            {
                EnsureFolderAndFilesExist();
                File.WriteAllLines(historyFilePath, matches);
            }
            catch
            {
            }
        }

        public static List<string> LoadHistory()
        {
            try
            {
                EnsureFolderAndFilesExist();

                if (File.Exists(historyFilePath))
                {
                    return File.ReadAllLines(historyFilePath).ToList();
                }
            }
            catch
            {
            }

            return new List<string>();
        }

        public static void SaveMatch(string matchResult)
        {
            try
            {
                EnsureFolderAndFilesExist();
                File.AppendAllText(
                    historyFilePath,
                    matchResult + Environment.NewLine);
            }
            catch
            {
            }
        }

        public static void SaveLeaderboard(List<string> data)
        {
            try
            {
                EnsureFolderAndFilesExist();
                File.WriteAllLines(leaderboardFilePath, data);
            }
            catch
            {
            }
        }

        public static List<string> LoadLeaderboard()
        {
            try
            {
                EnsureFolderAndFilesExist();

                if (File.Exists(leaderboardFilePath))
                {
                    return File.ReadAllLines(leaderboardFilePath).ToList();
                }
            }
            catch
            {
            }

            return new List<string>();
        }

        public static void SaveLeaderboardEntry(string winnerName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(winnerName) ||
                    winnerName == "Draw")
                    return;

                EnsureFolderAndFilesExist();

                File.AppendAllText(
                    leaderboardFilePath,
                    winnerName + Environment.NewLine);
            }
            catch
            {
            }
        }

        public static List<PlayerStatistics> LoadPlayerStatistics()
        {
            List<PlayerStatistics> statistics = new List<PlayerStatistics>();

            try
            {
                EnsureFolderAndFilesExist();

                string[] lines = File.ReadAllLines(leaderboardFilePath);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split('|');

                    if (parts.Length != 4)
                        continue;

                    if (!int.TryParse(parts[1], out int wins))
                        continue;

                    if (!int.TryParse(parts[2], out int losses))
                        continue;

                    if (!int.TryParse(parts[3], out int draws))
                        continue;

                    PlayerStatistics player = new PlayerStatistics
                    {
                        PlayerName = parts[0],
                        Wins = wins,
                        Losses = losses,
                        Draws = draws
                    };

                    statistics.Add(player);
                }
            }
            catch
            {
            }

            return statistics;
        }

        public static void SavePlayerStatistics(
            List<PlayerStatistics> statistics)
        {
            try
            {
                EnsureFolderAndFilesExist();

                List<string> lines = new List<string>();

                foreach (PlayerStatistics player in statistics)
                {
                    if (string.IsNullOrWhiteSpace(player.PlayerName))
                        continue;

                    lines.Add(
                        $"{player.PlayerName}|{player.Wins}|{player.Losses}|{player.Draws}");
                }

                File.WriteAllLines(leaderboardFilePath, lines);
            }
            catch
            {
            }
        }

        public static void UpdatePlayerStatistics(
            string player1,
            string player2,
            string result)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(player1) ||
                    string.IsNullOrWhiteSpace(player2))
                    return;

                EnsureFolderAndFilesExist();

                List<PlayerStatistics> statistics =
                    LoadPlayerStatistics();

                PlayerStatistics player1Stats =
                    statistics.FirstOrDefault(
                        x => x.PlayerName.Equals(
                            player1,
                            StringComparison.OrdinalIgnoreCase));

                PlayerStatistics player2Stats =
                    statistics.FirstOrDefault(
                        x => x.PlayerName.Equals(
                            player2,
                            StringComparison.OrdinalIgnoreCase));

                if (player1Stats == null)
                {
                    player1Stats = new PlayerStatistics(player1);
                    statistics.Add(player1Stats);
                }

                if (player2Stats == null)
                {
                    player2Stats = new PlayerStatistics(player2);
                    statistics.Add(player2Stats);
                }

                if (result.Equals(
                    player1,
                    StringComparison.OrdinalIgnoreCase))
                {
                    player1Stats.AddWin();
                    player2Stats.AddLoss();
                }
                else if (result.Equals(
                    player2,
                    StringComparison.OrdinalIgnoreCase))
                {
                    player2Stats.AddWin();
                    player1Stats.AddLoss();
                }
                else if (result.Equals(
                    "Draw",
                    StringComparison.OrdinalIgnoreCase))
                {
                    player1Stats.AddDraw();
                    player2Stats.AddDraw();
                }

                SavePlayerStatistics(statistics);
            }
            catch
            {
            }
        }

        public static void SaveTournamentMatch(string matchResult)
        {
            try
            {
                EnsureFolderAndFilesExist();

                File.AppendAllText(
                    tournamentHistoryFilePath,
                    matchResult + Environment.NewLine);
            }
            catch
            {
            }
        }

        public static void ClearTournamentHistory()
        {
            try
            {
                EnsureFolderAndFilesExist();
                File.WriteAllText(tournamentHistoryFilePath, "");
            }
            catch
            {
            }
        }

        public static List<string> LoadTournamentHistory()
        {
            try
            {
                EnsureFolderAndFilesExist();

                if (File.Exists(tournamentHistoryFilePath))
                {
                    return File.ReadAllLines(
                        tournamentHistoryFilePath).ToList();
                }
            }
            catch
            {
            }

            return new List<string>();
        }

        public static void SaveTournamentResult(
            string tournamentName,
            string champion)
        {
            try
            {
                EnsureFolderAndFilesExist();

                string result =
                    $"TOURNAMENT: {tournamentName} | CHAMPION: {champion}";

                File.AppendAllText(
                    tournamentHistoryFilePath,
                    result + Environment.NewLine);

                File.AppendAllText(
                    tournamentLeaderboardFilePath,
                    champion + Environment.NewLine);
            }
            catch
            {
            }
        }

        public static void SaveTournamentLeaderboard(
            List<string> data)
        {
            try
            {
                EnsureFolderAndFilesExist();

                File.WriteAllLines(
                    tournamentLeaderboardFilePath,
                    data);
            }
            catch
            {
            }
        }

        public static List<string> LoadTournamentLeaderboard()
        {
            try
            {
                EnsureFolderAndFilesExist();

                if (File.Exists(tournamentLeaderboardFilePath))
                {   
                    return File.ReadAllLines(
                        tournamentLeaderboardFilePath).ToList();
                }
            }
            catch
            {
            }

            return new List<string>();
        }
    }
}
