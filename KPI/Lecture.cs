namespace KPI
{
    public class Lecture
    {
        private static int ID = 0;
        public int id;
        public string name;
        public string textOfLecture;
        public Test test;
        
        public Lecture(string name, string textOfLecture, Test test)
        {
            Lecture.ID++;
            this.id = Lecture.ID;
            this.name = name;
            this.textOfLecture = textOfLecture;
            this.test = test;
        }
    }
}