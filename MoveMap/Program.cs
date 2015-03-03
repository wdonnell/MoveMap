using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Utilities.Collections;
using QuickGraph;
using Spider.Utils;

namespace MoveMap
{
    class MoveMap
    {
        static void Main(string[] args)
        {
            PDFParser parser = new PDFParser();
            parser.ExtractText("./birdman_script.pdf", "birdman_script.txt");

            String input = File.ReadAllText("./birdman_script.txt");
            //String input = File.ReadAllText("./plot_summary_whiplash.txt"); //taken from http://www.themoviespoiler.com/2014Spoilers/Whiplash.html
            //String input = File.ReadAllText("./plot_summary_birdman.txt"); //taken from http://www.themoviespoiler.com/2014Spoilers/Birdman.html
            //List<String> scenes = new List<string>(input.Split('\n'));
            List<String> scenes = ScriptParser.parseScript(input);
            //List<String> characterNames = new List<string>() { "Andrew", "Fletcher", "Tanner", "Metz", "Jim", "Nicole", "Connolly" };
            List<String> characterNames = new List<string>() { "Riggan", "Birdman", "Jake", "Ralph", "Sam", "Lesley", "Laura", "Mike", "Sylvia", "Tabitha", "Annie", "Han", "Korean", "Stage Hand", "Crew Member", "Gabriel", "Clara", "Larry", "Tourist", "Man on Street", "Lady", "Kid #1", "KID #2", "Woman on Street", "Crowd", "Old Usher", "Bartender", "Waiter", "Guy", "Lady", "Good Neighbor", "Usher"};
            List<Character> characters = new List<Character>();
            foreach (String s in characterNames)
            {
                characters.Add(new Character(s));
            }

            //Dictionary<String[], int> links = new Dictionary<String[], int>();
            List<CharacterLink> links = new List<CharacterLink>();
            Hashtable test = new Hashtable();

            int i = 0;
            foreach (String scene in scenes)
            {
                i++;
                var result = SceneParser.parseScene(scene, characters).OrderBy(x => x.name);
                String toPrint = "Scene " + i + ": ";
                foreach (var a in result)
                {
                    foreach (var b in result)
                    {
                        if(a != b) //dont' link characters to themselves
                        { 
                            String[] key = new String[] {a.name, b.name};
                            key = key.OrderBy(x => x).ToArray();
                            var link = new CharacterLink { a = key[0], b = key[1], value = 1};
                            if (!links.Contains(link)) //this is a total mess.  IMPROVE THIS PLEASE
                            {
                                links.Add(link);
                            }
                            else
                            {
                                links.Single(x => x.Equals(link)).value++;
                            }
                            a.addLink(b);
                        }
                    }
                }
            }
            double lengthFactor = 7; //TODO: make this dependent on average or max link score.  hardcoding for now, obvs
            double sizefactor = 25; //ohh this is so bad and lazy.  fix later
            //output neato file
            String output = "strict graph G {\n";
            foreach (Character c in characters)
            {
                output += "\"" + c.name + "\"[height=" + (double)c.links.Sum(x => x.Value) / sizefactor + " width=" + (double)c.links.Sum(x => x.Value) /sizefactor + "];\n";
            }
            foreach (CharacterLink link in links)
            {
                output += "\t\"" + link.a + "\" -- \"" + link.b + "\" [len= " + (lengthFactor / (double)link.value) * 2 + "];\n";
            }
            output += "}\n";
            File.WriteAllText("./output.neato", output);
            System.Diagnostics.Process.Start("neato", "-ofile -O -Tpng output.neato");
        }

        public static class ScriptParser
        {
            public static List<String> parseScript(String input)
            {
                return new List<String>(input.Split(new string[] { "INT.", "EXT." }, StringSplitOptions.None));
            }
        }

        public static class SceneParser
        {
            public static List<Character> parseScene(String text, List<Character> characters)
            {
                List<Character> toReturn = new List<Character>();
                foreach (Character character in characters)
                {
                    if (text.Contains(character.name.ToUpper()))
                    {
                        toReturn.Add(character);
                    }
                }
                return toReturn;
            }
        }
    }
}
