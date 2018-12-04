using ChessDotNet.Pieces;
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

        public CrazyhouseDetailedMove(DetailedMove move) : base(new Move(move.OriginalPosition, move.NewPosition, move.Player), move.Piece, move.IsCapture, move.Castling, move.SAN) { IsDrop = false; }

        public CrazyhouseDetailedMove(Drop drop, string san)
        {
            IsDrop = true;
            _drop = drop;
            Player = drop.Player;
            SAN = san;
        }
    }
}
