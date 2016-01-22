namespace ChessDotNet
{
    public abstract class ChessPiece
    {
        public abstract Player Owner
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            ChessPiece piece1 = this;
            ChessPiece piece2 = (ChessPiece)obj;
            return piece1.Owner == piece2.Owner;
        }

        public override int GetHashCode()
        {
            return new { Piece = GetFenCharacter(), Owner }.GetHashCode();
        }

        public static bool operator ==(ChessPiece piece1, ChessPiece piece2)
        {
            if (ReferenceEquals(piece1, piece2))
                return true;
            if ((object)piece1 == null || (object)piece2 == null)
                return false;
            return piece1.Equals(piece2);
        }

        public static bool operator !=(ChessPiece piece1, ChessPiece piece2)
        {
            if (ReferenceEquals(piece1, piece2))
                return false;
            if ((object)piece1 == null || (object)piece2 == null)
                return true;
            return !piece1.Equals(piece2);
        }

        public abstract string GetFenCharacter();
    }
}
