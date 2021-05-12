using System.Collections.Generic;

namespace KPI
{
    public class Lecture
    {
        private static int ID = 0;
        private int id;
        public int number;
        public string title;
        public List<string> lectureBlocks;
        public Test test;
        
        public Lecture(int nubmer, string title, List<string> lectureBlocks, Test test)
        {
            Lecture.ID++;
            this.id = Lecture.ID;
            this.number = number;
            this.title = title;
            this.lectureBlocks = lectureBlocks;
            this.test = test;
        }
    }
}