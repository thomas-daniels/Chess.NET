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

        public override bool IsValidDestination(Position origin, Position destination, ChessGame game)
        {
            Utilities.ThrowIfNull(origin, "origin");
            Utilities.ThrowIfNull(destination, "destination");
            PositionDistance posDelta = new PositionDistance(origin, destination);
            if ((posDelta.DistanceX != 2 || posDelta.DistanceY != 1) && (posDelta.DistanceX != 1 || posDelta.DistanceY != 2))
                return false;
            return true;
        }
    }
}
