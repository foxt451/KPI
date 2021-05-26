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
        public void NormalUserCommands(LectureDataBase db)
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
                selectedMenu = SafeReadNumberInRange(1, numberMenuItems);
                if (selectedMenu == 1)
                    PrintAccountSettingsOfNormalUser();
                if (selectedMenu == 3)
                    PrintLectures(db);
                Console.Clear();
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
        public void PrintLectures(LectureDataBase db)
        {
            int selectItem = -1;
            LectureOutputter lectureOutputter = new LectureOutputter();
            TestOutputter testOutputter = new TestOutputter();
            while (selectItem != 3)
            {
                Console.Clear();
                Console.WriteLine("This is lectures Menu\n");
                for (int i = 0; i < db.GetLectures().Count; i++)
                {
                    Console.WriteLine($"{i + 1} --> " + db.GetLectures()[i].title);
                }
                Console.Write("To select a lecture, enter its number: ");
                int numberOfLecture = SafeReadNumberInRange(1, db.GetLectures().Count);
                lectureOutputter.OutputLecture(db.GetLectures()[numberOfLecture - 1]);
                Console.WriteLine("1 - to choose another lecture");
                Console.WriteLine("2 - to go to the test after this lecture");
                Console.WriteLine("3 - to quit of lectures");
                selectItem = SafeReadNumberInRange(1, 3);
                if (selectItem == 2)
                {
                    testOutputter.RunTest(db.GetLectures()[numberOfLecture - 1].test);
                }
            }
        }
        public void PrintAccountSettingsOfNormalUser()
        {
            int selectedItem = -1;
            while (selectedItem != 7)
            {
                Console.Clear();
                Console.WriteLine("Account data:\n");
                Console.WriteLine("Your login: " + this._user.login + "\n");
                Console.WriteLine("Your password: " + passw + "\n");
                // Console.WriteLine("Your date of birth: " + this._user.ChangeEmail() + "\n");
                // Console.WriteLine("Your date of birth: " + this._user.ChangeName() + "\n");
                // Console.WriteLine("Your date of birth: " + this._user.ChangeNumber() + "\n");
                // Console.WriteLine("Your date of birth: " + this._user.ChangeBirthDate() + "\n");
                // Console.WriteLine("Your date of birth: " + this._user.ChangePlaceOfWork() + "\n");
                // Console.WriteLine("Your date of birth: " + this._user.ChangePassword() + "\n");
                Console.WriteLine("1 - to change your email\n");
                Console.WriteLine("2 - to change your name\n");
                Console.WriteLine("3 - to change your phone number\n");
                Console.WriteLine("4 - to change your date of birth\n");
                Console.WriteLine("5 - to change your place of work\n");
                Console.WriteLine("6 - to change your password\n");
                Console.WriteLine("7 - to quit from menu\n");
                Console.Write("Enter your input: ");
                selectedItem = SafeReadNumberInRange(1, 7);
                if (selectedItem == 1)
                {
                    Console.Write("Enter new email: ");
                    string email = Console.ReadLine();
                    this._user.ChangeEmail(email);
                    Console.WriteLine("Your email successfully changed!!\n");
                    Console.ReadKey();
                }
                if (selectedItem == 2)
                {
                    Console.Write("Enter new name: ");
                    string name = Console.ReadLine();
                    this._user.ChangeName(name);
                    Console.WriteLine("Your name successfully changed!!\n");
                    Console.ReadKey();
                }
                if (selectedItem == 3)
                {
                    Console.Write("Enter new phone number: ");
                    string phoneNumber = Console.ReadLine();
                    this._user.ChangeNumber(phoneNumber);
                    Console.WriteLine("Your phone number successfully changed!!\n");
                    Console.ReadKey();
                }
                if (selectedItem == 4)
                {
                    Console.Write("Enter new date of birth: ");
                    string dateOfBirth = Console.ReadLine();
                    this._user.ChangeBirthDate(dateOfBirth);
                    Console.WriteLine("Your date of birth successfully changed!!\n");
                    Console.ReadKey();
                }
                if (selectedItem == 5)
                {
                    Console.Write("Enter new place of work: ");
                    string placeOfWork = Console.ReadLine();
                    this._user.ChangePlaceOfWork(placeOfWork);
                    Console.WriteLine("Your place of work successfully changed!!\n");
                    Console.ReadKey();
                }
                if (selectedItem == 6)
                {
                    Console.Write("Enter new password: ");
                    string password = Console.ReadLine();
                    this._user.ChangePassword(password);
                    Console.WriteLine("Your password successfully changed!!\n");
                    Console.ReadKey();
                }
            }
        }

        public int SafeReadNumberInRange(int lowerBound, int upperBound)
        {
            int result;
            string choice = Console.ReadLine();
            while (!int.TryParse(choice, out result) || result < lowerBound || result > upperBound)
            {
                this.WrongInput("Incorrect input!!\nPlease try again: ");
                choice = Console.ReadLine();
            }

            return result;
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
                if (string.IsNullOrEmpty(name)) continue;
                if (name_rgx.IsMatch(name)) break;
                Console.Write("Incorrect input! Please, try again: ");
            }
            Console.WriteLine("Please, choose your nickname:");
            Regex nickname_rgx = new Regex(@"^\w{4,12}$");
            while (true)
            {
                login = Console.ReadLine();
                if (string.IsNullOrEmpty(login)) continue;
                if (!nickname_rgx.IsMatch(login)){Console.Write("Incorrect nickname! Please, try another one: "); continue;}
                if (File.Exists(login+".csv")) {Console.Write("Such user already exists! Please, try another nickname: "); continue;}
                break;
            }
            Console.WriteLine("Please, choose your password:");
            Regex passw_rgx = new Regex(@"^\w{8,}$");
            while (true)
            {
                passw = Console.ReadLine();
                if (string.IsNullOrEmpty(passw)) continue;
                if (passw_rgx.IsMatch(passw)) break;
                Console.Write("Such password isn't allowed! Please, try another one: ");
            }
            Console.WriteLine("Please, enter your date of birth:");
            Regex date_rgx = new Regex(@"^\d{2}\.\d{2}\.\d{4}$");
            string birthDate;
            while (true)
            {
                birthDate = Console.ReadLine();
                if (string.IsNullOrEmpty(birthDate)) continue;
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
                if (string.IsNullOrEmpty(email)) continue;
                if (email_rgx.IsMatch(email)) break;
                Console.Write("Incorrect email format! Please, try again: ");
            }
            string phoneNumber;
            Console.WriteLine("Please, enter your phoneNumber:");
            Regex num_rgx = new Regex(@"^(\+38)?0\d{9}");
            while (true)
            {
                phoneNumber = Console.ReadLine();
                if (string.IsNullOrEmpty(phoneNumber)) continue;
                if (num_rgx.IsMatch(phoneNumber)) break;
                Console.Write("Incorrect phone number format! Please, try again: ");
            }
            FileOperations fo = new FileOperations(path, Output.login+".csv");
            fo.NewUser(passw, name, birthDate, placeOfWork, email, phoneNumber);
        }
    }
}
