﻿
namespace SOSGameLogic.Interfaces
{
    public interface IGame
    {

        char GetCurrentPlayer();
        bool IsCellOccupied(int row, int col);
        bool IsGameOver();
        void MakeMove(int row, int col);
        void SwitchPlayer();
    }
}
