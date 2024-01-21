using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SOSGameGU.GameManagers
{
    public class DrawSOSLineManager
    {
        public DrawSOSLineManager()
        {
            
        }

        // Create and style a line to represent an SOS sequence.
        private Line CreateSOSLine(SOSLine sosLine, MainWindow mainWindow, Grid GameBoardGrid, Canvas GameCanvas)
        {
            Line line = new Line
            {
                StrokeThickness = 2
            };

            // Set the line's color based on the player who formed the SOS sequence.
            if (sosLine.Player == mainWindow.player1)
            {
                line.Stroke = Brushes.Blue;
            }
            else if (sosLine.Player == mainWindow.player2)
            {
                line.Stroke = Brushes.Red;
            }
            else
            {
                line.Stroke = Brushes.Black; // Default color.
            }

            // Set the line's coordinates based on the SOSLine object.
            SetLineCoordinates(line, sosLine, mainWindow, GameBoardGrid, GameCanvas);

            return line;
        }

        // Set the coordinates of the line based on the SOSLine object.
        private void SetLineCoordinates(Line line, SOSLine sosLine, MainWindow mainWindow, Grid GameBoardGrid, Canvas GameCanvas)
        {
            int numberOfColumns = GameBoardGrid.ColumnDefinitions.Count;
            int numberOfRows = GameBoardGrid.RowDefinitions.Count;
            double cellWidth = GameBoardGrid.ActualWidth / numberOfColumns;
            double cellHeight = GameBoardGrid.ActualHeight / numberOfRows;

            double startX = 0.0;
            double startY = 0.0;
            double endX = 0.0;
            double endY = 0.0;

            if (sosLine.Type == SOSLineType.HorizontalToTheLeft ||
                sosLine.Type == SOSLineType.HorizontalToTheRight ||
                sosLine.Type == SOSLineType.VerticalUpWard ||
                sosLine.Type == SOSLineType.VerticalDownWard)
            {
                startX = CalculateXPosition(sosLine.StartCol, mainWindow.cellWidth) + (mainWindow.cellWidth / 2);
                startY = CalculateYPosition(sosLine.StartRow, mainWindow.cellHeight) + (mainWindow.cellHeight / 2);
                endX = CalculateXPosition(sosLine.EndCol, mainWindow.cellWidth) + (mainWindow.cellWidth / 2);
                endY = CalculateYPosition(sosLine.EndRow, mainWindow.cellHeight) + (mainWindow.cellHeight / 2);
            }
            else if (sosLine.Type == SOSLineType.DiagonalTopLeftToBottomRightWithMiddle ||
                    sosLine.Type == SOSLineType.DiagonalTopRightToBottomLeftWithMiddle ||
                    sosLine.Type == SOSLineType.DiagonalBottomRightToTopLeft ||
                    sosLine.Type == SOSLineType.DiagonalBottomLeftToTopRight)
            {
                startX = CalculateXPosition(sosLine.StartCol, mainWindow.cellWidth) + (mainWindow.cellWidth / 2);
                startY = CalculateYPosition(sosLine.StartRow, mainWindow.cellHeight) + mainWindow.cellHeight / 2;
                endX = CalculateXPosition(sosLine.EndCol, mainWindow.cellWidth) + (mainWindow.cellWidth / 2);
                endY = CalculateYPosition(sosLine.EndRow, mainWindow.cellHeight) + mainWindow.cellHeight / 2;
            }

            sosLine.X1Middle = (startX + endX) / 2;
            sosLine.Y1Middle = (startY + endY) / 2;
            line.X1 = startX;
            line.Y1 = startY;
            line.X2 = endX;
            line.Y2 = endY;
        }

        // Calculate the X position of a cell within the grid.
        private double CalculateXPosition(int col, double cellWidth)
        {
            return col * cellWidth;
        }

        // Calculate the Y position of a cell within the grid.
        private double CalculateYPosition(int row, double cellHeight)
        {
            return row * cellHeight;
        }

        // Draw SOS lines on the canvas based on detected SOS sequences.
        public void DrawLinesOnCanvas(MainWindow mainWindow, Grid GameBoardGrid, Canvas gameCanvas)
        {
            gameCanvas.Children.Clear(); // Clear the canvas before redrawing lines.
            List<SOSLine> sosLines = mainWindow.game.GetDetectedSOSLines();
            foreach (SOSLine sosLine in sosLines)
            {
                Line line = CreateSOSLine(sosLine, mainWindow, GameBoardGrid, gameCanvas);
                gameCanvas.Children.Add(line); // Add the line to the canvas.
            }
        }

        // Clear all lines from the canvas.
        public void ClearLinesOnCanvas(Canvas canvas)
        {
            canvas.Children.Clear();
        }
    }
}
