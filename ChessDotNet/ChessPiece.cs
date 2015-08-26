using System;

namespace ChessDotNet
{
    public class ChessPiece
    {
        public Pieces Piece
        {
            get;
            private set;
        }

        public Players Player
        {
            get;
            private set;
        }

        public static ChessPiece None
        {
            get
            {
                return new ChessPiece(Pieces.None, Players.None);
            }
        }

        public ChessPiece(Pieces piece, Players player)
        {
            Piece = piece;
            Player = player;
        }

        public override bool Equals(object obj)
        {
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
            return piece1.Equals(piece2);
        }

        public static bool operator !=(ChessPiece piece1, ChessPiece piece2)
        {
            return !piece1.Equals(piece2);
        }

        public override string ToString()
        {
            return String.Format("ChessPiece: {0}, {1}", Piece, Player);
        }
    }
}
