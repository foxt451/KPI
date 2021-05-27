using System;
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
            results[indLection] = 0;
            string toFile = "";
            for (int i = 0; i < progress.Count; i++)
            {
                toFile += Convert.ToString(progress[i]);
                if (i != progress.Count - 1)
                {
                    toFile += ",";
                }
            }
            fo.AddToLine(7,toFile);
            toFile = "";
            for (int i = 0; i < results.Count; i++)
            {
                toFile += Convert.ToString(results[i]);
                if (i != results.Count - 1)
                {
                    toFile += ",";
                }
            }
            fo.AddToLine(8, toFile);
        }

        public void takeTest(int indTest, int result)
        {
            results[indTest] = result;
            string toFile = "";
            for (int i = 0; i < progress.Count; i++)
            {
                toFile += Convert.ToString(progress[i]);
                if (i != progress.Count - 1)
                {
                    toFile += ",";
                }
            }
            fo.AddToLine(7,toFile);
            toFile = "";
            for (int i = 0; i < results.Count; i++)
            {
                toFile += Convert.ToString(results[i]);
                if (i != results.Count - 1)
                {
                    toFile += ",";
                }
            }
            fo.AddToLine(8, toFile);
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