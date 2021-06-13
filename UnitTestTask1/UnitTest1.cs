using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ClassLibraryChess;
using ClassLibraryChess.FigureTypes;
using System.Collections.Generic;

namespace UnitTestTask1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FigurePositionTest()
        {
            King king = new King("e8", "bk_", ChessFigure.Colors.Black, "king");
            string res = king.HorizontalPosition + " " + king.VerticalPosition;
            string expected = "4 7";
            
            Assert.AreEqual(res, expected);
        }

        [TestMethod]
        public void ShortFigureNameTest()
        {
            Queen white_Queen = new Queen("d1", "wq_", ChessFigure.Colors.White, "queen");
            string expected = "wq_";

            Assert.AreEqual(white_Queen.ShortFigureName, expected);
        }

        [TestMethod]
        public void KindOfFigureTest()
        {
            Pawn pawn = new Pawn("c2", "wp3", ChessFigure.Colors.White, "pawn");
            string expected = "pawn";

            Assert.AreEqual(pawn.KindOfFigure, expected);
        }

        [TestMethod]
        public void ChessBoardTest()
        {
            Knight knight = new Knight("b8", "bk1", ChessFigure.Colors.Black, "knight");
            string expected = "b8";

            Assert.AreEqual(knight.ChessBoard, expected);
        }

        [TestMethod]
        public void PawnMoveTest_1()
        {
            Pawn pawn = new Pawn("e7", "bp5", ChessFigure.Colors.Black, "pawn");
            string expected = "e5";

            pawn.Move("e5");

            Assert.AreEqual(pawn.ChessBoard, expected);
        }

        [TestMethod]
        public void PawnMoveTest_2()
        {
            Pawn pawn = new Pawn("e7", "bp5", ChessFigure.Colors.Black, "pawn");
            string expected = "e6";

            pawn.Move("e6");

            Assert.AreEqual(pawn.ChessBoard, expected);
        }

        [TestMethod]
        public void PawnBeatTest()
        {
            Pawn black_Pawn = new Pawn("e7", "bp5", ChessFigure.Colors.Black, "pawn");
            Pawn white_Pawn = new Pawn("f2", "wp6", ChessFigure.Colors.White, "pawn");
            string expected = "f4";

            black_Pawn.Move("e5");
            white_Pawn.Move("f4");
            black_Pawn.Beat("f4");

            Assert.AreEqual(black_Pawn.ChessBoard, expected);
        }

        [TestMethod]
        public void KnightMoveTest()
        {
            Knight knight = new Knight("b8", "bk1", ChessFigure.Colors.Black, "knight");
            string expected = "c6";

            knight.Move("c6");

            Assert.AreEqual(knight.ChessBoard, expected);
        }

        [TestMethod]
        public void KnightBeatTest()
        {
            Knight black_Knight = new Knight("g8", "bk2", ChessFigure.Colors.Black, "knight");
            Pawn white_Pawn = new Pawn("f6", "wp6", ChessFigure.Colors.White, "pawn");

            string expected = "f6";

            black_Knight.Beat("f6");

            Assert.AreEqual(black_Knight.ChessBoard, expected);
        }

        [TestMethod]
        public void BishopMoveTest()
        {
            Bishop bishop = new Bishop("f8", "bb2", ChessFigure.Colors.Black, "bishop");
            string expected = "a3";

            bishop.Move("a3");

            Assert.AreEqual(bishop.ChessBoard, expected);
        }

        [TestMethod]
        public void BishopBeatTest()
        {
            Bishop black_Bishop = new Bishop("c8", "bb1", ChessFigure.Colors.Black, "bishop");
            Pawn white_Pawn = new Pawn("g4", "wp6", ChessFigure.Colors.White, "pawn");
            string expected = "g4";

            black_Bishop.Beat("g4");

            Assert.AreEqual(black_Bishop.ChessBoard, expected);
        }

        [TestMethod]
        public void RookMoveTest()
        {
            Rook rook = new Rook("a8", "br1", ChessFigure.Colors.Black, "rook");
            string expected = "a6";

            rook.Move("a6");

            Assert.AreEqual(rook.ChessBoard, expected);
        }

        [TestMethod]
        public void RookBeatTest()
        {
            Rook black_Rook = new Rook("h4", "br2", ChessFigure.Colors.Black, "rook");
            Pawn white_Pawn = new Pawn("d4", "wp6", ChessFigure.Colors.White, "pawn");
            string expected = "d4";

            black_Rook.Beat("d4");

            Assert.AreEqual(black_Rook.ChessBoard, expected);
        }

        [TestMethod]
        public void QueenMoveTest_1()
        {
            Queen queen = new Queen("d1", "wq_", ChessFigure.Colors.White, "queen");
            string expected = "d5";

            queen.Move("d5");

            Assert.AreEqual(queen.ChessBoard, expected);
        }

        [TestMethod]
        public void QueenMoveTest_2()
        {
            Queen queen = new Queen("d1", "wq_", ChessFigure.Colors.White, "queen");
            string expected = "f3";

            queen.Move("f3");

            Assert.AreEqual(queen.ChessBoard, expected);
        }

        [TestMethod]
        public void QueenBeatTest()
        {
            Queen black_Queen = new Queen("d8", "bq_", ChessFigure.Colors.Black, "queen");
            Pawn white_Pawn = new Pawn("b6", "wp6", ChessFigure.Colors.White, "pawn");
            string expected = "b6";

            black_Queen.Beat("b6");

            Assert.AreEqual(black_Queen.ChessBoard, expected);
        }

        [TestMethod]
        public void KingMoveTest()
        {
            King king = new King("h6", "wk_", ChessFigure.Colors.White, "king");
            string expected = "h5";

            king.Move("h5");

            Assert.AreEqual(king.ChessBoard, expected);
        }

        [TestMethod]
        public void KingBeatTest()
        {
            King black_King = new King("e8", "bk_", ChessFigure.Colors.Black, "king");
            Pawn white_Pawn = new Pawn("f7", "wp6", ChessFigure.Colors.White, "pawn");
            string expected = "f7";

            black_King.Beat("f7");

            Assert.AreEqual(black_King.ChessBoard, expected);
        }

        [TestMethod]
        public void ShortCastlingTest()
        {
            King king = new King("e8", "bk_", ChessFigure.Colors.Black, "king");
            Rook rook = new Rook("h8", "br2", ChessFigure.Colors.Black, "rook");
            ChessFigure.DoShortCastling("g8", "f8", king, rook);
            string res = king.ChessBoard + " " + rook.ChessBoard;
            string expected = "g8 f8";

            Assert.AreEqual(res, expected);
        }

        [TestMethod]
        public void LongCastlingTest()
        {
            King king = new King("e1", "bk_", ChessFigure.Colors.White, "king");
            Rook rook = new Rook("h1", "br2", ChessFigure.Colors.White, "rook");
            ChessFigure.DoLongCastling("c1", "d1", "b1", king, rook);
            string res = king.ChessBoard + " " + rook.ChessBoard;
            string expected = "c1 d1";

            Assert.AreEqual(res, expected);
        }
    }
}
