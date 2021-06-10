using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryChess.FigureTypes
{
    public class Pawn : ChessFigure
    {
        private bool isFirstStep;

        public Pawn(string combination, string shortFigureName, Colors color, string kindOfFigure)
            : base(combination, shortFigureName, color, kindOfFigure)
        {
            isFirstStep = true;
        }
        protected override bool CanMove(string combination)
        {
            IsCombinationValid(combination);
            int newXPos = (int)combination[0] - 97;
            int newYPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
            if ((int)color == 0)
            {
                if (HorizontalPosition - newXPos == 0 && (VerticalPosition - newYPos == 1
                    || VerticalPosition - newYPos == 2) && !OccupiedPositionsList.Contains(combination))
                {
                    if (VerticalPosition - newYPos == 2 && !isFirstStep)
                    {
                        ErrorMessage = "Impossiable to make a move";
                        return false;
                    }
                    string jumpOverCell = ChessBoard[0] + Convert.ToString(ChessBoard[1] - 49);
                    if (VerticalPosition - newYPos == 2 && OccupiedPositionsList.Contains(jumpOverCell))
                    {
                        ErrorMessage = "Impossiable to make a move";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (Math.Abs(HorizontalPosition - newXPos) == 1 && VerticalPosition - newYPos == 1
                    && WhiteOccupiedPositions.Contains(combination))
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "Impossiable to make a move";
                    return false;
                }
            }
            else
            {
                if (HorizontalPosition - newXPos == 0 && (VerticalPosition - newYPos == -1
                    || VerticalPosition - newYPos == -2) && !OccupiedPositionsList.Contains(combination))
                {
                    if (VerticalPosition - newYPos == -2 && !isFirstStep)
                    {
                        ErrorMessage = "Impossiable to make a move";
                        return false;
                    }
                    string jumpOverCell = ChessBoard[0] + Convert.ToString(ChessBoard[1] - 47);
                    if (VerticalPosition - newYPos == -2 && OccupiedPositionsList.Contains(jumpOverCell))
                    {
                        ErrorMessage = "Impossiable to make a move";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (Math.Abs(HorizontalPosition - newXPos) == 1 && VerticalPosition - newYPos == -1
                    && BlackOccupiedPositions.Contains(combination))
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "Impossiable to make a move";
                    return false;
                }
            }
        }
        protected override bool CanBeat(string combination)
        {
            if (!OccupiedPositionsList.Contains(combination))
            {
                ErrorMessage = "Chosed field is empty";
                return false;
            }
            IsCombinationValid(combination);
            int newXPos = combination[0] - 97;
            int newYPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;

            if (Math.Abs(HorizontalPosition - newXPos) == 1 && Math.Abs(VerticalPosition - newYPos) == 1
                    && (WhiteOccupiedPositions.Contains(combination) && color == Colors.Black ||
                    BlackOccupiedPositions.Contains(combination) && color == Colors.White))
            {
                return true;
            }
            else
            {
                ErrorMessage = "Impossiable to make a move";
                return false;
            }
        }
        public override void Move(string combination)
        {
            if (CanMove(combination))
            {
                SetNewPos(combination, color, KindOfFigure, false);
                isFirstStep = false;
            }
        }
        public override void Beat(string combination)
        {
            if (CanBeat(combination))
            {
                SetNewPos(combination, color, KindOfFigure, true);
                isFirstStep = false;
            }
        }
    }
}
