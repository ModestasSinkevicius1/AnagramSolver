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
            List<Anagram> anagrams = new()
            {
                new Anagram("balas", null, null, 0),
                new Anagram("dievas", null, null, 0),
                new Anagram("semti", null, null, 0),
                new Anagram("sabal", null, null, 0),
                new Anagram("geimas", null, null, 0),
                new Anagram("salab", null, null, 0),
                new Anagram("itsem", null, null, 0),
                new Anagram("svetaine", null, null, 0),
                new Anagram("krabas", null, null, 0),
                new Anagram("stalas", null, null, 0)
            };

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);

            wordService = new WordService(mockWordRepository.Object);

            int actual = wordService.GetWords(pageNumber, pageSize).Count;
            int expected = pageSize;

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
            List<Anagram> anagrams = new()
            {
                new Anagram("balas", null, null, 0),
                new Anagram("dievas", null, null, 0),
                new Anagram("semti", null, null, 0),
                new Anagram("sabal", null, null, 0),
                new Anagram("geimas", null, null, 0),
                new Anagram("salab", null, null, 0),
                new Anagram("itsem", null, null, 0),
                new Anagram("svetaine", null, null, 0),
                new Anagram("krabas", null, null, 0),
                new Anagram("stalas", null, null, 0)
            };

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);

            wordService = new WordService(mockWordRepository.Object);

            int expected = wordService.GetWords(pageNumber, pageSize).Count;
            int actual = 10;

            Assert.That(expected, Is.EqualTo(actual));
        }
    }
}
