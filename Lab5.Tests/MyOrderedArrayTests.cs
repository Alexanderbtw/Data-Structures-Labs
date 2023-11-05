using System.Reflection;

namespace Lab5.Tests
{
    [TestClass]
    public class MyOrderedArrayTests
    {
        [TestMethod]
        public void Append_ThirdElement_RealSizeEqual4()
        {
            // Arrange
            var array = new MyOrderedArray<int>(new int[] { 1, 2 });
            var real_size = typeof(MyOrderedArray<int>).GetField("real_size", BindingFlags.NonPublic | BindingFlags.Instance);

            // Art
            var prev_size = real_size!.GetValue(array);
            array.Append(3);
            var curr_size = real_size.GetValue(array);

            // Assert
            Assert.AreEqual(2, prev_size);
            Assert.AreEqual(4, curr_size);
        }

        [TestMethod]
        public void Insert_First_WorkCorrect()
        {
            // Arrange
            var expected = new int[] {3, 1, 2};
            var array = new MyOrderedArray<int>(new int[] { 1, 2 });

            // Art
            array.Insert(3, 0);
            var actual = array.ToArray();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FirstRemove_ExistingAndNotExisting_ReturnedTrueAndFalse()
        {
            // Arrange
            var array = new MyOrderedArray<int>(new int[] { 1, 2, 3 });

            // Art
            var res1 = array.FirstRemove(1);
            var res2 = array.FirstRemove(1);

            // Assert
            Assert.IsTrue(res1);
            Assert.IsFalse(res2);
        }

        [TestMethod]
        public void FirstRemove_NotExisting0_ReturnedFalse()
        {
            // Arrange
            var array = new MyOrderedArray<int>(new int[] { 1, 2, 3 });

            // Art
            var actual = array.FirstRemove(0);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void RemoveAll_FirstAndLast_Returned2()
        {
            // Arrange
            var array = new MyOrderedArray<int>(new int[] { 1, 2, 1 });

            // Art
            var actual = array.RemoveAll(1);

            // Assert
            Assert.AreEqual(2, actual);
        }

        [DataRow(new int[] { 5, 3, 4, 1, 2 })]
        [DataTestMethod]
        public void Sort_Array_SortedCorrect(int[] test_values)
        {
            // Arrange
            var array = new MyOrderedArray<int>(test_values);

            // Art
            Array.Sort(test_values);
            array.Sort();
            int[] actual_values = array.ToArray();

            // Assert
            CollectionAssert.AreEqual(test_values, actual_values);
        }

        [DataRow(1, 0)]
        [DataRow(2, 3)]
        [DataRow(3, -1)]
        [DataTestMethod]
        public void Find_1And2And3_ReturnedIndex(int test_value, int expected)
        {
            // Arrange
            var array = new MyOrderedArray<int>(new int[] {1, 4, 5, 2});

            // Art
            var actual = array.Find(test_value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindDublicates_112_Returnder1()
        {
            // Arrange
            var array = new MyOrderedArray<int>(new int[] { 1, 1, 2 });
            var expected_dublicates = new Dictionary<int, int>() { { 1, 1 } };

            // Art
            var actual_count = array.FindDublicates(out var actual_dublicates);

            // Assert
            Assert.AreEqual(1, actual_count);
            CollectionAssert.AreEqual(expected_dublicates, actual_dublicates);
        }

        [DataRow(1, true)]
        [DataRow(2, false)]
        [DataTestMethod]
        public void Contains_1And2_ReturnedTrueAndFalse(int test_value, bool expected)
        {
            // Arrange
            var array = new MyOrderedArray<int>(new int[] {-1, 4, 1});

            // Art
            var actual = array.Contains(test_value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Indexator_ExistIndex_ReturnedValue()
        {
            // Arrange
            var array = new MyOrderedArray<int>(new int[] { 1, 2, 3 });

            // Art
            var actual = array[2];

            // Assert
            Assert.AreEqual(actual, 3);
        }

        [TestMethod]
        public void Indexator_NotExistIndex_ThrowOutOfRangeException()
        {
            // Arrange
            var array = new MyOrderedArray<int>(new int[] { 1, 2, 3 });

            // Art
            void actual() => _ = array[3];

            // Assert
            Assert.ThrowsException<IndexOutOfRangeException>(actual);
        }

        [TestMethod]
        public void Clone_ArrayAndClonedNotReferenced()
        {
            // Arrange
            var array = new MyOrderedArray<int>(new int[] { 1, 2, 3 });

            // Art
            MyOrderedArray<int>? cloned = array.Clone() as MyOrderedArray<int>;
            cloned![0] = 10;

            // Assert
            Assert.AreNotSame(array, cloned);
        }
    }
}