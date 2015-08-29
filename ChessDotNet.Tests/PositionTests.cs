using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public class PositionTests
    {
        [Test]
        public void TestEquality()
        {
            Position position1 = new Position(File.C, Rank.Six);
            Position position2 = new Position(File.C, Rank.Six);
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
            Position position1 = new Position(File.E, Rank.Two);
            Position position2 = new Position(File.B, Rank.Five);
            Assert.AreNotEqual(position1, position2, "position1 and position2 are equal");
            Assert.False(position1.Equals(position2), "position1.Equals(position2) is True");
            Assert.False(position2.Equals(position1), "position2.Equals(position1) is True");
            Assert.True(position1 != position2, "position1 != position2 should be true");
            Assert.True(position2 != position1, "position2 != position1 should be true");
            Assert.False(position1 == position2, "position1 == position2 should be false");
            Assert.False(position2 == position1, "position2 == position1 should be false");
            Assert.AreNotEqual(position1.GetHashCode(), position2.GetHashCode(), "Hash codes of position1 and position2 should be different");

            Position position3 = new Position(File.E, Rank.Two);
            Position position4 = new Position(File.E, Rank.Five);
            Assert.AreNotEqual(position3, position4, "position3 and position4 are equal");
            Assert.False(position3.Equals(position4), "position3.Equals(position4) is True");
            Assert.False(position3.Equals(position4), "position4.Equals(position3) is True");
            Assert.True(position3 != position4, "position3 != position4 should be true");
            Assert.True(position4 != position3, "position4 != position3 should be true");
            Assert.False(position3 == position4, "position3 == position4 should be false");
            Assert.False(position4 == position3, "position4 == position3 should be false");
            Assert.AreNotEqual(position3.GetHashCode(), position4.GetHashCode(), "Hash codes of position3 and position4 should be different");

            Position position5 = new Position(File.E, Rank.Two);
            Position position6 = new Position(File.B, Rank.Two);
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
            Position position1 = new Position(File.E, Rank.One);
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
            Position position1 = new Position(File.D, Rank.Three);
            string str = "abc";
            Assert.False(position1.Equals(str), "position1.Equals(str) should be false");
        }

        [Test]
        public void TestConstructors()
        {
            Assert.AreEqual(new Position(File.A, Rank.One), new Position("A1"));
            Assert.AreEqual(new Position(File.B, Rank.Two), new Position("B2"));
            Assert.AreEqual(new Position(File.C, Rank.Three), new Position("C3"));
            Assert.AreEqual(new Position(File.D, Rank.Four), new Position("D4"));
            Assert.AreEqual(new Position(File.E, Rank.Five), new Position("E5"));
            Assert.AreEqual(new Position(File.F, Rank.Six), new Position("F6"));
            Assert.AreEqual(new Position(File.G, Rank.Seven), new Position("G7"));
            Assert.AreEqual(new Position(File.H, Rank.Eight), new Position("H8"));
        }

        [Test]
        public void TestToString()
        {
            Assert.AreEqual("H5", new Position(File.H, Rank.Five).ToString());
            Assert.AreEqual("H5", new Position("H5").ToString());
        }
    }
}
