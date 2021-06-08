using System;
using System.Collections.Generic;
using ClassLibraryChess;
using ClassLibraryChess.FigureTypes;

namespace ConsoleApp1
{
    class Program
    {
        static bool isBeat = false;
        static string pawnTransformationInfo = "";
        static bool wasTheCastling = false;
        static void Main(string[] args)
        {
            try
            {
                King black_King = new King("e8", "bk_", ChessFigure.Colors.Black, "king"),
                     white_King = new King("e1", "wk_", ChessFigure.Colors.White, "king");

                Queen black_Queen = new Queen("d8", "bq_", ChessFigure.Colors.Black, "queen"),
                      white_Queen = new Queen("d3", "wq_", ChessFigure.Colors.White, "queen");

                Rook black_Rook_1 = new Rook("a8", "br1", ChessFigure.Colors.Black, "rook"),
                     black_Rook_2 = new Rook("h8", "br2", ChessFigure.Colors.Black, "rook"),
                     white_Rook_1 = new Rook("a1", "wr1", ChessFigure.Colors.White, "rook"),
                     white_Rook_2 = new Rook("h1", "wr2", ChessFigure.Colors.White, "rook");

                Bishop black_Bishop_1 = new Bishop("c8", "bb1", ChessFigure.Colors.Black, "bishop"),
                       black_Bishop_2 = new Bishop("f8", "bb2", ChessFigure.Colors.Black, "bishop"),
                       white_Bishop_1 = new Bishop("c3", "wb1", ChessFigure.Colors.White, "bishop"),
                       white_Bishop_2 = new Bishop("f1", "wb2", ChessFigure.Colors.White, "bishop");

                Knight black_Knight_1 = new Knight("b8", "bk1", ChessFigure.Colors.Black, "knight"),
                       black_Knight_2 = new Knight("g8", "bk2", ChessFigure.Colors.Black, "knight"),
                       white_Knight_1 = new Knight("b3", "wk1", ChessFigure.Colors.White, "knight"),
                       white_Knight_2 = new Knight("g1", "wk2", ChessFigure.Colors.White, "knight");

                Pawn black_Pawn_1 = new Pawn("a7", "bp1", ChessFigure.Colors.Black, "pawn"),
                     black_Pawn_2 = new Pawn("b7", "bp2", ChessFigure.Colors.Black, "pawn"),
                     black_Pawn_3 = new Pawn("c7", "bp3", ChessFigure.Colors.Black, "pawn"),
                     black_Pawn_4 = new Pawn("d7", "bp4", ChessFigure.Colors.Black, "pawn"),
                     black_Pawn_5 = new Pawn("e7", "bp5", ChessFigure.Colors.Black, "pawn"),
                     black_Pawn_6 = new Pawn("f7", "bp6", ChessFigure.Colors.Black, "pawn"),
                     black_Pawn_7 = new Pawn("g7", "bp7", ChessFigure.Colors.Black, "pawn"),
                     black_Pawn_8 = new Pawn("h7", "bp8", ChessFigure.Colors.Black, "pawn"),
                     white_Pawn_1 = new Pawn("a2", "wp1", ChessFigure.Colors.White, "pawn"),
                     white_Pawn_2 = new Pawn("b2", "wp2", ChessFigure.Colors.White, "pawn"),
                     white_Pawn_3 = new Pawn("c2", "wp3", ChessFigure.Colors.White, "pawn"),
                     white_Pawn_4 = new Pawn("d2", "wp4", ChessFigure.Colors.White, "pawn"),
                     white_Pawn_5 = new Pawn("e2", "wp5", ChessFigure.Colors.White, "pawn"),
                     white_Pawn_6 = new Pawn("f2", "wp6", ChessFigure.Colors.White, "pawn"),
                     white_Pawn_7 = new Pawn("g2", "wp7", ChessFigure.Colors.White, "pawn"),
                     white_Pawn_8 = new Pawn("h2", "wp8", ChessFigure.Colors.White, "pawn");

                List<ChessFigure> blackFigures = new List<ChessFigure>();
                blackFigures.AddRange(new ChessFigure[] { black_King, black_Queen,
                black_Rook_1, black_Rook_2,
                black_Bishop_1, black_Bishop_2,
                black_Knight_1, black_Knight_2,
                black_Pawn_1, black_Pawn_2, black_Pawn_3, black_Pawn_4,
                black_Pawn_5, black_Pawn_6, black_Pawn_7, black_Pawn_8 });

                List<ChessFigure> whiteFigures = new List<ChessFigure>();
                whiteFigures.AddRange(new ChessFigure[] { white_King, white_Queen,
                white_Rook_1, white_Rook_2,
                white_Bishop_1, white_Bishop_2,
                white_Knight_1, white_Knight_2,
                white_Pawn_1, white_Pawn_2, white_Pawn_3, white_Pawn_4,
                white_Pawn_5, white_Pawn_6, white_Pawn_7, white_Pawn_8 });

                string[,] chessField = new string[8, 8];
                ChessFigure.IsWhiteShouldMove = true;

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

                int selector = -1;
                while (selector != 0)
                {
                    PrintChessField(chessField);
                    Console.WriteLine("\nВыберите операцию:\n1 - Ходить\n2 - Бить" +
                        "              \n3 - Короткая рокировка\n4 - Длинная рокировка\n0 - Выход");
                    int selecter = Convert.ToInt32(Console.ReadLine());
                    switch (selecter)
                    {
                        case 1:
                            MoveFigure(chessField, blackFigures, whiteFigures);
                            break;
                        case 2:
                            BeatFigure(chessField, blackFigures, whiteFigures);
                            break;
                        case 3:
                            Castling(chessField, blackFigures, whiteFigures, "short");
                            break;
                        case 4:
                            Castling(chessField, blackFigures, whiteFigures, "long");
                            break;
                        case 0:
                            Console.WriteLine("...");
                            break;
                    }
                }
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
       }
        static void PrintChessField(string[,] chessField)
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
                Console.Write("│ " + (i+1));
                if (i != 7)
                {
                    Console.WriteLine("\n├────┼────┼────┼────┼────┼────┼────┼────┤");
                }
            }
            Console.WriteLine("\n└────┴────┴────┴────┴────┴────┴────┴────┘");
            Console.WriteLine("  a    b    c    d    e    f    g    h");
        }
        static void MoveFigure(string[,] chessField, List<ChessFigure> blackFigures, List<ChessFigure> whiteFigures)
        {
            Console.WriteLine("\nВведите краткое название фигуры:");
            string shortName = Console.ReadLine();
            Console.WriteLine("Введите клетку шахматного поля:");
            string chessBoard = Console.ReadLine();
            for (int i = 0; i < 16; i++)
            {
                if (blackFigures[i].ShortFigureName == shortName)
                {
                    if (!ChessFigure.IsWhiteShouldMove)
                    {
                        ChangePosition(chessField, blackFigures, chessBoard, shortName, i);
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
                        ChangePosition(chessField, whiteFigures, chessBoard, shortName, i);
                    }
                    else
                    {
                        Console.WriteLine("Black figures should move now");
                    }
                    break;
                }
            }
        }
        static void BeatFigure(string[,] chessField, List<ChessFigure> blackFigures, List<ChessFigure> whiteFigures)
        {
            isBeat = true;
            MoveFigure(chessField, blackFigures, whiteFigures);
        }
        static void ChangePosition(string[,] chessField, List<ChessFigure> figures, string chessBoard, string shortName, int index)
        {
            ChessFigure.ErrorMessage = null;
            int xPos = figures[index].HorizontalPosition;
            int yPos = figures[index].VerticalPosition;
            chessField[yPos, xPos] = null;
            if (isBeat)
            {
                figures[index].Beat(chessBoard);
            }
            else
            {
                figures[index].Move(chessBoard);
                if (figures[index].KindOfFigure.Equals("pawn") &&
                    ((ChessFigure.IsWhiteShouldMove && figures[index].VerticalPosition == 7) ||
                    (!ChessFigure.IsWhiteShouldMove && figures[index].VerticalPosition == 0)))
                {
                    figures[index] = (ChessFigure)figures[index].Clone();
                    pawnTransformationInfo += "\n" + figures[index].ShortFigureName + " - " + figures[index].KindOfFigure;
                }
            }
            int newXPos = figures[index].HorizontalPosition;
            int newYPos = figures[index].VerticalPosition;
            chessField[newYPos, newXPos] = shortName;
            if(ChessFigure.ErrorMessage != null)
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
                        ChessFigure.DoShortCastling("f1", "g1", whiteFigures[indexOfKing], whiteFigures[indexOfRook]);
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
                        ChessFigure.DoShortCastling("f8", "g8", whiteFigures[indexOfKing], whiteFigures[indexOfRook]);
                    }
                    else
                    {
                        ChessFigure.DoLongCastling("c8", "d8", "b8", whiteFigures[indexOfKing], whiteFigures[indexOfRook]);
                    }
                }
                if (ChessFigure.ErrorMessage != null)
                {
                    Console.WriteLine(ChessFigure.ErrorMessage);
                }
                else
                {
                    int oldKingXpos = 4, oldRookXpos, newKingXpos, newRookXpos;

                    if (castlingType.Equals("shoret"))
                    {
                        oldRookXpos = 7;
                        newKingXpos = 5;
                        newRookXpos = 6;
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
