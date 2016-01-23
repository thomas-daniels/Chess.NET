using System;

namespace ChessDotNet.Pieces
{
    public class Knight : ChessPiece
    {
        public override Player Owner
        {
            get;
            set;
        }

        public Knight(Player owner)
        {
            Owner = owner;
        }

        public override string GetFenCharacter()
        {
            return Owner == Player.White ? "N" : "n";
        }

        public override bool IsValidMove(Move move, ChessGame game)
        {
            Utilities.ThrowIfNull(move, "move");
            Utilities.ThrowIfNull(game, "game");
            Position origin = move.OriginalPosition;
            Position destination = move.NewPosition;

            PositionDistance posDelta = new PositionDistance(origin, destination);
            if ((posDelta.DistanceX != 2 || posDelta.DistanceY != 1) && (posDelta.DistanceX != 1 || posDelta.DistanceY != 2))
                return false;
            return true;
        }

        public override float GetRelativePieceValue()
        {
            return 3;
        }
    }
}
