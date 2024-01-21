using System;
using System.Collections.Generic;

namespace SOSGameLogic.Interfaces
{
    public interface IGame
    {

        IPlayer GetCurrentPlayer();
        char GetCurrentPlayerSymbol();
        IBoard GetBoard();
        void MakeMove(int row, int col);
        void SwitchPlayer();
        bool IsCellOccupied(int row, int col);
        bool IsGameOver();
        void DetectSOS();
        List<SOSLine> GetDetectedSOSLines();

    }
}
