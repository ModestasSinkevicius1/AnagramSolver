using NUnit.Framework;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using Moq;
using System.Collections.Generic;

namespace AnagramSolver.Tests
{
    [TestFixture]
    public class AnagramSolverLogicTests
    {
        Mock<IWordRepository> mockWordRepository;
        //Mock<AnagramConfig> mockAnagramConfig;

        AnagramSolverLogic anagramSolverLogic;

        [SetUp]
        public void Setup()
        {
            mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
            //mockAnagramConfig = new Mock<AnagramConfig>(MockBehavior.Strict);
        }

        [Test]
        [Retry(2)]
        [TestCase("labas")]        
        [TestCase("gražu")]
        [TestCase("trikampis")]
        [TestCase("veidas")]
        [TestCase("mesti")]
        public void GetAnagrams_CheckIfGivenWordReturnsList_ExpectedTrue(string value)
        {
            List<Anagram> expected = new List<Anagram>();            

            mockWordRepository.Setup(p => p.GetWords()).Returns(expected);          

            anagramSolverLogic = new AnagramSolverLogic(mockWordRepository.Object);

            var actual = anagramSolverLogic.GetAnagrams(value);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [Retry(2)]
        [TestCase(1, "labas")]
        [TestCase(0, "gražu")]
        [TestCase(0, "trikampis")]
        [TestCase(1, "veidas")]
        [TestCase(1, "mesti")]
        public void GetAnagrams_CheckIfGivenWordOutputsExactQuantityAnagrams_ExpectedTrue(int expected, string value)
        {
            List<Anagram> anagrams = new List<Anagram>();

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);         

            anagramSolverLogic = new AnagramSolverLogic(mockWordRepository.Object);

            var anagram = anagramSolverLogic.GetAnagrams(value);

            int actual = anagram.Count;

            Assert.That(actual, Is.EqualTo(expected));
        }       
    }
}