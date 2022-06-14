using Binary_Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UTest
{
    [TestClass]
    public class BinaryTreeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            TreeViewModel viewModel = new TreeViewModel();
            PrivateObject obj = new PrivateObject(viewModel);
            object[] parameters = { "((15 ÷ (7 - (1 + 1) ) ) x -3 ) - (2 + (1 + 1))"
                                    , true };

            // Act
            var result = obj.Invoke("CreateBinaryTree", parameters);

            // Assert
            Assert.AreEqual(-13, (int)result);
        }
    }
}
