namespace ChessDotNet
{
    public struct Position
    {
        public enum Files
        {
            A = 0,
            B = 1,
            C = 2,
            D = 3,
            E = 4,
            F = 5,
            G = 6,
            H = 7,
            None = -1
        }

        public enum Ranks
        {
            One = 7,
            Two = 6,
            Three = 5,
            Four = 4,
            Five = 3,
            Six = 2,
            Seven = 1,
            Eight = 0,
            None = -1
        }

        Files _file;
        public Files File
        {
            get
            {
                return _file;
            }
        }

        Ranks _rank;
        public Ranks Rank
        {
            get
            {
                return _rank;
            }
        }

        public Position(Files file, Ranks rank)
        {
            _file = file;
            _rank = rank;
        }
    }
}
