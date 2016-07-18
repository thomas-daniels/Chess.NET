using NUnit.Framework;
using System.Collections.Generic;

namespace ChessDotNet.Tests
{
    [TestFixture]
    public static class PgnReaderTests
    {
        [Test]
        public static void TestPgns()
        {
            Dictionary<string, string> testValues = new Dictionary<string, string>()
            {
                { "1. e4 e5 2. d3 d6 3. Nf3 Nc6",  "r1bqkbnr/ppp2ppp/2np4/4p3/4P3/3P1N2/PPP2PPP/RNBQKB1R w KQkq - 2 4" },
                { "1. e4 d5 2. exd5", "rnbqkbnr/ppp1pppp/8/3P4/8/8/PPPP1PPP/RNBQKBNR b KQkq - 0 2" },
                { "1. Nf3 a5 2. Nc3 h5 3. Nd5 b5 4. Nf4 g6 5. Nd4 d6 6. e3 e6 7. Nfe2", "rnbqkbnr/2p2p2/3pp1p1/pp5p/3N4/4P3/PPPPNPPP/R1BQKB1R b KQkq - 1 7" },
                { "1. e4 e5 2. Bc4 Nc6 3. Qh5 Nf6 4. Qxf7#", "r1bqkb1r/pppp1Qpp/2n2n2/4p3/2B1P3/8/PPPP1PPP/RNB1K1NR b KQkq - 0 4" },
                { "1.e2e4! $0", "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1" },
                { "1. Nf3 a6 2. Nc3 h6 3. Nd5 a5 4. Nf4 h5 5. Nd4 b6 6. e3 e6 7. Ndxe6", "rnbqkbnr/2pp1pp1/1p2N3/p6p/5N2/4P3/PPPP1PPP/R1BQKB1R b KQkq - 0 7" },
                { "1. Nf3 a6 2. Nc3 h6 3. Nd5 a5 4. Ne5 a4 5. Nd3 f5 6. N5f4", "rnbqkbnr/1pppp1p1/7p/5p2/p4N2/3N4/PPPPPPPP/R1BQKB1R b KQkq - 1 6" },
                { "1. Nf3 a6 2. Nc3 h6 3. Nd5 a5 4. Ne5 a4 5. Nd3 f5 6. e3 f4 7. N3xf4", "rnbqkbnr/1pppp1p1/7p/3N4/p4N2/4P3/PPPP1PPP/R1BQKB1R b KQkq - 0 7" },
                { "1. e4 e5 2. Nf3 Nc6 3. Nc3 Nf6 4. Bc4 Bc5 5. O-O O-O 6. d3 h6 7. h3 Bb4 8. a3 Bxc3 9. bxc3 d5 10. exd5 Nxd5 11. Bxd5 Qxd5", "r1b2rk1/ppp2pp1/2n4p/3qp3/8/P1PP1N1P/2P2PP1/R1BQ1RK1 w - - 0 12" },
                { "1. h4 a6 2. h5 a5 3. h6 a4 4. hxg7 a3 5. gxh8=Q", "rnbqkbnQ/1ppppp1p/8/8/8/p7/PPPPPPP1/RNBQKBNR b KQq - 0 5" },
                { "1. h4?!", "rnbqkbnr/pppppppp/8/8/7P/8/PPPPPPP1/RNBQKBNR b KQkq h3 0 1" }
            };
            foreach (KeyValuePair<string, string> testValue in testValues)
            {
                PgnReader<ChessGame> reader = new PgnReader<ChessGame>();
                string pgn = testValue.Key;
                reader.ReadPgnFromString(pgn);
                Assert.AreEqual(testValue.Value, reader.Game.GetFen(), pgn);
            }
        }

        [Test]
        public static void TestExpectedExceptions()
        {
            string[] invalid = new string[]
            {
                "1. e5",
                "apgoeogeioe",
                "1. Nf3 a6 2. Nc3 h6 3. Nd5 a5 4. Ne5 a4 5. Nd3 f5 6. e3 f4 7. Nxf4",
                "1. Nf3 a5 2. Nc3 h5 3. Nd5 b5 4. Nf4 g6 5. Nd4 d6 6. e3 e6 7. Ne2",
                "1. Nf3 a5 2. Nc3 h5 3. Nd5 b5 4. Nf4 g6 5. Nd4 d6 6. e3 e6 7. Na2e2",
            };
            foreach (string inv in invalid)
            {
                PgnReader<ChessGame> reader = new PgnReader<ChessGame>();
                Assert.Throws<PgnException>(() => { reader.ReadPgnFromString(inv); });
            }
        }
    }
}