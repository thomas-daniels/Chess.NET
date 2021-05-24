using System;
using System.Collections.Generic;
using ChessDotNet.Pieces;
using ChessDotNet.Variants.Atomar.Pieces;

namespace ChessDotNet.Variants.Atomar
{
    public class AtomarChessGame : ChessGame
    {

        private Dictionary<char, Piece> fenMappings = new Dictionary<char, Piece>()
        {
            { 'K', new AtomarKing(Player.White) },
            { 'k', new AtomarKing(Player.Black) },
            { 'Q', new Queen(Player.White) },
            { 'q', new Queen(Player.Black) },
            { 'R', new Rook(Player.White) },
            { 'r', new Rook(Player.Black) },
            { 'B', new Bishop(Player.White) },
            { 'b', new Bishop(Player.Black) },
            { 'N', new Knight(Player.White) },
            { 'n', new Knight(Player.Black) },
            { 'P', new Pawn(Player.White) },
            { 'p', new Pawn(Player.Black) },
        };

        protected override Dictionary<char, Piece> FenMappings
        {
            get
            {
                return fenMappings;
            }
        }

        public AtomarChessGame() : base() { }
        public AtomarChessGame(Piece[][] board, Player whoseTurn) : base(board, whoseTurn) { }
        public AtomarChessGame(GameCreationData data) : base(data) { }
        public AtomarChessGame(string fen) : base(fen) { }
        public AtomarChessGame(IEnumerable<Move> moves, bool movesAreValidated) : base(moves, movesAreValidated) { }

        public override bool DrawCanBeClaimed
        {
            get
            {
                return base.DrawCanBeClaimed && !KingIsGone(WhoseTurn);
            }
        }

        protected override MoveType ApplyMove(Move move, bool alreadyValidated, out Piece captured, out CastlingType castlingType)
        {
            MoveType type = base.ApplyMove(move, alreadyValidated, out captured, out castlingType);
            if (!type.HasFlag(MoveType.Capture))
                return type;
            var surroundingSquares = new int[][] { new int[] { 1, 0 }, new int[] { 1, 1 }, new int[] { 0, 1 }, new int[] { -1, -1 },
                                                       new int[] { -1, 0 }, new int[] { 0, -1 }, new int[] { -1, 1 }, new int[] { 1, -1 } };
            if (!(GetPieceAt(move.NewPosition) is King))
            {
                SetPieceAt(move.NewPosition.File, move.NewPosition.Rank, null);
            }
            foreach (int[] surroundingSquaresDistance in surroundingSquares)
            {
                File f = move.NewPosition.File + surroundingSquaresDistance[0];
                int r = move.NewPosition.Rank + surroundingSquaresDistance[1];
                if (f < 0 || (int)f >= BoardWidth || r < 1 || r > BoardHeight)
                    continue;
                if (!(GetPieceAt(f, r) is Pawn) && !(GetPieceAt(f, r) is King))
                {
                    SetPieceAt(f, r, null);
                }
            }

            if (CanBlackCastleKingSide && GetPieceAt(InitialBlackRookFileKingsideCastling, 8) == null)
            {
                CanBlackCastleKingSide = false;
            }
            if (CanBlackCastleQueenSide && GetPieceAt(InitialBlackRookFileQueensideCastling, 8) == null)
            {
                CanBlackCastleQueenSide = false;
            }
            if (CanWhiteCastleKingSide && GetPieceAt(InitialWhiteRookFileKingsideCastling, 1) == null)
            {
                CanWhiteCastleKingSide = false;
            }
            if (CanWhiteCastleQueenSide && GetPieceAt(InitialWhiteRookFileQueensideCastling, 1) == null)
            {
                CanWhiteCastleQueenSide = false;
            }

            return type;
        }

        public Position FindKing(Player player)
        {
            for (int f = 0; f < BoardWidth; f++)
            {
                for (int r = 1; r <= BoardHeight; r++)
                {
                    Piece p = GetPieceAt((File)f, r);
                    if (p is King && p.Owner == player)
                    {
                        return new Position((File)f, r);
                    }
                }
            }
            return null;
        }

        protected override bool IsValidMove(Move move, bool validateCheck, bool careAboutWhoseTurnItIs)
        {
            ChessUtilities.ThrowIfNull(move, nameof(move));
            if (move.OriginalPosition.Equals(move.NewPosition))
                return false;
            Piece piece = GetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank);
            if (careAboutWhoseTurnItIs && move.Player != WhoseTurn) return false;
            if (piece == null || piece.Owner != move.Player) return false;
            Piece pieceAtDestination = GetPieceAt(move.NewPosition);
            if (pieceAtDestination != null)
            {
                if (pieceAtDestination.Owner == move.Player && !(piece is King && pieceAtDestination is Rook))
                {
                    return false;
                }
            }
            if (!piece.IsValidMove(move, this))
            {
                return false;
            }
            if (validateCheck && WouldBeSuicideOrInvalidSelfMoveInCheck(move, move.Player))
            {
                return false;
            }
            else if (WouldBeSuicide(move, move.Player))
            {
                return false;
            }

            return true;
        }

        public virtual bool KingIsGone(Player player)
        {
            for (int f = 0; f < BoardWidth; f++)
            {
                for (int r = 1; r <= BoardHeight; r++)
                {
                    Piece p = GetPieceAt((File)f, r);
                    if (p is King && p.Owner == player)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override bool IsDraw()
        {
            return !KingIsGone(Player.White) && !KingIsGone(Player.Black) && base.IsDraw();
        }

        public override bool IsWinner(Player player)
        {
            return IsCheckmated(ChessUtilities.GetOpponentOf(player)) || KingIsGone(ChessUtilities.GetOpponentOf(player));
        }

        protected virtual bool WouldBeSuicide(Move move, Player player)
        {
            ChessUtilities.ThrowIfNull(move, nameof(move));
            var copy = new AtomarChessGame(Board, player);
            copy.ApplyMove(move, true);
            bool ownKingIsGone = copy.KingIsGone(player);
            if (ownKingIsGone)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected virtual bool WouldBeSuicideOrInvalidSelfMoveInCheck(Move move, Player player)
        {
            ChessUtilities.ThrowIfNull(move, nameof(move));
            var copy = new AtomarChessGame(Board, player);
            copy.ApplyMove(move, true);
            bool ownKingIsGone = copy.KingIsGone(player);
            bool otherKingIsGone = copy.KingIsGone(ChessUtilities.GetOpponentOf(player));
            if (ownKingIsGone)
            {
                return true;
            }
            else if (otherKingIsGone)
            {
                return false;
            }
            else
            {
                return copy.IsInCheck(player);
            }
        }

        public override bool Undo()
        {
            throw new NotImplementedException("Undo not yet implemented for atomic.");
        }

        public override bool IsInCheck(Player player)
        {
            return false;
        }

        public override bool WouldBeInCheckAfter(Move move, Player player)
        {
            return false;
        }
    }
}
