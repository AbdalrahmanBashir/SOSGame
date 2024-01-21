using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;


namespace SOSGameLogicTest
{
    public class GeneralGameModeTests
    {
        [Fact]
        public static void IsGameOverShouldReturnTrueWhenPlayer1Wins()
        {
            // Arrange
            IBoard board = new Board(3);
            IPlayer player1 = new HumanPlayer('S');
            IPlayer player2 = new HumanPlayer('O');
            var gameMode = new GeneralGameMode();
            player1.IncreaseScore(3);
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board.PlaceSymbol(row, col, 'S');
                }
            }
            bool isGameOver = gameMode.IsGameOver(board, player1, player2);

            // Assert
            Assert.True(isGameOver);
        }

        [Fact]
        public void IsGameOverShouldReturnTrueWhenPlayer2Wins()
        {
            // Arrange
            IBoard board = new Board(3);
            IPlayer player1 = new HumanPlayer('S');
            IPlayer player2 = new HumanPlayer('O');
            var gameMode = new GeneralGameMode();
            player2.IncreaseScore(3);
           
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board.PlaceSymbol(row, col, 'S');
                }
            }
            bool isGameOver = gameMode.IsGameOver(board, player1, player2);

            // Assert
            Assert.True(isGameOver);
        }

        [Fact]
        public void IsGameOverShouldReturnFalseWhenGameIsIncomplete()
        {
            // Arrange
            IBoard board = new Board(3);
            IPlayer player1 = new HumanPlayer('S');
            IPlayer player2 = new HumanPlayer('O');
            var gameMode = new GeneralGameMode();
            bool isGameOver = gameMode.IsGameOver(board, player1, player2);
            Assert.False(isGameOver);
        }

  

        [Fact]
        public void IsDrawShouldReturnFalseWhenGameIsIncomplete()
        {
            // Arrange
            IBoard board = new Board(3);
            IPlayer player1 = new HumanPlayer('S');
            IPlayer player2 = new HumanPlayer('S');
            var gameMode = new GeneralGameMode();
            
            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    board.PlaceSymbol(row, col, 'S');
                }
            }


            bool isDraw = gameMode.IsDraw(board, player1, player2);

            Assert.False(isDraw);
        }
        [Fact]
        public void IsDrawShouldReturnTrueWhenGameIsComplete()
        {
            // Arrange
            IBoard board = new Board(3);
            IPlayer player1 = new HumanPlayer('S');
            IPlayer player2 = new HumanPlayer('S');
            var gameMode = new GeneralGameMode();
            player1.IncreaseScore(9);
            player2.IncreaseScore(9);
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board.PlaceSymbol(row, col, 'S');
                }
            }


            bool isDraw = gameMode.IsDraw(board, player1, player2);

            Assert.True(isDraw);
        }
    }
}
