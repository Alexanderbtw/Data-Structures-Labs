using Lab3.Enums;
using Lab3.Models;

namespace Lab3.Tests
{
    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void WorkingCorrect()
        {
            //Arrange
            MyQueue<WorkTask> queue = new(2);
            WorkTask item1 = new WorkTask("ITask1", 1, TaskType.T0);
            WorkTask item2 = new WorkTask("ITask2", 2, TaskType.T1);

            //Art
            queue.Push(item1);
            queue.Push(item2);

            //Assert
            Assert.AreEqual(item1, queue.Pop());
            Assert.AreEqual(item2, queue.Pop());
        }

        [TestMethod]
        public void ThrowExceptionIfFullPush()
        {
            //Arrange
            MyQueue<WorkTask> queue = new(1);

            //Art
            queue.Push(new WorkTask());

            //Assert
            Assert.ThrowsException<InvalidOperationException>(() => queue.Push(new WorkTask()));
        }

        [TestMethod]
        public void ThrowExceptionIfEmptyPop()
        {
            //Arrange
            MyQueue<WorkTask> queue = new(0);

            //Art
            Action pop_action = () => queue.Pop();

            //Assert
            Assert.ThrowsException<InvalidOperationException>(pop_action);
        }
    }
}
