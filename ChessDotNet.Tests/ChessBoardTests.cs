using NUnit.Framework;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public class ChessBoardTests
    {
        [Test]
        public void TestArrayGetting()
        {
            ChessBoard cb = new ChessBoard();
            int one = (int)Position.Ranks.One;
            int two = (int)Position.Ranks.Two;
            int three = (int)Position.Ranks.Three;
            int four = (int)Position.Ranks.Four;
            int five = (int)Position.Ranks.Five;
            int six = (int)Position.Ranks.Six;
            int seven = (int)Position.Ranks.Seven;
            int eight = (int)Position.Ranks.Eight;
            int a = (int)Position.Files.A;
            int b = (int)Position.Files.B;
            int c = (int)Position.Files.C;
            int d = (int)Position.Files.D;
            int e = (int)Position.Files.E;
            int f = (int)Position.Files.F;
            int g = (int)Position.Files.G;
            int h = (int)Position.Files.H;
            ChessPiece kw = new ChessPiece(Pieces.King, Players.White);
            ChessPiece kb = new ChessPiece(Pieces.King, Players.Black);
            ChessPiece qw = new ChessPiece(Pieces.Queen, Players.White);
            ChessPiece qb = new ChessPiece(Pieces.Queen, Players.Black);
            ChessPiece rw = new ChessPiece(Pieces.Rook, Players.White);
            ChessPiece rb = new ChessPiece(Pieces.Rook, Players.Black);
            ChessPiece nw = new ChessPiece(Pieces.Knight, Players.White);
            ChessPiece nb = new ChessPiece(Pieces.Knight, Players.Black);
            ChessPiece bw = new ChessPiece(Pieces.Bishop, Players.White);
            ChessPiece bb = new ChessPiece(Pieces.Bishop, Players.Black);
            ChessPiece pw = new ChessPiece(Pieces.Pawn, Players.White);
            ChessPiece pb = new ChessPiece(Pieces.Pawn, Players.Black);
            ChessPiece o = ChessPiece.None;
            Assert.AreEqual(cb.Board[one, a], rw);
            Assert.AreEqual(cb.Board[one, b], nw);
            Assert.AreEqual(cb.Board[one, c], bw);
            Assert.AreEqual(cb.Board[one, d], qw);
            Assert.AreEqual(cb.Board[one, e], kw);
            Assert.AreEqual(cb.Board[one, f], bw);
            Assert.AreEqual(cb.Board[one, g], nw);
            Assert.AreEqual(cb.Board[one, h], rw);
            Assert.AreEqual(cb.Board[two, a], pw);
            Assert.AreEqual(cb.Board[two, b], pw);
            Assert.AreEqual(cb.Board[two, c], pw);
            Assert.AreEqual(cb.Board[two, d], pw);
            Assert.AreEqual(cb.Board[two, e], pw);
            Assert.AreEqual(cb.Board[two, f], pw);
            Assert.AreEqual(cb.Board[two, g], pw);
            Assert.AreEqual(cb.Board[two, h], pw);
            Assert.AreEqual(cb.Board[three, a], o);
            Assert.AreEqual(cb.Board[three, b], o);
            Assert.AreEqual(cb.Board[three, c], o);
            Assert.AreEqual(cb.Board[three, d], o);
            Assert.AreEqual(cb.Board[three, e], o);
            Assert.AreEqual(cb.Board[three, f], o);
            Assert.AreEqual(cb.Board[three, g], o);
            Assert.AreEqual(cb.Board[three, h], o);
            Assert.AreEqual(cb.Board[four, a], o);
            Assert.AreEqual(cb.Board[four, b], o);
            Assert.AreEqual(cb.Board[four, c], o);
            Assert.AreEqual(cb.Board[four, d], o);
            Assert.AreEqual(cb.Board[four, e], o);
            Assert.AreEqual(cb.Board[four, f], o);
            Assert.AreEqual(cb.Board[four, g], o);
            Assert.AreEqual(cb.Board[four, h], o);
            Assert.AreEqual(cb.Board[five, a], o);
            Assert.AreEqual(cb.Board[five, b], o);
            Assert.AreEqual(cb.Board[five, c], o);
            Assert.AreEqual(cb.Board[five, d], o);
            Assert.AreEqual(cb.Board[five, e], o);
            Assert.AreEqual(cb.Board[five, f], o);
            Assert.AreEqual(cb.Board[five, g], o);
            Assert.AreEqual(cb.Board[five, h], o);
            Assert.AreEqual(cb.Board[six, a], o);
            Assert.AreEqual(cb.Board[six, b], o);
            Assert.AreEqual(cb.Board[six, c], o);
            Assert.AreEqual(cb.Board[six, d], o);
            Assert.AreEqual(cb.Board[six, e], o);
            Assert.AreEqual(cb.Board[six, f], o);
            Assert.AreEqual(cb.Board[six, g], o);
            Assert.AreEqual(cb.Board[six, h], o);
            Assert.AreEqual(cb.Board[seven, a], pb);
            Assert.AreEqual(cb.Board[seven, b], pb);
            Assert.AreEqual(cb.Board[seven, c], pb);
            Assert.AreEqual(cb.Board[seven, d], pb);
            Assert.AreEqual(cb.Board[seven, e], pb);
            Assert.AreEqual(cb.Board[seven, f], pb);
            Assert.AreEqual(cb.Board[seven, g], pb);
            Assert.AreEqual(cb.Board[seven, h], pb);
            Assert.AreEqual(cb.Board[eight, a], rb);
            Assert.AreEqual(cb.Board[eight, b], nb);
            Assert.AreEqual(cb.Board[eight, c], bb);
            Assert.AreEqual(cb.Board[eight, d], qb);
            Assert.AreEqual(cb.Board[eight, e], kb);
            Assert.AreEqual(cb.Board[eight, f], bb);
            Assert.AreEqual(cb.Board[eight, g], nb);
            Assert.AreEqual(cb.Board[eight, h], rb);
        }

        [Test]
        public void TestGetPieceAt()
        {
            ChessBoard cb = new ChessBoard();
            Position.Ranks one = Position.Ranks.One;
            Position.Ranks two = Position.Ranks.Two;
            Position.Ranks three = Position.Ranks.Three;
            Position.Ranks four = Position.Ranks.Four;
            Position.Ranks five = Position.Ranks.Five;
            Position.Ranks six = Position.Ranks.Six;
            Position.Ranks seven = Position.Ranks.Seven;
            Position.Ranks eight = Position.Ranks.Eight;
            Position.Files a = Position.Files.A;
            Position.Files b = Position.Files.B;
            Position.Files c = Position.Files.C;
            Position.Files d = Position.Files.D;
            Position.Files e = Position.Files.E;
            Position.Files f = Position.Files.F;
            Position.Files g = Position.Files.G;
            Position.Files h = Position.Files.H;
            ChessPiece kw = new ChessPiece(Pieces.King, Players.White);
            ChessPiece kb = new ChessPiece(Pieces.King, Players.Black);
            ChessPiece qw = new ChessPiece(Pieces.Queen, Players.White);
            ChessPiece qb = new ChessPiece(Pieces.Queen, Players.Black);
            ChessPiece rw = new ChessPiece(Pieces.Rook, Players.White);
            ChessPiece rb = new ChessPiece(Pieces.Rook, Players.Black);
            ChessPiece nw = new ChessPiece(Pieces.Knight, Players.White);
            ChessPiece nb = new ChessPiece(Pieces.Knight, Players.Black);
            ChessPiece bw = new ChessPiece(Pieces.Bishop, Players.White);
            ChessPiece bb = new ChessPiece(Pieces.Bishop, Players.Black);
            ChessPiece pw = new ChessPiece(Pieces.Pawn, Players.White);
            ChessPiece pb = new ChessPiece(Pieces.Pawn, Players.Black);
            ChessPiece o = ChessPiece.None;
            Assert.AreEqual(cb.GetPieceAt(a, one), rw);
            Assert.AreEqual(cb.GetPieceAt(b, one), nw);
            Assert.AreEqual(cb.GetPieceAt(c, one), bw);
            Assert.AreEqual(cb.GetPieceAt(d, one), qw);
            Assert.AreEqual(cb.GetPieceAt(e, one), kw);
            Assert.AreEqual(cb.GetPieceAt(f, one), bw);
            Assert.AreEqual(cb.GetPieceAt(g, one), nw);
            Assert.AreEqual(cb.GetPieceAt(h, one), rw);
            Assert.AreEqual(cb.GetPieceAt(a, two), pw);
            Assert.AreEqual(cb.GetPieceAt(b, two), pw);
            Assert.AreEqual(cb.GetPieceAt(c, two), pw);
            Assert.AreEqual(cb.GetPieceAt(d, two), pw);
            Assert.AreEqual(cb.GetPieceAt(e, two), pw);
            Assert.AreEqual(cb.GetPieceAt(f, two), pw);
            Assert.AreEqual(cb.GetPieceAt(g, two), pw);
            Assert.AreEqual(cb.GetPieceAt(h, two), pw);
            Assert.AreEqual(cb.GetPieceAt(a, three), o);
            Assert.AreEqual(cb.GetPieceAt(b, three), o);
            Assert.AreEqual(cb.GetPieceAt(c, three), o);
            Assert.AreEqual(cb.GetPieceAt(d, three), o);
            Assert.AreEqual(cb.GetPieceAt(e, three), o);
            Assert.AreEqual(cb.GetPieceAt(f, three), o);
            Assert.AreEqual(cb.GetPieceAt(g, three), o);
            Assert.AreEqual(cb.GetPieceAt(h, three), o);
            Assert.AreEqual(cb.GetPieceAt(a, four), o);
            Assert.AreEqual(cb.GetPieceAt(b, four), o);
            Assert.AreEqual(cb.GetPieceAt(c, four), o);
            Assert.AreEqual(cb.GetPieceAt(d, four), o);
            Assert.AreEqual(cb.GetPieceAt(e, four), o);
            Assert.AreEqual(cb.GetPieceAt(f, four), o);
            Assert.AreEqual(cb.GetPieceAt(g, four), o);
            Assert.AreEqual(cb.GetPieceAt(h, four), o);
            Assert.AreEqual(cb.GetPieceAt(a, five), o);
            Assert.AreEqual(cb.GetPieceAt(b, five), o);
            Assert.AreEqual(cb.GetPieceAt(c, five), o);
            Assert.AreEqual(cb.GetPieceAt(d, five), o);
            Assert.AreEqual(cb.GetPieceAt(e, five), o);
            Assert.AreEqual(cb.GetPieceAt(f, five), o);
            Assert.AreEqual(cb.GetPieceAt(g, five), o);
            Assert.AreEqual(cb.GetPieceAt(h, five), o);
            Assert.AreEqual(cb.GetPieceAt(a, six), o);
            Assert.AreEqual(cb.GetPieceAt(b, six), o);
            Assert.AreEqual(cb.GetPieceAt(c, six), o);
            Assert.AreEqual(cb.GetPieceAt(d, six), o);
            Assert.AreEqual(cb.GetPieceAt(e, six), o);
            Assert.AreEqual(cb.GetPieceAt(f, six), o);
            Assert.AreEqual(cb.GetPieceAt(g, six), o);
            Assert.AreEqual(cb.GetPieceAt(h, six), o);
            Assert.AreEqual(cb.GetPieceAt(a, seven), pb);
            Assert.AreEqual(cb.GetPieceAt(b, seven), pb);
            Assert.AreEqual(cb.GetPieceAt(c, seven), pb);
            Assert.AreEqual(cb.GetPieceAt(d, seven), pb);
            Assert.AreEqual(cb.GetPieceAt(e, seven), pb);
            Assert.AreEqual(cb.GetPieceAt(f, seven), pb);
            Assert.AreEqual(cb.GetPieceAt(g, seven), pb);
            Assert.AreEqual(cb.GetPieceAt(h, seven), pb);
            Assert.AreEqual(cb.GetPieceAt(a, eight), rb);
            Assert.AreEqual(cb.GetPieceAt(b, eight), nb);
            Assert.AreEqual(cb.GetPieceAt(c, eight), bb);
            Assert.AreEqual(cb.GetPieceAt(d, eight), qb);
            Assert.AreEqual(cb.GetPieceAt(e, eight), kb);
            Assert.AreEqual(cb.GetPieceAt(f, eight), bb);
            Assert.AreEqual(cb.GetPieceAt(g, eight), nb);
            Assert.AreEqual(cb.GetPieceAt(h, eight), rb);
        }

        [Test]
        public void TestValidMoveWhitePawn()
        {
            ChessBoard cb = new ChessBoard();

            Move m1 = new Move(new Position(Position.Files.A, Position.Ranks.Two), new Position(Position.Files.A, Position.Ranks.Three), Players.White);

            Assert.True(cb.IsValidMove(m1), "m1 is considered invalid");
        }

        [Test]
        public void TestValidMoveWhiteKnight()
        {
            ChessBoard cb = new ChessBoard();

            Move m1 = new Move(new Position(Position.Files.B, Position.Ranks.One), new Position(Position.Files.C, Position.Ranks.Three), Players.White);

            Assert.True(cb.IsValidMove(m1), "m1 is considered invalid");
        }

        [Test]
        public void TestValidMoveWhiteBishopC()
        {
            ChessBoard cb = new ChessBoard();
            Move m1 = new Move(new Position(Position.Files.D, Position.Ranks.Two), new Position(Position.Files.D, Position.Ranks.Three), Players.White);
            Move m2 = new Move(new Position(Position.Files.C, Position.Ranks.One), new Position(Position.Files.F, Position.Ranks.Four), Players.White);

            cb.ApplyMove(m1, true);

            Assert.True(cb.IsValidMove(m2), "m2 is considered invalid");
        }

        [Test]
        public void TestValidMoveWhiteBishopF()
        {
            ChessBoard cb = new ChessBoard();
            Move m1 = new Move(new Position(Position.Files.E, Position.Ranks.Two), new Position(Position.Files.E, Position.Ranks.Three), Players.White);
            Move m2 = new Move(new Position(Position.Files.F, Position.Ranks.One), new Position(Position.Files.C, Position.Ranks.Four), Players.White);

            cb.ApplyMove(m1, true);

            Assert.True(cb.IsValidMove(m2), "m2 is considered invalid");
        }

        [Test]
        public void TestValidMoveWhiteRookA()
        {
            ChessBoard cb = new ChessBoard();
            Move m1 = new Move(new Position(Position.Files.A, Position.Ranks.Two), new Position(Position.Files.A, Position.Ranks.Three), Players.White);
            Move m2 = new Move(new Position(Position.Files.A, Position.Ranks.One), new Position(Position.Files.A, Position.Ranks.Two), Players.White);

            cb.ApplyMove(m1, true);

            Assert.True(cb.IsValidMove(m2), "m2 is considered invalid");
        }

        [Test]
        public void TestValidMoveWhiteRookH()
        {
            ChessBoard cb = new ChessBoard();
            Move m1 = new Move(new Position(Position.Files.H, Position.Ranks.Two), new Position(Position.Files.H, Position.Ranks.Three), Players.White);
            Move m2 = new Move(new Position(Position.Files.H, Position.Ranks.One), new Position(Position.Files.H, Position.Ranks.Two), Players.White);

            cb.ApplyMove(m1, true);

            Assert.True(cb.IsValidMove(m2), "m2 is considered invalid");
        }

        [Test]
        public void TestValidMoveWhiteQueenDiagonal()
        {
            ChessBoard cb = new ChessBoard();
            Move m1 = new Move(new Position(Position.Files.E, Position.Ranks.Two), new Position(Position.Files.E, Position.Ranks.Three), Players.White);
            Move m2 = new Move(new Position(Position.Files.D, Position.Ranks.One), new Position(Position.Files.H, Position.Ranks.Five), Players.White);

            cb.ApplyMove(m1, true);

            Assert.True(cb.IsValidMove(m2), "m2 is considered invalid");
        }

        [Test]
        public void TestValidMoveWhiteQueenVertical()
        {
            ChessBoard cb = new ChessBoard();
            Move m1 = new Move(new Position(Position.Files.D, Position.Ranks.Two), new Position(Position.Files.D, Position.Ranks.Three), Players.White);
            Move m2 = new Move(new Position(Position.Files.D, Position.Ranks.One), new Position(Position.Files.D, Position.Ranks.Two), Players.White);

            cb.ApplyMove(m1, true);

            Assert.True(cb.IsValidMove(m2), "m2 is considered invalid");
        }

        [Test]
        public void TestValidMoveWhiteQueenHorizontal()
        {
            ChessBoard cb = new ChessBoard();
            Move m1 = new Move(new Position(Position.Files.B, Position.Ranks.One), new Position(Position.Files.C, Position.Ranks.Three), Players.White);
            Move m2 = new Move(new Position(Position.Files.D, Position.Ranks.Two), new Position(Position.Files.D, Position.Ranks.Three), Players.White);
            Move m3 = new Move(new Position(Position.Files.C, Position.Ranks.One), new Position(Position.Files.D, Position.Ranks.Two), Players.White);
            Move m4 = new Move(new Position(Position.Files.D, Position.Ranks.One), new Position(Position.Files.C, Position.Ranks.One), Players.White);
            Move m5 = new Move(new Position(Position.Files.D, Position.Ranks.One), new Position(Position.Files.B, Position.Ranks.One), Players.White);

            cb.ApplyMove(m1, true);
            cb.ApplyMove(m2, true);
            cb.ApplyMove(m3, true);

            Assert.True(cb.IsValidMove(m4), "m4 is considered invalid");
            Assert.True(cb.IsValidMove(m5), "m5 is considered invalid");
        }

        [Test]
        public void TestValidMoveWhiteKingDiagonal()
        {
            ChessBoard cb = new ChessBoard();
            Move m1 = new Move(new Position(Position.Files.F, Position.Ranks.Two), new Position(Position.Files.F, Position.Ranks.Three), Players.White);
            Move m2 = new Move(new Position(Position.Files.E, Position.Ranks.One), new Position(Position.Files.F, Position.Ranks.Two), Players.White);

            cb.ApplyMove(m1, true);

            Assert.True(cb.IsValidMove(m2), "m2 is considered invalid");
        }

        [Test]
        public void TestValidMoveWhiteKingHorizontal()
        {
            ChessBoard cb = new ChessBoard();
            Move m1 = new Move(new Position(Position.Files.D, Position.Ranks.Two), new Position(Position.Files.D, Position.Ranks.Three), Players.White);
            Move m2 = new Move(new Position(Position.Files.D, Position.Ranks.One), new Position(Position.Files.D, Position.Ranks.Two), Players.White);
            Move m3 = new Move(new Position(Position.Files.E, Position.Ranks.One), new Position(Position.Files.D, Position.Ranks.One), Players.White);

            cb.ApplyMove(m1, true);
            cb.ApplyMove(m2, true);

            Assert.True(cb.IsValidMove(m3), "m3 is considered invalid");
        }

        [Test]
        public void TestValidMoveWhiteKingVertical()
        {
            ChessBoard cb = new ChessBoard();
            Move m1 = new Move(new Position(Position.Files.E, Position.Ranks.Two), new Position(Position.Files.E, Position.Ranks.Three), Players.White);
            Move m2 = new Move(new Position(Position.Files.E, Position.Ranks.One), new Position(Position.Files.E, Position.Ranks.Two), Players.White);

            cb.ApplyMove(m1, true);

            Assert.True(cb.IsValidMove(m2), "m2 is considered invalid");
        }

        [Test]
        public void TestApplyMoveWhitePawn()
        {
            ChessBoard cb = new ChessBoard();
            Move m1 = new Move(new Position(Position.Files.E, Position.Ranks.Two), new Position(Position.Files.E, Position.Ranks.Three), Players.White);
            Assert.True(cb.ApplyMove(m1, false));
            ChessPiece kw = new ChessPiece(Pieces.King, Players.White);
            ChessPiece kb = new ChessPiece(Pieces.King, Players.Black);
            ChessPiece qw = new ChessPiece(Pieces.Queen, Players.White);
            ChessPiece qb = new ChessPiece(Pieces.Queen, Players.Black);
            ChessPiece rw = new ChessPiece(Pieces.Rook, Players.White);
            ChessPiece rb = new ChessPiece(Pieces.Rook, Players.Black);
            ChessPiece nw = new ChessPiece(Pieces.Knight, Players.White);
            ChessPiece nb = new ChessPiece(Pieces.Knight, Players.Black);
            ChessPiece bw = new ChessPiece(Pieces.Bishop, Players.White);
            ChessPiece bb = new ChessPiece(Pieces.Bishop, Players.Black);
            ChessPiece pw = new ChessPiece(Pieces.Pawn, Players.White);
            ChessPiece pb = new ChessPiece(Pieces.Pawn, Players.Black);
            ChessPiece o = ChessPiece.None;
            ChessPiece[,] expected = new ChessPiece[8, 8]
            {
                { rb, nb, bb, qb, kb, bb, nb, rb },
                { pb, pb, pb, pb, pb, pb, pb, pb },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, pw, o, o, o },
                { pw, pw, pw, pw, o, pw, pw, pw },
                { rw, nw, bw, qw, kw, bw, nw, rw }
            };
            Assert.AreEqual(expected, cb.Board, "Unexpected board layout after applying m1");
            Move m2 = new Move(new Position(Position.Files.E, Position.Ranks.Three), new Position(Position.Files.E, Position.Ranks.Four), Players.White);
            Assert.True(cb.ApplyMove(m2, true));
            expected = new ChessPiece[8, 8]
            {
                { rb, nb, bb, qb, kb, bb, nb, rb },
                { pb, pb, pb, pb, pb, pb, pb, pb },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, pw, o, o, o },
                { o, o, o, o, o, o, o, o },
                { pw, pw, pw, pw, o, pw, pw, pw },
                { rw, nw, bw, qw, kw, bw, nw, rw }
            };
            Assert.AreEqual(expected, cb.Board, "Unexpected board layout after applying m2");
        }
    }
}
