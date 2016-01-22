namespace ChessDotNet.Pieces
{
    public class Bishop : ChessPiece
    {
        public override Player Owner
        {
            get;
            set;
        }

        public Bishop(Player owner)
        {
            Owner = owner;
        }

        public override string GetFenCharacter()
        {
            return Owner == Player.White ? "B" : "b";
        }
    }
}
