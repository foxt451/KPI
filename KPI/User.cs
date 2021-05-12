using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI
{
    public class User
    {
        public string login { get; private set; }
        /*private string password;
        private string name;
        private string surname;
        private string email;
        private string phone;*/
        private FileOperations fo;
        private Output output;
        public User(Output output) { this.output = output; fo = new FileOperations(output.path, Output.login+".csv"); }
        
        public bool Authorization(string log, string password)
        {
            if (fo.IsExists(output.path, log))
            {
                string pass = fo.GetLine(0);
                if (pass.Equals(password))
                {
                    string accessLevel = fo.GetLine(1);
                    output.Welcome(fo.GetLine(2));
                    if (accessLevel.Equals("admin"))
                    {
                        output.AdminCommands();
                        return true;
                    }
                    if (accessLevel.Equals("user"))
                    {
                        output.NormalUserCommands();
                        return true;
                    }
                }
            }
            output.DoesNotExist(log);
            return false;
        }

        public void ChangePassword(string passw)
        {
            fo.ChangeInFile(0, passw);
        }
        public void ChangeName(string name)
        {
            fo.ChangeInFile(2, name);
        }
        public void ChangeBirthDate(string date)
        {
            fo.ChangeInFile(3, date);
        }
        public void ChangePlaceOfWork(string place)
        {
            fo.ChangeInFile(2, place);
        }
        public void ChangeEmail(string email)
        {
            fo.ChangeInFile(2, email);
        }
        public void ChangeNumber(string number)
        {
            fo.ChangeInFile(2, number);
        }
    }
}
