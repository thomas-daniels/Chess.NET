using ChessDotNet.Pieces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChessDotNet
{
    public class PgnReader<TGame> where TGame : ChessGame, new()
    {
        public TGame Game
        {
            get;
            private set;
        }

        public PgnReader()
        {
            Game = new TGame();
        }

        public void ReadPgnFromString(string pgn)
        {
            IEnumerable<string> moves = pgn.Split(new char[] { '.', ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                           .Where(x => !((x.Length == 1 && char.IsDigit(x[0])) || x[0] == '$'));
            TGame game = new TGame();
            int ply = 0;
            foreach (string _ in moves)
            {
                string move = _.TrimEnd('#', '?', '!', '+').Trim();
                ply++;
                Player player = ply % 2 == 0 ? Player.Black : Player.White;
                Piece piece = game.MapPgnCharToPiece(move[0], player);
                if (!(piece is Pawn))
                {
                    move = move.Remove(0, 1);
                }

                if (move[0] == 'x')
                {
                    move = move.Remove(0, 1);
                }
                else if (move.Length == 4 && move[1] == 'x')
                {
                    move = move.Remove(1, 1);
                }

                Position destination;
                int rankRestriction = -1;
                File fileRestriction = File.None;
                Position origin = null;

                if (move.Length == 2)
                {
                    destination = new Position(move);
                }
                else if (move.Length == 3)
                {
                    if (char.IsDigit(move[0]))
                    {
                        rankRestriction = int.Parse(move[0].ToString());
                    }
                    else
                    {
                        bool recognized = Enum.TryParse<File>(move[0].ToString(), true, out fileRestriction);
                        if (!recognized)
                        {
                            throw new PgnException("Invalid PGN: unrecognized origin file.");
                        }
                    }
                    destination = new Position(move.Remove(0, 1));
                }
                else if (move.Length == 4)
                {
                    origin = new Position(move.Substring(0, 2));
                    destination = new Position(move.Substring(2, 2));
                }
                else
                {
                    throw new PgnException("Invalid PGN.");
                }

                if (origin != null)
                {
                    Move m = new Move(origin, destination, player);
                    if (game.IsValidMove(m))
                    {
                        game.ApplyMove(m, true);
                    }
                    else
                    {
                        throw new PgnException("Invalid PGN: contains invalid moves.");
                    }
                }
                else
                {
                    Piece[][] board = game.GetBoard();
                    List<Move> validMoves = new List<Move>();
                    for (int r = 0; r < game.BoardHeight; r++)
                    {
                        if (rankRestriction != -1 && r != 8 - rankRestriction) continue;
                        for (int f = 0; f < game.BoardWidth; f++)
                        {
                            if (fileRestriction != File.None && f != (int)fileRestriction) continue;
                            if (board[r][f] != piece) continue;
                            Move m = new Move(new Position((File)f, 8 - r), destination, player);
                            if (game.IsValidMove(m))
                            {
                                validMoves.Add(m);
                            }
                        }
                    }
                    if (validMoves.Count == 0) throw new PgnException("Invalid PGN: contains invalid moves.");
                    if (validMoves.Count > 1) throw new PgnException("Invalid PGN: contains ambiguous moves.");
                    game.ApplyMove(validMoves[0], true);
                }
            }
            Game = game;
        }
    }
}