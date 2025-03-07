using BoosterTest.Interfaces;
using System.Diagnostics.Metrics;

namespace BoosterTest.Models.Defined
{
    class WordObject : CountedAbstract<string>, ICounted
    {
        public WordObject(string word) : base(word)
        {
            Length = word.Length;
        }

        public override string? ToString()
        {
            return $"{this.Entry} {this.Length} {this.getCount()}";
        }
    }
}
