using System;
using System.Collections.Generic;

namespace ClassLibraryChess
{
    public abstract class ChessFigure
    {
        public enum Colors
        {
            Black = 0,
            White = 1
        }
        protected Colors color;
        public static bool IsWhiteShouldMove { get; set; }
        public string ShortFigureName { get; set; }
        public string ChessBoard { get; set; }
        public int HorizontalPosition { get; set; }
        public int VerticalPosition { get;  set; }
        protected static List<string> OccupiedPositionsList = new List<string>();
        protected static List<string> WhiteOccupiedPositions = new List<string>();
        protected static List<string> BlackOccupiedPositions = new List<string>();

        public ChessFigure(string combination, string shortFigureName, Colors color)
        {
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

        protected void SetFigurePosition(string combination, Colors color)
        {
            IsCombinationValid(combination);
            SetNewPos((int)combination[0] - 97, Convert.ToInt32(Convert.ToString(combination[1])) - 1, combination, color);
        }
        protected void IsCombinationValid(string combination)
        {
            if (combination.Length != 2)
            {
                throw new Exception("Invalid length of combination");
            }
            else
            {
                int xPos = (int)combination[0] - 97;
                int yPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
                if ((xPos < 0 || xPos >= 8) || (yPos < 0 || yPos >= 8))
                {
                    throw new Exception("Invalid value for position");
                }
            }
        }
        protected void SetNewPos(int xPos, int yPos, string combination, Colors color)
        {
            if ((BlackOccupiedPositions.Contains(combination) && color == 0) ||
                    (WhiteOccupiedPositions.Contains(combination) && (int)color == 1))
            {
                throw new Exception("Impossiable to make a move");
            }
            BlackOccupiedPositions.Remove(ChessBoard);
            WhiteOccupiedPositions.Remove(ChessBoard);
            OccupiedPositionsList.Remove(ChessBoard);
            HorizontalPosition = xPos;
            VerticalPosition = yPos;
            ChessBoard = combination;
            OccupiedPositionsList.Add(ChessBoard);
            if ((int)color == 0)
            {
                BlackOccupiedPositions.Add(ChessBoard);
            }
            else
            {
                WhiteOccupiedPositions.Add(ChessBoard);
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
