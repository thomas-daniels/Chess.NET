using System.Collections.Generic;

namespace ChessDotNet
{
    public class ChessBoard
    {
        public ChessPiece[,] Board
        {
            get;
            private set;
        }

        public List<Move> Moves
        {
            get;
            private set;
        }

        public ChessBoard()
        {
            Board = new ChessPiece[8, 8];
            Moves = new List<Move>();
            InitBoard();
        }

        public ChessBoard(ChessPiece[,] board, List<Move> moves)
        {
            Board = (ChessPiece[,])board.Clone();
            Moves = new List<Move>(moves);

        }

        public void InitBoard()
        {
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
            Board = new ChessPiece[8, 8]
            {
                { rb, nb, bb, qb, kb, bb, nb, rb },
                { pb, pb, pb, pb, pb, pb, pb, pb },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, o, o, o, o },
                { pw, pw, pw, pw, pw, pw, pw, pw },
                { rw, nw, bw, qw, kw, bw, nw, rw }
            };
        }

        public ChessPiece GetPieceAt(Position p)
        {
            return GetPieceAt(p.File, p.Rank);
        }

        public ChessPiece GetPieceAt(Position.Files file, Position.Ranks rank)
        {
            return Board[(int)rank, (int)file];
        }

        protected void SetPieceAt(Position.Files file, Position.Ranks rank, ChessPiece cp)
        {
            Board[(int)rank, (int)file] = cp;
        }

        public bool IsValidMove(Move m)
        {
            return IsValidMove(m, true);
        }

        public bool IsValidMove(Move m, bool validateCheck)
        {
            if (m.OriginalPosition.Equals(m.NewPosition))
                return false;
            ChessPiece piece = GetPieceAt(m.OriginalPosition.File, m.OriginalPosition.Rank);
            if (piece.Player != m.Player) return false;
            if (GetPieceAt(m.NewPosition).Player == m.Player)
            {
                return false;
            }
            PositionDelta posDelta = new PositionDelta(m.OriginalPosition, m.NewPosition);
            switch (piece.Piece)
            {
                case Pieces.King:
                    if ((posDelta.DeltaX != 1 || posDelta.DeltaY != 1)
                        && (posDelta.DeltaX != 0 || posDelta.DeltaY != 1)
                        && (posDelta.DeltaX != 1 || posDelta.DeltaY != 0))
                        return false; // TODO: take castling in account
                    foreach (int[] p in new int[][] { new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { -1, 0 }, new int[] { 0, -1 },
                        new int[] { 1, 1 }, new int[] { 1, -1 }, new int[] { -1, 1 }, new int[] { -1, -1 } })
                    {
                        int r = (int)m.NewPosition.Rank + p[0];
                        int f = (int)m.NewPosition.File + p[1];
                        if (r < 0 || r >= Board.GetLength(0) || f < 0 || f >= Board.GetLength(1))
                        {
                            continue;
                        }
                        ChessPiece cp = Board[r, f];
                        if (cp.Piece == Pieces.King && cp.Player != m.Player)
                        {
                            return false;
                        }
                    }
                    break;
                case Pieces.Pawn:
                    if ((posDelta.DeltaX != 0 || posDelta.DeltaY != 1) && (posDelta.DeltaX != 1 || posDelta.DeltaY != 1)
                        && (posDelta.DeltaX != 0 || posDelta.DeltaY != 2))
                        return false;
                    if (piece.Player == Players.White && (int)m.OriginalPosition.Rank < (int)m.NewPosition.Rank)
                        return false;
                    if (piece.Player == Players.Black && (int)m.OriginalPosition.Rank > (int)m.NewPosition.Rank)
                        return false;
                    if (posDelta.DeltaY == 2)
                    {
                        if ((m.OriginalPosition.Rank != Position.Ranks.Two && m.Player == Players.White)
                            || (m.OriginalPosition.Rank != Position.Ranks.Seven && m.Player == Players.Black))
                            return false;
                        if (m.OriginalPosition.Rank == Position.Ranks.Two && GetPieceAt(m.OriginalPosition.File, Position.Ranks.Three).Piece != Pieces.None)
                            return false;
                        if (m.OriginalPosition.Rank == Position.Ranks.Seven && GetPieceAt(m.OriginalPosition.File, Position.Ranks.Six).Piece != Pieces.None)
                            return false;
                    }
                    if (posDelta.DeltaX == 0 && (posDelta.DeltaY == 1 || posDelta.DeltaY == 2))
                    {
                        if (GetPieceAt(m.NewPosition).Player != Players.None)
                            return false;
                    }
                    else
                    {
                        if (GetPieceAt(m.NewPosition).Player != (m.Player == Players.White ? Players.Black : Players.White))
                            return false;
                    }
                    // TODO: take en passant in account
                    break;
                case Pieces.Queen:
                    if (posDelta.DeltaX != posDelta.DeltaY && posDelta.DeltaX != 0 && posDelta.DeltaY != 0)
                        return false;
                    bool increasingRank = (int)m.NewPosition.Rank > (int)m.OriginalPosition.Rank;
                    bool increasingFile = (int)m.NewPosition.File > (int)m.OriginalPosition.File;
                    if (posDelta.DeltaX == posDelta.DeltaY)
                    {
                        for (int f = (int)m.OriginalPosition.File + (increasingFile ? 1 : -1), r = (int)m.OriginalPosition.Rank + (increasingRank ? 1 : -1);
                            increasingFile ? f < (int)m.NewPosition.File : f > (int)m.NewPosition.File;
                            f += increasingFile ? 1 : -1, r += increasingRank ? 1 : -1)
                        {
                            if (Board[r, f].Player != Players.None)
                            {
                                return false;
                            }
                        }
                    }
                    else if (posDelta.DeltaX == 0)
                    {
                        int f = (int)m.OriginalPosition.File;
                        for (int r = (int)m.OriginalPosition.Rank + (increasingRank ? 1 : -1);
                            increasingRank ? r < (int)m.NewPosition.Rank : r > (int)m.NewPosition.Rank;
                            r += increasingRank ? 1 : -1)
                        {
                            if (Board[r, f].Player != Players.None)
                            {
                                return false;
                            }
                        }
                    }
                    else // (posDelta.DeltaY == 0)
                    {
                        int r = (int)m.OriginalPosition.Rank;
                        for (int f = (int)m.OriginalPosition.File + (increasingFile ? 1 : -1);
                            increasingFile ? f < (int)m.NewPosition.File : f > (int)m.NewPosition.File;
                            f += increasingFile ? 1 : -1)
                        {
                            if (Board[r, f].Player != Players.None)
                            {
                                return false;
                            }
                        }
                    }
                    break;
                case Pieces.Rook:
                    if (posDelta.DeltaX != 0 && posDelta.DeltaY != 0)
                        return false;
                    increasingRank = (int)m.NewPosition.Rank > (int)m.OriginalPosition.Rank;
                    increasingFile = (int)m.NewPosition.File > (int)m.OriginalPosition.File;
                    if (posDelta.DeltaX == 0)
                    {
                        int f = (int)m.OriginalPosition.File;
                        for (int r = (int)m.OriginalPosition.Rank + (increasingRank ? 1 : -1);
                            increasingRank ? r < (int)m.NewPosition.Rank : r > (int)m.NewPosition.Rank;
                            r += increasingRank ? 1 : -1)
                        {
                            if (Board[r, f].Player != Players.None)
                            {
                                return false;
                            }
                        }
                    }
                    else // (posDelta.DeltaY == 0)
                    {
                        int r = (int)m.OriginalPosition.Rank;
                        for (int f = (int)m.OriginalPosition.File + (increasingFile ? 1 : -1);
                            increasingFile ? f < (int)m.NewPosition.File : f > (int)m.NewPosition.File;
                            f += increasingFile ? 1 : -1)
                        {
                            if (Board[r, f].Player != Players.None)
                            {
                                return false;
                            }
                        }
                    }
                    break;
                case Pieces.Bishop:
                    if (posDelta.DeltaX != posDelta.DeltaY)
                        return false;
                    increasingRank = (int)m.NewPosition.Rank > (int)m.OriginalPosition.Rank;
                    increasingFile = (int)m.NewPosition.File > (int)m.OriginalPosition.File;
                    for (int f = (int)m.OriginalPosition.File + (increasingFile ? 1 : -1), r = (int)m.OriginalPosition.Rank + (increasingRank ? 1 : -1);
                         increasingFile ? f < (int)m.NewPosition.File : f > (int)m.NewPosition.File;
                         f += increasingFile ? 1 : -1, r += increasingRank ? 1 : -1)
                    {
                        if (Board[r, f].Player != Players.None)
                        {
                            return false;
                        }
                    }
                    break;
                case Pieces.Knight:
                    if ((posDelta.DeltaX != 2 || posDelta.DeltaY != 1) && (posDelta.DeltaX != 1 || posDelta.DeltaY != 2))
                        return false;
                    break;
                default:
                    return false;
            }
            if (validateCheck && WouldBeInCheckAfter(m, m.Player))
            {
                return false;
            }

            return true;
        }

        public bool ApplyMove(Move m, bool alreadyValidated)
        {
            if (!alreadyValidated && !IsValidMove(m))
                return false;
            ChessPiece movingPiece = GetPieceAt(m.OriginalPosition.File, m.OriginalPosition.Rank);
            SetPieceAt(m.NewPosition.File, m.NewPosition.Rank, movingPiece);
            SetPieceAt(m.OriginalPosition.File, m.OriginalPosition.Rank, ChessPiece.None);
            return true;
        }

        public bool IsInCheck(Players player)
        {
            List<Position> piecePositions = new List<Position>();
            Position kingPos = new Position(Position.Files.None, Position.Ranks.None);

            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    ChessPiece curr = Board[i, j];
                    if (curr.Piece != Pieces.None && curr.Piece != Pieces.King && curr.Player == (player == Players.White ? Players.Black : Players.White))
                    {
                        piecePositions.Add(new Position((Position.Files)j, (Position.Ranks)i));
                    }
                    else if (curr.Piece == Pieces.King && curr.Player == player)
                    {
                        kingPos = new Position((Position.Files)j, (Position.Ranks)i);
                    }
                }
            }

            for (int i = 0; i < piecePositions.Count; i++)
            {
                if (IsValidMove(new Move(piecePositions[i], kingPos, player == Players.White ? Players.Black : Players.White), false))
                {
                    return true;
                }
            }

            return false;
        }

        protected bool WouldBeInCheckAfter(Move m, Players player)
        {
            ChessBoard copy = new ChessBoard(Board, Moves);
            copy.ApplyMove(m, true);
            return copy.IsInCheck(player);
        }
    }
}
