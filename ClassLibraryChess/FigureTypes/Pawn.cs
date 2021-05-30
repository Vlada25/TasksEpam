using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryChess.FigureTypes
{
    public class Pawn : ChessFigure
    {
        private bool isFirstStep;
        public Pawn(string combination, string shortFigureName, Colors color) : base(combination, shortFigureName, color)
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
                        SetNewPos(newXPos, newYPos, combination, color);
                    }
                }
                else if(Math.Abs(HorizontalPosition - newXPos) == 1 && VerticalPosition - newYPos == 1 
                    && WhiteOccupiedPositions.Contains(combination))
                {
                    SetNewPos(newXPos, newYPos, combination, color);
                }
                else
                {
                    throw new Exception("Impossiable to make a move");
                }
            }
            else if ((int)color == 1)
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
                        SetNewPos(newXPos, newYPos, combination, color);
                    }
                }
                else if (Math.Abs(HorizontalPosition - newXPos) == 1 && VerticalPosition - newYPos == -1
                    && BlackOccupiedPositions.Contains(combination))
                {
                    SetNewPos(newXPos, newYPos, combination, color);
                }
                else
                {
                    throw new Exception("Impossiable to make a move");
                }
            }
            else
            {
                throw new Exception("Impossiable to make a move");
            }
            isFirstStep = false;
        }
    }
}
