using FakeItEasy;
using FunctionTest.Services.Interfaces;
using Microsoft.Extensions.Logging;


namespace FunctionTest.Services.Unittest
{
    public class CharacterManagerServiceTest
    {
        ICountedObjectManager<char> objectUnderTest;

        [SetUp]
        public void Setup()
        {
            var dd = A.Fake<ILoggerFactory>();
            objectUnderTest = new CharacterManagerService(dd);

        }

        [Test]
        public void TestAddingOne()
        {
            objectUnderTest.AddItem('a');
            char[] results = objectUnderTest.GetAllItems();
            Assert.AreEqual(results.Length, 1);
        }

        [Test]
        public void TestAddingTwo()
        {
            objectUnderTest.AddItem('a');
            objectUnderTest.AddItem('A');
            char[] results = objectUnderTest.GetAllItems();
            Assert.AreEqual(results.Length, 2);
        }
        [Test]
        public void TestAddingTwoAndOneBad()
        {
            objectUnderTest.AddItem('a');
            objectUnderTest.AddItem('A');
            objectUnderTest.AddItem(' ');
            char[] results = objectUnderTest.GetAllItems();
            Assert.AreEqual(results.Length, 2);
        }

        [Test]
        public void TestAddingTwoExpectone()
        {
            objectUnderTest.AddItem('a');
            objectUnderTest.AddItem('a');
            char[] results = objectUnderTest.GetAllItems();
            Assert.AreEqual(results.Length, 1);
            Dictionary<char, int> count = objectUnderTest.GetOrderByCount(1,true);
            Assert.AreEqual(count.Count, 1);
            Assert.IsTrue(count['a'] == 2);
        }
    }
}