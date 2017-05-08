namespace ChessDotNet.Variants.Crazyhouse
{
    public class Drop
    {
        public Piece ToDrop { get; private set; }
        public Position Destination { get; private set; }
        public Player Player { get; private set; }

        public Drop(Piece toDrop, Position destination, Player player)
        {
            ToDrop = toDrop;
            Destination = destination;
            Player = player;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            Drop drop1 = this;
            Drop drop2 = obj as Drop;
            return drop1.ToDrop.Equals(drop2.ToDrop) && drop1.Destination.Equals(drop2.Destination) && drop1.Player == drop2.Player;
        }

        public override int GetHashCode()
        {
            return new { ToDrop = ToDrop, Destination = Destination, Player = Player }.GetHashCode();
        }

        public static bool operator ==(Drop drop1, Drop drop2)
        {
            if (ReferenceEquals(drop1, drop2))
                return true;
            if ((object)drop1 == null || (object)drop2 == null)
                return false;
            return drop1.Equals(drop2);
        }

        public static bool operator !=(Drop drop1, Drop drop2)
        {
            if (ReferenceEquals(drop1, drop2))
                return false;
            if ((object)drop1 == null || (object)drop2 == null)
                return true;
            return !drop1.Equals(drop2);
        }
    }
}
