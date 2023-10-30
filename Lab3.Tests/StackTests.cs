using Lab3.Enums;
using Lab3.Models;

namespace Lab3.Tests
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void WorkingCorrect()
        {
            //Arrange
            MyStack<WorkTask> stack = new();
            WorkTask item1 = new WorkTask("ITask1", 1, TaskType.T0);
            WorkTask item2 = new WorkTask("ITask2", 2, TaskType.T1);

            //Art
            stack.Push(item1);
            stack.Push(item2);

            //Assert
            Assert.AreEqual(item2, stack.Pop());
            Assert.AreEqual(item1, stack.Pop());
        }

        [TestMethod]
        public void ThrowExceptionIfEmptyPop()
        {
            //Arrange
            Stack<WorkTask> stack = new();

            //Art
            Action pop_action = () => stack.Pop();

            //Assert
            Assert.ThrowsException<InvalidOperationException>(pop_action);
        }
    }
}
