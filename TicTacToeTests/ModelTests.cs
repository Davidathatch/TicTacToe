using System.Drawing;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Models;

namespace TicTacToe.Tests
{
    [TestClass]
    public class ModelTests
    {
        [TestInitialize]
        public void TestInitialization()
        {
            Player playerOne = new('X');
            Player playerTwo = new('O');
        }

        /// <summary>
        /// Check that the Board constructor works correctly.
        /// </summary>
        [TestMethod]
        public void ConstructBoard()
        {
            Board newBoard = new(3);
            Assert.IsNotNull(newBoard);
            Assert.AreEqual(8, newBoard.BoardComponents.Count);
            Assert.IsFalse(newBoard.GameOver);
        }

        /// <summary>
        /// Check that the Board constructor creates the correct number of components and tiles, even with
        /// varying board sizes.
        /// </summary>
        /// <param name="sizes">A list of board sizes to test</param>
        [TestMethod]
        [DataRow(1, 2, 3, 4, 5, 6, 7, 8, 9)]
        public void ConstructDifferentBoardSizes(params int[] sizes)
        {
            foreach (int siz in sizes)
            {
                Board newBoard = new(siz);

                //Check that the correct number of BoardComponents were created
                Assert.AreEqual((siz * 2) + 2, newBoard.BoardComponents.Count);

                //Check that the correct number of BoardTiles were created
                foreach (BoardComponent component in newBoard.BoardComponents.Values)
                {
                    Assert.AreEqual(siz, component.BoardTiles.Count);
                }
            }
        }

        /// <summary>
        /// Check that attempting to create a Board with a size less than or equal to zero results
        /// in an exception being thrown.
        /// </summary>
        [TestMethod]
        public void InvalidBoardSizeThrowsException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Board newBoard = new(0); });
        }

        /// <summary>
        /// Checks that when a board is created, all BoardTiles belong to the correct BoardComponents.
        /// </summary>
        [TestMethod]
        [DataRow(1, 2, 3, 4, 5, 6, 7, 8, 9)]
        public void CheckObjectRelationships(params int[] sizes)
        {
            //Test for several different board sizes
            foreach (int siz in sizes)
            {
                Board newBoard = new(siz);

                //Track the next point in which the current tile should be contained within a diagonal component
                Point nextLTR = new(1, 1);
                Point nextRTL = new(1, siz);

                //Loop through each row in this board
                for (int rowIndex = 1; rowIndex <= siz; rowIndex++)
                {
                    //Get the current row and tile
                    BoardComponent currentRow = newBoard.BoardComponents[$"0{rowIndex}"];
                    LinkedListNode<BoardTile> currentTile = currentRow.BoardTiles.First!;

                    //Loop through all the boardTiles in this row
                    for (int columnIndex = 1; columnIndex <= siz; columnIndex++)
                    {
                        //Check that the current tile is the same as the tile in the column component
                        BoardTile columnTile =
                            newBoard.BoardComponents[$"1{columnIndex}"].BoardTiles.ElementAt(rowIndex - 1);

                        Assert.AreSame(currentTile.Value, columnTile);

                        //Check that tile appears in the diagonal components, if applicable
                        Point currentCoordinates = new(columnIndex, rowIndex);
                        if (currentCoordinates.Equals(nextLTR))
                        {
                            BoardTile ltrTile = newBoard.BoardComponents["2ltr"].BoardTiles.ElementAt(rowIndex - 1);
                            Assert.AreSame(currentTile.Value, ltrTile);
                            nextLTR.X++;
                            nextLTR.Y++;
                        }

                        if (currentCoordinates.Equals(nextRTL))
                        {
                            BoardTile rtlTile = newBoard.BoardComponents["2rtl"].BoardTiles.ElementAt(rowIndex - 1);
                            Assert.AreSame(currentTile.Value, rtlTile);
                            nextRTL.X--;
                            nextRTL.Y++;
                        }

                        //Move to the next tile
                        currentTile = currentTile.Next!;
                    }
                }
            }
        }

        /// <summary>
        /// Tests that all tiles in the board are stored in the BoardTiles dictionary with
        /// the correct coordinates. This is done by retrieving tiles using their expected points
        /// and verifying that the appropriate BoardComponents are altered upon claiming a tile.
        ///
        /// Examples:
        /// Claiming tiles (1, 1), (2, 2), and (3, 3) should remove the top row 01,
        /// Claiming tiles (1, 1), (1, 2), and (1, 3) should remove the left column 11.
        /// Claiming tiles (1, 1), (2, 2) and (3, 3) should remove the left to right diagonal 2ltr.
        ///
        /// With each tile being claimed, the number of non-challenged claims is also verified.
        /// </summary>
        /// <param name="sizes"></param>
        [TestMethod]
        [DataRow(1, 2, 3, 4, 5, 6, 7, 8, 9)]
        public void TestTilesInOrder(params int[] sizes)
        {
            foreach (int siz in sizes)
            {
                Board newBoard = new(siz);
                Player testPlayer = new('X');

                //Loop through all the rows on the board
                for (int rowIndex = 1; rowIndex <= siz; rowIndex++)
                {
                    BoardComponent currentRow = newBoard.BoardComponents[$"0{rowIndex}"];
                    Assert.IsNull(currentRow.ClaimedBy);
                    Assert.AreEqual(0, currentRow.ClaimedTiles);
                    
                    //Mark all the tiles in this row as claimed by player one
                    for (int tileIndex = 1; tileIndex <= siz; tileIndex++)
                    {
                        newBoard.BoardTiles[new Point(tileIndex, rowIndex)].ClaimTile(testPlayer);
                        Assert.AreEqual(tileIndex, currentRow.ClaimedTiles);
                    }
                    
                    //Check that the row was completely claimed, and was therefore removed from the board
                    Assert.IsTrue(newBoard.GameOver);
                    
                    //Reset for the next row
                    newBoard.GameOver = false;
                }

                //Reset the board for the next test
                newBoard = new(siz);
                
                //Loop through all the columns on the board
                for (int columnIndex = 1; columnIndex <= siz; columnIndex++)
                {
                    BoardComponent currentColumn = newBoard.BoardComponents[$"1{columnIndex}"];
                    Assert.IsNull(currentColumn.ClaimedBy);
                    Assert.AreEqual(0, currentColumn.ClaimedTiles);
                    
                    //Mark all the tiles in this column as claimed by player one
                    for (int tileIndex = 1; tileIndex <= siz; tileIndex++)
                    {
                        newBoard.BoardTiles[new Point(columnIndex, tileIndex)].ClaimTile(testPlayer);
                        Assert.AreEqual(tileIndex, currentColumn.ClaimedTiles);
                    }
                    
                    //Check that the column was completely claimed, and was therefore removed from the board
                    Assert.IsTrue(newBoard.GameOver);
                    
                    //Reset for the next column
                    newBoard.GameOver = false;
                }
                
                //Reset the board for the next test
                newBoard = new(siz);
                
                //Loop through the left to right diagonal
                BoardComponent ltrComponent = newBoard.BoardComponents["2ltr"];
                Assert.IsNull(ltrComponent.ClaimedBy);
                Assert.AreEqual(0, ltrComponent.ClaimedTiles);
                
                for (int tileIndex = 1; tileIndex <= siz; tileIndex++)
                {
                    newBoard.BoardTiles[new Point(tileIndex, tileIndex)].ClaimTile(testPlayer);
                    Assert.AreEqual(tileIndex, ltrComponent.ClaimedTiles);
                }
                
                //Check that the diagonal was completely claimed, and was therefore removed from the board
                Assert.IsTrue(newBoard.GameOver);
                
                //Reset for the next diagonal
                newBoard = new(siz);
                
                //Loop through the right to left diagonal
                BoardComponent rtlComponent = newBoard.BoardComponents["2rtl"];
                Assert.IsNull(rtlComponent.ClaimedBy);
                Assert.AreEqual(0, rtlComponent.ClaimedTiles);
                
                for (int tileIndex = 1; tileIndex <= siz; tileIndex++)
                {
                    newBoard.BoardTiles[new Point(siz - (tileIndex - 1), tileIndex)].ClaimTile(testPlayer);
                    Assert.AreEqual(tileIndex, rtlComponent.ClaimedTiles);
                }
                
                //Check that the diagonal was completely claimed, and was therefore removed from the board
                Assert.IsTrue(newBoard.GameOver);
                
                //Reset for the next diagonal
                newBoard.GameOver = false;
            }
        }

        [TestMethod]
        [DataRow(1, 2, 3, 4, 5, 6, 7, 8, 9)]
        public void CheckComponentCompletionUncontested(params int[] sizes)
        {
            foreach (int siz in sizes)
            {
                var testBoard = new Board(siz);
                var testPlayerOne = new Player('x');
                var testPlayerTwo = new Player('o');

                //Test that rows are only marked as claimed when each tile is claimed by a single player
                for (int rowIndex = 1; rowIndex <= siz; rowIndex++)
                {
                    var currentRow = testBoard.BoardComponents[$"0{rowIndex}"];

                    //Claim each tile in this component, verifying that the component isn't claimed until each tile is claimed
                    foreach (BoardTile boardTile in currentRow.BoardTiles)
                    {
                        Assert.IsFalse(currentRow.Unwinnable);
                        boardTile.ClaimTile(testPlayerOne);
                    }

                    //Verify that the component is claimed, now that each tile has been claimed
                    Assert.IsTrue(currentRow.Unwinnable);
                }

                //Reset the board and test the columns
                testBoard = new Board(siz);

                for (int columnIndex = 1; columnIndex <= siz; columnIndex++)
                {
                    var currentColumn = testBoard.BoardComponents[$"1{columnIndex}"];

                    foreach (BoardTile boardTile in currentColumn.BoardTiles)
                    {
                        Assert.IsFalse(currentColumn.Unwinnable);
                        boardTile.ClaimTile(testPlayerOne);
                    }

                    Assert.IsTrue(currentColumn.Unwinnable);
                }

                //Reset the board and test the left to right diagonal
                testBoard = new Board(siz);
                var LTR = testBoard.BoardComponents["2ltr"];

                foreach (BoardTile boardTile in LTR.BoardTiles)
                {
                    Assert.IsFalse(LTR.Unwinnable);
                    boardTile.ClaimTile(testPlayerOne);
                }

                Assert.IsTrue(LTR.Unwinnable);

                //Reset the board and test the right to left diagonal
                testBoard = new Board(siz);
                var RTL = testBoard.BoardComponents["2rtl"];

                foreach (BoardTile boardTile in RTL.BoardTiles)
                {
                    Assert.IsFalse(RTL.Unwinnable);
                    boardTile.ClaimTile(testPlayerOne);
                }

                Assert.IsTrue(RTL.Unwinnable);
            }
        }
    }
}
