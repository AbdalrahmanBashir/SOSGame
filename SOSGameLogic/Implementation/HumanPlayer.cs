using SOSGameLogic.Interfaces;

/// <summary>
/// Represents a player in the SOS (Scissors, Paper, Stone) game.
/// </summary>
namespace SOSGameLogic.Implementation
{
    public class Player : IPlayer
    {
        private char playerSymbol; // Stores the player's symbol ('S' or 'O')
        private int score; // Stores the player's score

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class with the specified symbol.
        /// </summary>
        /// <param name="symbol">The symbol representing the player ('S' or 'O').</param>
        public Player(char symbol)
        {
            playerSymbol = symbol; // Initialize the player's symbol
            score = 0; // Initialize the player's score to zero
        }

        /// <summary>
        /// Returns the player's symbol ('S' or 'O').
        /// </summary>
        /// <returns>The symbol representing the player.</returns>
        public char GetPlayerSymbol()
        {
            return playerSymbol;
        }

        /// <summary>
        /// Increases the player's score by the given points.
        /// </summary>
        /// <param name="points">The points to increase the player's score by.</param>
        public void IncreaseScore(int points)
        {
            score += points;
        }

        /// <summary>
        /// Returns the player's current score.
        /// </summary>
        /// <returns>The current score of the player.</returns>
        public int GetScore()
        {
            return score;
        }

        /// <summary>
        /// Sets the player's symbol to the specified symbol.
        /// </summary>
        /// <param name="symbol">The symbol to set for the player ('S' or 'O').</param>
        public void SetPlayerSymbol(char symbol)
        {
            playerSymbol = symbol;
        }

        /// <summary>
        /// Resets the player's score to zero.
        /// </summary>
        public void ResetScore()
        {
            score = 0; // Set the score back to 0
        }
    }
}
