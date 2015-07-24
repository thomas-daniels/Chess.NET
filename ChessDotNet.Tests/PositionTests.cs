using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public class PositionTests
    {
        [Test]
        public void TestEquality()
        {
            Position p1 = new Position(Position.Files.C, Position.Ranks.Six);
            Position p2 = new Position(Position.Files.C, Position.Ranks.Six);
            Assert.AreEqual(p1, p2, "p1 and p2 are not equal");
            Assert.True(p1.Equals(p2), "p1.Equals(p2) is False");
            Assert.True(p2.Equals(p1), "p2.Equals(p1) is False");
        }

        [Test]
        public void TestInequality()
        {
            Position p1 = new Position(Position.Files.E, Position.Ranks.Two);
            Position p2 = new Position(Position.Files.B, Position.Ranks.Five);
            Assert.AreNotEqual(p1, p2, "p1 and p2 are equal");
            Assert.False(p1.Equals(p2), "p1.Equals(p2) is True");
            Assert.False(p2.Equals(p1), "p2.Equals(p1) is True");

            Position p3 = new Position(Position.Files.E, Position.Ranks.Two);
            Position p4 = new Position(Position.Files.E, Position.Ranks.Five);
            Assert.AreNotEqual(p3, p4, "p3 and p4 are equal");
            Assert.False(p3.Equals(p4), "p3.Equals(p4) is True");
            Assert.False(p3.Equals(p4), "p4.Equals(p3) is True");

            Position p5 = new Position(Position.Files.E, Position.Ranks.Two);
            Position p6 = new Position(Position.Files.B, Position.Ranks.Two);
            Assert.AreNotEqual(p5, p6, "p5 and p6 are equal");
            Assert.False(p5.Equals(p6), "p5.Equals(p6) is True");
            Assert.False(p6.Equals(p5), "p6.Equals(p5) is True");
        }

        [Test]
        public void TestConstructors()
        {
            Assert.AreEqual(new Position("A1"), new Position(Position.Files.A, Position.Ranks.One));
            Assert.AreEqual(new Position("B2"), new Position(Position.Files.B, Position.Ranks.Two));
            Assert.AreEqual(new Position("C3"), new Position(Position.Files.C, Position.Ranks.Three));
            Assert.AreEqual(new Position("D4"), new Position(Position.Files.D, Position.Ranks.Four));
            Assert.AreEqual(new Position("E5"), new Position(Position.Files.E, Position.Ranks.Five));
            Assert.AreEqual(new Position("F6"), new Position(Position.Files.F, Position.Ranks.Six));
            Assert.AreEqual(new Position("G7"), new Position(Position.Files.G, Position.Ranks.Seven));
            Assert.AreEqual(new Position("H8"), new Position(Position.Files.H, Position.Ranks.Eight));
        }
    }
}
