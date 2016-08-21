using ChessDotNet.Pieces;
using System.Collections.Generic;

namespace ChessDotNet.Variants.KingOfTheHill
{
    public class KingOfTheHillChessGame : ChessGame
    {
        Position[] center = new Position[]
        {
            new Position(File.E, 4),
            new Position(File.E, 5),
            new Position(File.D, 4),
            new Position(File.D, 5)
        };

        public virtual Position[] Center
        {
            get
            {
                return center;
            }
        }
        public KingOfTheHillChessGame() : base() { }
        public KingOfTheHillChessGame(Piece[][] board, Player whoseTurn) : base(board, whoseTurn) { }
        public KingOfTheHillChessGame(GameCreationData data) : base(data) { }
        public KingOfTheHillChessGame(string fen) : base(fen) { }
        public KingOfTheHillChessGame(IEnumerable<Move> moves, bool movesAreValidated) : base(moves, movesAreValidated) { }

        Cache<bool> kingInCenterCacheWhite = new Cache<bool>(false, -1);
        Cache<bool> kingInCenterCacheBlack = new Cache<bool>(false, -1);
        public bool IsKingInCenter(Player player)
        {
            Cache<bool> cache = player == Player.White ? kingInCenterCacheWhite : kingInCenterCacheBlack;
            if (cache.CachedAt == Moves.Count)
            {
                return cache.Value;
            }

            foreach (Position pos in center)
            {
                Piece p = GetPieceAt(pos);
                if (p is King && p.Owner == player)
                {
                    return cache.UpdateCache(true, Moves.Count);
                }
            }

            return cache.UpdateCache(false, Moves.Count);
        }

        public override bool IsWinner(Player player)
        {
            return IsKingInCenter(player) || IsCheckmated(ChessUtilities.GetOpponentOf(player));
        }
    }
}
