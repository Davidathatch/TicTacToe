namespace TicTacToe.Models
{
    public class Board
    {
        public Board(int boardSize)
        {
            //If boardSize is an invalid value, throw an exception
            if (boardSize <= 0)
            {
                throw new ArgumentOutOfRangeException($"{boardSize} is an invalid Board size. Must be 1 or greater.");
            }
            
            BoardComponents = new();
            GameOver = false;
            GenerateBoard(boardSize);
        }

        /// <summary>
        /// Generates the board components and tiles, saving them to the dictionary <see cref="BoardComponents"/>.
        /// </summary>
        /// <param name="boardSize">Size of the board (board will always square)</param>
        private void GenerateBoard(int boardSize)
        {
            //Used to keep track of which tiles in the current row need to be added to the two diagonal components
            int ltrCounter = 1;
            int rtlCounter = boardSize;

            //Create the two diagonal components, of which there will only ever be two, regardless of the board size
            BoardComponent rtlBoardComponent = new("2rtl", ProcessComponentComplete);
            BoardComponent ltrBoardComponent = new("2ltr", ProcessComponentComplete);

            //Create all necessary rows and columns for the given grid size (the grid is always square)
            for (int componentIndex = 1; componentIndex <= boardSize; componentIndex++)
            {
                //Create Row (First digit of index is 0, second digit is row position)
                BoardComponent newRow = new($"0{componentIndex}", ProcessComponentComplete);

                //Create Column (First digit of index is 1, second digit is column position)
                BoardComponent newColumn = new($"1{componentIndex}", ProcessComponentComplete);

                for (int tileIndex = 1; tileIndex <= boardSize; tileIndex++)
                {
                    //Create new tile to be added to the board
                    BoardTile newTile = new();

                    //If the new tile needs to be added to either of the diagonal components, do so
                    if (tileIndex == rtlCounter)
                    {
                        newTile.OnClaimed += rtlBoardComponent.ProcessTileClaim;
                        rtlBoardComponent.AddBoardTile(newTile);
                    }

                    if (tileIndex == ltrCounter)
                    {
                        newTile.OnClaimed += ltrBoardComponent.ProcessTileClaim;
                        ltrBoardComponent.AddBoardTile(newTile);
                    }

                    /*
                     If the current index of the components (row/column) being created is equal to the index of the
                    tile being created within the components, only create one tile to assign to both components.
                     */
                    if (tileIndex == componentIndex)
                    {
                        newTile.OnClaimed += newRow.ProcessTileClaim;
                        newTile.OnClaimed += newColumn.ProcessTileClaim;
                        newRow.AddBoardTile(newTile);
                        newColumn.AddBoardTile(newTile);
                    }
                    //Otherwise, create a new tile for the column and add the previously created tile to the current row
                    else
                    {
                        newTile.OnClaimed += newRow.ProcessTileClaim;
                        newRow.AddBoardTile(newTile);

                        BoardTile newColumnTile = new();
                        newColumnTile.OnClaimed += newColumn.ProcessTileClaim;
                        newColumn.AddBoardTile(newColumnTile);
                    }
                }

                //Add rows and columns to BoardComponents
                BoardComponents.Add(newRow.BoardIndex, newRow);
                BoardComponents.Add(newColumn.BoardIndex, newColumn);

                //Increment and decrement the diagonal counters
                rtlCounter++;
                ltrCounter--;
            }

            //Add the two diagonal components
            BoardComponents.Add(rtlBoardComponent.BoardIndex, rtlBoardComponent);
            BoardComponents.Add(ltrBoardComponent.BoardIndex, ltrBoardComponent);
        }

        /// <summary>
        /// All components comprising this board.
        /// </summary>
        public Dictionary<string, BoardComponent> BoardComponents { get; set; }

        /// <summary>
        /// True if the game has ended.
        /// </summary>
        public bool GameOver { get; set; }

        /// <summary>
        /// Player whose turn it currently is.
        /// </summary>
        public Player? CurrentPlayer { get; set; }

        /// <summary>
        /// Takes a completed component and removes it from the dictionary.
        /// </summary>
        /// <param name="completedComponent"></param>
        public void ProcessComponentComplete(BoardComponent completedComponent)
        {
            BoardComponents.Remove(completedComponent.BoardIndex);

            //If the component has been completed and is still claimed by a player, then that player has won.
            if (completedComponent.ClaimedBy is not null && completedComponent.NonChallengedClaims > 0)
            {
                GameOver = true;
                completedComponent.ClaimedBy.Winner = true;
                completedComponent.MarkAsWinner();
            }
        }
    }
}
