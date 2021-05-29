using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryChess.FigureTypes
{
    public class King : ChessFigure
    {
        public King(string combination) : base(combination)
        {

        }
        public King(string combination, string shortFigureName, Colors color) : base(combination, shortFigureName, color)
        {

        }

        public override void Move(string combination)
        {
            IsCombinationValid(combination);
            int newXPos = (int)combination[0] - 97;
            int newYPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
            if ( Math.Abs(HorizontalPosition - newXPos) <= 1 && Math.Abs(VerticalPosition - newYPos) <= 1)
            {
                OccupiedPositionsList.Sort();
                int indexOfEnteredCombination = OccupiedPositionsList.BinarySearch(combination);
                if (indexOfEnteredCombination < 0)
                {
                    OccupiedPositionsList.Remove(ChessBoard);
                    OccupiedPositionsList.Add(combination);
                    HorizontalPosition = newXPos;
                    VerticalPosition = newYPos;
                    ChessBoard = combination;
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
        }
    }
}
