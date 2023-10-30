using Lab3.Models;

namespace Lab3.Tests
{
    [TestClass]
    public class ProcessorTests
    {
        [TestMethod]
        public void ThrowExceptionIfAlreadySet()
        {
            //Arrange
            Processor proc = new(Enums.TaskType.T0);
            WorkTask task1 = new();
            WorkTask task2 = new();

            //Art
            proc.SetTask(task1);

            //Assert
            Assert.ThrowsException<InvalidOperationException>(() => proc.SetTask(task2));
        }

        [TestMethod]
        public void ThrowExceptionIfTypeNotEquals()
        {
            Processor proc = new(Enums.TaskType.T0);

            WorkTask task = new("Test", 1, Enums.TaskType.T1);

            Assert.ThrowsException<InvalidOperationException>(() => proc.SetTask(task));
        }

        [TestMethod]
        public void ThrowExceptionIfTaskNotSetWhenGet()
        {
            Processor proc = new(Enums.TaskType.T0);

            WorkTask? task = proc.GetTask();

            Assert.IsNull(task);
        }
    }
}
