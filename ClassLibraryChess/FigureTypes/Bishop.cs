using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryChess.FigureTypes
{
    public class Bishop : ChessFigure
    {
        public Bishop(string combination) : base(combination)
        {

        }
        public Bishop(string combination, string shortFigureName, Colors color) : base(combination, shortFigureName, color)
        {

        }
        public override void Move(string combination)
        {
            IsCombinationValid(combination);

            int newXPos = (int)combination[0] - 97;
            int newYPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
            if (Math.Abs(HorizontalPosition - newXPos) == Math.Abs(VerticalPosition - newYPos))
            {
                HorizontalPosition = newXPos;
                VerticalPosition = newYPos;
                ChessBoard = combination;
            }
            else
            {
                throw new Exception("Impossiable to make a move");
            }
        }
    }
}
