using BoosterTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoosterTest.Models.Defined
{
    class CharacterObject : CountedAbstract<char>, ICounted
    {
        public CharacterObject(Char character) : base(character)
        {
            Length = 1;
        }
        public override string? ToString()
        {
            return $"{this.Entry} {this.getCount()}";
        }
    }

}
