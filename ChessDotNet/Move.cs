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
            Move move1 = this;
            Move move2 = (Move)obj;
            return move1.OriginalPosition.Equals(move2.OriginalPosition)
                && move1.NewPosition.Equals(move2.NewPosition)
                && move1.Player == move2.Player;
        }

        public override int GetHashCode()
        {
            return new { OriginalPosition, NewPosition, Player }.GetHashCode();
        }

        public static bool operator ==(Move move1, Move move2)
        {
            return move1.Equals(move2);
        }

        public static bool operator !=(Move move1, Move move2)
        {
            return !move1.Equals(move2);
        }

        public override string ToString()
        {
            return OriginalPosition.ToString() + "-" + NewPosition.ToString();
        }
    }
}
