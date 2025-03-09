
namespace FunctionTest.Services.Interfaces
{
    internal interface ICountedObjectManager<T> where T : notnull
    {
        void AddItem(T item);
        bool Contains(T item);
        T[] GetAllItems();
        Dictionary<T, int> GetOrderByCount(int numberToReturn, bool Descending);
        Dictionary<T, int> GetOrderByLength(int numberToReturn, bool Descending);
        void ResetCount();

    }
}