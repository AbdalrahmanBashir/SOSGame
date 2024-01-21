namespace SOSGameLogic.Interfaces
{
    public interface IPlayer
    {
        char GetPlayerSymbol();
        int GetScore();
        void IncreaseScore(int points);
        void SetPlayerSymbol(char symbol);
        void ResetScore();
        void MakeMove(IBoard board, int row, int col);
    }
}
