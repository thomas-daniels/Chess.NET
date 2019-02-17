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
                { "1.e2e4! $0 { This is a comment. } {} *", "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1" },
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

        [Test]
        public static void Issue24_1()
        {
            PgnReader<ChessGame> reader = new PgnReader<ChessGame>();
            reader.ReadPgnFromString("1. c4 {[%clk 0:04:50.1]} 1... e5 {[%clk 0:05:00.3]} 2. Nc3 {[%clk 0:04:49.4]} 2... Nf6 {[%clk 0:05:01.4]} 3. g3 {[%clk 0:04:50]} 3... Nc6 {[%clk 0:05:02]} 4. Bg2 {[%clk 0:04:48.7]} 4... Bb4 {[%clk 0:05:03.3]} 5. d3 {[%clk 0:04:48.9]} 5... Bxc3+ {[%clk 0:05:03.2]} 6. bxc3 {[%clk 0:04:49.6]} 6... O-O {[%clk 0:05:04.6]} 7. e3 {[%clk 0:04:50.4]} 7... d5 {[%clk 0:05:03.8]} 8. cxd5 {[%clk 0:04:50.8]} 8... Nxd5 {[%clk 0:05:05.7]} 9. Bb2 {[%clk 0:04:45.4]} 9... Be6 {[%clk 0:05:00.5]} 10. Ne2 {[%clk 0:04:45.7]} 10... Re8 {[%clk 0:04:58.7]} 11. O-O {[%clk 0:04:45.8]} 11... Nce7 {[%clk 0:04:55]} 12. c4 {[%clk 0:04:44.9]} 12... Nb4 {[%clk 0:04:47.1]} 13. d4 {[%clk 0:04:37.2]} 13... exd4 {[%clk 0:04:42.9]} 14. Bxd4 {[%clk 0:04:32.9]} 14... Nbc6 {[%clk 0:04:38.3]} 15. Bd5 {[%clk 0:04:22.9]} 15... Bxd5 {[%clk 0:04:35.3]} 16. cxd5 {[%clk 0:04:23.6]} 16... Qxd5 {[%clk 0:04:33.9]} 17. Bb2 {[%clk 0:04:20.4]} 17... Qf3 {[%clk 0:04:28.4]} 18. Nf4 {[%clk 0:04:18.1]} 18... Qxd1 {[%clk 0:04:25]} 19. Rfxd1 {[%clk 0:04:18.4]} 19... Rad8 {[%clk 0:04:25.9]} 20. Ba3 {[%clk 0:04:14.7]} 20... g5 {[%clk 0:04:21.1]} 21. Nh5 {[%clk 0:04:12]} 21... Rxd1+ {[%clk 0:04:07.1]} 22. Rxd1 {[%clk 0:04:11.1]} 22... Rc8 {[%clk 0:03:50.4]} 23. Nf6+ {[%clk 0:04:07]} 23... Kg7 {[%clk 0:03:49.4]} 24. Bb2 {[%clk 0:04:06.9]} 24... Kg6 {[%clk 0:03:47.4]} 25. Rd7 {[%clk 0:04:00]} 25... h5 {[%clk 0:03:34.8]} 26. Ne4 {[%clk 0:03:46.5]} 26... Nf5 {[%clk 0:03:20.8]} 27. Bf6 {[%clk 0:03:36.1]} 27... g4 {[%clk 0:03:17.8]} 28. Bb2 {[%clk 0:03:21.1]} 28... b6 {[%clk 0:03:05.3]} 29. Nc3 {[%clk 0:03:17.1]} 29... Ne5 {[%clk 0:03:03.9]} 30. Nd5 {[%clk 0:03:14.1]} 30... Nxd7 {[%clk 0:03:01.8]} 31. Nf4+ {[%clk 0:03:13.5]} 31... Kg5 {[%clk 0:02:52.9]} 32. h4+ {[%clk 0:03:10.5]} 32... gxh3 {[%clk 0:02:52.7]} 33. Nxh3+ {[%clk 0:03:11]} 33... Kh6 {[%clk 0:02:46.4]} 34. Nf4 {[%clk 0:03:10.6]} 34... c5 {[%clk 0:02:39.2]} 0-1");
            Assert.AreEqual("2r5/p2n1p2/1p5k/2p2n1p/5N2/4P1P1/PB3P2/6K1 w - c6 0 35", reader.Game.GetFen());
        }

        [Test]
        public static void Issue24_2()
        {
            PgnReader<ChessGame> reader = new PgnReader<ChessGame>();
            reader.ReadPgnFromString("1. Nf3 {[%clk 0:04:43]} 1... g6 {[%clk 0:04:58.6]} 2. e4 {[%clk 0:04:42.6]} 2... Bg7 {[%clk 0:04:58.6]} 3. e5 {[%clk 0:04:43.6]} 3... Nc6 {[%clk 0:04:58]} 4. d4 {[%clk 0:04:42.2]} 4... e6 {[%clk 0:04:57.8]} 5. Bf4 {[%clk 0:04:41.3]} 5... Nge7 {[%clk 0:04:57.1]} 6. Bc4 {[%clk 0:04:40.8]} 6... O-O {[%clk 0:04:57.2]} 7. O-O {[%clk 0:04:41.6]} 7... f6 {[%clk 0:04:54.4]} 8. Nc3 {[%clk 0:04:35.3]} 8... fxe5 {[%clk 0:04:55.2]} 9. Bxe5 {[%clk 0:04:35.3]} 9... Nxe5 {[%clk 0:04:54.4]} 10. Nxe5 {[%clk 0:04:35.8]} 10... Bxe5 {[%clk 0:04:53.8]} 11. dxe5 {[%clk 0:04:35.8]} 11... Nc6 {[%clk 0:04:55.2]} 12. f4 {[%clk 0:04:33.6]} 12... d5 {[%clk 0:04:44.6]} 13. exd6 {[%clk 0:04:32.4]} 13... Qxd6 {[%clk 0:04:45.7]} 14. Qxd6 {[%clk 0:04:28.8]} 14... cxd6 {[%clk 0:04:46.9]} 15. Nb5 {[%clk 0:04:26]} 15... d5 {[%clk 0:04:39.7]} 16. Nc7 {[%clk 0:04:23.8]} 16... Rb8 {[%clk 0:04:36.5]} 17. Bb5 {[%clk 0:04:20.1]} 17... a6 {[%clk 0:04:36]} 18. Ba4 {[%clk 0:04:20.3]} 18... b5 {[%clk 0:04:35.4]} 19. Bb3 {[%clk 0:04:19.4]} 19... Rb7 {[%clk 0:04:34.2]} 20. Nxd5 {[%clk 0:04:14.3]} 20... exd5 {[%clk 0:04:12.6]} 21. Bxd5+ {[%clk 0:04:13.1]} 21... Kg7 {[%clk 0:04:14.1]} 22. Bxc6 {[%clk 0:04:11.8]} 22... Rc7 {[%clk 0:04:15.6]} 23. Be4 {[%clk 0:04:10.3]} 23... Bf5 {[%clk 0:04:13.9]} 24. Bxf5 {[%clk 0:04:09.2]}  24... Rxf5 {[%clk 0:04:15.3]} 25. c3 {[%clk 0:04:10.2]} 25... Rd5 {[%clk 0:04:16.4]} 26. Rad1 {[%clk 0:04:08.9]} 26... Rcd7 {[%clk 0:04:16.7]} 27. Rxd5 {[%clk 0:04:08.7]} 27... Rxd5 {[%clk 0:04:18.2]} 28. Rb1 {[%clk 0:04:08.6]} 28... Rd2 {[%clk 0:04:18]} 29. h3 {[%clk 0:04:09]} 29... Kf6 {[%clk 0:04:18.7]} 30. Kh2 {[%clk 0:04:09]} 30... h5 {[%clk 0:04:18.4]} 31. Kg3 {[%clk 0:04:10.1]} 31... Kf5 {[%clk 0:04:18.4]} 32. Kf3 {[%clk 0:04:09.1]} 32... h4 {[%clk 0:04:07.8]} 33. g4+ {[%clk 0:03:51.1]} 33... hxg3 {[%clk 0:04:07.1]} 34. Kxg3 {[%clk 0:03:52.2]} 34... Rd3+ {[%clk 0:04:08.1]} 35. Kg2 {[%clk 0:03:49.7]} 35... Kxf4 {[%clk 0:04:09.3]} 36. Rf1+ {[%clk 0:03:46.6]} 36... Kg5 {[%clk 0:04:10.4]} 37. Rf2 {[%clk 0:03:43.6]} 37... Kh4 {[%clk 0:04:06.3]} 38. Rf4+ {[%clk 0:03:39]} 38... Kg5 {[%clk 0:04:04.2]} 39. Rg4+ {[%clk 0:03:39.6]} 39... Kf5 {[%clk 0:04:01.5]} 40. Rb4 {[%clk 0:03:36.3]} 40... g5 {[%clk 0:03:44]} 41. a3 {[%clk 0:03:28.3]} 41... Ke5 {[%clk 0:03:33.9]} 42. Kh2 {[%clk 0:03:21.4]} 42... Kf5 {[%clk 0:03:30.4]} 43. c4 {[%clk 0:03:13.9]} 43... bxc4 {[%clk 0:03:26.4]} 44. Rxc4 {[%clk 0:03:13.6]} 44... Rd2+ {[%clk 0:03:27.5]} 45. Kg3 {[%clk 0:03:13.4]} 45... Rxb2 {[%clk 0:03:26]} 46. Rc5+ {[%clk 0:03:14.2]} 46... Ke4 {[%clk 0:03:23.9]} 47. Ra5 {[%clk 0:03:09.6]} 47... Rb3+ {[%clk 0:03:06.3]} 48. Kg4 {[%clk 0:03:08.3]} 48... Kd4 {[%clk 0:03:05.7]} 49. Rxa6 {[%clk 0:03:03.4]} 49... Kc4 {[%clk 0:02:58.9]} 50. a4 {[%clk 0:03:03.1]} 50... Ra3 {[%clk 0:02:49.1]} 51. Ra5 {[%clk 0:02:57.9]} 51... Kb4 {[%clk 0:02:45.4]} 52. Rxg5 {[%clk 0:02:56.4]} 52... Rxa4 {[%clk 0:02:42.6]} 53. h4 {[%clk 0:02:55.4]} 53... Kc3+ {[%clk 0:02:41.5]} 54. Kh5 {[%clk 0:02:55.1]} 54... Kd3 {[%clk 0:02:40.3]} 55. Rg4 {[%clk 0:02:52.5]} 55... Ra5+ {[%clk 0:02:39.1]} 56. Kg6 {[%clk 0:02:52.1]} 56... Ke3 {[%clk 0:02:38.1]} 57. h5 {[%clk 0:02:52.5]} 57... Kf3 {[%clk 0:02:39.2]} 58. Rg5 {[%clk 0:02:52]} 58... Ra8 {[%clk 0:02:38.4]} 59. Kh7 {[%clk 0:02:52.6]} 59... Kf4 {[%clk 0:02:38.7]} 60. Rg7 {[%clk 0:02:52.4]} 60... Ra6 {[%clk 0:02:34.2]} 61. h6 {[%clk 0:02:50.1]} 61... Kf5 {[%clk 0:02:34.4]} 62. Rg1 {[%clk 0:02:41.3]} 62... Ra7+ {[%clk 0:02:28.4]} 63. Kg8 {[%clk 0:02:40.5]} 63... Ra8+ {[%clk 0:02:26.5]} 64. Kf7 {[%clk 0:02:40.7]} 64... Ra7+ {[%clk 0:02:27.5]} 65. Ke8 {[%clk 0:02:40.6]} 65... Ra8+ {[%clk 0:02:26.8]} 66. Kd7 {[%clk 0:02:41.6]} 66... Ra7+ {[%clk 0:02:25.6]} 67. Ke8 {[%clk 0:02:37.6]} 67... Ra8+ {[%clk 0:02:26.5]} 68. Kf7 {[%clk 0:02:38.4]} 68... Ra7+ {[%clk 0:02:27.6]} 69. Kg8 {[%clk 0:02:39.6]} 69... Ra8+ {[%clk 0:02:28.8]} 70. Kh7 {[%clk 0:02:41]} 70... Ra7+ {[%clk 0:02:30]} 71. Kh8 {[%clk 0:02:40.6]} 71... Ra8+ {[%clk 0:02:27.7]} 72. Rg8 {[%clk 0:02:39.7]} 72... Ra6 {[%clk 0:02:18.8]} 73. h7 {[%clk 0:02:40.2]} 73... Ra7 {[%clk 0:02:10.3]} 74. Rg7 {[%clk 0:02:26]} 74... Ra8+ {[%clk 0:02:10.1]} 75. Rg8 {[%clk 0:02:27.1]} 75... Ra7 {[%clk 0:02:11.3]} 76. Rg3 {[%clk 0:02:12.5]} 76... Ra8+ {[%clk 0:02:08.1]} 77. Kg7 {[%clk 0:02:11.5]} 77... Ra7+ {[%clk 0:02:08.1]} 78. Kh6 {[%clk 0:02:11.6]} 78... Ra8 {[%clk 0:02:05.9]} 79. Rg8 {[%clk 0:02:08.9]} 79... Ra6+ {[%clk 0:02:07]} 80. Kh5 {[%clk 0:02:09.6]} 80... Ra1 {[%clk 0:02:02.7]} 81. Kh6 {[%clk 0:02:03]} 81... Ra6+ {[%clk 0:02:00.7]} 82. Kh5 {[%clk 0:02:00.7]} 1-0");
            Assert.AreEqual("6R1/7P/r7/5k1K/8/8/8/8 b - - 18 82", reader.Game.GetFen());
        }

        [Test]
        public static void Issue24_3()
        {
            PgnReader<ChessGame> reader = new PgnReader<ChessGame>();
            reader.ReadPgnFromString("1. Na3 d5 2. Nb1 Kd7 3. Na3 Ke6 4. Nb1 Kf5 5. Na3 h5 6. Nb1 h4 7. g4+ hxg3");
            Assert.AreEqual("rnbq1bnr/ppp1ppp1/8/3p1k2/8/6p1/PPPPPP1P/RNBQKBNR w KQ - 0 8", reader.Game.GetFen());
        }
    }
}