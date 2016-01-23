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

        public override bool IsValidMove(Position origin, Position destination, ChessGame game)
        {
            return new Bishop(Owner).IsValidMove(origin, destination, game) || new Rook(Owner).IsValidMove(origin, destination, game);
        }
    }
}
