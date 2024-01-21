using SOSGameLogic.Interfaces;

namespace SOSGameLogic.Implementation
{
    public abstract class BaseGameMode : IBaseGameMode
    {
        internal bool CheckHorizontalToTheLeft(char[,] board, int row, int col)
        {

            int boardWidth = board.GetLength(1);

            if ((col >= 2 && col <= boardWidth - 1) && (board[row, col] == 'S') &&
               (board[row, col - 1] == 'O') && (board[row, col - 2] == 'S'))

            {
                return true;
            }
            if (col >= 1 && col <= boardWidth - 2 && board[row, col] == 'O' && board[row, col - 1] == 'S' && board[row, col + 1] == 'S')
            {
                return true;
            }

            return false;
        }
        internal bool CheckHorizontalToTheRight(char[,] board, int row, int col)
        {
            int boardWidth = board.GetLength(1);
            int miniCellToTheRight = boardWidth - 2;

            if ((col < miniCellToTheRight) && (board[row, col] == 'S') &&
                (board[row, col + 1] == 'O') && (board[row, col + 2] == 'S'))
            {
                return true;
            }

            return false;
        }
        internal bool CheckVerticalDownWard(char[,] board, int row, int col)
        {

            int boardHeight = board.GetLength(0);
            int miniCellToTheTop = boardHeight - 2;

            // Check for potential SOS formations vertically
            if (row < miniCellToTheTop && row + 2 < boardHeight &&
                board[row, col] == 'S' &&
                board[row + 1, col] == 'O' &&
                board[row + 2, col] == 'S')
            {
                return true; // SOS formation detected
            }

            if (row >= 1 && row <= boardHeight - 1 &&
                row - 1 >= 0 && row + 1 < boardHeight &&
                board[row, col] == 'O' &&
                board[row - 1, col] == 'S' &&
                board[row + 1, col] == 'S')
            {
                return true; // SOS formation detected
            }

            return false; // No SOS formation detected

        }
        internal bool CheckVerticalUpward(char[,] board, int row, int col)
        {


            if ((row >= 2) && (board[row, col] == 'S') &&
                (board[row - 1, col] == 'O') && (board[row - 2, col] == 'S'))
            {
                return true;

            }

            return false;

        }
        internal bool CheckDiagonalTopLeftToBottomRight(char[,] board, int row, int col)
        {
            int count = 0;
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);
            if (row >= 0 && col >= 0 &&
                row + 2 < boardHeight && col + 2 < boardWidth)
            {
                if (board[row, col] == 'S' &&
                    board[row + 1, col + 1] == 'O' &&
                    board[row + 2, col + 2] == 'S')
                {
                    count = 3;
                }
            }

            return count >= 3;
        }
        internal bool CheckDiagonalTopRightToBottomLeft(char[,] board, int row, int col)
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);
            if (row >= 0 && col >= 2 && col <= boardWidth - 1 && row + 2 <= boardHeight - 1)
            {
                if (board[row, col] == 'S' && board[row + 1, col - 1] == 'O' && board[row + 2, col - 2] == 'S')
                {
                    return true;
                }
            }

            return false;
        }
        internal bool CheckDiagonalBottomRightToTopLeft(char[,] board, int row, int col)
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);
            if (row >= 2 && col >= 2 && col <= boardWidth - 1 && row <= boardHeight - 1)
            {
                if (board[row, col] == 'S' && board[row - 1, col - 1] == 'O' && board[row - 2, col - 2] == 'S')
                {
                    return true;
                }
            }

            return false;
        }

        internal bool CheckDiagonalBottomLeftToRight(char[,] board, int row, int col)
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);

            // Check if there are enough cells to the bottom left to form an SOS
            if (row >= 2 && col >= 2 && row <= boardHeight - 1 && col <= boardWidth - 1)
            {
                // Check for SOS formation
                if (board[row, col] == 'S' &&
                    board[row - 1, col - 1] == 'O' &&
                    board[row - 2, col - 2] == 'S')
                {
                    return true; // SOS formation detected
                }
            }

            return false; // No SOS formation detected
        }

        public SOSLine DetectSOSLine(char[,] board,  int row, int col,  IPlayer currentPlayer)
        {

            if (CheckHorizontalToTheLeft(board,  row, col))
            {
                int startCol = col;
                int endCol = col - 2;
                int middleCol = col-1;
                int middleRow = row;
                if (board[row, col] == 'O' && board[row, col - 1] == 'S' && board[row, col + 1] == 'S')
                {
                    startCol = col + 1;
                    endCol = col -1;
                }
                return new SOSLine(row, startCol, row, endCol, middleRow, middleCol, SOSLineType.HorizontalToTheLeft, currentPlayer);
            }
            if (CheckHorizontalToTheRight(board, row, col))
            {
                int startCol = col;
                int endCol = col +2;
                int middleCol = col - 1;
                int middleRow = row;
                return new SOSLine(row, startCol, row, endCol, middleRow, middleCol, SOSLineType.HorizontalToTheRight, currentPlayer);
            }
            else if (CheckVerticalDownWard(board, row, col))
            {

                int startRow = row;
                int endRow = row + 2;
                int middleCol = col;
                int middleRow = row - 1;
                if (board[row, col] == 'O' && board[row -1, col ] == 'S' && board[row+1, col] == 'S')
                {
                    startRow = row + 1;
                    endRow = row - 1;
                    middleRow = row;
                }
                return new SOSLine(startRow, col, endRow, col, middleRow, middleCol, SOSLineType.VerticalDownWard, currentPlayer);
            }
            else if (CheckVerticalUpward(board, row, col))
            {
                int startRow = row;
                int endRow = row - 2;
                int middleCol = col;
                int middleRow = row - 1;
                return new SOSLine(startRow, col, endRow, col, middleRow, middleCol, SOSLineType.VerticalUpWard, currentPlayer);
            }
            else if (CheckDiagonalTopLeftToBottomRight(board,  row, col))
              {
                  int startRow = row;
                  int startCol = col;
                  int endRow = row + 2;
                  int endCol = col + 2;
                  int middleCol = col+1;
                  int middleRow = row+1;
                  return new SOSLine(startRow, startCol, endRow, endCol, middleRow, middleCol, SOSLineType.DiagonalTopLeftToBottomRightWithMiddle, currentPlayer);
              }
            else if (CheckDiagonalTopRightToBottomLeft(board,  row, col))
            {
                  int startRow = row;
                  int startCol = col;
                  int endRow = row + 2;
                  int endCol = col - 2;
                  int middleCol = col-1;
                  int middleRow = row+1;
                  return new SOSLine(startRow, startCol, endRow, endCol, middleRow, middleCol, SOSLineType.DiagonalTopRightToBottomLeftWithMiddle, currentPlayer);
            }
            else if (CheckDiagonalBottomRightToTopLeft(board,  row, col))
            {
                int startRow = row;
                int startCol = col;
                int endRow = row - 2;
                int endCol = col - 2;
                int middleCol = col -1;
                int middleRow = row - 1;
                return new SOSLine(startRow, startCol, endRow, endCol, middleRow, middleCol, SOSLineType.DiagonalBottomRightToTopLeft, currentPlayer);
            }
            else if(CheckDiagonalBottomLeftToRight(board, row, col))
            {
                int startRow = row;
                int startCol = col;
                int endRow = row - 2;
                int endCol = col + 2;
                int middleCol = col + 1;
                int middleRow = row - 1;
                return new SOSLine(startRow, startCol, endRow, endCol, middleRow, middleCol, SOSLineType.DiagonalBottomLeftToTopRight, currentPlayer);
            }

            return null;
        }


        public abstract bool IsGameOver(IBoard board, IPlayer _player1, IPlayer _player2);
        public abstract bool IsDraw(IBoard board, IPlayer _player1, IPlayer _player2);
        public abstract bool PlayerHasWon(IPlayer _player1, IPlayer _player2);

    }
}
