using FakeItEasy;
using FunctionTest.Models;
using FunctionTest.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace FunctionTest.Services.Unittest
{
    public class StatisticsServiceTest
    {
        IStatisticsService objectUnderTest;

        [SetUp]
        public void Setup()
        {
            var dd = A.Fake<ILoggerFactory>();
            var fackeLogger = A.Fake<ILogger<StatisticsService>>();
            objectUnderTest = new StatisticsService(fackeLogger, new WordManagerService(dd), new CharacterManagerService(dd));
        }

        [Test]
        public async Task TestAddingOneAsync()
        {
            await objectUnderTest.ConsumeString("a");
            ResultsTable results = objectUnderTest.GetLargestWords(5);
            Assert.AreEqual(results.TableData.Count, 1);
        }
        [Test]
        public async Task TestAddingTwoAsync()
        {
            await objectUnderTest.ConsumeString("a A");
            ResultsTable results = objectUnderTest.GetLargestWords(5);
            ResultsTable ResultsCharacter = objectUnderTest.GetAllCharactersFrequency();
            //As A as a word and a are the same we count them as the same.
            Assert.AreEqual(results.TableData.Count, 1);
            // But as a Character thre different.
            Assert.AreEqual(ResultsCharacter.TableData.Count, 2);
        }

        [Test]
        public async Task TestAddingFourCheckLargest()
        {
            await objectUnderTest.ConsumeString("a A a bcdefg");
            ResultsTable resultsLargest = objectUnderTest.GetLargestWords(5);
            
            
            //As A as a word and a are the same we count them as the same.
            Assert.AreEqual(resultsLargest.TableData.Count, 2);
            //WE are looking for the Length
            Assert.AreEqual(resultsLargest.TableData["a"], 1);
            Assert.IsTrue(resultsLargest.TableData.First().Key.Equals("bcdefg") && resultsLargest.TableData.First().Value == 6, "bcdefg is not first");
        }

        [Test]
        public async Task TestAddingFourCheckSmallest()
        {
            await objectUnderTest.ConsumeString("a A a bcdefg");
            ResultsTable resultsLargest = objectUnderTest.GetSmallestWords(5);

            //As A as a word and a are the same we count them as the same.
            Assert.AreEqual(resultsLargest.TableData.Count, 2);
            //WE are looking for the Length
            Assert.AreEqual(resultsLargest.TableData["a"], 1);
            Assert.IsTrue(resultsLargest.TableData.First().Key.Equals("a") && resultsLargest.TableData.First().Value == 1, "a is not first");
        }



        [Test]
        public async Task TestAddingFourCheckCharacters()
        {
            await objectUnderTest.ConsumeString("a A a bcdefg");
            ResultsTable ResultsCharacter = objectUnderTest.GetAllCharactersFrequency();
            // But as a Character thre different.
            Assert.AreEqual(ResultsCharacter.TableData.Count, 8);
            Assert.AreEqual(ResultsCharacter.TableData["a"], 2);
            Assert.AreEqual(ResultsCharacter.TableData["A"], 1);
        }


        [Test]
        public async Task TestAddingFourCheckFrequency()
        {
            await objectUnderTest.ConsumeString("a A a bcdefg");
            
            ResultsTable resultsMost = objectUnderTest.MostFrequencyWord(5);

            Assert.AreEqual(resultsMost.TableData.Count, 2);
            // and here we shoudl be looking at the number of times a was repeated
            Assert.AreEqual(resultsMost.TableData["a"], 3);
            //Checking order
            Assert.IsTrue(resultsMost.TableData.First().Key.Equals("a") && resultsMost.TableData.First().Value == 3, "a is not first");
        }
       
    }
}