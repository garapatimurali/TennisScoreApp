using TennisScoreApp;

namespace TennisScoreAppUnitTests
{
    [TestClass]
    public class GameScoringTest
    {
        [TestMethod]
        public void PointWonBy_WhenAdvantageCurrentPlayer_ShouldWinSetPointCurrentPlayer()
        {

            var scoringService = new GameScoringService("player1", "player2");
            var currentPlayer = scoringService.GetCurrentPlayer("player1");
            var otherPlayer = scoringService.GetOtherPlayer(currentPlayer.Id);

            currentPlayer.GameScore = "ADV";
            otherPlayer.GameScore = "40";

            var isSetWon = scoringService.IsSetWon(currentPlayer);
            Assert.IsTrue(isSetWon);
            Assert.AreEqual(currentPlayer.SetsWon, 1);
            Assert.AreEqual(currentPlayer.GameScore, "0");
            Assert.AreEqual(currentPlayer.GameScore, otherPlayer.GameScore);
        }

        [TestMethod]
        public void PointWonBy_WhenBothPlayersAtFOurty_ShouldBeAdvantageCurrentPlayer()
        {
            var scoringService = new GameScoringService("player1", "player2");
            var currentPlayer = scoringService.GetCurrentPlayer("player1");
            var otherPlayer = scoringService.GetOtherPlayer(currentPlayer.Id);
            currentPlayer.GameScore = "40";
            otherPlayer.GameScore = "40";

            var isAdvantage = scoringService.IsAdvantage(currentPlayer);

            Assert.IsTrue(isAdvantage);
            Assert.AreEqual(currentPlayer.GameScore, "ADV");
            Assert.AreEqual(currentPlayer.SetsWon, 0);
        }

        [TestMethod]
        public void PointWonBy_WhenOnePlayerAdvantageOtherFourty_ShouldBeDeuce()
        {
            var scoringService = new GameScoringService("player1", "player2");
            var currentPlayer = scoringService.GetCurrentPlayer("player1");
            var otherPlayer = scoringService.GetOtherPlayer(currentPlayer.Id);
            currentPlayer.GameScore = "ADV";
            otherPlayer.GameScore = "40";

            var isDeuce = scoringService.IsDeuce(otherPlayer);

            Assert.IsTrue(isDeuce);
            Assert.AreEqual(currentPlayer.GameScore, otherPlayer.GameScore);
            Assert.AreEqual(currentPlayer.SetsWon, 0);
        }

        [TestMethod]
        public void PointWonBy_WhenPointWonBuPlayer_ShouldIncrementPlayerPoint()
        {
            var scoringService = new GameScoringService("player1", "player2");
            var currentPlayer = scoringService.GetCurrentPlayer("player1");
            var otherPlayer = scoringService.GetOtherPlayer(currentPlayer.Id);
            currentPlayer.GameScore = "15";
            otherPlayer.GameScore = "30";

            scoringService.PointWonBy("player1");

            Assert.AreEqual(currentPlayer.GameScore, "30");
            Assert.AreEqual(currentPlayer.SetsWon, 0);
        }

    }
}