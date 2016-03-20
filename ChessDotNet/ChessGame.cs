using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ChessDotNet.Pieces;
using System.Text;

namespace ChessDotNet
{
    public class ChessGame
    {
        bool _whiteRookAMoved = false;
        bool _whiteRookHMoved = false;
        bool _whiteKingMoved = false;
        bool _blackRookAMoved = false;
        bool _blackRookHMoved = false;
        bool _blackKingMoved = false;
        bool _drawn = false;
        string _drawReason = null;
        Player _resigned = Player.None;

        protected virtual Dictionary<char, Piece> FenMappings
        {
            get
            {
                return new Dictionary<char, Piece>()
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
                    { 'P', new Pawn(Player.White) },
                    { 'p', new Pawn(Player.Black) },
                };
            }
        }

        public GameStatus Status
        {
            get
            {
                return CalculateStatus(WhoseTurn, true);
            }
        }

        public Player WhoseTurn
        {
            get;
            private set;
        }

        int _halfMoveClock = 0;
        public int HalfMoveClock
        {
            get
            {
                return _halfMoveClock;
            }
        }

        int _fullMoveNumber = 1;
        public int FullMoveNumber
        {
            get
            {
                return _fullMoveNumber;
            }
        }

        public ReadOnlyCollection<Piece> PiecesOnBoard
        {
            get
            {
                return new ReadOnlyCollection<Piece>(Board.SelectMany(x => x).Where(x => x != null).ToList());
            }
        }

        public virtual int BoardWidth
        {
            get
            {
                return 8;
            }
        }

        public virtual int BoardHeight
        {
            get
            {
                return 8;
            }
        }

        protected Piece[][] Board;
        public Piece[][] GetBoard()
        {
            return CloneBoard(Board);
        }

        List<DetailedMove> _moves = new List<DetailedMove>();
        public ReadOnlyCollection<DetailedMove> Moves
        {
            get
            {
                return new ReadOnlyCollection<DetailedMove>(_moves);
            }
        }

        public bool BlackKingMoved
        {
            get
            {
                return _blackKingMoved;
            }
        }

        public bool BlackRookAMoved
        {
            get
            {
                return _blackRookAMoved;
            }
        }

        public bool BlackRookHMoved
        {
            get
            {
                return _blackRookHMoved;
            }
        }

        public bool WhiteKingMoved
        {
            get
            {
                return _whiteKingMoved;
            }
        }

        public bool WhiteRookAMoved
        {
            get
            {
                return _whiteRookAMoved;
            }
        }

        public bool WhiteRookHMoved
        {
            get
            {
                return _whiteRookHMoved;
            }
        }

        protected static Piece[][] CloneBoard(Piece[][] originalBoard)
        {
            Utilities.ThrowIfNull(originalBoard, "originalBoard");
            Piece[][] newBoard = new Piece[originalBoard.Length][];
            for (int i = 0; i < originalBoard.Length; i++)
            {
                newBoard[i] = new Piece[originalBoard[i].Length];
                Array.Copy(originalBoard[i], newBoard[i], originalBoard[i].Length);
            }
            return newBoard;
        }

        public ChessGame()
        {
            WhoseTurn = Player.White;
            _moves = new List<DetailedMove>();
            Board = new Piece[8][];
            Piece kw = new King(Player.White);
            Piece kb = new King(Player.Black);
            Piece qw = new Queen(Player.White);
            Piece qb = new Queen(Player.Black);
            Piece rw = new Rook(Player.White);
            Piece rb = new Rook(Player.Black);
            Piece nw = new Knight(Player.White);
            Piece nb = new Knight(Player.Black);
            Piece bw = new Bishop(Player.White);
            Piece bb = new Bishop(Player.Black);
            Piece pw = new Pawn(Player.White);
            Piece pb = new Pawn(Player.Black);
            Piece o = null;
            Board = new Piece[8][]
            {
                new[] { rb, nb, bb, qb, kb, bb, nb, rb },
                new[] { pb, pb, pb, pb, pb, pb, pb, pb },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { o, o, o, o, o, o, o, o },
                new[] { pw, pw, pw, pw, pw, pw, pw, pw },
                new[] { rw, nw, bw, qw, kw, bw, nw, rw }
            };
        }

        public ChessGame(IEnumerable<Move> moves, bool movesAreValidated) : this()
        {
            if (moves == null)
                throw new ArgumentNullException("moves");
            if (moves.Count() == 0)
                throw new ArgumentException("The Count of moves has to be greater than 0.");
            foreach (Move m in moves)
            {
                if (ApplyMove(m, movesAreValidated) == MoveType.Invalid)
                {
                    throw new ArgumentException("Invalid move passed to ChessGame constructor.");
                }
            }
        }

        public ChessGame(string fen)
        {
            GameCreationData data = FenStringToGameCreationData(fen);
            UseGameCreationData(data);
        }

        public ChessGame(GameCreationData data)
        {
            UseGameCreationData(data);
        }

        [Obsolete("This constructor is obsolete, use ChessGame(GameCreationData) instead.")]
        public ChessGame(Piece[][] board, Player whoseTurn)
        {
            Board = CloneBoard(board);
            _moves = new List<DetailedMove>();
            WhoseTurn = whoseTurn;
            Piece e1 = GetPieceAt(File.E, 1);
            Piece e8 = GetPieceAt(File.E, 8);
            Piece a1 = GetPieceAt(File.A, 1);
            Piece h1 = GetPieceAt(File.H, 1);
            Piece a8 = GetPieceAt(File.A, 8);
            Piece h8 = GetPieceAt(File.H, 8);
            if (!(e1 is King) || e1.Owner != Player.White)
                _whiteKingMoved = true;
            if (!(e8 is King) || e8.Owner != Player.Black)
                _blackKingMoved = true;
            if (!(a1 is Rook) || a1.Owner != Player.White)
                _whiteRookAMoved = true;
            if (!(h1 is Rook) || h1.Owner != Player.White)
                _whiteRookHMoved = true;
            if (!(a8 is Rook) || a8.Owner != Player.Black)
                _blackRookAMoved = true;
            if (!(h8 is Rook) || h8.Owner != Player.Black)
                _blackRookHMoved = true;
        }

        protected virtual void UseGameCreationData(GameCreationData data)
        {
            Board = CloneBoard(data.Board);
            WhoseTurn = data.WhoseTurn;
            Piece e1 = GetPieceAt(File.E, 1);
            Piece e8 = GetPieceAt(File.E, 8);
            Piece a1 = GetPieceAt(File.A, 1);
            Piece h1 = GetPieceAt(File.H, 1);
            Piece a8 = GetPieceAt(File.A, 8);
            Piece h8 = GetPieceAt(File.H, 8);
            if (!(e1 is King) || e1.Owner != Player.White)
                _whiteKingMoved = true;
            if (!(e8 is King) || e8.Owner != Player.Black)
                _blackKingMoved = true;
            if (!(a1 is Rook) || a1.Owner != Player.White || !data.CanWhiteCastleQueenSide)
                _whiteRookAMoved = true;
            if (!(h1 is Rook) || h1.Owner != Player.White || !data.CanWhiteCastleKingSide)
                _whiteRookHMoved = true;
            if (!(a8 is Rook) || a8.Owner != Player.Black || !data.CanBlackCastleQueenSide)
                _blackRookAMoved = true;
            if (!(h8 is Rook) || h8.Owner != Player.Black || !data.CanBlackCastleKingSide)
                _blackRookHMoved = true;

            if (data.EnPassant != null)
            {
                DetailedMove latestMove = new DetailedMove(new Move(new Position(data.EnPassant.File, data.WhoseTurn == Player.White ? 7 : 2),
                                                                    new Position(data.EnPassant.File, data.WhoseTurn == Player.White ? 5 : 4),
                                                                    Utilities.GetOpponentOf(data.WhoseTurn)),
                                          new Pawn(Utilities.GetOpponentOf(data.WhoseTurn)),
                                          false,
                                          CastlingType.None);
                _moves.Add(latestMove);
            }

            _halfMoveClock = data.HalfMoveClock;
            _fullMoveNumber = data.FullMoveNumber;
        }

        public virtual string GetFen()
        {
            StringBuilder fenBuilder = new StringBuilder();
            Piece[][] board = GetBoard();
            for (int i = 0; i < board.Length; i++)
            {
                Piece[] row = board[i];
                int empty = 0;
                foreach (Piece piece in row)
                {
                    char pieceChar = piece == null ? '\0' : piece.GetFenCharacter();
                    if (pieceChar == '\0')
                    {
                        empty++;
                        continue;
                    }
                    if (empty != 0)
                    {
                        fenBuilder.Append(empty);
                        empty = 0;
                    }
                    fenBuilder.Append(pieceChar);
                }
                if (empty != 0)
                {
                    fenBuilder.Append(empty);
                }
                if (i != board.Length - 1)
                {
                    fenBuilder.Append('/');
                }
            }

            fenBuilder.Append(' ');

            fenBuilder.Append(WhoseTurn == Player.White ? 'w' : 'b');

            fenBuilder.Append(' ');

            bool hasAnyCastlingOptions = false;
            if (!WhiteKingMoved)
            {
                if (!WhiteRookHMoved)
                {
                    fenBuilder.Append('K');
                    hasAnyCastlingOptions = true;
                }
                if (!WhiteRookAMoved)
                {
                    fenBuilder.Append('Q');
                    hasAnyCastlingOptions = true;
                }
            }
            if (!BlackKingMoved)
            {
                if (!BlackRookHMoved)
                {
                    fenBuilder.Append('k');
                    hasAnyCastlingOptions = true;
                }
                if (!BlackRookAMoved)
                {
                    fenBuilder.Append('q');
                    hasAnyCastlingOptions = true;
                }
            }
            if (!hasAnyCastlingOptions)
            {
                fenBuilder.Append('-');
            }

            fenBuilder.Append(' ');

            DetailedMove last;
            if (Moves.Count > 0 && (last = Moves[Moves.Count - 1]).Piece is Pawn && Math.Abs(last.OriginalPosition.Rank - last.NewPosition.Rank) == 2)
            {
                fenBuilder.Append(last.NewPosition.File.ToString().ToLowerInvariant());
                fenBuilder.Append(last.Player == Player.White ? 3 : 6);
            }
            else
            {
                fenBuilder.Append("-");
            }

            fenBuilder.Append(' ');

            fenBuilder.Append(_halfMoveClock);

            fenBuilder.Append(' ');

            fenBuilder.Append(_fullMoveNumber);

            return fenBuilder.ToString();
        }

        protected virtual GameCreationData FenStringToGameCreationData(string fen)
        {
            Dictionary<char, Piece> fenMappings = FenMappings;
            string[] parts = fen.Split(' ');
            if (parts.Length != 6) throw new ArgumentException("The FEN string does not have 6 parts.");
            Piece[][] board = new Piece[8][];
            string[] rows = parts[0].Split('/');
            if (rows.Length != 8) throw new ArgumentException("The board in the FEN string does not have 8 rows.");
            GameCreationData data = new GameCreationData();
            for (int i = 0; i < 8; i++)
            {
                string row = rows[i];
                Piece[] currentRow = new Piece[8] { null, null, null, null, null, null, null, null };
                int j = 0;
                foreach (char c in row)
                {
                    if (char.IsDigit(c))
                    {
                        j += (int)char.GetNumericValue(c);
                        continue;
                    }
                    if (!fenMappings.ContainsKey(c)) throw new ArgumentException("The FEN string contains an unknown piece.");
                    currentRow[j] = fenMappings[c];
                    j++;
                }
                if (j != 8)
                {
                    throw new ArgumentException("Not enough pieces provided for a row in the FEN string.");
                }
                board[i] = currentRow;
            }
            data.Board = board;

            if (parts[1] == "w")
            {
                data.WhoseTurn = Player.White;
            }
            else if (parts[1] == "b")
            {
                data.WhoseTurn = Player.Black;
            }
            else
            {
                throw new ArgumentException("Expected `w` or `b` for the active player in the FEN string.");
            }

            if (parts[2].Contains('K')) data.CanWhiteCastleKingSide = true;
            else data.CanWhiteCastleKingSide = false;

            if (parts[2].Contains('Q')) data.CanWhiteCastleQueenSide = true;
            else data.CanWhiteCastleQueenSide = false;

            if (parts[2].Contains('k')) data.CanBlackCastleKingSide = true;
            else data.CanBlackCastleKingSide = false;

            if (parts[2].Contains('q')) data.CanBlackCastleQueenSide = true;
            else data.CanBlackCastleQueenSide = false;

            if (parts[3] == "-") data.EnPassant = null;
            else
            {
                Position ep = new Position(parts[3]);
                if ((data.WhoseTurn == Player.White && (ep.Rank != 6 || !(data.Board[3][(int)ep.File] is Pawn))) ||
                    (data.WhoseTurn == Player.Black && (ep.Rank != 3 || !(data.Board[4][(int)ep.File] is Pawn))))
                {
                    throw new ArgumentException("Invalid en passant field in FEN.");
                }
                data.EnPassant = ep;
            }

            int halfmoveClock;
            if (int.TryParse(parts[4], out halfmoveClock))
            {
                data.HalfMoveClock = halfmoveClock;
            }
            else
            {
                throw new ArgumentException("Halfmove clock in FEN is invalid.");
            }

            int fullMoveNumber;
            if (int.TryParse(parts[5], out fullMoveNumber))
            {
                data.FullMoveNumber = fullMoveNumber;
            }
            else
            {
                throw new ArgumentException("Fullmove number in FEN is invalid.");
            }

            return data;
        }

        protected virtual GameStatus CalculateStatus(Player playerToValidate, bool validateHasAnyValidMoves)
        {
            if (_drawn)
            {
                return new GameStatus(GameEvent.Draw, Player.None, _drawReason);
            }
            if (_resigned != Player.None)
            {
                return new GameStatus(GameEvent.Resign, _resigned, _resigned.ToString() + " resigned");
            }
            Player other = Utilities.GetOpponentOf(playerToValidate);
            if (IsInCheck(playerToValidate))
            {
                if (validateHasAnyValidMoves && !HasAnyValidMoves(playerToValidate))
                {
                    return new GameStatus(GameEvent.Checkmate, other, playerToValidate.ToString() + " is checkmated");
                }
                else
                {
                    return new GameStatus(GameEvent.Check, other, playerToValidate.ToString() + " is in check");
                }
            }
            else if (validateHasAnyValidMoves && !HasAnyValidMoves(playerToValidate))
            {
                return new GameStatus(GameEvent.Stalemate, other, "Stalemate");
            }
            return new GameStatus(GameEvent.None, Player.None, "No special event");
        }

        public Piece GetPieceAt(Position position)
        {
            Utilities.ThrowIfNull(position, "position");
            return GetPieceAt(position.File, position.Rank);
        }

        public Piece GetPieceAt(File file, int rank)
        {
            return Board[8 - rank][(int)file];
        }

        protected virtual void SetPieceAt(File file, int rank, Piece piece)
        {
            Board[8 - rank][(int)file] = piece;
        }

        public bool IsValidMove(Move move)
        {
            Utilities.ThrowIfNull(move, "move");
            return IsValidMove(move, true, true);
        }

        protected bool IsValidMove(Move move, bool validateCheck)
        {
            Utilities.ThrowIfNull(move, "move");
            return IsValidMove(move, validateCheck, true);
        }

        protected virtual bool IsValidMove(Move move, bool validateCheck, bool careAboutWhoseTurnItIs)
        {
            Utilities.ThrowIfNull(move, "move");
            if (move.OriginalPosition.Equals(move.NewPosition))
                return false;
            Piece piece = GetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank);
            if (careAboutWhoseTurnItIs && move.Player != WhoseTurn) return false;
            if (piece.Owner != move.Player) return false;
            Piece pieceAtDestination = GetPieceAt(move.NewPosition);
            if (pieceAtDestination != null && pieceAtDestination.Owner == move.Player)
            {
                return false;
            }
            if (!piece.IsValidMove(move, this))
            {
                return false;
            }
            if (validateCheck && WouldBeInCheckAfter(move, move.Player))
            {
                return false;
            }

            return true;
        }

        protected virtual CastlingType ApplyCastle(Move move)
        {
            CastlingType castle;
            int rank = move.Player == Player.White ? 1 : 8;
            File rookFile;
            File newRookFile;
            if (move.NewPosition.File == File.C)
            {
                castle = CastlingType.QueenSide;
                rookFile = File.A;
                newRookFile = File.D;
            }
            else
            {
                castle = CastlingType.KingSide;
                rookFile = File.H;
                newRookFile = File.F;
            }
            SetPieceAt(newRookFile, rank, new Rook(move.Player));
            SetPieceAt(rookFile, rank, null);
            return castle;
        }

        public virtual MoveType ApplyMove(Move move, bool alreadyValidated)
        {
            Utilities.ThrowIfNull(move, "move");
            if (!alreadyValidated && !IsValidMove(move))
                return MoveType.Invalid;
            MoveType type = MoveType.Move;
            Piece movingPiece = GetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank);
            Piece capturedPiece = GetPieceAt(move.NewPosition.File, move.NewPosition.Rank);
            Piece newPiece = movingPiece;
            bool isCapture = capturedPiece != null;
            CastlingType castle = CastlingType.None;
            if (movingPiece is Pawn)
            {
                _halfMoveClock = 0;
                PositionDistance pd = new PositionDistance(move.OriginalPosition, move.NewPosition);
                if (pd.DistanceX == 1 && pd.DistanceY == 1 && GetPieceAt(move.NewPosition) == null)
                { // en passant
                    isCapture = true;
                    SetPieceAt(move.NewPosition.File, move.OriginalPosition.Rank, null);
                }
                if (move.NewPosition.Rank == (move.Player == Player.White ? 8 : 1))
                {
                    newPiece = move.Promotion;
                    type |= MoveType.Promotion;
                }
            }
            else if (movingPiece is King)
            {
                if (movingPiece.Owner == Player.White)
                    _whiteKingMoved = true;
                else
                    _blackKingMoved = true;

                if (new PositionDistance(move.OriginalPosition, move.NewPosition).DistanceX == 2)
                {
                    castle = ApplyCastle(move);
                    type |= MoveType.Castling;
                }
            }
            else if (movingPiece is Rook)
            {
                if (move.Player == Player.White)
                {
                    if (move.OriginalPosition.File == File.A && move.OriginalPosition.Rank == 1)
                        _whiteRookAMoved = true;
                    else if (move.OriginalPosition.File == File.H && move.OriginalPosition.Rank == 1)
                        _whiteRookHMoved = true;
                }
                else
                {
                    if (move.OriginalPosition.File == File.A && move.OriginalPosition.Rank == 8)
                        _blackRookAMoved = true;
                    else if (move.OriginalPosition.File == File.H && move.OriginalPosition.Rank == 8)
                        _blackRookHMoved = true;
                }
            }
            if (isCapture)
            {
                type |= MoveType.Capture;
                _halfMoveClock = 0;
            }
            if (!isCapture && !(movingPiece is Pawn))
            {
                _halfMoveClock++;
            }
            if (move.Player == Player.Black)
            {
                _fullMoveNumber++;
            }
            SetPieceAt(move.NewPosition.File, move.NewPosition.Rank, newPiece);
            SetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank, null);
            WhoseTurn = Utilities.GetOpponentOf(move.Player);
            _moves.Add(new DetailedMove(move, movingPiece, isCapture, castle));
            return type;
        }

        public ReadOnlyCollection<Move> GetValidMoves(Position from)
        {
            Utilities.ThrowIfNull(from, "from");
            return GetValidMoves(from, false);
        }

        protected virtual ReadOnlyCollection<Move> GetValidMoves(Position from, bool returnIfAny)
        {
            Utilities.ThrowIfNull(from, "from");
            Piece piece = GetPieceAt(from);
            if (piece == null || piece.Owner != WhoseTurn) return new ReadOnlyCollection<Move>(new List<Move>());
            return piece.GetValidMoves(from, returnIfAny, this);
        }

        public ReadOnlyCollection<Move> GetValidMoves(Player player)
        {
            return GetValidMoves(player, false);
        }

        protected virtual ReadOnlyCollection<Move> GetValidMoves(Player player, bool returnIfAny)
        {
            if (player != WhoseTurn) return new ReadOnlyCollection<Move>(new List<Move>());
            List<Move> validMoves = new List<Move>();
            for (int r = 1; r <= Board.Length; r++)
            {
                for (int f = 0; f < Board[8 - r].Length; f++)
                {
                    Piece p = GetPieceAt((File)f, r);
                    if (p != null && p.Owner == player)
                    {
                        validMoves.AddRange(GetValidMoves(new Position((File)f, r), returnIfAny));
                        if (returnIfAny && validMoves.Count > 0)
                        {
                            return new ReadOnlyCollection<Move>(validMoves);
                        }
                    }
                }
            }
            return new ReadOnlyCollection<Move>(validMoves);
        }

        public virtual bool HasAnyValidMoves(Position from)
        {
            Utilities.ThrowIfNull(from, "from");
            ReadOnlyCollection<Move> validMoves = GetValidMoves(from, true);
            return validMoves.Count > 0;
        }

        public virtual bool HasAnyValidMoves(Player player)
        {
            ReadOnlyCollection<Move> validMoves = GetValidMoves(player, true);
            return validMoves.Count > 0;
        }

        protected virtual bool IsInCheck(Player player)
        {
            Position kingPos = new Position(File.None, -1);

            for (int r = 1; r <= Board.Length; r++)
            {
                for (int f = 0; f < Board[8 - r].Length; f++)
                {
                    Piece curr = GetPieceAt((File)f, r);
                    if (curr is King && curr.Owner == player)
                    {
                        kingPos = new Position((File)f, r);
                        break;
                    }
                }
                if (kingPos != new Position(File.None, -1))
                {
                    break;
                }
            }

            if (kingPos.File == File.None)
                return false;

            for (int r = 1; r <= Board.Length; r++)
            {
                for (int f = 0; f < Board[8 - r].Length; f++)
                {
                    Piece curr = GetPieceAt((File)f, r);
                    if (curr == null) continue;
                    Player p = curr.Owner;
                    Move move = new Move(new Position((File)f, r), kingPos, p);
                    List<Move> moves = new List<Move>();
                    if (curr is Pawn && ((move.NewPosition.Rank == 8 && move.Player == Player.White) || (move.NewPosition.Rank == 1 && move.Player == Player.Black)))
                    {
                        moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, new Queen(move.Player)));
                        moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, new Rook(move.Player)));
                        moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, new Bishop(move.Player)));
                        moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, new Bishop(move.Player)));
                    }
                    else
                    {
                        moves.Add(move);
                    }
                    foreach (Move m in moves)
                    {
                        if (IsValidMove(m, false, false))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public virtual bool WouldBeInCheckAfter(Move move, Player player)
        {
            Utilities.ThrowIfNull(move, "move");
            GameCreationData gcd = new GameCreationData();
            gcd.Board = Board;
            gcd.CanWhiteCastleKingSide = !_whiteRookHMoved && !_whiteKingMoved;
            gcd.CanWhiteCastleQueenSide = !_whiteRookAMoved && !_whiteKingMoved;
            gcd.CanBlackCastleKingSide = !_blackRookHMoved && !_blackKingMoved;
            gcd.CanBlackCastleQueenSide = !_blackRookAMoved && !_blackKingMoved;
            gcd.EnPassant = null;
            if (_moves.Count > 0)
            {
                DetailedMove last = _moves.Last();
                if (last.Piece is Pawn && new PositionDistance(last.OriginalPosition, last.NewPosition).DistanceY == 2)
                {
                    gcd.EnPassant = new Position(last.NewPosition.File, last.Player == Player.White ? 3 : 6);
                }
            }
            gcd.HalfMoveClock = _halfMoveClock;
            gcd.FullMoveNumber = _fullMoveNumber;
            ChessGame copy = new ChessGame(gcd);
            copy.ApplyMove(move, true);
            GameStatus status = copy.CalculateStatus(player, false);
            return status.Event == GameEvent.Check && status.PlayerWhoCausedEvent != player;
        }

        public void Draw(string reason)
        {
            _drawn = true;
            _drawReason = reason;
        }

        public void Resign(Player player)
        {
            _resigned = player;
        }
    }
}
