using System;

namespace ChessDotNet
{
    public struct PositionDelta
    {
        public int DeltaX
        {
            get;
            private set;
        }

        public int DeltaY
        {
            get;
            private set;
        }
        public PositionDelta(Position pos1, Position pos2)
        {
            DeltaX = Math.Abs((int)pos1.File - (int)pos2.File);
            DeltaY = Math.Abs((int)pos1.Rank - (int)pos2.Rank);
        }
    }
}
