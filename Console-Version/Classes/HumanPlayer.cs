using System;

namespace AdvancedTicTacToeGame.Classes
{
    public class HumanPlayer : Player
    {
        // Constructor
        public HumanPlayer(string name, char symbol)
            : base(name, symbol)
        {
        }

        // Overriding Abstract Method
        public override int MakeMove()
        {
            Console.Write($"\n{Name}, enter your move (1-9): ");

            int move;

            while (!int.TryParse(Console.ReadLine(), out move) || move < 1 || move > 9)
            {
                Console.Write("Invalid input! Enter a number between 1 and 9: ");
            }

            return move;
        }

        // Overriding Virtual Method
        public override void DisplayInfo()
        {
            Console.WriteLine($"Human Player: {Name} | Symbol: {Symbol}");
        }
    }
}
