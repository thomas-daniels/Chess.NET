using ChessDotNet.Pieces;
using System;

namespace ChessDotNet.Variants.Horde.Pieces
{
    public class HordePawn : Pawn
    {
        public HordePawn() : base(Player.White) { }
        public HordePawn(Player owner) : base(Player.White)
        {
            if (owner != Player.White)
            {
                throw new ArgumentException("Horde pawns must be white.");
            }
        }

        public override Piece GetWithInvertedOwner()
        {
            if (this is HordePawn)
            {
                return new Pawn(Player.Black);
            }
            else
            {
                return new HordePawn();
            }
        }

        public override bool IsValidMove(Move move, ChessGame game)
        {
            bool validByStandardRules = base.IsValidMove(move, game);
            if (validByStandardRules) return true;
            if (move.OriginalPosition.Rank == 1 &&
                move.NewPosition.Rank == 3 &&
                move.OriginalPosition.File == move.NewPosition.File &&
                game.GetPieceAt(move.NewPosition) == null &&
                game.GetPieceAt(new Position(move.OriginalPosition.File, move.OriginalPosition.Rank + 1)) == null)
                // Horde pawns at the first rank can also move two squares on their first move. However, these can't be en-passant captured.
            {
                return true;
            }

            return false;
        }
    }
}
