using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace KPI{
    public class FileOperations
    {
        private string path;

        public FileOperations(string p, string fileName)
        {
            this.path = Path.Combine(p, fileName);
            if (!File.Exists(path))
            {
                using (File.Create(path)) { }
            }
        }
        public void AddToFile(string line) //adds a line at the end of file
        {
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(line);
            }
        }
        public void AddToLine(int index, string data) //adds data to the end of given line
        {
            List<string> information = new List<string>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                int ctr = 0;
                while (!sr.EndOfStream)
                {
                    string item = sr.ReadLine();
                    string[] splited = item.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    if (ctr == index) information.Add(item+","+data);
                    else information.Add(item);
                    ctr++;
                }
            }
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (string item in information)
                {
                    sw.WriteLine(item);
                }
            }
        }
        public void ChangeInFile(int index, string line)    //replace line in file at given index by another given line
        {
            List<string> information = new List<string>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                int ctr = 0;
                while (!sr.EndOfStream)
                {
                    string item = sr.ReadLine();
                    string[] splited = item.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    if (ctr == index) information.Add(line);
                    else information.Add(item);
                    ctr++;
                }
            }
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (string item in information)
                {
                    sw.WriteLine(item);
                }
            }
        }
        public string GetLine(int index)    //returns the line at given index from file
        {
            string item = String.Empty;
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                int ctr = 0;
                while (!sr.EndOfStream)
                {
                    item = sr.ReadLine();
                    if (ctr==index) return item;
                    ctr++;
                }
            }
            return item;
        }
        public List<string> ReadAllFile() //returns the list of file lines
        {
            List<string> information = new List<string>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                while (!sr.EndOfStream) information.Add(sr.ReadLine());
            }
            return information;
        }

        public bool SignIn(string passw)    //check given password for validity
        {

            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string item = sr.ReadLine();
                if (item.Equals(passw)) return true;
                return false;
            }
        }
    }
}
