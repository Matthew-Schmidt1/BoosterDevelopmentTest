using Booster.CodingTest.Library;
using FunctionTest.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace FunctionTest.Services
{
    internal class WordStreamService : IWordStreamService
    {
        private readonly ILogger _Logger;
        private readonly WordStream _WordStream = new();
        public WordStreamService(ILogger<WordStreamService> logger)
        {
            _Logger = logger;
        }
        public async Task<string> GetStreamDataAsync(int BufferSize = 1024)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(GetStreamDataAsync));
            byte[] data = new byte[BufferSize];
            await _WordStream.ReadAsync(data);
            var str = System.Text.Encoding.Default.GetString(data);
            return str;
        }
    }
}
