using System.Drawing;

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
            BoardTiles = new();
            GameOver = false;
            BoardSize = boardSize;
            GenerateBoard(boardSize);
        }

        /// <summary>
        /// Generates the board components and tiles, saving them to the dictionary <see cref="BoardComponents"/>.
        /// </summary>
        /// <param name="boardSize">Size of the board (board will always square)</param>
        private void GenerateBoard(int boardSize)
        {
            //Coordinates of the next tile to be added to the two diagonal components
            Point nextLTR = new(1, 1);
            Point nextRTL = new(boardSize, 1);

            //Create all the components
            for (int index = 1; index <= boardSize; index++)
            {
                //Create and add the row and column at the current index
                BoardComponent newRow = new($"0{index}", ProcessComponentComplete);
                BoardComponent newColumn = new($"1{index}", ProcessComponentComplete);
                BoardComponents.Add(newRow.BoardIndex, newRow);
                BoardComponents.Add(newColumn.BoardIndex, newColumn);
            }
            
            //Create and add the two diagonal components
            BoardComponent ltrComponent = new("2ltr", ProcessComponentComplete);
            BoardComponent rtlComponent = new("2rtl", ProcessComponentComplete);
            BoardComponents.Add(ltrComponent.BoardIndex, ltrComponent);
            BoardComponents.Add(rtlComponent.BoardIndex, rtlComponent);

            for (int rowIndex = 1; rowIndex <= boardSize; rowIndex++)
            {
                BoardComponent currentRow = BoardComponents[$"0{rowIndex}"];

                for (int columnIndex = 1; columnIndex <= boardSize; columnIndex++)
                {
                    //Create the tile and save it to the board
                    BoardTile newTile = new() {TilePosition = new(columnIndex, rowIndex)};
                    BoardTiles.Add(newTile.TilePosition, newTile);
                    
                    BoardComponent currentColumn = BoardComponents[$"1{columnIndex}"];
                    
                    //Add the new tile to the current row and column
                    currentRow.AddBoardTile(newTile);
                    currentColumn.AddBoardTile(newTile);

                    Point currentCoordinates = new Point(columnIndex, rowIndex);
                    
                    //If this tile should be included in the left to right diagonal, add it
                    if (currentCoordinates.Equals(nextLTR))
                    {
                        ltrComponent.AddBoardTile(newTile);
                        nextLTR.X++;
                        nextLTR.Y++;
                    }

                    //If this tile should be included in the right to left diagonal, add it
                    if (currentCoordinates.Equals(nextRTL))
                    {
                        rtlComponent.AddBoardTile(newTile);
                        nextRTL.X--;
                        nextRTL.Y++;
                    }
                }
            }
        }

        /// <summary>
        /// All components comprising this board.
        /// </summary>
        public Dictionary<string, BoardComponent> BoardComponents { get; set; }

        /// <summary>
        /// Size of this board. Each component will contain this number of tiles.
        /// </summary>
        public int BoardSize { get; set; }

        /// <summary>
        /// All tiles on this board, accessible through their coordinates.
        /// X = Column, Y = Row
        /// </summary>
        public Dictionary<Point, BoardTile> BoardTiles { get; set; }

        /// <summary>
        /// True if the game has ended.
        /// </summary>
        public bool GameOver { get; set; }

        /// <summary>
        /// If the game has been won, this property will reference the
        /// winning component.
        /// </summary>
        public BoardComponent? WinningComponent { get; set; }

        /// <summary>
        /// Invoked once a component is won or all winnable components are gone.
        /// </summary>
        public event Action OnGameOver;

        /// <summary>
        /// Takes a completed component and removes it from the dictionary.
        /// </summary>
        /// <param name="completedComponent"></param>
        public void ProcessComponentComplete(BoardComponent completedComponent)
        {
            BoardComponents.Remove(completedComponent.BoardIndex);

            //If the component has been completed and is still claimed by a player, then that player has won.
            if (completedComponent.Uncontested && completedComponent.ClaimedTiles == completedComponent.BoardTiles.Count)
            {
                GameOver = true;
                completedComponent.MarkAsWinner();
                WinningComponent = completedComponent;
                OnGameOver?.Invoke();
            }
            //Otherwise, check that there are still components capable of being won. If there aren't any, end the game.
            else if (BoardComponents.Count == 0)
            {
                GameOver = true;
                OnGameOver?.Invoke();
            }
        }
    }
}
