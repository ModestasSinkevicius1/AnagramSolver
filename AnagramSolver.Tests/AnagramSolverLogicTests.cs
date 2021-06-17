using NUnit.Framework;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using Moq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AnagramSolver.Tests
{
    [TestFixture]
    public class AnagramSolverLogicTests
    {
        Mock<IWordRepository> mockWordRepository;        
        Mock<IOptions<AnagramConfig>> mockAnagramConfig;               

        AnagramSolverLogic anagramSolverLogic;

        [SetUp]
        public void Setup()
        {
            mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
            mockAnagramConfig = new Mock<IOptions<AnagramConfig>>(MockBehavior.Strict);
        }

        [Test]
        [Retry(2)]
        [TestCase("labas")]        
        [TestCase("gra�u")]
        [TestCase("trikampis")]
        [TestCase("veidas")]
        [TestCase("mesti")]
        public void GetAnagrams_CheckIfGivenWordReturnsList_ExpectedTrue(string value)
        {
            List<Anagram> expected = new List<Anagram>();            

            mockWordRepository.Setup(p => p.GetWords()).Returns(expected);
            mockAnagramConfig.Setup(p => p.Value).Returns(new AnagramConfig());

            anagramSolverLogic = new AnagramSolverLogic(mockWordRepository.Object, mockAnagramConfig.Object);

            var actual = anagramSolverLogic.GetAnagrams(value);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [Retry(2)]
        [TestCase("labas", 2)]
        [TestCase("veidas", 1)]
        [TestCase("mesti", 1)]
        [TestCase("gra�u", 0)]
        [TestCase("trikampis", 0)]
        public void GetAnagrams_CheckIfGivenWordOutputsExactQuantityAnagrams_ExpectedTrue(string value, int expected)
        {
            List<Anagram> anagrams = new List<Anagram>();
            anagrams.Add(new Anagram("balas", null, null, 0));
            anagrams.Add(new Anagram("dievas", null, null, 0));
            anagrams.Add(new Anagram("semti", null, null, 0));
            anagrams.Add(new Anagram("sabal", null, null, 0));

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);
            mockAnagramConfig.Setup(p => p.Value).Returns(new AnagramConfig());

            anagramSolverLogic = new AnagramSolverLogic(mockWordRepository.Object, mockAnagramConfig.Object);
                        
            var anagram = anagramSolverLogic.GetAnagrams(value);

            int actual = anagram.Count;

            Assert.That(actual, Is.EqualTo(expected));
        }       
    }
}