using System;
using System.Collections.ObjectModel;

namespace ChessDotNet.Pieces
{
    public class Pawn : ChessPiece
    {
        public override Player Owner
        {
            get;
            set;
        }

        public Pawn(Player owner)
        {
            Owner = owner;
        }

        public override string GetFenCharacter()
        {
            return Owner == Player.White ? "P" : "p";
        }

        public override bool IsValidMove(Move move, ChessGame game)
        {
            Utilities.ThrowIfNull(move, "move");
            Utilities.ThrowIfNull(game, "game");
            Position origin = move.OriginalPosition;
            Position destination = move.NewPosition;

            Piece promotion = move.Promotion;
            PositionDistance posDelta = new PositionDistance(origin, destination);
            if ((posDelta.DistanceX != 0 || posDelta.DistanceY != 1) && (posDelta.DistanceX != 1 || posDelta.DistanceY != 1)
                        && (posDelta.DistanceX != 0 || posDelta.DistanceY != 2))
                return false;
            if (Owner == Player.White)
            {
                if ((int)origin.Rank < (int)destination.Rank)
                    return false;
                if (destination.Rank == Rank.Eight && promotion == Piece.None)
                    return false;
            }
            if (Owner == Player.Black)
            {
                if ((int)origin.Rank > (int)destination.Rank)
                    return false;
                if (destination.Rank == Rank.One && promotion == Piece.None)
                    return false;
            }
            bool checkEnPassant = false;
            if (posDelta.DistanceY == 2)
            {
                if ((origin.Rank != Rank.Two && Owner == Player.White)
                    || (origin.Rank != Rank.Seven && Owner == Player.Black))
                    return false;
                if (origin.Rank == Rank.Two && game.GetPieceAt(origin.File, Rank.Three) != null)
                    return false;
                if (origin.Rank == Rank.Seven && game.GetPieceAt(origin.File, Rank.Six) != null)
                    return false;
            }
            if (posDelta.DistanceX == 0 && (posDelta.DistanceY == 1 || posDelta.DistanceY == 2))
            {
                if (game.GetPieceAt(destination).Owner != Player.None)
                    return false;
            }
            else
            {
                if (game.GetPieceAt(destination).Owner != Utilities.GetOpponentOf(Owner))
                    checkEnPassant = true;
                if (game.GetPieceAt(destination).Owner == Owner)
                    return false;
            }
            if (checkEnPassant)
            {
                ReadOnlyCollection<DetailedMove> _moves = game.Moves;
                if (_moves.Count == 0)
                {
                    return false;
                }
                if ((origin.Rank != Rank.Five && Owner == Player.White)
                    || (origin.Rank != Rank.Four && Owner == Player.Black))
                    return false;
                Move latestMove = _moves[_moves.Count - 1];
                if (latestMove.Player != Utilities.GetOpponentOf(Owner))
                    return false;
                if (game.GetPieceAt(latestMove.NewPosition) != null)
                    return false;
                if (Owner == Player.White)
                {
                    if (latestMove.OriginalPosition.Rank != Rank.Seven || latestMove.NewPosition.Rank != Rank.Five)
                        return false;
                }
                else // (m.Player == Players.Black)
                {
                    if (latestMove.OriginalPosition.Rank != Rank.Two || latestMove.NewPosition.Rank != Rank.Four)
                        return false;
                }
                if (destination.File != latestMove.NewPosition.File)
                    return false;
            }
            return true;
        }

        public override float GetRelativePieceValue()
        {
            return 1;
        }
    }
}
