using FunctionTest.Models.Defined;
using FunctionTest.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace FunctionTest.Services
{
    public class WordManagerService : ManagerCountedServiceBase<Word, string>, ICountedObjectManager<string>
    {
        public WordManagerService( ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _Logger = new Logger<WordManagerService>(_LoggerFactory);
        }

        public override void AddItem(string item)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(AddItem));
            item = SanitizeInput(item);
            if (string.IsNullOrWhiteSpace(item))
            {
                _Logger.LogTrace($"item IsNullOrWhiteSpace");
                return;
            }
            if (Contains(item))
            {
                // We alread know the word so we are increating  the count.
                FindItem(item)?.IncreaseCount();
            }
            else
            {
                // we didn't find the word so we are adding it to be managed.
                Items.Add(new Word(item, new Logger<Word>(_LoggerFactory)));
            }

            _Logger.LogTrace("Finishing {MethodName}", nameof(AddItem));
            return;
        }

        private string SanitizeInput(string item)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(SanitizeInput));
            char[] arr = item.Where(c => (char.IsLetterOrDigit(c))).ToArray();
            var result = new string(arr);
            _Logger.LogTrace("Finishing {MethodName}", nameof(SanitizeInput));
            return result;
        }

        public override bool Contains(string item)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(Contains));
            var result = Items.Any(s => s.Entry.Equals(item, StringComparison.InvariantCultureIgnoreCase));
            _Logger.LogTrace("Finishing {MethodName}", nameof(Contains));
            return result;
        }
        protected override Word? FindItem(string item)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(FindItem));
            var result = Items.Find(f => f.Entry.Equals(item, StringComparison.InvariantCultureIgnoreCase));
            _Logger.LogTrace("Finishing {MethodName}", nameof(FindItem));
            return result;
        }
    }
}
