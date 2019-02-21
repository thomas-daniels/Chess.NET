namespace ChessDotNet
{
    public class DetailedMove : Move
    {
        public Piece Piece
        {
            get;
            set;
        }

        public bool IsCapture
        {
            get;
            set;
        }

        public CastlingType Castling
        {
            get;
            set;
        }

        public string SAN
        {
            get;
            set;
        }

        public Piece CapturedPiece
        {
            get;
            set;
        }

        public int? LastHalfMoveClock
        {
            get;
            set;
        }

        public bool EnPassant
        {
            get;
            set;
        }


        protected DetailedMove() { }

        public DetailedMove(Position originalPosition, Position newPosition, Player player, char? promotion, Piece piece, bool isCapture, CastlingType castling, string san) :
            base(originalPosition, newPosition, player, promotion)
        {
            Piece = piece;
            IsCapture = isCapture;
            Castling = castling;
            SAN = san;
        }

        public DetailedMove(Position originalPosition, Position newPosition, Player player, char? promotion, Piece piece, bool isCapture, CastlingType castling, string san, Piece captured, int? lastHalfMoveClock, bool enPassant)
            : this(originalPosition, newPosition, player, promotion, piece, isCapture, castling, san)
        {
            CapturedPiece = captured;
            LastHalfMoveClock = lastHalfMoveClock;
            EnPassant = enPassant;
        }

        public DetailedMove(Move move, Piece piece, bool isCapture, CastlingType castling, string san)
            : this(move.OriginalPosition, move.NewPosition, move.Player, move.Promotion, piece, isCapture, castling, san)
        {
        }

        public DetailedMove(Move move, Piece piece, Piece capturedPiece, CastlingType castling, string san, int halfMoveClock, bool enPassant)
            : this(move.OriginalPosition, move.NewPosition, move.Player, move.Promotion, piece, capturedPiece != null, castling, san, capturedPiece, halfMoveClock, enPassant)
        {
        }
    }
}
