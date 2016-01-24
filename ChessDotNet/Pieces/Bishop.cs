using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChessDotNet.Pieces
{
    public class Bishop : Piece
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

        public override char GetFenCharacter()
        {
            return Owner == Player.White ? 'B' : 'b';
        }

        public override bool IsValidMove(Move move, ChessGame game)
        {
            Utilities.ThrowIfNull(move, "move");
            Utilities.ThrowIfNull(game, "game");
            Position origin = move.OriginalPosition;
            Position destination = move.NewPosition;

            PositionDistance posDelta = new PositionDistance(origin, destination);
            if (posDelta.DistanceX != posDelta.DistanceY)
                return false;
            bool increasingRank = (int)destination.Rank > (int)origin.Rank;
            bool increasingFile = (int)destination.File > (int)origin.File;
            for (int f = (int)origin.File + (increasingFile ? 1 : -1), r = (int)origin.Rank + (increasingRank ? 1 : -1);
                 increasingFile ? f < (int)destination.File : f > (int)destination.File;
                 f += increasingFile ? 1 : -1, r += increasingRank ? 1 : -1)
            {
                if (game.GetPieceAt((File)f, (Rank)r) != null)
                {
                    return false;
                }
            }
            return true;
        }

        public override ReadOnlyCollection<Move> GetValidMoves(Position from, bool returnIfAny, ChessGame game)
        {
            List<Move> validMoves = new List<Move>();
            Piece piece = game.GetPieceAt(from);
            int l0 = game.BoardHeight;
            int l1 = game.BoardWidth;
            for (int i = -7; i < 8; i++)
            {
                if (i == 0)
                    continue;
                if ((int)from.Rank + i > -1 && (int)from.Rank + i < l0
                    && (int)from.File + i > -1 && (int)from.File + i < l1)
                {
                    Move move = new Move(from, new Position(from.File + i, from.Rank + i), piece.Owner);
                    if (game.IsValidMove(move))
                    {
                        validMoves.Add(move);
                        if (returnIfAny)
                            return new ReadOnlyCollection<Move>(validMoves);
                    }
                }
                if ((int)from.Rank - i > -1 && (int)from.Rank - i < l0
                    && (int)from.File + i > -1 && (int)from.File + i < l1)
                {
                    Move move = new Move(from, new Position(from.File + i, from.Rank - i), piece.Owner);
                    if (game.IsValidMove(move))
                    {
                        validMoves.Add(move);
                        if (returnIfAny)
                            return new ReadOnlyCollection<Move>(validMoves);
                    }
                }
            }
            return new ReadOnlyCollection<Move>(validMoves);
        }
    }
}
