using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChessDotNet.Pieces
{
    public class Pawn : Piece
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

        public override char GetFenCharacter()
        {
            return Owner == Player.White ? 'P' : 'p';
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
                if (destination.Rank == Rank.Eight && promotion == null)
                    return false;
            }
            if (Owner == Player.Black)
            {
                if ((int)origin.Rank > (int)destination.Rank)
                    return false;
                if (destination.Rank == Rank.One && promotion == null)
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
            Piece pieceAtDestination = game.GetPieceAt(destination);
            if (posDelta.DistanceX == 0 && (posDelta.DistanceY == 1 || posDelta.DistanceY == 2))
            {
                if (pieceAtDestination != null)
                    return false;
            }
            else
            {
                if (pieceAtDestination == null)
                    checkEnPassant = true;
                else if (pieceAtDestination.Owner == Owner)
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
                if (game.GetPieceAt(latestMove.NewPosition).Owner == Owner)
                    return false;
                if (!(game.GetPieceAt(latestMove.NewPosition) is Pawn))
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

        public override ReadOnlyCollection<Move> GetValidMoves(Position from, bool returnIfAny, ChessGame game)
        {
            Utilities.ThrowIfNull(from, "from");
            List<Move> validMoves = new List<Move>();
            Piece piece = game.GetPieceAt(from);
            Piece[][] board = game.GetBoard();
            int l0 = board.Length;
            int l1 = board[0].Length;
            int[][] directions;
            if (piece.Owner == Player.Black)
            {
                directions = new int[][] { new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 1 }, new int[] { -1, 1 } };
            }
            else
            {
                directions = new int[][] { new int[] { 0, -1 }, new int[] { 0, -2 }, new int[] { -1, -1 }, new int[] { 1, -1 } };
            }
            foreach (int[] dir in directions)
            {
                if ((int)from.File + dir[0] < 0 || (int)from.File + dir[0] >= l1
                    || (int)from.Rank + dir[1] < 0 || (int)from.Rank + dir[1] >= l0)
                    continue;
                Move move = new Move(from, new Position(from.File + dir[0], from.Rank + dir[1]), piece.Owner);
                List<Move> moves = new List<Move>();
                if ((move.NewPosition.Rank == Rank.Eight && move.Player == Player.White) || (move.NewPosition.Rank == Rank.One && move.Player == Player.Black))
                {
                    moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, new Queen(move.Player)));
                    moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, new Rook(move.Player)));
                    moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, new Knight(move.Player)));
                    moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, new Bishop(move.Player)));
                }
                else
                {
                    moves.Add(move);
                }
                foreach (Move m in moves)
                {
                    if (game.IsValidMove(m))
                    {
                        validMoves.Add(m);
                        if (returnIfAny)
                            return new ReadOnlyCollection<Move>(validMoves);
                    }
                }
            }
            return new ReadOnlyCollection<Move>(validMoves);
        }
    }
}
