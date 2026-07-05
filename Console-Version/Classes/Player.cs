using System;

namespace AdvancedTicTacToeGame.Classes
    {
        public abstract class Player
        {
            // Encapsulation (Private Fields)
            private string name;
            private char symbol;

            // Properties
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public char Symbol
            {
                get { return symbol; }
                set { symbol = value; }
            }

            // Constructor
            public Player(string name, char symbol)
            {
                Name = name;
                Symbol = symbol;
            }

        // Abstract Method (Abstraction)
        public abstract int MakeMove();


        // Virtual Method (Polymorphism k liye)
        public virtual void DisplayInfo()
            {
                Console.WriteLine($"Player: {Name} | Symbol: {Symbol}");
            }
        }
    }

