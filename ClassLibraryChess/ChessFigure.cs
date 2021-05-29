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
        public string ShortFigureName { get; set; }
        public string ChessBoard { get; set; }
        public int HorizontalPosition { get; set; }
        public int VerticalPosition { get;  set; }
        protected static List<string> OccupiedPositionsList = new List<string>();

        public ChessFigure(string combination)
        {
            SetFigurePosition(combination);
        }
        public ChessFigure(string combination, string shortFigureName, Colors color)
        {
            SetFigurePosition(combination);
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

        protected void SetFigurePosition(string combination)
        {
            IsCombinationValid(combination);
            SetNewFigurePosition((int)combination[0] - 97, Convert.ToInt32(Convert.ToString(combination[1])) - 1, combination);
            OccupiedPositionsList.Add(ChessBoard);
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
        protected void SetNewFigurePosition(int xPos, int yPos, string combination)
        {
            OccupiedPositionsList.Remove(ChessBoard);
            HorizontalPosition = xPos;
            VerticalPosition = yPos;
            ChessBoard = combination;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
