using System;
using System.Collections.Generic;

namespace AdvancedTicTacToeGame.Classes
{
    public class Board
    {
        private char[] cells;

        public Board()
        {
            cells = new char[9];

            for (int i = 0; i < 9; i++)
            {
                cells[i] = (char)(i + '1');
            }
        }

        private void PrintCell(char cell)
        {
            if (cell == 'X')
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (cell == 'O')
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Write(cell);

            Console.ResetColor();
        }
        public void DisplayBoard()
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine("╔═══╦═══╦═══╗");

            Console.Write("║ ");
            PrintCell(cells[0]);
            Console.Write(" ║ ");

            PrintCell(cells[1]);
            Console.Write(" ║ ");

            PrintCell(cells[2]);
            Console.WriteLine(" ║");

            Console.WriteLine("╠═══╬═══╬═══╣");

            Console.Write("║ ");
            PrintCell(cells[3]);
            Console.Write(" ║ ");

            PrintCell(cells[4]);
            Console.Write(" ║ ");

            PrintCell(cells[5]);
            Console.WriteLine(" ║");

            Console.WriteLine("╠═══╬═══╬═══╣");

            Console.Write("║ ");
            PrintCell(cells[6]);
            Console.Write(" ║ ");

            PrintCell(cells[7]);
            Console.Write(" ║ ");

            PrintCell(cells[8]);
            Console.WriteLine(" ║");

            Console.WriteLine("╚═══╩═══╩═══╝");

            Console.ResetColor();

            Console.WriteLine();
        }

        public bool IsPositionAvailable(int position)
        {
            return cells[position - 1] != 'X' &&
                   cells[position - 1] != 'O';
        }

        public List<int> GetAvailableMoves()
        {
            List<int> moves = new List<int>();

            for (int i = 1; i <= 9; i++)
            {
                if (IsPositionAvailable(i))
                {
                    moves.Add(i);
                }
            }

            return moves;
        }

        public void PlaceMove(int position, char symbol)
        {
            cells[position - 1] = symbol;
        }

        public bool CheckWinner(char symbol)
        {
            return

            (cells[0] == symbol && cells[1] == symbol && cells[2] == symbol) ||

            (cells[3] == symbol && cells[4] == symbol && cells[5] == symbol) ||

            (cells[6] == symbol && cells[7] == symbol && cells[8] == symbol) ||

            (cells[0] == symbol && cells[3] == symbol && cells[6] == symbol) ||

            (cells[1] == symbol && cells[4] == symbol && cells[7] == symbol) ||

            (cells[2] == symbol && cells[5] == symbol && cells[8] == symbol) ||

            (cells[0] == symbol && cells[4] == symbol && cells[8] == symbol) ||

            (cells[2] == symbol && cells[4] == symbol && cells[6] == symbol);
        }

        public bool IsDraw()
        {
            foreach (char cell in cells)
            {
                if (cell != 'X' && cell != 'O')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
