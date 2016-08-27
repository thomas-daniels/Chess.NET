using NUnit.Framework;

namespace ChessDotNet.Tests
{
    using Pieces;

    [TestFixture]
    public static class MoveTests
    {
        [Test]
        public static void TestEquality()
        {
            Position position1 = new Position(File.G, 4);
            Position position2 = new Position(File.H, 5);
            Position position3 = new Position(File.G, 4);
            Position position4 = new Position(File.H, 5);
            Move move1 = new Move(position1, position2, Player.White);
            Move move2 = new Move(position3, position4, Player.White);
            Move move3 = null;
            Move move4 = null;
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
            Position position1 = new Position(File.G, 4);
            Position position2 = new Position(File.H, 5);
            Position position3 = new Position(File.G, 4);
            Position position4 = new Position(File.H, 5);
            Move move1 = new Move(position1, position2, Player.White);
            Move move2 = new Move(position3, position4, Player.Black);
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
        public static void TestInequality_DifferentFile()
        {
            Position position1 = new Position(File.G, 4);
            Position position2 = new Position(File.H, 5);
            Position position3 = new Position(File.F, 4);
            Position position4 = new Position(File.H, 5);
            Move move1 = new Move(position1, position2, Player.Black);
            Move move2 = new Move(position3, position4, Player.Black);
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
        public static void TestInequality_DifferentRank()
        {
            Position position1 = new Position(File.G, 4);
            Position position2 = new Position(File.H, 5);
            Position position3 = new Position(File.F, 4);
            Position position4 = new Position(File.H, 6);
            Move move1 = new Move(position1, position2, Player.Black);
            Move move2 = new Move(position3, position4, Player.Black);
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
        public static void TestInequality_DifferentRankAndFile()
        {
            Position position1 = new Position(File.G, 4);
            Position position2 = new Position(File.H, 5);
            Position position3 = new Position(File.A, 1);
            Position position4 = new Position(File.B, 2);
            Move move1 = new Move(position1, position2, Player.White);
            Move move2 = new Move(position3, position4, Player.White);
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
            Position position1 = new Position(File.A, 7);
            Position position2 = new Position(File.A, 8);
            Move move1 = new Move(position1, position2, Player.White, 'Q');
            Move move2 = new Move(position1, position2, Player.White, 'N');
            Assert.AreNotEqual(move1, move2, "move1 and move2 should not be equal");
            Assert.False(move1.Equals(move2), "move1.Equals(move2) should be false");
            Assert.False(move2.Equals(move1), "move2.Equals(move1) should be false");
            Assert.True(move1 != move2, "move1 != move2 should be true");
            Assert.True(move2 != move1, "move2 != move1 should be true");
            Assert.False(move1 == move2, "move1 == move2 should be false");
            Assert.False(move2 == move1, "move2 == move1 should be false");
            Assert.AreNotEqual(move1.GetHashCode(), move2.GetHashCode());
        }

        [Test]
        public static void TestInequality_OneIsNull()
        {
            Position position1 = new Position(File.G, 4);
            Position position2 = new Position(File.G, 8);
            Move move1 = new Move(position1, position2, Player.Black);
            Move move2 = null;
            Assert.False(move1 == move2, "move1 == move2 should be false");
            Move move3 = null;
            Move move4 = new Move(position1, position2, Player.Black);
            Assert.False(move3 == move4, "move3 == move4 should be false");
        }

        [Test]
        public static void TestInequality_DifferentType()
        {
            Position position1 = new Position(File.G, 4);
            Position position2 = new Position(File.G, 8);
            Move move1 = new Move(position1, position2, Player.Black);
            Assert.False(move1.Equals(position1), "move1.Equals(position1) should be false");
        }
    }
}
