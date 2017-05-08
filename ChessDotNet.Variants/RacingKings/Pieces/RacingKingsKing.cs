using ChessDotNet.Pieces;

namespace ChessDotNet.Variants.RacingKings.Pieces
{
    public class RacingKingsKing : King
    {
        public RacingKingsKing(Player owner) : base(owner, false) { }

        public override Piece GetWithInvertedOwner()
        {
            return new RacingKingsKing(ChessUtilities.GetOpponentOf(Owner));
        }
    }
}
