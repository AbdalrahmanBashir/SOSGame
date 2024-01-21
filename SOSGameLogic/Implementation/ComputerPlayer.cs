using SOSGameLogic.Interfaces;
using System;
using System.Collections.Generic;


namespace SOSGameLogic.Implementation
{
    public class ComputerPlayer : BasePlayer
    {
        // Private variable to store the player symbol
        private char playerSymbol;

        // Constructor
        public ComputerPlayer()
        {
        }

        // Generate a random position on the board
        internal Tuple<int, int> GenerateRandomPosition(IBoard board)
        {
            var random = new Random();
            int numRows = board.GetBoard().GetLength(0);
            int numCols = board.GetBoard().GetLength(1);

            do
            {
                int randomRow = random.Next(numRows);
                int randomCol = random.Next(numCols);
                if (board.GetBoard()[randomRow, randomCol] == ' ')
                {
                    return Tuple.Create(randomRow, randomCol);
                }
            } while (true);
        }

        // Generate an intelligent move for the computer player
        public Tuple<int, int> GenerateIntelligentMove(IBoard board)
        {
            char[,] gameBoard = board.GetBoard();
            int numRows = gameBoard.GetLength(0);
            int numCols = gameBoard.GetLength(1);
            int row, col;

            // Check for potential SOS formations and block them
            for (row = 0; row < numRows; row++)
            {
                for (col = 0; col < numCols; col++)
                {
                    if (gameBoard[row, col] == ' ')
                    {
                        if (CheckHorizontalToTheLeft(gameBoard, row, col) ||
                            CheckHorizontalToTheRight(gameBoard, row, col) ||
                            CheckVerticalDownWard(gameBoard, row, col) ||
                            CheckVerticalUpward(gameBoard, row, col) ||
                            CheckDiagonalBottomLeftToRight(gameBoard, row, col) ||
                            CheckDiagonalBottomRightToTopLeft(gameBoard, row, col) ||
                            CheckDiagonalTopLeftToBottomRight(gameBoard, row, col) ||
                            CheckDiagonalTopRightToBottomLeft(gameBoard, row, col))
                        {
                            return Tuple.Create(row, col);
                        }
                    }
                }
            }

            // If no potential SOS to block, find the first empty cell
            Tuple<int, int> computerMove = GenerateRandomPosition(board);
            return Tuple.Create(computerMove.Item1, computerMove.Item2);
        }

        // Check for SOS formations horizontally to the left
        internal bool CheckHorizontalToTheLeft(char[,] board, int row, int col)
        {
            int boardWidth = board.GetLength(1);

            if ((col >= 2 && col <= boardWidth - 1) && (board[row, col] == ' ') &&
                (board[row, col - 1] == 'O') && (board[row, col - 2] == 'S'))
            {
                return true;
            }

            return false;
        }

        // Check for SOS formations horizontally to the right
        internal bool CheckHorizontalToTheRight(char[,] board, int row, int col)
        {
            int boardWidth = board.GetLength(1);
            int miniCellToTheRight = boardWidth - 2;

            if ((col < miniCellToTheRight) && (board[row, col] == ' ') &&
                (board[row, col + 1] == 'O') && (board[row, col + 2] == 'S'))
            {
                return true;
            }

            return false;
        }

        // Check for SOS formations vertically downward
        internal bool CheckVerticalDownWard(char[,] board, int row, int col)
        {
            int boardHeight = board.GetLength(0);
            int miniCellToTheTop = boardHeight - 2;

            if (row < miniCellToTheTop && row + 2 < boardHeight &&
                board[row, col] == ' ' &&
                board[row + 1, col] == 'O' &&
                board[row + 2, col] == 'S')
            {
                return true;
            }

            return false;
        }

        // Check for SOS formations vertically upward
        internal bool CheckVerticalUpward(char[,] board, int row, int col)
        {
            if ((row >= 2) && (board[row, col] == ' ') &&
                (board[row - 1, col] == 'O') && (board[row - 2, col] == 'S'))
            {
                return true;
            }

            return false;
        }

        // Check for SOS formations diagonally from top-left to bottom-right
        internal bool CheckDiagonalTopLeftToBottomRight(char[,] board, int row, int col)
        {
            int count = 0;
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);
            if (row >= 0 && col >= 0 &&
                row + 2 < boardHeight && col + 2 < boardWidth)
            {
                if (board[row, col] == ' ' &&
                    board[row + 1, col + 1] == 'O' &&
                    board[row + 2, col + 2] == 'S')
                {
                    count = 3;
                }
            }

            return count >= 3;
        }

        // Check for SOS formations diagonally from top-right to bottom-left
        internal bool CheckDiagonalTopRightToBottomLeft(char[,] board, int row, int col)
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);
            if (row >= 0 && col >= 2 && col <= boardWidth - 1 && row + 2 <= boardHeight - 1)
            {
                if (board[row, col] == ' ' && board[row + 1, col - 1] == 'O' && board[row + 2, col - 2] == 'S')
                {
                    return true;
                }
            }

            return false;
        }

        // Check for SOS formations diagonally from bottom-right to top-left
        internal bool CheckDiagonalBottomRightToTopLeft(char[,] board, int row, int col)
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);
            if (row >= 2 && col >= 2 && col <= boardWidth - 1 && row <= boardHeight - 1)
            {
                if (board[row, col] == ' ' && board[row - 1, col - 1] == 'O' && board[row - 2, col - 2] == 'S')
                {
                    return true;
                }
            }

            return false;
        }

        // Check for SOS formations diagonally from bottom-left to top-right
        internal bool CheckDiagonalBottomLeftToRight(char[,] board, int row, int col)
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);

            // Check if there are enough cells to the bottom left to form an SOS
            if (row >= 2 && col >= 2 && row <= boardHeight - 1 && col <= boardWidth - 1)
            {
                // Check for SOS formation
                if (board[row, col] == ' ' &&
                    board[row - 1, col - 1] == 'O' &&
                    board[row - 2, col - 2] == 'S')
                {
                    return true; // SOS formation detected
                }
            }

            return false; // No SOS formation detected
        }

        // Make a move on the board
        public override void MakeMove(IBoard board, int row, int col)
        {
            playerSymbol = GenerateRandomSymbol();
            board.PlaceSymbol(row, col, playerSymbol);
        }

        // Get the player symbol
        public override char GetPlayerSymbol()
        {
            return playerSymbol;
        }

        // Generate a random player symbol ('S' or 'O')
        internal char GenerateRandomSymbol()
        {
            return (new Random()).Next(2) == 0 ? 'S' : 'O';
        }

        // Set the player symbol (not implemented)
        public override void SetPlayerSymbol(char symbol)
        {
            throw new NotImplementedException();
        }
    }
}
