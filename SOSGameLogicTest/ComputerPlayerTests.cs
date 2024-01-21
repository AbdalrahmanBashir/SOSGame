using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;
using System.Windows.Documents;
using Xunit;

namespace SOSGameLogicTest
{
    public class ComputerPlayerTests
    {
        

        [Fact]
        public void GenerateRandomPositionReturnsValidPosition()
        {
            // Arrange
            IBoard board = new Board(6);
            ComputerPlayer computerPlayer = new ComputerPlayer();

            // Act
            var position = computerPlayer.GenerateRandomPosition(board);

            // Assert
            Assert.NotNull(position);
            Assert.True(position.Item1 >= 0 && position.Item1 < board.GetBoard().GetLength(0));
            Assert.True(position.Item2 >= 0 && position.Item2 < board.GetBoard().GetLength(1));
        }

        [Fact]
        public void GenerateIntelligentMoveReturnsValidMove()
        {
            // Arrange
            IBoard board = new Board(6);
            ComputerPlayer computerPlayer = new ComputerPlayer();

            // Act
            var move = computerPlayer.GenerateIntelligentMove(board);

            // Assert
            Assert.NotNull(move);
            Assert.True(move.Item1 >= 0 && move.Item1 < board.GetBoard().GetLength(0));
            Assert.True(move.Item2 >= 0 && move.Item2 < board.GetBoard().GetLength(1));
        }

        [Fact]
        public void MakeMovePlacesSymbolOnBoard()
        {
            // Arrange
            IBoard board = new Board(6);
            ComputerPlayer computerPlayer = new ComputerPlayer();

            // Act
            computerPlayer.MakeMove(board, 1, 2);

            // Assert
            Assert.Equal(computerPlayer.GetPlayerSymbol(), board.GetSymbolAt(1, 2));
        }

        [Fact]
        public void GetPlayerSymbolReturnsCorrectSymbol()
        {
            // Arrange
            ComputerPlayer computerPlayer = new ComputerPlayer();
            IBoard board = new Board(6);

            // Act
           char playerSymbol= computerPlayer.GenerateRandomSymbol();

            // Assert
            Assert.True(playerSymbol == 'S' || playerSymbol == 'O');
        }

        [Fact]
        public void CheckHorizontalToTheLeftDetectsSOSFormation()
        {
            // Arrange
            IBoard board = new Board(6);
            ComputerPlayer computerPlayer = new ComputerPlayer();

            // Place an SOS formation horizontally to the left
            
            board.PlaceSymbol(1, 1, 'O');
            board.PlaceSymbol(1, 0, 'S');
            int row = 1;
            int col = 2;
            // Act
            bool result = computerPlayer.CheckHorizontalToTheLeft(board.GetBoard(), row, col);

            // Assert
            Assert.True(board.GetBoard()[row, col] == ' ');
            Assert.True(result);
        }

        [Fact]
        public void CheckVerticalDownwardDetectsSOSFormation()
        {
            // Arrange
            IBoard board = new Board(6);
            ComputerPlayer computerPlayer = new ComputerPlayer();

            // Place an SOS formation vertically downward
            //board.PlaceSymbol(1, 2, 'S');
            board.PlaceSymbol(2, 2, 'O');
            board.PlaceSymbol(3, 2, 'S');
            int row = 1;
            int col = 2;

            // Act
            bool result = computerPlayer.CheckVerticalDownWard(board.GetBoard(), row, col);

            // Assert
            Assert.True(board.GetBoard()[row, col] == ' ');
            Assert.True(result);
        }

    }
}
