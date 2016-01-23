using NUnit.Framework;

namespace ChessDotNet.Tests
{
    using Pieces;

    [TestFixture]
    public static class DetailedMoveTests
    {
        [Test]
        public static void TestEquality()
        {
            DetailedMove move1 = new DetailedMove(new Position(File.E, Rank.Two), new Position(File.E, Rank.Four), Player.White, null, new Pawn(Player.White), false, CastlingType.None);
            DetailedMove move2 = new DetailedMove(new Position(File.E, Rank.Two), new Position(File.E, Rank.Four), Player.White, null, new Pawn(Player.White), false, CastlingType.None);
            DetailedMove move3 = null;
            DetailedMove move4 = null;
            Assert.AreEqual(move1, move2, "move1 and move2 should be equal");
            Assert.True(move1.Equals(move2), "move1.Equals(move2) should be true");
            Assert.True(move2.Equals(move1), "move2.Equals(move1) should be true");
            Assert.True(move1 == move2, "move1 == move2 should be true");
            Assert.True(move2 == move1, "move2 == move1 should be true");
            Assert.True(move3 == move4, "move3 == move4 should be True");
            Assert.False(move1 != move2, "move1 != move2 should be false");
            Assert.False(move2 != move1, "move2 != move1 should be false");
            Assert.AreEqual(move1.GetHashCode(), move2.GetHashCode());
        }

        [Test]
        public static void TestInequality_DifferentPlayer()
        {
            DetailedMove move1 = new DetailedMove(new Position(File.E, Rank.Two), new Position(File.E, Rank.Four), Player.White, null, new Pawn(Player.White), false, CastlingType.None);
            DetailedMove move2 = new DetailedMove(new Position(File.E, Rank.Two), new Position(File.E, Rank.Four), Player.Black, null, new Pawn(Player.Black), false, CastlingType.None);
            Assert.AreNotEqual(move1, move2, "move1 and move2 are equal");
            Assert.False(move1.Equals(move2), "move1.Equals(move2) should be false");
            Assert.False(move2.Equals(move1), "move2.Equals(move1) should be false");
            Assert.True(move1 != move2, "move1 != move2 should be true");
            Assert.True(move2 != move1, "move2 != move1 should be true");
            Assert.False(move1 == move2, "move1 == move2 should be true");
            Assert.False(move2 == move1, "move2 == move1 should be true");
            Assert.AreNotEqual(move1.GetHashCode(), move2.GetHashCode());
        }

        [Test]
        public static void TestInequality_DifferentPiece()
        {
            DetailedMove move1 = new DetailedMove(new Position(File.E, Rank.Two), new Position(File.E, Rank.Four), Player.White, null, new Pawn(Player.White), false, CastlingType.None);
            DetailedMove move2 = new DetailedMove(new Position(File.E, Rank.Two), new Position(File.E, Rank.Four), Player.White, null, new Queen(Player.White), false, CastlingType.None);
            Assert.AreNotEqual(move1, move2, "move1 and move2 are equal");
            Assert.False(move1.Equals(move2), "move1.Equals(move2) should be false");
            Assert.False(move2.Equals(move1), "move2.Equals(move1) should be false");
            Assert.True(move1 != move2, "move1 != move2 should be true");
            Assert.True(move2 != move1, "move2 != move1 should be true");
            Assert.False(move1 == move2, "move1 == move2 should be true");
            Assert.False(move2 == move1, "move2 == move1 should be true");
            Assert.AreNotEqual(move1.GetHashCode(), move2.GetHashCode());
        }

        [Test]
        public static void TestInequality_DifferentPromotion()
        {
            DetailedMove move1 = new DetailedMove(new Position(File.E, Rank.Two), new Position(File.E, Rank.Four), Player.White, null, new Pawn(Player.White), false, CastlingType.None);
            DetailedMove move2 = new DetailedMove(new Position(File.E, Rank.Two), new Position(File.E, Rank.Four), Player.White, null, new Queen(Player.White), false, CastlingType.None);
            Assert.AreNotEqual(move1, move2, "move1 and move2 are equal");
            Assert.False(move1.Equals(move2), "move1.Equals(move2) should be false");
            Assert.False(move2.Equals(move1), "move2.Equals(move1) should be false");
            Assert.True(move1 != move2, "move1 != move2 should be true");
            Assert.True(move2 != move1, "move2 != move1 should be true");
            Assert.False(move1 == move2, "move1 == move2 should be true");
            Assert.False(move2 == move1, "move2 == move1 should be true");
            Assert.AreNotEqual(move1.GetHashCode(), move2.GetHashCode());
        }

        [Test]
        public static void TestInequality_DifferentIsCapture()
        {
            DetailedMove move1 = new DetailedMove(new Position(File.E, Rank.Two), new Position(File.E, Rank.Four), Player.White, null, new Queen(Player.White), true, CastlingType.None);
            DetailedMove move2 = new DetailedMove(new Position(File.E, Rank.Two), new Position(File.E, Rank.Four), Player.White, null, new Queen(Player.White), false, CastlingType.None);
            Assert.AreNotEqual(move1, move2, "move1 and move2 are equal");
            Assert.False(move1.Equals(move2), "move1.Equals(move2) should be false");
            Assert.False(move2.Equals(move1), "move2.Equals(move1) should be false");
            Assert.True(move1 != move2, "move1 != move2 should be true");
            Assert.True(move2 != move1, "move2 != move1 should be true");
            Assert.False(move1 == move2, "move1 == move2 should be true");
            Assert.False(move2 == move1, "move2 == move1 should be true");
            Assert.AreNotEqual(move1.GetHashCode(), move2.GetHashCode());
        }

        [Test]
        public static void TestInequality_DifferentCastlingType()
        {
            DetailedMove move1 = new DetailedMove(new Position(File.E, Rank.One), new Position(File.C, Rank.One), Player.White, null, new King(Player.White), true, CastlingType.QueenSide);
            DetailedMove move2 = new DetailedMove(new Position(File.E, Rank.One), new Position(File.G, Rank.One), Player.White, null, new King(Player.White), false, CastlingType.KingSide);
            Assert.AreNotEqual(move1, move2, "move1 and move2 are equal");
            Assert.False(move1.Equals(move2), "move1.Equals(move2) should be false");
            Assert.False(move2.Equals(move1), "move2.Equals(move1) should be false");
            Assert.True(move1 != move2, "move1 != move2 should be true");
            Assert.True(move2 != move1, "move2 != move1 should be true");
            Assert.False(move1 == move2, "move1 == move2 should be true");
            Assert.False(move2 == move1, "move2 == move1 should be true");
            Assert.AreNotEqual(move1.GetHashCode(), move2.GetHashCode());
        }
    }
}
