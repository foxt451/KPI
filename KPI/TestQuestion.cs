using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI
{
    public class TestQuestion
    {
        public string question;
        public string[] options;
        public int answerIndex;

        public TestQuestion(string question, string[] options, int answerIndex)
        {
            this.question = question;
            this.options = options;
            this.answerIndex = answerIndex;
        }
    }
}
