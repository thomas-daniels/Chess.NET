using System;
using System.Linq;

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

        public static File[] FilesBetween(File file1, File file2, bool file1Inclusive, bool file2Inclusive)
        {
            if (file1 == file2)
            {
                if (file1Inclusive || file2Inclusive) { return new File[] { file1 }; }
                else { return new File[] { }; }
            }
            int min = Math.Min((int)file1, (int)file2);
            int max = Math.Max((int)file1, (int)file2);
            bool minInc;
            bool maxInc;
            if (min == (int)file1)
            {
                minInc = file1Inclusive;
                maxInc = file2Inclusive;
            }
            else
            {
                maxInc = file1Inclusive;
                minInc = file2Inclusive;
            }
            File[] files = new File[] { File.A, File.B, File.C, File.D, File.E, File.F, File.G, File.H };
            return files.Skip(min + (minInc ? 0 : 1)).Take(max - min + (maxInc ? 1 : 0) - (minInc ? 0 : 1)).ToArray();
        }
    }
}
