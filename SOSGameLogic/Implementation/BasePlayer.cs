using SOSGameLogic.Interfaces;

namespace SOSGameLogic
{
    public abstract class BasePlayer : IPlayer
    {
        
        private int score = 0;
        
       
        public int GetScore()
        {
            return score;
        }

        public void IncreaseScore(int points)
        {
            score += points;
        }

        public void ResetScore()
        {
            score = 0;
        }

        public abstract void SetPlayerSymbol(char symbol);

        public abstract char GetPlayerSymbol();

        public abstract void MakeMove(IBoard board, int row, int col);



    }
}
