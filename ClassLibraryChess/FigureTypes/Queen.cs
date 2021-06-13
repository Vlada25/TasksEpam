using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryChess.FigureTypes
{
    public class Queen : ChessFigure
    {
        public Queen(string combination, string shortFigureName, Colors color, string kindOfFigure)
            : base(combination, shortFigureName, color, kindOfFigure)
        {

        }
        protected override bool CanMove(string combination)
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
                        ErrorMessage = "Impossiable to make a move";
                        return false;
                    }
                }
                return true;
            }
            else if (Math.Abs(HorizontalPosition - newXPos) > 0 && Math.Abs(VerticalPosition - newYPos) == 0)
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
                for (int i = 1; i < Math.Abs(VerticalPosition - newYPos); i++)
                {
                    yIndex = i;
                    if (VerticalPosition - newYPos > 0)
                    {
                        yIndex = -i;
                    }
                    string jumpOverCell = Convert.ToChar(HorizontalPosition + 97) + Convert.ToString(yIndex + VerticalPosition + 1);
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
                SetFigurePosition(combination, color, KindOfFigure, false);
            }
        }
        public override void Beat(string combination)
        {
            if (CanBeat(combination))
            {
                SetFigurePosition(combination, color, KindOfFigure, true);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Queen queen &&
                   base.Equals(obj) &&
                   color == queen.color &&
                   ShortFigureName == queen.ShortFigureName &&
                   KindOfFigure == queen.KindOfFigure &&
                   ChessBoard == queen.ChessBoard &&
                   HorizontalPosition == queen.HorizontalPosition &&
                   VerticalPosition == queen.VerticalPosition;
        }

        public override int GetHashCode()
        {
            int hashCode = -970351339;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + color.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ShortFigureName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(KindOfFigure);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ChessBoard);
            hashCode = hashCode * -1521134295 + HorizontalPosition.GetHashCode();
            hashCode = hashCode * -1521134295 + VerticalPosition.GetHashCode();
            return hashCode;
        }
    }
}
