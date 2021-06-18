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
        [TestCase("labas", 2)]
        [TestCase("veidas", 1)]
        [TestCase("mesti", 1)]
        [TestCase("gražu", 0)]
        [TestCase("trikampis", 0)]
        [TestCase("dažai", 0)]
        [TestCase("valtis", 0)]
        [TestCase("ledas", 0)]
        [TestCase("miegas", 1)]
        [TestCase("", 0)]     
        public void GetAnagrams_CheckIfGivenWordOutputsExactQuantityAnagrams_ExpectedTrue(string value, int expected)
        {
            List<Anagram> anagrams = new List<Anagram>();

            anagrams.Add(new Anagram("balas", null, null, 0));
            anagrams.Add(new Anagram("dievas", null, null, 0));
            anagrams.Add(new Anagram("semti", null, null, 0));
            anagrams.Add(new Anagram("sabal", null, null, 0));
            anagrams.Add(new Anagram("geimas", null, null, 0));            

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);
            mockAnagramConfig.Setup(p => p.Value).Returns(
                new AnagramConfig() { MinWordLength = 12, TotalOutputAnagrams = 3 });

            anagramSolverLogic = new AnagramSolverLogic(mockWordRepository.Object, mockAnagramConfig.Object);
                        
            var anagram = anagramSolverLogic.GetAnagrams(value);

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
        [TestCase("", 0)]        
        public void GetAnagrams_CheckIfGivenWordLengthThrowsException_ExpectedFail(string value, int length)
        {
            List<Anagram> anagrams = new List<Anagram>();

            anagrams.Add(new Anagram("balas", null, null, 0));
            anagrams.Add(new Anagram("dievas", null, null, 0));
            anagrams.Add(new Anagram("semti", null, null, 0));
            anagrams.Add(new Anagram("sabal", null, null, 0));
            anagrams.Add(new Anagram("geimas", null, null, 0));

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);
            mockAnagramConfig.Setup(p => p.Value).Returns(
                new AnagramConfig() { MinWordLength = length, TotalOutputAnagrams = 3 });

            anagramSolverLogic = new AnagramSolverLogic(mockWordRepository.Object, mockAnagramConfig.Object);            

            var ex = Assert.Throws<StringTooLongException>(() => anagramSolverLogic.GetAnagrams(value));
            Assert.That(ex.Message, Is.EqualTo("input word too long"));
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
        [TestCase("", 0)]
        public void GetAnagrams_CheckIfAnagramResultIsGivenLessOrEqualToParams_ExpectedTrue(string value, int total)
        {
            List<Anagram> anagrams = new List<Anagram>();

            anagrams.Add(new Anagram("balas", null, null, 0));
            anagrams.Add(new Anagram("dievas", null, null, 0));
            anagrams.Add(new Anagram("semti", null, null, 0));
            anagrams.Add(new Anagram("sabal", null, null, 0));
            anagrams.Add(new Anagram("geimas", null, null, 0));
            anagrams.Add(new Anagram("salab", null, null, 0));
            anagrams.Add(new Anagram("itsem", null, null, 0));

            mockWordRepository.Setup(p => p.GetWords()).Returns(anagrams);
            mockAnagramConfig.Setup(p => p.Value).Returns(
                new AnagramConfig() { MinWordLength = 12, TotalOutputAnagrams = total });

            anagramSolverLogic = new AnagramSolverLogic(mockWordRepository.Object, mockAnagramConfig.Object);

            var anagram = anagramSolverLogic.GetAnagrams(value);

            int expected = mockAnagramConfig.Object.Value.TotalOutputAnagrams;

            int actual = anagram.Count;

            Assert.That(actual, Is.LessThanOrEqualTo(expected));
        }
    }
}