namespace TicTacToe.Models
{
    /// <summary>
    /// Class holding various possible game statuses.
    /// </summary>
    public class GameStatus
    {
        /// <summary>
        /// The game is over and there is a winning player.
        /// </summary>
        /// <param name="winner">The winning player</param>
        public class Won(Player winner) : GameStatus
        {
            public Player Winner { get; set; } = winner;
        }

        /// <summary>
        /// The game is over and there is no winner.
        /// </summary>
        public class Tied : GameStatus
        {
        }

        /// <summary>
        /// The game is ongoing.
        /// </summary>
        public class Ongoing : GameStatus
        {

        }

        /// <summary>
        /// The game has not started yet.
        /// </summary>
        public class AwaitingStart : GameStatus
        {
        }
    }
}
