using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public class MoveTests
    {
        [Test]
        public void TestEquality()
        {
            Position position1 = new Position(Position.Files.G, Position.Ranks.Four);
            Position position2 = new Position(Position.Files.H, Position.Ranks.Five);
            Position position3 = new Position(Position.Files.G, Position.Ranks.Four);
            Position position4 = new Position(Position.Files.H, Position.Ranks.Five);
            Move move1 = new Move(position1, position2, Players.White);
            Move move2 = new Move(position3, position4, Players.White);
            Move move3 = null;
            Move move4 = null;
            Assert.AreEqual(move1, move2, "move1 and move2 are not equal");
            Assert.True(move1.Equals(move2), "move1.Equals(move2) is False");
            Assert.True(move2.Equals(move1), "move2.Equals(move1) is False");
            Assert.True(move1 == move2, "move1 == move2 is False");
            Assert.True(move2 == move1, "move2 == move1 is False");
            Assert.True(move3 == move4, "move3 == move4 should be True");
            Assert.False(move1 != move2, "move1 != move2 is True");
            Assert.False(move2 != move1, "move2 != move1 is True");
            Assert.AreEqual(move1.GetHashCode(), move2.GetHashCode());
        }

        [Test]
        public void TestInequality_DifferentPlayer()
        {
            Position position1 = new Position(Position.Files.G, Position.Ranks.Four);
            Position position2 = new Position(Position.Files.H, Position.Ranks.Five);
            Position position3 = new Position(Position.Files.G, Position.Ranks.Four);
            Position position4 = new Position(Position.Files.H, Position.Ranks.Five);
            Move move1 = new Move(position1, position2, Players.White);
            Move move2 = new Move(position3, position4, Players.Black);
            Assert.AreNotEqual(move1, move2, "move1 and move2 are equal");
            Assert.False(move1.Equals(move2), "move1.Equals(move2) is True");
            Assert.False(move2.Equals(move1), "move2.Equals(move1) is True");
            Assert.True(move1 != move2, "move1 != move2 is False");
            Assert.True(move2 != move1, "move2 != move1 is False");
            Assert.False(move1 == move2, "move1 == move2 is False");
            Assert.False(move2 == move1, "move2 == move1 is False");
            Assert.AreNotEqual(move1.GetHashCode(), move2.GetHashCode());
        }

        [Test]
        public void TestInequality_DifferentFile()
        {
            Position position1 = new Position(Position.Files.G, Position.Ranks.Four);
            Position position2 = new Position(Position.Files.H, Position.Ranks.Five);
            Position position3 = new Position(Position.Files.F, Position.Ranks.Four);
            Position position4 = new Position(Position.Files.H, Position.Ranks.Five);
            Move move1 = new Move(position1, position2, Players.Black);
            Move move2 = new Move(position3, position4, Players.Black);
            Assert.AreNotEqual(move1, move2, "move1 and move2 are equal");
            Assert.False(move1.Equals(move2), "move1.Equals(move2) is True");
            Assert.False(move2.Equals(move1), "move2.Equals(move1) is True");
            Assert.True(move1 != move2, "move1 != move2 is False");
            Assert.True(move2 != move1, "move2 != move1 is False");
            Assert.False(move1 == move2, "move1 == move2 is False");
            Assert.False(move2 == move1, "move2 == move1 is False");
            Assert.AreNotEqual(move1.GetHashCode(), move2.GetHashCode());
        }

        [Test]
        public void TestInequality_DifferentRank()
        {
            Position position1 = new Position(Position.Files.G, Position.Ranks.Four);
            Position position2 = new Position(Position.Files.H, Position.Ranks.Five);
            Position position3 = new Position(Position.Files.F, Position.Ranks.Four);
            Position position4 = new Position(Position.Files.H, Position.Ranks.Six);
            Move move1 = new Move(position1, position2, Players.Black);
            Move move2 = new Move(position3, position4, Players.Black);
            Assert.AreNotEqual(move1, move2, "move1 and move2 are equal");
            Assert.False(move1.Equals(move2), "move1.Equals(move2) is True");
            Assert.False(move2.Equals(move1), "move2.Equals(move1) is True");
            Assert.True(move1 != move2, "move1 != move2 is False");
            Assert.True(move2 != move1, "move2 != move1 is False");
            Assert.False(move1 == move2, "move1 == move2 is False");
            Assert.False(move2 == move1, "move2 == move1 is False");
            Assert.AreNotEqual(move1.GetHashCode(), move2.GetHashCode());
        }

        [Test]
        public void TestInequality_DifferentRankAndFile()
        {
            Position position1 = new Position(Position.Files.G, Position.Ranks.Four);
            Position position2 = new Position(Position.Files.H, Position.Ranks.Five);
            Position position3 = new Position(Position.Files.A, Position.Ranks.One);
            Position position4 = new Position(Position.Files.B, Position.Ranks.Two);
            Move move1 = new Move(position1, position2, Players.White);
            Move move2 = new Move(position3, position4, Players.White);
            Assert.AreNotEqual(move1, move2, "move1 and move2 are equal");
            Assert.False(move1.Equals(move2), "move1.Equals(move2) is True");
            Assert.False(move2.Equals(move1), "move2.Equals(move1) is True");
            Assert.True(move1 != move2, "move1 != move2 is False");
            Assert.True(move2 != move1, "move2 != move1 is False");
            Assert.False(move1 == move2, "move1 == move2 is False");
            Assert.False(move2 == move1, "move2 == move1 is False");
            Assert.AreNotEqual(move1.GetHashCode(), move2.GetHashCode());
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
