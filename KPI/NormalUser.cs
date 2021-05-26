using System.Collections.Generic;

namespace KPI
{
    public class NormalUser : User
    {
        private List<int> progress;
        private List<int> results;
        
        
        public NormalUser(Output output) : base(output)
        {
        }

        public void readLection(int indLection)
        {
            progress[indLection] = 1;
        }

        public void takeTest(int indTest, int result)
        {
            results[indTest] = result;
        }

        public List<int> getProgress()
        {
            return progress;
        }

        public List<int> getResult()
        {
            return results;
        }

        public void loadProgress()
        {
            string[] progressString = fo.GetLine(7).Split(',');
            string[] resultsString = fo.GetLine(8).Split(',');
            for (int i = 0; i < progressString.Length; i++)
            {
                progress[int.Parse(progressString[i])] = 1;
                results[int.Parse(progressString[i])] = int.Parse(resultsString[i]);
            }
        }
    }
}