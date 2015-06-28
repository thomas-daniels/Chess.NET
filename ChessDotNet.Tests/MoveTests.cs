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
            Assert.AreEqual(m1, m2, "m1 and m2 are not equal");
            Assert.True(m1.Equals(m2), "m1.Equals(m2) is False");
            Assert.True(m2.Equals(m1), "m2.Equals(m1) is False");
            Assert.True(m1 == m2, "m1 == m2 is False");
            Assert.True(m2 == m1, "m2 == m1 is False");
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
    }
}
