using NUnit.Framework;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;

namespace AnagramSolver.Tests
{
    public class AnagramSolverLogicTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void isLetterNotMoreThanGiven_CheckIfGivenAnagramPassesWordRequirement_ExpectedTrue()
        {
            AnagramSolverLogic anagramSolverLogic = new AnagramSolverLogic(null, null);

            bool expected = true;

            bool actual = anagramSolverLogic.IIsLetterNotMoreThanGiven("labas", "balas");

            Assert.AreEqual(expected, actual);           
        }
    }
}