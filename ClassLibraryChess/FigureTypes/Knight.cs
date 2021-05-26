﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryChess.FigureTypes
{
    public class Knight : ChessFigure
    {
        public Knight(string combination) : base(combination)
        {
           
        }

        public override void Move(string combination)
        {
            IsCombinationValid(combination);
            int newXPos = (int)combination[0] - 96;
            int newYPos = Convert.ToInt32(Convert.ToString(combination[1]));
            if ((Math.Abs(HorizontalPosition - newXPos) == 2 && Math.Abs(VerticalPosition - newYPos) == 1) ||
                (Math.Abs(HorizontalPosition - newXPos) == 1 && Math.Abs(VerticalPosition - newYPos) == 2))
            {
                HorizontalPosition = newXPos;
                VerticalPosition = newYPos;
                ChessBoard = combination;
            }
            else
            {
                throw new Exception("Impossiable to make a move");
            }
        }
    }
}
