﻿using System;
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
        protected override bool CanMove(string combination)
        {
            IsCombinationValid(combination);
            int newXPos = combination[0] - 97;
            int newYPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
            if (Math.Abs(HorizontalPosition - newXPos) <= 1 && Math.Abs(VerticalPosition - newYPos) <= 1
                && !IsTheFieldUnderAttack(combination, ChessBoard, color))
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
                SetNewPos(combination, color, KindOfFigure, false);
            }
        }
        public override void Beat(string combination)
        {
            if (CanBeat(combination))
            {
                SetNewPos(combination, color, KindOfFigure, true);
            }
        }
    }
}
