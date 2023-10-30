using System.Reflection;

namespace Lab4.Tests
{
    [TestClass]
    public class MyHashTableTests
    {
        [TestMethod]
        public void GetHash_TwoKeys_OneHashReturned()
        {
            // Arrange
            MyHashTable<int> hashTable = new MyHashTable<int>();
            MethodInfo? get_hash = typeof(MyHashTable<int>).GetMethod("GetHash", BindingFlags.NonPublic | BindingFlags.Instance);

            // Art
            var a = get_hash?.Invoke(hashTable, new object[] { "AA11AA" });
            var b = get_hash?.Invoke(hashTable, new object[] { "AA11AA" });

            // Assert
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void Add_NotValidKey_ThrowArgumentException()
        {
            // Arrange
            MyHashTable<int> hashTable = new MyHashTable<int>();
            Item<int> item = new Item<int>("FAILED", 1);

            // Assert
            Assert.ThrowsException<ArgumentException>(() => hashTable.Add("123", 1));
            Assert.ThrowsException<ArgumentException>(() => hashTable.Add(item));
        }

        [TestMethod]
        public void Add_TwoSameKeys_ThrowArgumentException()
        {
            // Arrange
            MyHashTable<int> hashTable = new MyHashTable<int>();
            Item<int> item = new Item<int>("AA11AA", 1);

            // Art
            hashTable.Add(item);

            // Assert
            Assert.ThrowsException<ArgumentException>(() => hashTable.Add("AA11AA", 2));
            Assert.ThrowsException<ArgumentException>(() => hashTable.Add(item));
        }

        [TestMethod]
        public void Find_ReturnsLinkToThisElement()
        {
            // Arrange
            MyHashTable<int> hashTable = new MyHashTable<int>();
            Item<int> item = new Item<int>("AA11AA", 1);

            // Art
            hashTable.Add(item);
            hashTable.Find("AA11AA", out var actual_item);
            item.Value = 2;

            // Arrange
            Assert.AreSame(item, actual_item);
            Assert.AreEqual(item.Value, actual_item?.Value);
        }

        [TestMethod]
        public void Remove_ElementsTrueDeleted()
        {
            // Arrange
            MyHashTable<int> hashTable = new MyHashTable<int>();
            Item<int> item = new Item<int>("AA11AA", 1);

            // Art
            hashTable.Add(item);
            hashTable.Remove(item);
            var excepted_count = hashTable.AllCount;
            var actual_count = 0;
            var dist = hashTable.GetBucketEnumerator();
            while (dist.MoveNext())
            {
                actual_count += dist.Current.Count;
            }

            // Arrange
            Assert.AreEqual(excepted_count, actual_count);
        }
    }
}