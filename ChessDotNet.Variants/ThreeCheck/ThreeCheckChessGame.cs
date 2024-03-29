﻿using ChessDotNet.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChessDotNet.Variants.ThreeCheck
{
    public class ThreeCheckChessGame : ChessGame
    {
        public int ChecksByWhite
        {
            get;
            protected set;
        }

        public int ChecksByBlack
        {
            get;
            protected set;
        }

        protected override int[] AllowedFenPartsLength
        {
            get
            {
                return new int[2] { 6, 7 };
            }
        }

        public ThreeCheckChessGame() : base() { }
        public ThreeCheckChessGame(Piece[][] board, Player whoseTurn) : base(board, whoseTurn) { }
        public ThreeCheckChessGame(GameCreationData data) : base(data) { }
        public ThreeCheckChessGame(string fen) : base(fen) {}
        public ThreeCheckChessGame(IEnumerable<Move> moves, bool movesAreValidated) : base(moves, movesAreValidated) { }

        protected override GameCreationData FenStringToGameCreationData(string fen)
        {
            GameCreationData gcd = base.FenStringToGameCreationData(fen);

            string[] parts = fen.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 7)
            {
                var re = new Regex(@"^\+(\d)\+(\d)$");
                Match m = re.Match(parts[6]);
                if (!m.Success)
                {
                    throw new ArgumentException("Invalid FEN: invalid check counter.");
                }
                gcd.ThreeCheck_ChecksByWhite = int.Parse(m.Groups[1].Value);
                gcd.ThreeCheck_ChecksByBlack = int.Parse(m.Groups[2].Value);
            }

            return gcd;
        }

        public override string GetFen()
        {
            return string.Format("{0} +{1}+{2}", base.GetFen(), ChecksByWhite, ChecksByBlack);
        }

        protected override void UseGameCreationData(GameCreationData data)
        {
            base.UseGameCreationData(data);
            ChecksByWhite = data.ThreeCheck_ChecksByWhite;
            ChecksByBlack = data.ThreeCheck_ChecksByBlack;
        }

        protected override MoveType ApplyMove(Move move, bool alreadyValidated, out Piece captured, out CastlingType castlingType)
        {
            MoveType ret = base.ApplyMove(move, alreadyValidated, out captured, out castlingType);
            if (ret == MoveType.Invalid)
            {
                return ret;
            }

            if (WhoseTurn == Player.White && IsInCheck(Player.White))
            {
                ChecksByBlack++;
            }
            if (WhoseTurn == Player.Black && IsInCheck(Player.Black))
            {
                ChecksByWhite++;
            }

            return ret;
        }

        public override bool IsWinner(Player player)
        {
            int checks = player == Player.White ? ChecksByWhite : ChecksByBlack;
            return checks >= 3 || IsCheckmated(ChessUtilities.GetOpponentOf(player));
        }

        public override bool Undo()
        {
            throw new NotImplementedException("Undo not implemented yet for three-checks.");
        }

        public override bool IsInsufficientMaterial()
        {
            var whitePieces = PiecesOnBoard.Where(p => p.Owner == Player.White).ToArray();
            var blackPieces = PiecesOnBoard.Where(p => p.Owner == Player.Black).ToArray();

            return whitePieces.Length == 1 && blackPieces.Length == 1 && whitePieces[0] is King && blackPieces[0] is King;
        }
    }
}
