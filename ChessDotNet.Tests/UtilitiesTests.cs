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

        [Test]
        public static void TestFilesBetween()
        {
            CollectionAssert.AreEqual(new File[] { File.B, File.C, File.D }, ChessUtilities.FilesBetween(File.B, File.D, true, true));
            CollectionAssert.AreEqual(new File[] { File.C }, ChessUtilities.FilesBetween(File.B, File.D, false, false));
            CollectionAssert.AreEqual(new File[] { File.C, File.D }, ChessUtilities.FilesBetween(File.B, File.D, false, true));
            CollectionAssert.AreEqual(new File[] { File.B, File.C }, ChessUtilities.FilesBetween(File.B, File.D, true, false));

            CollectionAssert.AreEqual(new File[] { File.B, File.C, File.D }, ChessUtilities.FilesBetween(File.D, File.B, true, true));
            CollectionAssert.AreEqual(new File[] { File.C }, ChessUtilities.FilesBetween(File.D, File.B, false, false));
            CollectionAssert.AreEqual(new File[] { File.B, File.C }, ChessUtilities.FilesBetween(File.D, File.B, false, true));
            CollectionAssert.AreEqual(new File[] { File.C, File.D }, ChessUtilities.FilesBetween(File.D, File.B, true, false));

            CollectionAssert.AreEqual(new File[] { }, ChessUtilities.FilesBetween(File.F, File.F, false, false));
            CollectionAssert.AreEqual(new File[] { File.F }, ChessUtilities.FilesBetween(File.F, File.F, true, false));
            CollectionAssert.AreEqual(new File[] { File.F }, ChessUtilities.FilesBetween(File.F, File.F, false, true));
            CollectionAssert.AreEqual(new File[] { File.F }, ChessUtilities.FilesBetween(File.F, File.F, true, true));
        }
    }
}
