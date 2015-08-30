using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public GameStatus Status
        {
            get;
            private set;
        }

        public Player WhoseTurn
        {
            get;
            private set;
        }

        protected ChessPiece[][] Board;
        public ChessPiece[][] GetBoard()
        {
            return CloneBoard(Board);
        }

        List<Move> _moves;
        public ReadOnlyCollection<Move> Moves
        {
            get
            {
                return new ReadOnlyCollection<Move>(_moves);
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
            Status = new GameStatus(GameEvent.None, Player.None, "No special event");
            Board = new ChessPiece[8][];
            WhoseTurn = Player.White;
            _moves = new List<Move>();
            InitBoard();
        }

        public ChessGame(ChessPiece[][] board, IEnumerable<Move> moves) :
            this(board, moves, true)
        {
        }

        protected ChessGame(ChessPiece[][] board, IEnumerable<Move> moves, bool validateCheck)
        {
            if (moves == null)
                throw new ArgumentNullException("moves");
            if (moves.Count() == 0)
                throw new ArgumentException("The Count of moves has to be greater than 0.");
            Board = CloneBoard(board);
            _moves = new List<Move>(moves);
            WhoseTurn = _moves.Last().Player == Player.White ? Player.Black : Player.White;
            foreach (Move m in _moves)
            {
                if (!_whiteKingMoved && m.Player == Player.White && m.OriginalPosition == new Position(File.E, Rank.One))
                    _whiteKingMoved = true;
                if (!_blackKingMoved && m.Player == Player.Black && m.OriginalPosition == new Position(File.E, Rank.Eight))
                    _blackKingMoved = true;
                if (!_whiteRookAMoved && m.Player == Player.White && m.OriginalPosition == new Position(File.A, Rank.One))
                    _whiteRookAMoved = true;
                if (!_whiteRookHMoved && m.Player == Player.White && m.OriginalPosition == new Position(File.H, Rank.One))
                    _whiteRookHMoved = true;
                if (!_blackRookAMoved && m.Player == Player.Black && m.OriginalPosition == new Position(File.A, Rank.Eight))
                    _blackRookAMoved = true;
                if (!_blackRookHMoved && m.Player == Player.Black && m.OriginalPosition == new Position(File.H, Rank.Eight))
                    _blackRookHMoved = true;
            }
            if (!validateCheck)
                return;
            ChangeStatus(moves.ElementAt(moves.Count() - 1).Player == Player.White ? Player.Black : Player.White, true);
        }

        public ChessGame(ChessPiece[][] board, Player whoseTurn) :
            this(board, whoseTurn, true)
        {
        }

        protected ChessGame(ChessPiece[][] board, Player whoseTurn, bool validateCheck)
        {
            Board = CloneBoard(board);
            _moves = new List<Move>();
            WhoseTurn = whoseTurn;
            ChessPiece e1 = GetPieceAt(File.E, Rank.One);
            ChessPiece e8 = GetPieceAt(File.E, Rank.Eight);
            ChessPiece a1 = GetPieceAt(File.A, Rank.One);
            ChessPiece h1 = GetPieceAt(File.H, Rank.One);
            ChessPiece a8 = GetPieceAt(File.A, Rank.Eight);
            ChessPiece h8 = GetPieceAt(File.H, Rank.Eight);
            if (e1.Piece != Piece.King || e1.Player != Player.White)
                _whiteKingMoved = true;
            if (e8.Piece != Piece.King || e8.Player != Player.Black)
                _blackKingMoved = true;
            if (a1.Piece != Piece.Rook || a1.Player != Player.White)
                _whiteRookAMoved = true;
            if (h1.Piece != Piece.Rook || h1.Player != Player.White)
                _whiteRookHMoved = true;
            if (a8.Piece != Piece.Rook || a8.Player != Player.Black)
                _blackRookAMoved = true;
            if (h8.Piece != Piece.Rook || h8.Player != Player.Black)
                _blackRookHMoved = true;
            if (!validateCheck)
                return;
            ChangeStatus(whoseTurn, true);
        }

        public void InitBoard()
        {
            ChessPiece kw = new ChessPiece(Piece.King, Player.White);
            ChessPiece kb = new ChessPiece(Piece.King, Player.Black);
            ChessPiece qw = new ChessPiece(Piece.Queen, Player.White);
            ChessPiece qb = new ChessPiece(Piece.Queen, Player.Black);
            ChessPiece rw = new ChessPiece(Piece.Rook, Player.White);
            ChessPiece rb = new ChessPiece(Piece.Rook, Player.Black);
            ChessPiece nw = new ChessPiece(Piece.Knight, Player.White);
            ChessPiece nb = new ChessPiece(Piece.Knight, Player.Black);
            ChessPiece bw = new ChessPiece(Piece.Bishop, Player.White);
            ChessPiece bb = new ChessPiece(Piece.Bishop, Player.Black);
            ChessPiece pw = new ChessPiece(Piece.Pawn, Player.White);
            ChessPiece pb = new ChessPiece(Piece.Pawn, Player.Black);
            ChessPiece o = ChessPiece.None;
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

        protected void ChangeStatus(Player playerToValidate, bool validateHasAnyValidMoves)
        {
            Utilities.ThrowIfNull(playerToValidate, "playerToValidate");
            Status = new GameStatus(GameEvent.None, Player.None, "No special event");
            Player other = playerToValidate == Player.White ? Player.Black : Player.White;
            if (IsInCheck(playerToValidate))
            {
                if (validateHasAnyValidMoves && !HasAnyValidMoves(playerToValidate))
                {
                    Status = new GameStatus(GameEvent.Checkmate, other, playerToValidate.ToString() + " is checkmated");
                }
                else
                {
                    Status = new GameStatus(GameEvent.Check, other, playerToValidate.ToString() + " is in check");
                }
            }
            else if (validateHasAnyValidMoves && !HasAnyValidMoves(playerToValidate))
            {
                Status = new GameStatus(GameEvent.Stalemate, other, "Stalemate");
            }
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

        protected void SetPieceAt(File file, Rank rank, ChessPiece piece)
        {
            Utilities.ThrowIfNull(piece, "piece");
            Board[(int)rank][(int)file] = piece;
        }

        public bool IsValidMove(Move move)
        {
            Utilities.ThrowIfNull(move, "move");
            return IsValidMove(move, true);
        }

        protected bool IsValidMoveKing(Move move)
        {
            Utilities.ThrowIfNull(move, "move");
            PositionDistance distance = new PositionDistance(move.OriginalPosition, move.NewPosition);
            if ((distance.DistanceX != 1 || distance.DistanceY != 1)
                        && (distance.DistanceX != 0 || distance.DistanceY != 1)
                        && (distance.DistanceX != 1 || distance.DistanceY != 0)
                        && (distance.DistanceX != 2 || distance.DistanceY != 0))
                return false;
            if (distance.DistanceX != 2)
                return true;
            return CanCastle(move);
        }

        protected bool CanCastle(Move move)
        {
            Utilities.ThrowIfNull(move, "move");
            if (move.Player == Player.White)
            {
                if (move.OriginalPosition.File != File.E || move.OriginalPosition.Rank != Rank.One)
                    return false;
                if (_whiteKingMoved || (Status.Event == GameEvent.Check && Status.PlayerWhoCausedEvent == Player.Black))
                    return false;
                if (move.NewPosition.File == File.C)
                {
                    if (_whiteRookAMoved || GetPieceAt(File.D, Rank.One).Piece != Piece.None
                        || GetPieceAt(File.C, Rank.One).Piece != Piece.None
                        || GetPieceAt(File.B, Rank.One).Piece != Piece.None
                        || WouldBeInCheckAfter(new Move(new Position(File.E, Rank.One), new Position(File.D, Rank.One), Player.White), Player.White)
                        || WouldBeInCheckAfter(new Move(new Position(File.E, Rank.One), new Position(File.C, Rank.One), Player.White), Player.White))
                        return false;
                }
                else
                {
                    if (_whiteRookHMoved || GetPieceAt(File.F, Rank.One).Piece != Piece.None
                        || GetPieceAt(File.G, Rank.One).Piece != Piece.None
                        || WouldBeInCheckAfter(new Move(new Position(File.E, Rank.One), new Position(File.F, Rank.One), Player.White), Player.White)
                        || WouldBeInCheckAfter(new Move(new Position(File.E, Rank.One), new Position(File.G, Rank.One), Player.White), Player.White))
                        return false;
                }
            }
            else
            {
                if (move.OriginalPosition.File != File.E || move.OriginalPosition.Rank != Rank.Eight)
                    return false;
                if (_blackKingMoved || (Status.Event == GameEvent.Check && Status.PlayerWhoCausedEvent == Player.White))
                    return false;
                if (move.NewPosition.File == File.C)
                {
                    if (_blackRookAMoved || GetPieceAt(File.D, Rank.Eight).Piece != Piece.None
                        || GetPieceAt(File.C, Rank.Eight).Piece != Piece.None
                        || GetPieceAt(File.B, Rank.Eight).Piece != Piece.None
                        || WouldBeInCheckAfter(new Move(new Position(File.E, Rank.Eight), new Position(File.D, Rank.Eight), Player.Black), Player.Black)
                        || WouldBeInCheckAfter(new Move(new Position(File.E, Rank.Eight), new Position(File.C, Rank.Eight), Player.Black), Player.Black))
                        return false;
                }
                else
                {
                    if (_blackRookHMoved || GetPieceAt(File.F, Rank.Eight).Piece != Piece.None
                        || GetPieceAt(File.G, Rank.Eight).Piece != Piece.None
                        || WouldBeInCheckAfter(new Move(new Position(File.E, Rank.Eight), new Position(File.F, Rank.Eight), Player.Black), Player.Black)
                        || WouldBeInCheckAfter(new Move(new Position(File.E, Rank.Eight), new Position(File.G, Rank.Eight), Player.Black), Player.Black))
                        return false;
                }
            }
            return true;
        }

        protected bool IsValidMovePawn(Move move)
        {
            Utilities.ThrowIfNull(move, "move");
            PositionDistance posDelta = new PositionDistance(move.OriginalPosition, move.NewPosition);
            if ((posDelta.DistanceX != 0 || posDelta.DistanceY != 1) && (posDelta.DistanceX != 1 || posDelta.DistanceY != 1)
                        && (posDelta.DistanceX != 0 || posDelta.DistanceY != 2))
                return false;
            if (move.Player == Player.White)
            {
                if ((int)move.OriginalPosition.Rank < (int)move.NewPosition.Rank)
                    return false;
                if (move.NewPosition.Rank == Rank.Eight && move.Promotion == Piece.None)
                    return false;
            }
            if (move.Player == Player.Black)
            {
                if ((int)move.OriginalPosition.Rank > (int)move.NewPosition.Rank)
                    return false;
                if (move.NewPosition.Rank == Rank.One && move.Promotion == Piece.None)
                    return false;
            }
            bool checkEnPassant = false;
            if (posDelta.DistanceY == 2)
            {
                if ((move.OriginalPosition.Rank != Rank.Two && move.Player == Player.White)
                    || (move.OriginalPosition.Rank != Rank.Seven && move.Player == Player.Black))
                    return false;
                if (move.OriginalPosition.Rank == Rank.Two && GetPieceAt(move.OriginalPosition.File, Rank.Three).Piece != Piece.None)
                    return false;
                if (move.OriginalPosition.Rank == Rank.Seven && GetPieceAt(move.OriginalPosition.File, Rank.Six).Piece != Piece.None)
                    return false;
            }
            if (posDelta.DistanceX == 0 && (posDelta.DistanceY == 1 || posDelta.DistanceY == 2))
            {
                if (GetPieceAt(move.NewPosition).Player != Player.None)
                    return false;
            }
            else
            {
                if (GetPieceAt(move.NewPosition).Player != (move.Player == Player.White ? Player.Black : Player.White))
                    checkEnPassant = true;
                if (GetPieceAt(move.NewPosition).Player == move.Player)
                    return false;
            }
            if (checkEnPassant)
            {
                if (_moves.Count == 0)
                {
                    return false;
                }
                if ((move.OriginalPosition.Rank != Rank.Five && move.Player == Player.White)
                    || (move.OriginalPosition.Rank != Rank.Four && move.Player == Player.Black))
                    return false;
                Move latestMove = _moves[_moves.Count - 1];
                if (latestMove.Player != (move.Player == Player.White ? Player.Black : Player.White))
                    return false;
                if (move.Player == Player.White)
                {
                    if (latestMove.OriginalPosition.Rank != Rank.Seven || latestMove.NewPosition.Rank != Rank.Five)
                        return false;
                }
                else // (m.Player == Players.Black)
                {
                    if (latestMove.OriginalPosition.Rank != Rank.Two || latestMove.NewPosition.Rank != Rank.Four)
                        return false;
                }
                if (move.NewPosition.File != latestMove.NewPosition.File)
                    return false;
            }
            return true;
        }

        protected bool IsValidMoveRook(Move move)
        {
            Utilities.ThrowIfNull(move, "move");
            PositionDistance posDelta = new PositionDistance(move.OriginalPosition, move.NewPosition);
            if (posDelta.DistanceX != 0 && posDelta.DistanceY != 0)
                return false;
            bool increasingRank = (int)move.NewPosition.Rank > (int)move.OriginalPosition.Rank;
            bool increasingFile = (int)move.NewPosition.File > (int)move.OriginalPosition.File;
            if (posDelta.DistanceX == 0)
            {
                int f = (int)move.OriginalPosition.File;
                for (int r = (int)move.OriginalPosition.Rank + (increasingRank ? 1 : -1);
                    increasingRank ? r < (int)move.NewPosition.Rank : r > (int)move.NewPosition.Rank;
                    r += increasingRank ? 1 : -1)
                {
                    if (Board[r][f].Player != Player.None)
                    {
                        return false;
                    }
                }
            }
            else // (posDelta.DeltaY == 0)
            {
                int r = (int)move.OriginalPosition.Rank;
                for (int f = (int)move.OriginalPosition.File + (increasingFile ? 1 : -1);
                    increasingFile ? f < (int)move.NewPosition.File : f > (int)move.NewPosition.File;
                    f += increasingFile ? 1 : -1)
                {
                    if (Board[r][f].Player != Player.None)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected bool IsValidMoveBishop(Move move)
        {
            Utilities.ThrowIfNull(move, "move");
            PositionDistance posDelta = new PositionDistance(move.OriginalPosition, move.NewPosition);
            if (posDelta.DistanceX != posDelta.DistanceY)
                return false;
            bool increasingRank = (int)move.NewPosition.Rank > (int)move.OriginalPosition.Rank;
            bool increasingFile = (int)move.NewPosition.File > (int)move.OriginalPosition.File;
            for (int f = (int)move.OriginalPosition.File + (increasingFile ? 1 : -1), r = (int)move.OriginalPosition.Rank + (increasingRank ? 1 : -1);
                 increasingFile ? f < (int)move.NewPosition.File : f > (int)move.NewPosition.File;
                 f += increasingFile ? 1 : -1, r += increasingRank ? 1 : -1)
            {
                if (Board[r][f].Player != Player.None)
                {
                    return false;
                }
            }
            return true;
        }

        protected bool IsValidMoveKnight(Move move)
        {
            Utilities.ThrowIfNull(move, "move");
            PositionDistance posDelta = new PositionDistance(move.OriginalPosition, move.NewPosition);
            if ((posDelta.DistanceX != 2 || posDelta.DistanceY != 1) && (posDelta.DistanceX != 1 || posDelta.DistanceY != 2))
                return false;
            return true;
        }

        protected bool IsValidMoveQueen(Move move)
        {
            Utilities.ThrowIfNull(move, "move");
            return IsValidMoveBishop(move) || IsValidMoveRook(move);
        }

        protected bool IsValidMove(Move move, bool validateCheck)
        {
            Utilities.ThrowIfNull(move, "move");
            if (move.OriginalPosition.Equals(move.NewPosition))
                return false;
            ChessPiece piece = GetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank);
            if (move.Player != WhoseTurn) return false;
            if (piece.Player != move.Player) return false;
            if (GetPieceAt(move.NewPosition).Player == move.Player)
            {
                return false;
            }
            PositionDistance posDelta = new PositionDistance(move.OriginalPosition, move.NewPosition);
            switch (piece.Piece)
            {
                case Piece.King:
                    if (!IsValidMoveKing(move))
                        return false;
                    break;
                case Piece.Pawn:
                    if (!IsValidMovePawn(move))
                        return false;
                    break;
                case Piece.Queen:
                    if (!IsValidMoveQueen(move))
                        return false;
                    break;
                case Piece.Rook:
                    if (!IsValidMoveRook(move))
                        return false;
                    break;
                case Piece.Bishop:
                    if (!IsValidMoveBishop(move))
                        return false;
                    break;
                case Piece.Knight:
                    if (!IsValidMoveKnight(move))
                        return false;
                    break;
                default:
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

        protected bool ApplyMove(Move move, bool alreadyValidated, bool validateHasAnyValidMoves)
        {
            Utilities.ThrowIfNull(move, "move");
            if (!alreadyValidated && !IsValidMove(move))
                return false;
            ChessPiece movingPiece = GetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank);
            ChessPiece newPiece = movingPiece;
            if (movingPiece.Piece == Piece.Pawn)
            {
                PositionDistance pd = new PositionDistance(move.OriginalPosition, move.NewPosition);
                if (pd.DistanceX == 1 && pd.DistanceY == 1 && GetPieceAt(move.NewPosition).Piece == Piece.None)
                { // en passant
                    SetPieceAt(move.NewPosition.File, move.OriginalPosition.Rank, ChessPiece.None);
                }
                if (move.NewPosition.Rank == (move.Player == Player.White ? Rank.Eight : Rank.One))
                {
                    newPiece = new ChessPiece(move.Promotion, move.Player);
                }
            }
            else if (movingPiece.Piece == Piece.King && movingPiece.Player == Player.White)
            {
                _whiteKingMoved = true;
            }
            else if (movingPiece.Piece == Piece.King && movingPiece.Player == Player.Black)
            {
                _blackKingMoved = true;
            }
            else if (movingPiece.Piece == Piece.Rook && move.OriginalPosition.File == File.A && move.OriginalPosition.Rank == Rank.One && move.Player == Player.White)
            {
                _whiteRookAMoved = true;
            }
            else if (movingPiece.Piece == Piece.Rook && move.OriginalPosition.File == File.H && move.OriginalPosition.Rank == Rank.One && move.Player == Player.White)
            {
                _whiteRookHMoved = true;
            }
            else if (movingPiece.Piece == Piece.Rook && move.OriginalPosition.File == File.A && move.OriginalPosition.Rank == Rank.Eight && move.Player == Player.Black)
            {
                _blackRookAMoved = true;
            }
            else if (movingPiece.Piece == Piece.Rook && move.OriginalPosition.File == File.H && move.OriginalPosition.Rank == Rank.Eight && move.Player == Player.Black)
            {
                _blackRookHMoved = true;
            }
            SetPieceAt(move.NewPosition.File, move.NewPosition.Rank, newPiece);
            SetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank, ChessPiece.None);
            WhoseTurn = move.Player == Player.White ? Player.Black : Player.White;
            if (movingPiece.Piece == Piece.King && new PositionDistance(move.OriginalPosition, move.NewPosition).DistanceX == 2)
            {
                Rank rank = move.Player == Player.White ? Rank.One : Rank.Eight;
                File rookFile = move.NewPosition.File == File.C ? File.A : File.H;
                File newRookFile = move.NewPosition.File == File.C ? File.D : File.F;
                SetPieceAt(newRookFile, rank, new ChessPiece(Piece.Rook, move.Player));
                SetPieceAt(rookFile, rank, ChessPiece.None);
            }
            _moves.Add(move);
            Player other = move.Player == Player.White ? Player.Black : Player.White;
            ChangeStatus(other, validateHasAnyValidMoves);
            return true;
        }

        public ReadOnlyCollection<Move> GetValidMoves(Position from)
        {
            Utilities.ThrowIfNull(from, "from");
            return GetValidMoves(from, false);
        }

        protected ReadOnlyCollection<Move> GetValidMovesKing(Position from, bool returnIfAny)
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

        protected ReadOnlyCollection<Move> GetValidMovesPawn(Position from, bool returnIfAny)
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
                if (IsValidMove(move))
                {
                    validMoves.Add(move);
                    if (returnIfAny)
                        return new ReadOnlyCollection<Move>(validMoves);
                }
            }
            return new ReadOnlyCollection<Move>(validMoves);
        }

        protected ReadOnlyCollection<Move> GetValidMovesKnight(Position from, bool returnIfAny)
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

        protected ReadOnlyCollection<Move> GetValidMovesRook(Position from, bool returnIfAny)
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

        protected ReadOnlyCollection<Move> GetValidMovesBishop(Position from, bool returnIfAny)
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

        protected ReadOnlyCollection<Move> GetValidMovesQueen(Position from, bool returnIfAny)
        {
            Utilities.ThrowIfNull(from, "from");
            ReadOnlyCollection<Move> horizontalVerticalMoves = GetValidMovesRook(from, returnIfAny);
            if (returnIfAny && horizontalVerticalMoves.Count > 0)
                return horizontalVerticalMoves;
            ReadOnlyCollection<Move> diagonalMoves = GetValidMovesBishop(from, returnIfAny);
            return new ReadOnlyCollection<Move>(horizontalVerticalMoves.Concat(diagonalMoves).ToList());
        }

        protected ReadOnlyCollection<Move> GetValidMoves(Position from, bool returnIfAny)
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

        protected ReadOnlyCollection<Move> GetValidMoves(Player player, bool returnIfAny)
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

        protected bool HasAnyValidMoves(Position from)
        {
            Utilities.ThrowIfNull(from, "from");
            ReadOnlyCollection<Move> validMoves = GetValidMoves(from, true);
            return validMoves.Count > 0;
        }

        protected bool HasAnyValidMoves(Player player)
        {
            ReadOnlyCollection<Move> validMoves = GetValidMoves(player, true);
            return validMoves.Count > 0;
        }

        protected bool IsInCheck(Player player)
        {
            List<Position> piecePositions = new List<Position>();
            Position kingPos = new Position(File.None, Rank.None);

            for (int i = 0; i < Board.Length; i++)
            {
                for (int j = 0; j < Board[i].Length; j++)
                {
                    ChessPiece curr = Board[i][j];
                    if (curr.Piece != Piece.None && curr.Player == (player == Player.White ? Player.Black : Player.White))
                    {
                        piecePositions.Add(new Position((File)j, (Rank)i));
                    }
                    else if (curr.Piece == Piece.King && curr.Player == player)
                    {
                        kingPos = new Position((File)j, (Rank)i);
                    }
                }
            }

            if (kingPos.File == File.None)
                return false;

            ChessGame copy = new ChessGame(Board, player == Player.White ? Player.Black : Player.White, false);
            for (int i = 0; i < piecePositions.Count; i++)
            {
                if (copy.IsValidMove(new Move(piecePositions[i], kingPos, player == Player.White ? Player.Black : Player.White), false))
                {
                    return true;
                }
            }

            return false;
        }

        protected bool WouldBeInCheckAfter(Move move, Player player)
        {
            Utilities.ThrowIfNull(move, "move");
            ChessGame copy = new ChessGame(Board, player, false);
            copy.ApplyMove(move, true, false);
            copy.ChangeStatus(player, false);
            return copy.Status.Event == GameEvent.Check && copy.Status.PlayerWhoCausedEvent != player;
        }

        public void Draw(string reason)
        {
            Status = new GameStatus(GameEvent.Draw, Player.None, reason);
        }

        public void Resign(Player player)
        {
            if (player == Player.None)
                throw new ArgumentException("player cannot be None.");
            Status = new GameStatus(GameEvent.Resign, player, player.ToString() + " resigned");
        }
    }
}
