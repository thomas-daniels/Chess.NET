using NUnit.Framework;

namespace ChessDotNet.Variants.Tests
{
    using KingOfTheHill;

    [TestFixture]
    public class KothChessGameTests
    {
        [Test]
        public void TestKingNotInCenter()
        {
            KingOfTheHillChessGame game = new KingOfTheHillChessGame("rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2");

            Assert.False(game.IsKingInCenter(Player.White));
            Assert.False(game.IsKingInCenter(Player.Black));
        }

        [Test]
        public void TestWhiteKingInCenter()
        {
            KingOfTheHillChessGame game = new KingOfTheHillChessGame("rnbqkbnr/ppp2ppp/8/4p3/4K3/8/PPPP1PPP/RNBQ1BNR b kq - 0 4");

            Assert.True(game.IsKingInCenter(Player.White));
            Assert.False(game.IsKingInCenter(Player.Black));
            Assert.True(game.IsWinner(Player.White));
        }
    }
}
