using FunctionTest.Models.Interfaces;
using Microsoft.Extensions.Logging;

namespace FunctionTest.Models
{
    public abstract class CountedAbstract<T> : ICounted<T>
    {
        public int Length { get; protected set; }
        public T Entry { get;set;}
        public int Count { get ; protected set; }
        protected readonly ILogger _Logger;
        public CountedAbstract() {
            Entry = (T)new Object();
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _Logger = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            throw new NotImplementedException();
        }

        public CountedAbstract(T item, ILogger<CountedAbstract<T>> logger)
        {
            Entry = item;
            Count = 1;
            _Logger = logger;
        }

        public void IncreaseCount()
        {
            _Logger.LogTrace("Starting {MethodName}",nameof(IncreaseCount));
            ++Count;
            _Logger.LogTrace("Finishing {@MethodName}",nameof(IncreaseCount));
        }

    }
}
