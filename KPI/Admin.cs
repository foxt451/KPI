using System.Collections.Generic;

namespace KPI
{
    public sealed class Admin : User
    {
        public Admin(Output output) : base(output) { }

        public void AddLecture(string title, List<string> data)
        {
            LectureDataBase dataBase = new LectureDataBase();
            dataBase.AddNewLecture(title, data);
        }
    }
}
