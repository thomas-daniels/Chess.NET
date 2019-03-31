using ChessDotNet.Pieces;
using ChessDotNet.Variants.Horde.Pieces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessDotNet.Variants.Horde
{
    public class HordeChessGame : ChessGame
    {
        public HordeChessGame() : base()
        {
            WhoseTurn = Player.White;
            Moves = new ReadOnlyCollection<DetailedMove>(new List<DetailedMove>());
            Board = new Piece[8][];
            Piece kb = FenMappings['k'];
            Piece qb = FenMappings['q'];
            Piece rb = FenMappings['r'];
            Piece nb = FenMappings['n'];
            Piece bb = FenMappings['b'];
            Piece pw = FenMappings['P'];
            Piece pb = FenMappings['p'];
            Piece o = null;
            Board = new Piece[8][]
            {
                new[] { rb, nb, bb, qb, kb, bb, nb, rb },
                new[] { pb, pb, pb, pb, pb, pb, pb, pb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, pw, pw, o, o, pw, pw, o },
                new[] { pw, pw, pw, pw, pw, pw, pw, pw },
                new[] { pw, pw, pw, pw, pw, pw, pw, pw },
                new[] { pw, pw, pw, pw, pw, pw, pw, pw },
                new[] { pw, pw, pw, pw, pw, pw, pw, pw }
            };
            CanBlackCastleKingSide = CanBlackCastleQueenSide = CastlingCanBeLegal;
            CanWhiteCastleKingSide = CanWhiteCastleQueenSide = false;
        }

        private Dictionary<char, Piece> fenMappings = new Dictionary<char, Piece>()
        {
            { 'K', new King(Player.White) },
            { 'k', new King(Player.Black) },
            { 'Q', new Queen(Player.White) },
            { 'q', new Queen(Player.Black) },
            { 'R', new Rook(Player.White) },
            { 'r', new Rook(Player.Black) },
            { 'B', new Bishop(Player.White) },
            { 'b', new Bishop(Player.Black) },
            { 'N', new Knight(Player.White) },
            { 'n', new Knight(Player.Black) },
            { 'P', new HordePawn() },
            { 'p', new Pawn(Player.Black) },
        };
        protected override Dictionary<char, Piece> FenMappings
        {
            get
            {
                return fenMappings;
            }
        }

        public HordeChessGame(Piece[][] board, Player whoseTurn) : base(board, whoseTurn) { }
        public HordeChessGame(GameCreationData data) : base(data) { }
        public HordeChessGame(string fen) : base(fen) { }
        public HordeChessGame(IEnumerable<Move> moves, bool movesAreValidated) : this() {
            if (moves == null)
                throw new ArgumentNullException("moves");
            foreach (Move m in moves)
            {
                if (ApplyMove(m, movesAreValidated) == MoveType.Invalid)
                {
                    throw new ArgumentException("Invalid move passed to ChessGame constructor.");
                }
            }
        }

        public override bool IsInCheck(Player player)
        {
            if (player == Player.White)
            {
                return false;
            }
            return base.IsInCheck(player);
        }

        public override bool IsCheckmated(Player player)
        {
            if (player == Player.White)
            {
                return false;
            }
            return base.IsCheckmated(player);
        }

        public bool IsHordeDestroyed()
        {
            for (int i = 0; i < BoardHeight; i++)
            {
                for (int j = 0; j < BoardWidth; j++)
                {
                    if (Board[i][j] != null && Board[i][j].Owner == Player.White)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override bool IsWinner(Player player)
        {
            if (player == Player.White)
            {
                return IsCheckmated(Player.Black);
            }
            else
            {
                return IsHordeDestroyed();
            }
        }
    }
}
