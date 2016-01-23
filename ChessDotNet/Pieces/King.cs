using System;

namespace ChessDotNet.Pieces
{
    public class King : ChessPiece
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

        public override string GetFenCharacter()
        {
            return Owner == Player.White ? "K" : "k";
        }

        public override bool IsValidMove(Position origin, Position destination, ChessGame game)
        {
            Utilities.ThrowIfNull(origin, "origin");
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
    }
}
