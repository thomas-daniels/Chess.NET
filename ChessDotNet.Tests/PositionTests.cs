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
            Assert.True(p1 == p2, "p1 == p2 should be true");
            Assert.True(p2 == p1, "p2 == p1 should be true");
            Assert.False(p1 != p2, "p1 != p2 should be false");
            Assert.False(p2 != p1, "p2 != p1 should be false");
            Assert.AreEqual(p1.GetHashCode(), p2.GetHashCode(), "Hash codes should be equal");
        }

        [Test]
        public void TestInequality()
        {
            Position p1 = new Position(Position.Files.E, Position.Ranks.Two);
            Position p2 = new Position(Position.Files.B, Position.Ranks.Five);
            Assert.AreNotEqual(p1, p2, "p1 and p2 are equal");
            Assert.False(p1.Equals(p2), "p1.Equals(p2) is True");
            Assert.False(p2.Equals(p1), "p2.Equals(p1) is True");
            Assert.True(p1 != p2, "p1 != p2 should be true");
            Assert.True(p2 != p1, "p2 != p1 should be true");
            Assert.False(p1 == p2, "p1 == p2 should be false");
            Assert.False(p2 == p1, "p2 == p1 should be false");
            Assert.AreNotEqual(p1.GetHashCode(), p2.GetHashCode(), "Hash codes of p1 and p2 should be different");

            Position p3 = new Position(Position.Files.E, Position.Ranks.Two);
            Position p4 = new Position(Position.Files.E, Position.Ranks.Five);
            Assert.AreNotEqual(p3, p4, "p3 and p4 are equal");
            Assert.False(p3.Equals(p4), "p3.Equals(p4) is True");
            Assert.False(p3.Equals(p4), "p4.Equals(p3) is True");
            Assert.True(p3 != p4, "p3 != p4 should be true");
            Assert.True(p4 != p3, "p4 != p3 should be true");
            Assert.False(p3 == p4, "p3 == p4 should be false");
            Assert.False(p4 == p3, "p4 == p3 should be false");
            Assert.AreNotEqual(p3.GetHashCode(), p4.GetHashCode(), "Hash codes of p3 and p4 should be different");

            Position p5 = new Position(Position.Files.E, Position.Ranks.Two);
            Position p6 = new Position(Position.Files.B, Position.Ranks.Two);
            Assert.AreNotEqual(p5, p6, "p5 and p6 are equal");
            Assert.False(p5.Equals(p6), "p5.Equals(p6) is True");
            Assert.False(p6.Equals(p5), "p6.Equals(p5) is True");
            Assert.True(p5 != p6, "p5 != p6 should be true");
            Assert.True(p6 != p5, "p6 != p5 should be true");
            Assert.False(p5 == p6, "p5 == p6 should be false");
            Assert.False(p6 == p5, "p6 == p5 should be false");
            Assert.AreNotEqual(p5.GetHashCode(), p6.GetHashCode(), "Hash codes of p5 and p6 should be different");
        }

        [Test]
        public void TestInequalityNull()
        {
            Position position1 = new Position(Position.Files.E, Position.Ranks.One);
            Position position2 = null;
            Assert.AreNotEqual(position1, position2, "position1 and position2 should not be equal");
            Assert.False(position1.Equals(position2), "position1.Equals(position2) should be false");
            Assert.True(position1 != position2, "position1 != position2 should be true");
            Assert.True(position2 != position1, "position2 != position1 should be true");
            Assert.False(position1 == position2, "position1 == position2 should be false");
            Assert.False(position2 == position1, "position2 == position1 should be false");
        }

        [Test]
        public void TestInequalityDifferentType()
        {
            Position position1 = new Position(Position.Files.D, Position.Ranks.Three);
            string str = "abc";
            Assert.False(position1.Equals(str), "position1.Equals(str) should be false");
        }

        [Test]
        public void TestConstructors()
        {
            Assert.AreEqual(new Position(Position.Files.A, Position.Ranks.One), new Position("A1"));
            Assert.AreEqual(new Position(Position.Files.B, Position.Ranks.Two), new Position("B2"));
            Assert.AreEqual(new Position(Position.Files.C, Position.Ranks.Three), new Position("C3"));
            Assert.AreEqual(new Position(Position.Files.D, Position.Ranks.Four), new Position("D4"));
            Assert.AreEqual(new Position(Position.Files.E, Position.Ranks.Five), new Position("E5"));
            Assert.AreEqual(new Position(Position.Files.F, Position.Ranks.Six), new Position("F6"));
            Assert.AreEqual(new Position(Position.Files.G, Position.Ranks.Seven), new Position("G7"));
            Assert.AreEqual(new Position(Position.Files.H, Position.Ranks.Eight), new Position("H8"));
        }

        [Test]
        public void TestToString()
        {
            Assert.AreEqual("H5", new Position(Position.Files.H, Position.Ranks.Five).ToString());
            Assert.AreEqual("H5", new Position("H5").ToString());
        }
    }
}
