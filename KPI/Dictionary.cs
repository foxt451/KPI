using System;
using System.Collections.Generic;

namespace KPI
{
    public class Dictionary
    {
        private Dictionary<string, string> _dictionary;
        public Dictionary()
        {
            FileOperations fo = new FileOperations("../../../Dictionary/", "dictionary.txt");
            List<string> strs = fo.ReadAllFile();
            _dictionary = new Dictionary<string, string>();
            foreach (var str in strs)
            {
                string[] parts = str.Split(';');
                _dictionary.Add(parts[0], parts[1]);
            }
        }

        public string findMethod(string searchLine)
        {
            foreach (var elem in _dictionary)
            {
                if (elem.Key.Contains(searchLine))
                {
                    return elem.Value;
                }
            }
            return "";
        }

        public void printDictionary()
        {
            Console.WriteLine("Dictionary:");
            foreach (var elem in _dictionary)
            {
                Console.WriteLine(elem.Key + " --> " + elem.Value);
            }
        }
    }
}