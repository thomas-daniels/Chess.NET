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
            ChessPiece kw = new ChessPiece(Pieces.King, Players.White);
            ChessPiece kb = new ChessPiece(Pieces.King, Players.Black);
            ChessPiece qw = new ChessPiece(Pieces.Queen, Players.White);
            ChessPiece qb = new ChessPiece(Pieces.Queen, Players.Black);
            ChessPiece rw = new ChessPiece(Pieces.Rook, Players.White);
            ChessPiece rb = new ChessPiece(Pieces.Rook, Players.Black);
            ChessPiece nw = new ChessPiece(Pieces.Knight, Players.White);
            ChessPiece nb = new ChessPiece(Pieces.Knight, Players.Black);
            ChessPiece bw = new ChessPiece(Pieces.Bishop, Players.White);
            ChessPiece bb = new ChessPiece(Pieces.Bishop, Players.Black);
            ChessPiece pw = new ChessPiece(Pieces.Pawn, Players.White);
            ChessPiece pb = new ChessPiece(Pieces.Pawn, Players.Black);
            ChessPiece o = ChessPiece.None;
            Assert.AreEqual(cb.Board[one, a], rw);
            Assert.AreEqual(cb.Board[one, b], nw);
            Assert.AreEqual(cb.Board[one, c], bw);
            Assert.AreEqual(cb.Board[one, d], qw);
            Assert.AreEqual(cb.Board[one, e], kw);
            Assert.AreEqual(cb.Board[one, f], bw);
            Assert.AreEqual(cb.Board[one, g], nw);
            Assert.AreEqual(cb.Board[one, h], rw);
            Assert.AreEqual(cb.Board[two, a], pw);
            Assert.AreEqual(cb.Board[two, b], pw);
            Assert.AreEqual(cb.Board[two, c], pw);
            Assert.AreEqual(cb.Board[two, d], pw);
            Assert.AreEqual(cb.Board[two, e], pw);
            Assert.AreEqual(cb.Board[two, f], pw);
            Assert.AreEqual(cb.Board[two, g], pw);
            Assert.AreEqual(cb.Board[two, h], pw);
            Assert.AreEqual(cb.Board[three, a], o);
            Assert.AreEqual(cb.Board[three, b], o);
            Assert.AreEqual(cb.Board[three, c], o);
            Assert.AreEqual(cb.Board[three, d], o);
            Assert.AreEqual(cb.Board[three, e], o);
            Assert.AreEqual(cb.Board[three, f], o);
            Assert.AreEqual(cb.Board[three, g], o);
            Assert.AreEqual(cb.Board[three, h], o);
            Assert.AreEqual(cb.Board[four, a], o);
            Assert.AreEqual(cb.Board[four, b], o);
            Assert.AreEqual(cb.Board[four, c], o);
            Assert.AreEqual(cb.Board[four, d], o);
            Assert.AreEqual(cb.Board[four, e], o);
            Assert.AreEqual(cb.Board[four, f], o);
            Assert.AreEqual(cb.Board[four, g], o);
            Assert.AreEqual(cb.Board[four, h], o);
            Assert.AreEqual(cb.Board[five, a], o);
            Assert.AreEqual(cb.Board[five, b], o);
            Assert.AreEqual(cb.Board[five, c], o);
            Assert.AreEqual(cb.Board[five, d], o);
            Assert.AreEqual(cb.Board[five, e], o);
            Assert.AreEqual(cb.Board[five, f], o);
            Assert.AreEqual(cb.Board[five, g], o);
            Assert.AreEqual(cb.Board[five, h], o);
            Assert.AreEqual(cb.Board[six, a], o);
            Assert.AreEqual(cb.Board[six, b], o);
            Assert.AreEqual(cb.Board[six, c], o);
            Assert.AreEqual(cb.Board[six, d], o);
            Assert.AreEqual(cb.Board[six, e], o);
            Assert.AreEqual(cb.Board[six, f], o);
            Assert.AreEqual(cb.Board[six, g], o);
            Assert.AreEqual(cb.Board[six, h], o);
            Assert.AreEqual(cb.Board[seven, a], pb);
            Assert.AreEqual(cb.Board[seven, b], pb);
            Assert.AreEqual(cb.Board[seven, c], pb);
            Assert.AreEqual(cb.Board[seven, d], pb);
            Assert.AreEqual(cb.Board[seven, e], pb);
            Assert.AreEqual(cb.Board[seven, f], pb);
            Assert.AreEqual(cb.Board[seven, g], pb);
            Assert.AreEqual(cb.Board[seven, h], pb);
            Assert.AreEqual(cb.Board[eight, a], rb);
            Assert.AreEqual(cb.Board[eight, b], nb);
            Assert.AreEqual(cb.Board[eight, c], bb);
            Assert.AreEqual(cb.Board[eight, d], qb);
            Assert.AreEqual(cb.Board[eight, e], kb);
            Assert.AreEqual(cb.Board[eight, f], bb);
            Assert.AreEqual(cb.Board[eight, g], nb);
            Assert.AreEqual(cb.Board[eight, h], rb);
        }
    }
}
