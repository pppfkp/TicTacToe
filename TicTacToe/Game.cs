using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Game
    {
        private readonly Board _board;
        private TicTacToe _currentPlayer;
        public int RoundCount { get; private set; }
        public Game(int dimensions, TicTacToe startingPlayer)
        {
            _board = new(dimensions);
            _currentPlayer = startingPlayer;
            RoundCount = 0;
            //_board.MakeAMove(0, 0, TicTacToe.X);
            //_board.MakeAMove(0, 1, TicTacToe.X);
            //_board.MakeAMove(0, 2, TicTacToe.O);
            //_board.MakeAMove(2, 0, TicTacToe.O);
            //_board.MakeAMove(2, 1, TicTacToe.O);
            //_board.MakeAMove(1, 0, TicTacToe.X);
            //_board.MakeAMove(1, 1, TicTacToe.X);
            //_board.Draw();
        }

        private TicTacToe TogglePlayer()
        {
            if (_currentPlayer == TicTacToe.O)
            {
                _currentPlayer = TicTacToe.X;
            } else
            {
                _currentPlayer = TicTacToe.O;
            }
            return _currentPlayer;
        }


        private void TryMakeAMove(int row, int column)
        {
            if (!MakeAMove(row,column))
            {
                Console.WriteLine("You can't make that move");
                return;
            }
            
        }
        private bool MakeAMove(int row, int column)
        {
            if (_board.NumberOfRows - 1 < row || _board.NumberOfRows - 1 < column )
            {
                return false;
            }

            if (_board._board[row,column] != TicTacToe.Empty)
            {
                return false;
            }
            _board.MakeAMove(row, column, _currentPlayer);
            TogglePlayer();
            ++RoundCount;
            return true;
        }

        //Winner checking implemented by using strings
        public TicTacToe CheckWinner()
        {
            var board = _board.ToString();
            //Console.WriteLine($"horizontal: {board}");
            
            foreach (var chunk in board.Chunk(_board.NumberOfRows))
            {
                string str = new(chunk);
                //Console.WriteLine(str);
                if (str.Contains("OOO"))
                {
                    return TicTacToe.O;
                }
                if (str.Contains("XXX"))
                {
                    return TicTacToe.X;
                }
            }

            char[] verticalBoard = new char[board.Length];

            for (int i = 0; i < board.Length; i++)
            {
                verticalBoard[(_board.NumberOfRows * (i % _board.NumberOfRows)) + (i / _board.NumberOfRows)] = board[i];
            }

            //Console.WriteLine($"vertical: {new string(verticalBoard)}");

            foreach (var chunk in verticalBoard.Chunk(_board.NumberOfRows))
            {
                string str = new(chunk);
                //Console.WriteLine(str);
                if (str.Contains("OOO"))
                {
                    return TicTacToe.O;
                }
                if (str.Contains("XXX"))
                {
                    return TicTacToe.X;
                }
            }


            char[] diagonalBoard = new char[_board.NumberOfRows];

            for (int i = 0; i < _board.NumberOfRows; i++)
            {
                diagonalBoard[i] = board[(_board.NumberOfRows * i) + i];
            }

            string strDiagonal = new string(diagonalBoard);
            //Console.WriteLine($"first diagonal: {strDiagonal}");

            if (strDiagonal.Contains("OOO"))
            {
                return TicTacToe.O;
            }
            if (strDiagonal.Contains("XXX"))
            {
                return TicTacToe.X;
            }

            diagonalBoard = new char[_board.NumberOfRows];

            for (int i = 0; i < _board.NumberOfRows; i++)
            {
                diagonalBoard[i] = board[(_board.NumberOfRows * i) + (_board.NumberOfRows - 1 - i)];
            }

            strDiagonal = new string(diagonalBoard);
            //Console.WriteLine($"second diagonal: {strDiagonal}");

            if (strDiagonal.Contains("OOO"))
            {
                return TicTacToe.O;
            }
            if (strDiagonal.Contains("XXX"))
            {
                return TicTacToe.X;
            }


            return TicTacToe.Empty;
        }

        public void PlayTraditional()
        {
            RoundCount = 0;
            _board.Clear();

            Console.WriteLine("Wybierz gracza rozpoczynającego: X lub O: ");
            string startingPlayer = Console.ReadLine();
            
            if (startingPlayer == "X")
            {
                _currentPlayer = TicTacToe.X;
            } else if (startingPlayer == "O") 
            {
                _currentPlayer = TicTacToe.O;
            }

            while (RoundCount != (_board.NumberOfFields))
            {
                _board.Draw();
                var winner = CheckWinner();
                if (winner == TicTacToe.X)
                {
                    Console.WriteLine("Gratulacje X, wygrałeś!");
                    return;
                } else if (winner == TicTacToe.O)
                {
                    Console.WriteLine("Gratulacje O, wygrałeś!");
                    return;
                }
                Console.WriteLine($"Gracz: {_currentPlayer} Wykonaj ruch");
                Console.WriteLine("Rzad: ");
                int.TryParse(Console.ReadLine(), out int row);
                Console.WriteLine("Kolumna: ");
                int.TryParse(Console.ReadLine(), out int column);
                TryMakeAMove(row, column);
            }

            _board.Draw();

            if (CheckWinner() == TicTacToe.X)
            {
                Console.WriteLine("Gratulacje X, wygrałeś!");
                return;
            }
            else if (CheckWinner() == TicTacToe.O)
            {
                Console.WriteLine("Gratulacje O, wygrałeś!");
                return;
            } else
            {
                Console.WriteLine("Remis");
            }
        }
    }
}
