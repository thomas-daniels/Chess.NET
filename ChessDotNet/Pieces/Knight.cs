namespace ChessDotNet.Pieces
{
    public class Knight : ChessPiece
    {
        public override Player Owner
        {
            get;
            set;
        }

        public Knight(Player owner)
        {
            Owner = owner;
        }

        public override string GetFenCharacter()
        {
            return Owner == Player.White ? "N" : "n";
        }
    }
}
