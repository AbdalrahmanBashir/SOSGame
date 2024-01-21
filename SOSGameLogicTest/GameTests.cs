using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;

namespace SOSGameLogicTest
{
    public class GameTests
    {
        private SimpleGameMode? modeLogic;

        [Fact]
        public void IsCellOccupied_WhenCellIsEmpty_ShouldReturnFalse()
        {
            // Arrange
            IPlayer player1 = new HumanPlayer('X');
            IPlayer player2 = new HumanPlayer('O');
            modeLogic = new SimpleGameMode();
            IGame game = new Game(3, player1, player2, modeLogic);

            // Act
            bool result = game.IsCellOccupied(1, 1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void MakeMoveValidMoveShouldPlaceSymbolAndSwitchPlayer()
        {
            // Arrange
            IPlayer player1 = new HumanPlayer('X');
            IPlayer player2 = new HumanPlayer('O');
            modeLogic = new SimpleGameMode();
            IGame game = new Game(3, player1, player2, modeLogic);

            // Act
            game.MakeMove(0, 0);

            // Assert
            Assert.True(game.IsCellOccupied(0, 0));
           
        }

        [Fact]
        public void SwitchPlayerShouldChangeCurrentPlayer()
        {
            // Arrange
            IPlayer player1 = new HumanPlayer('X');
            IPlayer player2 = new HumanPlayer('O');
            modeLogic = new SimpleGameMode();
            IGame game = new Game(3, player1, player2, modeLogic);

            // Act
            game.SwitchPlayer();

            // Assert
            Assert.Equal(player2, game.GetCurrentPlayer());

            // Act again
            game.SwitchPlayer();

            // Assert
            Assert.Equal(player1, game.GetCurrentPlayer());
        }

    }
}
