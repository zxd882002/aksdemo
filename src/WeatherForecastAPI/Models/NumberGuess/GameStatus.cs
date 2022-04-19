using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherForecastAPI.Models.NumberGuess
{
    public class GameStatus
    {
        private readonly Dictionary<Guid, GameStatusInformation> _gameStatus;

        public GameStatus()
        {
            _gameStatus = new Dictionary<Guid, GameStatusInformation>();
        }

        public GameStatusInformation CreateGameStatusInformation()
        {
            Guid guid = Guid.NewGuid();
            GameStatusInformation gameStatusInformation = new GameStatusInformation
            {
                GameIdentifier = guid,
                GameRetry = 10,
                GameNumber = GenerateNumber(),
                GameStatus = "Started",
                GameHistories = new GameHistory[10]
            };
            return gameStatusInformation;
        }

        private int[] GenerateNumber()
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

            return numbers.Take(4).ToArray();
        }
    }
}