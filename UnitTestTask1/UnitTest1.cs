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
        public void CheckFigurePosition()
        {
            King king = new King("e8", "bk_", ChessFigure.Colors.Black, "king");
            string res = king.HorizontalPosition + " " + king.VerticalPosition;
            string expected = "4 7";

            Assert.AreEqual(res, expected);
        }
    }
}
