using System;

namespace ChessDotNet.Pieces
{
    public class Queen : ChessPiece
    {
        public override Player Owner
        {
            get;
            set;
        }

        public Queen(Player owner)
        {
            Owner = owner;
        }

        public override string GetFenCharacter()
        {
            return Owner == Player.White ? "Q" : "q";
        }

        public override bool IsValidMove(Move move, ChessGame game)
        {
            Utilities.ThrowIfNull(move, "move");
            Utilities.ThrowIfNull(game, "game");
            return new Bishop(Owner).IsValidMove(move, game) || new Rook(Owner).IsValidMove(move, game);
        }

        public override float GetRelativePieceValue()
        {
            return 9;
        }
    }
}
