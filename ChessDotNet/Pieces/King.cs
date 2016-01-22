namespace ChessDotNet.Pieces
{
    public class King : ChessPiece
    {
        Player Owner
        {
            get;
            set;
        }

        public King(Player owner)
        {
            Owner = owner;
        }

        public override string GetFenCharacter()
        {
            return Owner == Player.White ? "K" : "k";
        }
    }
}
