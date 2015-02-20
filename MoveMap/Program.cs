using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoveMap
{
    class MoveMap
    {
        static void Main(string[] args)
        {
            String input = File.ReadAllText("./plot_summary.txt"); //taken from http://www.themoviespoiler.com/2014Spoilers/Whiplash.html
            List<String> scenes = new List<string>(input.Split('\n'));
            List<String> characters = new List<string>() { "Andrew", "Fletcher", "Tanner", "Metz", "Jim", "Nicole", "Connolly" };

            int i = 0;
            foreach (String scene in scenes)
            {
                i++;
                var result = SceneParser.parseScene(scene, characters);
                String toPrint = "Scene " + i + ": ";
                foreach (var s in result)
                {
                    toPrint += s + ", ";
                }
                Console.WriteLine(toPrint);
            }
        }

        public static class SceneParser
        { 
            public static List<String> parseScene(String text, List<String> characters)
            {
                //string[] sentences = Regex.Split(text, @"(?<=[\.!\?])\s+");
                List<String> toReturn = new List<string>();
                foreach (String name in characters)
                {
                    if (text.Split(new char[] {' ', '.', '?', '!', ','}).Contains(name))
                    {
                        toReturn.Add(name);
                    }
                }
                return toReturn;
            }
        }
    }
}
