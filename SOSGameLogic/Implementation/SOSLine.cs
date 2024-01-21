using SOSGameLogic.Interfaces;
public class SOSLine
{

    public int StartRow { get; }
    public int StartCol { get; }
    public int EndRow { get; }
    public int EndCol { get; }
    public int MiddleRow { get; }
    public int MiddleCol { get; }
    public char PlayerSymbol { get; }
    public SOSLineType Type { get; }
    public IPlayer Player { get; }
    public double X1Middle { get; set; }
    public double Y1Middle { get; set; }

    public SOSLine(int startRow, int startCol, int endRow, int endCol, int middleRow, int middleCol, SOSLineType type, IPlayer player)
    {
        StartRow = startRow;
        StartCol = startCol;
        EndRow = endRow;
        EndCol = endCol;
        MiddleRow = middleRow;
        MiddleCol = middleCol;
        Type = type;
        Player = player;
        PlayerSymbol = player.GetPlayerSymbol();
    }
}
