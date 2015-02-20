using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoveMap
{
    class Program
    {
        static void Main(string[] args)
        {
            String input = @"stuff";
            List<String> characters = new List<string>() { "One", "Two", "Three"};

        }

        private void parsePlot(String text)
        {
            string[] sentences = Regex.Split(text, @"(?<=[\.!\?])\s+");
        }
    }
}
