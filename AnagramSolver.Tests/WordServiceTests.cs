using NUnit.Framework;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using Moq;
using System.Collections.Generic;

namespace AnagramSolver.Tests
{
    [TestFixture]
    class WordServiceTests
    {
        Mock<IWordRepository> mockWordRepository;

        WordService wordService;

        [SetUp]
        public void Setup()
        {
            mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
        }

        [Test]
        [TestCase(0, 10)]
        [TestCase(1, 3)]
        [TestCase(1, 5)]
        [TestCase(0, 5)]
        public void GetWords_CheckIfGivenPageNumberOrSizeGivesResult_ExpectedTrue(int pageNumber, int pageSize)
        {
            List<WordModel> anagrams = new()
            {
                new WordModel(0, "balas", 0),
                new WordModel(1, "dievas", 0),
                new WordModel(2, "semti", 0),
                new WordModel(3, "sabal", 0),
                new WordModel(4, "geimas", 0),
                new WordModel(5, "salab", 0),
                new WordModel(6, "itsem", 0),
                new WordModel(7, "svetaine", 0),
                new WordModel(8, "krabas", 0),
                new WordModel(9, "stalas", 0)
            };

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);

            wordService = new WordService(mockWordRepository.Object);

            int actual = wordService.GetWords(pageNumber, pageSize).Count;
            int expected = pageSize;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(2, 3)]        
        public void GetWords_CheckIfGivenPageNumberOrSizeGivesExpectedWords_ExpectedTrue(int pageNumber, int pageSize)
        {
            List<WordModel> anagrams = new()
            {
                new WordModel(0, "balas", 0),
                new WordModel(1, "dievas", 0),
                new WordModel(2, "semti", 0),
                new WordModel(3, "sabal", 0),
                new WordModel(4, "geimas", 0),
                new WordModel(5, "salab", 0),
                new WordModel(6, "itsem", 0),
                new WordModel(7, "svetaine", 0),
                new WordModel(8, "krabas", 0),
                new WordModel(9, "stalas", 0)
            };

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);

            wordService = new WordService(mockWordRepository.Object);

            List<string> actual = wordService.GetWords(pageNumber, pageSize);
            List<string> expected = new()
            {
                "itsem",
                "svetaine",
                "krabas"
            };

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(-1, 10)]
        [TestCase(0, -10)]
        [TestCase(0, 0)]
        [TestCase(0, 100)]
        [TestCase(100, 0)]
        public void GetWords_CheckIfGivenBadNumberForPageNumberOrSizeResetsValuesToDefault_ExpectedTrue(int pageNumber, int pageSize)
        {
            List<WordModel> anagrams = new()
            {
                new WordModel(0, "balas", 0),
                new WordModel(1, "dievas", 0),
                new WordModel(2, "semti", 0),
                new WordModel(3, "sabal", 0),
                new WordModel(4, "geimas", 0),
                new WordModel(5, "salab", 0),
                new WordModel(6, "itsem", 0),
                new WordModel(7, "svetaine", 0),
                new WordModel(8, "krabas", 0),
                new WordModel(9, "stalas", 0)
            };

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);

            wordService = new WordService(mockWordRepository.Object);

            int actual = wordService.GetWords(pageNumber, pageSize).Count;
            int expected = 10;

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
