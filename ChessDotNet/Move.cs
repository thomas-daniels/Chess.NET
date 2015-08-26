namespace ChessDotNet
{
    public class Move
    {
        public Position OriginalPosition
        {
            get;
            private set;
        }

        public Position NewPosition
        {
            get;
            private set;
        }

        public Players Player
        {
            get;
            private set;
        }

        public Pieces Promotion
        {
            get;
            private set;
        }

        public Move(Position originalPos, Position newPos, Players player)
            : this(originalPos, newPos, player, Pieces.None)
        { }

        public Move(string originalPos, string newPos, Players player)
            : this(originalPos, newPos, player, Pieces.None)
        { }

        public Move(Position originalPos, Position newPos, Players player, Pieces promotion)
        {
            OriginalPosition = originalPos;
            NewPosition = newPos;
            Player = player;
            Promotion = promotion;
        }

        public Move(string originalPos, string newPos, Players player, Pieces promotion)
        {
            OriginalPosition = new Position(originalPos);
            NewPosition = new Position(newPos);
            Player = player;
            Promotion = promotion;
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

        public override string ToString()
        {
            return OriginalPosition.ToString() + "-" + NewPosition.ToString();
        }
    }
}
