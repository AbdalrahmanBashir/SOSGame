using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;

namespace SOSGameLogicTest
{
    public class GenericGameModeLogicTests
    {
        [Fact]
        public void DetectSOSLineShouldReturnSOSLine()
        {
            // Arrange
            char[,] board = new char[,]
            {
                { 'S', ' ', 'O', ' ', 'S' },
                { ' ', 'S', 'O', 'S', ' ' },
                { 'O', 'S', 'O', 'S', 'O' },
                { 'S', 'O', 'O', 'S', ' ' },
                { 'S', ' ', 'O', 'S', ' ' }
            };
            int row = 2;
            int col = 1;
            var gameMode = new SimpleGameMode();
            IPlayer currentPlayer = new HumanPlayer('S');

            // Act
            SOSLine sosLine = gameMode.DetectSOSLine(board, row, col, currentPlayer);

            // Assert
            Assert.NotNull(sosLine);
            Assert.Equal(2, sosLine.StartRow);
            Assert.Equal(1, sosLine.StartCol);
            Assert.Equal(2, sosLine.EndRow);
            Assert.Equal(3, sosLine.EndCol);
            Assert.Equal(SOSLineType.HorizontalToTheRight, sosLine.Type);
            Assert.Same(currentPlayer, sosLine.Player);
        }
        [Fact]
        public void DetectSOSLineShouldReturnNullSOSLine()
        {
            // Arrange
            char[,] board = new char[,]
            {
                { 'S', ' ', 'O', ' ', 'S' },
                { ' ', 'S', 'O', 'S', ' ' },
                { 'O', 'S', 'O', 'S', 'O' },
                { 'S', 'O', 'O', 'S', ' ' },
                { 'S', ' ', 'O', 'S', ' ' }
            };
            int row = 2;
            int col = 0;
            var gameMode = new SimpleGameMode();
            IPlayer currentPlayer = new HumanPlayer('O');

            // Act
            SOSLine sosLine = gameMode.DetectSOSLine(board, row, col, currentPlayer);

            // Assert
            Assert.Null(sosLine);
  
        }
        [Fact]
        public void CheckHorizontalToTheLeftValidSOSReturnsTrue()
        {
            // Arrange
            char[,] board = new char[3, 3]
            {
            { 'S', 'O', 'S' },
            { 'O', 'S', 'O' },
            { 'S', 'S', 'O' }
            };
            int row = 0;
            int col = 2;
            var gameMode = new SimpleGameMode();

            // Act
            bool result = gameMode.CheckHorizontalToTheLeft(board, row, col);
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckHorizontalToTheLeftUnValidSOSReturnsFalse()
        {
            // Arrange
            char[,] board = new char[3, 3]
            {
            { 'S', 'O', 'S' },
            { 'S', 'O', 'O' },
            { 'S', 'S', 'O' },
            };
            int row = 2;
            int col = 2;
            var gameMode = new SimpleGameMode();

            // Act
            bool result = gameMode.CheckHorizontalToTheLeft(board, row, col);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckHorizontalToTheRightValidSOSReturnsTrue()
        {
            // Arrange
            char[,] board = new char[3, 3]
            {
            { 'S', 'O', 'S' },
            { 'O', 'S', ' ' },
            { 'S', 'S', 'O' }
            };
            int row = 0;
            int col = 0;
            var gameMode = new SimpleGameMode();

            // Act
            bool result = gameMode.CheckHorizontalToTheRight(board, row, col);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckVerticalDownWardValidSOSReturnsTrue()
        {
            // Arrange
            char[,] board = new char[3, 3]
            {
            { 'S', 'O', 'S' },
            { 'O', 'S', ' ' },
            { 'S', 'S', 'O' }
            };
            int row = 0;
            int col = 0;
            var gameMode = new SimpleGameMode();

            // Act
            bool result = gameMode.CheckVerticalDownWard(board, row, col);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void CheckDiagonalTopLeftToBottomRightShouldReturnTrue()
        {
            // Arrange
            char[,] board = new char[,]
            {
                { 'S', ' ', ' ', ' ', ' ' },
                { 'O', 'O', 'S', ' ', ' ' },
                { ' ', ' ', 'S', 'O', ' ' },
                { ' ', ' ', ' ', 'O', ' ' },
                { ' ', ' ', ' ', ' ', 'S' }
            };
            int row = 0;
            int col = 0;
            var gameMode = new GeneralGameMode();
            // Act
            bool result = gameMode.CheckDiagonalTopLeftToBottomRight(board, row, col);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckDiagonalTopLeftToBottomRightShouldReturnFalse()
        {
            // Arrange
            char[,] board = new char[,]
            {
                { 'S', 'S', 'S', 'S', 'S' },
                { ' ', 'O', ' ', ' ', ' ' },
                { 'S', ' ', 'O', ' ', ' ' },
                { ' ', ' ', ' ', 'S', ' ' },
                { 'O', 'O', 'O', ' ', 'S' }
            };
            int row = 0;
            int col = 0;
            var gameMode = new GeneralGameMode();
            // Act
            bool result = gameMode.CheckDiagonalTopLeftToBottomRight(board, row, col);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckDiagonalTopLeftToBottomRightShouldReturnFalseOutOfBoundary()
        {
            // Arrange
            char[,] board = new char[,]
            {
                { 'S', ' ', ' ', 'O', ' ' },
                { ' ', 'O', ' ', ' ', 'O' },
                { ' ', ' ', 'S', 'O', ' ' },
                { 'O', ' ', ' ', 'O', ' ' },
                { ' ', ' ', ' ', 'O', 'S' }
            };
            int row = 5;
            int col = 3;
            var gameMode = new GeneralGameMode();
            // Act
            bool result = gameMode.CheckDiagonalTopLeftToBottomRight(board, row, col);

            // Assert
            Assert.False(result);
        }
    }
}
