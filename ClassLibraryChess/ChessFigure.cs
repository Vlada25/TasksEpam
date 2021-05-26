using System;

namespace ClassLibraryChess
{
    public abstract class ChessFigure
    {
        public string ChessBoard { get; set; }
        protected int HorizontalPosition { get; set; }
        protected int VerticalPosition { get;  set; }

        public ChessFigure(string combination)
        {
            SetFigurePosition(combination);
        }

        public abstract void Move(string combination);

        protected void SetFigurePosition(string combination)
        {
            IsCombinationValid(combination);
            HorizontalPosition = (int)combination[0] - 96;
            VerticalPosition = Convert.ToInt32(Convert.ToString(combination[1]));
            ChessBoard = combination;
        }
        protected void IsCombinationValid(string combination)
        {
            if (combination.Length != 2)
            {
                throw new Exception("Invalid length of combination");
            }
            else
            {
                int xPos = (int)combination[0] - 96;
                int yPos = Convert.ToInt32(Convert.ToString(combination[1]));
                if ((xPos <= 0 || xPos > 8) || (yPos <= 0 || yPos > 8))
                {
                    throw new Exception("Invalid value for position");
                }
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
