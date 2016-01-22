namespace ChessDotNet.Pieces
{
    public class Bishop : ChessPiece
    {
        public override Player Owner
        {
            get;
            set;
        }

        public Bishop(Player owner)
        {
            Owner = owner;
        }

        public override string GetFenCharacter()
        {
            return Owner == Player.White ? "B" : "b";
        }

        public override bool IsValidDestination(Position origin, Position destination, ChessGame game)
        {
            Utilities.ThrowIfNull(origin, "origin");
            Utilities.ThrowIfNull(destination, "destination");
            PositionDistance posDelta = new PositionDistance(origin, destination);
            if (posDelta.DistanceX != posDelta.DistanceY)
                return false;
            bool increasingRank = (int)destination.Rank > (int)origin.Rank;
            bool increasingFile = (int)destination.File > (int)origin.File;
            for (int f = (int)origin.File + (increasingFile ? 1 : -1), r = (int)origin.Rank + (increasingRank ? 1 : -1);
                 increasingFile ? f < (int)destination.File : f > (int)destination.File;
                 f += increasingFile ? 1 : -1, r += increasingRank ? 1 : -1)
            {
                if (game.GetPieceAt((File)f, (Rank)r).Owner != Player.None)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
