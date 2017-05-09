using NUnit.Framework;
using System.Collections.ObjectModel;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public static class Chess960CastlingTests
    {
        [Test]
        public static void Test960CastlingBlack1()
        {
            ChessGame game = new ChessGame("q1nbrk1r/pp2p1pp/2npbp2/8/4P3/1P3P2/P1PP1BPP/QNNBRRK1 b kq - 3 6");
            Assert.True(game.IsValidMove(new Move("F8", "H8", Player.Black)), "Kingside castling should be valid.");
            Assert.False(game.IsValidMove(new Move("F8", "E8", Player.Black)), "Queenside castling should be invalid.");

            Assert.Contains(new Move("F8", "H8", Player.Black), game.GetValidMoves(Player.Black));
            Assert.Contains(new Move("F8", "H8", Player.Black), game.GetValidMoves(new Position("F8")));

            game.ApplyMove(new Move("F8", "H8", Player.Black), true);
            Assert.AreEqual("q1nbrrk1/pp2p1pp/2npbp2/8/4P3/1P3P2/P1PP1BPP/QNNBRRK1 w - - 4 7", game.GetFen());
        }

        [Test]
        public static void Test960CastlingBlack2()
        {
            ChessGame game = new ChessGame("r1k3br/pppp2pp/2n2p2/8/7N/8/PKP2PPP/3R2BR b kq - 2 12");
            Assert.True(game.IsValidMove(new Move("C8", "A8", Player.Black)), "Queenside castling should be valid.");
            Assert.False(game.IsValidMove(new Move("C8", "H8", Player.Black)), "Kingside castling should be invalid.");

            Assert.Contains(new Move("C8", "A8", Player.Black), game.GetValidMoves(Player.Black));
            Assert.Contains(new Move("C8", "A8", Player.Black), game.GetValidMoves(new Position("C8")));

            game.ApplyMove(new Move("C8", "A8", Player.Black), true);
            Assert.AreEqual("2kr2br/pppp2pp/2n2p2/8/7N/8/PKP2PPP/3R2BR w - - 3 13", game.GetFen());
        }

        [Test]
        public static void Test960CastlingBlack3()
        {
            ChessGame game = new ChessGame("nrk1rnqb/pppp2pp/5pb1/4p3/4B2P/PN1P2P1/1PP1PP2/1RKRBNQ1 b KQq - 0 7");
            Assert.True(game.IsValidMove(new Move("C8", "B8", Player.Black)));
        }

        [Test]
        public static void Test960CastlingWhite1()
        {
            ChessGame game = new ChessGame("qnbbrkr1/pppp1ppp/6n1/4p3/8/1P4N1/P1PPPPPP/QNBBRKR1 w KQkq - 1 3");
            Assert.True(game.IsValidMove(new Move("F1", "G1", Player.White)), "Kingside castling should be valid.");
            Assert.False(game.IsValidMove(new Move("F1", "E1", Player.White)), "Queenside castling should be invalid.");

            Assert.Contains(new Move("F1", "G1", Player.White), game.GetValidMoves(Player.White));
            Assert.Contains(new Move("F1", "G1", Player.White), game.GetValidMoves(new Position("F1")));

            game.ApplyMove(new Move("F1", "G1", Player.White), true);
            Assert.AreEqual("qnbbrkr1/pppp1ppp/6n1/4p3/8/1P4N1/P1PPPPPP/QNBBRRK1 b kq - 2 3", game.GetFen());
        }

        [Test]
        public static void Test960CastlingWhite2()
        {
            ChessGame game = new ChessGame("bbrqknr1/pp1ppppp/6n1/2p5/4P3/5Q2/PPPP1PPP/BBR1KNRN w KQkq - 2 3");
            Assert.True(game.IsValidMove(new Move("E1", "C1", Player.White)), "Queenside castling should be valid.");
            Assert.False(game.IsValidMove(new Move("E1", "G1", Player.White)), "Kingside castling should be invalid.");

            Assert.Contains(new Move("E1", "C1", Player.White), game.GetValidMoves(Player.White));
            Assert.Contains(new Move("E1", "C1", Player.White), game.GetValidMoves(new Position("E1")));

            game.ApplyMove(new Move("E1", "C1", Player.White), true);
            Assert.AreEqual("bbrqknr1/pp1ppppp/6n1/2p5/4P3/5Q2/PPPP1PPP/BBKR1NRN b kq - 3 3", game.GetFen());
        }

        [Test]
        public static void Test960Castling_GetValidMoves()
        {
            ChessGame game = new ChessGame("rk2rbbq/1p1pp3/p1pnnppp/8/8/3NNQPP/PPPPPPBB/RK2R3 w KQkq - 0 1");

            ReadOnlyCollection<Move> validMoves = game.GetValidMoves(Player.White);
            Assert.Contains(new Move("B1", "A1", Player.White), validMoves);
            Assert.Contains(new Move("B1", "E1", Player.White), validMoves);
        }

        [Test]
        public static void TestCastling_NoCastlingButCapture()
        {
            ChessGame game = new ChessGame("r2q3r/4bkpp/p3Rn2/1ppp4/3P4/2P5/PP1B1PPP/R2Q2K1 b - - 0 1");
            game.ApplyMove(new Move("F7", "E6", Player.Black), true);
            Assert.AreEqual("r2q3r/4b1pp/p3kn2/1ppp4/3P4/2P5/PP1B1PPP/R2Q2K1 w - - 0 2", game.GetFen());
        }
    }
}
