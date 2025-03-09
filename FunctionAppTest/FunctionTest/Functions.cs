using FunctionTest.Models;
using FunctionTest.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;


namespace FunctionTest
{
    public class Functions
    {
        private readonly ILogger _Logger;
        private readonly IStatisticsService _StatisticsService;
        private readonly IWordStreamService _WordStreamService;
        public Functions(ILogger<Functions> logger, IStatisticsService statisticsService, IWordStreamService wordStreamService)
        {
            _StatisticsService = statisticsService;
            _WordStreamService = wordStreamService;
            _Logger = logger;
        }

        [Function("GetAll")]
       
        public async Task<IActionResult> GetAll([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _Logger.LogDebug("Starting {@MethodName}", nameof(GetAll));
            var bufferSize = req.Query["BufferSize"];
            
            if (string.IsNullOrWhiteSpace(bufferSize)) return new BadRequestObjectResult("BufferSize cannot be null");
            if(!int.TryParse(bufferSize, out int bufferSizeInteger)) return new BadRequestObjectResult("BufferSize has to be a integer");
            var dataString = await _WordStreamService.GetStreamDataAsync(bufferSizeInteger);
            await _StatisticsService.ConsumeString(dataString);
            List<ResultsTable> data = new List<ResultsTable>();

            data.Add(_StatisticsService.TotalNumberOfCharactersAndWords());
            data.Add(_StatisticsService.GetLargestWords(5));
            data.Add(_StatisticsService.GetSmallestWords(5));
            data.Add(_StatisticsService.MostFrequencyWord(10));
            data.Add(_StatisticsService.GetAllCharactersFrequency());
            _Logger.LogDebug("Finishing {MethodName}", nameof(GetAll));
            return new OkObjectResult(data);
        }


        [Function("TotalNumberOfCharactersAndWords")]
        public async Task<IActionResult> TotalNumberOfCharactersAndWords([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _Logger.LogDebug("Starting {@MethodName}", nameof(TotalNumberOfCharactersAndWords));
            var bufferSize = req.Query["BufferSize"];
            
            if (string.IsNullOrWhiteSpace(bufferSize)) return new BadRequestObjectResult("BufferSize cannot be null");
            if (!int.TryParse(bufferSize, out int bufferSizeInteger)) return new BadRequestObjectResult("BufferSize has to be a integer");
            var dataString = await _WordStreamService.GetStreamDataAsync(bufferSizeInteger);
            await _StatisticsService.ConsumeString(dataString);
            List<ResultsTable> data = new List<ResultsTable>();

            data.Add(_StatisticsService.TotalNumberOfCharactersAndWords());
            _Logger.LogDebug("Finishing {MethodName}", nameof(TotalNumberOfCharactersAndWords));
            return new OkObjectResult(data);
        }

        [Function("GetLargestWords")]
        public async Task<IActionResult> GetLargestWords([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _Logger.LogDebug("Starting {@MethodName}", nameof(GetLargestWords));
            var bufferSize = req.Query["BufferSize"];
            
            if (string.IsNullOrWhiteSpace(bufferSize)) return new BadRequestObjectResult("BufferSize cannot be null");
            if (!int.TryParse(bufferSize, out int bufferSizeInteger)) return new BadRequestObjectResult("BufferSize has to be a integer");

            var number = req.Query["Number"];
            
            if (string.IsNullOrWhiteSpace(number)) return new BadRequestObjectResult("Number cannot be null");
            if (!int.TryParse(number, out int numberInteger)) return new BadRequestObjectResult("Number has to be a integer");

            var dataString = await _WordStreamService.GetStreamDataAsync(bufferSizeInteger);
            await _StatisticsService.ConsumeString(dataString);
            List<ResultsTable> data = new List<ResultsTable>();
            data.Add(_StatisticsService.GetLargestWords(numberInteger));
            _Logger.LogDebug("Finishing {MethodName}", nameof(GetLargestWords));
            return new OkObjectResult(data);
        }

        [Function("GetSmallestWords")]
        public async Task<IActionResult> GetSmallestWords([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _Logger.LogDebug("Starting {@MethodName}", nameof(GetSmallestWords));
            var bufferSize = req.Query["BufferSize"];
            
            if (string.IsNullOrWhiteSpace(bufferSize)) return new BadRequestObjectResult("BufferSize cannot be null");
            if (!int.TryParse(bufferSize, out int bufferSizeInteger)) return new BadRequestObjectResult("BufferSize has to be a integer");

            var number = req.Query["Number"];
            
            if (string.IsNullOrWhiteSpace(number)) return new BadRequestObjectResult("Number cannot be null");
            if (!int.TryParse(number, out int numberInteger)) return new BadRequestObjectResult("Number has to be a integer");

            var dataString = await _WordStreamService.GetStreamDataAsync(bufferSizeInteger);
            await _StatisticsService.ConsumeString(dataString);
            List<ResultsTable> data = new List<ResultsTable>();

            data.Add(_StatisticsService.GetSmallestWords(numberInteger));
            _Logger.LogDebug("Finishing {MethodName}", nameof(GetSmallestWords));
            return new OkObjectResult(data);
        }

        [Function("MostFrequencyWord")]
        public async Task<IActionResult> MostFrequencyWord([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _Logger.LogDebug("Starting {@MethodName}", nameof(MostFrequencyWord));
            var bufferSize = req.Query["BufferSize"];
            
            if (string.IsNullOrWhiteSpace(bufferSize)) return new BadRequestObjectResult("BufferSize cannot be null");
            if (!int.TryParse(bufferSize, out int bufferSizeInteger)) return new BadRequestObjectResult("BufferSize has to be a integer");

            var number = req.Query["Number"];
            
            if (string.IsNullOrWhiteSpace(number)) return new BadRequestObjectResult("Number cannot be null");
            if (!int.TryParse(number, out int numberInteger)) return new BadRequestObjectResult("Number has to be a integer");

            var dataString = await _WordStreamService.GetStreamDataAsync(bufferSizeInteger);
            await _StatisticsService.ConsumeString(dataString);
            List<ResultsTable> data = new List<ResultsTable>();

            data.Add(_StatisticsService.MostFrequencyWord(numberInteger));
            _Logger.LogDebug("Finishing {MethodName}", nameof(MostFrequencyWord));
            return new OkObjectResult(data);
        }

        [Function("GetAllCharactersFrequency")]
        public async Task<IActionResult> GetAllCharactersFrequency([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _Logger.LogDebug("Starting {@MethodName}", nameof(GetAllCharactersFrequency));
            var bufferSize = req.Query["BufferSize"];
            
            if (string.IsNullOrWhiteSpace(bufferSize)) return new BadRequestObjectResult("BufferSize cannot be null");
            if (!int.TryParse(bufferSize, out int bufferSizeInteger)) return new BadRequestObjectResult("BufferSize has to be a integer");

            var dataString = await _WordStreamService.GetStreamDataAsync(bufferSizeInteger);
            
            await _StatisticsService.ConsumeString(dataString);
            List<ResultsTable> data = new List<ResultsTable>();

            data.Add(_StatisticsService.GetAllCharactersFrequency());
            _Logger.LogDebug("Finishing {MethodName}", nameof(GetAllCharactersFrequency));
            return new OkObjectResult(data);
        }
    }
}
