using FakeItEasy;
using FunctionTest.Services.Interfaces;
using Microsoft.Extensions.Logging;


namespace FunctionTest.Services.Unittest
{
    public class WordManagerServiceTest
    {
        ICountedObjectManager<string> objectUnderTest;

        [SetUp]
        public void Setup()
        {
            var dd = A.Fake<ILoggerFactory>();
            objectUnderTest = new WordManagerService(dd);
        }

        [Test]
        public void TestAddingOne()
        {
            objectUnderTest.AddItem("a");
            string[] results = objectUnderTest.GetAllItems();
            Assert.AreEqual(results.Length, 1);
        }

        [Test]
        public void TestAddingTwoExpectOne()
        {
            //As A as a word and a are the same we count them as the same.
            objectUnderTest.AddItem("a");
            objectUnderTest.AddItem("A");
            string[] results = objectUnderTest.GetAllItems();
            Assert.AreEqual(results.Length, 1);
        }
        [Test]
        public void TestAddingTwoExpectOneAndOneBad()
        {
            //As A as a word and a are the same we count them as the same.
            objectUnderTest.AddItem("a");
            objectUnderTest.AddItem("A");
            objectUnderTest.AddItem(" ");
            string[] results = objectUnderTest.GetAllItems();
            Assert.AreEqual(results.Length, 1);
        }

        [Test]
        public void TestAddingTwoExpectone()
        {
            objectUnderTest.AddItem("a");
            objectUnderTest.AddItem("a");
            string[] results = objectUnderTest.GetAllItems();
            Assert.AreEqual(results.Length, 1);
            Dictionary<string, int> count = objectUnderTest.GetOrderByCount(1,true);
            Assert.AreEqual(count.Count, 1);
            Assert.IsTrue(count["a"] == 2);
        }

        [Test]
        public void TestRemovalOfNoneLetterCharacters()
        {
            objectUnderTest.AddItem("a a");
            
            string[] results = objectUnderTest.GetAllItems();
            Assert.AreEqual(results.Length, 1);
            Dictionary<string, int> count = objectUnderTest.GetOrderByCount(1, true);
            Assert.IsTrue(count["aa"] == 1);
        }

        [Test]
        public void CheckLengthofWords()
        {
            objectUnderTest.AddItem("aa");

            string[] results = objectUnderTest.GetAllItems();
            Assert.AreEqual(results.Length, 1);
            Dictionary<string, int> count = objectUnderTest.GetOrderByCount(1, true);
            Assert.IsTrue(count["aa"] == 1);
            Dictionary<string, int> length = objectUnderTest.GetOrderByLength(2,true);
            Assert.IsTrue(length["aa"] == 2);
        }
    }
}