using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;


namespace SOSGameLogicTest
{
    public class BoardTests
    {
        [Fact]
        public void GetBoard_ReturnsInitializedBoard()
        {
            // Arrange
            int boardSize = 3;
            IBoard board = new Board(boardSize);

            // Act
            char[,] actualBoard = board.GetBoard();

            // Assert
            Assert.NotNull(actualBoard);
            Assert.Equal(boardSize, actualBoard.GetLength(0));
            Assert.Equal(boardSize, actualBoard.GetLength(1));
            foreach (char cell in actualBoard)
            {
                Assert.Equal(' ', cell);
            }
        }

        [Theory]
        [InlineData(0, 0, true)] // Valid move to an empty cell
        [InlineData(1, 1, true)] // Invalid move to a non-empty cell
       [InlineData(4, 4, false)] // Invalid move outside of the board
        public void IsValidMove_ReturnsExpectedResult(int row, int col, bool expectedResult)
        {
            // Arrange
            int boardSize = 4;
            IBoard board = new Board(boardSize);

            // Act
            bool isValidMove = board.IsValidMove(row, col);

            // Assert
            Assert.Equal(expectedResult, isValidMove);
        }

        [Fact]
        public void PlaceSymbol_PlacesSymbolCorrectly()
        {
            // Arrange
            int boardSize = 3;
            IBoard board = new Board(boardSize);
            char symbol = 'S';

            // Act
            board.PlaceSymbol(0, 0, symbol);
            char placedSymbol = board.GetSymbolAt(0, 0);

            // Assert
            Assert.Equal(symbol, placedSymbol);
        }

        [Fact]
        public void IsBoardFull_EmptyBoard_ReturnsFalse()
        {
            // Arrange
            int boardSize = 3;
            IBoard board = new Board(boardSize);

            // Act
            bool isFull = board.IsBoardFull();

            // Assert
            Assert.False(isFull);
        }

        [Fact]
        public void IsBoardFull_FullBoard_ReturnsTrue()
        {
            // Arrange
            int boardSize = 3;
            IBoard board = new Board(boardSize);

            // Fill the entire board
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    board.PlaceSymbol(row, col, 'S');
                }
            }

            // Act
            bool isFull = board.IsBoardFull();

            // Assert
            Assert.True(isFull);
        }

    }
}
