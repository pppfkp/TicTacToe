using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Board
    {
        public readonly TicTacToe[,] _board;
        public Board(int size)
        {
            _board = new TicTacToe[size, size];
            Clear();
        }
        public int NumberOfRows { get => _board.GetLength(0); }
        public int NumberOfFields { get => _board.Length; }
        public void Clear()
        {
            for (int i = 0; i < NumberOfRows; i++)
            {
                for(int j = 0; j < NumberOfRows; j++)
                {
                    _board[i, j] = TicTacToe.Empty;
                }
            }
        }
        public void MakeAMove(int row, int column, TicTacToe element)
        {
            try
            {
                _board[row, column] = element;
            } 
            catch (IndexOutOfRangeException e) 
            {
                throw new IndexOutOfRangeException($"The board doesn't have a field with index [{row},{column}]");
            }
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in _board)
            {
                if (item == TicTacToe.X)
                {
                    sb.Append('X');
                }
                else if (item == TicTacToe.O)
                {
                    sb.Append('O');
                }
                else
                {
                    sb.Append(' ');
                }
            }
            return sb.ToString();
        }

        public void Draw()
        {
            Console.Write("   ");
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                Console.Write($" {j} ");
            }
            Console.WriteLine();
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                Console.Write($" {i} ");
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    var character = _board[i, j] switch
                    {
                        TicTacToe.O => "O",
                        TicTacToe.X => "X",
                        _ => " "
                    };
                    Console.Write($"[{character}]");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
