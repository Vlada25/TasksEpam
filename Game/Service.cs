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
        static string pawnTransformationInfo = "";
        static bool wasTheCastling = false;
        public static bool thePartyIsOver = false;
        public static void PrintChessField(string[,] chessField)
        {
            Console.WriteLine(pawnTransformationInfo);
            Console.WriteLine("┌────┬────┬────┬────┬────┬────┬────┬────┐");
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chessField[i, j] == null)
                    {
                        Console.Write("│    ");
                    }
                    else
                    {
                        Console.Write($"│{chessField[i, j]} ");
                    }
                }
                Console.Write("│ " + (i + 1));
                if (i != 7)
                {
                    Console.WriteLine("\n├────┼────┼────┼────┼────┼────┼────┼────┤");
                }
            }
            Console.WriteLine("\n└────┴────┴────┴────┴────┴────┴────┴────┘");
            Console.WriteLine("  a    b    c    d    e    f    g    h");
        }
        public static void MoveFigure(string[,] chessField, List<ChessFigure> blackFigures, List<ChessFigure> whiteFigures, string shortName, string chessBoard)
        {
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
                        Console.WriteLine("White figures should move now");
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
                        Console.WriteLine("Black figures should move now");
                    }
                    break;
                }
            }
        }
        public static void BeatFigure(string[,] chessField, List<ChessFigure> blackFigures, List<ChessFigure> whiteFigures, string shortName, string chessBoard)
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
                thePartyIsOver = true;
                if (ChessFigure.IsWhiteShouldMove)
                {
                    Console.WriteLine("\nWHITE WON!!!");
                }
                else
                {
                    Console.WriteLine("\nBLACK WON!!!");
                }
                return;
            }

            if (ChessFigure.ErrorMessage != null)
            {
                Console.WriteLine(ChessFigure.ErrorMessage);
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

            if (figures[index].CanDeclareCheck(otherFigures[indexOfKing]))
            {
                Console.WriteLine("\n Шах!");
            }
        }
        public static void Castling(string[,] chessField, List<ChessFigure> blackFigures, List<ChessFigure> whiteFigures, string castlingType)
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
                if (ChessFigure.ErrorMessage != null)
                {
                    Console.WriteLine(ChessFigure.ErrorMessage);
                }
                else
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
                }
                wasTheCastling = true;
            }
            else
            {
                Console.WriteLine("Castling can be done only once");
            }
        }
    }
}
