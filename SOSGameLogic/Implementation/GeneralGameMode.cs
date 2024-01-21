using SOSGameLogic.Interfaces;

namespace SOSGameLogic.Implementation
{
    public class GeneralGameMode : BaseGameMode
    {
        public GeneralGameMode() { }

        public override bool IsGameOver(IBoard board, IPlayer _player1, IPlayer _player2)
        {
            return board.IsBoardFull() && PlayerHasWon(_player1, _player2) || IsDraw(board, _player1, _player2);
        }

        public override bool PlayerHasWon(IPlayer _player1, IPlayer _player2)
        {
            
            return _player1.GetScore() > _player2.GetScore() || _player1.GetScore() < _player2.GetScore();
        }

        public override bool IsDraw(IBoard board, IPlayer _player1, IPlayer _player2)
        {

            return board.IsBoardFull() && !PlayerHasWon(_player1, _player2);
        }
    }
}
