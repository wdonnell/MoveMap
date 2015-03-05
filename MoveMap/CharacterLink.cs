using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveMap
{
    class CharacterLink : Object
    {
        public Character a;
        public Character b;
        public int value = 0;

        public override bool Equals(Object obj)
        {
            var other = (CharacterLink)obj;
            return (a == other.a) && (b == other.b);
        }
    }
}
