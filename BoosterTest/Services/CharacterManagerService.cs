using BoosterTest.Interfaces;
using BoosterTest.Models.Defined;

namespace BoosterTest.Services
{
    class CharacterManagerService : ICountedObjectManager<char>
    {
        public List<CharacterObject> Characters = new List<CharacterObject>();
        public void AddItem(char item)
        {
            if (char.IsWhiteSpace(item) || !char.IsLetterOrDigit(item)) return;
            if (contain(item))
            {
                // We alread know the word so we are increating  the count.
                FindWord(item)?.IncreaseCount();
                return;
            }
            // we didn't find the word so we are adding it to be managed.
            Characters.Add(new CharacterObject(item));
            return;
        }

        public bool contain(char item)
        {
            return Characters.Any(s => s.Entry.Equals(item));
        }

        public char[] GetAllItems()
        {
            return Characters.Select(s => s.Entry).ToArray<char>();
        }

        private CharacterObject[] getOrderedByCountDescending(int numberToReturn)
        {
            return Characters.OrderByDescending(w => w.getCount()).Take(numberToReturn).ToArray();
        }

        private CharacterObject[] getOrderedByCount(int numberToReturn)
        {
            return Characters.OrderBy(w => w.getCount()).Take(numberToReturn).ToArray();
        }

        public Dictionary<char, int> getOrderByCount(int numberToReturn, bool Descending)
        {
            if (Descending)
            {
                return getOrderedByCountDescending(numberToReturn).ToDictionary(k => k.Entry, v => v.getCount());
            }
            return getOrderedByCount(numberToReturn).ToDictionary(k => k.Entry, v => v.getCount());
        }

        public Dictionary<char, int> getOrderByLength(int numberToReturn, bool Descending)
        {
            var result = new Dictionary<char, int>();
            foreach( var character in GetAllItems())
            {
                result.Add(character, 1);
            }
            return result;
        }

        private CharacterObject FindWord(char word)
        {
            return Characters.Find(f => f.Entry.Equals(word));
        }
    }
}
