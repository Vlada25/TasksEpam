using System;
using System.Collections.Generic;
using System.Text;
using ClassLibraryChess;
using ClassLibraryChess.FigureTypes;

namespace Game
{
    class Service
    {
        static bool isBeat = false;
        static string pawnTransformationInfo = null;
        static bool wasTheCastling = false;
        static string winnerMessage;
        static string shahMessage = null;
        public static void StartFillOfChessField(List<ChessFigure> blackFigures, List<ChessFigure> whiteFigures, string[,] chessField)
        {
            for (int i = 0; i < 16; i++)
            {
                int xPos, yPos;

                xPos = blackFigures[i].HorizontalPosition;
                yPos = blackFigures[i].VerticalPosition;
                chessField[yPos, xPos] = blackFigures[i].ShortFigureName;

                xPos = whiteFigures[i].HorizontalPosition;
                yPos = whiteFigures[i].VerticalPosition;
                chessField[yPos, xPos] = whiteFigures[i].ShortFigureName;
            }
        }
        public static string Menu(string[,] gameProcess, List<ChessFigure> blackFigures, List<ChessFigure> whiteFigures, string[,] chessField)
        {
            string str = "";
            const int indexOfMenuItem = 0;
            const int indexOfFigureName = 1;
            const int indexOfCell = 2;
            int countOfMoves = gameProcess.Length / 3;

            for (int i = 0; i < countOfMoves; i++)
            {
                switch (Convert.ToInt32(gameProcess[i, indexOfMenuItem]))
                {
                    case 1:
                        str += "\n\n" + gameProcess[i, indexOfFigureName] + " moves to " + gameProcess[i, indexOfCell] + "\n";
                        MoveFigure(chessField, blackFigures, whiteFigures, gameProcess[i, indexOfFigureName], gameProcess[i, indexOfCell]);
                        break;
                    case 2:
                        str += "\n\n" + gameProcess[i, indexOfFigureName] + " beats " + gameProcess[i, indexOfCell] + "\n";
                        BeatFigure(chessField, blackFigures, whiteFigures, gameProcess[i, indexOfFigureName], gameProcess[i, indexOfCell]);
                        break;
                    case 3:
                        str += "\n\nShort castling\n";
                        Castling(chessField, blackFigures, whiteFigures, "short");
                        break;
                    case 4:
                        str += "\n\nLong castling\n";
                        Castling(chessField, blackFigures, whiteFigures, "long");
                        break;
                    case 0:
                        str += "\n\nExit\n";
                        break;
                    default:
                        str += "\n\nThere is no such item in the menu\n";
                        break;
                }
                if (winnerMessage != "")
                {
                    str += winnerMessage + "\n";
                }
                if (Convert.ToInt32(gameProcess[i, indexOfMenuItem]) != 0)
                {
                    str += PrintChessField(chessField) + "\n";
                }
            }
            return str;
        }
        public static string PrintChessField(string[,] chessField)
        {
            string str = "";
            if (ChessFigure.ErrorMessage != null)
            {
                str += $"{ChessFigure.ErrorMessage}\n";
            }
            str += $"{shahMessage}";
            str += $"{pawnTransformationInfo}";
            str += "┌────┬────┬────┬────┬────┬────┬────┬────┐\n";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chessField[i, j] == null)
                    {
                        str += "│    ";
                    }
                    else
                    {
                        str += $"│{chessField[i, j]} ";
                    }
                }
                str += "│ " + (i + 1);
                if (i != 7)
                {
                    str += "\n├────┼────┼────┼────┼────┼────┼────┼────┤\n";
                }
            }
            str += "\n└────┴────┴────┴────┴────┴────┴────┴────┘\n";
            str += "  a    b    c    d    e    f    g    h\n";

            shahMessage = null;

            return str;
        }
        static void MoveFigure(string[,] chessField, List<ChessFigure> blackFigures, List<ChessFigure> whiteFigures, string shortName, string chessBoard)
        {
            ChessFigure.ErrorMessage = null;
            for (int i = 0; i < 16; i++)
            {
                if (blackFigures[i].ShortFigureName == shortName)
                {
                    if (!ChessFigure.IsWhiteShouldMove)
                    {
                        ChangePosition(chessField, blackFigures, chessBoard, shortName, i, whiteFigures);
                    }
                    else
                    {
                        ChessFigure.ErrorMessage = "White figures should move now";
                    }
                    break;
                }
                else if (whiteFigures[i].ShortFigureName == shortName)
                {
                    if (ChessFigure.IsWhiteShouldMove)
                    {
                        ChangePosition(chessField, whiteFigures, chessBoard, shortName, i, blackFigures);
                    }
                    else
                    {
                        ChessFigure.ErrorMessage = "Black figures should move now";
                    }
                    break;
                }
            }
        }
        static void BeatFigure(string[,] chessField, List<ChessFigure> blackFigures, List<ChessFigure> whiteFigures, string shortName, string chessBoard)
        {
            isBeat = true;
            MoveFigure(chessField, blackFigures, whiteFigures, shortName, chessBoard);
        }
        static void ChangePosition(string[,] chessField, List<ChessFigure> figures, string chessBoard, string shortName, int index, List<ChessFigure> otherFigures)
        {
            ChessFigure.ErrorMessage = null;
            int xPos = figures[index].HorizontalPosition;
            int yPos = figures[index].VerticalPosition;
            chessField[yPos, xPos] = null;
            const int indexOfKing = 0;

            if (isBeat)
            {
                figures[index].Beat(chessBoard);
                isBeat = false;
            }
            else
            {
                figures[index].Move(chessBoard);
            }

            if (figures[index].KindOfFigure.Equals("pawn") &&
               ((ChessFigure.IsWhiteShouldMove && figures[index].VerticalPosition == 7) ||
               (!ChessFigure.IsWhiteShouldMove && figures[index].VerticalPosition == 0)))
            {
                figures[index] = (ChessFigure)figures[index].Clone();
                pawnTransformationInfo += "\n" + figures[index].ShortFigureName + " - " + figures[index].KindOfFigure;
            }

            int newXPos = figures[index].HorizontalPosition;
            int newYPos = figures[index].VerticalPosition;
            chessField[newYPos, newXPos] = shortName;

            if (otherFigures[indexOfKing].ChessBoard.Equals(chessBoard) && ChessFigure.ErrorMessage == null)
            {
                if (ChessFigure.IsWhiteShouldMove)
                {
                    winnerMessage = "\nWHITE WON!!!\n";
                }
                else
                {
                    winnerMessage = "\nBLACK WON!!!\n";
                }
                return;
            }

            bool wasTheErrorMessage = false; 

            if (ChessFigure.ErrorMessage != null)
            {
                wasTheErrorMessage = true;
            }
            else
            {
                if (ChessFigure.IsWhiteShouldMove)
                {
                    ChessFigure.IsWhiteShouldMove = false;
                }
                else
                {
                    ChessFigure.IsWhiteShouldMove = true;
                }
            }

            bool catchErrorMessage = true;

            if (figures[index].CanDeclareCheck(otherFigures[indexOfKing]))
            {
                shahMessage = "Shah!\n";
                catchErrorMessage = false;
            }

            if (catchErrorMessage && !wasTheErrorMessage)
            {
                ChessFigure.ErrorMessage = null;
            }
        }
        static void Castling(string[,] chessField, List<ChessFigure> blackFigures, List<ChessFigure> whiteFigures, string castlingType)
        {
            if (!castlingType.Equals("short") && !castlingType.Equals("long"))
            {
                throw new Exception("Invalid value of type of castling");
            }

            if (!wasTheCastling)
            {
                int indexOfKing = 0;
                int indexOfRook = castlingType.Equals("short") ? 3 : 2;

                ChessFigure.ErrorMessage = null;
                if (ChessFigure.IsWhiteShouldMove)
                {
                    if (castlingType.Equals("short"))
                    {
                        ChessFigure.DoShortCastling("g1", "f1", whiteFigures[indexOfKing], whiteFigures[indexOfRook]);
                    }
                    else
                    {
                        ChessFigure.DoLongCastling("c1", "d1", "b1", whiteFigures[indexOfKing], whiteFigures[indexOfRook]);
                    }
                }
                else
                {
                    if (castlingType.Equals("short"))
                    {
                        ChessFigure.DoShortCastling("g8", "f8", blackFigures[indexOfKing], blackFigures[indexOfRook]);
                    }
                    else
                    {
                        ChessFigure.DoLongCastling("c8", "d8", "b8", blackFigures[indexOfKing], blackFigures[indexOfRook]);
                    }
                }
                if (ChessFigure.ErrorMessage == null)
                {
                    int oldKingXpos = 4, oldRookXpos, newKingXpos, newRookXpos;

                    if (castlingType.Equals("short"))
                    {
                        oldRookXpos = 7;
                        newKingXpos = 6;
                        newRookXpos = 5;
                    }
                    else
                    {
                        oldRookXpos = 0;
                        newKingXpos = 2;
                        newRookXpos = 3;
                    }

                    if (ChessFigure.IsWhiteShouldMove)
                    {
                        ChessFigure.IsWhiteShouldMove = false;
                        chessField[0, oldKingXpos] = null;
                        chessField[0, oldRookXpos] = null;
                        chessField[0, newKingXpos] = whiteFigures[indexOfKing].ShortFigureName;
                        chessField[0, newRookXpos] = whiteFigures[indexOfRook].ShortFigureName;
                    }
                    else
                    {
                        ChessFigure.IsWhiteShouldMove = true;
                        chessField[7, oldKingXpos] = null;
                        chessField[7, oldRookXpos] = null;
                        chessField[7, newKingXpos] = blackFigures[indexOfKing].ShortFigureName;
                        chessField[7, newRookXpos] = blackFigures[indexOfRook].ShortFigureName;
                    }
                    wasTheCastling = true;
                }
            }
            else
            {
                ChessFigure.ErrorMessage = "Castling can be done only once";
            }
        }
    }
}
