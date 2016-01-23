using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChessDotNet.Pieces
{
    public class King : Piece
    {
        public override Player Owner
        {
            get;
            set;
        }

        public bool HasCastlingAbility
        {
            get;
            set;
        }

        public King(Player owner) : this(owner, true) { }

        public King(Player owner, bool hasCastlingAbility)
        {
            Owner = owner;
            HasCastlingAbility = hasCastlingAbility;
        }

        public override char GetFenCharacter()
        {
            return Owner == Player.White ? 'K' : 'k';
        }

        public override bool IsValidMove(Move move, ChessGame game)
        {
            Utilities.ThrowIfNull(move, "move");
            Position origin = move.OriginalPosition;
            Position destination = move.NewPosition;
            PositionDistance distance = new PositionDistance(origin, destination);
            if ((distance.DistanceX != 1 || distance.DistanceY != 1)
                        && (distance.DistanceX != 0 || distance.DistanceY != 1)
                        && (distance.DistanceX != 1 || distance.DistanceY != 0)
                        && (distance.DistanceX != 2 || distance.DistanceY != 0))
                return false;
            if (distance.DistanceX != 2)
                return true;
            return CanCastle(origin, destination, game);
        }

        protected virtual bool CanCastle(Position origin, Position destination, ChessGame game)
        {
            if (!HasCastlingAbility) return false;
            if (Owner == Player.White)
            {
                if (origin.File != File.E || origin.Rank != Rank.One)
                    return false;
                if (game.WhiteKingMoved || (game.Status.Event == GameEvent.Check && game.Status.PlayerWhoCausedEvent == Player.Black))
                    return false;
                if (destination.File == File.C)
                {
                    if (game.WhiteRookAMoved || game.GetPieceAt(File.D, Rank.One) != null
                        || game.GetPieceAt(File.C, Rank.One) != null
                        || game.GetPieceAt(File.B, Rank.One) != null
                        || game.WouldBeInCheckAfter(new Move(new Position(File.E, Rank.One), new Position(File.D, Rank.One), Player.White), Player.White)
                        || game.WouldBeInCheckAfter(new Move(new Position(File.E, Rank.One), new Position(File.C, Rank.One), Player.White), Player.White))
                        return false;
                }
                else
                {
                    if (game.WhiteRookHMoved || game.GetPieceAt(File.F, Rank.One) != null
                        || game.GetPieceAt(File.G, Rank.One) != null
                        || game.WouldBeInCheckAfter(new Move(new Position(File.E, Rank.One), new Position(File.F, Rank.One), Player.White), Player.White)
                        || game.WouldBeInCheckAfter(new Move(new Position(File.E, Rank.One), new Position(File.G, Rank.One), Player.White), Player.White))
                        return false;
                }
            }
            else
            {
                if (origin.File != File.E || origin.Rank != Rank.Eight)
                    return false;
                if (game.BlackKingMoved || (game.Status.Event == GameEvent.Check && game.Status.PlayerWhoCausedEvent == Player.White))
                    return false;
                if (destination.File == File.C)
                {
                    if (game.BlackRookAMoved || game.GetPieceAt(File.D, Rank.Eight) != null
                        || game.GetPieceAt(File.C, Rank.Eight) != null
                        || game.GetPieceAt(File.B, Rank.Eight) != null
                        || game.WouldBeInCheckAfter(new Move(new Position(File.E, Rank.Eight), new Position(File.D, Rank.Eight), Player.Black), Player.Black)
                        || game.WouldBeInCheckAfter(new Move(new Position(File.E, Rank.Eight), new Position(File.C, Rank.Eight), Player.Black), Player.Black))
                        return false;
                }
                else
                {
                    if (game.BlackRookHMoved || game.GetPieceAt(File.F, Rank.Eight) != null
                        || game.GetPieceAt(File.G, Rank.Eight) != null
                        || game.WouldBeInCheckAfter(new Move(new Position(File.E, Rank.Eight), new Position(File.F, Rank.Eight), Player.Black), Player.Black)
                        || game.WouldBeInCheckAfter(new Move(new Position(File.E, Rank.Eight), new Position(File.G, Rank.Eight), Player.Black), Player.Black))
                        return false;
                }
            }
            return true;
        }

        public override float GetRelativePieceValue()
        {
            return float.PositiveInfinity;
        }

        public override ReadOnlyCollection<Move> GetValidMoves(Position from, bool returnIfAny, ChessGame game)
        {
            Utilities.ThrowIfNull(from, "from");
            List<Move> validMoves = new List<Move>();
            Piece piece = game.GetPieceAt(from);
            Piece[][] board = game.GetBoard();
            int l0 = board.Length;
            int l1 = board[0].Length;
            int[][] directions = new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { -1, 0 },
                        new int[] { 1, 1 }, new int[] { 1, -1 }, new int[] { -1, 1 }, new int[] { -1, -1 } };
            foreach (int[] dir in directions)
            {
                if ((int)from.File + dir[0] < 0 || (int)from.File + dir[0] >= l1
                    || (int)from.Rank + dir[1] < 0 || (int)from.Rank + dir[1] >= l0)
                    continue;
                Move move = new Move(from, new Position(from.File + dir[0], from.Rank + dir[1]), piece.Owner);
                if (game.IsValidMove(move))
                {
                    validMoves.Add(move);
                    if (returnIfAny)
                        return new ReadOnlyCollection<Move>(validMoves);
                }
            }
            return new ReadOnlyCollection<Move>(validMoves);
        }
    }
}
