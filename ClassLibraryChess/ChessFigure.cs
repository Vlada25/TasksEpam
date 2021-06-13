using ClassLibraryChess.FigureTypes;
using System;
using System.Collections.Generic;

namespace ClassLibraryChess
{
    public abstract class ChessFigure : ICloneable
    {
        public enum Colors
        {
            Black,
            White
        }
        protected Colors color;
        public static string ErrorMessage;
        public static bool IsWhiteShouldMove { get; set; }
        public string ShortFigureName { get; set; }
        public string KindOfFigure { get; set; }
        public string ChessBoard { get; set; }
        public int HorizontalPosition { get; set; }
        public int VerticalPosition { get;  set; }

        protected static List<string> OccupiedPositionsList = new List<string>();
        protected static List<string> KindOfFiguresList = new List<string>();
        protected static List<string> WhiteOccupiedPositions = new List<string>();
        protected static List<string> BlackOccupiedPositions = new List<string>();

        public ChessFigure(string combination, string shortFigureName, Colors color, string kindOfFigure)
        {
            KindOfFigure = kindOfFigure;
            SetFigurePosition(combination, color);
            if (shortFigureName.Length > 3)
            {
                throw new Exception("Short name of figure can not include more than 3 symbols");
            }
            else
            {
                ShortFigureName = shortFigureName;
                this.color = color;
            }
        }

        protected ChessFigure()
        {
        }

        public abstract void Move(string combination);
        public abstract void Beat(string combination);
        protected abstract bool CanMove(string combination);
        protected abstract bool CanBeat(string combination);

        public void SetFigurePosition(string combination, Colors color)
        {
            IsCombinationValid(combination);
            HorizontalPosition = (int)combination[0] - 97;
            VerticalPosition = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
            ChessBoard = combination;

            OccupiedPositionsList.Add(combination);
            KindOfFiguresList.Add(KindOfFigure);
            if (color == Colors.Black)
            {
                BlackOccupiedPositions.Add(combination);
            }
            else
            {
                WhiteOccupiedPositions.Add(combination);
            }
        }
        protected void IsCombinationValid(string combination)
        {
            if (combination.Length != 2)
            {
                throw new Exception("Invalid length of combination");
            }
            else
            {
                int xPos = combination[0] - 97;
                int yPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;
                if ((xPos < 0 || xPos >= 8) || (yPos < 0 || yPos >= 8))
                {
                    throw new Exception("Invalid value for position");
                }
            }
        }
        protected void SetFigurePosition(string combination, Colors color, string kindOfFigure, bool isBeat)
        {
            int indexOfChessBoard;
            int xPos = combination[0] - 97;
            int yPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;

            if (isBeat)
            {
                indexOfChessBoard = OccupiedPositionsList.IndexOf(combination);
                OccupiedPositionsList.Remove(combination);
                if (color == Colors.Black)
                {
                    WhiteOccupiedPositions.Remove(combination);
                }
                else
                {
                    BlackOccupiedPositions.Remove(combination);
                }

                try
                {
                    KindOfFiguresList.RemoveAt(indexOfChessBoard);
                }
                catch { }
            }
            if ((BlackOccupiedPositions.Contains(combination) && color == Colors.Black) ||
                    (WhiteOccupiedPositions.Contains(combination) && color == Colors.White))
            {
                throw new Exception("Impossiable to make a move");
            }
            BlackOccupiedPositions.Remove(ChessBoard);
            WhiteOccupiedPositions.Remove(ChessBoard);
            indexOfChessBoard = OccupiedPositionsList.IndexOf(ChessBoard);
            OccupiedPositionsList.Remove(ChessBoard);
            if (KindOfFiguresList.Count > 0)
            {
                try
                {
                    KindOfFiguresList.RemoveAt(indexOfChessBoard);
                }
                catch { }
            }

            HorizontalPosition = xPos;
            VerticalPosition = yPos;
            ChessBoard = combination;

            OccupiedPositionsList.Add(ChessBoard);
            KindOfFiguresList.Add(kindOfFigure);
            if (color == Colors.Black)
            {
                BlackOccupiedPositions.Add(ChessBoard);
            }
            else
            {
                WhiteOccupiedPositions.Add(ChessBoard);
            }
        }
        protected static bool IsTheFieldUnderAttack(string combination, string nowChessBoard, Colors color)
        {
            bool isAnyFigureOnTheWay, isCaseWork;
            int newXPos = combination[0] - 97;
            int newYPos = Convert.ToInt32(Convert.ToString(combination[1])) - 1;

            for (int j = 0; j < KindOfFiguresList.Count; j++)
            {
                if (OccupiedPositionsList[j] == nowChessBoard)
                {
                    continue;
                }
                if (WhiteOccupiedPositions.Contains(OccupiedPositionsList[j]) && color == Colors.White ||
                    BlackOccupiedPositions.Contains(OccupiedPositionsList[j]) && color == Colors.Black)
                {
                    continue;
                }

                int horizontalPos = OccupiedPositionsList[j][0] - 97;
                int verticalPos = Convert.ToInt32(Convert.ToString(OccupiedPositionsList[j][1])) - 1;

                if (KindOfFiguresList[j] == "pawn") // PAWN
                {
                    if (Math.Abs(horizontalPos - newXPos) == 1 && Math.Abs(verticalPos - newYPos) == 1
                    && (WhiteOccupiedPositions.Contains(combination) && color == Colors.Black ||
                    BlackOccupiedPositions.Contains(combination) && color == Colors.White))
                    {
                        return true;
                    }
                }
                else if (KindOfFiguresList[j] == "rook") // ROOK
                {
                    isAnyFigureOnTheWay = false;
                    isCaseWork = false;

                    if (Math.Abs(horizontalPos - newXPos) > 0 && Math.Abs(verticalPos - newYPos) == 0)
                    {
                        isCaseWork = true;
                        int xIndex;
                        for (int i = 1; i < Math.Abs(horizontalPos - newXPos); i++)
                        {
                            xIndex = i;
                            if (horizontalPos - newXPos > 0)
                            {
                                xIndex = -i;
                            }
                            string jumpOverCell = Convert.ToChar(xIndex + horizontalPos + 97) + Convert.ToString(verticalPos + 1);
                            if (OccupiedPositionsList.Contains(jumpOverCell))
                            {
                                isAnyFigureOnTheWay = true;
                                break;
                            }
                        }
                    }
                    else if (Math.Abs(horizontalPos - newXPos) == 0 && Math.Abs(verticalPos - newYPos) > 0)
                    {
                        isCaseWork = true;
                        int yIndex;
                        for (int i = 1; i < Math.Abs(verticalPos - newYPos); i++)
                        {
                            yIndex = i;
                            if (verticalPos - newYPos > 0)
                            {
                                yIndex = -i;
                            }
                            string jumpOverCell = Convert.ToChar(horizontalPos + 97) + Convert.ToString(yIndex + verticalPos + 1);
                            if (OccupiedPositionsList.Contains(jumpOverCell))
                            {
                                isAnyFigureOnTheWay = true;
                            }
                        }
                    }
                    if (!isAnyFigureOnTheWay && isCaseWork)
                    {
                        return true;
                    }
                }
                else if (KindOfFiguresList[j] == "queen") // QUEEN
                {
                    isAnyFigureOnTheWay = false;
                    isCaseWork = false;

                    if (Math.Abs(horizontalPos - newXPos) == Math.Abs(verticalPos - newYPos))
                    {
                        isCaseWork = true;
                        int xIndex, yIndex;
                        for (int i = 1; i < Math.Abs(horizontalPos - newXPos); i++)
                        {
                            xIndex = i;
                            yIndex = i;
                            if (horizontalPos - newXPos > 0)
                            {
                                xIndex = -i;
                            }
                            if (verticalPos - newYPos > 0)
                            {
                                yIndex = -i;
                            }
                            string jumpOverCell = Convert.ToChar(xIndex + horizontalPos + 97) + Convert.ToString(yIndex + verticalPos + 1);
                            if (OccupiedPositionsList.Contains(jumpOverCell))
                            {
                                isAnyFigureOnTheWay = true;
                                break;
                            }
                        }
                    }
                    else if (Math.Abs(horizontalPos - newXPos) > 0 && Math.Abs(verticalPos - newYPos) == 0)
                    {
                        isCaseWork = true;
                        int xIndex;
                        for (int i = 1; i < Math.Abs(horizontalPos - newXPos); i++)
                        {
                            xIndex = i;
                            if (horizontalPos - newXPos > 0)
                            {
                                xIndex = -i;
                            }
                            string jumpOverCell = Convert.ToChar(xIndex + horizontalPos + 97) + Convert.ToString(verticalPos + 1);
                            if (OccupiedPositionsList.Contains(jumpOverCell))
                            {
                                isAnyFigureOnTheWay = true;
                                break;
                            }
                        }
                    }
                    else if (Math.Abs(horizontalPos - newXPos) == 0 && Math.Abs(verticalPos - newYPos) > 0)
                    {
                        isCaseWork = true;
                        int yIndex;
                        for (int i = 1; i < Math.Abs(verticalPos - newYPos); i++)
                        {
                            yIndex = i;
                            if (verticalPos - newYPos > 0)
                            {
                                yIndex = -i;
                            }
                            string jumpOverCell = Convert.ToChar(horizontalPos + 97) + Convert.ToString(yIndex + verticalPos + 1);
                            if (OccupiedPositionsList.Contains(jumpOverCell))
                            {
                                isAnyFigureOnTheWay = true;
                                break;
                            }
                        }
                    }
                    if (!isAnyFigureOnTheWay && isCaseWork)
                    {
                        return true;
                    }
                }
                else if (KindOfFiguresList[j] == "knight") // KNIGHT
                {
                    if ((Math.Abs(horizontalPos - newXPos) == 2 && Math.Abs(verticalPos - newYPos) == 1) &&
                        (Math.Abs(horizontalPos - newXPos) == 1 && Math.Abs(verticalPos - newYPos) == 2))
                    {
                        return true;
                    }
                }
                else if (KindOfFiguresList[j] == "king") // KING
                {
                    if (Math.Abs(horizontalPos - newXPos) <= 1 && Math.Abs(verticalPos - newYPos) <= 1)
                    {
                        return true;
                    }
                }
                else if (KindOfFiguresList[j] == "bishop") // BISHOP
                {
                    isAnyFigureOnTheWay = false;
                    isCaseWork = false;

                    if (Math.Abs(horizontalPos - newXPos) == Math.Abs(verticalPos - newYPos))
                    {
                        isCaseWork = true;
                        int xIndex, yIndex;
                        for (int i = 1; i < Math.Abs(horizontalPos - newXPos); i++)
                        {
                            xIndex = i;
                            yIndex = i;
                            if (horizontalPos - newXPos > 0)
                            {
                                xIndex = -i;
                            }
                            if (verticalPos - newYPos > 0)
                            {
                                yIndex = -i;
                            }
                            string jumpOverCell = Convert.ToChar(xIndex + horizontalPos + 97) + Convert.ToString(yIndex + verticalPos + 1);
                            if (OccupiedPositionsList.Contains(jumpOverCell))
                            {
                                isAnyFigureOnTheWay = true;
                                break;
                            }
                        }
                    }
                    if (!isAnyFigureOnTheWay && isCaseWork)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override string ToString()
        {
            return base.ToString();
        }

        public object Clone()
        {
            Console.WriteLine("Input new type of figure that the pawn should turn into: ");
            string figureType = InputFigureType();

            if (color == Colors.Black)
            {
                BlackOccupiedPositions.Remove(ChessBoard);
            }
            else
            {
                WhiteOccupiedPositions.Remove(ChessBoard);
            }

            if (figureType.Equals("rook"))
            {
                return new Rook(ChessBoard, ShortFigureName, color, figureType);
            }
            else if (figureType.Equals("queen"))
            {
                return new Queen(ChessBoard, ShortFigureName, color, figureType);
            }
            else if (figureType.Equals("knight"))
            {
                return new Knight(ChessBoard, ShortFigureName, color, figureType);
            }
            else if (figureType.Equals("bishop"))
            {
                return new Bishop(ChessBoard, ShortFigureName, color, figureType);
            }
            return this;
        }
        private string InputFigureType()
        {
            string figureType = Console.ReadLine();
            string[] possibleTypes = { "rook", "queen", "knight", "bishop" };
            bool isFigureTypeValid = false;

            foreach (string value in possibleTypes)
            {
                if (figureType.Equals(value))
                {
                    isFigureTypeValid = true;
                    break;
                }
            }

            while (!isFigureTypeValid)
            {
                Console.WriteLine("Invalid type! Re-enter, please: ");
                figureType = Console.ReadLine();
                foreach (string value in possibleTypes)
                {
                    if (figureType.Equals(value))
                    {
                        isFigureTypeValid = true;
                        break;
                    }
                }
            }
            return figureType;
        }
        public static void DoShortCastling(string newKingCell, string newRookCell, ChessFigure king, ChessFigure rook)
        {
            const int newXPosKing = 5, newXPosRook = 6;

            if (!OccupiedPositionsList.Contains(newKingCell) && !OccupiedPositionsList.Contains(newRookCell) && 
                !IsTheFieldUnderAttack(newKingCell, king.ChessBoard, king.color) && !IsTheFieldUnderAttack(king.ChessBoard, king.ChessBoard, king.color))
            {
                king.ChessBoard = newKingCell;
                king.HorizontalPosition = newXPosKing;
                rook.ChessBoard = newRookCell;
                rook.HorizontalPosition = newXPosRook;
            }
            else
            {
                ErrorMessage = "Impossible to do the short castling";
            }
        }
        public static void DoLongCastling(string newKingCell, string newRookCell, string leftOfTheRookCell, ChessFigure king, ChessFigure rook)
        {
            const int newXPosKing = 2, newXPosRook = 3;

            if (!OccupiedPositionsList.Contains(newKingCell) && !OccupiedPositionsList.Contains(newRookCell) && 
                !IsTheFieldUnderAttack(newKingCell, king.ChessBoard, king.color) && !OccupiedPositionsList.Contains(leftOfTheRookCell)
                && !IsTheFieldUnderAttack(king.ChessBoard, king.ChessBoard, king.color))
            {
                king.ChessBoard = newKingCell;
                king.HorizontalPosition = newXPosKing;
                rook.ChessBoard = newRookCell;
                rook.HorizontalPosition = newXPosRook;
            }
            else
            {
                ErrorMessage = "Impossible to do the long castling";
            }
        }
        public bool CanDeclareCheck(ChessFigure king)
        {
            if (CanBeat(king.ChessBoard))
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            return obj is ChessFigure figure &&
                   color == figure.color &&
                   ShortFigureName == figure.ShortFigureName &&
                   KindOfFigure == figure.KindOfFigure &&
                   ChessBoard == figure.ChessBoard &&
                   HorizontalPosition == figure.HorizontalPosition &&
                   VerticalPosition == figure.VerticalPosition;
        }

        public override int GetHashCode()
        {
            int hashCode = -1284036529;
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
