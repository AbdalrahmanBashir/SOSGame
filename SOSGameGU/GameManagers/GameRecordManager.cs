using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace SOSGameGU.GameManagers
{
    public class GameRecordManager
    {
        private readonly List<RecordData> gameRecords;

        public GameRecordManager()
        {
            gameRecords = new List<RecordData>();
        }

        public void RecordGameState(MainWindow mainWindow)
        {
            RecordData recordData = new RecordData
            {
                PlayerSymbol = mainWindow.game.GetCurrentPlayerSymbol(),
                PlayerType = GetPlayerType(mainWindow.game.GetCurrentPlayer()),
                Move = mainWindow.game.GetCurrentMove()
            };

            gameRecords.Add(recordData);
        }

        public void SaveRecordsToJson()
        {
            string json = JsonConvert.SerializeObject(gameRecords, Formatting.Indented, new StringEnumConverter());

            File.WriteAllText("gameRecords.json", json);
        }

        public List<RecordData> ReadRecordedDataFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<RecordData>>(json);
            }

            return new List<RecordData>();
        }

        private PlayerType GetPlayerType(IPlayer currentPlayer)
        {
            if (currentPlayer is HumanPlayer)
            {
                return PlayerType.Human;
            }
            else if (currentPlayer is ComputerPlayer)
            {
                return PlayerType.Computer;
            }
            else
            {
                throw new InvalidOperationException("Unknown player type");
            }
        }

        public class RecordData
        {
            public char PlayerSymbol { get; set; } = ' ';
            public PlayerType PlayerType { get; set; }
            public Tuple<int, int> Move { get; set; }
        }

        public enum PlayerType
        {
            Human,
            Computer
        }
    }
}
