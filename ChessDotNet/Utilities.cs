using System;

namespace ChessDotNet
{
    public static class Utilities
    {
        public static void ThrowIfNull(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static Player GetOpponentOf(Player player)
        {
            if (player == Player.None)
                throw new ArgumentException("`player` cannot be Player.None.");
            return player == Player.White ? Player.Black : Player.White;
        }

        public static int GetRelativePieceValue(ChessPiece piece)
        {
            return GetRelativePieceValue(piece.Piece);
        }

        public static int GetRelativePieceValue(Piece piece)
        {
            switch (piece)
            {
                case Piece.King:
                    return int.MaxValue;
                case Piece.Queen:
                    return 9;
                case Piece.Rook:
                    return 5;
                case Piece.Knight:
                case Piece.Bishop:
                    return 3;
                case Piece.Pawn:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
