using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public static class FenConvertTests
    {
        [Test]
        public static void TestStartPosition()
        {
            ChessGame game = new ChessGame();
            string fen = FenConvert.GameToFen(game);
            Assert.AreEqual("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", fen);
        }

        [Test]
        public static void TestAfter1e4()
        {
            ChessGame game = new ChessGame();
            game.ApplyMove(new Move("E2", "E4", Player.White), true);
            string fen = FenConvert.GameToFen(game);
            Assert.AreEqual("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1", fen);
        }

        [Test]
        public static void TestAfter1c5()
        {
            ChessGame game = new ChessGame();
            game.ApplyMove(new Move("E2", "E4", Player.White), true);
            game.ApplyMove(new Move("C7", "C5", Player.Black), true);
            string fen = FenConvert.GameToFen(game);
            Assert.AreEqual("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2", fen);
        }

        [Test]
        public static void TestAfter2Nf3()
        {
            ChessGame game = new ChessGame();
            game.ApplyMove(new Move("E2", "E4", Player.White), true);
            game.ApplyMove(new Move("C7", "C5", Player.Black), true);
            game.ApplyMove(new Move("G1", "F3", Player.White), true);
            string fen = FenConvert.GameToFen(game);
            Assert.AreEqual("rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2", fen);
        }

        [Test]
        public static void TestMovingWhiteKingLosingCastlingRights()
        {
            ChessGame game = new ChessGame();
            game.ApplyMove(new Move("E2", "E4", Player.White), true);
            game.ApplyMove(new Move("C7", "C5", Player.Black), true);
            game.ApplyMove(new Move("E1", "E2", Player.White), true);
            string fen = FenConvert.GameToFen(game);
            Assert.AreEqual("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPPKPPP/RNBQ1BNR b kq - 1 2", fen);
        }

        [Test]
        public static void TestMovingBlackKingLosingCastlingRights()
        {
            ChessGame game = new ChessGame();
            game.ApplyMove(new Move("E2", "E4", Player.White), true);
            game.ApplyMove(new Move("E7", "E5", Player.Black), true);
            game.ApplyMove(new Move("G1", "F3", Player.White), true);
            game.ApplyMove(new Move("E8", "E7", Player.Black), true);
            string fen = FenConvert.GameToFen(game);
            Assert.AreEqual("rnbq1bnr/ppppkppp/8/4p3/4P3/5N2/PPPP1PPP/RNBQKB1R w KQ - 2 3", fen);
        }

        [Test]
        public static void TestMovingWhiteARookLosingCastlingRights()
        {
            ChessGame game = new ChessGame();
            game.ApplyMove(new Move("A2", "A3", Player.White), true);
            game.ApplyMove(new Move("E7", "E5", Player.Black), true);
            game.ApplyMove(new Move("A1", "A2", Player.White), true);
            string fen = FenConvert.GameToFen(game);
            Assert.AreEqual("rnbqkbnr/pppp1ppp/8/4p3/8/P7/RPPPPPPP/1NBQKBNR b Kkq - 1 2", fen);
        }

        [Test]
        public static void TestMovingWhiteHRookLosingCastlingRights()
        {
            ChessGame game = new ChessGame();
            game.ApplyMove(new Move("H2", "H3", Player.White), true);
            game.ApplyMove(new Move("E7", "E5", Player.Black), true);
            game.ApplyMove(new Move("H1", "H2", Player.White), true);
            string fen = FenConvert.GameToFen(game);
            Assert.AreEqual("rnbqkbnr/pppp1ppp/8/4p3/8/7P/PPPPPPPR/RNBQKBN1 b Qkq - 1 2", fen);
        }

        [Test]
        public static void TestMovingBlackARookLosingCastlingRights()
        {
            ChessGame game = new ChessGame();
            game.ApplyMove(new Move("E2", "E4", Player.White), true);
            game.ApplyMove(new Move("A7", "A6", Player.Black), true);
            game.ApplyMove(new Move("G1", "F3", Player.White), true);
            game.ApplyMove(new Move("A8", "A7", Player.Black), true);
            string fen = FenConvert.GameToFen(game);
            Assert.AreEqual("1nbqkbnr/rppppppp/p7/8/4P3/5N2/PPPP1PPP/RNBQKB1R w KQk - 2 3", fen);
        }

        [Test]
        public static void TestMovingBlackHRookLosingCastlingRights()
        {
            ChessGame game = new ChessGame();
            game.ApplyMove(new Move("E2", "E4", Player.White), true);
            game.ApplyMove(new Move("H7", "H6", Player.Black), true);
            game.ApplyMove(new Move("G1", "F3", Player.White), true);
            game.ApplyMove(new Move("H8", "H7", Player.Black), true);
            string fen = FenConvert.GameToFen(game);
            Assert.AreEqual("rnbqkbn1/pppppppr/7p/8/4P3/5N2/PPPP1PPP/RNBQKB1R w KQq - 2 3", fen);
        }

        [Test]
        public static void TestHalfmoveClockAndFullmoveNumber()
        {
            ChessGame game = new ChessGame();
            game.ApplyMove(new Move("E2", "E4", Player.White), true);
            game.ApplyMove(new Move("E7", "E5", Player.Black), true);
            game.ApplyMove(new Move("E1", "E2", Player.White), true);
            game.ApplyMove(new Move("E8", "E7", Player.Black), true);
            game.ApplyMove(new Move("E2", "D3", Player.White), true);
            game.ApplyMove(new Move("E7", "D6", Player.Black), true);
            game.ApplyMove(new Move("D3", "C3", Player.White), true);
            game.ApplyMove(new Move("D6", "C6", Player.Black), true);
            game.ApplyMove(new Move("C3", "B3", Player.White), true);
            game.ApplyMove(new Move("C6", "B6", Player.Black), true);
            game.ApplyMove(new Move("B3", "A4", Player.White), true);
            game.ApplyMove(new Move("B6", "C5", Player.Black), true);
            game.ApplyMove(new Move("F1", "C4", Player.White), true);
            string fen = FenConvert.GameToFen(game);
            Assert.AreEqual("rnbq1bnr/pppp1ppp/8/2k1p3/K1B1P3/8/PPPP1PPP/RNBQ2NR b - - 11 7", fen);
            game.ApplyMove(new Move("C5", "C4", Player.Black), true);
            fen = FenConvert.GameToFen(game);
            Assert.AreEqual("rnbq1bnr/pppp1ppp/8/4p3/K1k1P3/8/PPPP1PPP/RNBQ2NR w - - 0 8", fen);
            game.ApplyMove(new Move("A4", "A5", Player.White), true);
            game.ApplyMove(new Move("H7", "H5", Player.Black), true);
            fen = FenConvert.GameToFen(game);
            Assert.AreEqual("rnbq1bnr/pppp1pp1/8/K3p2p/2k1P3/8/PPPP1PPP/RNBQ2NR w - h6 0 9", fen);
        }
    }
}
