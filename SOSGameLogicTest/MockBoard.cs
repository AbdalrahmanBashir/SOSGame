using SOSGameLogic.Interfaces;

namespace SOSGameLogicTest
{
    public class MockBoard : IBoard
    {
        private char[,] board;

        public MockBoard(int size)
        {
            board = new char[size, size];
        }

        public char[,] GetBoard()
        {
            return board;
        }

        public char GetSymbolAt(int row, int col)
        {
            throw new NotImplementedException();
        }

        public bool IsBoardFull()
        {
            throw new NotImplementedException();
        }

        public bool IsValidMove(int row, int col)
        {
            throw new NotImplementedException();
        }

        public void PlaceSymbol(int row, int col, char symbol)
        {
            board[row, col] = symbol;
        }
    }
}