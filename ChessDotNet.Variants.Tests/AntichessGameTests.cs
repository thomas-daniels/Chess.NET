using NUnit.Framework;
using System.Collections.ObjectModel;

namespace ChessDotNet.Variants.Tests
{
    using Antichess;
    using Antichess.Pieces;

    [TestFixture]
    public class AntichessGameTests
    {
        [Test]
        public static void TestIsValidMoveForcedCapture()
        {
            AntichessGame game = new AntichessGame("rnbqkbnr/p1pppppp/8/1p6/8/4P3/PPPP1PPP/RNBQKBNR w - - 0 2");
            Assert.True(game.IsValidMove(new Move("F1", "B5", Player.White)));
        }

        [Test]
        public static void TestIsValidMoveNoForcedCapture()
        {
            AntichessGame game = new AntichessGame();
            Assert.True(game.IsValidMove(new Move("G1", "H3", Player.White)));
        }

        [Test]
        public static void TestIsInvalidMove_ForcedCaptureExists()
        {
            AntichessGame game = new AntichessGame("rnbqkbnr/p1pppppp/8/1p6/8/4P3/PPPP1PPP/RNBQKBNR w - - 0 2");
            Assert.False(game.IsValidMove(new Move("H2", "H3", Player.White)));
        }

        [Test]
        public static void TestIsInvalidMove_IllegalMove()
        {
            AntichessGame game = new AntichessGame();
            Assert.False(game.IsValidMove(new Move("G1", "G3", Player.White)));
        }

        [Test]
        public static void TestIsInvalidMove_WrongTurnColor()
        {
            AntichessGame game = new AntichessGame("rnbqkbnr/p1pppppp/8/1p6/8/4P3/PPPP1PPP/RNBQKBNR w - - 0 2");
            Assert.False(game.IsValidMove(new Move("H7", "H6", Player.Black)));
        }

        [Test]
        public static void TestGetValidMovesBlackNoForcedCaptures()
        {
            AntichessGame game = new AntichessGame("rnbqkbnr/pppppppp/8/8/8/4P3/PPPP1PPP/RNBQKBNR b - - 0 1");
            Assert.AreEqual(20, game.GetValidMoves(Player.Black).Count);
        }

        [Test]
        public static void TestGetValidMovesWhiteWithForcedCaptures()
        {
            AntichessGame game = new AntichessGame("rn1qkbnr/p1pppppp/b7/1B6/8/4P3/PPPP1PPP/RNBQK1NR w - - 1 3");
            ReadOnlyCollection<Move> validMoves = game.GetValidMoves(Player.White);
            Assert.AreEqual(2, validMoves.Count);
            Assert.Contains(new Move("B5", "A6", Player.White), validMoves);
            Assert.Contains(new Move("B5", "D7", Player.White), validMoves);
        }

        [Test]
        public static void TestFenGeneration()
        {
            AntichessGame game = new AntichessGame();
            game.MakeMove(new Move("E2", "E3", Player.White), true);
            Assert.AreEqual("rnbqkbnr/pppppppp/8/8/8/4P3/PPPP1PPP/RNBQKBNR b - - 0 1", game.GetFen());
        }

        [Test]
        public static void TestIsWinnerWithNoPiecesLeft()
        {
            AntichessGame game = new AntichessGame("8/8/5B2/6P1/6B1/1P2P3/P1PQ1P1P/1N2K1NR b - - 0 19");
            Assert.True(game.IsStalemated(Player.Black));
            Assert.True(game.IsWinner(Player.Black));
        }

        [Test]
        public static void TestIsWinnerWithAPieceLeft()
        {
            AntichessGame game = new AntichessGame("1nbqk3/3pp3/2p2p2/5P2/8/8/8/4r3 w - - 0 20");
            Assert.True(game.IsStalemated(Player.White));
            Assert.True(game.IsWinner(Player.White));
        }

        [Test]
        public static void TestNoCastling()
        {
            AntichessGame game = new AntichessGame();
            game.MakeMove(new Move("E2", "E3", Player.White), true);
            game.MakeMove(new Move("E7", "E6", Player.Black), true);
            game.MakeMove(new Move("G1", "F3", Player.White), true);
            game.MakeMove(new Move("G8", "F6", Player.Black), true);
            game.MakeMove(new Move("F1", "E2", Player.White), true);
            game.MakeMove(new Move("F8", "E7", Player.Black), true);
            Assert.False(game.IsValidMove(new Move("E1", "G1", Player.White)));
        }

        [Test]
        public static void TestPromotion()
        {
            AntichessGame game = new AntichessGame("8/7P/6n1/8/8/8/8/8 w - - 0 1");
            Assert.AreEqual(5, game.GetValidMoves(Player.White).Count);
            Assert.True(game.IsValidMove(new Move("H7", "H8", Player.White, 'K')));
        }

        [Test]
        public static void TestNotStalemated()
        {
            AntichessGame game = new AntichessGame("8/7P/6n1/8/8/8/8/8 w - - 0 1");
            Assert.False(game.IsStalemated(Player.White));
        }

        [Test]
        public static void TestEnPassant()
        {
            AntichessGame game = new AntichessGame("rnbqkbnr/p1ppp2p/1p6/5PN1/8/1P6/P1PPPP1P/RNBQKB1R b - - 0 5");
            game.MakeMove(new Move("E7", "E5", Player.Black), true);
            Assert.True(game.IsValidMove(new Move("F5", "E6", Player.White)));
        }

        [Test]
        public static void TestUndoMove()
        {
            AntichessGame game = new AntichessGame();
            string initialFen = game.GetFen();
            game.MakeMove(new Move("E2", "E3", Player.White), true);
            Assert.True(game.Undo());
            Assert.AreEqual(initialFen, game.GetFen());
            Assert.AreEqual(0, game.Moves.Count);
            Assert.AreEqual(Player.White, game.WhoseTurn);
        }

        [Test]
        public static void TestUndoCapture()
        {
            AntichessGame game = new AntichessGame();
            game.MakeMove(new Move("E2", "E3", Player.White), true);
            game.MakeMove(new Move("B7", "B5", Player.Black), true);
            string expected = game.GetFen();
            game.MakeMove(new Move("F1", "B5", Player.White), true);
            Assert.True(game.Undo());
            Assert.AreEqual(expected, game.GetFen());
            Assert.AreEqual(2, game.Moves.Count);
            Assert.AreEqual(Player.White, game.WhoseTurn);
        }

        [Test]
        public static void TestUndoPromotion()
        {
            string fen = "8/5P2/2b5/8/8/8/5b2/8 w - - 0 39";
            AntichessGame game = new AntichessGame(fen);
            game.MakeMove(new Move("F7", "F8", Player.White, 'K'), true);
            Assert.True(game.Undo());
            Assert.AreEqual(fen, game.GetFen());
            Assert.AreEqual(Player.White, game.WhoseTurn);
        }

        [Test]
        public static void TestUndoCapturePromotion()
        {
            string fen = "rnbqkb1r/pppp1ppp/7n/8/8/7P/PPPPPp2/RNB1QBNR b - - 1 6";
            AntichessGame game = new AntichessGame(fen);
            game.MakeMove(new Move("F2", "E1", Player.Black, 'R'), true);
            Assert.True(game.Undo());
            Assert.AreEqual(fen, game.GetFen());
            Assert.AreEqual(Player.Black, game.WhoseTurn);
        }

        [Test]
        public static void TestUndoEnPassant()
        {
            string fen = "rnbqkb1r/pppp1ppp/7n/8/5pP1/7P/PPPPP3/RNBQKBNR b - g3 0 4";
            AntichessGame game = new AntichessGame(fen);
            game.MakeMove(new Move("F4", "G3", Player.Black), true);
            Assert.True(game.Undo());
            Assert.AreEqual(fen, game.GetFen());
            Assert.AreEqual(Player.Black, game.WhoseTurn);
        }

        [Test]
        public static void TestUndoMultiple()
        {
            string fen = "rnbqkb1r/pppp1ppp/7n/8/5pP1/7P/PPPPP3/RNBQKBNR b - g3 0 4";
            AntichessGame game = new AntichessGame(fen);
            game.MakeMove(new Move("F4", "G3", Player.Black), true);
            game.MakeMove(new Move("E1", "F2", Player.White), true);
            game.MakeMove(new Move("G3", "F2", Player.Black), true);
            game.MakeMove(new Move("D1", "E1", Player.White), true);
            game.MakeMove(new Move("F2", "E1", Player.White, 'N'), true);
            Assert.AreEqual(5, game.Undo(5));
            Assert.AreEqual(fen, game.GetFen());
            Assert.AreEqual(Player.Black, game.WhoseTurn);
        }
    }
}
