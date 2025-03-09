using FunctionTest.Models.Interfaces;
using Microsoft.Extensions.Logging;

namespace FunctionTest.Services
{
    public abstract class ManagerCountedServiceBase<T,K> where T : ICounted<K> where K: notnull
    {
        protected ILogger _Logger;
        protected readonly ILoggerFactory _LoggerFactory;

        public List<T> Items { get; protected set; } = new List<T>();

        protected ManagerCountedServiceBase(ILoggerFactory loggerFactory) //ILogger<ManagerCountedServiceBase<T, K>> logger)
        {
            _LoggerFactory = loggerFactory;
            _Logger = new Logger<ManagerCountedServiceBase<T, K>>(loggerFactory);
        }
        public abstract void AddItem(K item);
      
        public K[] GetAllItems()
        {
            _Logger.LogTrace("Starting {MethodName}",nameof(GetAllItems));
            var result = Items.Select(s => s.Entry).ToArray<K>();
            _Logger.LogTrace("Finishing {MethodName}",nameof(GetAllItems));
            return result;
        }
        public abstract bool Contains(K item);

        protected abstract T? FindItem(K item);

        public Dictionary<K, int> GetOrderByCount(int numberToReturn, bool Descending)
        {
            Dictionary<K, int>? result = null;
            _Logger.LogTrace("Starting {MethodName}",nameof(GetOrderByCount));
            if (Descending)
            {

                result = GetOrderedByCountDescending(numberToReturn).ToDictionary(k => k.Entry, v => v.Count);
            }
            else
            {
                result = GetOrderedByCount(numberToReturn).ToDictionary(k => k.Entry, v => v.Count);
            }
            _Logger.LogTrace("Finishing {MethodName}",nameof(GetOrderByCount));
            return result;
        }

        public Dictionary<K, int> GetOrderByLength(int numberToReturn, bool Descending)
        {
            Dictionary<K, int>? result = null;
            _Logger.LogTrace("Starting {MethodName}",nameof(GetOrderByLength));
            if (Descending)
            {
                result = getOrderedByLengthDescending(numberToReturn).ToDictionary(k => k.Entry, v => v.Length);
            }
            else
            {
                result = getOrderedByLength(numberToReturn).ToDictionary(k => k.Entry, v => v.Length);
            }
            _Logger.LogTrace("Finishing {MethodName}",nameof(GetOrderByLength));
            return result;
        }


        protected T[] GetOrderedByCount(int numberToReturn)
        {
            _Logger.LogTrace("Starting {MethodName}",nameof(GetOrderedByCount));
            T[] result= Items.OrderBy(w => w.Count).Take(numberToReturn).ToArray();
            _Logger.LogTrace("Finishing {MethodName}",nameof(GetOrderedByCount));
            return result;
        }

        protected T[] GetOrderedByCountDescending(int numberToReturn)
        {
            _Logger.LogTrace("Starting {MethodName}",nameof(GetOrderedByCountDescending));
            T[] result = Items.OrderByDescending(w => w.Count).Take(numberToReturn).ToArray();
            _Logger.LogTrace("Finishing {MethodName}",nameof(GetOrderedByCountDescending));
            return result;
        }

        protected T[] getOrderedByLength(int numberToReturn)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(getOrderedByLength));
            T[] result= Items.OrderBy(w => w.Length).Take(numberToReturn).ToArray();
            _Logger.LogTrace("Finishing {MethodName}", nameof(getOrderedByLength));
            return result;
        }


        protected T[] getOrderedByLengthDescending(int numberToReturn)
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(getOrderedByLengthDescending));
            T[] result = Items.OrderByDescending(w => w.Length).Take(numberToReturn).ToArray();
            _Logger.LogTrace("Finishing {MethodName}", nameof(getOrderedByLengthDescending));
            return result;
        }
        public void ResetCount()
        {
            _Logger.LogTrace("Starting {MethodName}", nameof(ResetCount));
            this.Items = new List<T>();
            _Logger.LogTrace("Finishing {MethodName}", nameof(ResetCount));
        }
    }
}