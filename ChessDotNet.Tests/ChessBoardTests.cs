using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public class ChessBoardTests
    {
        [Test]
        public void TestArrayGetting()
        {
            ChessBoard cb = new ChessBoard();
            int one = (int)Position.Ranks.One;
            int two = (int)Position.Ranks.Two;
            int three = (int)Position.Ranks.Three;
            int four = (int)Position.Ranks.Four;
            int five = (int)Position.Ranks.Five;
            int six = (int)Position.Ranks.Six;
            int seven = (int)Position.Ranks.Seven;
            int eight = (int)Position.Ranks.Eight;
            int a = (int)Position.Files.A;
            int b = (int)Position.Files.B;
            int c = (int)Position.Files.C;
            int d = (int)Position.Files.D;
            int e = (int)Position.Files.E;
            int f = (int)Position.Files.F;
            int g = (int)Position.Files.G;
            int h = (int)Position.Files.H;
            Assert.AreEqual(cb.Board[one, a], Pieces.Rook);
            Assert.AreEqual(cb.Board[one, b], Pieces.Knight);
            Assert.AreEqual(cb.Board[one, c], Pieces.Bishop);
            Assert.AreEqual(cb.Board[one, d], Pieces.Queen);
            Assert.AreEqual(cb.Board[one, e], Pieces.King);
            Assert.AreEqual(cb.Board[one, f], Pieces.Bishop);
            Assert.AreEqual(cb.Board[one, g], Pieces.Knight);
            Assert.AreEqual(cb.Board[one, h], Pieces.Rook);
            Assert.AreEqual(cb.Board[two, a], Pieces.Pawn);
            Assert.AreEqual(cb.Board[two, b], Pieces.Pawn);
            Assert.AreEqual(cb.Board[two, c], Pieces.Pawn);
            Assert.AreEqual(cb.Board[two, d], Pieces.Pawn);
            Assert.AreEqual(cb.Board[two, e], Pieces.Pawn);
            Assert.AreEqual(cb.Board[two, f], Pieces.Pawn);
            Assert.AreEqual(cb.Board[two, g], Pieces.Pawn);
            Assert.AreEqual(cb.Board[two, h], Pieces.Pawn);
            Assert.AreEqual(cb.Board[three, a], Pieces.None);
            Assert.AreEqual(cb.Board[three, b], Pieces.None);
            Assert.AreEqual(cb.Board[three, c], Pieces.None);
            Assert.AreEqual(cb.Board[three, d], Pieces.None);
            Assert.AreEqual(cb.Board[three, e], Pieces.None);
            Assert.AreEqual(cb.Board[three, f], Pieces.None);
            Assert.AreEqual(cb.Board[three, g], Pieces.None);
            Assert.AreEqual(cb.Board[three, h], Pieces.None);
            Assert.AreEqual(cb.Board[four, a], Pieces.None);
            Assert.AreEqual(cb.Board[four, b], Pieces.None);
            Assert.AreEqual(cb.Board[four, c], Pieces.None);
            Assert.AreEqual(cb.Board[four, d], Pieces.None);
            Assert.AreEqual(cb.Board[four, e], Pieces.None);
            Assert.AreEqual(cb.Board[four, f], Pieces.None);
            Assert.AreEqual(cb.Board[four, g], Pieces.None);
            Assert.AreEqual(cb.Board[four, h], Pieces.None);
            Assert.AreEqual(cb.Board[five, a], Pieces.None);
            Assert.AreEqual(cb.Board[five, b], Pieces.None);
            Assert.AreEqual(cb.Board[five, c], Pieces.None);
            Assert.AreEqual(cb.Board[five, d], Pieces.None);
            Assert.AreEqual(cb.Board[five, e], Pieces.None);
            Assert.AreEqual(cb.Board[five, f], Pieces.None);
            Assert.AreEqual(cb.Board[five, g], Pieces.None);
            Assert.AreEqual(cb.Board[five, h], Pieces.None);
            Assert.AreEqual(cb.Board[six, a], Pieces.None);
            Assert.AreEqual(cb.Board[six, b], Pieces.None);
            Assert.AreEqual(cb.Board[six, c], Pieces.None);
            Assert.AreEqual(cb.Board[six, d], Pieces.None);
            Assert.AreEqual(cb.Board[six, e], Pieces.None);
            Assert.AreEqual(cb.Board[six, f], Pieces.None);
            Assert.AreEqual(cb.Board[six, g], Pieces.None);
            Assert.AreEqual(cb.Board[six, h], Pieces.None);
            Assert.AreEqual(cb.Board[seven, a], Pieces.Pawn);
            Assert.AreEqual(cb.Board[seven, b], Pieces.Pawn);
            Assert.AreEqual(cb.Board[seven, c], Pieces.Pawn);
            Assert.AreEqual(cb.Board[seven, d], Pieces.Pawn);
            Assert.AreEqual(cb.Board[seven, e], Pieces.Pawn);
            Assert.AreEqual(cb.Board[seven, f], Pieces.Pawn);
            Assert.AreEqual(cb.Board[seven, g], Pieces.Pawn);
            Assert.AreEqual(cb.Board[seven, h], Pieces.Pawn);
            Assert.AreEqual(cb.Board[eight, a], Pieces.Rook);
            Assert.AreEqual(cb.Board[eight, b], Pieces.Knight);
            Assert.AreEqual(cb.Board[eight, c], Pieces.Bishop);
            Assert.AreEqual(cb.Board[eight, d], Pieces.Queen);
            Assert.AreEqual(cb.Board[eight, e], Pieces.King);
            Assert.AreEqual(cb.Board[eight, f], Pieces.Bishop);
            Assert.AreEqual(cb.Board[eight, g], Pieces.Knight);
            Assert.AreEqual(cb.Board[eight, h], Pieces.Rook);
        }
    }
}
