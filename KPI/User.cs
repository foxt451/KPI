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
        private string password;
        private string name;
        private string surname;
        private string email;
        private string phone;
        private FileOperations fo;
        private Output output;
        public User(Output output) { this.output = output; fo = new FileOperations(output.path, output.logsFile); }
        public User(string login, string password, string name, string surname, string email, string phone, Output output)
        {
            this.login = login;
            this.password = password;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.phone = phone;
            this.output = output;
            fo = new FileOperations(output.path, output.logsFile);
        }
        public bool Authorization(string log, string password)
        {
            if (fo.IsExists(log))
            {
                string pass = fo.GetLine(0);
                if (pass.Equals(password))
                {
                    string accessLevel = fo.GetLine(1);
                    if (accessLevel.Equals("admin"))
                    {
                        output.Welcome();
                        output.AdminCommands();
                        return true;
                    }
                    if (accessLevel.Equals("user"))
                    {
                        output.Welcome();
                        output.NormalUserCommands();
                        return true;
                    }
                }
            }
            output.DoesNotExists(log);
            return false;
        }
    }
}
