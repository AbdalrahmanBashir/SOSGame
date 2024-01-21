namespace SOSGameLogic.Interfaces
{
    public interface IBaseGameMode
    {
        SOSLine DetectSOSLine(char[,] board, int row, int col,  IPlayer currentPlayer);

        bool PlayerHasWon(IPlayer _player1, IPlayer _player2);

        bool IsGameOver(IBoard board, IPlayer _player1, IPlayer _player2);

        bool IsDraw(IBoard board, IPlayer _player1, IPlayer _player2);
    }
}
