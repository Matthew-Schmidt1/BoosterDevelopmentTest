using FunctionTest.Models.Defined;
using FunctionTest.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace FunctionTest.Services
{
    internal class CharacterManagerService : ManagerCountedServiceBase<Character, char>, ICountedObjectManager<char>
    {
        public CharacterManagerService(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _Logger = new Logger<CharacterManagerService>(_LoggerFactory);
        }
        public override void AddItem(char item)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(AddItem));
            if (char.IsWhiteSpace(item) || !char.IsLetterOrDigit(item)) return;
            if (Contains(item))
            {
                // We alread know the word so we are increating  the count.
                FindItem(item)?.IncreaseCount();
            }
            else
            {
                // we didn't find the word so we are adding it to be managed.
                Items.Add(new Character(item, new Logger<Character>(_LoggerFactory)));
            }
            _Logger.LogTrace("Finishing {MethodName}", nameof(AddItem));
            return;
        }
        public override bool Contains(char item)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(Contains));
            var result = Items.Any(s => s.Entry.Equals(item));
            _Logger.LogTrace("Finishing {MethodName}", nameof(Contains));
            return result;
        }
        protected override Character? FindItem(char item)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(FindItem));
            var result = Items.Find(f => f.Entry.Equals(item));
            _Logger.LogTrace("Finishing {MethodName}", nameof(FindItem));
            return result;
        }
    }
}
