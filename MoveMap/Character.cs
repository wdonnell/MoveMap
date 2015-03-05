using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveMap
{
    public class Character
    {
        public String name { get; set; }
        public Dictionary<Character, int> links { get; set; }

        public Character(String _name)
        {
            name = _name;
            links = new Dictionary<Character, int>();
        }

        public void addLink(Character c)
        {
            if (this != c) //don't link to self
            {
                if (links.ContainsKey(c))
                {
                    links[c]++;
                }
                else
                {
                    links.Add(c, 1);
                }
            }
        }

        public override bool Equals(Object obj)
        {
            var other = (Character)obj;
            return (name == other.name);
        }
    }
}
