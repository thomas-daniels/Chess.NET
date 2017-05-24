using ChessDotNet.Pieces;
using ChessDotNet.Variants.Crazyhouse;
using NUnit.Framework;

namespace ChessDotNet.Variants.Tests
{
    [TestFixture]
    public class CrazyhouseChessGameTests
    {
        [Test]
        public static void TestFenParsing()
        {
            CrazyhouseChessGame game = new CrazyhouseChessGame("rn2q1k1/ppp1b1pp/4p1b1/4N3/3P4/4B3/PPP4P/5Q1K/RPRPPPPBNrn w - - 52 27");
            CrazyhouseChessGame game2 = new CrazyhouseChessGame("rn2q1k1/ppp1b1pp/4p1b1/4N3/3P4/4B3/PPP4P/5Q1K[RPRPPPPBNrn] w - - 52 27");
            Assert.AreEqual(9, game.WhitePocket.Count);
            Assert.AreEqual(2, game.BlackPocket.Count);
            Assert.AreEqual(21, game.PiecesOnBoard.Count);
            Assert.AreEqual(9, game2.WhitePocket.Count);
            Assert.AreEqual(2, game2.BlackPocket.Count);
            Assert.AreEqual(21, game2.PiecesOnBoard.Count);
        }

        [Test]
        public static void TestIsInCheck()
        {
            CrazyhouseChessGame game = new CrazyhouseChessGame("rnbqkb1r/p1p2ppp/5p2/1B1p4/3P4/8/PPP2PPP/RNBQK1NR/PNp b KQkq - 9 5");
            Assert.True(game.IsInCheck(Player.Black));
            Assert.False(game.IsInCheck(Player.White));
            Assert.False(game.IsCheckmated(Player.Black));
        }

        [Test]
        public static void TestIsCheckmated()
        {
            CrazyhouseChessGame game = new CrazyhouseChessGame("r2q1r2/ppp4p/2np1Pp1/8/4P1Pk/6bP/PB3bPq/R6K/Pprnnnbpp w - - 76 39");
            Assert.True(game.IsCheckmated(Player.White));
            Assert.False(game.IsStalemated(Player.White));
        }

        [Test]
        public static void TestIsStalemated()
        {
            CrazyhouseChessGame game = new CrazyhouseChessGame("rnbqkbnr/pppppppp/8/5K2/8/8/ppp1qppp/rnb2bnr/pp w kq - 98 71");
            Assert.True(game.IsStalemated(Player.White));
            Assert.False(game.IsCheckmated(Player.White));
        }

        [Test]
        public static void TestNotCheckmatedAndIsValidDrop()
        {
            CrazyhouseChessGame game = new CrazyhouseChessGame("rnbqkbnr/1pppp2p/5p2/6pQ/1p1PP3/8/PBP2PPP/RN2KBNR/p b KQkq - 9 5");
            Assert.True(game.IsInCheck(Player.Black));
            Assert.False(game.IsCheckmated(Player.Black));

            Assert.True(game.IsValidDrop(new Drop(new Pawn(Player.Black), new Position("G6"), Player.Black)));
            Assert.False(game.IsValidDrop(new Drop(new Pawn(Player.Black), new Position("B5"), Player.Black)));
            Assert.False(game.IsValidDrop(new Drop(new Knight(Player.Black), new Position("G6"), Player.Black)));
        }

        [Test]
        public static void TestNotStalemated_CanDrop()
        {
            CrazyhouseChessGame game = new CrazyhouseChessGame("rnbqkbnr/pppppppp/8/7K/5q2/8/ppp2ppp/rnb2bnr/Pp w kq - 0 1");
            Assert.False(game.IsStalemated(Player.White));

            Assert.AreEqual(32, game.GetValidDrops(Player.White).Count);
        }

        [Test]
        public static void TestIsValidDrop_PawnsVsPieces()
        {
            CrazyhouseChessGame game = new CrazyhouseChessGame("rnbqk2r/1pppppb1/7p/8/p7/8/PPPPKPPP/RNBQ1BNR/PNp w kq - 10 6");
            Assert.False(game.IsValidDrop(new Drop(new Pawn(Player.White), new Position("E1"), Player.White)));
            Assert.True(game.IsValidDrop(new Drop(new Knight(Player.White), new Position("E1"), Player.White)));
        }

        [Test]
        public static void TestGetValidDrops()
        {
            CrazyhouseChessGame game = new CrazyhouseChessGame("rnbqk2r/1pppppb1/7p/8/p7/8/PPPPKPPP/RNBQ1BNR/PNp w kq - 10 6");
            Assert.AreEqual(67, game.GetValidDrops(Player.White).Count);
        }

        [Test]
        public static void TestApplyMove_AddToPocketIfCapture_AndFenGeneration_AndApplyDrop()
        {
            CrazyhouseChessGame game = new CrazyhouseChessGame();
            game.ApplyMove(new Move("E2", "E4", Player.White), true);
            game.ApplyMove(new Move("D7", "D5", Player.Black), true);
            game.ApplyMove(new Move("E4", "D5", Player.White), true);
            Assert.AreEqual(new Pawn(Player.White), game.WhitePocket[0]);
            Assert.AreEqual("rnbqkbnr/ppp1pppp/8/3P4/8/8/PPPP1PPP/RNBQKBNR/P b KQkq - 0 2", game.GetFen());

            game.ApplyMove(new Move("A7", "A5", Player.Black), true);
            Assert.True(game.ApplyDrop(new Drop(new Pawn(Player.White), new Position("H3"), Player.White), false));
            Assert.AreEqual("rnbqkbnr/1pp1pppp/8/p2P4/8/7P/PPPP1PPP/RNBQKBNR b KQkq - 1 3", game.GetFen());
            Assert.AreEqual(0, game.WhitePocket.Count);
        }

        [Test]
        public static void TestPgnRead()
        {
            PgnReader<CrazyhouseChessGame> reader = new PgnReader<CrazyhouseChessGame>();
            reader.ReadPgnFromString("1. e4 d5 2. exd5 a5 3. @h3 Qxd5 4. Nc3 Qxg2 5. Bxg2 Nc6 6. Bxc6+ bxc6 7. Q@e4 B@e6");
            Assert.AreEqual(2, reader.Game.BlackPocket.Count);
            Assert.AreEqual(1, reader.Game.WhitePocket.Count);
            Assert.AreEqual("r1b1kbnr/2p1pppp/2p1b3/p7/4Q3/2N4P/PPPP1P1P/R1BQK1NR/Npp w KQkq - 2 8", reader.Game.GetFen());

            Assert.True(reader.Game.Moves[0] is CrazyhouseDetailedMove);
            Assert.True(reader.Game.Moves[4] is CrazyhouseDetailedMove);
            Assert.AreEqual(new Drop(new Pawn(Player.White), new Position("H3"), Player.White), (reader.Game.Moves[4] as CrazyhouseDetailedMove).Drop);
        }

        [Test]
        public static void CapturePromotedPiece()
        {
            CrazyhouseChessGame game = new CrazyhouseChessGame("3r4/1P6/4k3/8/8/8/8/4K3 w - - 0 1");
            game.ApplyMove(new Move("B7", "B8", Player.White, 'Q'), true);
            game.ApplyMove(new Move("D8", "B8", Player.Black), true);
            Assert.AreEqual(1, game.BlackPocket.Count);
            Assert.AreEqual(0, game.WhitePocket.Count);
            Assert.AreEqual('p', game.BlackPocket[0].GetFenCharacter());
        }

        [Test]
        public static void TestFenTilde()
        {
            CrazyhouseChessGame game = new CrazyhouseChessGame("rnbqkb1r/pP3ppp/5n2/4p3/8/8/PPPP1PPP/RNBQKBNR/PPP w KQkq - 8 5");
            game.ApplyMove(new Move("B7", "A8", Player.White, 'Q'), true);
            Assert.AreEqual("Q~nbqkb1r/p4ppp/5n2/4p3/8/8/PPPP1PPP/RNBQKBNR/PPPR b KQk - 0 5", game.GetFen());

            game = new CrazyhouseChessGame("Q~n1qkb1r/pb3ppp/5n2/4p3/8/2N5/PPPP1PPP/R1BQKBNR/RPPP b KQk - 11 6");
            game.ApplyMove(new Move("B7", "A8", Player.Black), true);
            Assert.AreEqual('p', game.BlackPocket[0].GetFenCharacter());
        }
    }
}
