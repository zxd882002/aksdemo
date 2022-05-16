using System;
using System.Linq;

namespace WeatherForecastAPI.Infrastructure.Entensions
{
    public static class RandomExtensions
    {
        public static int[] GenerateNoDupeRandomNumber(this int numberCount)
        {
            int[] numbers = new int[10];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = i;
            }

            Random r = new Random();
            int loop = 100000;
            for (int i = 0; i < loop; i++)
            {
                int number1 = r.Next(0, 10);
                int number2 = r.Next(0, 10);
                int temp = numbers[number1];
                numbers[number1] = numbers[number2];
                numbers[number2] = temp;
            }

            return numbers.Take(numberCount).ToArray();
        }
    }
}
