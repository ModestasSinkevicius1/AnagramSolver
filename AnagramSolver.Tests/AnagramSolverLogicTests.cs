using NUnit.Framework;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using Moq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace AnagramSolver.Tests
{
    [TestFixture]
    public class AnagramSolverLogicTests
    {
        private Mock<IWordRepository> mockWordRepository;
        private Mock<IOptions<AnagramConfig>> mockAnagramConfig;

        private AnagramSolverLogic anagramSolverLogic;

        [SetUp]
        public void Setup()
        {    
            mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);           
            mockAnagramConfig = new Mock<IOptions<AnagramConfig>>(MockBehavior.Strict);
        }        

        [Test]        
        [TestCase("labas", 2)]       
        [TestCase("veidas", 1)]
        [TestCase("mesti", 1)]
        [TestCase("gražu", 0)]
        [TestCase("trikampis", 0)]
        [TestCase("dažai", 0)]
        [TestCase("valtis", 0)]
        [TestCase("ledas", 0)]
        [TestCase("miegas", 1)]           
        public void GetAnagrams_CheckIfGivenWordOutputsExactQuantityAnagrams_ExpectedTrue(string value, int expected)
        {
            List<WordModel> anagrams = new()
            {
                new WordModel(0, "balas", 0),
                new WordModel(1, "dievas", 0),
                new WordModel(2, "semti", 0),
                new WordModel(3, "sabal", 0),
                new WordModel(4, "geimas", 0),
                new WordModel(5, "labas", 0)
            };

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);
            mockWordRepository.Setup(p => p.CheckCachedWord(value)).Returns(false);
            mockAnagramConfig.Setup(p => p.Value).Returns(
                new AnagramConfig() { MinWordLength = 12, TotalOutputAnagrams = 3 });

            anagramSolverLogic = new AnagramSolverLogic(mockWordRepository.Object, mockAnagramConfig.Object);
                        
            IList<WordModel> anagram = anagramSolverLogic.GetAnagrams(value);

            int actual = anagram.Count;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("labas", 3)]
        [TestCase("veidas", 3)]
        [TestCase("mesti", 3)]
        [TestCase("gražu", 3)]
        [TestCase("trikampis", 3)]
        [TestCase("dažai", 3)]
        [TestCase("valtis", 3)]
        [TestCase("ledas", 3)]
        [TestCase("miegas", 3)]               
        public void GetAnagrams_CheckIfGivenWordLengthThrowsException_ExpectedException(string value, int length)
        {
            List<WordModel> anagrams = new()
            {
                new WordModel(0, "balas", 0),
                new WordModel(1, "dievas", 0),
                new WordModel(2, "semti", 0),
                new WordModel(3, "sabal", 0),
                new WordModel(4, "geimas", 0)
            };

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);
            mockWordRepository.Setup(p => p.CheckCachedWord(value)).Returns(false);
            mockAnagramConfig.Setup(p => p.Value).Returns(
                new AnagramConfig() { MinWordLength = length, TotalOutputAnagrams = 3 });

            anagramSolverLogic = new AnagramSolverLogic(mockWordRepository.Object, mockAnagramConfig.Object);       

            WordTooLongException ex = Assert.Throws<WordTooLongException>(() => anagramSolverLogic.GetAnagrams(value));

            string expected = ex.Message;
            string actual = "Error: input word too long";

            Assert.That(expected, Is.EqualTo(actual));            
        }

        [Test]
        [TestCase("labas", 2)]
        [TestCase("veidas", 6)]
        [TestCase("mesti", 1)]
        [TestCase("gražu", 3)]
        [TestCase("trikampis", 4)]
        [TestCase("dažai", 3)]
        [TestCase("valtis", 3)]
        [TestCase("ledas", 5)]
        [TestCase("miegas", 3)]       
        public void GetAnagrams_CheckIfAnagramResultIsGivenLessOrEqualToParams_ExpectedTrue(string value, int total)
        {
            List<WordModel> anagrams = new()
            {
                new WordModel(0, "balas", 0),
                new WordModel(1, "dievas", 0),
                new WordModel(2, "semti", 0),
                new WordModel(3, "sabal", 0),
                new WordModel(4, "geimas", 0),
                new WordModel(5, "salab", 0),
                new WordModel(6, "itsem", 0)
            };

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);
            mockWordRepository.Setup(p => p.CheckCachedWord(value)).Returns(false);
            mockAnagramConfig.Setup(p => p.Value).Returns(
                new AnagramConfig() { MinWordLength = 12, TotalOutputAnagrams = total });

            anagramSolverLogic = new AnagramSolverLogic(mockWordRepository.Object, mockAnagramConfig.Object);

            IList<WordModel> anagram = anagramSolverLogic.GetAnagrams(value);

            int expected = mockAnagramConfig.Object.Value.TotalOutputAnagrams;
            int actual = anagram.Count;

            Assert.That(actual, Is.LessThanOrEqualTo(expected));
        }

        [Test]
        [TestCase("")]       
        public void GetAnagrams_CheckIfEmptyWordThrowsException_ExpectedException(string value)
        {
            List<WordModel> anagrams = new();            

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);
            mockWordRepository.Setup(p => p.CheckCachedWord(value)).Returns(false);
            mockAnagramConfig.Setup(p => p.Value).Returns(
                new AnagramConfig() { MinWordLength = 6, TotalOutputAnagrams = 3 });

            anagramSolverLogic = new AnagramSolverLogic(mockWordRepository.Object, mockAnagramConfig.Object);

            WordIsEmptyException ex = Assert.Throws<WordIsEmptyException>(() => anagramSolverLogic.GetAnagrams(value));

            string expected = ex.Message;
            string actual = "Error: word was empty";

            Assert.That(expected, Is.EqualTo(actual));
        }
    }
}