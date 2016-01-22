namespace ChessDotNet.Pieces
{
    public class Pawn : ChessPiece
    {
        Player Owner
        {
            get;
            set;
        }

        public Pawn(Player owner)
        {
            Owner = owner;
        }

        public override string GetFenCharacter()
        {
            return Owner == Player.White ? "P" : "p";
        }
    }
}
