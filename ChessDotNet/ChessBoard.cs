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
            if (m.OriginalPosition.Equals(m.NewPosition))
                return false;
            ChessPiece piece = GetPieceAt(m.OriginalPosition.File, m.OriginalPosition.Rank);
            if (piece.Player != m.Player) return false;
            PositionDelta posDelta = new PositionDelta(m.OriginalPosition, m.NewPosition);
            switch (piece.Piece)
            {
                case Pieces.King:
                    if ((posDelta.DeltaX != 1 || posDelta.DeltaY != 1)
                        && (posDelta.DeltaX != 0 || posDelta.DeltaY != 1)
                        && (posDelta.DeltaX != 1 || posDelta.DeltaY != 0))
                        return false; // TODO: take castling in account
                    break;
                case Pieces.Pawn:
                    if (posDelta.DeltaX != 0 || posDelta.DeltaY != 1)
                        return false;
                    if (piece.Player == Players.White && (int)m.OriginalPosition.Rank < (int)m.NewPosition.Rank)
                        return false;
                    if (piece.Player == Players.Black && (int)m.OriginalPosition.Rank > (int)m.NewPosition.Rank)
                        return false;
                    // TODO: take capturing in account
                    break;
                case Pieces.Queen:
                    if (posDelta.DeltaX != posDelta.DeltaY && posDelta.DeltaX != 0 && posDelta.DeltaY != 0)
                        return false;
                    break;
                case Pieces.Rook:
                    if (posDelta.DeltaX != 0 && posDelta.DeltaY != 0)
                        return false;
                    break;
                case Pieces.Bishop:
                    if (posDelta.DeltaX != posDelta.DeltaY)
                        return false;
                    break;
                case Pieces.Knight:
                    if ((posDelta.DeltaX != 2 || posDelta.DeltaY != 1) && (posDelta.DeltaX != 1 || posDelta.DeltaY != 2))
                        return false;
                    break;
                default:
                    return false;
            }
            // TODO: Validate that you are not going to put yourself in check
            // TODO: Validate that other pieces are not in the way to do your move
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
    }
}
