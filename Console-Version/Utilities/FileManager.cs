using System;
using System.IO;

namespace AdvancedTicTacToeGame.Utilities
{
    public static class FileManager
    {
        private static readonly string dataFolder = "Data";

        private static void EnsureDataFolderExists()
        {
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }
        }
        public static void SaveText(string filePath, string data)
        {
            try
            {
                File.WriteAllText(filePath, data);

                Console.WriteLine("Data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static string LoadText(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }

                return "File not found.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public static void SaveHistory(List<string> matches)
        {
            try
            {
                EnsureDataFolderExists();

                File.WriteAllLines(
                    Path.Combine(dataFolder, "history.txt"), matches);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static List<string> LoadHistory()
        {
            try
            {
                string path = Path.Combine(dataFolder, "history.txt");
                if (File.Exists(path))
                {
                    return new List<string>(
                        File.ReadAllLines(path)
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return new List<string>();
        }

        public static void SaveLeaderboard(List<string> data)
        {
            try
            {
                EnsureDataFolderExists();
                File.WriteAllLines(
                Path.Combine(dataFolder, "leaderboard.txt"),
                data
                     );
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static List<string> LoadLeaderboard()
        {
            try
            {
                string path = Path.Combine(dataFolder, "leaderboard.txt");

                if (File.Exists(path))
                {
                    return new List<string>(
                        File.ReadAllLines(path)
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return new List<string>();
        }
    }
}
