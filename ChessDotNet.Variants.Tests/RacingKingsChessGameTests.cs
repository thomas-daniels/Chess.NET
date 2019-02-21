using NUnit.Framework;

namespace ChessDotNet.Variants.Tests
{
    using RacingKings;

    [TestFixture]
    public class RacingKingsChessGameTests
    {
        [Test]
        public static void TestStartPosition()
        {
            RacingKingsChessGame game = new RacingKingsChessGame();
            Assert.AreEqual("8/8/8/8/8/8/krbnNBRK/qrbnNBRQ w - - 0 1", game.GetFen());
        }

        [Test]
        public static void TestInvalidMove_NoCheck()
        {
            RacingKingsChessGame game = new RacingKingsChessGame();
            Assert.False(game.IsValidMove(new Move("E2", "C1", Player.White)));
        }

        [Test]
        public static void TestGetValidMoves()
        {
            RacingKingsChessGame game = new RacingKingsChessGame();
            Assert.AreEqual(21, game.GetValidMoves(Player.White).Count);
        }

        [Test]
        public static void TestIsWinnerWhite1()
        {
            RacingKingsChessGame game = new RacingKingsChessGame("5K2/1k6/8/8/8/8/1rbnNBR1/qrbnNBRQ b - - 11 6");
            Assert.False(game.IsWinner(Player.White));

            game.MakeMove(new Move("B7", "C7", Player.Black), true);
            Assert.True(game.IsWinner(Player.White));
        }

        [Test]
        public static void TestIsWinnerWhite2()
        {
            RacingKingsChessGame game = new RacingKingsChessGame("5K2/1k6/8/8/8/8/1rbnNBR1/qrbnNBRQ b - - 11 6");
            Assert.False(game.IsWinner(Player.White));

            game.MakeMove(new Move("B7", "B8", Player.Black), true);
            Assert.False(game.IsWinner(Player.White));
            Assert.True(game.IsDraw());
        }

        [Test]
        public static void TestIsWinnerBlack()
        {
            RacingKingsChessGame game = new RacingKingsChessGame("2k5/5K2/8/8/8/8/1rbnNBR1/qrbnNBRQ w - - 12 7");
            Assert.True(game.IsWinner(Player.Black));
        }

        [Test]
        public static void TestIsStalematedWhite()
        {
            RacingKingsChessGame game = new RacingKingsChessGame("4r3/6K1/8/1k3qb1/8/8/8/3n4 w - - 0 18");
            Assert.True(game.IsStalemated(Player.White));
            Assert.True(game.IsDraw());
        }

        [Test]
        public static void TestIsNotWinner()
        {
            RacingKingsChessGame game = new RacingKingsChessGame("4K3/2k5/8/8/8/8/1rbnNBR1/qrbnNBRQ b - - 11 6");
            game.MakeMove(new Move("C7", "C8", Player.Black), true);
            Assert.False(game.IsWinner(Player.Black));
            Assert.False(game.IsWinner(Player.White));
        }

        [Test]
        public static void TestUndoMove()
        {
            RacingKingsChessGame game = new RacingKingsChessGame();
            string initial = game.GetFen();
            game.MakeMove(new Move("F2", "D4", Player.White), true);
            Assert.True(game.Undo());
            Assert.AreEqual(initial, game.GetFen());
            Assert.AreEqual(Player.White, game.WhoseTurn);
        }

        [Test]
        public static void TestUndoMultiple()
        {
            RacingKingsChessGame game = new RacingKingsChessGame();
            string initial = game.GetFen();
            game.MakeMove(new Move("F2", "D4", Player.White), true);
            game.MakeMove(new Move("A2", "B3", Player.Black), true);
            game.MakeMove(new Move("D4", "B2", Player.White), true);
            game.MakeMove(new Move("D1", "B2", Player.Black), true);
            Assert.AreEqual(4, game.Undo(4));
            Assert.AreEqual(initial, game.GetFen());
            Assert.AreEqual(Player.White, game.WhoseTurn);
        }
    }
}
