using System.Collections.Generic;

namespace ChessDotNet.Variants.ThreeCheck
{
    public class ThreeCheckChessGame : ChessGame
    {
        public int WhiteInCheck
        {
            get;
            protected set;
        }

        public int BlackInCheck
        {
            get;
            protected set;
        }

        public ThreeCheckChessGame() : base() { }
        public ThreeCheckChessGame(Piece[][] board, Player whoseTurn) : base(board, whoseTurn) { }
        public ThreeCheckChessGame(GameCreationData data) : base(data) { }
        public ThreeCheckChessGame(string fen) : base(fen)
        {
            if (WhoseTurn == Player.White && IsInCheck(Player.White))
            {
                WhiteInCheck = 1;
            }
            if (WhoseTurn == Player.Black && IsInCheck(Player.Black))
            {
                BlackInCheck = 1;
            }
        }
        public ThreeCheckChessGame(IEnumerable<Move> moves, bool movesAreValidated) : base(moves, movesAreValidated) { }
        public ThreeCheckChessGame(string fen, int whiteInCheck, int blackInCheck) : this(fen)
        {
            WhiteInCheck = whiteInCheck;
            BlackInCheck = blackInCheck;
        }

        public override MoveType ApplyMove(Move move, bool alreadyValidated)
        {
            MoveType ret = base.ApplyMove(move, alreadyValidated);

            if (WhoseTurn == Player.White && IsInCheck(Player.White))
            {
                WhiteInCheck++;
            }
            if (WhoseTurn == Player.Black && IsInCheck(Player.Black))
            {
                BlackInCheck++;
            }

            return ret;
        }

        public override bool IsWinner(Player player)
        {
            int opponentChecked = player == Player.White ? BlackInCheck : WhiteInCheck;
            return opponentChecked >= 3 || IsCheckmated(ChessUtilities.GetOpponentOf(player));
        }
    }
}
