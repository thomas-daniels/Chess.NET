using System;

namespace ChessDotNet
{
    public static class ChessUtilities
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
    }
}
