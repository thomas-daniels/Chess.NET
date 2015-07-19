using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public class PositionDeltaTests
    {
        [Test]
        public void TestPositionDelta()
        {
            Position p1 = new Position(Position.Files.A, Position.Ranks.Two);
            Position p2 = new Position(Position.Files.A, Position.Ranks.Three);
            PositionDelta d = new PositionDelta(p1, p2);
            Assert.AreEqual(0, d.DeltaX);
            Assert.AreEqual(1, d.DeltaY);

            PositionDelta d2 = new PositionDelta(p2, p1);
            Assert.AreEqual(0, d.DeltaX);
            Assert.AreEqual(1, d.DeltaY);
        }
    }
}
