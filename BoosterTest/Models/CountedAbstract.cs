using BoosterTest.Interfaces;

namespace BoosterTest.Models
{
    abstract class CountedAbstract<T> 
    {
        public int Length { get; protected set; }
        public readonly T Entry;
        private int Count;

        public CountedAbstract(T entry)
        {
            this.Entry = entry;
            Count = 1;
        }

        public void IncreaseCount()
        {
            ++Count;
        }

        public int getCount()
        {
            return Count;
        }

        

    }
}
