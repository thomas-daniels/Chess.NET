using ChessDotNet.Pieces;

namespace ChessDotNet.Variants.Antichess.Pieces
{
    public class AntichessPawn : Pawn
    {
        protected override char[] ValidPromotionPieces
        {
            get
            {
                return new char[] { 'Q', 'q', 'R', 'r', 'B', 'b', 'N', 'n', 'K', 'k' };
            }
        }
        public AntichessPawn(Player owner) : base(owner) { }

        public override Piece GetWithInvertedOwner()
        {
            return new AntichessPawn(ChessUtilities.GetOpponentOf(Owner));
        }
    }
}
