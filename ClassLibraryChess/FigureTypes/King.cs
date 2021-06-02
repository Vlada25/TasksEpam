using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryChess.FigureTypes
{
    public class King : ChessFigure
    {
        public King(string combination, string shortFigureName, Colors color, string kindOfFigure)
            : base(combination, shortFigureName, color, kindOfFigure)
        {

        }

        public override void Move(string combination)
        {
            IsCombinationValid(combination);
            int newXPos = combination[0] - 97;
            int newYPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
            if (Math.Abs(HorizontalPosition - newXPos) <= 1 && Math.Abs(VerticalPosition - newYPos) <= 1
                && !IsTheFieldUnderAttack(newXPos, newYPos, combination, color))
            {
                SetNewPos(newXPos, newYPos, combination, color, KindOfFigure);
            }
            else
            {
                throw new Exception("Impossiable to make a move");
            }
        }
        public override void Beat(string combination)
        {
            if (!OccupiedPositionsList.Contains(combination))
            {
                throw new Exception("Chosed field is empty");
            }
            Move(combination);
        }
    }
}
