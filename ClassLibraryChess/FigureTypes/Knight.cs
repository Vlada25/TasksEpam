using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryChess.FigureTypes
{
    public class Knight : ChessFigure
    {
        public Knight(string combination, string shortFigureName, Colors color, string kindOfFigure)
            : base(combination, shortFigureName, color, kindOfFigure)
        {

        }
        protected override bool CanMove(string combination)
        {
            IsCombinationValid(combination);
            int newXPos = combination[0] - 97;
            int newYPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
            if ((Math.Abs(HorizontalPosition - newXPos) == 2 && Math.Abs(VerticalPosition - newYPos) == 1) ||
                (Math.Abs(HorizontalPosition - newXPos) == 1 && Math.Abs(VerticalPosition - newYPos) == 2))
            {
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
            return obj is Knight knight &&
                   base.Equals(obj) &&
                   color == knight.color &&
                   ShortFigureName == knight.ShortFigureName &&
                   KindOfFigure == knight.KindOfFigure &&
                   ChessBoard == knight.ChessBoard &&
                   HorizontalPosition == knight.HorizontalPosition &&
                   VerticalPosition == knight.VerticalPosition;
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
