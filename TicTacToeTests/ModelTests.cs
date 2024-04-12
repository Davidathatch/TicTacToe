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
                        
                        //Move to the next tile
                        currentTile = currentTile.Next!;
                    }
                }
            }
        }
        
        //TODO: verify that tiles are added in order
    }
}
