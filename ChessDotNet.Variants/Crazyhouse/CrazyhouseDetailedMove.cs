using System;

namespace ChessDotNet.Variants.Crazyhouse
{
    public class CrazyhouseDetailedMove : DetailedMove
    {
        public bool IsDrop { get; private set; }

        Drop _drop;
        public Drop Drop
        {
            get
            {
                if (IsDrop) return _drop;
                else throw new InvalidOperationException("Move is not a drop.");
            }
        }

        public CrazyhouseDetailedMove(DetailedMove move) : base(new Move(move.OriginalPosition, move.NewPosition, move.Player), move.Piece, move.IsCapture, move.Castling) { IsDrop = false; }

        public CrazyhouseDetailedMove(Drop drop)
        {
            IsDrop = true;
            _drop = drop;
            Player = drop.Player;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            CrazyhouseDetailedMove move = (CrazyhouseDetailedMove)obj;
            if (move.IsDrop != IsDrop) return false;

            if (!move.IsDrop)
            {
                return OriginalPosition.Equals(move.OriginalPosition)
                    && NewPosition.Equals(move.NewPosition)
                    && Player == move.Player
                    && Promotion == move.Promotion
                    && Piece == move.Piece
                    && IsCapture == move.IsCapture
                    && Castling == move.Castling;
            }
            else
            {
                return Player == move.Player && Drop.Equals(move.Drop);
            }
        }

        public override int GetHashCode()
        {
            if (!IsDrop)
            {
                return new { OriginalPosition, NewPosition, Player, Promotion, Piece, IsCapture, Castling }.GetHashCode();
            }
            else
            {
                return new { Player, Drop }.GetHashCode();
            }
        }

        public static bool operator ==(CrazyhouseDetailedMove move1, CrazyhouseDetailedMove move2)
        {
            if (ReferenceEquals(move1, move2))
                return true;
            if ((object)move1 == null || (object)move2 == null)
                return false;
            return move1.Equals(move2);
        }

        public static bool operator !=(CrazyhouseDetailedMove move1, CrazyhouseDetailedMove move2)
        {
            if (ReferenceEquals(move1, move2))
                return false;
            if ((object)move1 == null || (object)move2 == null)
                return true;
            return !move1.Equals(move2);
        }
    }
}
