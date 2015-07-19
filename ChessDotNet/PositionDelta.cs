using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public PositionDelta(Position p1, Position p2)
        {
            DeltaX = Math.Abs((int)p1.File - (int)p2.File);
            DeltaY = Math.Abs((int)p1.Rank - (int)p2.Rank);
        }
    }
}
