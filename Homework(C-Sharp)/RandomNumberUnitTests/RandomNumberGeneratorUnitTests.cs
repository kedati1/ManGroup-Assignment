using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomNumber;

namespace RandomNumberUnitTests
{
    [TestClass]
    public class RandomNumberGeneratorUnitTests
    {
        //Valid Sample Numbers For Unit Tests
        public int[] sampleNumbers = { -1, 0, 1, 2, 3};
        //Sample only containing 3 numbers, intended to be used with a 5 number probability array
        public int[] sampleNotEnoughNumbers = {-1, 0, 1};
        //Probability array adding up to 1
        public double[] probabilityNumbersCorrect = {0.01, 0.3, 0.58, 0.1, 0.01};
        //Probability One Hundred Percent for One Number
        public double[] probabilityOneHundredPercent= {0.0, 0.0, 0.0, 0.0, 1};
        //Probability array exceeds 1
        public double[] probabilityNumbersExceedOne = { 0.01, 0.2, 0.18, 0.9, 0.11 };
        //Probability array does not add up to 1
        public double[] probabilityNumbersLowerThanOne = { 0.01, 0.2, 0.18, 0.02, 0.11 };

        /// <summary>
        /// Number Returned from Generator fit within range of sample numbers
        /// </summary>
        [TestMethod]
        public void NumberAddedMatchSample()
        {
            //Initiate Variables To Be Used
            RandomNumberGenerator numbers = new RandomNumberGenerator(sampleNumbers, probabilityNumbersCorrect);
            bool testResults = false;

            //Action
            int returnedNumber = numbers.NextNumber();

            //Assert
            foreach (int i in sampleNumbers)
            {
                if (i == returnedNumber)
                {
                    testResults = true;
                }
            }
            Assert.IsTrue(testResults);
        }
        
        /// <summary>
        /// Test to error when probability exceeds 1
        /// </summary>
        [TestMethod]
        public void ProbabilityErrorsWhenItExceedsOne()
        {
            //Action and Assert
            try
            {
                RandomNumberGenerator numbers = new RandomNumberGenerator(sampleNumbers, probabilityNumbersExceedOne);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, RandomNumberGenerator.strProbabilityDoesNotEqualOne);
            }
        }

        /// <summary>
        /// Test to error when probability does not equal 1
        /// </summary>
        [TestMethod]
        public void ProbabilityErrorsWhenDoesNotAddUpToOne()
        {
            //Action and Assert
            try
            {
                RandomNumberGenerator numbers = new RandomNumberGenerator(sampleNumbers, probabilityNumbersLowerThanOne);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, RandomNumberGenerator.strProbabilityDoesNotEqualOne);
            }
        }

        /// <summary>
        /// Test to Catch error in the event that there's a different amount of probabilities as there are numbers 
        /// </summary>
        [TestMethod]
        public void AmountOfNumbersAndProbabilitiesDoNotAddUp()
        {
            //Action and Assert
            try
            {
                RandomNumberGenerator numbers = new RandomNumberGenerator(sampleNotEnoughNumbers, probabilityNumbersCorrect);
            }
            catch (System.ArgumentException e)
            {
                StringAssert.Contains(e.Message, RandomNumberGenerator.strAmountOfProbabilitiesAndNumbersDontMatch);
            }
        }

        /// <summary>
        /// Test to gather 100 numbers and ensure they're all valid 
        /// </summary>
        [TestMethod]
        public void GatherLargeQuantityOfNumbers()
        {
            //Arrange
            bool checker = false;
            RandomNumberGenerator numbers = new RandomNumberGenerator(sampleNumbers, probabilityNumbersCorrect);

            //Action
            int returnedNumber = numbers.NextNumber();
            for (int iteration = 0; iteration < 100; iteration++)
            {
                foreach (int number in sampleNumbers)
                {
                    //Check if number exists in sample
                    if (number == returnedNumber)
                    {
                        Assert.IsTrue(number == returnedNumber);
                    } else
                    {
                        Assert.IsFalse(number == returnedNumber);
                    }
                }
            }
        }

        /// <summary>
        /// Test to ensure all numbers returned the same at a 1 probability
        /// </summary>
        [TestMethod]
        public void SameNumberReturnedProbabilityOneHundredPercent()
        {
            //Arrange
            RandomNumberGenerator numbers = new RandomNumberGenerator(sampleNumbers, probabilityOneHundredPercent);
            int comparisonNumber = numbers.NextNumber();

            //Action
            for (int iteration = 0; iteration < 100; iteration++)
            {
                int returnedNumber = numbers.NextNumber();
                Assert.AreEqual(comparisonNumber, returnedNumber);
            }
        }
    }
}
