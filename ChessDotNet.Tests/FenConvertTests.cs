using NUnit.Framework;

namespace ChessDotNet.Tests
{
    using Pieces;
    using System;
    [TestFixture]
    public static class FenConvertTests
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
        public static void TestStartPosition()
        {
            ChessGame game = new ChessGame();
            string fen = game.GetFen();
            Assert.AreEqual("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", fen);
        }

        [Test]
        public static void TestAfter1e4()
        {
            ChessGame game = new ChessGame();
            game.MakeMove(new Move("E2", "E4", Player.White), true);
            string fen = game.GetFen();
            Assert.AreEqual("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1", fen);
        }

        [Test]
        public static void TestAfter1c5()
        {
            ChessGame game = new ChessGame();
            game.MakeMove(new Move("E2", "E4", Player.White), true);
            game.MakeMove(new Move("C7", "C5", Player.Black), true);
            string fen = game.GetFen();
            Assert.AreEqual("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2", fen);
        }

        [Test]
        public static void TestAfter2Nf3()
        {
            ChessGame game = new ChessGame();
            game.MakeMove(new Move("E2", "E4", Player.White), true);
            game.MakeMove(new Move("C7", "C5", Player.Black), true);
            game.MakeMove(new Move("G1", "F3", Player.White), true);
            string fen = game.GetFen();
            Assert.AreEqual("rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2", fen);
        }

        [Test]
        public static void TestMovingWhiteKingLosingCastlingRights()
        {
            ChessGame game = new ChessGame();
            game.MakeMove(new Move("E2", "E4", Player.White), true);
            game.MakeMove(new Move("C7", "C5", Player.Black), true);
            game.MakeMove(new Move("E1", "E2", Player.White), true);
            string fen = game.GetFen();
            Assert.AreEqual("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPPKPPP/RNBQ1BNR b kq - 1 2", fen);
        }

        [Test]
        public static void TestMovingBlackKingLosingCastlingRights()
        {
            ChessGame game = new ChessGame();
            game.MakeMove(new Move("E2", "E4", Player.White), true);
            game.MakeMove(new Move("E7", "E5", Player.Black), true);
            game.MakeMove(new Move("G1", "F3", Player.White), true);
            game.MakeMove(new Move("E8", "E7", Player.Black), true);
            string fen = game.GetFen();
            Assert.AreEqual("rnbq1bnr/ppppkppp/8/4p3/4P3/5N2/PPPP1PPP/RNBQKB1R w KQ - 2 3", fen);
        }

        [Test]
        public static void TestMovingWhiteARookLosingCastlingRights()
        {
            ChessGame game = new ChessGame();
            game.MakeMove(new Move("A2", "A3", Player.White), true);
            game.MakeMove(new Move("E7", "E5", Player.Black), true);
            game.MakeMove(new Move("A1", "A2", Player.White), true);
            string fen = game.GetFen();
            Assert.AreEqual("rnbqkbnr/pppp1ppp/8/4p3/8/P7/RPPPPPPP/1NBQKBNR b Kkq - 1 2", fen);
        }

        [Test]
        public static void TestMovingWhiteHRookLosingCastlingRights()
        {
            ChessGame game = new ChessGame();
            game.MakeMove(new Move("H2", "H3", Player.White), true);
            game.MakeMove(new Move("E7", "E5", Player.Black), true);
            game.MakeMove(new Move("H1", "H2", Player.White), true);
            string fen = game.GetFen();
            Assert.AreEqual("rnbqkbnr/pppp1ppp/8/4p3/8/7P/PPPPPPPR/RNBQKBN1 b Qkq - 1 2", fen);
        }

        [Test]
        public static void TestMovingBlackARookLosingCastlingRights()
        {
            ChessGame game = new ChessGame();
            game.MakeMove(new Move("E2", "E4", Player.White), true);
            game.MakeMove(new Move("A7", "A6", Player.Black), true);
            game.MakeMove(new Move("G1", "F3", Player.White), true);
            game.MakeMove(new Move("A8", "A7", Player.Black), true);
            string fen = game.GetFen();
            Assert.AreEqual("1nbqkbnr/rppppppp/p7/8/4P3/5N2/PPPP1PPP/RNBQKB1R w KQk - 2 3", fen);
        }

        [Test]
        public static void TestMovingBlackHRookLosingCastlingRights()
        {
            ChessGame game = new ChessGame();
            game.MakeMove(new Move("E2", "E4", Player.White), true);
            game.MakeMove(new Move("H7", "H6", Player.Black), true);
            game.MakeMove(new Move("G1", "F3", Player.White), true);
            game.MakeMove(new Move("H8", "H7", Player.Black), true);
            string fen = game.GetFen();
            Assert.AreEqual("rnbqkbn1/pppppppr/7p/8/4P3/5N2/PPPP1PPP/RNBQKB1R w KQq - 2 3", fen);
        }

        [Test]
        public static void TestHalfmoveClockAndFullmoveNumber()
        {
            ChessGame game = new ChessGame();
            game.MakeMove(new Move("E2", "E4", Player.White), true);
            game.MakeMove(new Move("E7", "E5", Player.Black), true);
            game.MakeMove(new Move("E1", "E2", Player.White), true);
            game.MakeMove(new Move("E8", "E7", Player.Black), true);
            game.MakeMove(new Move("E2", "D3", Player.White), true);
            game.MakeMove(new Move("E7", "D6", Player.Black), true);
            game.MakeMove(new Move("D3", "C3", Player.White), true);
            game.MakeMove(new Move("D6", "C6", Player.Black), true);
            game.MakeMove(new Move("C3", "B3", Player.White), true);
            game.MakeMove(new Move("C6", "B6", Player.Black), true);
            game.MakeMove(new Move("B3", "A4", Player.White), true);
            game.MakeMove(new Move("B6", "C5", Player.Black), true);
            game.MakeMove(new Move("F1", "C4", Player.White), true);
            string fen = game.GetFen();
            Assert.AreEqual("rnbq1bnr/pppp1ppp/8/2k1p3/K1B1P3/8/PPPP1PPP/RNBQ2NR b - - 11 7", fen);
            game.MakeMove(new Move("C5", "C4", Player.Black), true);
            fen = game.GetFen();
            Assert.AreEqual("rnbq1bnr/pppp1ppp/8/4p3/K1k1P3/8/PPPP1PPP/RNBQ2NR w - - 0 8", fen);
            game.MakeMove(new Move("A4", "A5", Player.White), true);
            game.MakeMove(new Move("H7", "H5", Player.Black), true);
            fen = game.GetFen();
            Assert.AreEqual("rnbq1bnr/pppp1pp1/8/K3p2p/2k1P3/8/PPPP1PPP/RNBQ2NR w - h6 0 9", fen);
        }

        [Test]
        public static void TestChessGameFenConstructorStartPosition()
        {
            ChessGame game = new ChessGame("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            Piece[][] expected = new Piece[8][]
            {
                new[] { rb, nb, bb, qb, kb, bb, nb, rb },
                new[] { pb, pb, pb, pb, pb, pb, pb, pb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { pw, pw, pw, pw, pw, pw, pw, pw },
                new[] { rw, nw, bw, qw, kw, bw, nw, rw }
            };
            Assert.AreEqual(expected, game.GetBoard());
            Assert.AreEqual(0, game.HalfMoveClock);
            Assert.AreEqual(1, game.FullMoveNumber);
            Assert.AreEqual(Player.White, game.WhoseTurn);
            Assert.True(game.CanBlackCastleKingSide);
            Assert.True(game.CanBlackCastleQueenSide);
            Assert.True(game.CanWhiteCastleKingSide);
            Assert.True(game.CanWhiteCastleQueenSide);
        }

        [Test]
        public static void TestChessGameFenConstructorAfter1e4()
        {
            ChessGame game = new ChessGame("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1");
            Piece[][] expected = new Piece[8][]
            {
                new[] { rb, nb, bb, qb, kb, bb, nb, rb },
                new[] { pb, pb, pb, pb, pb, pb, pb, pb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, pw, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { pw, pw, pw, pw, o, pw, pw, pw },
                new[] { rw, nw, bw, qw, kw, bw, nw, rw }
            };
            Assert.AreEqual(expected, game.GetBoard());
            Assert.AreEqual(0, game.HalfMoveClock);
            Assert.AreEqual(1, game.FullMoveNumber);
            Assert.AreEqual(new Position("E2"), game.Moves[game.Moves.Count - 1].OriginalPosition);
            Assert.AreEqual(new Position("E4"), game.Moves[game.Moves.Count - 1].NewPosition);
            Assert.AreEqual(Player.White, game.Moves[game.Moves.Count - 1].Player);
            Assert.AreEqual(Player.Black, game.WhoseTurn);
        }

        [Test]
        public static void TestChessGameFenConstructorAfter1e3()
        {
            ChessGame game = new ChessGame("rnbqkbnr/pppppppp/8/8/8/4P3/PPPP1PPP/RNBQKBNR w KQkq - 0 1");
            Piece[][] expected = new Piece[8][]
            {
                new[] { rb, nb, bb, qb, kb, bb, nb, rb },
                new[] { pb, pb, pb, pb, pb, pb, pb, pb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, pw, o, o, o },
                new[] { pw, pw, pw, pw, o, pw, pw, pw },
                new[] { rw, nw, bw, qw, kw, bw, nw, rw }
            };
            Assert.AreEqual(expected, game.GetBoard());
            Assert.AreEqual(0, game.HalfMoveClock);
            Assert.AreEqual(1, game.FullMoveNumber);
            Assert.AreEqual(0, game.Moves.Count);
        }

        [Test]
        public static void TestChessGameFenConstructorPartialCastlingRights()
        {
            ChessGame game = new ChessGame("rnbqkbn1/pppppppr/7p/8/4P3/5N2/PPPP1PPP/RNBQKB1R w KQq - 2 3");
            Piece[][] expected = new Piece[8][]
            {
                new[] { rb, nb, bb, qb, kb, bb, nb, o },
                new[] { pb, pb, pb, pb, pb, pb, pb, rb },
                new[] { o, o, o, o, o, o, o, pb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, pw, o, o, o },
                new[] { o, o, o, o, o, nw, o, o },
                new[] { pw, pw, pw, pw, o, pw, pw, pw },
                new[] { rw, nw, bw, qw, kw, bw, o, rw }
             };
            Assert.AreEqual(expected, game.GetBoard());
            Assert.AreEqual(2, game.HalfMoveClock);
            Assert.AreEqual(3, game.FullMoveNumber);
            Assert.False(game.CanBlackCastleKingSide);
        }

        [Test]
        public static void TestChessGameFenConstructorNoCastlingRights()
        {
            ChessGame game = new ChessGame("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 16 9");
            Piece[][] expected = new Piece[8][]
            {
                new[] { rb, nb, bb, qb, kb, bb, nb, rb },
                new[] { pb, pb, pb, pb, pb, pb, pb, pb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { pw, pw, pw, pw, pw, pw, pw, pw },
                new[] { rw, nw, bw, qw, kw, bw, nw, rw }
            };
            Assert.AreEqual(expected, game.GetBoard());
            Assert.AreEqual(16, game.HalfMoveClock);
            Assert.AreEqual(9, game.FullMoveNumber);
            Assert.AreEqual(Player.White, game.WhoseTurn);
            Assert.False(game.CanBlackCastleKingSide);
            Assert.False(game.CanBlackCastleQueenSide);
            Assert.False(game.CanWhiteCastleKingSide);
            Assert.False(game.CanWhiteCastleQueenSide);
        }

        [Test]
        public static void TestChessGameFenConstructorAfter1e4c5()
        {
            ChessGame game = new ChessGame("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2");
            Piece[][] expected = new Piece[8][]
            {
                new[] { rb, nb, bb, qb, kb, bb, nb, rb },
                new[] { pb, pb, o, pb, pb, pb, pb, pb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, pb, o, o, o, o, o },
                new[] { o, o, o, o, pw, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { pw, pw, pw, pw, o, pw, pw, pw },
                new[] { rw, nw, bw, qw, kw, bw, nw, rw }
            };
            Assert.AreEqual(expected, game.GetBoard());
            Assert.AreEqual(0, game.HalfMoveClock);
            Assert.AreEqual(2, game.FullMoveNumber);
            Assert.AreEqual(Player.White, game.WhoseTurn);
            Assert.AreEqual(new Position("C7"), game.Moves[game.Moves.Count - 1].OriginalPosition);
            Assert.AreEqual(new Position("C5"), game.Moves[game.Moves.Count - 1].NewPosition);
            Assert.AreEqual(Player.Black, game.Moves[game.Moves.Count - 1].Player);
        }

        [Test]
        public static void TestChessGameFenConstructorInvalid()
        {
            Assert.Throws<ArgumentException>(() => { ChessGame game = new ChessGame("rnbqkbnr/pppppppp/8/8/8/4P3/PPPP1PPP/RNBQKBNR b KQkq e3 0 1"); });
            Assert.Throws<ArgumentException>(() => { ChessGame game = new ChessGame("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e6 0 1"); });
            Assert.Throws<ArgumentException>(() => { ChessGame game = new ChessGame("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq"); });
            Assert.Throws<ArgumentException>(() => { ChessGame game = new ChessGame("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c3 0 2"); });
            Assert.Throws<ArgumentException>(() => { ChessGame game = new ChessGame("rnbqkbnr/pp1ppppp/2p5/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2"); });
            Assert.Throws<ArgumentException>(() => { ChessGame game = new ChessGame("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP w KQkq - 0 1"); });
            Assert.Throws<ArgumentException>(() => { ChessGame game = new ChessGame("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNZ w KQkq - 0 1"); });
            Assert.Throws<ArgumentException>(() => { ChessGame game = new ChessGame("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBN w KQkq - 0 1"); });
        }
    }
}
