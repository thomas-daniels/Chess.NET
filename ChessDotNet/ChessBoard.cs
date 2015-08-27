using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessDotNet
{
    public class Chessboard
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

        public ChessPiece[,] Board
        {
            get;
            private set;
        }

        public List<Move> Moves
        {
            get;
            private set;
        }

        public Chessboard()
        {
            Status = new GameStatus(GameStatus.Events.None, Players.None, "No special event");
            Board = new ChessPiece[8, 8];
            Moves = new List<Move>();
            InitBoard();
        }

        public Chessboard(ChessPiece[,] board, List<Move> moves) :
            this(board, moves, true)
        {
        }

        protected Chessboard(ChessPiece[,] board, List<Move> moves, bool validateCheck)
        {
            if (moves == null)
                throw new ArgumentNullException("moves");
            if (moves.Count == 0)
                throw new ArgumentException("The Count of moves has to be greater than 0.");
            Board = (ChessPiece[,])board.Clone();
            Moves = new List<Move>(moves);
            foreach (Move m in Moves)
            {
                if (!_whiteKingMoved && m.Player == Players.White && m.OriginalPosition == new Position(Position.Files.E, Position.Ranks.One))
                    _whiteKingMoved = true;
                if (!_blackKingMoved && m.Player == Players.Black && m.OriginalPosition == new Position(Position.Files.E, Position.Ranks.Eight))
                    _blackKingMoved = true;
                if (!_whiteRookAMoved && m.Player == Players.White && m.OriginalPosition == new Position(Position.Files.A, Position.Ranks.One))
                    _whiteRookAMoved = true;
                if (!_whiteRookHMoved && m.Player == Players.White && m.OriginalPosition == new Position(Position.Files.H, Position.Ranks.One))
                    _whiteRookHMoved = true;
                if (!_blackRookAMoved && m.Player == Players.Black && m.OriginalPosition == new Position(Position.Files.A, Position.Ranks.Eight))
                    _blackRookAMoved = true;
                if (!_blackRookHMoved && m.Player == Players.Black && m.OriginalPosition == new Position(Position.Files.H, Position.Ranks.Eight))
                    _blackRookHMoved = true;
            }
            if (!validateCheck)
                return;
            List<Tuple<Players, bool>> playersToValidateCheck = new List<Tuple<Players, bool>>();
            playersToValidateCheck.Add(new Tuple<Players, bool>(moves[moves.Count - 1].Player == Players.White ? Players.Black : Players.White, true));
            ChangeStatus(playersToValidateCheck, true);
        }

        public Chessboard(ChessPiece[,] board, Players whoseTurn) :
            this(board, whoseTurn, true)
        {
        }

        protected Chessboard(ChessPiece[,] board, Players whoseTurn, bool validateCheck)
        {
            Board = (ChessPiece[,])board.Clone();
            Moves = new List<Move>();
            ChessPiece e1 = GetPieceAt(Position.Files.E, Position.Ranks.One);
            ChessPiece e8 = GetPieceAt(Position.Files.E, Position.Ranks.Eight);
            ChessPiece a1 = GetPieceAt(Position.Files.A, Position.Ranks.One);
            ChessPiece h1 = GetPieceAt(Position.Files.H, Position.Ranks.One);
            ChessPiece a8 = GetPieceAt(Position.Files.A, Position.Ranks.Eight);
            ChessPiece h8 = GetPieceAt(Position.Files.H, Position.Ranks.Eight);
            if (e1.Piece != Pieces.King || e1.Player != Players.White)
                _whiteKingMoved = true;
            if (e8.Piece != Pieces.King || e8.Player != Players.Black)
                _blackKingMoved = true;
            if (a1.Piece != Pieces.Rook || a1.Player != Players.White)
                _whiteRookAMoved = true;
            if (h1.Piece != Pieces.Rook || h1.Player != Players.White)
                _whiteRookHMoved = true;
            if (a8.Piece != Pieces.Rook || a8.Player != Players.Black)
                _blackRookAMoved = true;
            if (h8.Piece != Pieces.Rook || h8.Player != Players.Black)
                _blackRookHMoved = true;
            if (!validateCheck)
                return;
            List<Tuple<Players, bool>> playersToValidate = new List<Tuple<Players, bool>>();
            playersToValidate.Add(new Tuple<Players, bool>(whoseTurn, true));
            ChangeStatus(playersToValidate, true);
        }

        public void InitBoard()
        {
            ChessPiece kw = new ChessPiece(Pieces.King, Players.White);
            ChessPiece kb = new ChessPiece(Pieces.King, Players.Black);
            ChessPiece qw = new ChessPiece(Pieces.Queen, Players.White);
            ChessPiece qb = new ChessPiece(Pieces.Queen, Players.Black);
            ChessPiece rw = new ChessPiece(Pieces.Rook, Players.White);
            ChessPiece rb = new ChessPiece(Pieces.Rook, Players.Black);
            ChessPiece nw = new ChessPiece(Pieces.Knight, Players.White);
            ChessPiece nb = new ChessPiece(Pieces.Knight, Players.Black);
            ChessPiece bw = new ChessPiece(Pieces.Bishop, Players.White);
            ChessPiece bb = new ChessPiece(Pieces.Bishop, Players.Black);
            ChessPiece pw = new ChessPiece(Pieces.Pawn, Players.White);
            ChessPiece pb = new ChessPiece(Pieces.Pawn, Players.Black);
            ChessPiece o = ChessPiece.None;
            Board = new ChessPiece[8, 8]
            {
                { rb, nb, bb, qb, kb, bb, nb, rb },
                { pb, pb, pb, pb, pb, pb, pb, pb },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, o, o, o, o },
                { pw, pw, pw, pw, pw, pw, pw, pw },
                { rw, nw, bw, qw, kw, bw, nw, rw }
            };
        }

        protected void ChangeStatus(List<Tuple<Players, bool>> playersToValidate, bool breakAfterChange)
        {
            Status = new GameStatus(GameStatus.Events.None, Players.None, "No special event");
            foreach (Tuple<Players, bool> t in playersToValidate)
            {
                Players p = t.Item1;
                bool validateHasAnyValidMoves = t.Item2;
                Players other = p == Players.White ? Players.Black : Players.White;
                if (IsInCheck(p))
                {
                    if (validateHasAnyValidMoves && !HasAnyValidMoves(p))
                    {
                        Status = new GameStatus(GameStatus.Events.Checkmate, other, p.ToString() + " is checkmated");
                        if (breakAfterChange)
                            break;
                    }
                    else
                    {
                        Status = new GameStatus(GameStatus.Events.Check, other, p.ToString() + " is in check");
                        if (breakAfterChange)
                            break;
                    }
                }
                else if (validateHasAnyValidMoves && !HasAnyValidMoves(p))
                {
                    Status = new GameStatus(GameStatus.Events.Stalemate, other, "Stalemate");
                    if (breakAfterChange)
                        break;
                }
            }
        }

        public ChessPiece GetPieceAt(Position p)
        {
            return GetPieceAt(p.File, p.Rank);
        }

        public ChessPiece GetPieceAt(Position.Files file, Position.Ranks rank)
        {
            return Board[(int)rank, (int)file];
        }

        protected void SetPieceAt(Position.Files file, Position.Ranks rank, ChessPiece cp)
        {
            Board[(int)rank, (int)file] = cp;
        }

        public bool IsValidMove(Move m)
        {
            return IsValidMove(m, true);
        }

        protected bool IsValidMoveKing(Move move)
        {
            PositionDelta posDelta = new PositionDelta(move.OriginalPosition, move.NewPosition);
            if ((posDelta.DeltaX != 1 || posDelta.DeltaY != 1)
                        && (posDelta.DeltaX != 0 || posDelta.DeltaY != 1)
                        && (posDelta.DeltaX != 1 || posDelta.DeltaY != 0)
                        && (posDelta.DeltaX != 2 || posDelta.DeltaY != 0))
                return false;
            if (posDelta.DeltaX != 2)
                return true;
            return CanCastle(move);
        }

        protected bool CanCastle(Move move)
        {
            if (move.Player == Players.White)
            {
                if (move.OriginalPosition.File != Position.Files.E || move.OriginalPosition.Rank != Position.Ranks.One)
                    return false;
                if (_whiteKingMoved || (Status.Event == GameStatus.Events.Check && Status.PlayerWhoCausedEvent == Players.Black))
                    return false;
                if (move.NewPosition.File == Position.Files.C)
                {
                    if (_whiteRookAMoved || GetPieceAt(Position.Files.D, Position.Ranks.One).Piece != Pieces.None
                        || GetPieceAt(Position.Files.C, Position.Ranks.One).Piece != Pieces.None
                        || GetPieceAt(Position.Files.B, Position.Ranks.One).Piece != Pieces.None
                        || WouldBeInCheckAfter(new Move(new Position(Position.Files.E, Position.Ranks.One), new Position(Position.Files.D, Position.Ranks.One), Players.White), Players.White)
                        || WouldBeInCheckAfter(new Move(new Position(Position.Files.E, Position.Ranks.One), new Position(Position.Files.C, Position.Ranks.One), Players.White), Players.White))
                        return false;
                }
                else
                {
                    if (_whiteRookHMoved || GetPieceAt(Position.Files.F, Position.Ranks.One).Piece != Pieces.None
                        || GetPieceAt(Position.Files.G, Position.Ranks.One).Piece != Pieces.None
                        || WouldBeInCheckAfter(new Move(new Position(Position.Files.E, Position.Ranks.One), new Position(Position.Files.F, Position.Ranks.One), Players.White), Players.White)
                        || WouldBeInCheckAfter(new Move(new Position(Position.Files.E, Position.Ranks.One), new Position(Position.Files.G, Position.Ranks.One), Players.White), Players.White))
                        return false;
                }
            }
            else
            {
                if (move.OriginalPosition.File != Position.Files.E || move.OriginalPosition.Rank != Position.Ranks.Eight)
                    return false;
                if (_blackKingMoved || (Status.Event == GameStatus.Events.Check && Status.PlayerWhoCausedEvent == Players.White))
                    return false;
                if (move.NewPosition.File == Position.Files.C)
                {
                    if (_blackRookAMoved || GetPieceAt(Position.Files.D, Position.Ranks.Eight).Piece != Pieces.None
                        || GetPieceAt(Position.Files.C, Position.Ranks.Eight).Piece != Pieces.None
                        || GetPieceAt(Position.Files.B, Position.Ranks.Eight).Piece != Pieces.None
                        || WouldBeInCheckAfter(new Move(new Position(Position.Files.E, Position.Ranks.Eight), new Position(Position.Files.D, Position.Ranks.Eight), Players.Black), Players.Black)
                        || WouldBeInCheckAfter(new Move(new Position(Position.Files.E, Position.Ranks.Eight), new Position(Position.Files.C, Position.Ranks.Eight), Players.Black), Players.Black))
                        return false;
                }
                else
                {
                    if (_blackRookHMoved || GetPieceAt(Position.Files.F, Position.Ranks.Eight).Piece != Pieces.None
                        || GetPieceAt(Position.Files.G, Position.Ranks.Eight).Piece != Pieces.None
                        || WouldBeInCheckAfter(new Move(new Position(Position.Files.E, Position.Ranks.Eight), new Position(Position.Files.F, Position.Ranks.Eight), Players.Black), Players.Black)
                        || WouldBeInCheckAfter(new Move(new Position(Position.Files.E, Position.Ranks.Eight), new Position(Position.Files.G, Position.Ranks.Eight), Players.Black), Players.Black))
                        return false;
                }
            }
            return true;
        }

        protected bool IsValidMovePawn(Move move)
        {
            PositionDelta posDelta = new PositionDelta(move.OriginalPosition, move.NewPosition);
            if ((posDelta.DeltaX != 0 || posDelta.DeltaY != 1) && (posDelta.DeltaX != 1 || posDelta.DeltaY != 1)
                        && (posDelta.DeltaX != 0 || posDelta.DeltaY != 2))
                return false;
            if (move.Player == Players.White)
            {
                if ((int)move.OriginalPosition.Rank < (int)move.NewPosition.Rank)
                    return false;
                if (move.NewPosition.Rank == Position.Ranks.Eight && move.Promotion == Pieces.None)
                    return false;
            }
            if (move.Player == Players.Black)
            {
                if ((int)move.OriginalPosition.Rank > (int)move.NewPosition.Rank)
                    return false;
                if (move.NewPosition.Rank == Position.Ranks.One && move.Promotion == Pieces.None)
                    return false;
            }
            bool checkEnPassant = false;
            if (posDelta.DeltaY == 2)
            {
                if ((move.OriginalPosition.Rank != Position.Ranks.Two && move.Player == Players.White)
                    || (move.OriginalPosition.Rank != Position.Ranks.Seven && move.Player == Players.Black))
                    return false;
                if (move.OriginalPosition.Rank == Position.Ranks.Two && GetPieceAt(move.OriginalPosition.File, Position.Ranks.Three).Piece != Pieces.None)
                    return false;
                if (move.OriginalPosition.Rank == Position.Ranks.Seven && GetPieceAt(move.OriginalPosition.File, Position.Ranks.Six).Piece != Pieces.None)
                    return false;
            }
            if (posDelta.DeltaX == 0 && (posDelta.DeltaY == 1 || posDelta.DeltaY == 2))
            {
                if (GetPieceAt(move.NewPosition).Player != Players.None)
                    return false;
            }
            else
            {
                if (GetPieceAt(move.NewPosition).Player != (move.Player == Players.White ? Players.Black : Players.White))
                    checkEnPassant = true;
                if (GetPieceAt(move.NewPosition).Player == move.Player)
                    return false;
            }
            if (checkEnPassant)
            {
                if (Moves.Count == 0)
                {
                    return false;
                }
                if ((move.OriginalPosition.Rank != Position.Ranks.Five && move.Player == Players.White)
                    || (move.OriginalPosition.Rank != Position.Ranks.Four && move.Player == Players.Black))
                    return false;
                Move latestMove = Moves[Moves.Count - 1];
                if (latestMove.Player != (move.Player == Players.White ? Players.Black : Players.White))
                    return false;
                if (move.Player == Players.White)
                {
                    if (latestMove.OriginalPosition.Rank != Position.Ranks.Seven || latestMove.NewPosition.Rank != Position.Ranks.Five)
                        return false;
                }
                else // (m.Player == Players.Black)
                {
                    if (latestMove.OriginalPosition.Rank != Position.Ranks.Two || latestMove.NewPosition.Rank != Position.Ranks.Four)
                        return false;
                }
                if (move.NewPosition.File != latestMove.NewPosition.File)
                    return false;
            }
            return true;
        }

        protected bool IsValidMoveRook(Move move)
        {
            PositionDelta posDelta = new PositionDelta(move.OriginalPosition, move.NewPosition);
            if (posDelta.DeltaX != 0 && posDelta.DeltaY != 0)
                return false;
            bool increasingRank = (int)move.NewPosition.Rank > (int)move.OriginalPosition.Rank;
            bool increasingFile = (int)move.NewPosition.File > (int)move.OriginalPosition.File;
            if (posDelta.DeltaX == 0)
            {
                int f = (int)move.OriginalPosition.File;
                for (int r = (int)move.OriginalPosition.Rank + (increasingRank ? 1 : -1);
                    increasingRank ? r < (int)move.NewPosition.Rank : r > (int)move.NewPosition.Rank;
                    r += increasingRank ? 1 : -1)
                {
                    if (Board[r, f].Player != Players.None)
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
                    if (Board[r, f].Player != Players.None)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected bool IsValidMoveBishop(Move move)
        {
            PositionDelta posDelta = new PositionDelta(move.OriginalPosition, move.NewPosition);
            if (posDelta.DeltaX != posDelta.DeltaY)
                return false;
            bool increasingRank = (int)move.NewPosition.Rank > (int)move.OriginalPosition.Rank;
            bool increasingFile = (int)move.NewPosition.File > (int)move.OriginalPosition.File;
            for (int f = (int)move.OriginalPosition.File + (increasingFile ? 1 : -1), r = (int)move.OriginalPosition.Rank + (increasingRank ? 1 : -1);
                 increasingFile ? f < (int)move.NewPosition.File : f > (int)move.NewPosition.File;
                 f += increasingFile ? 1 : -1, r += increasingRank ? 1 : -1)
            {
                if (Board[r, f].Player != Players.None)
                {
                    return false;
                }
            }
            return true;
        }

        protected bool IsValidMoveKnight(Move move)
        {
            PositionDelta posDelta = new PositionDelta(move.OriginalPosition, move.NewPosition);
            if ((posDelta.DeltaX != 2 || posDelta.DeltaY != 1) && (posDelta.DeltaX != 1 || posDelta.DeltaY != 2))
                return false;
            return true;
        }

        protected bool IsValidMoveQueen(Move move)
        {
            return IsValidMoveBishop(move) || IsValidMoveRook(move);
        }

        protected bool IsValidMove(Move move, bool validateCheck)
        {
            if (move.OriginalPosition.Equals(move.NewPosition))
                return false;
            ChessPiece piece = GetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank);
            if (piece.Player != move.Player) return false;
            if (GetPieceAt(move.NewPosition).Player == move.Player)
            {
                return false;
            }
            PositionDelta posDelta = new PositionDelta(move.OriginalPosition, move.NewPosition);
            switch (piece.Piece)
            {
                case Pieces.King:
                    if (!IsValidMoveKing(move))
                        return false;
                    break;
                case Pieces.Pawn:
                    if (!IsValidMovePawn(move))
                        return false;
                    break;
                case Pieces.Queen:
                    if (!IsValidMoveQueen(move))
                        return false;
                    break;
                case Pieces.Rook:
                    if (!IsValidMoveRook(move))
                        return false;
                    break;
                case Pieces.Bishop:
                    if (!IsValidMoveBishop(move))
                        return false;
                    break;
                case Pieces.Knight:
                    if ((posDelta.DeltaX != 2 || posDelta.DeltaY != 1) && (posDelta.DeltaX != 1 || posDelta.DeltaY != 2))
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
            return ApplyMove(move, alreadyValidated, true, false);
        }

        protected bool ApplyMove(Move move, bool alreadyValidated, bool validateHasAnyValidMoves, bool validateSelfCheck)
        {
            if (!alreadyValidated && !IsValidMove(move))
                return false;
            ChessPiece movingPiece = GetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank);
            ChessPiece newPiece = movingPiece;
            if (movingPiece.Piece == Pieces.Pawn)
            {
                PositionDelta pd = new PositionDelta(move.OriginalPosition, move.NewPosition);
                if (pd.DeltaX == 1 && pd.DeltaY == 1 && GetPieceAt(move.NewPosition).Piece == Pieces.None)
                { // en passant
                    SetPieceAt(move.NewPosition.File, move.OriginalPosition.Rank, ChessPiece.None);
                }
                if (move.NewPosition.Rank == (move.Player == Players.White ? Position.Ranks.Eight : Position.Ranks.One))
                {
                    newPiece = new ChessPiece(move.Promotion, move.Player);
                }
            }
            else if (movingPiece.Piece == Pieces.King && movingPiece.Player == Players.White)
            {
                _whiteKingMoved = true;
            }
            else if (movingPiece.Piece == Pieces.King && movingPiece.Player == Players.Black)
            {
                _blackKingMoved = true;
            }
            else if (movingPiece.Piece == Pieces.Rook && move.OriginalPosition.File == Position.Files.A && move.OriginalPosition.Rank == Position.Ranks.One && move.Player == Players.White)
            {
                _whiteRookAMoved = true;
            }
            else if (movingPiece.Piece == Pieces.Rook && move.OriginalPosition.File == Position.Files.H && move.OriginalPosition.Rank == Position.Ranks.One && move.Player == Players.White)
            {
                _whiteRookHMoved = true;
            }
            else if (movingPiece.Piece == Pieces.Rook && move.OriginalPosition.File == Position.Files.A && move.OriginalPosition.Rank == Position.Ranks.Eight && move.Player == Players.Black)
            {
                _blackRookAMoved = true;
            }
            else if (movingPiece.Piece == Pieces.Rook && move.OriginalPosition.File == Position.Files.H && move.OriginalPosition.Rank == Position.Ranks.Eight && move.Player == Players.Black)
            {
                _blackRookHMoved = true;
            }
            SetPieceAt(move.NewPosition.File, move.NewPosition.Rank, newPiece);
            SetPieceAt(move.OriginalPosition.File, move.OriginalPosition.Rank, ChessPiece.None);
            if (movingPiece.Piece == Pieces.King && new PositionDelta(move.OriginalPosition, move.NewPosition).DeltaX == 2)
            {
                Position.Ranks rank = move.Player == Players.White ? Position.Ranks.One : Position.Ranks.Eight;
                Position.Files rookFile = move.NewPosition.File == Position.Files.C ? Position.Files.A : Position.Files.H;
                Position.Files newRookFile = move.NewPosition.File == Position.Files.C ? Position.Files.D : Position.Files.F;
                SetPieceAt(newRookFile, rank, new ChessPiece(Pieces.Rook, move.Player));
                SetPieceAt(rookFile, rank, ChessPiece.None);
            }
            Moves.Add(move);
            Players other = move.Player == Players.White ? Players.Black : Players.White;
            List<Tuple<Players, bool>> playersToValidate = new List<Tuple<Players, bool>>();
            playersToValidate.Add(new Tuple<Players, bool>(other, validateHasAnyValidMoves));
            if (validateSelfCheck)
            {
                playersToValidate.Add(new Tuple<Players, bool>(move.Player, false));
            }
            ChangeStatus(playersToValidate, false);
            return true;
        }

        public IReadOnlyCollection<Move> GetValidMoves(Position from)
        {
            return GetValidMoves(from, false);
        }

        protected IReadOnlyCollection<Move> GetValidMovesKing(Position from, bool returnIfAny)
        {
            List<Move> validMoves = new List<Move>();
            ChessPiece cp = GetPieceAt(from);
            int l0 = Board.GetLength(0);
            int l1 = Board.GetLength(1);
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
                        return validMoves;
                }
            }
            return validMoves;
        }

        protected IReadOnlyCollection<Move> GetValidMovesPawn(Position from, bool returnIfAny)
        {
            List<Move> validMoves = new List<Move>();
            ChessPiece cp = GetPieceAt(from);
            int l0 = Board.GetLength(0);
            int l1 = Board.GetLength(1);
            int[][] directions;
            if (cp.Player == Players.Black)
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
                        return validMoves;
                }
            }
            return validMoves;
        }

        protected IReadOnlyCollection<Move> GetValidMovesKnight(Position from, bool returnIfAny)
        {
            List<Move> validMoves = new List<Move>();
            ChessPiece cp = GetPieceAt(from);
            int l0 = Board.GetLength(0);
            int l1 = Board.GetLength(1);
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
                        return validMoves;
                }
            }
            return validMoves;
        }

        protected IReadOnlyCollection<Move> GetValidMovesRook(Position from, bool returnIfAny)
        {
            List<Move> validMoves = new List<Move>();
            ChessPiece cp = GetPieceAt(from);
            int l0 = Board.GetLength(0);
            int l1 = Board.GetLength(1);
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
                            return validMoves;
                    }
                }
                if ((int)from.File + i > -1 && (int)from.File + i < l1)
                {
                    Move move = new Move(from, new Position(from.File + i, from.Rank), cp.Player);
                    if (IsValidMove(move))
                    {
                        validMoves.Add(move);
                        if (returnIfAny)
                            return validMoves;
                    }
                }
            }
            return validMoves;
        }

        protected IReadOnlyCollection<Move> GetValidMovesBishop(Position from, bool returnIfAny)
        {
            List<Move> validMoves = new List<Move>();
            ChessPiece cp = GetPieceAt(from);
            int l0 = Board.GetLength(0);
            int l1 = Board.GetLength(1);
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
                            return validMoves;
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
                            return validMoves;
                    }
                }
            }
            return validMoves;
        }

        protected IReadOnlyCollection<Move> GetValidMovesQueen(Position from, bool returnIfAny)
        {
            IReadOnlyCollection<Move> horizontalVerticalMoves = GetValidMovesRook(from, returnIfAny);
            if (returnIfAny && horizontalVerticalMoves.Count > 0)
                return horizontalVerticalMoves;
            IReadOnlyCollection<Move> diagonalMoves = GetValidMovesBishop(from, returnIfAny);
            return new ReadOnlyCollection<Move>(horizontalVerticalMoves.Concat(diagonalMoves).ToList());
        }

        protected IReadOnlyCollection<Move> GetValidMoves(Position from, bool returnIfAny)
        {
            Pieces piece = GetPieceAt(from).Piece;
            switch (piece)
            {
                case Pieces.King:
                    return GetValidMovesKing(from, returnIfAny);
                case Pieces.Pawn:
                    return GetValidMovesPawn(from, returnIfAny);
                case Pieces.Knight:
                    return GetValidMovesKnight(from, returnIfAny);
                case Pieces.Rook:
                    return GetValidMovesRook(from, returnIfAny);
                case Pieces.Bishop:
                    return GetValidMovesBishop(from, returnIfAny);
                case Pieces.Queen:
                    return GetValidMovesQueen(from, returnIfAny);
                default:
                    return new Collection<Move>();
            }
        }

        public List<Move> GetValidMoves(Players player)
        {
            return GetValidMoves(player, false);
        }

        protected List<Move> GetValidMoves(Players player, bool returnIfAny)
        {
            List<Move> validMoves = new List<Move>();
            for (int x = 0; x < Board.GetLength(0); x++)
            {
                for (int y = 0; y < Board.GetLength(1); y++)
                {
                    if (Board[x, y].Player == player)
                    {
                        validMoves.AddRange(GetValidMoves(new Position((Position.Files)y, (Position.Ranks)x), returnIfAny));
                        if (returnIfAny && validMoves.Count > 0)
                        {
                            return validMoves;
                        }
                    }
                }
            }
            return validMoves;
        }

        protected bool HasAnyValidMoves(Position from)
        {
            IReadOnlyCollection<Move> validMoves = GetValidMoves(from, true);
            return validMoves.Count > 0;
        }

        protected bool HasAnyValidMoves(Players player)
        {
            List<Move> validMoves = GetValidMoves(player, true);
            return validMoves.Count > 0;
        }

        protected bool IsInCheck(Players player)
        {
            List<Position> piecePositions = new List<Position>();
            Position kingPos = new Position(Position.Files.None, Position.Ranks.None);

            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    ChessPiece curr = Board[i, j];
                    if (curr.Piece != Pieces.None && curr.Player == (player == Players.White ? Players.Black : Players.White))
                    {
                        piecePositions.Add(new Position((Position.Files)j, (Position.Ranks)i));
                    }
                    else if (curr.Piece == Pieces.King && curr.Player == player)
                    {
                        kingPos = new Position((Position.Files)j, (Position.Ranks)i);
                    }
                }
            }

            if (kingPos.File == Position.Files.None)
                return false;

            for (int i = 0; i < piecePositions.Count; i++)
            {
                if (IsValidMove(new Move(piecePositions[i], kingPos, player == Players.White ? Players.Black : Players.White), false))
                {
                    return true;
                }
            }

            return false;
        }

        protected bool WouldBeInCheckAfter(Move move, Players player)
        {
            Chessboard copy = new Chessboard(Board, player, false);
            copy.ApplyMove(move, true, false, true);
            return copy.Status.Event == GameStatus.Events.Check && copy.Status.PlayerWhoCausedEvent != player;
        }

        public void Draw(string reason)
        {
            Status = new GameStatus(GameStatus.Events.Draw, Players.None, reason);
        }

        public void Resign(Players player)
        {
            Status = new GameStatus(GameStatus.Events.Resign, player, player.ToString() + " resigned");
        }
    }
}
