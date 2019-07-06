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

        protected override bool UseTildesInFenGeneration
        {
            get
            {
                return true;
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
            return IsInCheck(player) && !HasAnyValidDrops(player) && !HasAnyValidMoves(player);
        }

        public override bool IsStalemated(Player player)
        {
            return WhoseTurn == player && !IsInCheck(player) && !HasAnyValidDrops(player) && !HasAnyValidMoves(player);
        }

        protected virtual bool IsValidDrop(Drop drop, bool validateCheck, bool careAboutWhoseTurnItIs)
        {
            ChessUtilities.ThrowIfNull(drop, nameof(drop));
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
            var copy = new CrazyhouseChessGame(GetFen());
            copy.SetPieceAt(drop.Destination.File, drop.Destination.Rank, drop.ToDrop);
            return copy.IsInCheck(player);
        }

        public virtual bool ApplyDrop(Drop drop, bool alreadyValidated)
        {
            ChessUtilities.ThrowIfNull(drop, nameof(drop));
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

            AddDetailedMove(new CrazyhouseDetailedMove(drop, "")); // placeholder without SAN for IsCheckmated/IsInCheck
            string san = string.Format("{0}@{1}{2}",
                drop.ToDrop is Pawn ? "" : char.ToUpperInvariant(drop.ToDrop.GetFenCharacter()).ToString(),
                drop.Destination.ToString().ToLowerInvariant(),
                IsCheckmated(WhoseTurn) ? "#" : (IsInCheck(WhoseTurn) ? "+" : ""));
            RemoveLastDetailedMove();
            AddDetailedMove(new CrazyhouseDetailedMove(drop, san));
            return true;
        }

        protected override MoveType ApplyMove(Move move, bool alreadyValidated, out Piece captured, out CastlingType castlingType)
        {
            MoveType ret = base.ApplyMove(move, alreadyValidated, out captured, out castlingType);
            if (ret.HasFlag(MoveType.Capture))
            {
                (move.Player == Player.White ? whitePocket : blackPocket).Add(!captured.IsPromotionResult ? captured.GetWithInvertedOwner() : new Pawn(ChessUtilities.GetOpponentOf(captured.Owner)));
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
            ChessUtilities.ThrowIfNull(on, nameof(on));
            if (GetPieceAt(on) != null || WhoseTurn != player) return new List<Drop>();
            if (WouldBeInCheckAfter(new Drop(new Queen(player), on, player), player)) return new List<Drop>();

            bool isValidForPawns = on.Rank != 1 && on.Rank != 8;
            var valid = new List<Drop>();
            foreach (Piece p in (player == Player.White ? whitePocket : blackPocket))
            {
                if (p is Pawn && !isValidForPawns) continue;
                var d = new Drop(p, on, player);
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
            var valid = new List<Drop>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    var pos = new Position((File)i, j);
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

        public override bool Undo()
        {
            throw new NotImplementedException("Undo not yet implemented for crazyhouse.");
        }
    }
}
