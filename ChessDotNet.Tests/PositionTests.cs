using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public class PositionTests
    {
        [Test]
        public void TestEquality()
        {
            Position position1 = new Position(Position.Files.C, Position.Ranks.Six);
            Position position2 = new Position(Position.Files.C, Position.Ranks.Six);
            Assert.AreEqual(position1, position2, "position1 and position2 are not equal");
            Assert.True(position1.Equals(position2), "position1.Equals(position2) is False");
            Assert.True(position2.Equals(position1), "position2.Equals(position1) is False");
            Assert.True(position1 == position2, "position1 == position2 should be true");
            Assert.True(position2 == position1, "position2 == position1 should be true");
            Assert.False(position1 != position2, "position1 != position2 should be false");
            Assert.False(position2 != position1, "position2 != position1 should be false");
            Assert.AreEqual(position1.GetHashCode(), position2.GetHashCode(), "Hash codes should be equal");
        }

        [Test]
        public void TestInequality()
        {
            Position position1 = new Position(Position.Files.E, Position.Ranks.Two);
            Position position2 = new Position(Position.Files.B, Position.Ranks.Five);
            Assert.AreNotEqual(position1, position2, "position1 and position2 are equal");
            Assert.False(position1.Equals(position2), "position1.Equals(position2) is True");
            Assert.False(position2.Equals(position1), "position2.Equals(position1) is True");
            Assert.True(position1 != position2, "position1 != position2 should be true");
            Assert.True(position2 != position1, "position2 != position1 should be true");
            Assert.False(position1 == position2, "position1 == position2 should be false");
            Assert.False(position2 == position1, "position2 == position1 should be false");
            Assert.AreNotEqual(position1.GetHashCode(), position2.GetHashCode(), "Hash codes of position1 and position2 should be different");

            Position position3 = new Position(Position.Files.E, Position.Ranks.Two);
            Position position4 = new Position(Position.Files.E, Position.Ranks.Five);
            Assert.AreNotEqual(position3, position4, "position3 and position4 are equal");
            Assert.False(position3.Equals(position4), "position3.Equals(position4) is True");
            Assert.False(position3.Equals(position4), "position4.Equals(position3) is True");
            Assert.True(position3 != position4, "position3 != position4 should be true");
            Assert.True(position4 != position3, "position4 != position3 should be true");
            Assert.False(position3 == position4, "position3 == position4 should be false");
            Assert.False(position4 == position3, "position4 == position3 should be false");
            Assert.AreNotEqual(position3.GetHashCode(), position4.GetHashCode(), "Hash codes of position3 and position4 should be different");

            Position position5 = new Position(Position.Files.E, Position.Ranks.Two);
            Position position6 = new Position(Position.Files.B, Position.Ranks.Two);
            Assert.AreNotEqual(position5, position6, "position5 and position6 are equal");
            Assert.False(position5.Equals(position6), "position5.Equals(position6) is True");
            Assert.False(position6.Equals(position5), "position6.Equals(position5) is True");
            Assert.True(position5 != position6, "position5 != position6 should be true");
            Assert.True(position6 != position5, "position6 != position5 should be true");
            Assert.False(position5 == position6, "position5 == position6 should be false");
            Assert.False(position6 == position5, "position6 == position5 should be false");
            Assert.AreNotEqual(position5.GetHashCode(), position6.GetHashCode(), "Hash codes of position5 and position6 should be different");
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
