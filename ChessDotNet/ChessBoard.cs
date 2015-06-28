using System.Collections.Generic;

namespace ChessDotNet
{
    public class ChessBoard
    {
        public Pieces[,] Board
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
            Board = new Pieces[8, 8];
            Moves = new List<Move>();
            InitBoard();
        }

        public void InitBoard()
        {
            Pieces r = Pieces.Rook;
            Pieces q = Pieces.Queen;
            Pieces k = Pieces.King;
            Pieces n = Pieces.Knight;
            Pieces b = Pieces.Bishop;
            Pieces p = Pieces.Pawn;
            Pieces o = Pieces.None;
            Board = new Pieces[8, 8]
            {
                { r, n, b, q, k, b, n, r },
                { p, p, p, p, p, p, p, p },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, o, o, o, o },
                { o, o, o, o, o, o, o, o },
                { p, p, p, p, p, p, p, p },
                { r, n, b, q, k, b, n, r }
            };
        }
    }
}
