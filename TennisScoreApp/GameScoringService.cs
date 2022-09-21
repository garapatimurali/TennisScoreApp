using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TennisScoreApp
{
    public class GameScoringService
    {
        private readonly string[] validScores = new string[4] { "0", "15", "30", "40" };

        private List<Player> _players;

        public GameScoringService(string player1, string player2)
        {
            _players = new List<Player>()
            {
                new Player() { Id = 1, Name = player1},
                new Player() { Id = 2, Name = player2},
            };
        }

        /// <summary>
        /// Adds a point to the given player based on the Tennis scoring rules.
        /// </summary>
        /// <param name="player">Name of the player to whom we need to add a point</param>
        /// <returns>Score card with sets won by players and current game score.</returns>
        public string PointWonBy(string player)
        {
            var currentPlayer = GetCurrentPlayer(player);
            
            if (IsSetWon(currentPlayer))
                return $"{_players.First().SetsWon}-{_players.Last().SetsWon}";

            if (IsAdvantage(currentPlayer))
                return $"{_players.First().SetsWon}-{_players.Last().SetsWon}, Advantage {currentPlayer.Name}";

            if (IsDeuce(currentPlayer))
                return $"{_players.First().SetsWon}-{_players.Last().SetsWon}, Deuce";

            var prevScore = currentPlayer?.GameScore;
            currentPlayer.GameScore = validScores[Array.FindIndex(validScores, e => e.Equals(prevScore) == true) + 1];
            return $"{_players.First().SetsWon}-{_players.Last().SetsWon}, {_players.First().GameScore}-{_players.Last().GameScore}";
        }

        /// <summary>
        /// Finds out if a given player has won a set point.
        /// </summary>
        /// <param name="currentPlayer">the player who won the current game point</param>
        /// <returns>true if player has won a setpoint else false</returns>
        public bool IsSetWon(Player currentPlayer)
        {
            var otherPlayer = GetOtherPlayer(currentPlayer.Id);

            var currentPlayerCanWinFromAdvantage = currentPlayer.GameScore.Equals("ADV") 
                                                    && validScores.Contains(otherPlayer.GameScore);
            var currentPlayerCanWinFromFourty = currentPlayer.GameScore.Equals(validScores.Last()) 
                                                && validScores[0..(validScores.Length-1)].Contains(otherPlayer.GameScore);

            if (currentPlayerCanWinFromAdvantage || currentPlayerCanWinFromFourty)
            {
                SetPointWon(currentPlayer);
                return true;
            }
                
            return false;
        }

        /// <summary>
        /// Checks to see if the score are a tie between both players.
        /// </summary>
        /// <param name="currentPlayer">the player who won the current game point</param>
        /// <returns>true if game scores are tie else false</returns>
        public bool IsDeuce(Player currentPlayer)
        {
            var otherPlayer = GetOtherPlayer(currentPlayer.Id);
            if (currentPlayer.GameScore.Equals(validScores.Last()) && otherPlayer.GameScore == "ADV")
            {
                PlayersAreDeuce(currentPlayer, otherPlayer);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks to see if a player has got advantage point from deuce.
        /// </summary>
        /// <param name="currentPlayer">the player who won the current game point</param>
        /// <returns>true if the player has advantage else false</returns>
        public bool IsAdvantage(Player currentPlayer)
        {
            var otherPlayer = GetOtherPlayer(currentPlayer.Id);
            if (currentPlayer.GameScore.Equals(validScores.Last()) && otherPlayer.GameScore.Equals(validScores.Last()))
            {
                PlayerHasAdvantage(currentPlayer);
                return true;
            }

            return false;
        }

        private void PlayerHasAdvantage(Player player) => player.GameScore = "ADV";

        /// <summary>
        /// Resets the points of both players back to game point 40 when it is a tie (back from advantage for one player).
        /// </summary>
        /// <param name="currentPlayer">the player who won the current game point</param>
        /// <param name="otherPlayer">the other player who has lost the current game point</param>
        private void PlayersAreDeuce(Player currentPlayer, Player otherPlayer)
        {
            currentPlayer.GameScore = validScores.Last();
            otherPlayer.GameScore = validScores.Last();
        }

        /// <summary>
        /// Increments a set point for the player who won the current game point.
        /// </summary>
        /// <param name="currentPlayer">the player who won the current game point</param>
        private void SetPointWon(Player currentPlayer)
        {
            if (currentPlayer == null) return;

            currentPlayer.SetsWon += 1;
            currentPlayer.GameScore = validScores.First();

            var otherPlayer = GetOtherPlayer(currentPlayer.Id);
            if (otherPlayer == null) return;

            otherPlayer.GameScore = validScores.First();
        }

        public Player? GetCurrentPlayer(string player) => _players.Find(p => p.Name.ToUpper().Equals(player.ToUpper()));
        public Player? GetOtherPlayer(int? currentPlayerId) => _players.Find(p => p.Id != currentPlayerId);
    }
}
