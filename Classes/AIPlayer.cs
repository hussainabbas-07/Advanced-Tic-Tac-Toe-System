using System;
using System.Collections.Generic;
using AdvancedTicTacToeWinForms.Enums;

namespace AdvancedTicTacToeWinForms.Classes
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

        public int GetBestMove(char[] board, char humanSymbol)
        {
            List<int> availableMoves = GetAvailableMoves(board);
            if (availableMoves.Count == 0) return -1;

            switch (DifficultyLevel)
            {
                case Difficulty.Easy:
                    return GetEasyMove(availableMoves);

                case Difficulty.Medium:
                    return GetMediumMove(board, availableMoves, humanSymbol);

                case Difficulty.Hard:
                    return GetHardMove(board, availableMoves, humanSymbol);

                default:
                    return GetEasyMove(availableMoves);
            }
        }

        private int GetEasyMove(List<int> availableMoves)
        {
            int index = random.Next(0, availableMoves.Count);
            return availableMoves[index];
        }

        private int GetMediumMove(char[] board, List<int> availableMoves, char humanSymbol)
        {
            foreach (int move in availableMoves)
            {
                board[move] = this.Symbol;
                if (IsWinning(board, this.Symbol))
                {
                    board[move] = ' ';
                    return move;
                }
                board[move] = ' ';
            }

            foreach (int move in availableMoves)
            {
                board[move] = humanSymbol;
                if (IsWinning(board, humanSymbol))
                {
                    board[move] = ' ';
                    return move;
                }
                board[move] = ' ';
            }

            return GetEasyMove(availableMoves);
        }

        private int GetHardMove(char[] board, List<int> availableMoves, char humanSymbol)
        {
            int winningOrBlocking = GetMediumMove(board, availableMoves, humanSymbol);

            board[winningOrBlocking] = this.Symbol;
            bool isWin = IsWinning(board, this.Symbol);
            board[winningOrBlocking] = humanSymbol;
            bool isBlock = IsWinning(board, humanSymbol);
            board[winningOrBlocking] = ' ';

            if (isWin || isBlock) return winningOrBlocking;

            if (availableMoves.Contains(4)) return 4;

            List<int> corners = new List<int> { 0, 2, 6, 8 };
            List<int> freeCorners = availableMoves.FindAll(m => corners.Contains(m));
            if (freeCorners.Count > 0)
            {
                return freeCorners[random.Next(0, freeCorners.Count)];
            }

            return GetEasyMove(availableMoves);
        }

        private List<int> GetAvailableMoves(char[] board)
        {
            List<int> moves = new List<int>();
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == ' ' || board[i] == '\0')
                    moves.Add(i);
            }
            return moves;
        }

        private bool IsWinning(char[] b, char mark)
        {
            int[,] winPatterns = new int[,]
            {
                {0,1,2}, {3,4,5}, {6,7,8},
                {0,3,6}, {1,4,7}, {2,5,8},
                {0,4,8}, {2,4,6}
            };

            for (int i = 0; i < 8; i++)
            {
                if (b[winPatterns[i, 0]] == mark &&
                    b[winPatterns[i, 1]] == mark &&
                    b[winPatterns[i, 2]] == mark)
                    return true;
            }
            return false;
        }

        public override int MakeMove()
        {
            return random.Next(1, 10);
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"AI Player: {Name} | Symbol: {Symbol} | Difficulty: {DifficultyLevel}");
        }
    }
}