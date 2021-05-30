using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryChess.FigureTypes
{
    public class Knight : ChessFigure
    {
        public Knight(string combination, string shortFigureName, Colors color) : base(combination, shortFigureName, color)
        {
           
        }

        public override void Move(string combination)
        {
            IsCombinationValid(combination);
            int newXPos = (int)combination[0] - 97;
            int newYPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
            if ((Math.Abs(HorizontalPosition - newXPos) == 2 && Math.Abs(VerticalPosition - newYPos) == 1) ||
                (Math.Abs(HorizontalPosition - newXPos) == 1 && Math.Abs(VerticalPosition - newYPos) == 2))
            {
                SetNewPos(newXPos, newYPos, combination, color);
            }
            else
            {
                throw new Exception("Impossiable to make a move");
            }
        }
    }
}
