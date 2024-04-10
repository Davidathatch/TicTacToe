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

        [TestMethod]
        public void ConstructBoard()
        {
            Board newBoard = new(3);
            Assert.IsNotNull(newBoard);
            Assert.AreEqual(8, newBoard.BoardComponents.Count);
            Assert.IsFalse(newBoard.GameOver);
        }
    }
}
