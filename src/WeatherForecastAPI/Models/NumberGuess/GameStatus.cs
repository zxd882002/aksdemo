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
                GameAnswer = GenerateNumber(),
                GameStatus = "Started",
                GameHistories = new List<GameHistory>()
            };
            _gameStatus[guid] = gameStatusInformation;
            return gameStatusInformation;
        }

        public GameStatusInformation? CheckResult(string gameIdentifierString, int[] inputs)
        {
            try
            {
                Guid gameIdentifier = Guid.Parse(gameIdentifierString);
                if (_gameStatus.ContainsKey(gameIdentifier))
                {
                    var information = _gameStatus[gameIdentifier];
                    string checkResult = Check(information.GameAnswer, inputs);
                    information.GameHistories.Add(new GameHistory
                    {
                        Input = string.Join("", inputs),
                        Result = checkResult
                    });
                    information.GameRetry = information.GameRetry - 1;

                    if (checkResult == "4A0B")
                    {
                        // answer is correct
                        information.GameStatus = "Pass";
                        _gameStatus.Remove(gameIdentifier);

                    }
                    else if (information.GameRetry == 0)
                    {
                        // anwer is not correct and no retries
                        information.GameStatus = "Fail";
                        _gameStatus.Remove(gameIdentifier);
                    }

                    return information;
                }
                return null;
            }
            catch
            {
                return null;
            }
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
        private string Check(int[] expected, int[] actual)
        {
            int aCount = 0;
            int bCount = 0;
            for (int i = 0; i < expected.Length; i++)
            {
                int expectedNumber = expected[i];
                for (int j = 0; j < actual.Length; j++)
                {
                    int actualNumber = actual[j];
                    if (expectedNumber == actualNumber && i == j)
                    {
                        aCount++;
                    }
                    else if (expectedNumber == actualNumber)
                    {
                        bCount++;
                    }
                }
            }
            return $"{aCount}A{bCount}B";
        }
    }
}