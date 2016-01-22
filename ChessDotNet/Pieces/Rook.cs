namespace ChessDotNet.Pieces
{
    public class Rook : ChessPiece
    {
        Player Owner
        {
            get;
            set;
        }

        public Rook(Player owner)
        {
            Owner = owner;
        }

        public override string GetFenCharacter()
        {
            return Owner == Player.White ? "R" : "r";
        }
    }
}
