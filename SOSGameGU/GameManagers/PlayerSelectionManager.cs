using SOSGameLogic.Interfaces;
using System.Windows.Controls;
using System.Windows;
using SOSGameLogic.Implementation;

namespace SOSGameGU.GameManagers
{
    public class PlayerSelectionManager
    {
        private MainWindow mainWindow;

        public PlayerSelectionManager(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            // Initialize player selection UI components...
            InitializePlayerSelection();
        }

        private void InitializePlayerSelection()
        {
            mainWindow.rbPlayer1S.Checked += RadioButton_Checked;
            mainWindow.rbPlayer1O.Checked += RadioButton_Checked;
            mainWindow.rbPlayer2S.Checked += RadioButton_Checked;
            mainWindow.rbPlayer2O.Checked += RadioButton_Checked;
        }

        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            IPlayer currentPlayer = null;

            // Determine which player's radio button was checked...
            switch (radioButton.Name)
            {
                case "rbPlayer1S":
                case "rbPlayer1O":
                    currentPlayer = mainWindow.player1;
                    break;
                case "rbPlayer2S":
                case "rbPlayer2O":
                    currentPlayer = mainWindow.player2;
                    break;
                default:
                    break;
            }

            if (currentPlayer != null)
            {
                if (radioButton.Name.EndsWith("S"))
                {
                    currentPlayer.SetPlayerSymbol('S');
                    if (currentPlayer == mainWindow.player1)
                    {
                        mainWindow.player1SymbolSelected = true;
                    }
                    else if (currentPlayer == mainWindow.player2)
                    {
                        mainWindow.player2SymbolSelected = true;
                    }
                }
                else if (radioButton.Name.EndsWith("O"))
                {
                    currentPlayer.SetPlayerSymbol('O');
                    if (currentPlayer == mainWindow.player1)
                    {
                        mainWindow.player1SymbolSelected = true;
                    }
                    else if (currentPlayer == mainWindow.player2)
                    {
                        mainWindow.player2SymbolSelected = true;
                    }
                }
            }

            mainWindow.btnStartGame.IsEnabled = mainWindow.player1SymbolSelected || mainWindow.player2SymbolSelected;
        }

        public void RadioButton_PlayerType_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (!mainWindow.game.IsGameOver())
                {
                    MessageBox.Show("Cannot change player type once the game has started.");
                    radioButton.IsChecked = false;
                    return;
                }
                if (radioButton.Name == "rbPlayer2Human")
                {
                    // Enable "S" and "O" for human player
                    mainWindow.rbPlayer2S.IsEnabled = true;
                    mainWindow.rbPlayer2O.IsEnabled = true;
                }
                else if (radioButton.Name == "rbPlayer2Computer")
                {
                    // Disable "S" and "O" for computer player
                    mainWindow.rbPlayer2S.IsEnabled = false;
                    mainWindow.rbPlayer2O.IsEnabled = false;
                }
                if (radioButton.Name == "rbPlayer1Human")
                {
                    // Enable "S" and "O" for human player
                    mainWindow.rbPlayer1S.IsEnabled = true;
                    mainWindow.rbPlayer1O.IsEnabled = true;
                }
                else if (radioButton.Name == "rbPlayer1Computer")
                {
                    // Disable "S" and "O" for computer player
                    mainWindow.rbPlayer1S.IsEnabled = false;
                    mainWindow.rbPlayer1O.IsEnabled = false;
                }

                if (radioButton.Name == "rbPlayer1Human")
                {
                    mainWindow.player1 = new HumanPlayer(mainWindow.playerSymbol);
                }
                else if (radioButton.Name == "rbPlayer1Computer")
                {
                    mainWindow.player1 = new ComputerPlayer();
                }

                if (radioButton.Name == "rbPlayer2Human")
                {
                    mainWindow.player2 = new HumanPlayer(mainWindow.playerSymbol);
                }
                else if (radioButton.Name == "rbPlayer2Computer")
                {
                    mainWindow.player2 = new ComputerPlayer();
                }
            }
        }

    }
}
