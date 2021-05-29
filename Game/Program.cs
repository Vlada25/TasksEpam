using System;
using ClassLibraryChess;
using ClassLibraryChess.FigureTypes;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            King black_King = new King("e8", "bk_", ChessFigure.Colors.Black),
                 white_King = new King("e1", "wk_", ChessFigure.Colors.White);

            Queen black_Queen = new Queen("d8", "bq_", ChessFigure.Colors.Black),
                  white_Queen = new Queen("d1", "wq_", ChessFigure.Colors.White);

            Rook black_Rook_1 = new Rook("a8", "br1", ChessFigure.Colors.Black),
                 black_Rook_2 = new Rook("h8", "br2", ChessFigure.Colors.Black),
                 white_Rook_1 = new Rook("a1", "wr1", ChessFigure.Colors.White),
                 white_Rook_2 = new Rook("h1", "wr2", ChessFigure.Colors.White);

            Bishop black_Bishop_1 = new Bishop("c8", "bs1", ChessFigure.Colors.Black),
                   black_Bishop_2 = new Bishop("f8", "bs2", ChessFigure.Colors.Black),
                   white_Bishop_1 = new Bishop("c1", "ws1", ChessFigure.Colors.White),
                   white_Bishop_2 = new Bishop("f1", "ws2", ChessFigure.Colors.White);

            Knight black_Knight_1 = new Knight("b8", "bk1", ChessFigure.Colors.Black),
                   black_Knight_2 = new Knight("g8", "bk2", ChessFigure.Colors.Black),
                   white_Knight_1 = new Knight("b1", "wk1", ChessFigure.Colors.White),
                   white_Knight_2 = new Knight("g1", "wk2", ChessFigure.Colors.White);

            Pawn black_Pawn_1 = new Pawn("a7", "bp1", ChessFigure.Colors.Black),
                 black_Pawn_2 = new Pawn("b7", "bp2", ChessFigure.Colors.Black),
                 black_Pawn_3 = new Pawn("c7", "bp3", ChessFigure.Colors.Black),
                 black_Pawn_4 = new Pawn("d7", "bp4", ChessFigure.Colors.Black),
                 black_Pawn_5 = new Pawn("e7", "bp5", ChessFigure.Colors.Black),
                 black_Pawn_6 = new Pawn("f7", "bp6", ChessFigure.Colors.Black),
                 black_Pawn_7 = new Pawn("g7", "bp7", ChessFigure.Colors.Black),
                 black_Pawn_8 = new Pawn("h7", "bp8", ChessFigure.Colors.Black),
                 white_Pawn_1 = new Pawn("a2", "wp1", ChessFigure.Colors.White),
                 white_Pawn_2 = new Pawn("b2", "wp2", ChessFigure.Colors.White),
                 white_Pawn_3 = new Pawn("c2", "wp3", ChessFigure.Colors.White),
                 white_Pawn_4 = new Pawn("d2", "wp4", ChessFigure.Colors.White),
                 white_Pawn_5 = new Pawn("e2", "wp5", ChessFigure.Colors.White),
                 white_Pawn_6 = new Pawn("f2", "wp6", ChessFigure.Colors.White),
                 white_Pawn_7 = new Pawn("g2", "wp7", ChessFigure.Colors.White),
                 white_Pawn_8 = new Pawn("h2", "wp8", ChessFigure.Colors.White);

            ChessFigure[] blackFigures = { black_King, black_Queen,
                black_Rook_1, black_Rook_2,
                black_Bishop_1, black_Bishop_2,
                black_Knight_1, black_Knight_2,
                black_Pawn_1, black_Pawn_2, black_Pawn_3, black_Pawn_4,
                black_Pawn_5, black_Pawn_6, black_Pawn_7, black_Pawn_8 };

            ChessFigure[] whiteFigures = { white_King, white_Queen,
                white_Rook_1, white_Rook_2,
                white_Bishop_1, white_Bishop_2,
                white_Knight_1, white_Knight_2,
                white_Pawn_1, white_Pawn_2, white_Pawn_3, white_Pawn_4,
                white_Pawn_5, white_Pawn_6, white_Pawn_7, white_Pawn_8 };

            string[,] chessField = new string[8, 8];
            try
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

                while (true)
                {
                    PrintChessField(chessField);
                    MoveFigure(chessField, blackFigures, whiteFigures);
                }
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
        static void PrintChessField(string[,] chessField)
        {
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
            Console.WriteLine("  a    b    c    d    e    f    h    g");
        }
        static void MoveFigure(string[,] chessField, ChessFigure[] blackFigures, ChessFigure[] whiteFigures)
        {
            Console.WriteLine("\nВведите краткое название фигуры:");
            string shortName = Console.ReadLine();
            Console.WriteLine("Введите клетку шахматного поля:");
            string chessBoard = Console.ReadLine();
            for (int i = 0; i < 16; i++)
            {
                if (blackFigures[i].ShortFigureName == shortName)
                {
                    ChangePosition(chessField, blackFigures, chessBoard, shortName, i);
                    break;
                }
                else if (whiteFigures[i].ShortFigureName == shortName)
                {
                    ChangePosition(chessField, whiteFigures, chessBoard, shortName, i);
                    break;
                }
            }
        }
        static void ChangePosition(string[,] chessField, ChessFigure[] figures, string chessBoard, string shortName, int index)
        {
            int xPos = figures[index].HorizontalPosition;
            int yPos = figures[index].VerticalPosition;
            chessField[yPos, xPos] = null;

            figures[index].Move(chessBoard);

            int newXPos = figures[index].HorizontalPosition;
            int newYPos = figures[index].VerticalPosition;
            chessField[newYPos, newXPos] = shortName;
        }
    }
}
