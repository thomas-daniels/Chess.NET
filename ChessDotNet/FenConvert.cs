using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessDotNet.Pieces;

namespace ChessDotNet
{
    public static class FenConvert
    {
        public static string GameToFen(ChessGame game)
        {
            StringBuilder fenBuilder = new StringBuilder();
            ChessPiece[][] board = game.GetBoard();
            for (int i = 0; i < board.Length; i++)
            {
                ChessPiece[] row = board[i];
                int empty = 0;
                foreach (ChessPiece piece in row)
                {
                    char pieceChar = piece.GetFenCharacter()[0];
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

            fenBuilder.Append(game.WhoseTurn == Player.White ? 'w' : 'b');

            fenBuilder.Append(' ');

            bool hasAnyCastlingOptions = false;
            if (!game.WhiteKingMoved)
            {
                if (!game.WhiteRookHMoved)
                {
                    fenBuilder.Append('K');
                    hasAnyCastlingOptions = true;
                }
                if (!game.WhiteRookAMoved)
                {
                    fenBuilder.Append('Q');
                    hasAnyCastlingOptions = true;
                }
            }
            if (!game.BlackKingMoved)
            {
                if (!game.BlackRookHMoved)
                {
                    fenBuilder.Append('k');
                    hasAnyCastlingOptions = true;
                }
                if (!game.BlackRookAMoved)
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
            if (game.Moves.Count > 0 && (last = game.Moves[game.Moves.Count - 1]).Piece is Pawn && Math.Abs(last.OriginalPosition.Rank - last.NewPosition.Rank) == 2)
            {
                fenBuilder.Append(last.NewPosition.File.ToString().ToLowerInvariant());
                fenBuilder.Append(last.Player == Player.White ? 3 : 6);
            }
            else
            {
                fenBuilder.Append("-");
            }

            fenBuilder.Append(' ');

            IEnumerable<Move> movesReversed = game.Moves.Reverse();
            int halfmoveCounter = 0;
            foreach (DetailedMove move in movesReversed)
            {
                if (!(move.Piece is Pawn) && !move.IsCapture)
                {
                    halfmoveCounter++;
                }
                else
                {
                    break;
                }
            }
            fenBuilder.Append(halfmoveCounter);

            fenBuilder.Append(' ');

            fenBuilder.Append(game.Moves.Count / 2 + 1);

            return fenBuilder.ToString();
        }
    }
}
