using System;
using NUnit.Framework;

namespace ChessDotNet.Tests
{
    using Pieces;

    [TestFixture]
    public static class UtilitiesTests
    {
        [Test]
        public static void TestThrowIfNull()
        {
            Assert.Throws<ArgumentNullException>(delegate ()
            {
                object value = null;
                ChessUtilities.ThrowIfNull(value, "value");
            });

            Assert.DoesNotThrow(delegate ()
            {
                Piece piece = new Bishop(Player.White);
                ChessUtilities.ThrowIfNull(piece, "piece");
            });
        }

        [Test]
        public static void TestGetOpponentOf()
        {
            Assert.AreEqual(Player.Black, ChessUtilities.GetOpponentOf(Player.White));
            Assert.AreEqual(Player.White, ChessUtilities.GetOpponentOf(Player.Black));
            Assert.Throws<ArgumentException>(delegate ()
            {
                ChessUtilities.GetOpponentOf(Player.None);
            });
        }
    }
}
