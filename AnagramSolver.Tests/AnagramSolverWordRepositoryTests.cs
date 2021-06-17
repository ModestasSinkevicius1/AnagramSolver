using NUnit.Framework;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using System.Collections.Generic;

namespace AnagramSolver.Tests
{
    class AnagramSolverWordRepositoryTests
    {
        private AnagramSolverWordRepository _wordRepository;

        [SetUp]
        public void Setup()
        {
            _wordRepository = new AnagramSolverWordRepository();
        }

        [Test]
        public void GetWords_DoesMethodSucceedsReadingCorrectFile_ExpectedTrue()
        {
            List<Anagram> expected = new List<Anagram>();

            IList<Anagram> actual = _wordRepository.GetWords();

            Assert.AreEqual(expected, actual);
        }       
    }
}
