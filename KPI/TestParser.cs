using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KPI
{
    class TestParser
    {
        private const string TESTS_FOLDER = "tests";
        private const string TESTS_EXTENSION = ".txt";

        public Test ReadTestFromFile(string testName)
        {
            string path = Path.Combine(TESTS_FOLDER, testName + TESTS_EXTENSION);

            Test test = new();
            using (StreamReader reader = new(path))
            {
                JsonSerializerOptions jsonOptions = new();
                jsonOptions.IncludeFields = true;
                string json = reader.ReadToEnd();
                test = JsonSerializer.Deserialize<Test>(json, jsonOptions);
            }
            return test;
        }

        public void WriteTestToFile(Test test, string testName)
        {
            string path = Path.Combine(TESTS_FOLDER, testName + TESTS_EXTENSION);

            using (StreamWriter writer = new(path))
            {
                JsonSerializerOptions jsonOptions = new();
                jsonOptions.IncludeFields = true;
                string json = JsonSerializer.Serialize(test, jsonOptions);
                writer.Write(json);
            }
        }
    }
}
