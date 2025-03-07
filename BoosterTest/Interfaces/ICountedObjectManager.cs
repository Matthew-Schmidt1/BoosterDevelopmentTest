
namespace BoosterTest.Interfaces
{
    internal interface ICountedObjectManager<T>
    {
        void AddItem(T item);
        bool contain(T item);
        T[] GetAllItems();
        Dictionary<T, int> getOrderByCount(int numberToReturn, bool Descending);
        Dictionary<T, int> getOrderByLength(int numberToReturn, bool Descending);
    }
}