using System;

namespace ChessDotNet
{
    public struct PositionDistance
    {
        public int DistanceX
        {
            get;
            private set;
        }

        public int DistanceY
        {
            get;
            private set;
        }
        public PositionDistance(Position position1, Position position2)
        {
            if (position1 == null)
                throw new ArgumentNullException("position1");
            if (position2 == null)
                throw new ArgumentNullException("position2");
            DistanceX = Math.Abs((int)position1.File - (int)position2.File);
            DistanceY = Math.Abs((int)position1.Rank - (int)position2.Rank);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            PositionDistance distance2 = (PositionDistance)obj;
            return DistanceX == distance2.DistanceX && DistanceY == distance2.DistanceY;
        }

        public override int GetHashCode()
        {
            return new { DistanceX, DistanceY }.GetHashCode();
        }

        public static bool operator ==(PositionDistance distance1, PositionDistance distance2)
        {
            return distance1.Equals(distance2);
        }

        public static bool operator !=(PositionDistance distance1, PositionDistance distance2)
        {
            return !distance1.Equals(distance2);
        }
    }
}
