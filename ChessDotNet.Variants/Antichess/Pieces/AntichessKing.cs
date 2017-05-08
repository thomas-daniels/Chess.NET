using ChessDotNet.Pieces;

namespace ChessDotNet.Variants.Antichess.Pieces
{
    public class AntichessKing : King
    {
        public AntichessKing(Player owner) : base(owner, false) { }

        public override Piece GetWithInvertedOwner()
        {
            return new AntichessKing(ChessUtilities.GetOpponentOf(Owner));
        }
    }
}
