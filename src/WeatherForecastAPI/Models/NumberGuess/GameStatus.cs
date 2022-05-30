using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecastAPI.Infrastructure.Entensions;
using WeatherForecastAPI.Infrastructure.Redis;

namespace WeatherForecastAPI.Models.NumberGuess
{
    public class GameStatus
    {
        private readonly IRedisHelper _redisHelper;

        public GameStatus(IRedisHelper redisHelper)
        {
            _redisHelper = redisHelper;
        }

        public async Task<GameStatusInformation> CreateGameStatusInformation()
        {
            Guid guid = Guid.NewGuid();
            GameStatusInformation gameStatusInformation = new GameStatusInformation
            {
                GameIdentifier = guid.ToString(),
                GameRetry = 10,
                GameAnswer = 4.GenerateNoDupeRandomNumber(),
                GameStatus = "Started",
                GameHistories = new List<GameHistory>()
            };
            await _redisHelper.SaveToRedis(guid.ToString(), gameStatusInformation, TimeSpan.FromDays(1));
            return gameStatusInformation;
        }

        public async Task<GameStatusInformation> CheckResult(string gameIdentifierString, int[] inputs)
        {

            var information = await _redisHelper.GetFromRedis<GameStatusInformation>(gameIdentifierString);
            if (information == null)
            {
                throw new Exception("Game not found");
            }

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
                await _redisHelper.RemoveFromRedis(gameIdentifierString);

            }
            else if (information.GameRetry == 0)
            {
                // anwer is not correct and no retries
                information.GameStatus = "Fail";
                await _redisHelper.RemoveFromRedis(gameIdentifierString);
            }
            else
            {
                await _redisHelper.SaveToRedis(gameIdentifierString, information, TimeSpan.FromDays(1));
            }

            return information;
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