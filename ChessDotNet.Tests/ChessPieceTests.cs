using NUnit.Framework;

namespace ChessDotNet.Tests
{
    using Pieces;

    [TestFixture]
    public static class ChessPieceTests
    {
        [Test]
        public static void TestEquality()
        {
            Piece piece1 = new King(Player.White);
            Piece piece2 = new King(Player.White);
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
            Piece piece1 = new King(Player.White);
            Piece piece2 = new King(Player.Black);
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
            Piece piece1 = new King(Player.Black);
            Piece piece2 = new Queen(Player.Black);
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
            Piece piece1 = new King(Player.White);
            Piece piece2 = new Queen(Player.Black);
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
            Piece piece1 = new Rook(Player.White);
            Piece piece2 = null;
            Assert.AreNotEqual(piece1, piece2, "piece1 and piece2 are equal");
            Assert.False(piece1.Equals(piece2), "piece1.Equals(piece2) should be false");
            Assert.False(piece1 == piece2, "piece1 == piece2 should be false");
            Assert.False(piece2 == piece1, "piece2 == piece1 should be false");
            Assert.True(piece1 != piece2, "piece1 != piece2 should be True");
            Assert.True(piece2 != piece1, "piece2 != piece1 should be True");

            piece1 = null;
            piece2 = new Bishop(Player.Black);
            Assert.AreNotEqual(piece1, piece2, "piece1 and piece2 are equal");
            Assert.False(piece2.Equals(piece1), "piece1.Equals(piece2) should be false");
            Assert.False(piece1 == piece2, "piece1 == piece2 should be false");
            Assert.False(piece2 == piece1, "piece2 == piece1 should be false");
            Assert.True(piece1 != piece2, "piece1 != piece2 should be True");
            Assert.True(piece2 != piece1, "piece2 != piece1 should be True");
        }
    }
}
