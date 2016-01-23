using System;

namespace ChessDotNet.Pieces
{
    public class Rook : ChessPiece
    {
        public override Player Owner
        {
            get;
            set;
        }

        public Rook(Player owner)
        {
            Owner = owner;
        }

        public override char GetFenCharacter()
        {
            return Owner == Player.White ? 'R' : 'r';
        }

        public override bool IsValidMove(Move move, ChessGame game)
        {
            Utilities.ThrowIfNull(move, "move");
            Utilities.ThrowIfNull(game, "game");
            Position origin = move.OriginalPosition;
            Position destination = move.NewPosition;

            PositionDistance posDelta = new PositionDistance(origin, destination);
            if (posDelta.DistanceX != 0 && posDelta.DistanceY != 0)
                return false;
            bool increasingRank = (int)destination.Rank > (int)origin.Rank;
            bool increasingFile = (int)destination.File > (int)origin.File;
            if (posDelta.DistanceX == 0)
            {
                int f = (int)origin.File;
                for (int r = (int)origin.Rank + (increasingRank ? 1 : -1);
                    increasingRank ? r < (int)destination.Rank : r > (int)destination.Rank;
                    r += increasingRank ? 1 : -1)
                {
                    if (game.GetPieceAt((File)f, (Rank)r).Owner != Player.None)
                    {
                        return false;
                    }
                }
            }
            else // (posDelta.DeltaY == 0)
            {
                int r = (int)origin.Rank;
                for (int f = (int)origin.File + (increasingFile ? 1 : -1);
                    increasingFile ? f < (int)destination.File : f > (int)destination.File;
                    f += increasingFile ? 1 : -1)
                {
                    if (game.GetPieceAt((File)f, (Rank)r).Owner != Player.None)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override float GetRelativePieceValue()
        {
            return 5;
        }
    }
}
