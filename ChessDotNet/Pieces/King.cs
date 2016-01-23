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
    }
}
