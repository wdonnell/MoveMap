using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QuickGraph;

namespace MoveMap
{
    class MoveMap
    {
        static void Main(string[] args)
        {
            //String input = File.ReadAllText("./plot_summary.txt"); //taken from http://www.themoviespoiler.com/2014Spoilers/Whiplash.html
            String input = File.ReadAllText("./plot_summary_birdman.txt"); //taken from http://www.themoviespoiler.com/2014Spoilers/Birdman.html
            List<String> scenes = new List<string>(input.Split('\n'));
            //List<String> characters = new List<string>() { "Andrew", "Fletcher", "Tanner", "Metz", "Jim", "Nicole", "Connolly" };
            List<String> characterNames = new List<string>() { "Riggan", "Birdman", "Jake", "Ralph", "Sam", "Lesley", "Laura", "Mike", "Sylvia", "Tabitha" };
            List<Character> characters = new List<Character>();
            foreach (String s in characterNames)
            {
                characters.Add(new Character(s));
            }

            int i = 0;
            foreach (String scene in scenes)
            {
                i++;
                var result = SceneParser.parseScene(scene, characters);
                String toPrint = "Scene " + i + ": ";
                foreach (var a in result)
                {
                    foreach (var b in result)
                    {
                        a.addLink(b);
                    }
                }
            }
            double lengthFactor = 10; //TODO: make this dependent on average or max link score.  hardcoding for now, obvs
            //output neato file
            String output = "strict graph G {\n";
            foreach (Character c in characters)
            {
                //Console.WriteLine(c.name + ":");
                foreach (var lin in c.links)
                {
                    //Console.WriteLine("\t" + lin.Key.name + " " + lin.Value);
                    output += "\t" + c.name + " -- " + lin.Key.name + " [weight=" + lin.Value + " len= " + lengthFactor/lin.Value + "];\n";
                }
            }
            output += "}\n";
            File.WriteAllText("./output.neato", output);
        }

        public static class SceneParser
        {
            public static List<Character> parseScene(String text, List<Character> characters)
            {
                List<Character> toReturn = new List<Character>();
                foreach (Character character in characters)
                {
                    if (text.Split(new char[] { ' ', '.', '?', '!', ',' }).Contains(character.name))
                    {
                        toReturn.Add(character);
                    }
                }
                return toReturn;
            }
        }
    }
}
