using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public static class ChessPieceTests
    {
        [Test]
        public static void TestEquality()
        {
            ChessPiece piece1 = new ChessPiece(Piece.King, Player.White);
            ChessPiece piece2 = new ChessPiece(Piece.King, Player.White);
            Assert.AreEqual(piece1, piece2, "piece1 and piece2 are not equal");
            Assert.True(piece1.Equals(piece2), "piece1.Equals(piece2) should be True");
            Assert.True(piece2.Equals(piece1), "piece2.Equals(piece1) should be True");
            Assert.True(piece1 == piece2, "piece1 == piece2 should be True");
            Assert.True(piece2 == piece1, "piece2 == piece1 should be True");
            Assert.False(piece1 != piece2, "piece1 != piece2 should be false");
            Assert.False(piece2 != piece1, "piece2 != piece1 should be false");
            Assert.AreEqual(piece1.GetHashCode(), piece2.GetHashCode(), "Hash codes are different");
        }

        [Test]
        public static void TestInequality_DifferentPlayer()
        {
            ChessPiece piece1 = new ChessPiece(Piece.King, Player.White);
            ChessPiece piece2 = new ChessPiece(Piece.King, Player.Black);
            Assert.AreNotEqual(piece1, piece2, "piece1 and piece2 are equal");
            Assert.False(piece1.Equals(piece2), "piece1.Equals(piece2) should be false");
            Assert.False(piece2.Equals(piece1), "piece2.Equals(piece1) should be false");
            Assert.False(piece1 == piece2, "piece1 == piece2 should be false");
            Assert.False(piece2 == piece1, "piece2 == piece1 should be false");
            Assert.True(piece1 != piece2, "piece1 != piece2 should be True");
            Assert.True(piece2 != piece1, "piece2 != piece1 should be True");
            Assert.AreNotEqual(piece1.GetHashCode(), piece2.GetHashCode(), "Hash codes are equal");
        }

        [Test]
        public static void TestInequality_DifferentPiece()
        {
            ChessPiece piece1 = new ChessPiece(Piece.King, Player.Black);
            ChessPiece piece2 = new ChessPiece(Piece.Queen, Player.Black);
            Assert.AreNotEqual(piece1, piece2, "piece1 and piece2 are equal");
            Assert.False(piece1.Equals(piece2), "piece1.Equals(piece2) should be false");
            Assert.False(piece2.Equals(piece1), "piece2.Equals(piece1) should be false");
            Assert.False(piece1 == piece2, "piece1 == piece2 should be false");
            Assert.False(piece2 == piece1, "piece2 == piece1 should be false");
            Assert.True(piece1 != piece2, "piece1 != piece2 should be True");
            Assert.True(piece2 != piece1, "piece2 != piece1 should be True");
            Assert.AreNotEqual(piece1.GetHashCode(), piece2.GetHashCode(), "Hash codes are equal");
        }

        [Test]
        public static void TestInequality_DifferentPieceAndPlayer()
        {
            ChessPiece piece1 = new ChessPiece(Piece.King, Player.White);
            ChessPiece piece2 = new ChessPiece(Piece.Queen, Player.Black);
            Assert.AreNotEqual(piece1, piece2, "piece1 and piece2 are equal");
            Assert.False(piece1.Equals(piece2), "piece1.Equals(piece2) should be false");
            Assert.False(piece2.Equals(piece1), "piece2.Equals(piece1) should be false");
            Assert.False(piece1 == piece2, "piece1 == piece2 should be false");
            Assert.False(piece2 == piece1, "piece2 == piece1 should be false");
            Assert.True(piece1 != piece2, "piece1 != piece2 should be True");
            Assert.True(piece2 != piece1, "piece2 != piece1 should be True");
            Assert.AreNotEqual(piece1.GetHashCode(), piece2.GetHashCode(), "Hash codes are equal");
        }

        [Test]
        public static void TestInEquality_OneIsNull()
        {
            ChessPiece piece1 = new ChessPiece(Piece.Rook, Player.White);
            ChessPiece piece2 = null;
            Assert.AreNotEqual(piece1, piece2, "piece1 and piece2 are equal");
            Assert.False(piece1.Equals(piece2), "piece1.Equals(piece2) should be false");
            Assert.False(piece1 == piece2, "piece1 == piece2 should be false");
            Assert.False(piece2 == piece1, "piece2 == piece1 should be false");
            Assert.True(piece1 != piece2, "piece1 != piece2 should be True");
            Assert.True(piece2 != piece1, "piece2 != piece1 should be True");

            piece1 = null;
            piece2 = new ChessPiece(Piece.Bishop, Player.Black);
            Assert.AreNotEqual(piece1, piece2, "piece1 and piece2 are equal");
            Assert.False(piece2.Equals(piece1), "piece1.Equals(piece2) should be false");
            Assert.False(piece1 == piece2, "piece1 == piece2 should be false");
            Assert.False(piece2 == piece1, "piece2 == piece1 should be false");
            Assert.True(piece1 != piece2, "piece1 != piece2 should be True");
            Assert.True(piece2 != piece1, "piece2 != piece1 should be True");
        }
    }
}
