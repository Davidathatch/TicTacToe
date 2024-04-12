namespace TicTacToe.Models
{
    public class BoardComponent
    {
        /// <summary>
        /// Creates a new board component with no tiles.
        /// </summary>
        /// <param name="boardIndex">Value for <see cref="BoardIndex"/></param>
        /// <param name="onComplete">Value for <see cref="OnComplete"/></param>
        public BoardComponent(string boardIndex, Action<BoardComponent> onComplete)
        {
            BoardIndex = boardIndex;
            OnComplete = onComplete;

            BoardTiles = new();
            NonChallengedClaims = 0;
        }

        /// <summary>
        /// List containing all BoardTiles contained in this component.
        /// </summary>
        public LinkedList<BoardTile> BoardTiles { get; set; }

        /// <summary>
        /// The number of tiles in this component that have been claimed by a single
        /// player without competition.
        /// </summary>
        public int NonChallengedClaims { get; set; }

        /// <summary>
        /// Player who currently has claim over this component. It is claimed
        /// once a player claims one tile, and their claim remains until another
        /// player claims a tile in this component.
        /// </summary>
        public Player? ClaimedBy { get; set; }

        /// <summary>
        /// The index by which this component can be accessed in the Board class.
        ///
        /// First character represents the type of component (0 = Row, 1 = Column, 2 = Diagonal)
        /// Second character represents the position of the column or row, or LTR/RTL for diagonal.
        /// </summary>
        public string BoardIndex { get; set; }

        /// <summary>
        /// Method to be called once this component has either been won, or has tiles
        /// claimed by two different players, making it unwinnable.
        /// </summary>
        public Action<BoardComponent> OnComplete { get; set; }

        /// <summary>
        /// Processes a newly claimed tile
        /// </summary>
        /// <param name="claimedTile">Tile that has been claimed</param>
        public void ProcessTileClaim(BoardTile claimedTile)
        {
            //This is the first tile within the component being claimed:
            if (ClaimedBy is null)
            {
                ClaimedBy = claimedTile.ClaimedBy;
                NonChallengedClaims++;
                return;
            }

            //This is not the first tile within this component being claimed, and it is being claimed by the same player:
            if (Equals(ClaimedBy, claimedTile.ClaimedBy))
            {
                NonChallengedClaims++;

                //If this component has been entirely claimed by a single player, alert the board.
                if (NonChallengedClaims == BoardTiles.Count)
                {
                    OnComplete(this);
                }

                return;
            }

            //This is not the first tile within this component being claimed, and it is being claimed by a different player:
            NonChallengedClaims = 0;
            ClaimedBy = null;
            OnComplete(this);
            return;
        }

        /// <summary>
        /// Add a tile to this component. Tiles should be added in the order they appear on the board (left to right
        /// for rows, top to bottom for columns)
        /// </summary>
        /// <param name="newTile">Tile being added</param>
        public void AddBoardTile(BoardTile newTile)
        {
            BoardTiles.AddLast(newTile);
        }

        /// <summary>
        /// Mark each tile within this component as a winning tile.
        /// </summary>
        public void MarkAsWinner()
        {
            foreach (BoardTile boardTile in BoardTiles)
            {
                boardTile.WinningTile = true;
            }
        }
    }
}