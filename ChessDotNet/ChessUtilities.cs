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

        private static string Chess960StartingArray(int n)
        {
            string[] fenParts = new string[8];

            int n2 = n / 4;
            int b1 = n % 4;
            fenParts[1 + b1 * 2] = "B";

            int n3 = n2 / 4;
            int b2 = n2 % 4;
            fenParts[b2 * 2] = "B";

            int n4 = n3 / 6;
            int q = n3 % 6;

            int free = 0;
            for (int i = 0; i < fenParts.Length; i++)
            {
                if (fenParts[i] == null)
                {
                    if (free == q)
                    {
                        fenParts[i] = "Q";
                        break;
                    }
                    free++;
                }
            }

            bool[] knightPositioning = new bool[][]
            {
                new bool[] { true, true, false, false, false },
                new bool[] { true, false, true, false, false },
                new bool[] { true, false, false, true, false },
                new bool[] { true, false, false, false, true },
                new bool[] { false, true, true, false, false },
                new bool[] { false, true, false, true, false },
                new bool[] { false, true, false, false, true },
                new bool[] { false, false, true, true, false },
                new bool[] { false, false, true, false, true },
                new bool[] { false, false, false, true, true }
            }[n4];
            int knightPosCounter = 0;
            for (int i = 0; i < fenParts.Length; i++)
            {
                if (fenParts[i] == null)
                {
                    if (knightPositioning[knightPosCounter])
                    {
                        fenParts[i] = "N";
                    }
                    knightPosCounter++;
                }
            }

            free = 0;
            for (int i = 0; i < fenParts.Length; i++)
            {
                if (fenParts[i] == null)
                {
                    switch (free)
                    {
                        case 0:
                            fenParts[i] = "R";
                            break;
                        case 1:
                            fenParts[i] = "K";
                            break;
                        case 2:
                            fenParts[i] = "R";
                            break;
                    }
                    free++;
                    if (free > 2)
                    {
                        break;
                    }
                }
            }

            return string.Join("", fenParts);
        }

        public static string FenForChess960Symmetrical(int n)
        {
            if (n < 0 || n > 959)
            {
                throw new ArgumentException("'n' must be greater than or equal to 0, and smaller than or equal to 959.");
            }

            string startingPos = Chess960StartingArray(n);
            return string.Format("{0}/pppppppp/8/8/8/8/PPPPPPPP/{1} w KQkq - 0 1", startingPos.ToLower(), startingPos);
        }

        public static string FenForChess960Asymmetrical(int nWhite, int nBlack)
        {
            if (nWhite < 0 || nWhite > 959 || nBlack < 0 || nBlack > 959)
            {
                throw new ArgumentException("'nWhite' and 'nBlack' must be greater than or equal to 0, and smaller than or equal to 959.");
            }

            string startingPosWhite = Chess960StartingArray(nWhite);
            string startingPosBlack = Chess960StartingArray(nBlack).ToLower();
            return string.Format("{0}/pppppppp/8/8/8/8/PPPPPPPP/{1} w KQkq - 0 1", startingPosBlack, startingPosWhite);
        }
    }
}
