using SOSGameLogic.Interfaces;

namespace SOSGameLogic.Implementation
{
    public class Board : IBoard
    {
        
        public char[,] board;
        public Board(int size)
        {
            board = new char[size, size]; // Initialize the game board with the specified size
            InitializeBoard(); // Initialize the game board with empty spaces
        }

        // Initializes the game board with empty spaces
        private void InitializeBoard()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = ' '; // Initialize all cells with empty spaces
                }
            }
        }

    
        public char[,] GetBoard()
        {
            return board;
        }

        public bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < board.GetLength(0) &&
                   col >= 0 && col < board.GetLength(1) &&
                   board[row, col] == ' ';
        }

        public void PlaceSymbol(int row, int col, char symbol)
        {
            board[row, col] = symbol;
        }

        public char GetSymbolAt(int row, int col)
        {
            return board[row, col];
        }

        public bool IsBoardFull()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == ' ')
                    {
                        return false; // If an empty cell is found, the board is not full
                    }
                }
            }
            return true; // If no empty cells are found, the board is full
        }
    }
}
