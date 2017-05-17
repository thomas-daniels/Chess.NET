using ChessDotNet.Pieces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessDotNet.Variants.Crazyhouse
{
    public class CrazyhouseChessGame : ChessGame
    {
        List<Piece> whitePocket = new List<Piece>();
        List<Piece> blackPocket = new List<Piece>();

        public ReadOnlyCollection<Piece> WhitePocket
        {
            get
            {
                return new ReadOnlyCollection<Piece>(whitePocket);
            }
        }

        public ReadOnlyCollection<Piece> BlackPocket
        {
            get
            {
                return new ReadOnlyCollection<Piece>(blackPocket);
            }
        }

        public CrazyhouseChessGame() : base() { }
        public CrazyhouseChessGame(GameCreationData data) : base(data) { }
        public CrazyhouseChessGame(string fen) : base(fen) { }
        public CrazyhouseChessGame(IEnumerable<Move> moves, bool movesAreValidated) : base(moves, movesAreValidated) { }

        public bool IsValidDrop(Drop drop)
        {
            return IsValidDrop(drop, true, true);
        }

        public override bool NeedsPgnMoveSpecialTreatment(string move, Player player)
        {
            return move.Contains("@");
        }

        public override bool HandleSpecialPgnMove(string move, Player player)
        {
            if (move.Length != 3 && move.Length != 4)
            {
                throw new PgnException("Crazyhouse drop has to be 3 or 4 letters.");
            }
            Drop d = null;
            if (move.Length == 3)
            {
                d = new Drop(new Pawn(player), new Position(move.Remove(0, 1)), player);
            }
            else
            {
                d = new Drop(MapPgnCharToPiece(move[0], player), new Position(move.Remove(0, 2)), player);
            }
            if (!ApplyDrop(d, false))
            {
                throw new PgnException("Invalid crazyhouse drop: " + move);
            }
            return false;
        }


        protected override int[] ValidFenBoardRows { get { return new int[2] { 8, 9 }; } }
        protected override Piece[][] InterpretBoardOfFen(string board)
        {
            string[] rows = board.Split(new char[] { '/', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);

            string pocketInfo;

            if (rows.Length == 8) pocketInfo = "";
            else pocketInfo = rows[8];

            foreach (char pocketPiece in pocketInfo)
            {
                Player owner = char.IsUpper(pocketPiece) ? Player.White : Player.Black;
                (owner == Player.White ? whitePocket : blackPocket).Add(FenMappings[pocketPiece]);
            }

            return base.InterpretBoardOfFen(string.Join("/", rows.Take(8)));
        }

        public override string GetFen()
        {
            string temp = base.GetFen();
            if (whitePocket.Count == 0 && blackPocket.Count == 0) return temp;

            string[] parts = temp.Split(' ');
            parts[0] += "/";
            foreach (Piece w in whitePocket)
            {
                parts[0] += w.GetFenCharacter();
            }
            foreach (Piece b in blackPocket)
            {
                parts[0] += b.GetFenCharacter();
            }

            return string.Join(" ", parts);
        }

        protected override bool FiftyMovesAndThisCanResultInDraw
        {
            get
            {
                return false;
            }
        }

        public override bool IsCheckmated(Player player)
        {
            Cache<bool> cache = player == Player.White ? checkmatedCacheWhite : checkmatedCacheBlack;
            if (cache.CachedAt == Moves.Count)
            {
                return cache.Value;
            }

            return cache.UpdateCache(IsInCheck(player) && !HasAnyValidDrops(player) && !HasAnyValidMoves(player), Moves.Count);
        }

        public override bool IsStalemated(Player player)
        {
            Cache<bool> cache = player == Player.White ? stalematedCacheWhite : stalematedCacheBlack;
            if (cache.CachedAt == Moves.Count)
            {
                return cache.Value;
            }

            return cache.UpdateCache(WhoseTurn == player && !IsInCheck(player) && !HasAnyValidDrops(player) && !HasAnyValidMoves(player), Moves.Count);
        }

        protected virtual bool IsValidDrop(Drop drop, bool validateCheck, bool careAboutWhoseTurnItIs)
        {
            ChessUtilities.ThrowIfNull(drop, "drop");
            if (careAboutWhoseTurnItIs && drop.Player != WhoseTurn) return false;
            if (GetPieceAt(drop.Destination) != null) return false;

            if (drop.Player == Player.White)
            {
                if (!whitePocket.Contains(drop.ToDrop)) return false;
            }
            else
            {
                if (!blackPocket.Contains(drop.ToDrop)) return false;
            }

            if (drop.ToDrop is Pawn)
            {
                if (drop.Destination.Rank == 1 || drop.Destination.Rank == 8) return false;
            }

            if (validateCheck)
            {
                if (WouldBeInCheckAfter(drop, drop.Player))
                {
                    return false;
                }
            }

            return true;
        }

        protected override void AddDetailedMove(DetailedMove dm)
        {
            AddDetailedMove(new CrazyhouseDetailedMove(dm));
        }

        protected virtual void AddDetailedMove(CrazyhouseDetailedMove cdm)
        {
            base.AddDetailedMove(cdm);
        }

        public virtual bool WouldBeInCheckAfter(Drop drop, Player player)
        {
            CrazyhouseChessGame copy = new CrazyhouseChessGame(GetFen());
            copy.ApplyDrop(drop, true);
            return copy.IsInCheck(player);
        }

        public virtual bool ApplyDrop(Drop drop, bool alreadyValidated)
        {
            ChessUtilities.ThrowIfNull(drop, "drop");
            if (!alreadyValidated && !IsValidDrop(drop))
            {
                return false;
            }
            SetPieceAt(drop.Destination.File, drop.Destination.Rank, drop.ToDrop);
            (drop.Player == Player.White ? whitePocket : blackPocket).Remove(drop.ToDrop);
            i_halfMoveClock++;
            if (drop.Player == Player.Black)
            {
                i_fullMoveNumber++;
            }
            WhoseTurn = ChessUtilities.GetOpponentOf(WhoseTurn);
            AddDetailedMove(new CrazyhouseDetailedMove(drop));
            return true;
        }

        public override MoveType ApplyMove(Move move, bool alreadyValidated, out Piece captured)
        {
            MoveType ret = base.ApplyMove(move, alreadyValidated, out captured);
            if (ret.HasFlag(MoveType.Capture))
            {
                if (move.Player == Player.White)
                {
                    whitePocket.Add(captured.GetWithInvertedOwner());
                }
                else
                {
                    blackPocket.Add(captured.GetWithInvertedOwner());
                }
            }
            return ret;
        }

        public ReadOnlyCollection<Drop> GetValidDrops(Player player, Position on)
        {
            return new ReadOnlyCollection<Drop>(GetValidDrops(player, on, false));
        }

        public ReadOnlyCollection<Drop> GetValidDrops(Player player)
        {
            return new ReadOnlyCollection<Drop>(GetValidDrops(player, false));
        }

        public bool HasAnyValidDrops(Player player, Position on)
        {
            return GetValidDrops(player, on, true).Count != 0;
        }

        public bool HasAnyValidDrops(Player player)
        {
            return GetValidDrops(player, true).Count != 0;
        }

        protected virtual List<Drop> GetValidDrops(Player player, Position on, bool returnIfAny)
        {
            ChessUtilities.ThrowIfNull(on, "on");
            if (GetPieceAt(on) != null || WhoseTurn != player) return new List<Drop>();
            if (WouldBeInCheckAfter(new Drop(new Queen(player), on, player), player)) return new List<Drop>();

            bool isValidForPawns = on.Rank != 1 && on.Rank != 8;
            List<Drop> valid = new List<Drop>();
            foreach (Piece p in (player == Player.White ? whitePocket : blackPocket))
            {
                if (p is Pawn && !isValidForPawns) continue;
                Drop d = new Drop(p, on, player);
                if (!valid.Contains(d))
                {
                    valid.Add(d);
                    if (returnIfAny)
                    {
                        return valid;
                    }
                }
            }
            return valid;
        }

        protected virtual List<Drop> GetValidDrops(Player player, bool returnIfAny)
        {
            if (player == Player.None) throw new ArgumentException("'player' must be White or Black.", "player");
            List<Drop> valid = new List<Drop>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    Position pos = new Position((File)i, j);
                    List<Drop> validOnPos = GetValidDrops(player, pos, returnIfAny);
                    if (validOnPos.Count != 0)
                    {
                        valid.AddRange(validOnPos);
                        if (returnIfAny)
                        {
                            return valid;
                        }
                    }
                }
            }
            return valid;
        }
    }
}
