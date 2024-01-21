using SOSGameLogic.Interfaces;

namespace SOSGameLogic.Implementation
{
    public class HumanPlayer : BasePlayer
    {
        private char playerSymbol;
       

        public HumanPlayer(char symbol)
        {
            playerSymbol = symbol; // Initialize the player's symbol
          
        }

        public override char GetPlayerSymbol()
        {
            return playerSymbol;
        }

        public override  void MakeMove(IBoard board ,int row, int col)
        {
            char currentPlayerSymbol = GetPlayerSymbol();
            board.PlaceSymbol(row, col, currentPlayerSymbol);
        }

        public override  void SetPlayerSymbol(char symbol)
        {
            playerSymbol = symbol;
        }

        


    }
}
