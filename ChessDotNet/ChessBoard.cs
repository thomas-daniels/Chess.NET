using System.Collections.Generic;

namespace ChessDotNet
{
    public class ChessBoard
    {
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

        public ChessBoard()
        {
            Status = new GameStatus(GameStatus.Events.None, Players.None, "No special event");
            Board = new ChessPiece[8, 8];
            Moves = new List<Move>();
            InitBoard();
        }

        public ChessBoard(ChessPiece[,] board, List<Move> moves):
            this(board, moves, true)
        {
        }

        protected ChessBoard(ChessPiece[,] board, List<Move> moves, bool validateCheck)
        {
            Board = (ChessPiece[,])board.Clone();
            Moves = new List<Move>(moves);
            if (!validateCheck)
                return;
            List<Players> playersToValidateCheck = new List<Players>();
            if (moves.Count != 0)
            {
                playersToValidateCheck.Add(moves[moves.Count - 1].Player == Players.White ? Players.Black : Players.White);
            }
            else
            {
                playersToValidateCheck.Add(Players.White);
                playersToValidateCheck.Add(Players.Black);
            }
            ChangeStatus(playersToValidateCheck, true, true);
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

        protected void ChangeStatus(List<Players> playersToValidate, bool validateHasAnyValidMoves, bool breakAfterChange)
        {
            Status = new GameStatus(GameStatus.Events.None, Players.None, "No special event");
            foreach (Players p in playersToValidate)
            {
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

        protected bool IsValidMove(Move m, bool validateCheck)
        {
            if (m.OriginalPosition.Equals(m.NewPosition))
                return false;
            ChessPiece piece = GetPieceAt(m.OriginalPosition.File, m.OriginalPosition.Rank);
            if (piece.Player != m.Player) return false;
            if (GetPieceAt(m.NewPosition).Player == m.Player)
            {
                return false;
            }
            PositionDelta posDelta = new PositionDelta(m.OriginalPosition, m.NewPosition);
            switch (piece.Piece)
            {
                case Pieces.King:
                    if ((posDelta.DeltaX != 1 || posDelta.DeltaY != 1)
                        && (posDelta.DeltaX != 0 || posDelta.DeltaY != 1)
                        && (posDelta.DeltaX != 1 || posDelta.DeltaY != 0))
                        return false; // TODO: take castling in account
                    break;
                case Pieces.Pawn:
                    if ((posDelta.DeltaX != 0 || posDelta.DeltaY != 1) && (posDelta.DeltaX != 1 || posDelta.DeltaY != 1)
                        && (posDelta.DeltaX != 0 || posDelta.DeltaY != 2))
                        return false;
                    if (piece.Player == Players.White && (int)m.OriginalPosition.Rank < (int)m.NewPosition.Rank)
                        return false;
                    if (piece.Player == Players.Black && (int)m.OriginalPosition.Rank > (int)m.NewPosition.Rank)
                        return false;
                    bool checkEnPassant = false;
                    if (posDelta.DeltaY == 2)
                    {
                        if ((m.OriginalPosition.Rank != Position.Ranks.Two && m.Player == Players.White)
                            || (m.OriginalPosition.Rank != Position.Ranks.Seven && m.Player == Players.Black))
                            return false;
                        if (m.OriginalPosition.Rank == Position.Ranks.Two && GetPieceAt(m.OriginalPosition.File, Position.Ranks.Three).Piece != Pieces.None)
                            return false;
                        if (m.OriginalPosition.Rank == Position.Ranks.Seven && GetPieceAt(m.OriginalPosition.File, Position.Ranks.Six).Piece != Pieces.None)
                            return false;
                    }
                    if (posDelta.DeltaX == 0 && (posDelta.DeltaY == 1 || posDelta.DeltaY == 2))
                    {
                        if (GetPieceAt(m.NewPosition).Player != Players.None)
                            return false;
                    }
                    else
                    {
                        if (GetPieceAt(m.NewPosition).Player != (m.Player == Players.White ? Players.Black : Players.White))
                            checkEnPassant = true;
                        if (GetPieceAt(m.NewPosition).Player == m.Player)
                            return false;
                    }
                    if (checkEnPassant)
                    {
                        if (Moves.Count == 0)
                        {
                            return false;
                        }
                        if ((m.OriginalPosition.Rank != Position.Ranks.Five && m.Player == Players.White)
                            || (m.OriginalPosition.Rank != Position.Ranks.Four && m.Player == Players.Black))
                            return false;
                        Move latestMove = Moves[Moves.Count - 1];
                        if (latestMove.Player != (m.Player == Players.White ? Players.Black : Players.White))
                            return false;
                        if (m.Player == Players.White)
                        {
                            if (latestMove.OriginalPosition.Rank != Position.Ranks.Seven || latestMove.NewPosition.Rank != Position.Ranks.Five)
                                return false;
                        }
                        else // (m.Player == Players.Black)
                        {
                            if (latestMove.OriginalPosition.Rank != Position.Ranks.Two || latestMove.NewPosition.Rank != Position.Ranks.Four)
                                return false;
                        }
                        if (m.NewPosition.File != latestMove.NewPosition.File)
                            return false;
                    }
                    break;
                case Pieces.Queen:
                    if (posDelta.DeltaX != posDelta.DeltaY && posDelta.DeltaX != 0 && posDelta.DeltaY != 0)
                        return false;
                    bool increasingRank = (int)m.NewPosition.Rank > (int)m.OriginalPosition.Rank;
                    bool increasingFile = (int)m.NewPosition.File > (int)m.OriginalPosition.File;
                    if (posDelta.DeltaX == posDelta.DeltaY)
                    {
                        for (int f = (int)m.OriginalPosition.File + (increasingFile ? 1 : -1), r = (int)m.OriginalPosition.Rank + (increasingRank ? 1 : -1);
                            increasingFile ? f < (int)m.NewPosition.File : f > (int)m.NewPosition.File;
                            f += increasingFile ? 1 : -1, r += increasingRank ? 1 : -1)
                        {
                            if (Board[r, f].Player != Players.None)
                            {
                                return false;
                            }
                        }
                    }
                    else if (posDelta.DeltaX == 0)
                    {
                        int f = (int)m.OriginalPosition.File;
                        for (int r = (int)m.OriginalPosition.Rank + (increasingRank ? 1 : -1);
                            increasingRank ? r < (int)m.NewPosition.Rank : r > (int)m.NewPosition.Rank;
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
                        int r = (int)m.OriginalPosition.Rank;
                        for (int f = (int)m.OriginalPosition.File + (increasingFile ? 1 : -1);
                            increasingFile ? f < (int)m.NewPosition.File : f > (int)m.NewPosition.File;
                            f += increasingFile ? 1 : -1)
                        {
                            if (Board[r, f].Player != Players.None)
                            {
                                return false;
                            }
                        }
                    }
                    break;
                case Pieces.Rook:
                    if (posDelta.DeltaX != 0 && posDelta.DeltaY != 0)
                        return false;
                    increasingRank = (int)m.NewPosition.Rank > (int)m.OriginalPosition.Rank;
                    increasingFile = (int)m.NewPosition.File > (int)m.OriginalPosition.File;
                    if (posDelta.DeltaX == 0)
                    {
                        int f = (int)m.OriginalPosition.File;
                        for (int r = (int)m.OriginalPosition.Rank + (increasingRank ? 1 : -1);
                            increasingRank ? r < (int)m.NewPosition.Rank : r > (int)m.NewPosition.Rank;
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
                        int r = (int)m.OriginalPosition.Rank;
                        for (int f = (int)m.OriginalPosition.File + (increasingFile ? 1 : -1);
                            increasingFile ? f < (int)m.NewPosition.File : f > (int)m.NewPosition.File;
                            f += increasingFile ? 1 : -1)
                        {
                            if (Board[r, f].Player != Players.None)
                            {
                                return false;
                            }
                        }
                    }
                    break;
                case Pieces.Bishop:
                    if (posDelta.DeltaX != posDelta.DeltaY)
                        return false;
                    increasingRank = (int)m.NewPosition.Rank > (int)m.OriginalPosition.Rank;
                    increasingFile = (int)m.NewPosition.File > (int)m.OriginalPosition.File;
                    for (int f = (int)m.OriginalPosition.File + (increasingFile ? 1 : -1), r = (int)m.OriginalPosition.Rank + (increasingRank ? 1 : -1);
                         increasingFile ? f < (int)m.NewPosition.File : f > (int)m.NewPosition.File;
                         f += increasingFile ? 1 : -1, r += increasingRank ? 1 : -1)
                    {
                        if (Board[r, f].Player != Players.None)
                        {
                            return false;
                        }
                    }
                    break;
                case Pieces.Knight:
                    if ((posDelta.DeltaX != 2 || posDelta.DeltaY != 1) && (posDelta.DeltaX != 1 || posDelta.DeltaY != 2))
                        return false;
                    break;
                default:
                    return false;
            }
            if (validateCheck && WouldBeInCheckAfter(m, m.Player))
            {
                return false;
            }

            return true;
        }

        public bool ApplyMove(Move m, bool alreadyValidated)
        {
            return ApplyMove(m, alreadyValidated, true, false);
        }

        protected bool ApplyMove(Move m, bool alreadyValidated, bool validateHasAnyValidMoves, bool validateSelfCheck)
        {
            if (!alreadyValidated && !IsValidMove(m))
                return false;
            ChessPiece movingPiece = GetPieceAt(m.OriginalPosition.File, m.OriginalPosition.Rank);
            if (movingPiece.Piece == Pieces.Pawn)
            {
                PositionDelta pd = new PositionDelta(m.OriginalPosition, m.NewPosition);
                if (pd.DeltaX == 1 && pd.DeltaY == 1 && GetPieceAt(m.NewPosition).Piece == Pieces.None)
                { // en passant
                    SetPieceAt(m.NewPosition.File, m.OriginalPosition.Rank, ChessPiece.None);
                }
            }
            SetPieceAt(m.NewPosition.File, m.NewPosition.Rank, movingPiece);
            SetPieceAt(m.OriginalPosition.File, m.OriginalPosition.Rank, ChessPiece.None);
            Moves.Add(m);
            Players other = m.Player == Players.White ? Players.Black : Players.White;
            List<Players> playersToValidate = new List<Players>();
            playersToValidate.Add(other);
            if (validateSelfCheck)
            {
                playersToValidate.Add(m.Player);
            }
            ChangeStatus(playersToValidate, validateHasAnyValidMoves, false);
            return true;
        }

        public List<Move> GetValidMoves(Position from)
        {
            return GetValidMoves(from, false);
        }

        protected List<Move> GetValidMoves(Position from, bool returnIfAny)
        {
            List<Move> validMoves = new List<Move>();
            ChessPiece cp = GetPieceAt(from);
            int l0 = Board.GetLength(0);
            int l1 = Board.GetLength(1);
            switch (cp.Piece)
            {
                case Pieces.King:
                    int[][] directions = new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { -1, 0 },
                        new int[] { 1, 1 }, new int[] { 1, -1 }, new int[] { -1, 1 }, new int[] { -1, -1 } };
                    foreach (int[] dir in directions)
                    {
                        if ((int)from.File + dir[0] < 0 || (int)from.File + dir[0] >= l1
                            || (int)from.Rank + dir[1] < 0 || (int)from.Rank + dir[1] >= l0)
                            continue;
                        Move m = new Move(from, new Position(from.File + dir[0], from.Rank + dir[1]), cp.Player);
                        if (IsValidMove(m))
                        {
                            validMoves.Add(m);
                            if (returnIfAny)
                                return validMoves;
                        }
                    }
                    break;
                case Pieces.Pawn:
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
                        Move m = new Move(from, new Position(from.File + dir[0], from.Rank + dir[1]), cp.Player);
                        if (IsValidMove(m))
                        {
                            validMoves.Add(m);
                            if (returnIfAny)
                                return validMoves;
                        }
                    }
                    break;
                case Pieces.Knight:
                    directions = new int[][] { new int[] { 2, 1 }, new int[] { -2, -1 }, new int[] { 1, 2 }, new int[] { -1, -2 },
                        new int[] { 1, -2 }, new int[] { -1, 2 }, new int[] { 2, -1 }, new int[] { -2, 1 } };
                    foreach (int[] dir in directions)
                    {
                        if ((int)from.File + dir[0] < 0 || (int)from.File + dir[0] >= l1
                            || (int)from.Rank + dir[1] < 0 || (int)from.Rank + dir[1] >= l0)
                            continue;
                        Move m = new Move(from, new Position(from.File + dir[0], from.Rank + dir[1]), cp.Player);
                        if (IsValidMove(m))
                        {
                            validMoves.Add(m);
                            if (returnIfAny)
                                return validMoves;
                        }
                    }
                    break;
                case Pieces.Rook:
                    for (int i = -7; i < 8; i++)
                    {
                        if (i == 0)
                            continue;
                        if ((int)from.Rank + i > -1 && (int)from.Rank + i < l0)
                        {
                            Move m = new Move(from, new Position(from.File, from.Rank + i), cp.Player);
                            if (IsValidMove(m))
                            {
                                validMoves.Add(m);
                                if (returnIfAny)
                                    return validMoves;
                            }
                        }
                        if ((int)from.File + i > -1 && (int)from.File + i < l1)
                        {
                            Move m = new Move(from, new Position(from.File + i, from.Rank), cp.Player);
                            if (IsValidMove(m))
                            {
                                validMoves.Add(m);
                                if (returnIfAny)
                                    return validMoves;
                            }
                        }
                    }
                    break;
                case Pieces.Bishop:
                    for (int i = -7; i < 8; i++)
                    {
                        if (i == 0)
                            continue;
                        if ((int)from.Rank + i > -1 && (int)from.Rank + i < l0
                            && (int)from.File + i > -1 && (int)from.File + i < l1)
                        {
                            Move m = new Move(from, new Position(from.File + i, from.Rank + i), cp.Player);
                            if (IsValidMove(m))
                            {
                                validMoves.Add(m);
                                if (returnIfAny)
                                    return validMoves;
                            }
                        }
                        if ((int)from.Rank - i > -1 && (int)from.Rank - i < l0
                            && (int)from.File + i > -1 && (int)from.File + i < l1)
                        {
                            Move m = new Move(from, new Position(from.File + i, from.Rank - i), cp.Player);
                            if (IsValidMove(m))
                            {
                                validMoves.Add(m);
                                if (returnIfAny)
                                    return validMoves;
                            }
                        }
                    }
                    break;
                case Pieces.Queen:
                    for (int i = -7; i < 8; i++)
                    {
                        if (i == 0)
                            continue;
                        if ((int)from.Rank + i > -1 && (int)from.Rank + i < l0)
                        {
                            Move m = new Move(from, new Position(from.File, from.Rank + i), cp.Player);
                            if (IsValidMove(m))
                            {
                                validMoves.Add(m);
                                if (returnIfAny)
                                    return validMoves;
                            }
                        }
                        if ((int)from.File + i > -1 && (int)from.File + i < l1)
                        {
                            Move m = new Move(from, new Position(from.File + i, from.Rank), cp.Player);
                            if (IsValidMove(m))
                            {
                                validMoves.Add(m);
                                if (returnIfAny)
                                    return validMoves;
                            }
                        }
                        if ((int)from.Rank + i > -1 && (int)from.Rank + i < l0
                            && (int)from.File + i > -1 && (int)from.File + i < l1)
                        {
                            Move m = new Move(from, new Position(from.File + i, from.Rank + i), cp.Player);
                            if (IsValidMove(m))
                            {
                                validMoves.Add(m);
                                if (returnIfAny)
                                    return validMoves;
                            }
                        }
                        if ((int)from.Rank - i > -1 && (int)from.Rank - i < l0
                            && (int)from.File + i > -1 && (int)from.File + i < l1)
                        {
                            Move m = new Move(from, new Position(from.File + i, from.Rank - i), cp.Player);
                            if (IsValidMove(m))
                            {
                                validMoves.Add(m);
                                if (returnIfAny)
                                    return validMoves;
                            }
                        }
                    }
                    break;
            }
            return validMoves;
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
            List<Move> validMoves = GetValidMoves(from, true);
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

        protected bool WouldBeInCheckAfter(Move m, Players player)
        {
            ChessBoard copy = new ChessBoard(Board, Moves, false);
            copy.ApplyMove(m, true, false, true);
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
