using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherForecastAPI.Infrastructure.KMP;
using WeatherForecastAPI.Models.WeatcherForecast;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Index = index,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("KMP")]
        public string CheckKmp(string testString, string pattern)
        {
            StringBuilder sb = new StringBuilder();
            KmpSearcher<char> searcher = new KmpSearcher<char>();
            List<char> patternArray = pattern.ToCharArray().ToList();
            int[] next = searcher.GetNext(patternArray);
            sb.AppendLine("next:");
            for(int i = 0; i < pattern.Length; i++)
            {
                sb.AppendLine($"{i}\t{patternArray[i]}\t{next[i]}");
            }

            sb.AppendLine();

            List<char> searchStringArray = testString.ToCharArray().ToList();
            List<int> matchIndexList = searcher.Search(searchStringArray, patternArray);
            sb.AppendLine("matches:");
            foreach (var matchIndex in matchIndexList)
            {
                sb.AppendLine($"index: {matchIndex}");
            }

            return sb.ToString();
        }
    }
}
