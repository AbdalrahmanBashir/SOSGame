using SOSGameLogic.Implementation;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SOSGameGU.GameManagers
{
    public class GameSetupManager
    {
        public GameSetupManager()
        {
        }

        // Method to start the game and set up the game board grid.
        public void StartGame(Grid GameBoardGrid, MainWindow mainWindow, int boardSize, Canvas GameCanvas)
        {
            // Get the dimensions of the game board grid.
            double gridWidth = GameBoardGrid.ActualWidth;
            double gridHeight = GameBoardGrid.ActualHeight;

            // Loop through the rows and columns to create game board cells.
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    // Create a button representing a game board cell.
                    Button cellButton = new Button
                    {
                        Width = gridWidth / boardSize, // Set the cell's width based on the grid size.
                        Height = gridHeight / boardSize, // Set the cell's height based on the grid size.
                        Content = new TextBlock
                        {
                            TextAlignment = TextAlignment.Center, // Center-align the text within the cell.
                            VerticalAlignment = VerticalAlignment.Center, // Vertically center-align the text.
                            HorizontalAlignment = HorizontalAlignment.Center, // Horizontally center-align the text.
                        },
                        Tag = new Tuple<int, int>(row, col) // Store the cell's position as a tag.
                    };

                    // Add a click event handler to the cell button, connecting it to the main game logic.
                    cellButton.Click += mainWindow.Cell_Click;

                    // Set the row and column positions of the cell within the grid.
                    Grid.SetRow(cellButton, row);
                    Grid.SetColumn(cellButton, col);

                    // Add the cell button to the game board grid.
                    GameBoardGrid.Children.Add(cellButton);
                }
            }

            // Create a new game instance with the specified board size, players, and game mode.
            mainWindow.game = new Game(boardSize, mainWindow.player1, mainWindow.player2, mainWindow._modeLogic);
            

        }
        public void ClearGameBoard(Grid GameBoardGrid)
        {
            GameBoardGrid.Children.Clear();
            GameBoardGrid.RowDefinitions.Clear();
            GameBoardGrid.ColumnDefinitions.Clear();
        }

        // Method to clear game-related variables
        public void ClearGameVariables(MainWindow mainWindow)
        {
           
            mainWindow.currentPlayerTurnName = mainWindow.player1Name;
            mainWindow.txtCurrentPlayerTurn.Text = string.Empty;
            //mainWindow.player1 = new Player(mainWindow.playerSymbol);
            //mainWindow.player2 = new Player(mainWindow.playerSymbol);
            mainWindow.player1.ResetScore();
            mainWindow.player2.ResetScore();
            mainWindow.game = null; // Reset the game logic instance
        }
    }
}
