using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToFile = @"C:\Users\User\Documents\TestTest.txt";

            var inutTextFile = System.IO.File.ReadAllLines(pathToFile);

            Dictionary<string, List<string>> organizationalTree = new Dictionary<string, List<string>>();
            List<string> existing;
            List<string> excludedKeyValues = new List<string>();
            
            foreach (var item in inutTextFile)
            {
                var localLine = item.Split(',');
                if (!excludedKeyValues.Contains(localLine[3]) && !excludedKeyValues.Contains(localLine[0]))
                {
                    if (!organizationalTree.TryGetValue(localLine[3], out existing))
                    {
                        existing = new List<string>();
                        organizationalTree[localLine[3]] = existing;
                    }
                    existing.Add(localLine[0]);
                }
                if(excludedKeyValues.Contains(localLine[3]) || excludedKeyValues.Contains(localLine[0]))
                {
                    excludedKeyValues.Add(localLine[0]);
                }
            }

            Console.WriteLine("SELECT * FROM [TABLE] WHERE ");
            var last = organizationalTree.Last();
            foreach (KeyValuePair<string, List<string>> kvp in organizationalTree)
            {
               // Console.WriteLine(" Key = {0} ", kvp.Key);
                foreach (var item in kvp.Value)
                {
                    if (item == last.Value.Last())
                        Console.WriteLine("Code = {0}", item);
                    else
                        Console.WriteLine("Code = {0} OR ", item);
                }
            }

            Console.WriteLine();

            foreach (KeyValuePair<string, List<string>> kvp in organizationalTree)
            {
                Console.WriteLine(" Key = {0} ", kvp.Key);
                foreach (var item in kvp.Value)
                {
                    Console.WriteLine("       Value = {0}", item);
                }
            }

            Console.ReadKey();
        }
    }
}
