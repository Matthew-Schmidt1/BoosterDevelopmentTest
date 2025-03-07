using BoosterTest.Interfaces;
using BoosterTest.Models.Defined;

namespace BoosterTest.Services
{
    class WordManagerService: ICountedObjectManager<string>
    {
        public List<WordObject> words = new List<WordObject>();

        public bool contain(string item)
        {
            return words.Any(s => s.Entry.Equals(item, StringComparison.InvariantCultureIgnoreCase));
        }

        public void AddItem(string item)
        {
            item = SanitizeInput(item);
            if (string.IsNullOrWhiteSpace(item)) return;
            
            
            if (contain(item))
            {
                // We alread know the word so we are increating  the count.
                FindWord(item)?.IncreaseCount();
                return;
            }
            // we didn't find the word so we are adding it to be managed.
            words.Add(new WordObject(item));
            return;
        }

        private string SanitizeInput(string item)
        {
            char[] arr = item.Where(c => (char.IsLetterOrDigit(c))).ToArray();
            return new string(arr);
        }

        private WordObject FindWord(string item)
        {
            return words.Find(f => f.Entry.Equals(item, StringComparison.InvariantCultureIgnoreCase));
        }


        public string[] GetAllItems()
        {
            return words.Select(s => s.Entry).ToArray<string>();
        }


        private WordObject[] getOrderedByLengthDescending(int numberToReturn)
        {
            return words.OrderByDescending(w => w.Length).Take(numberToReturn).ToArray();
        }

        private WordObject[] getOrderedByLength(int numberToReturn)
        {
            return words.OrderBy(w => w.Length).Take(numberToReturn).ToArray();
        }

        private WordObject[] getOrderedByCountDescending(int numberToReturn)
        {
            return words.OrderByDescending(w => w.getCount()).Take(numberToReturn).ToArray();
        }

        private WordObject[] getOrderedByCount(int numberToReturn)
        {
            return words.OrderBy(w => w.getCount()).Take(numberToReturn).ToArray();
        }

        public Dictionary<string,int> getOrderByCount(int numberToReturn, bool Descending)
        {
            if (Descending)
            {
                return getOrderedByCountDescending(numberToReturn).ToDictionary(k => k.Entry,v => v.getCount());
            }
            return getOrderedByCount(numberToReturn).ToDictionary(k => k.Entry, v => v.getCount());
        }

        public Dictionary<string, int> getOrderByLength(int numberToReturn, bool Descending)
        {
            if (Descending)
            {
                return getOrderedByLengthDescending(numberToReturn).ToDictionary(k => k.Entry, v => v.getCount());
            }
            return getOrderedByLength(numberToReturn).ToDictionary(k => k.Entry, v => v.getCount());
        }
    }
}
