using Microsoft.Extensions.Logging;

namespace FunctionTest.Models.Defined
{
    public class Character : CountedAbstract<char>
    {
        public Character()
        {
            
        }
        public Character(char item,ILogger<Character> logger) : base(item,logger)
        {
            Length = 1;
        }
        public override string? ToString()
        {
            return $"{this.Entry} {this.Count}";
        }
    }

}
