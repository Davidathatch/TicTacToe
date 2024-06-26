﻿using System.Drawing;

namespace TicTacToe.Models
{
    public class BoardTile()
    {

        /// <summary>
        /// True if this tile has been claimed by a player. 
        /// </summary>
        public bool Claimed { get; set; } = false;

        /// <summary>
        /// The player that claimed this tile.
        /// </summary>
        public Player? ClaimedBy { get; set; } = null;

        /// <summary>
        /// True if this tile is part of the row, column, or diagonal
        /// that won this round (if applicable).
        /// </summary>
        public bool WinningTile { get; set; } = false;

        /// <summary>
        /// This tile's position on the board.
        /// X = Column, Y = Row
        /// </summary>
        public Point TilePosition { get; set; }

        /// <summary>
        /// Event to be invoked when this tile is claimed.
        /// </summary>
        public event Action<BoardTile> OnClaimed;

        public void ClaimTile(Player claimedBy)
        {
            ClaimedBy = claimedBy;
            Claimed = true;
            OnClaimed?.Invoke(this);
            //TODO: finish tile play method
        }
    }
}
