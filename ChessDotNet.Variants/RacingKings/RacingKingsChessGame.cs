using ChessDotNet.Pieces;
using ChessDotNet.Variants.RacingKings.Pieces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessDotNet.Variants.RacingKings
{
    public class RacingKingsChessGame : ChessGame
    {
        protected override bool CastlingCanBeLegal
        {
            get
            {
                return false;
            }
        }

        private Dictionary<char, Piece> fenMappings = new Dictionary<char, Piece>()
        {
            { 'K', new RacingKingsKing(Player.White) },
            { 'k', new RacingKingsKing(Player.Black) },
            { 'Q', new Queen(Player.White) },
            { 'q', new Queen(Player.Black) },
            { 'R', new Rook(Player.White) },
            { 'r', new Rook(Player.Black) },
            { 'B', new Bishop(Player.White) },
            { 'b', new Bishop(Player.Black) },
            { 'N', new Knight(Player.White) },
            { 'n', new Knight(Player.Black) },
            { 'P', new Pawn(Player.White) },
            { 'p', new Pawn(Player.Black) },
        };
        protected override Dictionary<char, Piece> FenMappings
        {
            get
            {
                return fenMappings;
            }
        }

        public RacingKingsChessGame()
        {
            WhoseTurn = Player.White;
            Moves = new ReadOnlyCollection<DetailedMove>(new List<DetailedMove>());
            Board = new Piece[8][];
            Piece kb = FenMappings['k'];
            Piece qb = FenMappings['q'];
            Piece rb = FenMappings['r'];
            Piece nb = FenMappings['n'];
            Piece bb = FenMappings['b'];
            Piece kw = FenMappings['K'];
            Piece qw = FenMappings['Q'];
            Piece rw = FenMappings['R'];
            Piece bw = FenMappings['B'];
            Piece nw = FenMappings['N'];
            Piece o = null;
            Board = new Piece[8][]
            {
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { kb, rb, bb, nb, nw, bw, rw, kw },
                new[] { qb, rb, bb, nb, nw, bw, rw, qw }
            };
            CanBlackCastleKingSide = CanBlackCastleQueenSide = CanWhiteCastleKingSide = CanWhiteCastleQueenSide = false;
        }
        public RacingKingsChessGame(Piece[][] board, Player whoseTurn) : base(board, whoseTurn) { }
        public RacingKingsChessGame(GameCreationData data) : base(data) { }
        public RacingKingsChessGame(string fen) : base(fen) { }
        public RacingKingsChessGame(IEnumerable<Move> moves, bool movesAreValidated) : base(moves, movesAreValidated) { }

        protected override bool IsValidMove(Move move, bool validateCheck, bool careAboutWhoseTurnItIs)
        {
            bool validAsPerStandardRules = base.IsValidMove(move, validateCheck, careAboutWhoseTurnItIs);
            if (!validAsPerStandardRules) return false;
            if (validateCheck && WouldBeInCheckAfter(move, ChessUtilities.GetOpponentOf(move.Player)))
            {
                return false;
                // In racing kings, you can't check(mate) your opponent.
            }

            return true;
        }

        protected bool IsKingOnBackRank(Player player)
        {
            for (int i = 0; i < BoardWidth; i++)
            {
                Piece p = Board[0][i];
                if (p is King && p.Owner == player)
                {
                    return true;
                }
            }
            return false;
        }

        protected Position FindKing(Player player)
        {
            for (int r = 0; r < BoardHeight; r++)
            {
                for (int f = 0; f < BoardWidth; f++)
                {
                    if (Board[r][f] is King && Board[r][f].Owner == player)
                    {
                        return new Position((File)f, 8 - r);
                    }
                }
            }
            return null;
        }

        public override bool IsWinner(Player player)
        {
            bool kingOnBackRank = IsKingOnBackRank(player);
            if (!kingOnBackRank) return kingOnBackRank;

            if (player == Player.Black)
            {
                return true;
            }
            else if (WhoseTurn == Player.White)
            {
                if (FindKing(Player.Black).Rank != 8)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                ReadOnlyCollection<Move> validKingMoves = GetValidMoves(FindKing(Player.Black));
                if (validKingMoves.Any(x => x.NewPosition.Rank == 8))
                {
                    return false;
                }
                return true;
            }
        }
    }
}
