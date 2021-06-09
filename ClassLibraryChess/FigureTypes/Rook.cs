using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryChess.FigureTypes
{
    public class Rook : ChessFigure
    {
        public Rook(string combination, string shortFigureName, Colors color, string kindOfFigure)
            : base(combination, shortFigureName, color, kindOfFigure)
        {

        }
        protected override bool CanMove(string combination)
        {
            IsCombinationValid(combination);
            int newXPos = combination[0] - 97;
            int newYPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
            if (Math.Abs(HorizontalPosition - newXPos) > 0 && Math.Abs(VerticalPosition - newYPos) == 0)
            {
                int xIndex;
                for (int i = 1; i < Math.Abs(HorizontalPosition - newXPos); i++)
                {
                    xIndex = i;
                    if (HorizontalPosition - newXPos > 0)
                    {
                        xIndex = -i;
                    }
                    string jumpOverCell = Convert.ToChar(xIndex + HorizontalPosition + 97) + Convert.ToString(VerticalPosition + 1);
                    Console.WriteLine(jumpOverCell);
                    if (OccupiedPositionsList.Contains(jumpOverCell))
                    {
                        ErrorMessage = "Impossiable to make a move";
                        return false;
                    }
                }
                return true;
            }
            else if (Math.Abs(HorizontalPosition - newXPos) == 0 && Math.Abs(VerticalPosition - newYPos) > 0)
            {
                int yIndex;
                for (int i = 1; i < Math.Abs(VerticalPosition - newXPos); i++)
                {
                    yIndex = i;
                    if (VerticalPosition - newYPos > 0)
                    {
                        yIndex = -i;
                    }
                    string jumpOverCell = Convert.ToChar(HorizontalPosition + 97) + Convert.ToString(yIndex + VerticalPosition + 1);
                    Console.WriteLine(jumpOverCell);
                    if (OccupiedPositionsList.Contains(jumpOverCell))
                    {
                        ErrorMessage = "Impossiable to make a move";
                        return false;
                    }
                }
                return true;
            }
            else
            {
                ErrorMessage = "Impossiable to make a move";
                return false;
            }
        }
        protected override bool CanBeat(string combination)
        {
            if (!OccupiedPositionsList.Contains(combination))
            {
                ErrorMessage = "Chosed field is empty";
                return false;
            }
            return CanMove(combination);
        }
        public override void Move(string combination)
        {
            if (CanMove(combination))
            {
                SetNewPos(combination, color, KindOfFigure);
            }
        }
        public override void Beat(string combination)
        {
            if (CanBeat(combination))
            {
                SetNewPos(combination, color, KindOfFigure);
            }
        }
    }
}
