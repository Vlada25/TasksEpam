using System;
using System.Collections.Generic;
using ClassLibraryChess;
using ClassLibraryChess.FigureTypes;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                King black_King = new King("e8", "bk_", ChessFigure.Colors.Black, "king"),
                     white_King = new King("e1", "wk_", ChessFigure.Colors.White, "king");

                Queen black_Queen = new Queen("d8", "bq_", ChessFigure.Colors.Black, "queen"),
                      white_Queen = new Queen("d1", "wq_", ChessFigure.Colors.White, "queen");

                Rook black_Rook_1 = new Rook("a8", "br1", ChessFigure.Colors.Black, "rook"),
                     black_Rook_2 = new Rook("h8", "br2", ChessFigure.Colors.Black, "rook"),
                     white_Rook_1 = new Rook("a1", "wr1", ChessFigure.Colors.White, "rook"),
                     white_Rook_2 = new Rook("h1", "wr2", ChessFigure.Colors.White, "rook");

                Bishop black_Bishop_1 = new Bishop("c8", "bb1", ChessFigure.Colors.Black, "bishop"),
                       black_Bishop_2 = new Bishop("f8", "bb2", ChessFigure.Colors.Black, "bishop"),
                       white_Bishop_1 = new Bishop("c1", "wb1", ChessFigure.Colors.White, "bishop"),
                       white_Bishop_2 = new Bishop("f1", "wb2", ChessFigure.Colors.White, "bishop");

                Knight black_Knight_1 = new Knight("b8", "bk1", ChessFigure.Colors.Black, "knight"),
                       black_Knight_2 = new Knight("g8", "bk2", ChessFigure.Colors.Black, "knight"),
                       white_Knight_1 = new Knight("b1", "wk1", ChessFigure.Colors.White, "knight"),
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

                string[,] gameProcess = { { "1", "wp4", "d4"},
                    { "1", "bp5", "e6"},
                    { "1", "wp5", "e4"},
                    { "1", "bp4", "d5"},
                    { "1", "wk1", "d2"},
                    { "1", "bp3", "c5"},
                    { "1", "wk2", "f3"},
                    { "2", "bp3", "d4"},
                    { "2", "wk2", "d4"},
                    { "1", "bk2", "f6"},
                    { "2", "wp5", "d5"},
                    { "2", "bq_", "d5"},
                    { "1", "wk2", "b5"},
                    { "1", "bk1", "a6"},
                    { "1", "wk2", "c3"},
                    { "1", "bq_", "d8"},
                    { "1", "wp1", "a3"},
                    { "1", "bb2", "e7"},
                    { "1", "wq_", "f3"},
                    { "3", "-", "-"},
                    { "2", "wb2", "a6"},
                    { "5", "-", "-"} };

                const int indexOfMenuItem = 0;
                const int indexOfFigureName = 1;
                const int indexOfCell = 2;

                int countOfMoves = gameProcess.Length / 3;

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

                Console.WriteLine(" Start the game");
                Service.PrintChessField(chessField); 

                for (int i = 0; i < countOfMoves; i++)
                {
                    switch (Convert.ToInt32(gameProcess[i, indexOfMenuItem]))
                    {
                        case 1:
                            Console.WriteLine("\n\n " + gameProcess[i, indexOfFigureName] + " ходит на " + gameProcess[i, indexOfCell]);
                            Service.MoveFigure(chessField, blackFigures, whiteFigures, gameProcess[i, indexOfFigureName], gameProcess[i, indexOfCell]);
                            break;
                        case 2:
                            Console.WriteLine("\n\n " + gameProcess[i, indexOfFigureName] + " бьёт " + gameProcess[i, indexOfCell]);
                            Service.BeatFigure(chessField, blackFigures, whiteFigures, gameProcess[i, indexOfFigureName], gameProcess[i, indexOfCell]);
                            break;
                        case 3:
                            Console.WriteLine("\n\n Короткая рокировка");
                            Service.Castling(chessField, blackFigures, whiteFigures, "short");
                            break;
                        case 4:
                            Console.WriteLine("\n\n Длинная рокировка");
                            Service.Castling(chessField, blackFigures, whiteFigures, "long");
                            break;
                        case 0:
                            Console.WriteLine("Exit");
                            break;
                    }
                    Service.PrintChessField(chessField);
                }
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
       }
    }
}
