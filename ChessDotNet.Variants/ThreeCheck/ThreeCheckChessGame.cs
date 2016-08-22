using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ChessDotNet.Variants.ThreeCheck
{
    public class ThreeCheckChessGame : ChessGame
    {
        public int ChecksByWhite
        {
            get;
            protected set;
        }

        public int ChecksByBlack
        {
            get;
            protected set;
        }

        protected override int[] AllowedFenPartsLength
        {
            get
            {
                return new int[2] { 6, 7 };
            }
        }

        public ThreeCheckChessGame() : base() { }
        public ThreeCheckChessGame(Piece[][] board, Player whoseTurn) : base(board, whoseTurn) { }
        public ThreeCheckChessGame(GameCreationData data) : base(data) { }
        public ThreeCheckChessGame(string fen) : base(fen) {}
        public ThreeCheckChessGame(IEnumerable<Move> moves, bool movesAreValidated) : base(moves, movesAreValidated) { }

        protected override GameCreationData FenStringToGameCreationData(string fen)
        {
            GameCreationData gcd = base.FenStringToGameCreationData(fen);

            string[] parts = fen.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 7)
            {
                Regex re = new Regex(@"^\+(\d)\+(\d)$");
                Match m = re.Match(parts[6]);
                if (!m.Success)
                {
                    throw new ArgumentException("Invalid FEN: invalid check counter.");
                }
                gcd.ThreeCheck_ChecksByWhite = int.Parse(m.Groups[1].Value);
                gcd.ThreeCheck_ChecksByBlack = int.Parse(m.Groups[2].Value);
            }

            return gcd;
        }

        protected override void UseGameCreationData(GameCreationData data)
        {
            base.UseGameCreationData(data);
            ChecksByWhite = data.ThreeCheck_ChecksByWhite;
            ChecksByBlack = data.ThreeCheck_ChecksByBlack;
        }

        public override MoveType ApplyMove(Move move, bool alreadyValidated)
        {
            MoveType ret = base.ApplyMove(move, alreadyValidated);

            if (WhoseTurn == Player.White && IsInCheck(Player.White))
            {
                ChecksByBlack++;
            }
            if (WhoseTurn == Player.Black && IsInCheck(Player.Black))
            {
                ChecksByWhite++;
            }

            return ret;
        }

        public override bool IsWinner(Player player)
        {
            int checks = player == Player.White ? ChecksByWhite : ChecksByBlack;
            return checks >= 3 || IsCheckmated(ChessUtilities.GetOpponentOf(player));
        }
    }
}
