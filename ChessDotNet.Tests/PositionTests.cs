using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public static class PositionTests
    {
        [Test]
        public static void TestEquality()
        {
            Position position1 = new Position(File.C, 6);
            Position position2 = new Position(File.C, 6);
            Assert.AreEqual(position1, position2, "position1 and position2 are not equal");
            Assert.True(position1.Equals(position2), "position1.Equals(position2) should be true");
            Assert.True(position2.Equals(position1), "position2.Equals(position1) should be true");
            Assert.True(position1 == position2, "position1 == position2 should be true");
            Assert.True(position2 == position1, "position2 == position1 should be true");
            Assert.False(position1 != position2, "position1 != position2 should be false");
            Assert.False(position2 != position1, "position2 != position1 should be false");
            Assert.AreEqual(position1.GetHashCode(), position2.GetHashCode(), "Hash codes should be equal");
        }

        [Test]
        public static void TestInequality()
        {
            Position position1 = new Position(File.E, 2);
            Position position2 = new Position(File.B, 5);
            Assert.AreNotEqual(position1, position2, "position1 and position2 are equal");
            Assert.False(position1.Equals(position2), "position1.Equals(position2) should be false");
            Assert.False(position2.Equals(position1), "position2.Equals(position1) should be false");
            Assert.True(position1 != position2, "position1 != position2 should be true");
            Assert.True(position2 != position1, "position2 != position1 should be true");
            Assert.False(position1 == position2, "position1 == position2 should be false");
            Assert.False(position2 == position1, "position2 == position1 should be false");
            Assert.AreNotEqual(position1.GetHashCode(), position2.GetHashCode(), "Hash codes of position1 and position2 should be different");

            Position position3 = new Position(File.E, 2);
            Position position4 = new Position(File.E, 5);
            Assert.AreNotEqual(position3, position4, "position3 and position4 are equal");
            Assert.False(position3.Equals(position4), "position3.Equals(position4) should be false");
            Assert.False(position3.Equals(position4), "position4.Equals(position3) should be false");
            Assert.True(position3 != position4, "position3 != position4 should be true");
            Assert.True(position4 != position3, "position4 != position3 should be true");
            Assert.False(position3 == position4, "position3 == position4 should be false");
            Assert.False(position4 == position3, "position4 == position3 should be false");
            Assert.AreNotEqual(position3.GetHashCode(), position4.GetHashCode(), "Hash codes of position3 and position4 should be different");

            Position position5 = new Position(File.E, 2);
            Position position6 = new Position(File.B, 2);
            Assert.AreNotEqual(position5, position6, "position5 and position6 are equal");
            Assert.False(position5.Equals(position6), "position5.Equals(position6) should be false");
            Assert.False(position6.Equals(position5), "position6.Equals(position5) should be false");
            Assert.True(position5 != position6, "position5 != position6 should be true");
            Assert.True(position6 != position5, "position6 != position5 should be true");
            Assert.False(position5 == position6, "position5 == position6 should be false");
            Assert.False(position6 == position5, "position6 == position5 should be false");
            Assert.AreNotEqual(position5.GetHashCode(), position6.GetHashCode(), "Hash codes of position5 and position6 should be different");
        }

        [Test]
        public static void TestInequalityNull()
        {
            Position position1 = new Position(File.E, 1);
            Position position2 = null;
            Assert.AreNotEqual(position1, position2, "position1 and position2 should not be equal");
            Assert.False(position1.Equals(position2), "position1.Equals(position2) should be false");
            Assert.True(position1 != position2, "position1 != position2 should be true");
            Assert.True(position2 != position1, "position2 != position1 should be true");
            Assert.False(position1 == position2, "position1 == position2 should be false");
            Assert.False(position2 == position1, "position2 == position1 should be false");
        }

        [Test]
        public static void TestInequalityDifferentType()
        {
            Position position1 = new Position(File.D, 3);
            string str = "abc";
            Assert.False(position1.Equals(str), "position1.Equals(str) should be false");
        }

        [Test]
        public static void TestConstructors()
        {
            Assert.AreEqual(new Position(File.A, 1), new Position("A1"));
            Assert.AreEqual(new Position(File.B, 2), new Position("B2"));
            Assert.AreEqual(new Position(File.C, 3), new Position("C3"));
            Assert.AreEqual(new Position(File.D, 4), new Position("D4"));
            Assert.AreEqual(new Position(File.E, 5), new Position("E5"));
            Assert.AreEqual(new Position(File.F, 6), new Position("F6"));
            Assert.AreEqual(new Position(File.G, 7), new Position("G7"));
            Assert.AreEqual(new Position(File.H, 8), new Position("H8"));
        }

        [Test]
        public static void TestToString()
        {
            Assert.AreEqual("H5", new Position(File.H, 5).ToString());
            Assert.AreEqual("H5", new Position("H5").ToString());
        }
    }
}
