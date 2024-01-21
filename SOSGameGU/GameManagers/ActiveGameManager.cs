using SOSGameLogic.Implementation;
using System;
using System.Windows.Controls;
using System.Windows;
using SOSGameLogic.Interfaces;
using System.Windows.Media;

namespace SOSGameGU.GameManagers
{
    public class ActiveGameManager
    {
        private DrawSOSLineManager drawSOSLineManager;
        public ActiveGameManager(DrawSOSLineManager drawSOSLineManager)
        {
            this.drawSOSLineManager = drawSOSLineManager;
        }

        // Handles the click event when a cell is clicked
        public void Cell_Click(object sender, Grid GameBoardGrid, MainWindow mainWindow, Canvas GameCanvas,HumanPlayer humanPlayer)
        {
            drawSOSLineManager = new DrawSOSLineManager();
            Button cellButton = (Button)sender;
            Tuple<int, int> cellPosition = (Tuple<int, int>)cellButton.Tag;
            int row = cellPosition.Item1;
            int col = cellPosition.Item2;

            // Check if the game is not over
            if (!mainWindow.game.IsGameOver())
            {
                // Check if both players have selected valid symbols
                if (!mainWindow.player1SymbolSelected || !mainWindow.player2SymbolSelected)
                {
                    MessageBox.Show("Both players must choose valid player symbols");
                    return;
                }
                // Check if the selected cell is not occupied
                else if (mainWindow.game.IsCellOccupied(row, col))
                {
                    MessageBox.Show("This cell is already occupied.\nPlease choose an empty cell.");
                    return;
                }
                else
                {
                    // Handle the move for the current player
                    HandlePlayerMove(mainWindow, GameBoardGrid, GameCanvas, row, col, cellButton, humanPlayer);
                }
            }

            // Check and update the game state
            CheckGameState(mainWindow);
        }

        // Checks the game state and displays the result
        public void CheckGameState(MainWindow mainWindow)
        {
            if (mainWindow.game.IsGameOver())
            {
                if (mainWindow._modeLogic is SimpleGameMode)
                {
                    if (mainWindow.player1.GetScore() >= 3)
                    {
                        MessageBox.Show($"Game Over! {mainWindow.player1Name} wins!\n" +
                            $"with score {mainWindow.player1.GetScore()}");
                    }
                    else if (mainWindow.player2.GetScore() >= 3)
                    {
                        MessageBox.Show($"Game Over! {mainWindow.player2Name} wins!\n" +
                            $"with score {mainWindow.player2.GetScore()}");
                    }
                    else
                    {
                        MessageBox.Show("Game Over! It's a Draw!");
                    }
                }
                else if (mainWindow._modeLogic is GeneralGameMode)
                {
                    // Add logic to determine the winner or draw in the GeneralGameMode.
                    if (mainWindow.player1.GetScore() > mainWindow.player2.GetScore())
                    {
                        MessageBox.Show($"Game Over! {mainWindow.player1Name} wins!\n" +
                            $"with score {mainWindow.player1.GetScore()}");
                    }
                    else if (mainWindow.player2.GetScore() > mainWindow.player1.GetScore())
                    {
                        MessageBox.Show($"Game Over! {mainWindow.player2Name} wins!\n" +
                            $"with score {mainWindow.player2.GetScore()}");
                    }
                    else
                    {
                        MessageBox.Show("Game Over! It's a Draw!");
                    }
                }
            }
        }

        // Handles the move for the human player
        private void HandlePlayerMove(MainWindow mainWindow, Grid GameBoardGrid, Canvas GameCanvas, int row, int col, Button cellButton, HumanPlayer humanPlayer)
        {
     
            mainWindow.game.MakeMove(row, col);
            UpdateUIAfterMove(mainWindow, GameBoardGrid, GameCanvas, cellButton,  humanPlayer.GetPlayerSymbol(), humanPlayer);
            mainWindow.txtPlayer1Score.Text = mainWindow.player1.GetScore().ToString();
            mainWindow.txtPlayer2Score.Text = mainWindow.player2.GetScore().ToString();
            UpdateCurrentPlayerTurn(mainWindow, humanPlayer);
        }

        // Handles the move for the computer player
        public void HandleComputerMoves(MainWindow mainWindow, Grid GameBoardGrid, Canvas GameCanvas, ComputerPlayer computer)
        {
            IBoard board = mainWindow.game.GetBoard();
            Tuple<int, int> computerMove = computer.GenerateIntelligentMove(board);
            mainWindow.game.MakeMove(computerMove.Item1, computerMove.Item2);
            Button cellButton = GetCellButtonFromPosition(mainWindow, computerMove.Item1, computerMove.Item2);
            UpdateUIAfterMove(mainWindow, GameBoardGrid, GameCanvas, cellButton, computer.GetPlayerSymbol(), computer);
            mainWindow.txtPlayer1Score.Text = mainWindow.player1.GetScore().ToString();
            mainWindow.txtPlayer2Score.Text = mainWindow.player2.GetScore().ToString();
            UpdateCurrentPlayerTurn(mainWindow, computer);

        }

        // Updates the UI after a move, including the color of the player symbols
        private void UpdateUIAfterMove(MainWindow mainWindow, Grid GameBoardGrid, Canvas GameCanvas, Button cellButton, char currentPlayerSymbol, IPlayer player)
        {
            drawSOSLineManager = new DrawSOSLineManager();
            cellButton.Content = currentPlayerSymbol.ToString();
            cellButton.HorizontalAlignment = HorizontalAlignment.Center;
            cellButton.VerticalAlignment = VerticalAlignment.Center;
            cellButton.FontSize = 15;

            drawSOSLineManager.DrawLinesOnCanvas(mainWindow, GameBoardGrid, GameCanvas);

            // Add color to player symbols
            if (player == mainWindow.player1)
            {
                cellButton.Foreground = Brushes.Blue;
            }
            else if (player == mainWindow.player2)
            {
                cellButton.Foreground = Brushes.Red;
            }

        }

        // Updates the current player turn information
        public void UpdateCurrentPlayerTurn(MainWindow mainWindow, IPlayer player)
        {
            mainWindow.Dispatcher.Invoke(() =>
            {

                if (mainWindow.game.GetCurrentPlayer() == mainWindow.player1)
                {
                    mainWindow.currentPlayerTurnName = mainWindow.player1Name;
                }
                else if (mainWindow.game.GetCurrentPlayer() == mainWindow.player2)
                {
                    mainWindow.currentPlayerTurnName = mainWindow.player2Name;
                }

                mainWindow.txtCurrentPlayerTurn.Text = "Current Turn: " + mainWindow.currentPlayerTurnName;


            });
        }

        // Retrieves the button representing a cell based on its position
        private Button GetCellButtonFromPosition(MainWindow mainWindow, int row, int col)
        {
            foreach (UIElement element in mainWindow.GameBoardGrid.Children)
            {
                if (element is Button cellButton)
                {
                    Tuple<int, int> cellPosition = (Tuple<int, int>)cellButton.Tag;
                    if (cellPosition.Item1 == row && cellPosition.Item2 == col)
                    {
                        return cellButton;
                    }
                }
            }

            return null;
        }
    }
}
