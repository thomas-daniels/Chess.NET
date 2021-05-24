using ChessDotNet.Pieces;

namespace ChessDotNet.Variants.Atomar.Pieces
{
    public class AtomarKing : King
    {
        public AtomarKing(Player owner) : base(owner, true) { }

        public override bool IsValidMove(Move move, ChessGame game)
        {
            return !(game.GetPieceAt(move.NewPosition) is King) && base.IsValidMove(move, game);
        }
    }
}
