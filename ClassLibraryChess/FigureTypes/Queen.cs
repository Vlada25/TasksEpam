﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryChess.FigureTypes
{
    public class Queen : ChessFigure
    {
        public Queen(string combination, string shortFigureName, Colors color) : base(combination, shortFigureName, color)
        {

        }
        public override void Move(string combination)
        {
            IsCombinationValid(combination);
            int newXPos = (int)combination[0] - 97;
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
                SetNewPos(newXPos, newYPos, combination, color);
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
                    Console.WriteLine(jumpOverCell);
                    if (OccupiedPositionsList.Contains(jumpOverCell))
                    {
                        throw new Exception("Impossiable to make a move");
                    }
                }
                SetNewPos(newXPos, newYPos, combination, color);
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
                        throw new Exception("Impossiable to make a move");
                    }
                }
                SetNewPos(newXPos, newYPos, combination, color);
            }
            else
            {
                throw new Exception("Impossiable to make a move");
            }
        }
    }
}
