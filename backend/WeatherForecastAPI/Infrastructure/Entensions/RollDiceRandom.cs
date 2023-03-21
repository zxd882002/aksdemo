using System;
using System.Security.Cryptography;

namespace WeatherForecastAPI.Infrastructure.Entensions
{
    public static class RollDiceRandom
    {
        public static int RollDice(this int numberSides)
        {
            if (numberSides <= 0)
                throw new ArgumentOutOfRangeException("numberSides");

            // Create a byte array to hold the random value.
            int randomNumber;
            do
            {
                randomNumber = RandomNumberGenerator.GetInt32(numberSides);
            }
            while (!IsFairRoll(randomNumber, numberSides));
            // Return the random number mod the number
            // of sides.  The possible values are zero-
            // based, so we add one.
            return randomNumber + 1;
        }

        private static bool IsFairRoll(int roll, int numSides)
        {
            // There are MaxValue / numSides full sets of numbers that can come up
            // in a single byte.  For instance, if we have a 6 sided die, there are
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
            int fullSetsOfValues = int.MaxValue / numSides;

            // If the roll is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return roll < numSides * fullSetsOfValues;
        }
    }
}
