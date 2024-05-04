using TicTacToe.Models;

namespace TicTacToe
{
    public class GameState
    {
        /// <summary>
        /// Player one
        /// </summary>
        public Player? PlayerOne { get; set; }

        /// <summary>
        /// Player two
        /// </summary>
        public Player? PlayerTwo { get; set; }

        /// <summary>
        /// Player whose turn it is currently
        /// </summary>
        public Player? CurrentPlayer { get; set; }

        public static void ClaimTile(string tileId) {}
    }
}
