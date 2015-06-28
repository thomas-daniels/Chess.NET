using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public class ChessPieceTests
    {
        [Test]
        public void TestEquality()
        {
            ChessPiece c1 = new ChessPiece(Pieces.King, Players.White);
            ChessPiece c2 = new ChessPiece(Pieces.King, Players.White);
            Assert.AreEqual(c1, c2, "c1 and c2 are not equal");
            Assert.True(c1.Equals(c2), "c1.Equals(c2) is False");
            Assert.True(c2.Equals(c1), "c2.Equals(c1) is False");
            Assert.True(c1 == c2, "c1 == c2 is False");
            Assert.True(c2 == c1, "c2 == c1 is False");
            Assert.False(c1 != c2, "c1 != c2 is True");
            Assert.False(c2 != c1, "c2 != c1 is True");
            Assert.AreEqual(c1.GetHashCode(), c2.GetHashCode(), "Hash codes are different");
        }

        [Test]
        public void TestInequality_DifferentPlayer()
        {
            ChessPiece c1 = new ChessPiece(Pieces.King, Players.White);
            ChessPiece c2 = new ChessPiece(Pieces.King, Players.Black);
            Assert.AreNotEqual(c1, c2, "c1 and c2 are equal");
            Assert.False(c1.Equals(c2), "c1.Equals(c2) is True");
            Assert.False(c2.Equals(c1), "c2.Equals(c1) is True");
            Assert.False(c1 == c2, "c1 == c2 is True");
            Assert.False(c2 == c1, "c2 == c1 is True");
            Assert.True(c1 != c2, "c1 != c2 is False");
            Assert.True(c2 != c1, "c2 != c1 is False");
            Assert.AreNotEqual(c1.GetHashCode(), c2.GetHashCode(), "Hash codes are equal");
        }

        [Test]
        public void TestInequality_DifferentPiece()
        {
            ChessPiece c1 = new ChessPiece(Pieces.King, Players.Black);
            ChessPiece c2 = new ChessPiece(Pieces.Queen, Players.Black);
            Assert.AreNotEqual(c1, c2, "c1 and c2 are equal");
            Assert.False(c1.Equals(c2), "c1.Equals(c2) is True");
            Assert.False(c2.Equals(c1), "c2.Equals(c1) is True");
            Assert.False(c1 == c2, "c1 == c2 is True");
            Assert.False(c2 == c1, "c2 == c1 is True");
            Assert.True(c1 != c2, "c1 != c2 is False");
            Assert.True(c2 != c1, "c2 != c1 is False");
            Assert.AreNotEqual(c1.GetHashCode(), c2.GetHashCode(), "Hash codes are equal");
        }

        [Test]
        public void TestInequality_DifferentPieceAndPlayer()
        {
            ChessPiece c1 = new ChessPiece(Pieces.King, Players.White);
            ChessPiece c2 = new ChessPiece(Pieces.Queen, Players.Black);
            Assert.AreNotEqual(c1, c2, "c1 and c2 are equal");
            Assert.False(c1.Equals(c2), "c1.Equals(c2) is True");
            Assert.False(c2.Equals(c1), "c2.Equals(c1) is True");
            Assert.False(c1 == c2, "c1 == c2 is True");
            Assert.False(c2 == c1, "c2 == c1 is True");
            Assert.True(c1 != c2, "c1 != c2 is False");
            Assert.True(c2 != c1, "c2 != c1 is False");
            Assert.AreNotEqual(c1.GetHashCode(), c2.GetHashCode(), "Hash codes are equal");
        }
    }
}
