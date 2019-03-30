using NUnit.Framework;

namespace ChessDotNet.Variants.Tests
{
    using Pieces;
    using Atomic;
    using System;

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
            Assert.AreEqual(game.MakeMove(new Move("G3", "E4", Player.White), true), MoveType.Move | MoveType.Capture);

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
            game.MakeMove(new Move("F3", "F7", Player.White), true);
            Assert.True(game.KingIsGone(Player.Black));
            Assert.True(game.IsWinner(Player.White));
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
            game.MakeMove(new Move("H7", "B7", Player.White), true);
            Assert.True(game.IsCheckmated(Player.Black));
            Assert.True(game.IsWinner(Player.White));
        }

        [Test]
        public void TestStalemate()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { kb, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, qw, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, kw }
            };
            AtomicChessGame game = new AtomicChessGame(board, Player.Black);
            Assert.True(game.IsStalemated(Player.Black));
        }

        [Test]
        public void TestStalemateAdjacentKings()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, qw, qw },
                new[] { o, o, o, o, o, o, kb, kw }
            };
            AtomicChessGame game = new AtomicChessGame(board, Player.Black);
            Assert.True(game.IsStalemated(Player.Black));
        }

        [Test]
        public void TestValidMoveAdjacentKings()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { pw, pw, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, kb, o, kw }
            };
            AtomicChessGame game = new AtomicChessGame(board, Player.Black);
            Assert.True(game.IsValidMove(new Move("F1", "G1", Player.Black)));
        }

        [Test]
        public void TestExplosionWhenInCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, kb, o },
                new[] { o, o, o, o, o, o, pb, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, qw, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { rb, o, o, o, kw, o, o, o }
            };
            AtomicChessGame game = new AtomicChessGame(board, Player.White);
            Assert.True(game.IsValidMove(new Move("G4", "G7", Player.White)));
        }

        [Test]
        public void TestNotInCheckWhenAdjacent()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { pw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, qw, o },
                new[] { o, o, o, o, o, o, kb, kw }
            };
            AtomicChessGame game = new AtomicChessGame(board, Player.Black);
            Assert.False(game.IsInCheck(Player.Black));
        }

        [Test]
        public void TestFenCastlingFieldAfterRookExplosion_BlackKingside()
        {
            AtomicChessGame game = new AtomicChessGame("rnbqkbnr/ppp3pp/3pp3/5pN1/8/4P3/PPPP1PPP/RNBQKB1R w KQkq - 0 4");
            game.MakeMove(new Move("g5", "h7", Player.White), true);
            Assert.False(game.CanBlackCastleKingSide);
            Assert.AreEqual("rnbqkb2/ppp3p1/3pp3/5p2/8/4P3/PPPP1PPP/RNBQKB1R b KQq - 0 4", game.GetFen());
        }

        [Test]
        public void TestFenCastlingFieldAfterRookExplosion_BlackQueenside()
        {
            AtomicChessGame game = new AtomicChessGame("rnbqkbnr/pp2pppp/8/1Npp4/8/8/PPPPPPPP/R1BQKBNR w KQkq - 0 3");
            game.MakeMove(new Move("b5", "a7", Player.White), true);
            Assert.False(game.CanBlackCastleQueenSide);
            Assert.AreEqual("2bqkbnr/1p2pppp/8/2pp4/8/8/PPPPPPPP/R1BQKBNR b KQk - 0 3", game.GetFen());
        }

        [Test]
        public void TestFenCastlingFieldAfterRookExplosion_WhiteKingside()
        {
            AtomicChessGame game = new AtomicChessGame("rnbqkb1r/pppppppp/8/8/5Pn1/2P5/PPQPP1PP/RNB1KBNR b KQkq - 2 3");
            game.MakeMove(new Move("g4", "h2", Player.Black), true);
            Assert.False(game.CanWhiteCastleKingSide);
            Assert.AreEqual("rnbqkb1r/pppppppp/8/8/5P2/2P5/PPQPP1P1/RNB1KB2 w Qkq - 0 4", game.GetFen());
        }

        [Test]
        public void TestFenCastlingFieldAfterRookExplosion_WhiteQueenside()
        {
            AtomicChessGame game = new AtomicChessGame("r1bqkbnr/pppppppp/8/8/1nPP4/6P1/PP2PP1P/RNBQKBNR b KQkq - 0 3");
            game.MakeMove(new Move("b4", "a2", Player.Black), true);
            Assert.False(game.CanWhiteCastleQueenSide);
            Assert.AreEqual("r1bqkbnr/pppppppp/8/8/2PP4/6P1/1P2PP1P/2BQKBNR w Kkq - 0 4", game.GetFen());
        }

        [Test]
        public void Test960Castling()
        {
            AtomicChessGame game = new AtomicChessGame("rqknrnbb/pppppppp/8/8/8/8/PPPPPPPP/RQKNRNBB w KQkq - 0 1");
            string[] moves = { "g2g3", "d8c6", "h2h3", "f8e6", "f2f3", "f7f6", "a2a3", "g8f7", "b2b3", "g7g6", "c2c3", "h8g7", "e2e3", "b7b6", "d2d3", "b8b7", "c3c4" };
            foreach (string m in moves)
            {
                game.MakeMove(new Move(m.Substring(0, 2), m.Substring(2, 2), game.WhoseTurn), true);
            }

            Assert.True(game.IsValidMove(new Move("C8", "E8", Player.Black)));
            Assert.True(game.IsValidMove(new Move("C8", "A8", Player.Black)));
        }

        [Test]
        public void Test960Castling2()
        {
            AtomicChessGame game = new AtomicChessGame("bnrbkrnq/pppppppp/8/8/8/8/PPPPPPPP/BNRBKRNQ w KQkq - 0 1");
            string[] moves = { "b2b3", "h7h6", "a1b2", "g7g6", "b1a3", "e7e6", "c2c3", "d7d6", "d1c2", "c7c6", "g1f3", "b7b6", "h2h3", "a7a6", "h1h2", "f7f6" };
            foreach (string m in moves)
            {
                game.MakeMove(new Move(m.Substring(0, 2), m.Substring(2, 2), game.WhoseTurn), true);
            }

            Assert.True(game.IsValidMove(new Move("E1", "C1", Player.White)));
            Assert.True(game.IsValidMove(new Move("E1", "F1", Player.White)));
        }

        [Test]
        public void TestInvalidMove_CapturingOwn()
        {
            AtomicChessGame game = new AtomicChessGame("rqknrnbb/pppppppp/8/8/8/8/PPPPPPPP/RQKNRNBB w KQkq - 0 1");
            string[] moves = { "g2g3", "d8c6", "h2h3", "f8e6", "f2f3", "f7f6", "a2a3", "g8f7", "b2b3", "g7g6", "c2c3", "h8g7", "e2e3", "b7b6", "d2d3", "b8b7", "c3c4" };
            foreach (string m in moves)
            {
                game.MakeMove(new Move(m.Substring(0, 2), m.Substring(2, 2), game.WhoseTurn), true);
            }

            Assert.False(game.IsValidMove(new Move("C8", "C7", Player.Black)));
        }

        [Test]
        public static void TestSanPawnCapturePromotionNoCheckmate()
        {
            AtomicChessGame game = new AtomicChessGame("k1r5/3P4/K7/8/8/8/8/8 w - - 0 1");
            game.MakeMove(new Move("D7", "C8", Player.White, 'R'), true);
            System.Console.WriteLine(game.GetFen());
            Assert.AreEqual("dxc8=R", game.Moves[game.Moves.Count - 1].SAN);
        }

        [Test]
        public static void TestPgnGameEndsInExplosion()
        {
            AtomicChessGame game = new AtomicChessGame();
            game.MakeMove(new Move("E2", "E4", Player.White), true);
            game.MakeMove(new Move("D7", "D5", Player.Black), true);
            game.MakeMove(new Move("E4", "D5", Player.White), true);
            game.MakeMove(new Move("D8", "D2", Player.Black), true);
            Assert.AreEqual("1. e4 d5 2. exd5 Qxd2# 0-1", game.GetPGN());
        }

        [Test]
        public static void TestUndoNYI()
        {
            string fen = "rnbqk1nr/ppp2ppp/8/2bpp3/3PP3/N7/PPP2PPP/R1BQKBNR w KQkq - 2 4";
            AtomicChessGame game = new AtomicChessGame(fen);
            game.MakeMove(new Move("E4", "D5", Player.White), true);
            Assert.Throws<NotImplementedException>(() => game.Undo());
        }

        [Test]
        public static void ValidateThatEmptyCannotMove()
        {
            AtomicChessGame game = new AtomicChessGame();
            var move = new Move("c3", "c4", Player.White);
            var valid = game.IsValidMove(move);
            Assert.IsFalse(valid);
        }
    }
}
