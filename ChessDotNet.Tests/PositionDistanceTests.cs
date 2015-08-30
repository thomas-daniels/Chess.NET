using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public class PositionDistanceTests
    {
        [Test]
        public void TestPositionDistance()
        {
            Position position1 = new Position(File.A, Rank.Two);
            Position position2 = new Position(File.A, Rank.Three);
            PositionDistance distance1 = new PositionDistance(position1, position2);
            Assert.AreEqual(0, distance1.DistanceX);
            Assert.AreEqual(1, distance1.DistanceY);

            PositionDistance distance2 = new PositionDistance(position2, position1);
            Assert.AreEqual(0, distance2.DistanceX);
            Assert.AreEqual(1, distance2.DistanceY);
        }
    }
}
