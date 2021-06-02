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
        public override void Move(string combination)
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
                        throw new Exception("Impossiable to make a move");
                    }
                    string jumpOverCell = ChessBoard[0] + Convert.ToString(ChessBoard[1] - 49);
                    if (VerticalPosition - newYPos == 2 && OccupiedPositionsList.Contains(jumpOverCell))
                    {
                        throw new Exception("Impossiable to make a move");
                    }
                    else
                    {
                        SetNewPos(newXPos, newYPos, combination, color, "pawn");
                    }
                }
                else if(Math.Abs(HorizontalPosition - newXPos) == 1 && VerticalPosition - newYPos == 1 
                    && WhiteOccupiedPositions.Contains(combination))
                {
                    SetNewPos(newXPos, newYPos, combination, color, KindOfFigure);
                }
                else
                {
                    throw new Exception("Impossiable to make a move");
                }
            }
            else
            {
                if (HorizontalPosition - newXPos == 0 && (VerticalPosition - newYPos == -1
                    || VerticalPosition - newYPos == -2) && !OccupiedPositionsList.Contains(combination))
                {
                    if (VerticalPosition - newYPos == -2 && !isFirstStep)
                    {
                        throw new Exception("Impossiable to make a move");
                    }
                    string jumpOverCell = ChessBoard[0] + Convert.ToString(ChessBoard[1] - 47);
                    if (VerticalPosition - newYPos == -2 && OccupiedPositionsList.Contains(jumpOverCell))
                    {
                        throw new Exception("Impossiable to make a move");
                    }
                    else
                    {
                        SetNewPos(newXPos, newYPos, combination, color, KindOfFigure);
                    }
                }
                else if (Math.Abs(HorizontalPosition - newXPos) == 1 && VerticalPosition - newYPos == -1
                    && BlackOccupiedPositions.Contains(combination))
                {
                    SetNewPos(newXPos, newYPos, combination, color, KindOfFigure);
                }
                else
                {
                    throw new Exception("Impossiable to make a move");
                }
            }
            isFirstStep = false;
        }
        public override void Beat(string combination)
        {
            if (!OccupiedPositionsList.Contains(combination))
            {
                throw new Exception("Chosed field is empty");
            }
            IsCombinationValid(combination);
            int newXPos = combination[0] - 97;
            int newYPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
            if (Math.Abs(HorizontalPosition - newXPos) == 1 && Math.Abs(VerticalPosition - newYPos) == 1
                    && (WhiteOccupiedPositions.Contains(combination) && color == Colors.Black || 
                    BlackOccupiedPositions.Contains(combination) && color == Colors.White))
            {
                SetNewPos(newXPos, newYPos, combination, color, KindOfFigure);
            }
            else
            {
                throw new Exception("Impossiable to make a move");
            }
            isFirstStep = false;
        }
    }
}
