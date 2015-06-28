namespace ChessDotNet
{
    public class Move
    {
        public Position OriginalPosition
        {
            get;
        }

        public Position NewPosition
        {
            get;
        }

        public Players Player
        {
            get;
        }

        public Move(Position originalPos, Position newPos, Players player)
        {
            OriginalPosition = originalPos;
            NewPosition = newPos;
            Player = player;
        }

        public override bool Equals(object obj)
        {
            Move m1 = this;
            Move m2 = (Move)obj;
            return m1.OriginalPosition.Equals(m2.OriginalPosition)
                && m1.NewPosition.Equals(m2.NewPosition)
                && m1.Player == m2.Player;
        }

        public override int GetHashCode()
        {
            return new { OriginalPosition, NewPosition, Player }.GetHashCode();
        }

        public static bool operator ==(Move m1, Move m2)
        {
            return m1.Equals(m2);
        }

        public static bool operator !=(Move m1, Move m2)
        {
            return !m1.Equals(m2);
        }
    }
}
