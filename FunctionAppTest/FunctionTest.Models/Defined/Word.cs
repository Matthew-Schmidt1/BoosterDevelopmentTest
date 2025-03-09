using Microsoft.Extensions.Logging;

namespace FunctionTest.Models.Defined
{
    public class Word : CountedAbstract<string>
    {
        public Word() { }
        public Word(string item,ILogger<Word> logger) : base(item, logger)
        {
            Length = item.Length;
        }

        public override string? ToString()
        {
            return $"{this.Entry} {this.Length} {this.Count}";
        }
    }
}
