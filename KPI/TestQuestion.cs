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
        public List<string> options = new();
        public int answerIndex;

        public bool ValidateAnswer(int optionIndex)
        {
            return optionIndex == answerIndex;
        }
    }
}
