using SOSGameLogic.Interfaces;

namespace SOSGameLogic.Implementation
{
    public class SimpleGameMode : BaseGameMode
    {

        public SimpleGameMode() { }
        

        public override bool IsGameOver(IBoard board, IPlayer _player1, IPlayer _player2)
        {
            return board.IsBoardFull() || PlayerHasWon(_player1,_player2);
        }

        public override bool PlayerHasWon(IPlayer _player1, IPlayer _player2)
        {

            int winningScore = 3;
            return _player1.GetScore() >= winningScore || _player2.GetScore() >= winningScore;
        }


        public override bool IsDraw(IBoard board, IPlayer _player1, IPlayer _player2)
        {
            // Check if the board is full and no player has won
            return board.IsBoardFull() && !PlayerHasWon(_player1, _player2);
        }
    }
    
}
