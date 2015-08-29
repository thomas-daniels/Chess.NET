using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public class PositionDeltaTests
    {
        [Test]
        public void TestPositionDelta()
        {
            Position position1 = new Position(File.A, Rank.Two);
            Position position2 = new Position(File.A, Rank.Three);
            PositionDelta delta1 = new PositionDelta(position1, position2);
            Assert.AreEqual(0, delta1.DeltaX);
            Assert.AreEqual(1, delta1.DeltaY);

            PositionDelta delta2 = new PositionDelta(position2, position1);
            Assert.AreEqual(0, delta2.DeltaX);
            Assert.AreEqual(1, delta2.DeltaY);
        }
    }
}
