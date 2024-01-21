namespace SOSGameLogic.Interfaces
{
    public interface IBoard
    {
        char[,] GetBoard();
        char GetSymbolAt(int row, int col);
        bool IsBoardFull();
        bool IsValidMove(int row, int col);
        void PlaceSymbol(int row, int col, char symbol);
    }
}
