using System;

namespace RandomNumber
{
    /** 
     * Class to generate a random number between -1 and 3
     */
    public class RandomNumberGenerator
    {
        public const string strProbabilityDoesNotEqualOne = "Probability values do not add up to 1";
        public const string strAmountOfProbabilitiesAndNumbersDontMatch = "The Amount of Numbers and Amount of Probabilities do not match! Please make sure they both contain equal amount of values";
        private readonly int[] numberRange;
        private readonly double[] probability;

        public static void Main(string[] args)
        {

        }

        /// <summary> 
        ///Function to check if number range and probability variables are acceptable
        /// </summary>
        /// <param name="numberRange" type=array>Array of integer values to set the range the generator can pick from</param>
        /// <param name="probability" type=array>Array of probabilities to link to the numberRange provided</param>
        /// <returns>number if criteria is acceptable</returns>
        public RandomNumberGenerator(int[] numberRange, double[] probability)
        {
            int probabilityCalculator = 0;

            //Multiplying each probability by 100 to ensure it all adds up to a whole number
            foreach(double i in probability)
            {
              double num = i * 100;
                //Converting from double to int because multipliying it out of its decimal value
                probabilityCalculator += Convert.ToInt32(num);
            }
            
            //Checking if the probabilities given, add up to 100
            if (probabilityCalculator != 100)
            {
                throw new ArgumentOutOfRangeException("probability", probabilityCalculator, strProbabilityDoesNotEqualOne);
            }

            //Checking if there's an equal amount of numbers as there are probabilities, to ensure they can be matched
            if (numberRange.Length != probability.Length)
            {
                throw new ArgumentException(strAmountOfProbabilitiesAndNumbersDontMatch);
            }
            this.numberRange = numberRange;
            this.probability = probability;
        }

        /// <summary>
        /// Generates Number within range and returns value
        /// </summary>
        /// <param name="numberRange" type=array>Array of integer values to set the range the generator can pick from</param>
        /// <param name="probability" type=array>Array of probabilities to link to the numberRange provided</param>
        /// <returns>returns integer specified within the range provided</returns>
        public int NextNumber()
        {
            Random random = new Random();
            int randomNumber= random.Next(100)+1;
            int index = 0;

            //looping through probabilities
            foreach (double i in probability)
            {
                //Multiplying Probability by 100 to turn into whole number
                double num = i * 100;

                //Deducting from random number until value reaches 0 or below
                randomNumber -= Convert.ToInt32(num);

                if (randomNumber <=0)
                {
                    return numberRange[index];
                }

                index ++;
            }
            throw new ArgumentException("ERROR: Unable to generate random number!");
        }
    }
}
