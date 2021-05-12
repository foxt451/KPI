using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI
{
    // all test are written in files by this example:
    // 1st line (title):
    // Pointers and variables
    // then [
    // then the index of the right answer (from 0):
    // 2
    // then the question text:
    // What is...
    // then ] (end of question)
    // then options,  separated by { (options are specified without letters/numbers):
    // {
    // somehting..
    // }
    // {
    // other..
    // }
    // {
    // else..
    // }
    // then, again [, indicating next question:
    // [
    // What is...
    // ...


    // example:
    // Functions
    // [
    // 0
    // What is...
    // ]
    // {
    // it's a..
    // }
    // {
    // no, it's...
    // }
    // {
    // no...
    // }
    // [
    // 2
    // What is..
    // ...
    // }
    class TestParser
    {
        private const string TESTS_FOLDER = "tests";

        public Test ReadTestFromFile(string fileName)
        {
            string path = Path.Combine(TESTS_FOLDER, fileName);

            Test test = new();
            using (StreamReader reader = new(path))
            {
                // read title
                string title = reader.ReadLine();
                test.title = title;

                // remove first [
                reader.ReadLine();

                // read questions
                List<TestQuestion> questions = new();
                while (!reader.EndOfStream)
                {
                    TestQuestion question = new();
                    // read the right answer and question text
                    int answer = int.Parse(reader.ReadLine());
                    question.answerIndex = answer;
                    string text = ReadToSeparator(reader, "]");
                    question.question = text;
                    // read options
                    List<string> options = new();
                    while (!reader.EndOfStream && reader.ReadLine() != "[")
                    {
                        string option = ReadToSeparator(reader, "}");
                        options.Add(option);
                    }
                    question.options = options;
                    questions.Add(question);
                }

                test.questions = questions;
            }
            return test;
        }

        private string ReadToSeparator(StreamReader reader, string separator)
        {
            string text = "";
            while (!reader.EndOfStream)
            {
                string nextLine = reader.ReadLine();
                if (nextLine == separator)
                {
                    break;
                }
                else
                {
                    text += nextLine;
                }
            }
            return text;
        }
    }
}
