using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public class MoveTests
    {
        [Test]
        public void TestEquality()
        {
            Position p1 = new Position(Position.Files.G, Position.Ranks.Four);
            Position p2 = new Position(Position.Files.H, Position.Ranks.Five);
            Position p3 = new Position(Position.Files.G, Position.Ranks.Four);
            Position p4 = new Position(Position.Files.H, Position.Ranks.Five);
            Move m1 = new Move(p1, p2, Players.White);
            Move m2 = new Move(p3, p4, Players.White);
            Move m3 = null;
            Move m4 = null;
            Assert.AreEqual(m1, m2, "m1 and m2 are not equal");
            Assert.True(m1.Equals(m2), "m1.Equals(m2) is False");
            Assert.True(m2.Equals(m1), "m2.Equals(m1) is False");
            Assert.True(m1 == m2, "m1 == m2 is False");
            Assert.True(m2 == m1, "m2 == m1 is False");
            Assert.True(m3 == m4, "m3 == m4 should be True");
            Assert.False(m1 != m2, "m1 != m2 is True");
            Assert.False(m2 != m1, "m2 != m1 is True");
            Assert.AreEqual(m1.GetHashCode(), m2.GetHashCode());
        }

        [Test]
        public void TestInequality_DifferentPlayer()
        {
            Position p1 = new Position(Position.Files.G, Position.Ranks.Four);
            Position p2 = new Position(Position.Files.H, Position.Ranks.Five);
            Position p3 = new Position(Position.Files.G, Position.Ranks.Four);
            Position p4 = new Position(Position.Files.H, Position.Ranks.Five);
            Move m1 = new Move(p1, p2, Players.White);
            Move m2 = new Move(p3, p4, Players.Black);
            Assert.AreNotEqual(m1, m2, "m1 and m2 are equal");
            Assert.False(m1.Equals(m2), "m1.Equals(m2) is True");
            Assert.False(m2.Equals(m1), "m2.Equals(m1) is True");
            Assert.True(m1 != m2, "m1 != m2 is False");
            Assert.True(m2 != m1, "m2 != m1 is False");
            Assert.False(m1 == m2, "m1 == m2 is False");
            Assert.False(m2 == m1, "m2 == m1 is False");
            Assert.AreNotEqual(m1.GetHashCode(), m2.GetHashCode());
        }

        [Test]
        public void TestInequality_DifferentFile()
        {
            Position p1 = new Position(Position.Files.G, Position.Ranks.Four);
            Position p2 = new Position(Position.Files.H, Position.Ranks.Five);
            Position p3 = new Position(Position.Files.F, Position.Ranks.Four);
            Position p4 = new Position(Position.Files.H, Position.Ranks.Five);
            Move m1 = new Move(p1, p2, Players.Black);
            Move m2 = new Move(p3, p4, Players.Black);
            Assert.AreNotEqual(m1, m2, "m1 and m2 are equal");
            Assert.False(m1.Equals(m2), "m1.Equals(m2) is True");
            Assert.False(m2.Equals(m1), "m2.Equals(m1) is True");
            Assert.True(m1 != m2, "m1 != m2 is False");
            Assert.True(m2 != m1, "m2 != m1 is False");
            Assert.False(m1 == m2, "m1 == m2 is False");
            Assert.False(m2 == m1, "m2 == m1 is False");
            Assert.AreNotEqual(m1.GetHashCode(), m2.GetHashCode());
        }

        [Test]
        public void TestInequality_DifferentRank()
        {
            Position p1 = new Position(Position.Files.G, Position.Ranks.Four);
            Position p2 = new Position(Position.Files.H, Position.Ranks.Five);
            Position p3 = new Position(Position.Files.F, Position.Ranks.Four);
            Position p4 = new Position(Position.Files.H, Position.Ranks.Six);
            Move m1 = new Move(p1, p2, Players.Black);
            Move m2 = new Move(p3, p4, Players.Black);
            Assert.AreNotEqual(m1, m2, "m1 and m2 are equal");
            Assert.False(m1.Equals(m2), "m1.Equals(m2) is True");
            Assert.False(m2.Equals(m1), "m2.Equals(m1) is True");
            Assert.True(m1 != m2, "m1 != m2 is False");
            Assert.True(m2 != m1, "m2 != m1 is False");
            Assert.False(m1 == m2, "m1 == m2 is False");
            Assert.False(m2 == m1, "m2 == m1 is False");
            Assert.AreNotEqual(m1.GetHashCode(), m2.GetHashCode());
        }

        [Test]
        public void TestInequality_DifferentRankAndFile()
        {
            Position p1 = new Position(Position.Files.G, Position.Ranks.Four);
            Position p2 = new Position(Position.Files.H, Position.Ranks.Five);
            Position p3 = new Position(Position.Files.A, Position.Ranks.One);
            Position p4 = new Position(Position.Files.B, Position.Ranks.Two);
            Move m1 = new Move(p1, p2, Players.White);
            Move m2 = new Move(p3, p4, Players.White);
            Assert.AreNotEqual(m1, m2, "m1 and m2 are equal");
            Assert.False(m1.Equals(m2), "m1.Equals(m2) is True");
            Assert.False(m2.Equals(m1), "m2.Equals(m1) is True");
            Assert.True(m1 != m2, "m1 != m2 is False");
            Assert.True(m2 != m1, "m2 != m1 is False");
            Assert.False(m1 == m2, "m1 == m2 is False");
            Assert.False(m2 == m1, "m2 == m1 is False");
            Assert.AreNotEqual(m1.GetHashCode(), m2.GetHashCode());
        }

        [Test]
        public void TestInequality_DifferentPromotion()
        {
            Position position1 = new Position(Position.Files.A, Position.Ranks.Seven);
            Position position2 = new Position(Position.Files.A, Position.Ranks.Eight);
            Move move1 = new Move(position1, position2, Players.White, Pieces.Queen);
            Move move2 = new Move(position1, position2, Players.White, Pieces.Knight);
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
        public void TestInequality_OneIsNull()
        {
            Position position1 = new Position(Position.Files.G, Position.Ranks.Four);
            Position position2 = new Position(Position.Files.G, Position.Ranks.Eight);
            Move move1 = new Move(position1, position2, Players.Black);
            Move move2 = null;
            Assert.False(move1 == move2, "move1 == move2 should be false");
            Move move3 = null;
            Move move4 = new Move(position1, position2, Players.Black);
            Assert.False(move3 == move4, "move3 == move4 should be false");
        }

        [Test]
        public void TestInequality_DifferentType()
        {
            Position position1 = new Position(Position.Files.G, Position.Ranks.Four);
            Position position2 = new Position(Position.Files.G, Position.Ranks.Eight);
            Move move1 = new Move(position1, position2, Players.Black);
            Assert.False(move1.Equals(position1), "move1.Equals(position1) should be false");
        }
    }
}
