using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ChessPiece c1 = this;
            ChessPiece c2 = (ChessPiece)obj;
            return c1.Piece == c2.Piece && c1.Player == c2.Player;
        }

        public override int GetHashCode()
        {
            return new { Piece, Player }.GetHashCode();
        }

        public static bool operator ==(ChessPiece c1, ChessPiece c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(ChessPiece c1, ChessPiece c2)
        {
            return !c1.Equals(c2);
        }
    }
}
