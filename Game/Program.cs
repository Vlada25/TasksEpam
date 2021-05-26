using System;
using ClassLibraryChess;
using ClassLibraryChess.FigureTypes;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            King b_King = new King("e8"),
                 w_King = new King("e1");
            Queen b_Queen = new Queen("d8"),
                  w_Queen = new Queen("d1");
            Rook b_Rook_1 = new Rook("a8"),
                 b_Rook_2 = new Rook("h8"),
                 w_Rook_1 = new Rook("a1"),
                 w_Rook_2 = new Rook("h1");
            Bishop b_Bishop_1 = new Bishop("c8"),
                   b_Bichop_2 = new Bishop("f8"),
                   w_Bishop_1 = new Bishop("c1"),
                   w_Bichop_2 = new Bishop("f1");
            Knight b_Knight_1 = new Knight("b8"),
                   b_Knight_2 = new Knight("g8"),
                   w_Knight_1 = new Knight("b1"),
                   w_Knight_2 = new Knight("g1");
            Pawn bp1 = new Pawn("a7"),
                 bp2 = new Pawn("b7"),
                 bp3 = new Pawn("c7"),
                 bp4 = new Pawn("d7"),
                 bp5 = new Pawn("e7"),
                 bp6 = new Pawn("f7"),
                 bp7 = new Pawn("g7"),
                 bp8 = new Pawn("h7"),
                 wp1 = new Pawn("a1"),
                 wp2 = new Pawn("b1"),
                 wp3 = new Pawn("c1"),
                 wp4 = new Pawn("d1"),
                 wp5 = new Pawn("e1"),
                 wp6 = new Pawn("f1"),
                 wp7 = new Pawn("g1"),
                 wp8 = new Pawn("h1");

            /*
            Console.WriteLine(b_King.ChessBoard);
            string comb = Console.ReadLine();
            try
            {
                b_King.Move(comb);
                Console.WriteLine(b_King.ChessBoard);
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
            */
        }
    }
}
