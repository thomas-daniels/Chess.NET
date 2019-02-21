using NUnit.Framework;

namespace ChessDotNet.Variants.Tests
{
    using KingOfTheHill;

    [TestFixture]
    public class KothChessGameTests
    {
        [Test]
        public void TestKingNotInCenter()
        {
            KingOfTheHillChessGame game = new KingOfTheHillChessGame("rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2");

            Assert.False(game.IsKingInCenter(Player.White));
            Assert.False(game.IsKingInCenter(Player.Black));
        }

        [Test]
        public void TestWhiteKingInCenter()
        {
            KingOfTheHillChessGame game = new KingOfTheHillChessGame("rnbqkbnr/ppp2ppp/8/4p3/4K3/8/PPPP1PPP/RNBQ1BNR b kq - 0 4");

            Assert.True(game.IsKingInCenter(Player.White));
            Assert.False(game.IsKingInCenter(Player.Black));
            Assert.True(game.IsWinner(Player.White));
        }

        [Test]
        public static void TestUndoOnNewGame()
        {
            const string initialBoard = "8/8/3k4/8/8/4K3/8/Q6R w - - 0 1";
            KingOfTheHillChessGame game = new KingOfTheHillChessGame(initialBoard);
            Assert.False(game.Undo());
        }

        [Test]
        public static void TestUndoLastWhiteMove()
        {
            const string initialBoard = "8/8/3k4/8/8/4K3/8/Q6R w - - 0 1";
            KingOfTheHillChessGame game = new KingOfTheHillChessGame(initialBoard);
            game.MakeMove(new Move("a1", "h8", Player.White), true);
            Assert.True(game.Undo());
            Assert.AreEqual(initialBoard, game.GetFen());
        }

        [Test]
        public static void TestUndoLastBlackMove()
        {
            const string initialBoard = "8/8/3k4/8/8/4K3/8/Q6R b - - 0 1";
            KingOfTheHillChessGame game = new KingOfTheHillChessGame(initialBoard);
            game.MakeMove(new Move("d6", "d7", Player.Black), true);
            Assert.True(game.Undo());
            Assert.AreEqual(initialBoard, game.GetFen());
        }

        [Test]
        public static void TestUndoZeroMove()
        {
            const string initialBoard = "8/8/3k4/8/8/4K3/8/Q6R w - - 0 1";
            KingOfTheHillChessGame game = new KingOfTheHillChessGame(initialBoard);
            Assert.AreEqual(0, game.Undo(0));
            Assert.AreEqual(initialBoard, game.GetFen());
        }

        [Test]
        public static void TestUndoOneMove()
        {
            const string initialBoard = "8/8/3k4/8/8/4K3/8/Q6R w - - 0 1";
            KingOfTheHillChessGame game = new KingOfTheHillChessGame(initialBoard);
            game.MakeMove(new Move("a1", "h8", Player.White), true);
            Assert.AreEqual(1, game.Undo(1));
            Assert.AreEqual(initialBoard, game.GetFen());
        }

        [Test]
        public static void TestUndoUpToThreeMoves()
        {
            const string initialBoard = "8/8/3k4/8/8/4K3/8/Q6R w - - 0 1";
            KingOfTheHillChessGame game = new KingOfTheHillChessGame(initialBoard);
            game.MakeMove(new Move("a1", "h8", Player.White), true);
            game.MakeMove(new Move("d6", "d7", Player.Black), true);
            Assert.AreEqual(2, game.Undo(3));
            Assert.AreEqual(initialBoard, game.GetFen());
        }

        [Test]
        public static void TestUndoCaptureWhite()
        {
            const string initialBoard = "rnbqk1nr/pppp1ppp/8/4p3/1b6/2PP4/PP2PPPP/RNBQKBNR w KQkq - 1 3";
            KingOfTheHillChessGame game = new KingOfTheHillChessGame(initialBoard);
            game.MakeMove(new Move("c3", "b2", Player.White), true);
            game.Undo();
            Assert.AreEqual(initialBoard, game.GetFen());
        }

        [Test]
        public static void TestUndoCaptureBlack()
        {
            const string initialBoard = "rnbqkbnr/pppp1ppp/4p3/8/8/BP6/P1PPPPPP/RN1QKBNR b KQkq - 1 2";
            KingOfTheHillChessGame game = new KingOfTheHillChessGame(initialBoard);
            game.MakeMove(new Move("f8", "a3", Player.Black), true);
            game.Undo();
            Assert.AreEqual(initialBoard, game.GetFen());
        }

        [Test]
        public static void TestUndoPromotionWhite()
        {
            const string initialBoard = "8/1k2P3/8/8/8/8/1K2p3/8 w - - 0 1";
            KingOfTheHillChessGame game = new KingOfTheHillChessGame(initialBoard);
            game.MakeMove(new Move("e7", "e8", Player.White, 'q'), true);
            game.Undo();
            Assert.AreEqual(initialBoard, game.GetFen());
        }

        [Test]
        public static void TestUndoPromotionBlack()
        {
            const string initialBoard = "8/1k2P3/8/8/8/8/1K2p3/8 b - - 0 1";
            KingOfTheHillChessGame game = new KingOfTheHillChessGame(initialBoard);
            game.MakeMove(new Move("e2", "e1", Player.Black, 'N'), true);
            game.Undo();
            Assert.AreEqual(initialBoard, game.GetFen());
        }

        [Test]
        public static void TestUndoEnPassantWhite()
        {
            const string initialBoard = "8/1k6/8/3pP3/8/8/1K6/8 w - d6 0 2";
            KingOfTheHillChessGame game = new KingOfTheHillChessGame(initialBoard);
            game.MakeMove(new Move("e5", "d6", Player.White), true);
            game.Undo();
            Assert.AreEqual(initialBoard, game.GetFen());
        }

        [Test]
        public static void TestUndoEnPassantBlack()
        {
            const string initialBoard = "8/1k2P3/8/8/4pP2/8/1K6/8 b - f3 0 1";
            KingOfTheHillChessGame game = new KingOfTheHillChessGame(initialBoard);
            game.MakeMove(new Move("e4", "f3", Player.Black), true);
            game.Undo();
            Assert.AreEqual(initialBoard, game.GetFen());
        }

        [Test]
        public static void TestUndoQueenSideCastles()
        {
            const string pgn = "1. d4 d5 2. Be3 Be6 3. Nc3 Nc6 4. Qd3 Qd6 5. O-O-O O-O-O";
            var reader = new PgnReader<ChessGame>();
            reader.ReadPgnFromString(pgn);
            var game = reader.Game;

            game.Undo();
            Assert.True(game.CanBlackCastleQueenSide);
            Assert.True(game.CanBlackCastleKingSide);
            game.Undo();
            Assert.True(game.CanWhiteCastleQueenSide);
            Assert.True(game.CanWhiteCastleKingSide);
        }

        [Test]
        public static void TestUndoQueenSideCastlesWithKingSideInactive()
        {
            const string pgn = "1. d4 d5 2. Be3 Be6 3. Nc3 Nc6 4. Qd3 Qd6 5. h3 h6 6. Rh2 Rh7 7. Rh1 Rh8 8. O-O-O O-O-O";
            var reader = new PgnReader<ChessGame>();
            reader.ReadPgnFromString(pgn);
            var game = reader.Game;

            game.Undo();
            Assert.True(game.CanBlackCastleQueenSide);
            Assert.False(game.CanBlackCastleKingSide);
            game.Undo();
            Assert.True(game.CanWhiteCastleQueenSide);
            Assert.False(game.CanWhiteCastleKingSide);
        }

        [Test]
        public static void TestUndoKingSideCastles()
        {
            const string pgn = "1. d4 d5 2. Be3 Be6 3. Nc3 Nc6 4. Qd3 Qd6 5. O-O-O O-O-O";
            var reader = new PgnReader<ChessGame>();
            reader.ReadPgnFromString(pgn);
            var game = reader.Game;

            game.Undo();
            Assert.True(game.CanBlackCastleQueenSide);
            Assert.True(game.CanBlackCastleKingSide);
            game.Undo();
            Assert.True(game.CanWhiteCastleQueenSide);
            Assert.True(game.CanWhiteCastleKingSide);
        }

        [Test]
        public static void TestUndoKingSideCastlesWithQueenSideInactive()
        {
            const string pgn = "1. e4 e5 2. Bc4 Bc5 3. Nf3 Nf6 4. a3 a6 5. Ra2 Ra7 6. Ra1 Ra8 7. O-O O-O";
            var reader = new PgnReader<ChessGame>();
            reader.ReadPgnFromString(pgn);
            var game = reader.Game;

            game.Undo();
            Assert.False(game.CanBlackCastleQueenSide);
            Assert.True(game.CanBlackCastleKingSide);
            game.Undo();
            Assert.False(game.CanWhiteCastleQueenSide);
            Assert.True(game.CanWhiteCastleKingSide);
        }
    }
}
