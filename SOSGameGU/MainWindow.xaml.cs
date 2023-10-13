using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SOSGameGU
{
    public partial class MainWindow : Window
    {
        public IGame game;              // Reference to the game logic
        public IPlayer player1;         // Player 1
        public IPlayer player2;         // Player 2
        string player1Name;             // Name of Player 1
        string player2Name;             // Name of Player 2
        string currentPlayerTurnName;   // Name of the current player
        public int boardSize;
        public bool player1SymbolSelected = false;
        public bool player2SymbolSelected = false;
        public IGenericGameModeLogic _modeLogic;
        public char playerSymbol = ' '; // Player symbol (S or O)
        public readonly IPlayer currentPlayer;

        public MainWindow()
        {
            InitializeComponent();

            boardSize = 0;
            player1 = new Player(playerSymbol); // Initialize Player 1
            player2 = new Player(playerSymbol); // Initialize Player 2

            _modeLogic = new SimpleGameMode(boardSize);
            game = new Game(boardSize, player1, player1, _modeLogic); // Initialize the game
        }

        // Event handler for when a radio button for game mode is checked
        public void RadioButton_Checked_Game_Mode(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                // Check which radio button was checked and update the selected game mode
                if (radioButton.Name == "rbSimpleMode")
                {
                    _modeLogic = new SimpleGameMode(boardSize);
                  
                }
                else if (radioButton.Name == "rbGeneralMode")
                {
                    _modeLogic = new GeneralGameMode();
                }
               
            }
        }

        // Event handler for the "Start Game" button click
        public void StartGame_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected board size from the ComboBox
            ComboBoxItem selectedItem = (ComboBoxItem)ddlBoardSize.SelectedItem;

            if (selectedItem != null)
            {
                // Parse the selected item's content to get the board size
                string[] parts = selectedItem.Content.ToString().Split('x');
                int rows = int.Parse(parts[0]);
                int cols = int.Parse(parts[1]);
                Console.WriteLine("Selected board size: " + rows + "x" + cols);

                // Clear any existing rows and columns from the grid
                GameBoardGrid.RowDefinitions.Clear();
                GameBoardGrid.ColumnDefinitions.Clear();
                Console.WriteLine("Cleared existing rows and columns from the grid.");

                player1Name = lblPlayer1.Content.ToString();
                player2Name = lblPlayer2.Content.ToString();
                currentPlayerTurnName = player1Name;
                txtCurrentPlayerTurn.Text = "Current Turn: " + currentPlayerTurnName;
                Console.WriteLine("Initialized player names and current player's turn.");
                // Add new rows and columns based on the selected board size
                for (int i = 0; i < rows; i++)
                {
                    GameBoardGrid.RowDefinitions.Add(new RowDefinition());
                    Console.WriteLine("Added new rows and columns to the grid.");
                }

                for (int j = 0; j < cols; j++)
                {
                    GameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                        StartGame(rows);

            }
            else
            {
                MessageBox.Show("Please select a board size before starting the game.");
            }
        }

        // Event handler for player symbol radio buttons
        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            IPlayer currentPlayer = null;

            // Determine which player's radio button was checked
            switch (radioButton.Name)
            {
                case "rbPlayer1S":
                case "rbPlayer1O":
                    currentPlayer = player1;
                    break;
                case "rbPlayer2S":
                case "rbPlayer2O":
                    currentPlayer = player2;
                    break;
                default:
                    break;
            }

            if (currentPlayer != null)
            {
                if (radioButton.Name.EndsWith("S"))
                {
                    currentPlayer.SetPlayerSymbol('S');
                    if (currentPlayer == player1)
                    {
                        player1SymbolSelected = true;
                    }
                    else if (currentPlayer == player2)
                    {
                        player2SymbolSelected = true;
                    }
                }
                else if (radioButton.Name.EndsWith("O"))
                {
                    currentPlayer.SetPlayerSymbol('O');
                    if (currentPlayer == player1)
                    {
                        player1SymbolSelected = true;
                    }
                    else if (currentPlayer == player2)
                    {
                        player2SymbolSelected = true;
                    }
                }
            }
            
        
      

            // Enable or disable the "Start Game" button based on symbol selections
            btnStartGame.IsEnabled = player1SymbolSelected || player2SymbolSelected;

        }

        // Start the game with the specified board size
        public void StartGame(int boardSize)
        {
            GameBoardGrid.Children.Clear();

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    Button cellButton = new Button
                    {
                        Width = 40,
                        Height = 40,
                        Content = new TextBlock
                        {
                            TextAlignment = TextAlignment.Center, // Center-align the text
                            VerticalAlignment = VerticalAlignment.Center, // Vertically center-align the text
                            HorizontalAlignment = HorizontalAlignment.Center, 
                            
                        },
                        Tag = new Tuple<int, int>(row, col)
                    };

                    cellButton.Click += Cell_Click;
                    Grid.SetRow(cellButton, row);
                    Grid.SetColumn(cellButton, col);
                    GameBoardGrid.Children.Add(cellButton);
                }
            }

            //IPlayer currentPlayer = (_modeLogic is SimpleGameMode) ? player1 : player2;
            game = new Game(boardSize, player1, player2, _modeLogic);

            Console.WriteLine(game.ToString());
        }

        // Event handler for cell button clicks
        public void Cell_Click(object sender, RoutedEventArgs e)
        {
            if (!player1SymbolSelected || !player2SymbolSelected)
            {
                MessageBox.Show("Both players must choose valid player symbols ('S' or 'O') before making a move.");
                //Console.WriteLine();
                return;
            }
            else if (game.IsGameOver())
            {
                //MessageBox.Show("The game is already over. Please start a new game.");
                if (player1.GetScore() >= 3)
                {
                    lblWinner.Content = "Player 1 (S) Wins!";
                }
                else if (player2.GetScore() >= 3)
                {
                    lblWinner.Content = "Player 2 (O) Wins!";
                }
                else
                {
                    lblWinner.Content = "It's a draw!";
                }
                return;
            }
            else
            {
                // Get the button that was clicked
                Button cellButton = (Button)sender;
                // Extract the row and column information from the button's Tag property
                Tuple<int, int> cellPosition = (Tuple<int, int>)cellButton.Tag;
                int row = cellPosition.Item1;
                int col = cellPosition.Item2;
                // Check if the cell is already filled
                if (game.IsCellOccupied(row, col))
                {
                    MessageBox.Show("This cell is already occupied. Please choose an empty cell.");
                    return;
                }
                char currentPlayerSymbol = game.GetCurrentPlayer();

                game.MakeMove(row, col);
                cellButton.Content = currentPlayerSymbol.ToString();
                currentPlayerTurnName = (currentPlayerTurnName == player1Name) ? player2Name : player1Name;
                txtCurrentPlayerTurn.Text = "Current Turn: " + currentPlayerTurnName;
            }


        }
    }
}
