using System.Collections.Generic;
using ChessDotNet.Pieces;

namespace ChessDotNet.Variants.Atomic
{
    public class AtomicChessGame : ChessGame
    {
        public AtomicChessGame() : base() { }
        public AtomicChessGame(Piece[][] board, Player whoseTurn) : base(board, whoseTurn) { }
        public AtomicChessGame(GameCreationData data) : base(data) { }
        public AtomicChessGame(string fen) : base(fen) { }
        public AtomicChessGame(IEnumerable<Move> moves, bool movesAreValidated) : base(moves, movesAreValidated) { }

        public override MoveType ApplyMove(Move move, bool alreadyValidated)
        {
            MoveType type = base.ApplyMove(move, alreadyValidated);
            if (!type.HasFlag(MoveType.Capture))
                return type;
            int[][] surroundingSquares = new int[][] { new int[] { 1, 0 }, new int[] { 1, 1 }, new int[] { 0, 1 }, new int[] { -1, -1 },
                                                       new int[] { -1, 0 }, new int[] { 0, -1 }, new int[] { -1, 1 }, new int[] { 1, -1 } };
            SetPieceAt(move.NewPosition.File, move.NewPosition.Rank, null);
            foreach (int[] surroundingSquaresDistance in surroundingSquares)
            {
                File f = move.NewPosition.File + surroundingSquaresDistance[0];
                int r = move.NewPosition.Rank + surroundingSquaresDistance[1];
                if (f < 0 || (int)f >= BoardWidth || r < 1 || r > BoardHeight)
                    continue;
                if (!(GetPieceAt(f, r) is Pawn))
                {
                    SetPieceAt(f, r, null);
                }
            }
            return type;
        }

        protected override bool IsValidMove(Move move, bool validateCheck, bool careAboutWhoseTurnItIs)
        {
            ChessUtilities.ThrowIfNull(move, "move");
            if (move.OriginalPosition.Equals(move.NewPosition))
                return false;
            Piece piece = GetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank);
            if (careAboutWhoseTurnItIs && move.Player != WhoseTurn) return false;
            if (piece.Owner != move.Player) return false;
            Piece pieceAtDestination = GetPieceAt(move.NewPosition);
            if (pieceAtDestination != null && (pieceAtDestination.Owner == move.Player || piece is King))
            {
                return false;
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

        Cache<bool> kingIsGoneCacheWhite = new Cache<bool>(false, -1);
        Cache<bool> kingIsGoneCacheBlack = new Cache<bool>(false, -1);
        public virtual bool KingIsGone(Player player)
        {
            Cache<bool> cache = player == Player.White ? kingIsGoneCacheWhite : kingIsGoneCacheBlack;
            if (cache.CachedAt == Moves.Count)
            {
                return cache.Value;
            }

            for (int f = 0; f < BoardWidth; f++)
            {
                for (int r = 1; r <= BoardHeight; r++)
                {
                    Piece p = GetPieceAt((File)f, r);
                    if (p is King && p.Owner == player)
                    {
                        return cache.UpdateCache(false, Moves.Count);
                    }
                }
            }
            return cache.UpdateCache(true, Moves.Count);
        }

        protected virtual bool WouldBeSuicide(Move move, Player player)
        {
            ChessUtilities.ThrowIfNull(move, "move");
            AtomicChessGame copy = new AtomicChessGame(Board, player);
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
            ChessUtilities.ThrowIfNull(move, "move");
            AtomicChessGame copy = new AtomicChessGame(Board, player);
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
    }
}
