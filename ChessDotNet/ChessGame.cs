using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ChessDotNet.Pieces;

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

        List<DetailedMove> _moves;
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

        public ChessGame(Piece[][] board, Player whoseTurn)
        {
            Board = CloneBoard(board);
            _moves = new List<DetailedMove>();
            WhoseTurn = whoseTurn;
            Piece e1 = GetPieceAt(File.E, Rank.One);
            Piece e8 = GetPieceAt(File.E, Rank.Eight);
            Piece a1 = GetPieceAt(File.A, Rank.One);
            Piece h1 = GetPieceAt(File.H, Rank.One);
            Piece a8 = GetPieceAt(File.A, Rank.Eight);
            Piece h8 = GetPieceAt(File.H, Rank.Eight);
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

        public Piece GetPieceAt(File file, Rank rank)
        {
            return Board[(int)rank][(int)file];
        }

        protected virtual void SetPieceAt(File file, Rank rank, Piece piece)
        {
            Board[(int)rank][(int)file] = piece;
        }

        public bool IsValidMove(Move move)
        {
            Utilities.ThrowIfNull(move, "move");
            return IsValidMove(move, true);
        }

        protected virtual bool IsValidMove(Move move, bool validateCheck)
        {
            Utilities.ThrowIfNull(move, "move");
            if (move.OriginalPosition.Equals(move.NewPosition))
                return false;
            Piece piece = GetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank);
            if (move.Player != WhoseTurn) return false;
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
            Rank rank = move.Player == Player.White ? Rank.One : Rank.Eight;
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
                PositionDistance pd = new PositionDistance(move.OriginalPosition, move.NewPosition);
                if (pd.DistanceX == 1 && pd.DistanceY == 1 && GetPieceAt(move.NewPosition) == null)
                { // en passant
                    isCapture = true;
                    SetPieceAt(move.NewPosition.File, move.OriginalPosition.Rank, null);
                }
                if (move.NewPosition.Rank == (move.Player == Player.White ? Rank.Eight : Rank.One))
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
                    if (move.OriginalPosition.File == File.A && move.OriginalPosition.Rank == Rank.One)
                        _whiteRookAMoved = true;
                    else if (move.OriginalPosition.File == File.H && move.OriginalPosition.Rank == Rank.One)
                        _whiteRookHMoved = true;
                }
                else
                {
                    if (move.OriginalPosition.File == File.A && move.OriginalPosition.Rank == Rank.Eight)
                        _blackRookAMoved = true;
                    else if (move.OriginalPosition.File == File.H && move.OriginalPosition.Rank == Rank.Eight)
                        _blackRookHMoved = true;
                }
            }
            if (isCapture)
            {
                type |= MoveType.Capture;
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
            for (int x = 0; x < Board.Length; x++)
            {
                for (int y = 0; y < Board[x].Length; y++)
                {
                    if (Board[x][y] != null && Board[x][y].Owner == player)
                    {
                        validMoves.AddRange(GetValidMoves(new Position((File)y, (Rank)x), returnIfAny));
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
            Position kingPos = new Position(File.None, Rank.None);

            for (int i = 0; i < Board.Length; i++)
            {
                for (int j = 0; j < Board[i].Length; j++)
                {
                    Piece curr = Board[i][j];
                    if (curr is King && curr.Owner == player)
                    {
                        kingPos = new Position((File)j, (Rank)i);
                    }
                }
            }

            if (kingPos.File == File.None)
                return false;

            return CanAnyPieceMoveTo(kingPos, false);
        }

        public virtual bool CanAnyPieceMoveTo(Position to, bool takeWhoseTurnInAccount)
        {
            List<Position> piecePositions = new List<Position>();

            for (int i = 0; i < Board.Length; i++)
            {
                for (int j = 0; j < Board[i].Length; j++)
                {
                    Piece curr = Board[i][j];
                    if (curr != null)
                    {
                        if (takeWhoseTurnInAccount && WhoseTurn != curr.Owner) continue;
                        piecePositions.Add(new Position((File)j, (Rank)i));
                    }
                }
            }

            ChessGame copyWhite = new ChessGame(Board, Player.White);
            ChessGame copyBlack = new ChessGame(Board, Player.Black);
            for (int i = 0; i < piecePositions.Count; i++)
            {
                Piece cp = GetPieceAt(piecePositions[i]);
                Player player = cp.Owner;
                Move move = new Move(piecePositions[i], to, player);
                List<Move> moves = new List<Move>();
                if (cp is Pawn && ((move.NewPosition.Rank == Rank.Eight && move.Player == Player.White) || (move.NewPosition.Rank == Rank.One && move.Player == Player.Black)))
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
                    if ((player == Player.White ? copyWhite : copyBlack).IsValidMove(m, false))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public virtual bool WouldBeInCheckAfter(Move move, Player player)
        {
            Utilities.ThrowIfNull(move, "move");
            ChessGame copy = new ChessGame(Board, player);
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
