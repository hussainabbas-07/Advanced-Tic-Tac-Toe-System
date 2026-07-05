using System;
using AdvancedTicTacToeGame.Enums;

namespace AdvancedTicTacToeGame.Classes
{
    public class AIPlayer : Player
    {
        public Difficulty DifficultyLevel { get; set; }

        private Random random = new Random();

        public AIPlayer(string name, char symbol, Difficulty difficulty)
            : base(name, symbol)
        {
            DifficultyLevel = difficulty;
        }

        public override int MakeMove()
        {
            int move;

            switch (DifficultyLevel)
            {
                case Difficulty.Easy:

                    move = random.Next(1, 10);

                    break;

                case Difficulty.Medium:

                    move = random.Next(1, 10);

                    break;

                case Difficulty.Hard:

                    move = random.Next(1, 10);

                    break;

                default:

                    move = random.Next(1, 10);

                    break;
            }

            Console.WriteLine($"\n{Name} selected position {move}");

            return move;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"AI Player: {Name} | Symbol: {Symbol} | Difficulty: {DifficultyLevel}");
        }
    }
}
