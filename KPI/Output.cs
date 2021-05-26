using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace KPI
{
    public class Output
    {
        public static string login;
        private static string passw;
        public string path = @"\UserData\";
        private User _user;

        public void StartWork()
        {
            FileOperations fo = new FileOperations("", "");
            while (true)
            {
                Console.WriteLine("Do you have an account? Enter 1 to YES or 0 to NO");
                string registered = Console.ReadLine();
                if (registered=="1")
                {
                    for (int i = 0; i < 3; i++)
                    {
                        while (true)
                        {
                            Console.Write("Enter your login: ");
                            login = Console.ReadLine();
                            if (login != "" && fo.IsExists(path, login)) break;
                            WrongInput("Such account doesn't exist! Try another login!");
                        }
                        _user = new User(this);
                        Console.Write("Enter your password: ");
                        passw = Console.ReadLine();
                        //string login = Console.ReadLine();
                        bool flag = _user.Authorization(login, passw);
                        if (flag) break;
                    }
                    Console.Clear();
                    WrongInput("To many atemptions! Try again later.");
                    Environment.Exit(1);
                }

                if (registered == "0")
                {
                    Registration();
                    _user.Authorization(login, passw);
                }
                else
                {
                    Console.Clear();
                    WrongInput();
                }
            }
            
        }

        public void Welcome(string name)
        {
            Console.Clear();
            Console.WriteLine($"Wellcome back, {name}!\n");
        }
        public void NormalUserCommands()
        {
            Console.Clear();
            Console.WriteLine("Hello, " + _user.login + "! This is user Menu\n");
            int selectedMenu = -1;
            const int numberMenuItems = 5;
            while (selectedMenu != numberMenuItems)
            {
                Console.WriteLine("1 - to see your account settings\n");
                Console.WriteLine("2 - to see your learning progress\n");
                Console.WriteLine("3 - to see lectures\n");
                Console.WriteLine("4 - to see reference\n");
                Console.WriteLine("5 - to log out from account\n");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                while (!int.TryParse(choice, out selectedMenu) || selectedMenu < 1 || selectedMenu > numberMenuItems)
                {
                    this.WrongInput();
                    choice = Console.ReadLine();
                }
            }
            
            
            /*_user = new User(login, path, logsFile);
            Console.WriteLine("Your functional:");
            Console.WriteLine("Enter '1' check your password \n" + "Enter '2' to check your login \n" + "Enter '3' to change your password \n");
            int command = Int32.Parse(Console.ReadLine());
            Console.Clear();
            if (command == 1) _user.ViewPassword();
            else if (command == 2) Console.WriteLine($"Your login: {login}");
            else if (command == 3)
            {
                Console.WriteLine("Enter a new password:");
                string password = Console.ReadLine();
                _user.ChangePassword(password);
            }
            else WrongInput("Such command doesn't exist!");*/
        }
        public void AdminCommands()
        {
            /*Admin admin = new Admin(login, path, logsFile);
            Console.WriteLine("Your functional:");
            Console.WriteLine("Enter '1' to add user \n" + "Enter '2' to remove user \n" + "Enter '3' to change user information \n" + "Enter '4' to change student's group \n" + "Enter '5' to change student's course \n" + "Enter '6' to check student's course \n" + "Enter '7' to check student's group \n" + "Enter '8' to check student's marks \n" + "Enter '9' to exit \n" + "Enter '0' to open user's menu");
            int command = Int32.Parse(Console.ReadLine());
            Console.Clear();
            if (command == 9) Environment.Exit(0);
            else if (command == 0) { UserCommands(); AdminCommands(); }
            Console.Write("Enter user's login: ");
            string userLogin = Console.ReadLine();
            if (command == 1)
            {
                Console.WriteLine("Enter a password for new user:");
                string password = Console.ReadLine();
                Console.WriteLine("Enter a access level for new user:");
                string accessLevel = Console.ReadLine();
                if (accessLevel.Equals("1"))
                {
                    string group;
                    while (true)
                    {
                        Console.Write("Enter a group for new student: ");
                        group = Console.ReadLine();
                        if (group.Length == 4 && Char.IsLetter(group[0]) && Char.IsLetter(group[1]) &&
                            Char.IsDigit(group[2]) && Char.IsDigit(group[3])) break;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        WrongInput("Invalid group name!");
                        Console.ResetColor();
                    }
                    string course;
                    while (true)
                    {
                        Console.Write("Enter a course for new student: ");
                        course = Console.ReadLine();
                        if (Int32.Parse(course) > 0 && Int32.Parse(course) < 7) break;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        WrongInput("Invalid course number! Try again!");
                        Console.ResetColor();
                    }
                    admin.AddUser(userLogin, password, accessLevel, group, course);
                }
                else admin.AddUser(userLogin, password, accessLevel);
            }
            else if (command == 2) admin.RemoveUser(userLogin);
            else if (command == 3)
            {
                Console.Write($"Enter a new {userLogin}'s password: ");
                string passw = Console.ReadLine();
                admin.ChangeUsersPassword(userLogin, passw);
            }
            else if (command == 4)
            {
                string group;
                while (true)
                {
                    Console.WriteLine("Enter a group which you want to join (Example: IP01");
                    Console.Write("Group: ");
                    group = Console.ReadLine();
                    if (group.Length == 4 && Char.IsLetter(group[0]) && Char.IsLetter(group[1]) &&
                        Char.IsDigit(group[2]) && Char.IsDigit(group[3])) break;
                    Console.Clear();
                    WrongInput("Invalid group name!");
                }
                admin.ChangeGroup(userLogin, group);
            }
            else if (command == 5) admin.ChangeCourse(userLogin);
            else if (command == 6) admin.CheckCourse(userLogin);
            else if (command == 7) admin.CheckGroup(userLogin);
            else if (command == 8) admin.CheckMarks(userLogin);
            else WrongInput("Such command doesn't exist!");
            AdminCommands();*/
        }

        public void WrongInput(string line = "Incorrect input!")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(line);
            Console.ResetColor();
        }
        public void DoesNotExist(string login)
        {
            Console.WriteLine($"{login} does not exists!");
        }

        public void Success()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success");
            Console.ResetColor();
        }

        public void ListOutput(List<string> information)
        {
            Console.Clear();
            foreach (string item in information)
            {
                string[] marks = item.Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (marks.Length == 1)
                {
                    Console.WriteLine(marks[0] + " has no marks");
                    continue;
                }
                string line = marks[0] + ": ";
                for (int i = 1; i < marks.Length; i++)
                {
                    line += marks[i];
                    if (i + 1 < marks.Length) line += ", ";
                }
                Console.WriteLine(line);
            }
        }
        public void LineOutput(string line) => Console.WriteLine(line);

        private void Registration()
        {
            Console.WriteLine("Do you want to get an account? Enter 1 to YES or 0 to NO");
            string go = Console.ReadLine();
            if (go=="0") Environment.Exit(0);
            //there will be a lot of cycles and validity check
            Regex name_rgx = new Regex(@"^[A-Z][a-z]*\s[A-Z][a-z]*$");
            Console.WriteLine("Please, enter your full name:");
            string name;
            while (true)
            {
                name = Console.ReadLine();
                if (name_rgx.IsMatch(name)) break;
                Console.Write("Incorrect input! Please, try again: ");
            }
            Console.WriteLine("Please, choose your nickname:");
            Regex nickname_rgx = new Regex(@"^[\w]{4,12}+$");
            while (true)
            {
                login = Console.ReadLine();
                if (!nickname_rgx.IsMatch(login)){Console.Write("Incorrect nickname! Please, try another one: "); continue;}
                if (File.Exists(login+".csv")) {Console.Write("Such user already exists! Please, try another nickname: "); continue;}
                break;
            }
            Console.WriteLine("Please, choose your password:");
            Regex passw_rgx = new Regex(@"^\w{8,}$");
            while (true)
            {
                passw = Console.ReadLine();
                if (passw_rgx.IsMatch(passw)) break;
                Console.Write("Such password isn't allowed! Please, try another one: ");
            }
            Console.WriteLine("Please, enter your date of birth:");
            Regex date_rgx = new Regex(@"^\d{2}\.\d{2}\.\d{4}$");
            string birthDate;
            while (true)
            {
                birthDate = Console.ReadLine();
                if (date_rgx.IsMatch(birthDate)) break;
                Console.Write("Incorrect date format! Please, try again: ");
            }
            Console.WriteLine("Please, enter your place of work or study:");
            string placeOfWork = Console.ReadLine();
            Console.WriteLine("Please, enter your email:");
            string email;
            Regex email_rgx = new Regex(@"^[\w]+\@[a-z]+(\.[a-z])*$");
            while (true)
            {
                email = Console.ReadLine();
                if (email_rgx.IsMatch(email)) break;
                Console.Write("Incorrect email format! Please, try again: ");
            }
            string phoneNumber;
            Console.WriteLine("Please, enter your phoneNumber:");
            Regex num_rgx = new Regex(@"^(\+38)?0\d{9}");
            while (true)
            {
                phoneNumber = Console.ReadLine();
                if (num_rgx.IsMatch(phoneNumber)) break;
                Console.Write("Incorrect phone number format! Please, try again: ");
            }
            FileOperations fo = new FileOperations(path, Output.login+".csv");
            fo.NewUser(passw, name, birthDate, placeOfWork, email, phoneNumber);
        }
    }
}
