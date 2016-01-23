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

        public ReadOnlyCollection<ChessPiece> PiecesOnBoard
        {
            get
            {
                return new ReadOnlyCollection<ChessPiece>(Board.SelectMany(x => x).Where(x => x != null).ToList());
            }
        }

        protected ChessPiece[][] Board;
        public ChessPiece[][] GetBoard()
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

        protected static ChessPiece[][] CloneBoard(ChessPiece[][] originalBoard)
        {
            Utilities.ThrowIfNull(originalBoard, "originalBoard");
            ChessPiece[][] newBoard = new ChessPiece[originalBoard.Length][];
            for (int i = 0; i < originalBoard.Length; i++)
            {
                newBoard[i] = new ChessPiece[originalBoard[i].Length];
                Array.Copy(originalBoard[i], newBoard[i], originalBoard[i].Length);
            }
            return newBoard;
        }

        public ChessGame()
        {
            WhoseTurn = Player.White;
            _moves = new List<DetailedMove>();
            Board = new ChessPiece[8][];
            ChessPiece kw = new King(Player.White);
            ChessPiece kb = new King(Player.Black);
            ChessPiece qw = new Queen(Player.White);
            ChessPiece qb = new Queen(Player.Black);
            ChessPiece rw = new Rook(Player.White);
            ChessPiece rb = new Rook(Player.Black);
            ChessPiece nw = new Knight(Player.White);
            ChessPiece nb = new Knight(Player.Black);
            ChessPiece bw = new Bishop(Player.White);
            ChessPiece bb = new Bishop(Player.Black);
            ChessPiece pw = new Pawn(Player.White);
            ChessPiece pb = new Pawn(Player.Black);
            ChessPiece o = null;
            Board = new ChessPiece[8][]
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
                if (!ApplyMove(m, movesAreValidated))
                {
                    throw new ArgumentException("Invalid move passed to ChessGame constructor.");
                }
            }
        }

        public ChessGame(ChessPiece[][] board, Player whoseTurn)
        {
            Board = CloneBoard(board);
            _moves = new List<DetailedMove>();
            WhoseTurn = whoseTurn;
            ChessPiece e1 = GetPieceAt(File.E, Rank.One);
            ChessPiece e8 = GetPieceAt(File.E, Rank.Eight);
            ChessPiece a1 = GetPieceAt(File.A, Rank.One);
            ChessPiece h1 = GetPieceAt(File.H, Rank.One);
            ChessPiece a8 = GetPieceAt(File.A, Rank.Eight);
            ChessPiece h8 = GetPieceAt(File.H, Rank.Eight);
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
            if (!(a1 is Rook) || h8.Owner != Player.Black)
                _blackRookHMoved = true;
        }

        public virtual float GetRelativePieceValue(Player player)
        {
            return PiecesOnBoard.Where(x => x.Owner == player && !(x is King))
                                .Select(y => y.GetRelativePieceValue())
                                .Sum();
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

        public ChessPiece GetPieceAt(Position position)
        {
            Utilities.ThrowIfNull(position, "position");
            return GetPieceAt(position.File, position.Rank);
        }

        public ChessPiece GetPieceAt(File file, Rank rank)
        {
            return Board[(int)rank][(int)file];
        }

        protected virtual void SetPieceAt(File file, Rank rank, ChessPiece piece)
        {
            Utilities.ThrowIfNull(piece, "piece");
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
            ChessPiece piece = GetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank);
            if (move.Player != WhoseTurn) return false;
            if (piece.Owner != move.Player) return false;
            if (GetPieceAt(move.NewPosition).Owner == move.Player)
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

        public bool ApplyMove(Move move, bool alreadyValidated)
        {
            Utilities.ThrowIfNull(move, "move");
            return ApplyMove(move, alreadyValidated, true);
        }

        protected virtual bool ApplyMove(Move move, bool alreadyValidated, bool validateHasAnyValidMoves)
        {
            Utilities.ThrowIfNull(move, "move");
            if (!alreadyValidated && !IsValidMove(move))
                return false;
            ChessPiece movingPiece = GetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank);
            ChessPiece capturedPiece = GetPieceAt(move.NewPosition.File, move.NewPosition.Rank);
            ChessPiece newPiece = movingPiece;
            bool isCapture = capturedPiece.Piece != Piece.None;
            CastlingType castle = CastlingType.None;
            if (movingPiece.Piece == Piece.Pawn)
            {
                PositionDistance pd = new PositionDistance(move.OriginalPosition, move.NewPosition);
                if (pd.DistanceX == 1 && pd.DistanceY == 1 && GetPieceAt(move.NewPosition).Piece == Piece.None)
                { // en passant
                    isCapture = true;
                    SetPieceAt(move.NewPosition.File, move.OriginalPosition.Rank, ChessPiece.None);
                }
                if (move.NewPosition.Rank == (move.Player == Player.White ? Rank.Eight : Rank.One))
                {
                    newPiece = new ChessPiece(move.Promotion, move.Player);
                }
            }
            else if (movingPiece.Piece == Piece.King)
            {
                if (movingPiece.Player == Player.White)
                    _whiteKingMoved = true;
                else
                    _blackKingMoved = true;

                if (new PositionDistance(move.OriginalPosition, move.NewPosition).DistanceX == 2)
                {
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
                    SetPieceAt(newRookFile, rank, new ChessPiece(Piece.Rook, move.Player));
                    SetPieceAt(rookFile, rank, ChessPiece.None);
                }
            }
            else if (movingPiece.Piece == Piece.Rook)
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
            SetPieceAt(move.NewPosition.File, move.NewPosition.Rank, newPiece);
            SetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank, ChessPiece.None);
            WhoseTurn = Utilities.GetOpponentOf(move.Player);
            _moves.Add(new DetailedMove(move, movingPiece.Piece, isCapture, castle));
            return true;
        }

        public ReadOnlyCollection<Move> GetValidMoves(Position from)
        {
            Utilities.ThrowIfNull(from, "from");
            return GetValidMoves(from, false);
        }

        protected virtual ReadOnlyCollection<Move> GetValidMovesKing(Position from, bool returnIfAny)
        {
            Utilities.ThrowIfNull(from, "from");
            List<Move> validMoves = new List<Move>();
            ChessPiece cp = GetPieceAt(from);
            int l0 = Board.Length;
            int l1 = Board[0].Length;
            int[][] directions = new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { -1, 0 },
                        new int[] { 1, 1 }, new int[] { 1, -1 }, new int[] { -1, 1 }, new int[] { -1, -1 } };
            foreach (int[] dir in directions)
            {
                if ((int)from.File + dir[0] < 0 || (int)from.File + dir[0] >= l1
                    || (int)from.Rank + dir[1] < 0 || (int)from.Rank + dir[1] >= l0)
                    continue;
                Move move = new Move(from, new Position(from.File + dir[0], from.Rank + dir[1]), cp.Player);
                if (IsValidMove(move))
                {
                    validMoves.Add(move);
                    if (returnIfAny)
                        return new ReadOnlyCollection<Move>(validMoves);
                }
            }
            return new ReadOnlyCollection<Move>(validMoves);
        }

        protected virtual ReadOnlyCollection<Move> GetValidMovesPawn(Position from, bool returnIfAny)
        {
            Utilities.ThrowIfNull(from, "from");
            List<Move> validMoves = new List<Move>();
            ChessPiece cp = GetPieceAt(from);
            int l0 = Board.Length;
            int l1 = Board[0].Length;
            int[][] directions;
            if (cp.Player == Player.Black)
            {
                directions = new int[][] { new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 1 }, new int[] { -1, 1 } };
            }
            else
            {
                directions = new int[][] { new int[] { 0, -1 }, new int[] { 0, -2 }, new int[] { -1, -1 }, new int[] { 1, -1 } };
            }
            foreach (int[] dir in directions)
            {
                if ((int)from.File + dir[0] < 0 || (int)from.File + dir[0] >= l1
                    || (int)from.Rank + dir[1] < 0 || (int)from.Rank + dir[1] >= l0)
                    continue;
                Move move = new Move(from, new Position(from.File + dir[0], from.Rank + dir[1]), cp.Player);
                List<Move> moves = new List<Move>();
                if ((move.NewPosition.Rank == Rank.Eight && move.Player == Player.White) || (move.NewPosition.Rank == Rank.One && move.Player == Player.Black))
                {
                    moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, Piece.Queen));
                    moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, Piece.Rook));
                    moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, Piece.Knight));
                    moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, Piece.Bishop));
                }
                else
                {
                    moves.Add(move);
                }
                foreach (Move m in moves)
                {
                    if (IsValidMove(m))
                    {
                        validMoves.Add(m);
                        if (returnIfAny)
                            return new ReadOnlyCollection<Move>(validMoves);
                    }
                }
            }
            return new ReadOnlyCollection<Move>(validMoves);
        }

        protected virtual ReadOnlyCollection<Move> GetValidMovesKnight(Position from, bool returnIfAny)
        {
            Utilities.ThrowIfNull(from, "from");
            List<Move> validMoves = new List<Move>();
            ChessPiece cp = GetPieceAt(from);
            int l0 = Board.Length;
            int l1 = Board[0].Length;
            int[][] directions = new int[][] { new int[] { 2, 1 }, new int[] { -2, -1 }, new int[] { 1, 2 }, new int[] { -1, -2 },
                        new int[] { 1, -2 }, new int[] { -1, 2 }, new int[] { 2, -1 }, new int[] { -2, 1 } };
            foreach (int[] dir in directions)
            {
                if ((int)from.File + dir[0] < 0 || (int)from.File + dir[0] >= l1
                    || (int)from.Rank + dir[1] < 0 || (int)from.Rank + dir[1] >= l0)
                    continue;
                Move move = new Move(from, new Position(from.File + dir[0], from.Rank + dir[1]), cp.Player);
                if (IsValidMove(move))
                {
                    validMoves.Add(move);
                    if (returnIfAny)
                        return new ReadOnlyCollection<Move>(validMoves);
                }
            }
            return new ReadOnlyCollection<Move>(validMoves);
        }

        protected virtual ReadOnlyCollection<Move> GetValidMovesRook(Position from, bool returnIfAny)
        {
            Utilities.ThrowIfNull(from, "from");
            List<Move> validMoves = new List<Move>();
            ChessPiece cp = GetPieceAt(from);
            int l0 = Board.Length;
            int l1 = Board[0].Length;
            for (int i = -7; i < 8; i++)
            {
                if (i == 0)
                    continue;
                if ((int)from.Rank + i > -1 && (int)from.Rank + i < l0)
                {
                    Move move = new Move(from, new Position(from.File, from.Rank + i), cp.Player);
                    if (IsValidMove(move))
                    {
                        validMoves.Add(move);
                        if (returnIfAny)
                            return new ReadOnlyCollection<Move>(validMoves);
                    }
                }
                if ((int)from.File + i > -1 && (int)from.File + i < l1)
                {
                    Move move = new Move(from, new Position(from.File + i, from.Rank), cp.Player);
                    if (IsValidMove(move))
                    {
                        validMoves.Add(move);
                        if (returnIfAny)
                            return new ReadOnlyCollection<Move>(validMoves);
                    }
                }
            }
            return new ReadOnlyCollection<Move>(validMoves);
        }

        protected virtual ReadOnlyCollection<Move> GetValidMovesBishop(Position from, bool returnIfAny)
        {
            Utilities.ThrowIfNull(from, "from");
            List<Move> validMoves = new List<Move>();
            ChessPiece cp = GetPieceAt(from);
            int l0 = Board.Length;
            int l1 = Board[0].Length;
            for (int i = -7; i < 8; i++)
            {
                if (i == 0)
                    continue;
                if ((int)from.Rank + i > -1 && (int)from.Rank + i < l0
                    && (int)from.File + i > -1 && (int)from.File + i < l1)
                {
                    Move move = new Move(from, new Position(from.File + i, from.Rank + i), cp.Player);
                    if (IsValidMove(move))
                    {
                        validMoves.Add(move);
                        if (returnIfAny)
                            return new ReadOnlyCollection<Move>(validMoves);
                    }
                }
                if ((int)from.Rank - i > -1 && (int)from.Rank - i < l0
                    && (int)from.File + i > -1 && (int)from.File + i < l1)
                {
                    Move move = new Move(from, new Position(from.File + i, from.Rank - i), cp.Player);
                    if (IsValidMove(move))
                    {
                        validMoves.Add(move);
                        if (returnIfAny)
                            return new ReadOnlyCollection<Move>(validMoves);
                    }
                }
            }
            return new ReadOnlyCollection<Move>(validMoves);
        }

        protected virtual ReadOnlyCollection<Move> GetValidMovesQueen(Position from, bool returnIfAny)
        {
            Utilities.ThrowIfNull(from, "from");
            ReadOnlyCollection<Move> horizontalVerticalMoves = GetValidMovesRook(from, returnIfAny);
            if (returnIfAny && horizontalVerticalMoves.Count > 0)
                return horizontalVerticalMoves;
            ReadOnlyCollection<Move> diagonalMoves = GetValidMovesBishop(from, returnIfAny);
            return new ReadOnlyCollection<Move>(horizontalVerticalMoves.Concat(diagonalMoves).ToList());
        }

        protected virtual ReadOnlyCollection<Move> GetValidMoves(Position from, bool returnIfAny)
        {
            Utilities.ThrowIfNull(from, "from");
            ChessPiece cp = GetPieceAt(from);
            if (cp.Player != WhoseTurn) return new ReadOnlyCollection<Move>(new List<Move>());
            Piece piece = cp.Piece;
            switch (piece)
            {
                case Piece.King:
                    return GetValidMovesKing(from, returnIfAny);
                case Piece.Pawn:
                    return GetValidMovesPawn(from, returnIfAny);
                case Piece.Knight:
                    return GetValidMovesKnight(from, returnIfAny);
                case Piece.Rook:
                    return GetValidMovesRook(from, returnIfAny);
                case Piece.Bishop:
                    return GetValidMovesBishop(from, returnIfAny);
                case Piece.Queen:
                    return GetValidMovesQueen(from, returnIfAny);
                default:
                    return new ReadOnlyCollection<Move>(new List<Move>());
            }
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
                    if (Board[x][y].Player == player)
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
                    ChessPiece curr = Board[i][j];
                    if (curr.Piece == Piece.King && curr.Player == player)
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
                    ChessPiece curr = Board[i][j];
                    if (curr.Piece != Piece.None)
                    {
                        if (takeWhoseTurnInAccount && WhoseTurn != curr.Player) continue;
                        piecePositions.Add(new Position((File)j, (Rank)i));
                    }
                }
            }

            ChessGame copyWhite = new ChessGame(Board, Player.White);
            ChessGame copyBlack = new ChessGame(Board, Player.Black);
            for (int i = 0; i < piecePositions.Count; i++)
            {
                ChessPiece cp = GetPieceAt(piecePositions[i]);
                Player player = cp.Player;
                Move move = new Move(piecePositions[i], to, player);
                List<Move> moves = new List<Move>();
                if (cp.Piece == Piece.Pawn && ((move.NewPosition.Rank == Rank.Eight && move.Player == Player.White) || (move.NewPosition.Rank == Rank.One && move.Player == Player.Black)))
                {
                    moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, Piece.Queen));
                    moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, Piece.Rook));
                    moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, Piece.Bishop));
                    moves.Add(new Move(move.OriginalPosition, move.NewPosition, move.Player, Piece.Knight));
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
            copy.ApplyMove(move, true, false);
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
