using NUnit.Framework;

namespace ChessDotNet.Variants.Tests
{
    using Pieces;
    using Atomic;

    [TestFixture]
    public class AtomicChessGameTests
    {
        static readonly Piece kw = new King(Player.White);
        static readonly Piece kb = new King(Player.Black);
        static readonly Piece qw = new Queen(Player.White);
        static readonly Piece qb = new Queen(Player.Black);
        static readonly Piece rw = new Rook(Player.White);
        static readonly Piece rb = new Rook(Player.Black);
        static readonly Piece nw = new Knight(Player.White);
        static readonly Piece nb = new Knight(Player.Black);
        static readonly Piece bw = new Bishop(Player.White);
        static readonly Piece bb = new Bishop(Player.Black);
        static readonly Piece pw = new Pawn(Player.White);
        static readonly Piece pb = new Pawn(Player.Black);
        static readonly Piece o = null;

        [Test]
        public void TestExplosions()
        {
            Piece[][] board = new Piece[8][]
            {
                new Piece[8] { o, o, o, o, kb, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, pw, qb, o, o, o },
                new Piece[8] { o, o, o, bw, pb, o, o, o },
                new Piece[8] { o, o, o, o, o, o, nw, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, kw, o, o, o }
            };

            AtomicChessGame game = new AtomicChessGame(board, Player.White);
            Assert.AreEqual(game.ApplyMove(new Move("G3", "E4", Player.White), true), MoveType.Move | MoveType.Capture);

            Piece[][] expected = new Piece[8][]
            {
                new Piece[8] { o, o, o, o, kb, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, pw, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, kw, o, o, o }
            };
            Piece[][] actual = game.GetBoard();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestVariantEnd()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, nb, bb, qb, kb, bb, nb, rb },
                new[] { o, pb, pb, pb, o, pb, pb, pb },
                new[] { pb, o, o, o, pb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, pw, qw, o },
                new[] { pw, pw, pw, pw, o, pw, pw, pw },
                new[] { rw, nw, bw, o, kw, bw, nw, rw }
            };
            AtomicChessGame game = new AtomicChessGame(board, Player.White);
            game.ApplyMove(new Move("F3", "F7", Player.White), true);
            GameStatus status = game.Status;
            Assert.AreEqual(GameEvent.VariantEnd, status.Event);
            Assert.AreEqual("King exploded", status.EventExplanation);
            Assert.AreEqual(Player.White, status.PlayerWhoCausedEvent);
        }

        [Test]
        public void TestCheckmate()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, qw },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            AtomicChessGame game = new AtomicChessGame(board, Player.White);
            game.ApplyMove(new Move("H7", "B7", Player.White), true);
            GameStatus status = game.Status;
            Assert.AreEqual(GameEvent.Checkmate, status.Event);
            Assert.AreEqual(Player.White, status.PlayerWhoCausedEvent);
        }
    }
}
