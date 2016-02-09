using System.Collections.Generic;
using ChessDotNet.Pieces;

namespace ChessDotNet.Variants.Atomic
{
    public class AtomicChessGame : ChessGame
    {
        public AtomicChessGame() : base() { }
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
                Rank r = move.NewPosition.Rank + surroundingSquaresDistance[1];
                if (f < 0 || (int)f >= BoardWidth || r < 0 || (int)r >= BoardHeight)
                    continue;
                if (!(GetPieceAt(f, r) is Pawn))
                {
                    SetPieceAt(f, r, null);
                }
            }
            return type;
        }

        protected override GameStatus CalculateStatus(Player playerToValidate, bool validateHasAnyValidMoves)
        {
            bool kingIsGone = KingIsGone(playerToValidate);
            if (kingIsGone)
            {
                return new GameStatus(GameEvent.VariantEnd, Utilities.GetOpponentOf(playerToValidate), "King exploded");
            }
            return base.CalculateStatus(playerToValidate, validateHasAnyValidMoves);
        }

        protected override bool IsValidMove(Move move, bool validateCheck, bool careAboutWhoseTurnItIs)
        {
            Utilities.ThrowIfNull(move, "move");
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

        protected virtual bool KingIsGone(Player player)
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    if (Board[x][y] is King && Board[x][y].Owner == player)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected virtual bool WouldBeSuicide(Move move, Player player)
        {
            Utilities.ThrowIfNull(move, "move");
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
            Utilities.ThrowIfNull(move, "move");
            AtomicChessGame copy = new AtomicChessGame(Board, player);
            copy.ApplyMove(move, true);
            bool ownKingIsGone = copy.KingIsGone(player);
            bool otherKingIsGone = copy.KingIsGone(Utilities.GetOpponentOf(player));
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
                GameStatus status = copy.CalculateStatus(player, false);
                return status.Event == GameEvent.Check && status.PlayerWhoCausedEvent != player;
            }
        }
    }
}
