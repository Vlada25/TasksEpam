using System;
using System.Collections.Generic;

namespace ClassLibraryChess
{
    public abstract class ChessFigure
    {
        public enum Colors
        {
            Black,
            White
        }
        protected Colors color;
        public static bool IsWhiteShouldMove { get; set; }
        public string ShortFigureName { get; set; }
        protected string KindOfFigure { get; set; }
        public string ChessBoard { get; set; }
        public int HorizontalPosition { get; set; }
        public int VerticalPosition { get;  set; }

        protected static List<string> OccupiedPositionsList = new List<string>();
        protected static List<string> KindOfFiguresList = new List<string>();
        protected static List<string> WhiteOccupiedPositions = new List<string>();
        protected static List<string> BlackOccupiedPositions = new List<string>();

        public ChessFigure(string combination, string shortFigureName, Colors color, string kindOfFigure)
        {
            KindOfFigure = kindOfFigure;
            SetFigurePosition(combination, color);
            if (shortFigureName.Length > 3)
            {
                throw new Exception("Short name of figure can not include more than 3 symbols");
            }
            else
            {
                ShortFigureName = shortFigureName;
                this.color = color;
            }
        }

        public abstract void Move(string combination);
        public abstract void Beat(string combination);

        public void SetFigurePosition(string combination, Colors color)
        {
            IsCombinationValid(combination);
            SetNewPos((int)combination[0] - 97, Convert.ToInt32(Convert.ToString(combination[1])) - 1, combination, color, KindOfFigure);
        }
        protected void IsCombinationValid(string combination)
        {
            if (combination.Length != 2)
            {
                throw new Exception("Invalid length of combination");
            }
            else
            {
                int xPos = combination[0] - 97;
                int yPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
                if ((xPos < 0 || xPos >= 8) || (yPos < 0 || yPos >= 8))
                {
                    throw new Exception("Invalid value for position");
                }
            }
        }
        protected void SetNewPos(int xPos, int yPos, string combination, Colors color, string kindOfFigure)
        {
            if ((BlackOccupiedPositions.Contains(combination) && color == Colors.Black) ||
                    (WhiteOccupiedPositions.Contains(combination) && color == Colors.White))
            {
                throw new Exception("Impossiable to make a move");
            }
            BlackOccupiedPositions.Remove(ChessBoard);
            WhiteOccupiedPositions.Remove(ChessBoard);
            OccupiedPositionsList.Remove(ChessBoard);
            KindOfFiguresList.Remove(kindOfFigure);

            HorizontalPosition = xPos;
            VerticalPosition = yPos;
            ChessBoard = combination;

            OccupiedPositionsList.Add(ChessBoard);
            KindOfFiguresList.Add(kindOfFigure);
            if (color == Colors.Black)
            {
                BlackOccupiedPositions.Add(ChessBoard);
            }
            else
            {
                WhiteOccupiedPositions.Add(ChessBoard);
            }
        }
        protected static bool IsTheFieldUnderAttack(int newXPos, int newYPos, string combination, Colors color)
        {
            bool isAnyFigureOnTheWay;
            for (int j = 0; j < KindOfFiguresList.Count; j++)
            {
                int horizontalPos = OccupiedPositionsList[j][0] - 97;
                int verticalPos = Convert.ToInt32(Convert.ToString(OccupiedPositionsList[j][1])) - 1;

                if (KindOfFiguresList[j] == "pawn") // PAWN
                {
                    if (Math.Abs(horizontalPos - newXPos) == 1 && Math.Abs(verticalPos - newYPos) == 1
                    && (WhiteOccupiedPositions.Contains(combination) && color == Colors.Black ||
                    BlackOccupiedPositions.Contains(combination) && color == Colors.White))
                    {
                        return true;
                    }
                }
                else if (KindOfFiguresList[j] == "rook") // ROOK
                {
                    isAnyFigureOnTheWay = false;
                    if (Math.Abs(horizontalPos - newXPos) > 0 && Math.Abs(verticalPos - newYPos) == 0)
                    {
                        int xIndex;
                        for (int i = 1; i < Math.Abs(horizontalPos - newXPos); i++)
                        {
                            xIndex = i;
                            if (horizontalPos - newXPos > 0)
                            {
                                xIndex = -i;
                            }
                            string jumpOverCell = Convert.ToChar(xIndex + horizontalPos + 97) + Convert.ToString(verticalPos + 1);
                            Console.WriteLine(jumpOverCell);
                            if (OccupiedPositionsList.Contains(jumpOverCell))
                            {
                                isAnyFigureOnTheWay = true;
                                break;
                            }
                        }
                    }
                    else if (Math.Abs(horizontalPos - newXPos) == 0 && Math.Abs(verticalPos - newYPos) > 0)
                    {
                        int yIndex;
                        for (int i = 1; i < Math.Abs(verticalPos - newXPos); i++)
                        {
                            yIndex = i;
                            if (verticalPos - newYPos > 0)
                            {
                                yIndex = -i;
                            }
                            string jumpOverCell = Convert.ToChar(horizontalPos + 97) + Convert.ToString(yIndex + verticalPos + 1);
                            Console.WriteLine(jumpOverCell);
                            if (OccupiedPositionsList.Contains(jumpOverCell))
                            {
                                isAnyFigureOnTheWay = true;
                            }
                        }
                    }
                    if (!isAnyFigureOnTheWay)
                    {
                        return true;
                    }
                }
                else if (KindOfFiguresList[j] == "queen") // QUEEN
                {
                    isAnyFigureOnTheWay = false;
                    if (Math.Abs(horizontalPos - newXPos) == Math.Abs(verticalPos - newYPos))
                    {
                        int xIndex, yIndex;
                        for (int i = 1; i < Math.Abs(horizontalPos - newXPos); i++)
                        {
                            xIndex = i;
                            yIndex = i;
                            if (horizontalPos - newXPos > 0)
                            {
                                xIndex = -i;
                            }
                            if (verticalPos - newYPos > 0)
                            {
                                yIndex = -i;
                            }
                            string jumpOverCell = Convert.ToChar(xIndex + horizontalPos + 97) + Convert.ToString(yIndex + verticalPos + 1);
                            if (OccupiedPositionsList.Contains(jumpOverCell))
                            {
                                isAnyFigureOnTheWay = true;
                            }
                        }
                    }
                    else if (Math.Abs(horizontalPos - newXPos) > 0 && Math.Abs(verticalPos - newYPos) == 0)
                    {
                        int xIndex;
                        for (int i = 1; i < Math.Abs(horizontalPos - newXPos); i++)
                        {
                            xIndex = i;
                            if (horizontalPos - newXPos > 0)
                            {
                                xIndex = -i;
                            }
                            string jumpOverCell = Convert.ToChar(xIndex + horizontalPos + 97) + Convert.ToString(verticalPos + 1);
                            Console.WriteLine(jumpOverCell);
                            if (OccupiedPositionsList.Contains(jumpOverCell))
                            {
                                isAnyFigureOnTheWay = true;
                            }
                        }
                    }
                    else if (Math.Abs(horizontalPos - newXPos) == 0 && Math.Abs(verticalPos - newYPos) > 0)
                    {
                        int yIndex;
                        for (int i = 1; i < Math.Abs(verticalPos - newXPos); i++)
                        {
                            yIndex = i;
                            if (verticalPos - newYPos > 0)
                            {
                                yIndex = -i;
                            }
                            string jumpOverCell = Convert.ToChar(horizontalPos + 97) + Convert.ToString(yIndex + verticalPos + 1);
                            Console.WriteLine(jumpOverCell);
                            if (OccupiedPositionsList.Contains(jumpOverCell))
                            {
                                isAnyFigureOnTheWay = true;
                            }
                        }
                    }
                    if (!isAnyFigureOnTheWay)
                    {
                        return true;
                    }
                }
                else if (KindOfFiguresList[j] == "knight") // KNIGHT
                {
                    if ((Math.Abs(horizontalPos - newXPos) == 2 && Math.Abs(verticalPos - newYPos) == 1) &&
                        (Math.Abs(horizontalPos - newXPos) == 1 && Math.Abs(verticalPos - newYPos) == 2))
                    {
                        return true;
                    }
                }
                else if (KindOfFiguresList[j] == "king") // KING
                {
                    if (Math.Abs(horizontalPos - newXPos) <= 1 && Math.Abs(verticalPos - newYPos) <= 1)
                    {
                        return true;
                    }
                }
                else if (KindOfFiguresList[j] == "bishop") // BISHOP
                {
                    isAnyFigureOnTheWay = false;
                    if (Math.Abs(horizontalPos - newXPos) == Math.Abs(verticalPos - newYPos))
                    {
                        int xIndex, yIndex;
                        for (int i = 1; i < Math.Abs(horizontalPos - newXPos); i++)
                        {
                            xIndex = i;
                            yIndex = i;
                            if (horizontalPos - newXPos > 0)
                            {
                                xIndex = -i;
                            }
                            if (verticalPos - newYPos > 0)
                            {
                                yIndex = -i;
                            }
                            string jumpOverCell = Convert.ToChar(xIndex + horizontalPos + 97) + Convert.ToString(yIndex + verticalPos + 1);
                            if (OccupiedPositionsList.Contains(jumpOverCell))
                            {
                                isAnyFigureOnTheWay = true;
                            }
                        }
                        if (!isAnyFigureOnTheWay)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
