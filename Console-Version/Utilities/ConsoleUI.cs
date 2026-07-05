using System;

namespace AdvancedTicTacToeGame.Utilities
{
    public static class ConsoleUI
    {
        public static void ShowTitle(string title)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║          ADVANCED TIC TAC TOE SYSTEM        ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"\n               {title}");

            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("\n════════════════════════════════════════════════");

            Console.ResetColor();
        }

        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void ShowMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ResetColor();
        }

        public static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"\n✓ {message}");

            Console.ResetColor();
        }

        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"\n✗ {message}");

            Console.ResetColor();
        }

        public static void ShowWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"\n⚠ {message}");

            Console.ResetColor();
        }

        public static void ShowMenuOption(int number, string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write($" [{number}] ");

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(text);

            Console.ResetColor();
        }

        public static void ShowDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("------------------------------------");

            Console.ResetColor();
        }

        public static void PressAnyKey()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("\nPress Any Key To Continue...");

            Console.ResetColor();

            Console.ReadKey();
        }

        public static void ShowWinner(string winnerName)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine($"║  🏆 WINNER : {winnerName.PadRight(18)}║");
            Console.WriteLine("╚══════════════════════════════════════╝");

            Console.ResetColor();
        }

        public static void ShowDraw()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║           MATCH DRAW!            ║");
            Console.WriteLine("╚════════════════════════════════════╝");

            Console.ResetColor();
        }

        public static void ShowHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("╔══════════════════════════════════╗");
            Console.WriteLine($"║ {title.PadRight(32)} ║");
            Console.WriteLine("╚══════════════════════════════════╝");

            Console.ResetColor();
        }

        public static void ShowFooter()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("\n════════════════════════════════════");

            Console.ResetColor();
        }

        public static void ShowDeveloperCredits()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine("\n╔══════════════════════════════════════════════╗");
            Console.WriteLine("║              PROJECT DEVELOPERS             ║");
            Console.WriteLine("╠══════════════════════════════════════════════╣");
            Console.WriteLine("║ Leader : Syed Hussain Abbas (CS251334)      ║");
            Console.WriteLine("║ Member : Anumta Khan (CS251006)             ║");
            Console.WriteLine("║ Instructor : Miss Durr-e-Shehwar            ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");

            Console.ResetColor();
        }
    }
}
