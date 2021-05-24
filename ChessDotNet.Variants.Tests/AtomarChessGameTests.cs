using NUnit.Framework;

namespace ChessDotNet.Variants.Tests
{
    using Pieces;
    using Atomar;
    using System;
    using ChessDotNet.Variants.Atomar.Pieces;

    [TestFixture]
    public class AtomarChessGameTests
    {
        static readonly Piece kw = new AtomarKing(Player.White);
        static readonly Piece kb = new AtomarKing(Player.Black);
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

            AtomarChessGame game = new AtomarChessGame(board, Player.White);
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
        public void TestKingsNextToEachOther()
        {
            Piece[][] board = new Piece[8][]
            {
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, pb, nb, o, o, o },
                new Piece[8] { o, o, o, pw, pb, o, o, o },
                new Piece[8] { o, o, o, o, o, nw, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, kb, o, kw, o, o, o }
            };

            AtomarChessGame game = new AtomarChessGame(board, Player.White);
            Assert.True(game.IsValidMove(new Move("E1", "D1", Player.White)));

            game.MakeMove(new Move("E1", "D1", Player.White), true);
            Assert.False(game.IsInCheck(Player.Black));
            Assert.False(game.IsInCheck(Player.White));
            Assert.False(game.IsValidMove(new Move("C1", "D1", Player.Black)));
            Assert.True(game.IsValidMove(new Move("C1", "D2", Player.Black)));
            Assert.True(game.IsValidMove(new Move("C1", "C2", Player.Black)));
        }

        [Test]
        public void TestCornerEndgame1()
        {
            Piece[][] board = new Piece[8][]
            {
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { qw, kb, o, o, o, o, o, o },
                new Piece[8] { kw, qw, o, o, o, o, o, o }
            };

            AtomarChessGame game = new AtomarChessGame(board, Player.White);

            Assert.False(game.IsInCheck(Player.Black));
            Assert.False(game.IsInCheck(Player.White));

            Assert.True(game.IsValidMove(new Move("B1", "B2", Player.White)));
            Assert.True(game.IsValidMove(new Move("A2", "B2", Player.White)));
            Assert.False(game.IsValidMove(new Move("A1", "B2", Player.White)));

            game.MakeMove(new Move("B1", "B2", Player.White), true);
            Assert.True(game.IsWinner(Player.White));
            Assert.AreEqual(1, game.PiecesOnBoard.Count);
        }

        [Test]
        public void TestCornerEndgame2()
        {
            Piece[][] board = new Piece[8][]
            {
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { qw, kb, o, o, o, o, o, o },
                new Piece[8] { kw, qw, o, o, o, o, o, o }
            };

            AtomarChessGame game = new AtomarChessGame(board, Player.Black);

            Assert.False(game.IsInCheck(Player.Black));
            Assert.False(game.IsInCheck(Player.White));

            Assert.True(game.IsValidMove(new Move("B2", "B1", Player.Black)));
            Assert.True(game.IsValidMove(new Move("B2", "A2", Player.Black)));
            Assert.False(game.IsValidMove(new Move("B2", "A1", Player.Black)));

            game.MakeMove(new Move("B2", "B1", Player.Black), true);
            Assert.True(game.IsDraw());
            Assert.AreEqual(2, game.PiecesOnBoard.Count);
        }

        [Test]
        public void TestKingsImmuneToExplosions()
        {
            Piece[][] board = new Piece[8][]
{
                new Piece[8] { nb, nw, nb, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, kb, o, o, o, o },
                new Piece[8] { o, o, o, bb, o, o, o, rw },
                new Piece[8] { o, o, o, kw, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o },
                new Piece[8] { o, o, o, o, o, o, o, o }
};

            AtomarChessGame game = new AtomarChessGame(board, Player.White);
            Assert.True(game.IsValidMove(new Move("H4", "D4", Player.White)));
            game.MakeMove(new Move("H4", "D4", Player.White), true);
            Assert.AreEqual("nNn5/8/8/3k4/8/3K4/8/8", game.GetFen().Split(" ")[0]);
        }

        [Test]
        public void TestCastling()
        {
            AtomarChessGame game = new AtomarChessGame("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            game.MakeMove(new Move("G1", "F3", Player.White), true);
            game.MakeMove(new Move("G8", "F6", Player.Black), true);
            game.MakeMove(new Move("G2", "G3", Player.White), true);
            game.MakeMove(new Move("G7", "G6", Player.Black), true);
            game.MakeMove(new Move("F1", "G2", Player.White), true);
            game.MakeMove(new Move("F8", "G7", Player.Black), true);
            Assert.True(game.IsValidMove(new Move("E1", "H1", Player.White)));
            var moves = game.GetValidMoves(Player.White);
            int ok = 0;
            foreach (var move in moves)
            {
                if (move.OriginalPosition.ToString() == "E1" && (move.NewPosition.ToString() == "G1" || move.NewPosition.ToString() == "H1"))
                {
                    ok++;
                }
            }
            Assert.AreEqual(2, ok);
        }

        [Test]
        public void TestCastling2()
        {
            AtomarChessGame game = new AtomarChessGame("3Rk2r/pp3ppp/n4n2/8/8/8/PPP2P1P/2K3NR b k - 0 1");
            Assert.True(game.IsValidMove(new Move("E8", "G8", Player.Black)));
            Assert.True(game.IsValidMove(new Move("E8", "H8", Player.Black)));
        }

        [Test]
        public void TestKingCanTakeOtherRookButNotOwnPawn()
        {
            AtomarChessGame game = new AtomarChessGame("3Rk2r/pp3ppp/n4n2/8/8/8/PPP2P1P/2K3NR b k - 0 1");
            Assert.True(game.IsValidMove(new Move("E8", "D8", Player.Black)));
            Assert.False(game.IsValidMove(new Move("E8", "F7", Player.Black)));
        }
    }
}
