using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public class ChessPieceTests
    {
        [Test]
        public void TestEquality()
        {
            ChessPiece piece1 = new ChessPiece(Pieces.King, Players.White);
            ChessPiece piece2 = new ChessPiece(Pieces.King, Players.White);
            Assert.AreEqual(piece1, piece2, "piece1 and piece2 are not equal");
            Assert.True(piece1.Equals(piece2), "piece1.Equals(piece2) is False");
            Assert.True(piece2.Equals(piece1), "piece2.Equals(piece1) is False");
            Assert.True(piece1 == piece2, "piece1 == piece2 is False");
            Assert.True(piece2 == piece1, "piece2 == piece1 is False");
            Assert.False(piece1 != piece2, "piece1 != piece2 is True");
            Assert.False(piece2 != piece1, "piece2 != piece1 is True");
            Assert.AreEqual(piece1.GetHashCode(), piece2.GetHashCode(), "Hash codes are different");
        }

        [Test]
        public void TestInequality_DifferentPlayer()
        {
            ChessPiece piece1 = new ChessPiece(Pieces.King, Players.White);
            ChessPiece piece2 = new ChessPiece(Pieces.King, Players.Black);
            Assert.AreNotEqual(piece1, piece2, "piece1 and piece2 are equal");
            Assert.False(piece1.Equals(piece2), "piece1.Equals(piece2) is True");
            Assert.False(piece2.Equals(piece1), "piece2.Equals(piece1) is True");
            Assert.False(piece1 == piece2, "piece1 == piece2 is True");
            Assert.False(piece2 == piece1, "piece2 == piece1 is True");
            Assert.True(piece1 != piece2, "piece1 != piece2 is False");
            Assert.True(piece2 != piece1, "piece2 != piece1 is False");
            Assert.AreNotEqual(piece1.GetHashCode(), piece2.GetHashCode(), "Hash codes are equal");
        }

        [Test]
        public void TestInequality_DifferentPiece()
        {
            ChessPiece piece1 = new ChessPiece(Pieces.King, Players.Black);
            ChessPiece piece2 = new ChessPiece(Pieces.Queen, Players.Black);
            Assert.AreNotEqual(piece1, piece2, "piece1 and piece2 are equal");
            Assert.False(piece1.Equals(piece2), "piece1.Equals(piece2) is True");
            Assert.False(piece2.Equals(piece1), "piece2.Equals(piece1) is True");
            Assert.False(piece1 == piece2, "piece1 == piece2 is True");
            Assert.False(piece2 == piece1, "piece2 == piece1 is True");
            Assert.True(piece1 != piece2, "piece1 != piece2 is False");
            Assert.True(piece2 != piece1, "piece2 != piece1 is False");
            Assert.AreNotEqual(piece1.GetHashCode(), piece2.GetHashCode(), "Hash codes are equal");
        }

        [Test]
        public void TestInequality_DifferentPieceAndPlayer()
        {
            ChessPiece piece1 = new ChessPiece(Pieces.King, Players.White);
            ChessPiece piece2 = new ChessPiece(Pieces.Queen, Players.Black);
            Assert.AreNotEqual(piece1, piece2, "piece1 and piece2 are equal");
            Assert.False(piece1.Equals(piece2), "piece1.Equals(piece2) is True");
            Assert.False(piece2.Equals(piece1), "piece2.Equals(piece1) is True");
            Assert.False(piece1 == piece2, "piece1 == piece2 is True");
            Assert.False(piece2 == piece1, "piece2 == piece1 is True");
            Assert.True(piece1 != piece2, "piece1 != piece2 is False");
            Assert.True(piece2 != piece1, "piece2 != piece1 is False");
            Assert.AreNotEqual(piece1.GetHashCode(), piece2.GetHashCode(), "Hash codes are equal");
        }
    }
}
