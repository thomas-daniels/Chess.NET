using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChessDotNet.Tests
{
    using Pieces;

    [TestFixture]
    public static class ChessGameTests
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
        public static void TestArrayGetting()
        {
            ChessGame cb = new ChessGame();
            int a = (int)File.A;
            int b = (int)File.B;
            int c = (int)File.C;
            int d = (int)File.D;
            int e = (int)File.E;
            int f = (int)File.F;
            int g = (int)File.G;
            int h = (int)File.H;
            Piece[][] board = cb.GetBoard();
            Assert.AreEqual(rw, board[7][a]);
            Assert.AreEqual(nw, board[7][b]);
            Assert.AreEqual(bw, board[7][c]);
            Assert.AreEqual(qw, board[7][d]);
            Assert.AreEqual(kw, board[7][e]);
            Assert.AreEqual(bw, board[7][f]);
            Assert.AreEqual(nw, board[7][g]);
            Assert.AreEqual(rw, board[7][h]);
            Assert.AreEqual(pw, board[6][a]);
            Assert.AreEqual(pw, board[6][b]);
            Assert.AreEqual(pw, board[6][c]);
            Assert.AreEqual(pw, board[6][d]);
            Assert.AreEqual(pw, board[6][e]);
            Assert.AreEqual(pw, board[6][f]);
            Assert.AreEqual(pw, board[6][g]);
            Assert.AreEqual(pw, board[6][h]);
            Assert.AreEqual(o, board[5][a]);
            Assert.AreEqual(o, board[5][b]);
            Assert.AreEqual(o, board[5][c]);
            Assert.AreEqual(o, board[5][d]);
            Assert.AreEqual(o, board[5][e]);
            Assert.AreEqual(o, board[5][f]);
            Assert.AreEqual(o, board[5][g]);
            Assert.AreEqual(o, board[5][h]);
            Assert.AreEqual(o, board[4][a]);
            Assert.AreEqual(o, board[4][b]);
            Assert.AreEqual(o, board[4][c]);
            Assert.AreEqual(o, board[4][d]);
            Assert.AreEqual(o, board[4][e]);
            Assert.AreEqual(o, board[4][f]);
            Assert.AreEqual(o, board[4][g]);
            Assert.AreEqual(o, board[4][h]);
            Assert.AreEqual(o, board[3][a]);
            Assert.AreEqual(o, board[3][b]);
            Assert.AreEqual(o, board[3][c]);
            Assert.AreEqual(o, board[3][d]);
            Assert.AreEqual(o, board[3][e]);
            Assert.AreEqual(o, board[3][f]);
            Assert.AreEqual(o, board[3][g]);
            Assert.AreEqual(o, board[3][h]);
            Assert.AreEqual(o, board[2][a]);
            Assert.AreEqual(o, board[2][b]);
            Assert.AreEqual(o, board[2][c]);
            Assert.AreEqual(o, board[2][d]);
            Assert.AreEqual(o, board[2][e]);
            Assert.AreEqual(o, board[2][f]);
            Assert.AreEqual(o, board[2][g]);
            Assert.AreEqual(o, board[2][h]);
            Assert.AreEqual(pb, board[1][a]);
            Assert.AreEqual(pb, board[1][b]);
            Assert.AreEqual(pb, board[1][c]);
            Assert.AreEqual(pb, board[1][d]);
            Assert.AreEqual(pb, board[1][e]);
            Assert.AreEqual(pb, board[1][f]);
            Assert.AreEqual(pb, board[1][g]);
            Assert.AreEqual(pb, board[1][h]);
            Assert.AreEqual(rb, board[0][a]);
            Assert.AreEqual(nb, board[0][b]);
            Assert.AreEqual(bb, board[0][c]);
            Assert.AreEqual(qb, board[0][d]);
            Assert.AreEqual(kb, board[0][e]);
            Assert.AreEqual(bb, board[0][f]);
            Assert.AreEqual(nb, board[0][g]);
            Assert.AreEqual(rb, board[0][h]);
        }

        [Test]
        public static void TestGetPieceAt()
        {
            ChessGame cb = new ChessGame();
            File a = File.A;
            File b = File.B;
            File c = File.C;
            File d = File.D;
            File e = File.E;
            File f = File.F;
            File g = File.G;
            File h = File.H;
            Assert.AreEqual(rw, cb.GetPieceAt(a, 1));
            Assert.AreEqual(nw, cb.GetPieceAt(b, 1));
            Assert.AreEqual(bw, cb.GetPieceAt(c, 1));
            Assert.AreEqual(qw, cb.GetPieceAt(d, 1));
            Assert.AreEqual(kw, cb.GetPieceAt(e, 1));
            Assert.AreEqual(bw, cb.GetPieceAt(f, 1));
            Assert.AreEqual(nw, cb.GetPieceAt(g, 1));
            Assert.AreEqual(rw, cb.GetPieceAt(h, 1));
            Assert.AreEqual(pw, cb.GetPieceAt(a, 2));
            Assert.AreEqual(pw, cb.GetPieceAt(b, 2));
            Assert.AreEqual(pw, cb.GetPieceAt(c, 2));
            Assert.AreEqual(pw, cb.GetPieceAt(d, 2));
            Assert.AreEqual(pw, cb.GetPieceAt(e, 2));
            Assert.AreEqual(pw, cb.GetPieceAt(f, 2));
            Assert.AreEqual(pw, cb.GetPieceAt(g, 2));
            Assert.AreEqual(pw, cb.GetPieceAt(h, 2));
            Assert.AreEqual(o, cb.GetPieceAt(a, 3));
            Assert.AreEqual(o, cb.GetPieceAt(b, 3));
            Assert.AreEqual(o, cb.GetPieceAt(c, 3));
            Assert.AreEqual(o, cb.GetPieceAt(d, 3));
            Assert.AreEqual(o, cb.GetPieceAt(e, 3));
            Assert.AreEqual(o, cb.GetPieceAt(f, 3));
            Assert.AreEqual(o, cb.GetPieceAt(g, 3));
            Assert.AreEqual(o, cb.GetPieceAt(h, 3));
            Assert.AreEqual(o, cb.GetPieceAt(a, 4));
            Assert.AreEqual(o, cb.GetPieceAt(b, 4));
            Assert.AreEqual(o, cb.GetPieceAt(c, 4));
            Assert.AreEqual(o, cb.GetPieceAt(d, 4));
            Assert.AreEqual(o, cb.GetPieceAt(e, 4));
            Assert.AreEqual(o, cb.GetPieceAt(f, 4));
            Assert.AreEqual(o, cb.GetPieceAt(g, 4));
            Assert.AreEqual(o, cb.GetPieceAt(h, 4));
            Assert.AreEqual(o, cb.GetPieceAt(a, 5));
            Assert.AreEqual(o, cb.GetPieceAt(b, 5));
            Assert.AreEqual(o, cb.GetPieceAt(c, 5));
            Assert.AreEqual(o, cb.GetPieceAt(d, 5));
            Assert.AreEqual(o, cb.GetPieceAt(e, 5));
            Assert.AreEqual(o, cb.GetPieceAt(f, 5));
            Assert.AreEqual(o, cb.GetPieceAt(g, 5));
            Assert.AreEqual(o, cb.GetPieceAt(h, 5));
            Assert.AreEqual(o, cb.GetPieceAt(a, 6));
            Assert.AreEqual(o, cb.GetPieceAt(b, 6));
            Assert.AreEqual(o, cb.GetPieceAt(c, 6));
            Assert.AreEqual(o, cb.GetPieceAt(d, 6));
            Assert.AreEqual(o, cb.GetPieceAt(e, 6));
            Assert.AreEqual(o, cb.GetPieceAt(f, 6));
            Assert.AreEqual(o, cb.GetPieceAt(g, 6));
            Assert.AreEqual(o, cb.GetPieceAt(h, 6));
            Assert.AreEqual(pb, cb.GetPieceAt(a, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(b, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(c, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(d, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(e, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(f, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(g, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(h, 7));
            Assert.AreEqual(rb, cb.GetPieceAt(a, 8));
            Assert.AreEqual(nb, cb.GetPieceAt(b, 8));
            Assert.AreEqual(bb, cb.GetPieceAt(c, 8));
            Assert.AreEqual(qb, cb.GetPieceAt(d, 8));
            Assert.AreEqual(kb, cb.GetPieceAt(e, 8));
            Assert.AreEqual(bb, cb.GetPieceAt(f, 8));
            Assert.AreEqual(nb, cb.GetPieceAt(g, 8));
            Assert.AreEqual(rb, cb.GetPieceAt(h, 8));
        }

        [Test]
        public static void TestCustomInitialize()
        {
            File a = File.A;
            File b = File.B;
            File c = File.C;
            File d = File.D;
            File e = File.E;
            File f = File.F;
            File g = File.G;
            File h = File.H;
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, o, bb, qb, kb, bb, nb, rb },
                new[] { pb, pb, pb, pb, pb, pb, pb, pb },
                new[] { o, o, nb, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, pw, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { pw, pw, pw, pw, o, pw, pw, pw },
                new[] { rw, nw, bw, qw, kw, bw, nw, rw }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Assert.AreEqual(rw, cb.GetPieceAt(a, 1));
            Assert.AreEqual(nw, cb.GetPieceAt(b, 1));
            Assert.AreEqual(bw, cb.GetPieceAt(c, 1));
            Assert.AreEqual(qw, cb.GetPieceAt(d, 1));
            Assert.AreEqual(kw, cb.GetPieceAt(e, 1));
            Assert.AreEqual(bw, cb.GetPieceAt(f, 1));
            Assert.AreEqual(nw, cb.GetPieceAt(g, 1));
            Assert.AreEqual(rw, cb.GetPieceAt(h, 1));
            Assert.AreEqual(pw, cb.GetPieceAt(a, 2));
            Assert.AreEqual(pw, cb.GetPieceAt(b, 2));
            Assert.AreEqual(pw, cb.GetPieceAt(c, 2));
            Assert.AreEqual(pw, cb.GetPieceAt(d, 2));
            Assert.AreEqual(o, cb.GetPieceAt(e, 2));
            Assert.AreEqual(pw, cb.GetPieceAt(f, 2));
            Assert.AreEqual(pw, cb.GetPieceAt(g, 2));
            Assert.AreEqual(pw, cb.GetPieceAt(h, 2));
            Assert.AreEqual(o, cb.GetPieceAt(a, 3));
            Assert.AreEqual(o, cb.GetPieceAt(b, 3));
            Assert.AreEqual(o, cb.GetPieceAt(c, 3));
            Assert.AreEqual(o, cb.GetPieceAt(d, 3));
            Assert.AreEqual(o, cb.GetPieceAt(e, 3));
            Assert.AreEqual(o, cb.GetPieceAt(f, 3));
            Assert.AreEqual(o, cb.GetPieceAt(g, 3));
            Assert.AreEqual(o, cb.GetPieceAt(h, 3));
            Assert.AreEqual(o, cb.GetPieceAt(a, 4));
            Assert.AreEqual(o, cb.GetPieceAt(b, 4));
            Assert.AreEqual(o, cb.GetPieceAt(c, 4));
            Assert.AreEqual(o, cb.GetPieceAt(d, 4));
            Assert.AreEqual(pw, cb.GetPieceAt(e, 4));
            Assert.AreEqual(o, cb.GetPieceAt(f, 4));
            Assert.AreEqual(o, cb.GetPieceAt(g, 4));
            Assert.AreEqual(o, cb.GetPieceAt(h, 4));
            Assert.AreEqual(o, cb.GetPieceAt(a, 5));
            Assert.AreEqual(o, cb.GetPieceAt(b, 5));
            Assert.AreEqual(o, cb.GetPieceAt(c, 5));
            Assert.AreEqual(o, cb.GetPieceAt(d, 5));
            Assert.AreEqual(o, cb.GetPieceAt(e, 5));
            Assert.AreEqual(o, cb.GetPieceAt(f, 5));
            Assert.AreEqual(o, cb.GetPieceAt(g, 5));
            Assert.AreEqual(o, cb.GetPieceAt(h, 5));
            Assert.AreEqual(o, cb.GetPieceAt(a, 6));
            Assert.AreEqual(o, cb.GetPieceAt(b, 6));
            Assert.AreEqual(cb.GetPieceAt(c, 6), nb);
            Assert.AreEqual(o, cb.GetPieceAt(d, 6));
            Assert.AreEqual(o, cb.GetPieceAt(e, 6));
            Assert.AreEqual(o, cb.GetPieceAt(f, 6));
            Assert.AreEqual(o, cb.GetPieceAt(g, 6));
            Assert.AreEqual(o, cb.GetPieceAt(h, 6));
            Assert.AreEqual(pb, cb.GetPieceAt(a, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(b, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(c, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(d, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(e, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(f, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(g, 7));
            Assert.AreEqual(pb, cb.GetPieceAt(h, 7));
            Assert.AreEqual(rb, cb.GetPieceAt(a, 8));
            Assert.AreEqual(o, cb.GetPieceAt(b, 8));
            Assert.AreEqual(bb, cb.GetPieceAt(c, 8));
            Assert.AreEqual(qb, cb.GetPieceAt(d, 8));
            Assert.AreEqual(kb, cb.GetPieceAt(e, 8));
            Assert.AreEqual(bb, cb.GetPieceAt(f, 8));
            Assert.AreEqual(nb, cb.GetPieceAt(g, 8));
            Assert.AreEqual(rb, cb.GetPieceAt(h, 8));
        }

        [Test]
        public static void TestValidMoveWhitePawn()
        {
            ChessGame cb = new ChessGame();

            Move move1 = new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");
        }

        [Test]
        public static void TestValidMoveWhitePawn_2Steps()
        {
            ChessGame cb = new ChessGame();

            Move move1 = new Move(new Position(File.D, 2), new Position(File.D, 4), Player.White);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");
        }

        [Test]
        public static void TestValidMoveWhitePawn_Capture()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.E, 2), new Position(File.E, 4), Player.White);
            Move move2 = new Move(new Position(File.D, 7), new Position(File.D, 5), Player.Black);
            Move move3 = new Move(new Position(File.E, 4), new Position(File.D, 5), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.True(cb.IsValidMove(move3));
        }

        [Test]
        public static void TestValidMoveWhitePawn_EnPassant()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.E, 2), new Position(File.E, 4), Player.White);
            Move move2 = new Move(new Position(File.E, 7), new Position(File.E, 6), Player.Black);
            Move move3 = new Move(new Position(File.E, 4), new Position(File.E, 5), Player.White);
            Move move4 = new Move(new Position(File.D, 7), new Position(File.D, 5), Player.Black);
            Move move5 = new Move(new Position(File.E, 5), new Position(File.D, 6), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);
            cb.MakeMove(move4, true);

            Assert.True(cb.IsValidMove(move5));
        }

        [Test]
        public static void TestInvalidMoveWhitePawn_EnPassant()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.E, 2), new Position(File.E, 4), Player.White);
            Move move2 = new Move(new Position(File.E, 7), new Position(File.E, 6), Player.Black);
            Move move3 = new Move(new Position(File.E, 4), new Position(File.E, 5), Player.White);
            Move move4 = new Move(new Position(File.H, 7), new Position(File.H, 5), Player.Black);
            Move move5 = new Move(new Position(File.E, 5), new Position(File.D, 6), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);
            cb.MakeMove(move4, true);

            Assert.False(cb.IsValidMove(move5));
        }

        [Test]
        public static void TestInvalidMoveWhitePawn_EnPassant_NoPawn()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, rb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, pw, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame game = new ChessGame(board, Player.Black);

            Move move1 = new Move(new Position(File.E, 7), new Position(File.E, 5), Player.Black);
            Move move2 = new Move(new Position(File.F, 5), new Position(File.E, 6), Player.White);
            game.MakeMove(move1, true);

            Assert.False(game.IsValidMove(move2));
        }

        [Test]
        public static void TestInvalidMoveWhitePawn_NoCapture()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.E, 2), new Position(File.E, 4), Player.White);
            Move move2 = new Move(new Position(File.D, 7), new Position(File.D, 6), Player.Black);
            Move move3 = new Move(new Position(File.E, 4), new Position(File.D, 5), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.False(cb.IsValidMove(move3));
        }

        [Test]
        public static void TestInvalidMoveWhitePawn_2StepsBlockingPiece()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { kw, o, kb, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, pb, o, o, o },
                new[] { o, o, o, o, pw, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move = new Move(new Position(File.E, 2), new Position(File.E, 4), Player.White);

            Assert.False(cb.IsValidMove(move), "move should be invalid");

        }

        [Test]
        public static void TestInvalidMoveWhitePawn_2StepsNotOnRank2()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.E, 2), new Position(File.E, 3), Player.White);
            Move move2 = new Move(new Position(File.E, 3), new Position(File.E, 5), Player.White);

            cb.MakeMove(move1, true);

            Assert.False(cb.IsValidMove(move2), "move2 should be invalid");
        }

        [Test]
        public static void TestValidMoveWhiteKnight()
        {
            ChessGame cb = new ChessGame();

            Move move1 = new Move(new Position(File.B, 1), new Position(File.C, 3), Player.White);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");
        }

        [Test]
        public static void TestValidMoveWhiteBishopC()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.D, 2), new Position(File.D, 3), Player.White);
            Move move2 = new Move(new Position(File.A, 7), new Position(File.A, 6), Player.Black);
            Move move3 = new Move(new Position(File.C, 1), new Position(File.F, 4), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.True(cb.IsValidMove(move3), "move3 should be valid");
        }

        [Test]
        public static void TestValidMoveWhiteBishopF()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.E, 2), new Position(File.E, 3), Player.White);
            Move move2 = new Move(new Position(File.A, 7), new Position(File.A, 6), Player.Black);
            Move move3 = new Move(new Position(File.F, 1), new Position(File.C, 4), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.True(cb.IsValidMove(move3), "move3 should be valid");
        }

        [Test]
        public static void TestValidMoveWhiteRookA()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.A, 7), new Position(File.A, 6), Player.Black);
            Move move3 = new Move(new Position(File.A, 1), new Position(File.A, 2), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.True(cb.IsValidMove(move3), "move3 should be valid");
        }

        [Test]
        public static void TestValidMoveWhiteRookH()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.H, 2), new Position(File.H, 3), Player.White);
            Move move2 = new Move(new Position(File.A, 7), new Position(File.A, 6), Player.Black);
            Move move3 = new Move(new Position(File.H, 1), new Position(File.H, 2), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.True(cb.IsValidMove(move3), "move3 should be valid");
        }

        [Test]
        public static void TestValidMoveWhiteQueenDiagonal()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.E, 2), new Position(File.E, 3), Player.White);
            Move move2 = new Move(new Position(File.A, 7), new Position(File.A, 6), Player.Black);
            Move move3 = new Move(new Position(File.D, 1), new Position(File.H, 5), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.True(cb.IsValidMove(move3), "move3 should be valid");
        }

        [Test]
        public static void TestValidMoveWhiteQueenVertical()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.D, 2), new Position(File.D, 3), Player.White);
            Move move2 = new Move(new Position(File.A, 7), new Position(File.A, 6), Player.Black);
            Move move3 = new Move(new Position(File.D, 1), new Position(File.D, 2), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.True(cb.IsValidMove(move3), "move3 should be valid");
        }

        [Test]
        public static void TestValidMoveWhiteQueenHorizontal()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.B, 1), new Position(File.C, 3), Player.White);
            Move move2 = new Move(new Position(File.A, 7), new Position(File.A, 6), Player.Black);
            Move move3 = new Move(new Position(File.D, 2), new Position(File.D, 3), Player.White);
            Move move4 = new Move(new Position(File.B, 7), new Position(File.B, 6), Player.Black);
            Move move5 = new Move(new Position(File.C, 1), new Position(File.D, 2), Player.White);
            Move move6 = new Move(new Position(File.C, 7), new Position(File.C, 6), Player.Black);
            Move move7 = new Move(new Position(File.D, 1), new Position(File.C, 1), Player.White);
            Move move8 = new Move(new Position(File.D, 1), new Position(File.B, 1), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);
            cb.MakeMove(move4, true);
            cb.MakeMove(move5, true);
            cb.MakeMove(move6, true);

            Assert.True(cb.IsValidMove(move7), "move7 should be valid");
            Assert.True(cb.IsValidMove(move8), "move8 should be valid");
        }

        [Test]
        public static void TestValidMoveWhiteKingDiagonal()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.F, 2), new Position(File.F, 3), Player.White);
            Move move2 = new Move(new Position(File.A, 7), new Position(File.A, 6), Player.Black);
            Move move3 = new Move(new Position(File.E, 1), new Position(File.F, 2), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.True(cb.IsValidMove(move3), "move3 should be valid");
        }

        [Test]
        public static void TestValidMoveWhiteKingHorizontal()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.D, 2), new Position(File.D, 3), Player.White);
            Move move2 = new Move(new Position(File.A, 7), new Position(File.A, 6), Player.Black);
            Move move3 = new Move(new Position(File.D, 1), new Position(File.D, 2), Player.White);
            Move move4 = new Move(new Position(File.H, 7), new Position(File.H, 6), Player.Black);
            Move move5 = new Move(new Position(File.E, 1), new Position(File.D, 1), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);
            cb.MakeMove(move4, true);

            Assert.True(cb.IsValidMove(move5), "move5 should be valid");
        }

        [Test]
        public static void TestValidMoveWhiteKingVertical()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.E, 2), new Position(File.E, 3), Player.White);
            Move move2 = new Move(new Position(File.A, 7), new Position(File.A, 6), Player.Black);
            Move move3 = new Move(new Position(File.E, 1), new Position(File.E, 2), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.True(cb.IsValidMove(move3), "move3 should be valid");
        }

        [Test]
        public static void TestValidMoveWhiteKing_KingsideCastling()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, rw }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.G, 1), Player.White);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");

            Assert.Contains(new Move("E1", "G1", Player.White), cb.GetValidMoves(Player.White));
            Assert.Contains(new Move("E1", "G1", Player.White), cb.GetValidMoves(new Position("E1")));
        }

        [Test]
        public static void TestValidMoveWhiteKing_KingsideCastling2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, rw }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.H, 1), Player.White);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");

            Assert.Contains(new Move("E1", "H1", Player.White), cb.GetValidMoves(Player.White));
            Assert.Contains(new Move("E1", "H1", Player.White), cb.GetValidMoves(new Position("E1")));
        }

        [Test]
        public static void TestValidMoveWhiteKing_QueensideCastling()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, kb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { rw, o, o, o, kw, o, o, rw }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.C, 1), Player.White);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");

            Assert.Contains(new Move("E1", "C1", Player.White), cb.GetValidMoves(Player.White));
            Assert.Contains(new Move("E1", "C1", Player.White), cb.GetValidMoves(new Position("E1")));
        }


        [Test]
        public static void TestValidMoveWhiteKing_QueensideCastling2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, kb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { rw, o, o, o, kw, o, o, rw }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.A, 1), Player.White);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");

            Assert.Contains(new Move("E1", "A1", Player.White), cb.GetValidMoves(Player.White));
            Assert.Contains(new Move("E1", "A1", Player.White), cb.GetValidMoves(new Position("E1")));
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_KingsideCastling_WouldPassThroughCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, rb, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, rw }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.G, 1), Player.White);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_KingsideCastling_WouldPassThroughCheck2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, rb, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, rw }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.H, 1), Player.White);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_QueensideCastling_WouldPassThroughCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, rb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { rw, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.C, 1), Player.White);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestValidMoveWhiteKing_KingsideCastling_WouldNotPassThroughCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, rb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { rw, o, o, o, kw, o, o, rw }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.G, 1), Player.White);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");
        }

        [Test]
        public static void TestValidMoveWhiteKing_QueensideCastling_WouldNotPassThroughCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, rb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { rw, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.C, 1), Player.White);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_KingsideCastling_BlockingPiece1()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, rw, rw }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.G, 1), Player.White);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_QueensideCastling_BlockingPiece1()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, kb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { rw, rw, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.C, 1), Player.White);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_KingsideCastling_BlockingPiece2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, rw, o, rw }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.G, 1), Player.White);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_QueensideCastling_BlockingPiece2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { rw, o, rw, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.C, 1), Player.White);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_QueensideCastling_BlockingPiece3()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { rw, o, o, rw, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.C, 1), Player.White);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_KingsideCastling_NoRook()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.G, 1), Player.White);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_QueensideCastling_NoRook()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.C, 1), Player.White);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_KingsideCastling_RookMoved()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, rw }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.H, 1), new Position(File.H, 2), Player.White);
            Move move2 = new Move(new Position(File.B, 8), new Position(File.B, 7), Player.Black);
            Move move3 = new Move(new Position(File.E, 1), new Position(File.G, 1), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.False(cb.IsValidMove(move3), "move3 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_QueensideCastling_RookMoved()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { rw, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.A, 1), new Position(File.A, 2), Player.White);
            Move move2 = new Move(new Position(File.B, 8), new Position(File.B, 7), Player.Black);
            Move move3 = new Move(new Position(File.E, 1), new Position(File.C, 1), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.False(cb.IsValidMove(move3), "move3 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_KingsideCastling_KingMoved()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, rw }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.D, 1), Player.White);
            Move move2 = new Move(new Position(File.B, 8), new Position(File.B, 7), Player.Black);
            Move move3 = new Move(new Position(File.D, 1), new Position(File.F, 1), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.False(cb.IsValidMove(move3), "move3 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_QueensideCastling_KingMoved()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { rw, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move1 = new Move(new Position(File.E, 1), new Position(File.D, 1), Player.White);
            Move move2 = new Move(new Position(File.B, 8), new Position(File.B, 7), Player.Black);
            Move move3 = new Move(new Position(File.D, 1), new Position(File.B, 1), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.False(cb.IsValidMove(move3), "move3 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhiteKing_KingsideCastling_Checkmated()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, rb, rb, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, pw, o, o, o, o },
                new[] { o, o, o, rw, kw, o, o, rw }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move castling = new Move(new Position(File.E, 1), new Position(File.G, 1), Player.White);
            Assert.False(cb.IsValidMove(castling), "castling move should be invalid; king is checkmated");
        }

        [Test]
        public static void TestValidMoveBlackPawn()
        {
            ChessGame cb = new ChessGame();
            cb.MakeMove(new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White), true);

            Move move1 = new Move(new Position(File.A, 7), new Position(File.A, 6), Player.Black);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");
        }

        [Test]
        public static void TestValidMoveBlackPawn_2Steps()
        {
            ChessGame cb = new ChessGame();
            cb.MakeMove(new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White), true);

            Move move1 = new Move(new Position(File.D, 7), new Position(File.D, 5), Player.Black);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");
        }

        [Test]
        public static void TestValidMoveBlackPawn_Capture()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.E, 7), new Position(File.E, 5), Player.Black);
            Move move3 = new Move(new Position(File.D, 2), new Position(File.D, 4), Player.White);
            Move move4 = new Move(new Position(File.E, 5), new Position(File.D, 4), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);

            Assert.True(cb.IsValidMove(move4));
        }

        [Test]
        public static void TestValidMoveBlackPawn_EnPassant()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.B, 1), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.E, 7), new Position(File.E, 5), Player.Black);
            Move move3 = new Move(new Position(File.E, 2), new Position(File.E, 3), Player.White);
            Move move4 = new Move(new Position(File.E, 5), new Position(File.E, 4), Player.Black);
            Move move5 = new Move(new Position(File.D, 2), new Position(File.D, 4), Player.White);
            Move move6 = new Move(new Position(File.E, 4), new Position(File.D, 3), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);
            cb.MakeMove(move4, true);
            cb.MakeMove(move5, true);

            Assert.True(cb.IsValidMove(move6));
        }

        [Test]
        public static void TestInvalidMoveBlackPawn_EnPassant()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.B, 1), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.E, 7), new Position(File.E, 5), Player.Black);
            Move move3 = new Move(new Position(File.E, 2), new Position(File.E, 3), Player.White);
            Move move4 = new Move(new Position(File.E, 5), new Position(File.E, 4), Player.Black);
            Move move5 = new Move(new Position(File.H, 2), new Position(File.H, 4), Player.White);
            Move move6 = new Move(new Position(File.E, 4), new Position(File.D, 3), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);
            cb.MakeMove(move4, true);
            cb.MakeMove(move5, true);

            Assert.False(cb.IsValidMove(move6));
        }

        [Test]
        public static void TestInvalidMoveBlackPawn_NoCapture()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.E, 7), new Position(File.E, 5), Player.Black);
            Move move3 = new Move(new Position(File.D, 2), new Position(File.D, 3), Player.White);
            Move move4 = new Move(new Position(File.E, 5), new Position(File.D, 4), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);

            Assert.False(cb.IsValidMove(move4));
        }

        [Test]
        public static void TestInvalidMoveBlackPawn_2StepsBlockingPiece()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, pb, o, o, o },
                new[] { o, o, o, o, pw, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { kw, o, kb, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move = new Move(new Position(File.E, 7), new Position(File.E, 5), Player.Black);

            Assert.False(cb.IsValidMove(move), "move should be invalid");

        }

        [Test]
        public static void TestInvalidMoveBlackPawn_2StepsNotOnRank7()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.E, 7), new Position(File.E, 6), Player.Black);
            Move move3 = new Move(new Position(File.H, 2), new Position(File.H, 3), Player.White);
            Move move4 = new Move(new Position(File.E, 6), new Position(File.E, 4), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);

            Assert.False(cb.IsValidMove(move4), "move4 should be invalid");
        }

        [Test]
        public static void TestValidMoveBlackKnight()
        {
            ChessGame cb = new ChessGame();
            cb.MakeMove(new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White), true);

            Move move1 = new Move(new Position(File.B, 8), new Position(File.C, 6), Player.Black);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");
        }

        [Test]
        public static void TestValidMoveBlackBishopC()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.D, 7), new Position(File.D, 6), Player.Black);
            Move move3 = new Move(new Position(File.H, 2), new Position(File.H, 3), Player.White);
            Move move4 = new Move(new Position(File.C, 8), new Position(File.F, 5), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);

            Assert.True(cb.IsValidMove(move4), "move4 should be valid");
        }

        [Test]
        public static void TestValidMoveBlackBishopF()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.E, 7), new Position(File.E, 6), Player.Black);
            Move move3 = new Move(new Position(File.H, 2), new Position(File.H, 3), Player.White);
            Move move4 = new Move(new Position(File.F, 8), new Position(File.C, 5), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);

            Assert.True(cb.IsValidMove(move4), "move4 should be valid");
        }

        [Test]
        public static void TestValidMoveBlackQueenDiagonal()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.E, 7), new Position(File.E, 6), Player.Black);
            Move move3 = new Move(new Position(File.H, 2), new Position(File.H, 3), Player.White);
            Move move4 = new Move(new Position(File.D, 8), new Position(File.H, 4), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);

            Assert.True(cb.IsValidMove(move4), "move4 should be valid");
        }

        [Test]
        public static void TestValidMoveBlackQueenVertical()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.D, 7), new Position(File.D, 6), Player.Black);
            Move move3 = new Move(new Position(File.H, 2), new Position(File.H, 3), Player.White);
            Move move4 = new Move(new Position(File.D, 8), new Position(File.D, 7), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);

            Assert.True(cb.IsValidMove(move4), "move4 should be valid");
        }

        [Test]
        public static void TestValidMoveBlackQueenHorizontal()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.B, 8), new Position(File.C, 6), Player.Black);
            Move move3 = new Move(new Position(File.B, 2), new Position(File.B, 3), Player.White);
            Move move4 = new Move(new Position(File.D, 7), new Position(File.D, 6), Player.Black);
            Move move5 = new Move(new Position(File.C, 2), new Position(File.C, 3), Player.White);
            Move move6 = new Move(new Position(File.C, 8), new Position(File.D, 7), Player.Black);
            Move move7 = new Move(new Position(File.D, 2), new Position(File.D, 3), Player.White);
            Move move8 = new Move(new Position(File.D, 8), new Position(File.C, 8), Player.Black);
            Move move9 = new Move(new Position(File.D, 8), new Position(File.B, 8), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);
            cb.MakeMove(move4, true);
            cb.MakeMove(move5, true);
            cb.MakeMove(move6, true);
            cb.MakeMove(move7, true);

            Assert.True(cb.IsValidMove(move8), "move8 should be valid");
            Assert.True(cb.IsValidMove(move9), "move9 should be valid");
        }

        [Test]
        public static void TestValidMoveBlackKingDiagonal()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.F, 7), new Position(File.F, 6), Player.Black);
            Move move3 = new Move(new Position(File.H, 2), new Position(File.H, 3), Player.White);
            Move move4 = new Move(new Position(File.E, 8), new Position(File.F, 7), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);

            Assert.True(cb.IsValidMove(move4), "move4 should be valid");
        }

        [Test]
        public static void TestValidMoveBlackKingHorizontal()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.D, 7), new Position(File.D, 6), Player.Black);
            Move move3 = new Move(new Position(File.H, 2), new Position(File.H, 3), Player.White);
            Move move4 = new Move(new Position(File.D, 8), new Position(File.D, 7), Player.Black);
            Move move5 = new Move(new Position(File.B, 2), new Position(File.B, 3), Player.White);
            Move move6 = new Move(new Position(File.E, 8), new Position(File.D, 8), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);
            cb.MakeMove(move4, true);
            cb.MakeMove(move5, true);

            Assert.True(cb.IsValidMove(move6), "move6 should be valid");
        }

        [Test]
        public static void TestValidMoveBlackKingVertical()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.E, 7), new Position(File.E, 6), Player.Black);
            Move move3 = new Move(new Position(File.H, 2), new Position(File.H, 3), Player.White);
            Move move4 = new Move(new Position(File.E, 8), new Position(File.E, 7), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);

            Assert.True(cb.IsValidMove(move4), "move4 should be valid");
        }

        [Test]
        public static void TestValidMoveBlackKing_KingsideCastling()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, rb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.G, 8), Player.Black);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");

            Assert.Contains(new Move("E8", "G8", Player.Black), cb.GetValidMoves(Player.Black));
            Assert.Contains(new Move("E8", "G8", Player.Black), cb.GetValidMoves(new Position("E8")));
        }

        [Test]
        public static void TestValidMoveBlackKing_KingsideCastling2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, rb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.H, 8), Player.Black);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");

            Assert.Contains(new Move("E8", "H8", Player.Black), cb.GetValidMoves(Player.Black));
            Assert.Contains(new Move("E8", "H8", Player.Black), cb.GetValidMoves(new Position("E8")));
        }

        [Test]
        public static void TestValidMoveBlackKing_QueensideCastling()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.C, 8), Player.Black);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");

            Assert.Contains(new Move("E8", "C8", Player.Black), cb.GetValidMoves(Player.Black));
            Assert.Contains(new Move("E8", "C8", Player.Black), cb.GetValidMoves(new Position("E8")));
        }

        [Test]
        public static void TestValidMoveBlackKing_QueensideCastling2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.A, 8), Player.Black);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");

            Assert.Contains(new Move("E8", "A8", Player.Black), cb.GetValidMoves(Player.Black));
            Assert.Contains(new Move("E8", "A8", Player.Black), cb.GetValidMoves(new Position("E8")));
        }

        [Test]
        public static void TestInvalidMoveBlackKing_KingsideCastling_WouldPassThroughCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, rb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, rw, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.G, 8), Player.Black);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_KingsideCastling_WouldPassThroughCheck2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, rb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, rw, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.H, 8), Player.Black);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_QueensideCastling_WouldPassThroughCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, rw, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.C, 8), Player.Black);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_QueensideCastling_WouldPassThroughCheck2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, rw, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.A, 8), Player.Black);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestValidMoveBlackKing_KingsideCastling_WouldNotPassThroughCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, rb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, rw }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.G, 8), Player.Black);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");
        }

        [Test]
        public static void TestValidMoveBlackKing_KingsideCastling_WouldNotPassThroughCheck2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, rb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, rw }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.H, 8), Player.Black);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");
        }

        [Test]
        public static void TestValidMoveBlackKing_QueensideCastling_WouldNotPassThroughCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, rw, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.C, 8), Player.Black);

            Assert.True(cb.IsValidMove(move1), "move1 should be valid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_KingsideCastling_BlockingPiece1()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, rb, rb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o}
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.G, 8), Player.Black);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_QueensideCastling_BlockingPiece1()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, rb, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.C, 8), Player.Black);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_KingsideCastling_BlockingPiece2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, rb, o, rb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.G, 8), Player.Black);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_QueensideCastling_BlockingPiece2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, o, rb, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.C, 8), Player.Black);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_QueensideCastling_BlockingPiece3()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, o, o, rb, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.C, 8), Player.Black);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_KingsideCastling_NoRook()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.G, 8), Player.Black);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_QueensideCastling_NoRook()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.C, 8), Player.Black);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_KingsideCastling_RookMoved()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, rb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, kw, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.H, 8), new Position(File.H, 7), Player.Black);
            Move move2 = new Move(new Position(File.B, 1), new Position(File.B, 2), Player.White);
            Move move3 = new Move(new Position(File.E, 8), new Position(File.G, 8), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.False(cb.IsValidMove(move3), "move3 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_QueensideCastling_RookMoved()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, kw, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.A, 8), new Position(File.A, 7), Player.Black);
            Move move2 = new Move(new Position(File.B, 1), new Position(File.B, 2), Player.White);
            Move move3 = new Move(new Position(File.E, 8), new Position(File.C, 8), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.False(cb.IsValidMove(move3), "move3 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_KingsideCastling_KingMoved()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, rb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, kw, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.D, 8), Player.Black);
            Move move2 = new Move(new Position(File.B, 1), new Position(File.B, 2), Player.White);
            Move move3 = new Move(new Position(File.D, 8), new Position(File.F, 8), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.False(cb.IsValidMove(move3), "move3 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackKing_QueensideCastling_KingMoved()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, kw, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.E, 8), new Position(File.D, 8), Player.Black);
            Move move2 = new Move(new Position(File.B, 1), new Position(File.B, 2), Player.White);
            Move move3 = new Move(new Position(File.D, 8), new Position(File.B, 8), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);

            Assert.False(cb.IsValidMove(move3), "move3 should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhite_WouldBeInCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { kw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, qb },
                new[] { pw, o, o, pb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move = new Move(new Position(File.A, 8), new Position(File.A, 7), Player.White);

            Assert.False(cb.IsValidMove(move));
        }

        [Test]
        public static void TestInvalidMoveWhite_WouldBeCheckmated()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, rb },
                new[] { o, o, o, kw, nb, o, rb, o }
            };

            ChessGame cb = new ChessGame(board, Player.White);
            Move move = new Move(new Position(File.D, 1), new Position(File.E, 1), Player.White);

            Assert.False(cb.IsValidMove(move));
        }

        [Test]
        public static void TestInvalidMoveWhiteRook_NoPassThrough()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { rw, o, o, o, pw, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, kw, o, kb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            Move move = new Move(new Position(File.A, 7), new Position(File.G, 7), Player.White);

            Assert.False(cb.IsValidMove(move));
        }

        [Test]
        public static void TestInvalidMoveBlackKing_NoAdjacentKings()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kb, o, kw, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            Move move1 = new Move(new Position(File.D, 6), new Position(File.E, 6), Player.Black);
            Move move2 = new Move(new Position(File.D, 6), new Position(File.E, 7), Player.Black);
            Move move3 = new Move(new Position(File.D, 6), new Position(File.E, 5), Player.Black);

            Assert.False(cb.IsValidMove(move1), "move1 should be invalid");
            Assert.False(cb.IsValidMove(move2), "move2 should be invalid");
            Assert.False(cb.IsValidMove(move3), "move3 should be invalid");
        }

        [Test]
        public static void TestMakeMoveWhitePawn()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.E, 2), new Position(File.E, 3), Player.White);
            Assert.AreNotEqual(cb.MakeMove(move1, false), MoveType.Invalid);
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
            Assert.AreEqual(expected, cb.GetBoard(), "Unexpected board layout after applying move1");
            Move move2 = new Move(new Position(File.E, 3), new Position(File.E, 4), Player.White);
            Assert.AreNotEqual(cb.MakeMove(move2, true), MoveType.Invalid);
            expected = new Piece[8][]
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
            Assert.AreEqual(expected, cb.GetBoard(), "Unexpected board layout after applying move2");
        }

        [Test]
        public static void TestMakeMoveWhitePawn_EnPassant()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.E, 2), new Position(File.E, 4), Player.White);
            Move move2 = new Move(new Position(File.E, 7), new Position(File.E, 6), Player.Black);
            Move move3 = new Move(new Position(File.E, 4), new Position(File.E, 5), Player.White);
            Move move4 = new Move(new Position(File.D, 7), new Position(File.D, 5), Player.Black);
            Move move5 = new Move(new Position(File.E, 5), new Position(File.D, 6), Player.White);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);
            cb.MakeMove(move4, true);
            cb.MakeMove(move5, true);

            Piece[][] board = new Piece[8][]
            {
                new[] { rb, nb, bb, qb, kb, bb, nb, rb },
                new[] { pb, pb, pb, o, o, pb, pb, pb },
                new[] { o, o, o, pw, pb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { pw, pw, pw, pw, o, pw, pw, pw },
                new[] { rw, nw, bw, qw, kw, bw, nw, rw }
            };

            Assert.AreEqual(board, cb.GetBoard(), "Unexpected board layout after en passant capture.");
        }

        [Test]
        public static void TestMakeMoveWhitePawn_PromotionToQueen()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { pw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kw, o, kb, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            Move move = new Move(new Position(File.A, 7), new Position(File.A, 8), Player.White, 'Q');
            ChessGame cb = new ChessGame(board, Player.White);
            Assert.AreNotEqual(cb.MakeMove(move, false), MoveType.Invalid, "move should be valid");

            Piece[][] expected = new Piece[8][]
            {
                new[] { qw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kw, o, kb, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            Assert.AreEqual(expected, cb.GetBoard());
        }

        [Test]
        public static void TestMakeMoveBlackPawn_PromotionToQueen()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kw, o, kb, pb, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            Move move = new Move(new Position(File.G, 2), new Position(File.G, 1), Player.Black, 'Q');
            ChessGame cb = new ChessGame(board, Player.Black);
            Assert.AreNotEqual(cb.MakeMove(move, false), MoveType.Invalid, "move should be valid");

            Piece[][] expected = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kw, o, kb, o, o },
                new[] { o, o, o, o, o, o, qb, o }
            };
            Assert.AreEqual(expected, cb.GetBoard());
        }

        [Test]
        public static void TestInvalidMoveWhitePawnPromotion_NoPieceSpecified()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { pw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kw, o, kb, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            Move move = new Move(new Position(File.A, 7), new Position(File.A, 8), Player.White);
            ChessGame cb = new ChessGame(board, Player.White);
            Assert.False(cb.IsValidMove(move), "move should be invalid");
        }

        [Test]
        public static void TestInvalidMoveWhitePawnPromotion_PromotionToKing()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { pw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kw, o, kb, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            Move move = new Move(new Position(File.A, 7), new Position(File.A, 8), Player.White, 'K');
            ChessGame cb = new ChessGame(board, Player.White);
            Assert.False(cb.IsValidMove(move), "move should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackPawnPromotion_NoPieceSpecified()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kw, o, kb, pb, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            Move move = new Move(new Position(File.G, 2), new Position(File.G, 1), Player.Black);
            ChessGame cb = new ChessGame(board, Player.Black);
            Assert.False(cb.IsValidMove(move), "move should be invalid");
        }

        [Test]
        public static void TestInvalidMoveBlackPawnPromotion_PromotionToPawn()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kw, o, kb, pb, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            Move move = new Move(new Position(File.G, 2), new Position(File.G, 1), Player.Black, 'P');
            ChessGame cb = new ChessGame(board, Player.Black);
            Assert.False(cb.IsValidMove(move), "move should be invalid");
        }

        [Test]
        public static void TestMakeMoveBlackPawn_EnPassant()
        {
            ChessGame cb = new ChessGame();
            Move move1 = new Move(new Position(File.B, 1), new Position(File.A, 3), Player.White);
            Move move2 = new Move(new Position(File.E, 7), new Position(File.E, 5), Player.Black);
            Move move3 = new Move(new Position(File.E, 2), new Position(File.E, 3), Player.White);
            Move move4 = new Move(new Position(File.E, 5), new Position(File.E, 4), Player.Black);
            Move move5 = new Move(new Position(File.D, 2), new Position(File.D, 4), Player.White);
            Move move6 = new Move(new Position(File.E, 4), new Position(File.D, 3), Player.Black);

            cb.MakeMove(move1, true);
            cb.MakeMove(move2, true);
            cb.MakeMove(move3, true);
            cb.MakeMove(move4, true);
            cb.MakeMove(move5, true);
            cb.MakeMove(move6, true);

            Piece[][] board = new Piece[8][]
            {
                new[] { rb, nb, bb, qb, kb, bb, nb, rb },
                new[] { pb, pb, pb, pb, o, pb, pb, pb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { nw, o, o, pb, pw, o, o, o },
                new[] { pw, pw, pw, o, o, pw, pw, pw },
                new[] { rw, o, bw, qw, kw, bw, nw, rw }
            };

            Assert.AreEqual(board, cb.GetBoard(), "Unexpected board layout after en passant capture.");
        }

        [Test]
        public static void TestMakeMoveWhiteKing_KingsideCastling()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, rw }
            };
            Move move = new Move(new Position(File.E, 1), new Position(File.G, 1), Player.White);
            ChessGame cb = new ChessGame(board, Player.White);
            cb.MakeMove(move, true);

            Piece[][] expected = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, rw, kw, o }
            };

            Assert.AreEqual(expected, cb.GetBoard());
        }

        [Test]
        public static void TestMakeMoveWhiteKing_KingsideCastling2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, rw }
            };
            Move move = new Move(new Position(File.E, 1), new Position(File.H, 1), Player.White);
            ChessGame cb = new ChessGame(board, Player.White);
            cb.MakeMove(move, true);

            Piece[][] expected = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, rw, kw, o }
            };

            Assert.AreEqual(expected, cb.GetBoard());
        }

        [Test]
        public static void TestMakeMoveWhiteKing_QueensideCastling()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { rw, o, o, o, kw, o, o, o }
            };
            Move move = new Move(new Position(File.E, 1), new Position(File.C, 1), Player.White);
            ChessGame cb = new ChessGame(board, Player.White);
            cb.MakeMove(move, true);

            Piece[][] expected = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, kw, rw, o, o, o, o }
            };

            Assert.AreEqual(expected, cb.GetBoard());
        }


        [Test]
        public static void TestMakeMoveWhiteKing_QueensideCastling2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { rw, o, o, o, kw, o, o, o }
            };
            Move move = new Move(new Position(File.E, 1), new Position(File.A, 1), Player.White);
            ChessGame cb = new ChessGame(board, Player.White);
            cb.MakeMove(move, true);

            Piece[][] expected = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, kw, rw, o, o, o, o }
            };

            Assert.AreEqual(expected, cb.GetBoard());
        }

        [Test]
        public static void TestMakeMoveBlackKing_KingsideCastling()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, rb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            Move move = new Move(new Position(File.E, 8), new Position(File.G, 8), Player.Black);
            ChessGame cb = new ChessGame(board, Player.Black);
            cb.MakeMove(move, true);

            Piece[][] expected = new Piece[8][]
            {
                new[] { o, o, o, o, o, rb, kb, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };

            Assert.AreEqual(expected, cb.GetBoard());
        }

        [Test]
        public static void TestMakeMoveBlackKing_KingsideCastling2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, rb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            Move move = new Move(new Position(File.E, 8), new Position(File.H, 8), Player.Black);
            ChessGame cb = new ChessGame(board, Player.Black);
            cb.MakeMove(move, true);

            Piece[][] expected = new Piece[8][]
            {
                new[] { o, o, o, o, o, rb, kb, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };

            Assert.AreEqual(expected, cb.GetBoard());
        }

        [Test]
        public static void TestMakeMoveBlackKing_QueensideCastling()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            Move move = new Move(new Position(File.E, 8), new Position(File.C, 8), Player.Black);
            ChessGame cb = new ChessGame(board, Player.Black);
            cb.MakeMove(move, true);

            Piece[][] expected = new Piece[8][]
            {
                new[] { o, o, kb, rb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };

            Assert.AreEqual(expected, cb.GetBoard());
        }


        [Test]
        public static void TestMakeMoveBlackKing_QueensideCastling2()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { rb, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };
            Move move = new Move(new Position(File.E, 8), new Position(File.A, 8), Player.Black);
            ChessGame cb = new ChessGame(board, Player.Black);
            cb.MakeMove(move, true);

            Piece[][] expected = new Piece[8][]
            {
                new[] { o, o, kb, rb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o }
            };

            Assert.AreEqual(expected, cb.GetBoard());
        }

        [Test]
        public static void TestGetValidMovesWhiteStartingPosition()
        {
            ChessGame cb = new ChessGame();
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(Player.White);
            List<Move> expected = new List<Move>()
            {
                new Move("A2", "A3", Player.White),
                new Move("A2", "A4", Player.White),
                new Move("B2", "B3", Player.White),
                new Move("B2", "B4", Player.White),
                new Move("C2", "C3", Player.White),
                new Move("C2", "C4", Player.White),
                new Move("D2", "D3", Player.White),
                new Move("D2", "D4", Player.White),
                new Move("E2", "E3", Player.White),
                new Move("E2", "E4", Player.White),
                new Move("F2", "F3", Player.White),
                new Move("F2", "F4", Player.White),
                new Move("G2", "G3", Player.White),
                new Move("G2", "G4", Player.White),
                new Move("H2", "H3", Player.White),
                new Move("H2", "H4", Player.White),
                new Move("B1", "A3", Player.White),
                new Move("B1", "C3", Player.White),
                new Move("G1", "F3", Player.White),
                new Move("G1", "H3", Player.White)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesBlackStartingPosition()
        {
            ChessGame cb = new ChessGame();
            cb.MakeMove(new Move(new Position(File.A, 2), new Position(File.A, 3), Player.White), true);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(Player.Black);
            List<Move> expected = new List<Move>()
            {
                new Move("A7", "A6", Player.Black),
                new Move("A7", "A5", Player.Black),
                new Move("B7", "B6", Player.Black),
                new Move("B7", "B5", Player.Black),
                new Move("C7", "C6", Player.Black),
                new Move("C7", "C5", Player.Black),
                new Move("D7", "D6", Player.Black),
                new Move("D7", "D5", Player.Black),
                new Move("E7", "E6", Player.Black),
                new Move("E7", "E5", Player.Black),
                new Move("F7", "F6", Player.Black),
                new Move("F7", "F5", Player.Black),
                new Move("G7", "G6", Player.Black),
                new Move("G7", "G5", Player.Black),
                new Move("H7", "H6", Player.Black),
                new Move("H7", "H5", Player.Black),
                new Move("B8", "A6", Player.Black),
                new Move("B8", "C6", Player.Black),
                new Move("G8", "F6", Player.Black),
                new Move("G8", "H6", Player.Black)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesWhiteKing()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, kb, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kw, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.D, 4));
            List<Move> expected = new List<Move>()
            {
                new Move("D4", "C3", Player.White),
                new Move("D4", "C4", Player.White),
                new Move("D4", "C5", Player.White),
                new Move("D4", "D3", Player.White),
                new Move("D4", "D5", Player.White),
                new Move("D4", "E3", Player.White),
                new Move("D4", "E4", Player.White),
                new Move("D4", "E5", Player.White)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesWhiteKnight()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, kw, o, kb, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, nw, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.D, 4));
            List<Move> expected = new List<Move>()
            {
                new Move("D4", "C2", Player.White),
                new Move("D4", "B3", Player.White),
                new Move("D4", "C6", Player.White),
                new Move("D4", "B5", Player.White),
                new Move("D4", "E2", Player.White),
                new Move("D4", "E6", Player.White),
                new Move("D4", "F3", Player.White),
                new Move("D4", "F5", Player.White)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesWhiteBishop()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, kw, o, kb, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, bw, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.D, 4));
            List<Move> expected = new List<Move>()
            {
                new Move("D4", "A1", Player.White),
                new Move("D4", "B2", Player.White),
                new Move("D4", "C3", Player.White),
                new Move("D4", "E5", Player.White),
                new Move("D4", "F6", Player.White),
                new Move("D4", "G7", Player.White),
                new Move("D4", "H8", Player.White),
                new Move("D4", "G7", Player.White),
                new Move("D4", "F6", Player.White),
                new Move("D4", "E5", Player.White),
                new Move("D4", "C3", Player.White),
                new Move("D4", "B2", Player.White),
                new Move("D4", "A1", Player.White)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesWhiteRook()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kw, o, kb, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, rw, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.D, 4));
            List<Move> expected = new List<Move>()
            {
                new Move("D4", "D1", Player.White),
                new Move("D4", "D2", Player.White),
                new Move("D4", "D3", Player.White),
                new Move("D4", "D5", Player.White),
                new Move("D4", "D6", Player.White),
                new Move("D4", "D7", Player.White),
                new Move("D4", "D8", Player.White),
                new Move("D4", "A4", Player.White),
                new Move("D4", "B4", Player.White),
                new Move("D4", "C4", Player.White),
                new Move("D4", "E4", Player.White),
                new Move("D4", "F4", Player.White),
                new Move("D4", "G4", Player.White),
                new Move("D4", "H4", Player.White)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesWhiteQueen()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kw, o, kb, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, qw, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.D, 4));
            List<Move> expected = new List<Move>()
            {
                new Move("D4", "D1", Player.White),
                new Move("D4", "D2", Player.White),
                new Move("D4", "D3", Player.White),
                new Move("D4", "D5", Player.White),
                new Move("D4", "D6", Player.White),
                new Move("D4", "D7", Player.White),
                new Move("D4", "D8", Player.White),
                new Move("D4", "A4", Player.White),
                new Move("D4", "B4", Player.White),
                new Move("D4", "C4", Player.White),
                new Move("D4", "E4", Player.White),
                new Move("D4", "F4", Player.White),
                new Move("D4", "G4", Player.White),
                new Move("D4", "H4", Player.White),
                new Move("D4", "A1", Player.White),
                new Move("D4", "B2", Player.White),
                new Move("D4", "C3", Player.White),
                new Move("D4", "E5", Player.White),
                new Move("D4", "F6", Player.White),
                new Move("D4", "G7", Player.White),
                new Move("D4", "H8", Player.White),
                new Move("D4", "G7", Player.White),
                new Move("D4", "F6", Player.White),
                new Move("D4", "E5", Player.White),
                new Move("D4", "C3", Player.White),
                new Move("D4", "B2", Player.White),
                new Move("D4", "A1", Player.White)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesWhitePawn()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { kw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kb, o, o, o, o },
                new[] { pb, o, pb, o, o, o, o, o },
                new[] { o, pw, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.B, 2));
            List<Move> expected = new List<Move>()
            {
                new Move("B2", "B3", Player.White),
                new Move("B2", "B4", Player.White),
                new Move("B2", "A3", Player.White),
                new Move("B2", "C3", Player.White)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesWhitePawnPromotion()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { kw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, pw },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.H, 7));
            List<Move> expected = new List<Move>()
            {
                new Move("H7", "H8", Player.White, 'Q'),
                new Move("H7", "H8", Player.White, 'r'),
                new Move("H7", "H8", Player.White, 'B'),
                new Move("H7", "H8", Player.White, 'n')
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesBlackKing()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { kw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.D, 4));
            List<Move> expected = new List<Move>()
            {
                new Move("D4", "C3", Player.Black),
                new Move("D4", "C4", Player.Black),
                new Move("D4", "C5", Player.Black),
                new Move("D4", "D3", Player.Black),
                new Move("D4", "D5", Player.Black),
                new Move("D4", "E3", Player.Black),
                new Move("D4", "E4", Player.Black),
                new Move("D4", "E5", Player.Black)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesBlackKnight()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, kw, o, kb, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, nb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.D, 4));
            List<Move> expected = new List<Move>()
            {
                new Move("D4", "C2", Player.Black),
                new Move("D4", "B3", Player.Black),
                new Move("D4", "C6", Player.Black),
                new Move("D4", "B5", Player.Black),
                new Move("D4", "E2", Player.Black),
                new Move("D4", "E6", Player.Black),
                new Move("D4", "F3", Player.Black),
                new Move("D4", "F5", Player.Black)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesBlackBishop()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, kw, o, kb, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, bb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.D, 4));
            List<Move> expected = new List<Move>()
            {
                new Move("D4", "A1", Player.Black),
                new Move("D4", "B2", Player.Black),
                new Move("D4", "C3", Player.Black),
                new Move("D4", "E5", Player.Black),
                new Move("D4", "F6", Player.Black),
                new Move("D4", "G7", Player.Black),
                new Move("D4", "H8", Player.Black),
                new Move("D4", "G7", Player.Black),
                new Move("D4", "F6", Player.Black),
                new Move("D4", "E5", Player.Black),
                new Move("D4", "C3", Player.Black),
                new Move("D4", "B2", Player.Black),
                new Move("D4", "A1", Player.Black)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesBlackRook()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kw, o, kb, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, rb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.D, 4));
            List<Move> expected = new List<Move>()
            {
                new Move("D4", "D1", Player.Black),
                new Move("D4", "D2", Player.Black),
                new Move("D4", "D3", Player.Black),
                new Move("D4", "D5", Player.Black),
                new Move("D4", "D6", Player.Black),
                new Move("D4", "D7", Player.Black),
                new Move("D4", "D8", Player.Black),
                new Move("D4", "A4", Player.Black),
                new Move("D4", "B4", Player.Black),
                new Move("D4", "C4", Player.Black),
                new Move("D4", "E4", Player.Black),
                new Move("D4", "F4", Player.Black),
                new Move("D4", "G4", Player.Black),
                new Move("D4", "H4", Player.Black)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesBlackQueen()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kw, o, kb, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, qb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.D, 4));
            List<Move> expected = new List<Move>()
            {
                new Move("D4", "D1", Player.Black),
                new Move("D4", "D2", Player.Black),
                new Move("D4", "D3", Player.Black),
                new Move("D4", "D5", Player.Black),
                new Move("D4", "D6", Player.Black),
                new Move("D4", "D7", Player.Black),
                new Move("D4", "D8", Player.Black),
                new Move("D4", "A4", Player.Black),
                new Move("D4", "B4", Player.Black),
                new Move("D4", "C4", Player.Black),
                new Move("D4", "E4", Player.Black),
                new Move("D4", "F4", Player.Black),
                new Move("D4", "G4", Player.Black),
                new Move("D4", "H4", Player.Black),
                new Move("D4", "A1", Player.Black),
                new Move("D4", "B2", Player.Black),
                new Move("D4", "C3", Player.Black),
                new Move("D4", "E5", Player.Black),
                new Move("D4", "F6", Player.Black),
                new Move("D4", "G7", Player.Black),
                new Move("D4", "H8", Player.Black),
                new Move("D4", "G7", Player.Black),
                new Move("D4", "F6", Player.Black),
                new Move("D4", "E5", Player.Black),
                new Move("D4", "C3", Player.Black),
                new Move("D4", "B2", Player.Black),
                new Move("D4", "A1", Player.Black)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesBlackPawn()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { kw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kb, o, o, o, o },
                new[] { o, pb, o, o, o, o, o, o },
                new[] { pw, o, pw, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.B, 3));
            List<Move> expected = new List<Move>()
            {
                new Move("B3", "B2", Player.Black),
                new Move("B3", "A2", Player.Black),
                new Move("B3", "C2", Player.Black)
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestGetValidMovesBlackPawnPromotion()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { kw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, kb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, pb },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);
            ReadOnlyCollection<Move> actual = cb.GetValidMoves(new Position(File.H, 2));
            List<Move> expected = new List<Move>()
            {
                new Move("H2", "H1", Player.Black, 'q'),
                new Move("H2", "H1", Player.Black, 'R'),
                new Move("H2", "H1", Player.Black, 'N'),
                new Move("H2", "H1", Player.Black, 'b')
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (Move move in expected)
            {
                Assert.True(actual.Contains(move), "Actual does not contain " + move.ToString());
            }
        }

        [Test]
        public static void TestIsWhiteInCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { kw, o, o, o, o, o, o, qb },
                new[] { pw, o, o, pb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { kb, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);

            Assert.True(cb.IsInCheck(Player.White));

            cb.MakeMove(new Move(new Position(File.A, 7), new Position(File.A, 8), Player.White), true);
            Assert.False(cb.IsInCheck(Player.White));
        }

        [Test]
        public static void TestIsWhiteInCheck_OnRank1()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, pb, o },
                new[] { kb, o, o, o, o, o, o, kw }
            };
            ChessGame cb = new ChessGame(board, Player.White);

            Assert.True(cb.IsInCheck(Player.White));
        }

        [Test]
        public static void TestIsWhiteNotInCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { kw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, qb },
                new[] { pw, o, o, pb, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);

            Assert.False(cb.IsInCheck(Player.White));
        }

        [Test]
        public static void TestIsWhiteNotInCheck_PawnsCanOnlyCheckDiagonally()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, o },
                new[] { o, o, o, o, pb, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { kb, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);

            Assert.False(cb.IsInCheck(Player.White));
        }

        [Test]
        public static void TestIsBlackInCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, nw, o, o, o, o },
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { kw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, pb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);

            Assert.True(cb.IsInCheck(Player.Black));

            cb.MakeMove(new Move(new Position(File.B, 7), new Position(File.A, 7), Player.Black), true);

            Assert.False(cb.IsInCheck(Player.Black));
        }

        [Test]
        public static void TestIsBlackInCheck_OnRank8()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, kb, o, o, o },
                new[] { o, o, o, pw, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { kw, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);

            Assert.True(cb.IsInCheck(Player.Black));
        }

        [Test]
        public static void TestIsBlackNotInCheck()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, nb, o, o, o, o },
                new[] { o, kb, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { kw, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, pb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);

            Assert.False(cb.IsInCheck(Player.Black));
        }

        [Test]
        public static void TestIsBlackNotInCheck_PawnsCanOnlyCheckDiagonally()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kb, o, o, o },
                new[] { o, o, o, o, pw, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { kw, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);

            Assert.False(cb.IsInCheck(Player.Black));
        }

        [Test]
        public static void TestBlackCheckmated()
        {
            ChessGame cb = new ChessGame();
            cb.MakeMove(new Move("E2", "E4", Player.White), true);
            cb.MakeMove(new Move("E7", "E5", Player.Black), true);
            cb.MakeMove(new Move("F1", "C4", Player.White), true);
            cb.MakeMove(new Move("D7", "D6", Player.Black), true);
            cb.MakeMove(new Move("D1", "F3", Player.White), true);
            cb.MakeMove(new Move("H7", "H6", Player.Black), true);
            Assert.AreNotEqual(cb.MakeMove(new Move("F3", "F7", Player.White), false), MoveType.Invalid);

            Assert.True(cb.IsCheckmated(Player.Black));
            Assert.True(cb.IsWinner(Player.White));
        }

        [Test]
        public static void TestBlackStalemated()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { kb, o, kw, o, o, o, o, o },
                new[] { o, o, qw, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);

            Assert.True(cb.IsStalemated(Player.Black));
            Assert.True(cb.IsDraw());
        }

        [Test]
        public static void TestBlackNotStalemated()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { kb, o, kw, o, o, o, o, o },
                new[] { o, o, qw, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.White);

            Assert.False(cb.IsStalemated(Player.Black));
        }

        [Test]
        public static void TestBlackNotStalematedAfterMakeMove()
        {
            Piece[][] board = new Piece[8][]
            {
                new[] { o, o, kw, o, o, o, o, o },
                new[] { kb, o, qw, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o }
            };
            ChessGame cb = new ChessGame(board, Player.Black);

            Assert.AreNotEqual(cb.MakeMove(new Move("A7", "A8", Player.Black), false), MoveType.Invalid);

            Assert.False(cb.IsStalemated(Player.Black));
        }

        [Test]
        public static void TestMakeMove_ReturnedMoveType()
        {
            ChessGame game = new ChessGame();
            MoveType type = game.MakeMove(new Move("E2", "E4", Player.White), true);
            Assert.AreEqual(type, MoveType.Move);
            type = game.MakeMove(new Move("D7", "D5", Player.Black), true);
            Assert.AreEqual(type, MoveType.Move);
            type = game.MakeMove(new Move("E4", "D5", Player.White), true);
            Assert.AreEqual(type, MoveType.Move | MoveType.Capture);
            type = game.MakeMove(new Move("A5", "A4", Player.White), false);
            Assert.AreEqual(type, MoveType.Invalid);

            Piece[][] board = new Piece[8][]
            {
                new[] { rb, o, o, kb, o, o, o, o },
                new[] { o, pw, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { pb, o, o, o, o, o, o, o },
                new[] { o, o, o, o, kw, o, o, rw }
            };
            game = new ChessGame(board, Player.White);
            type = game.MakeMove(new Move("E1", "G1", Player.White), true);
            Assert.AreEqual(type, MoveType.Move | MoveType.Castling);
            type = game.MakeMove(new Move("A2", "A1", Player.Black, 'Q'), true);
            Assert.AreEqual(type, MoveType.Move | MoveType.Promotion);
            type = game.MakeMove(new Move("B7", "A8", Player.White, 'Q'), true);
            Assert.AreEqual(type, MoveType.Move | MoveType.Capture | MoveType.Promotion);
        }

        [Test]
        public static void TestFenCastlingFieldAfterRookCapture_BlackKingside()
        {
            ChessGame game = new ChessGame("rnbqkbnr/p1pppp1p/1p4p1/8/8/1P6/PBPPPPPP/RN1QKBNR w KQkq - 0 3");
            game.MakeMove(new Move("b2", "h8", Player.White), true);
            Assert.False(game.CanBlackCastleKingSide);
            Assert.AreEqual("rnbqkbnB/p1pppp1p/1p4p1/8/8/1P6/P1PPPPPP/RN1QKBNR b KQq - 0 3", game.GetFen());
        }

        [Test]
        public static void TestFenCastlingFieldAfterRookCapture_BlackQueenside()
        {
            ChessGame game = new ChessGame("rnbqkbnr/p1pppp1p/1p4p1/8/8/6P1/PPPPPPBP/RNBQK1NR w KQkq - 0 3");
            game.MakeMove(new Move("g2", "a8", Player.White), true);
            Assert.False(game.CanBlackCastleQueenSide);
            Assert.AreEqual("Bnbqkbnr/p1pppp1p/1p4p1/8/8/6P1/PPPPPP1P/RNBQK1NR b KQk - 0 3", game.GetFen());
        }

        [Test]
        public static void TestFenCastlingFieldAfterRookCapture_WhiteKingside()
        {
            ChessGame game = new ChessGame("rn1qkbnr/pbpppppp/1p6/8/1P6/6P1/P1PPPP1P/RNBQKBNR b KQkq - 0 3");
            game.MakeMove(new Move("b7", "h1", Player.Black), true);
            Assert.False(game.CanWhiteCastleKingSide);
            Assert.AreEqual("rn1qkbnr/p1pppppp/1p6/8/1P6/6P1/P1PPPP1P/RNBQKBNb w Qkq - 0 4", game.GetFen());
        }

        [Test]
        public static void TestFenCastlingFieldAfterRookCapture_WhiteQueenside()
        {
            ChessGame game = new ChessGame("rnbqk1nr/ppppppbp/6p1/8/6P1/1P6/P1PPPP1P/RNBQKBNR b KQkq - 0 3");
            game.MakeMove(new Move("g7", "a1", Player.Black), true);
            Assert.False(game.CanWhiteCastleQueenSide);
            Assert.AreEqual("rnbqk1nr/pppppp1p/6p1/8/6P1/1P6/P1PPPP1P/bNBQKBNR w Kkq - 0 4", game.GetFen());
        }

        [Test]
        public static void TestPgnGeneration()
        {
            string pgn = "1. e4 c5 2. Bc4 e6 3. Nc3 d5 4. exd5 exd5 5. Bxd5 Nf6 6. Bb3 Be7 7. Nge2 O-O 8. O-O Nc6 9. a3 Bd6 10. d3 Bxh2+ 11. Kxh2 Ng4+ 12. Kg3 h5 13. f3 h4+ 14. Kf4 Qf6+ 15. Ke4 Qe5# 0-1";
            PgnReader<ChessGame> reader = new PgnReader<ChessGame>();
            reader.ReadPgnFromString(pgn);
            Assert.AreEqual(pgn, reader.Game.GetPGN());
        }

        [Test]
        public static void TestSanAmbiguity1()
        {
            ChessGame game = new ChessGame("k7/8/K7/8/8/Q7/8/QQ6 w - - 0 1");
            game.MakeMove(new Move("B1", "B2", Player.White), true);
            Assert.AreEqual("Qbb2", game.Moves[game.Moves.Count - 1].SAN);

            game = new ChessGame("k7/8/K7/8/8/Q7/8/QQ6 w - - 0 1");
            game.MakeMove(new Move("A1", "B2", Player.White), true);
            Assert.AreEqual("Qa1b2", game.Moves[game.Moves.Count - 1].SAN);

            game = new ChessGame("k7/8/K7/8/8/Q7/8/QQ6 w - - 0 1");
            game.MakeMove(new Move("A3", "B2", Player.White), true);
            Assert.AreEqual("Q3b2", game.Moves[game.Moves.Count - 1].SAN);
        }

        [Test]
        public static void TestSanAmbiguity2()
        {
            ChessGame game = new ChessGame("7k/8/4n2K/8/8/5n2/2n5/8 b - - 0 1");
            game.MakeMove(new Move("F3", "D4", Player.Black), true);
            Assert.AreEqual("Nfd4", game.Moves[game.Moves.Count - 1].SAN);

            game = new ChessGame("7k/8/4n2K/8/8/5n2/2n5/8 b - - 0 1");
            game.MakeMove(new Move("C2", "D4", Player.Black), true);
            Assert.AreEqual("Ncd4", game.Moves[game.Moves.Count - 1].SAN);

            game = new ChessGame("7k/8/4n2K/8/8/5n2/2n5/8 b - - 0 1");
            game.MakeMove(new Move("E6", "D4", Player.Black), true);
            Assert.AreEqual("Ned4", game.Moves[game.Moves.Count - 1].SAN);
        }

        [Test]
        public static void TestSanAmbiguity3()
        {
            ChessGame game = new ChessGame("k7/8/K7/Q7/8/3B4/8/3B4 w - - 0 1");
            game.MakeMove(new Move("D1", "E2", Player.White), true);
            Assert.AreEqual("B1e2", game.Moves[game.Moves.Count - 1].SAN);

            game = new ChessGame("k7/8/K7/Q7/8/3B4/8/3B4 w - - 0 1");
            game.MakeMove(new Move("D3", "E2", Player.White), true);
            Assert.AreEqual("B3e2", game.Moves[game.Moves.Count - 1].SAN);
        }

        [Test]
        public static void TestSanAmbiguity4()
        {
            ChessGame game = new ChessGame("k7/8/K7/8/8/8/8/R6R w - - 0 1");
            game.MakeMove(new Move("A1", "E1", Player.White), true);
            Assert.AreEqual("Rae1", game.Moves[game.Moves.Count - 1].SAN);

            game = new ChessGame("k7/8/K7/8/8/8/8/R6R w - - 0 1");
            game.MakeMove(new Move("H1", "E1", Player.White), true);
            Assert.AreEqual("Rhe1", game.Moves[game.Moves.Count - 1].SAN);
        }

        [Test]
        public static void TestSanPawnCapturePromotionCheckmate()
        {
            ChessGame game = new ChessGame("k1r5/3P4/K7/8/8/8/8/8 w - - 0 1");
            game.MakeMove(new Move("D7", "C8", Player.White, 'R'), true);
            Assert.AreEqual("dxc8=R#", game.Moves[game.Moves.Count - 1].SAN);
        }

        [Test]
        public static void TestIsValidMoveEnPassantAfterCheck()
        {
            ChessGame cg = new ChessGame("2r5/p1pn1p2/1p6/5nkp/5Np1/4P1P1/PB3P1P/6K1 w - - 2 32");
            cg.MakeMove(new Move("H2", "H4", Player.White), true);
            Assert.IsTrue(cg.IsValidMove(new Move("G4", "H3", Player.Black)));
        }
    }
}
