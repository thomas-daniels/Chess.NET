using ChessDotNet.Pieces;
using ChessDotNet.Variants.Antichess.Pieces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessDotNet.Variants.Antichess
{
    public class AntichessGame : ChessGame
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
            { 'K', new AntichessKing(Player.White) },
            { 'k', new AntichessKing(Player.Black) },
            { 'Q', new Queen(Player.White) },
            { 'q', new Queen(Player.Black) },
            { 'R', new Rook(Player.White) },
            { 'r', new Rook(Player.Black) },
            { 'B', new Bishop(Player.White) },
            { 'b', new Bishop(Player.Black) },
            { 'N', new Knight(Player.White) },
            { 'n', new Knight(Player.Black) },
            { 'P', new AntichessPawn(Player.White) },
            { 'p', new AntichessPawn(Player.Black) },
        };

        protected override Dictionary<char, Piece> FenMappings
        {
            get
            {
                return fenMappings;
            }
        }

        public AntichessGame() : base() { }
        public AntichessGame(Piece[][] board, Player whoseTurn) : base(board, whoseTurn) { }
        public AntichessGame(GameCreationData data) : base(data) { }
        public AntichessGame(string fen) : base(fen) { }
        public AntichessGame(IEnumerable<Move> moves, bool movesAreValidated) : base(moves, movesAreValidated) { }

        public override bool IsInCheck(Player player)
        {
            return false;
        }

        public override bool IsCheckmated(Player player)
        {
            return false;
        }

        protected override bool IsValidMove(Move move, bool validateCheck, bool careAboutWhoseTurnItIs)
        {
            if (careAboutWhoseTurnItIs && WhoseTurn != move.Player) return false;

            return GetValidMoves(move.Player).Contains(move);
        }

        protected override ReadOnlyCollection<Move> GetValidMoves(Player player, bool returnIfAny)
        {
            ReadOnlyCollection<Move> valid = base.GetValidMoves(player, returnIfAny);
            if (valid.Any(x => GetPieceAt(x.NewPosition) != null))
            {
                valid = new ReadOnlyCollection<Move>(valid.Where(x => GetPieceAt(x.NewPosition) != null).ToList());
            }
            return valid;
        }

        protected override ReadOnlyCollection<Move> GetValidMoves(Position from, bool returnIfAny)
        {
            Piece piece = GetPieceAt(from);
            if (piece == null || piece.Owner != WhoseTurn) return new ReadOnlyCollection<Move>(new List<Move>());
            ReadOnlyCollection<Move> valid = piece.GetValidMoves(from, returnIfAny, this, m => base.IsValidMove(m, true, true));
            if (valid.Any(x => GetPieceAt(x.NewPosition) != null))
            {
                valid = new ReadOnlyCollection<Move>(valid.Where(x => GetPieceAt(x.NewPosition) != null).ToList());
            }
            return valid;

            // Antichess doesn't need a change to the 'returnIfAny' system even if the found "valid" move is invalid with the forced capture system:
            // only another valid move, a valid capture, can override the validness of that move, so we are sure that there *are* valid moves.
        }

        public override bool WouldBeInCheckAfter(Move move, Player player)
        {
            return false;
        }

        public override bool IsWinner(Player player)
        {
            return IsStalemated(player);
        }

        public override bool IsDraw()
        {
            return DrawClaimed;
        }
    }
}
