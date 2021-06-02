using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryChess.FigureTypes
{
    public class Bishop : ChessFigure
    {
        public Bishop(string combination, string shortFigureName, Colors color, string kindOfFigure)
            : base(combination, shortFigureName, color, kindOfFigure)
        {

        }

        public override void Move(string combination)
        {
            IsCombinationValid(combination);
            int newXPos = combination[0] - 97;
            int newYPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
            if (Math.Abs(HorizontalPosition - newXPos) == Math.Abs(VerticalPosition - newYPos))
            {
                int xIndex, yIndex;
                for (int i = 1; i < Math.Abs(HorizontalPosition - newXPos); i++)
                {
                    xIndex = i;
                    yIndex = i;
                    if (HorizontalPosition - newXPos > 0)
                    {
                        xIndex = -i;
                    }
                    if (VerticalPosition - newYPos > 0)
                    {
                        yIndex = -i;
                    }
                    string jumpOverCell = Convert.ToChar(xIndex + HorizontalPosition + 97) + Convert.ToString(yIndex + VerticalPosition + 1);
                    if (OccupiedPositionsList.Contains(jumpOverCell))
                    {
                        throw new Exception("Impossiable to make a move");
                    }
                }
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
