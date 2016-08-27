using NUnit.Framework;

namespace ChessDotNet.Variants.Tests
{
    using Horde;

    [TestFixture]
    public class HordeChessGameTests
    {
        [Test]
        public static void TestInitialPositionFen()
        {
            HordeChessGame game = new HordeChessGame();
            Assert.AreEqual("rnbqkbnr/pppppppp/8/1PP2PP1/PPPPPPPP/PPPPPPPP/PPPPPPPP/PPPPPPPP w kq - 0 1", game.GetFen());
        }

        [Test]
        public static void TestValidMovesPawnFirstRank()
        {
            HordeChessGame game = new HordeChessGame("rnbq1rk1/1p2bppp/2p5/PPP2PPP/PPPPPPP1/2P2PPP/1P1PPPPP/PPPPPPPP w - - 1 15");
            Assert.True(game.IsValidMove(new Move("A1", "A2", Player.White)));
            Assert.True(game.IsValidMove(new Move("A1", "A3", Player.White)));
            Assert.AreEqual(2, game.GetValidMoves(new Position("A1")).Count);
        }

        [Test]
        public static void TestValidMovesPawnSecondRank()
        {
            HordeChessGame game = new HordeChessGame("r1bq1rk1/1p1nbppp/2p5/PPP1PPPP/PPPP1PP1/2P2PPP/1P1PPPPP/PPPPPPPP w - - 1 16");
            Assert.True(game.IsValidMove(new Move("E2", "E3", Player.White)));
            Assert.True(game.IsValidMove(new Move("E2", "E4", Player.White)));
            Assert.AreEqual(2, game.GetValidMoves(new Position("E2")).Count);
        }

        [Test]
        public static void TestValidMovesPawnThirdRank()
        {
            HordeChessGame game = new HordeChessGame("r1bq1rk1/1p1nbp1p/2p4p/PPP1PPP1/PPPP1PP1/2P2PPP/1P1PPPPP/PPPPPPPP w - - 0 17");
            Assert.True(game.IsValidMove(new Move("H3", "H4", Player.White)));
            Assert.False(game.IsValidMove(new Move("H3", "H5", Player.White)));
            Assert.AreEqual(1, game.GetValidMoves(new Position("H3")).Count);
        }

        [Test]
        public static void TestHordePawnCapture()
        {
            HordeChessGame game = new HordeChessGame("r1bq1rk1/1p1nbp1p/2p4p/PPP1PPP1/PPPP1PP1/2P2PPP/1P1PPPPP/PPPPPPPP w - - 0 17");
            Assert.True(game.IsValidMove(new Move("G5", "H6", Player.White)));
            Assert.AreEqual(2, game.GetValidMoves(new Position("G5")).Count);
        }

        [Test]
        public static void TestEnPassantCaptureWhite1()
        {
            HordeChessGame game = new HordeChessGame("rnbqkbnr/pppp1ppp/8/1PPPpPP1/PPP1PPPP/PPPPPPPP/PPPPPPPP/PPPPPPPP w kq e6 0 2");
            Assert.True(game.IsValidMove(new Move("F5", "E6", Player.White)));
            Assert.True(game.IsValidMove(new Move("D5", "E6", Player.White)));
        }

        [Test]
        public static void TestEnPassantCaptureWhite2()
        {
            HordeChessGame game = new HordeChessGame("rnbqkbnr/pppppppp/8/1PPP1PP1/PPP1PPPP/PPPPPPPP/PPPPPPPP/PPPPPPPP b kq - 0 1");
            Assert.AreNotEqual(MoveType.Invalid, game.ApplyMove(new Move("E7", "E5", Player.Black), false));
            Assert.True(game.IsValidMove(new Move("F5", "E6", Player.White)));
            Assert.True(game.IsValidMove(new Move("D5", "E6", Player.White)));
        }

        [Test]
        public static void TestInvalidEnPassantCaptureBlack()
        {
            HordeChessGame game = new HordeChessGame("rnbq3r/pppp1kpp/5P1n/1P1P1P1P/P1P1PPP1/1PPPP1pP/PbPPP1PP/PPPPPPPP w - - 0 10");
            game.ApplyMove(new Move("F1", "F3", Player.White), true);
            Assert.False(game.IsValidMove(new Move("G3", "F2", Player.Black)));
        }

        [Test]
        public static void TestValidEnPassantCaptureBlack()
        {
            HordeChessGame game = new HordeChessGame("rnbqk1nr/pppp1ppp/5P2/PP1PPPP1/P1P3pP/1P1PP1PP/P1PPPPPP/bPPPPPPP w kq - 0 9");
            game.ApplyMove(new Move("F2", "F4", Player.White), true);
            Assert.True(game.IsValidMove(new Move("G4", "F3", Player.Black)));
        }

        [Test]
        public static void TestIsWinnerWhite()
        {
            HordeChessGame game = new HordeChessGame("3Q4/8/8/1k6/2Q5/1P4PP/PP1P2PP/PPPPPPPP b - - 2 57");
            Assert.True(game.IsWinner(Player.White));
        }

        [Test]
        public static void TestIsStalematedBlack()
        {
            HordeChessGame game = new HordeChessGame("3Q4/k7/2Q5/8/8/1P4PP/PP1P2PP/PPPPPPPP b - - 0 55");
            Assert.True(game.IsStalemated(Player.Black));
        }

        [Test]
        public static void TestIsStalematedWhite()
        {
            HordeChessGame game = new HordeChessGame("8/3k4/5p1q/7P/8/5r2/5P2/8 w - - 1 52");
            Assert.True(game.IsStalemated(Player.White));
        }

        [Test]
        public static void TestIsWinnerBlack()
        {
            HordeChessGame game = new HordeChessGame("8/3k4/5p2/7q/8/5r2/8/8 w - - 0 53");
            Assert.True(game.IsHordeDestroyed());
            Assert.True(game.IsWinner(Player.Black));
        }

        [Test]
        public static void TestHordeNotDestroyed()
        {
            HordeChessGame game = new HordeChessGame("8/3k4/5p2/5r1q/8/8/5P2/8 w - - 0 52");
            Assert.False(game.IsHordeDestroyed());
        }

        [Test]
        public static void TestFenEnPassantField1()
        {
            HordeChessGame game = new HordeChessGame("rn1qkbnr/pp4p1/8/1PP1P3/PPP1bPPP/PPP3PP/PPP3P1/PPPPPPPP w kq - 0 16");
            game.ApplyMove(new Move("D1", "D3", Player.White), true);
            Assert.AreEqual("rn1qkbnr/pp4p1/8/1PP1P3/PPP1bPPP/PPPP2PP/PPP3P1/PPP1PPPP b kq - 0 16", game.GetFen());
        }

        [Test]
        public static void TestFenEnPassantField2()
        {
            HordeChessGame game = new HordeChessGame("rn1qkbnr/pp4p1/8/1PP1P3/PPP1bPPP/PPP3PP/PPPP2P1/PPP1PPPP w kq - 0 16");
            game.ApplyMove(new Move("D2", "D4", Player.White), true);
            Assert.AreEqual("rn1qkbnr/pp4p1/8/1PP1P3/PPPPbPPP/PPP3PP/PPP3P1/PPP1PPPP b kq d3 0 16", game.GetFen());
        }
    }
}
