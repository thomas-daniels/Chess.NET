using System.Globalization;

namespace ChessDotNet
{
    public class ChessPiece
    {
        public Piece Piece
        {
            get;
            private set;
        }

        public Player Player
        {
            get;
            private set;
        }

        public static ChessPiece None
        {
            get
            {
                return new ChessPiece(Piece.None, Player.None);
            }
        }

        public ChessPiece(Piece piece, Player player)
        {
            Piece = piece;
            Player = player;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            ChessPiece piece1 = this;
            ChessPiece piece2 = (ChessPiece)obj;
            return piece1.Piece == piece2.Piece && piece1.Player == piece2.Player;
        }

        public override int GetHashCode()
        {
            return new { Piece, Player }.GetHashCode();
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

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ChessPiece: {0}, {1}", Piece, Player);
        }
    }
}
