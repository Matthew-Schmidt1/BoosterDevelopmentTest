using FunctionTest.Models;
using FunctionTest.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;


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
        [OpenApiOperation(operationId: "GetAll",Description ="Returns all data points")]
        [OpenApiParameter(name: "BufferSize", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The Buffer size for the buffer to read from the stream")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ResultsTable[]),
            Description = "The OK response message containing a JSON result.")]
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
        [OpenApiOperation(operationId: "TotalNumberOfCharactersAndWords",Description = "Returns Total number of characters and words")]
        [OpenApiParameter(name: "BufferSize", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The Buffer size for the buffer to read from the stream")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ResultsTable[]),
            Description = "The OK response message containing a JSON result.")]
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
        [OpenApiOperation(operationId: "GetLargestWords", Description = "Returns the x largest words")]
        [OpenApiParameter(name: "BufferSize", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description ="The Buffer size for the buffer to read from the stream")]
        [OpenApiParameter(name: "Number", In = ParameterLocation.Path, Required = true, Type = typeof(int),Description ="Number of Elements to return so 5 for the 5 largest words")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ResultsTable[]),
            Description = "The OK response message containing a JSON result.")]
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
        [OpenApiOperation(operationId: "GetSmallestWords", Description = "Returns the x smallest  words")]
        [OpenApiParameter(name: "BufferSize", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The Buffer size for the buffer to read from the stream")]
        [OpenApiParameter(name: "Number", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "Number of Elements to return so 5 for the 5 smallest words")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ResultsTable[]),
            Description = "The OK response message containing a JSON result.")]
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
        [OpenApiOperation(operationId: "MostFrequencyWord", Description = "Returns The x most frequently appearing words")]
        [OpenApiParameter(name: "BufferSize", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The Buffer size for the buffer to read from the stream")]
        [OpenApiParameter(name: "Number", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "Number of Elements to return so 5 for the 5 most Frequent words")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ResultsTable[]),
            Description = "The OK response message containing a JSON result.")]
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
        [OpenApiOperation(operationId: "GetAllCharactersFrequency",Description = "Returns a list of all characters appearing" +
            " in the stream, the frequency with which the characters appear, in descending order of frequency.")]
        [OpenApiParameter(name: "BufferSize", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The Buffer size for the buffer to read from the stream")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ResultsTable[]),
            Description = "The OK response message containing a JSON result.")]
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

        [Function("ResetCount")]
        [OpenApiOperation(operationId: "GetAllCharactersFrequency",Description ="Resets the count of all words and characters.")]
        [OpenApiResponseWithoutBody(HttpStatusCode.OK)]
        public IActionResult ResetCount([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _Logger.LogDebug("Starting {@MethodName}", nameof(ResetCount));

            _StatisticsService.ResetService();
            
            _Logger.LogDebug("Finishing {MethodName}", nameof(ResetCount));
            return new OkResult();
        }
    }
}

