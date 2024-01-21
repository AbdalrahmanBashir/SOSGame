using SOSGameLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace SOSGameLogic.Implementation
{
    public class Game : IGame
    {
        private readonly IBoard board;
        private IPlayer currentPlayer;
        private readonly IPlayer player1;
        private readonly IPlayer player2;
        private Tuple<int, int> playerMoves;
        private readonly List<SOSLine> detectedSOSLines;
        internal readonly IBaseGameMode modeLogic;
       

        public Game(int size, IPlayer player1, IPlayer player2, IBaseGameMode modeLogic)
        {
            board = new Board(size);
            this.player1 = player1;
            this.player2 = player2;
            currentPlayer = player1;
         
            detectedSOSLines = new List<SOSLine>();
            this.modeLogic = modeLogic;

        }

        public bool IsCellOccupied(int row, int col)
        {
            return board.GetSymbolAt(row, col) != ' ';
        }
        public bool IsGameOver()
        {
            return modeLogic.IsGameOver(board, player1, player2);
        }

        public void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == player1) ? player2 : player1;
        }

        public IPlayer GetCurrentPlayer()
        {
            return currentPlayer;
        }

        public void MakeMove(int row, int col)
        {
            if (!IsGameOver() && board.IsValidMove(row, col))
            {

                currentPlayer.MakeMove(board, row, col);

                playerMoves = Tuple.Create(row, col);
                SOSLine sosLine = modeLogic.DetectSOSLine(board.GetBoard(), playerMoves.Item1, playerMoves.Item2, currentPlayer);
                if (sosLine != null)
                {
                    detectedSOSLines.Add(sosLine);
                    currentPlayer.IncreaseScore(3);
                }
                else
                {
                    SwitchPlayer();
                }

            }    
        }

        public void DetectSOS()
        {
            
        }


        public List<SOSLine> GetDetectedSOSLines()
        {
            return detectedSOSLines;
        }

       
        public char GetCurrentPlayerSymbol()
        {
            return currentPlayer.GetPlayerSymbol();
        }

        public IBoard GetBoard()
        {
            return board;
        }

        
    }
}
