namespace ChessDotNet.Variants.Atomic
{
    using Pieces;

    public class AtomicChessGame : ChessGame
    {
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
                if (!(GetPieceAt(f, r) is Pawn))
                {
                    SetPieceAt(f, r, null);
                }
            }
            return type;
        }
    }
}
