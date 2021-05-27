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
        public string path = @"..\..\..\UserData\";
        private User _user;
        private NormalUser _normalUser;
        private LectureDataBase db;
        private Dictionary glossary;
    
        public Output(LectureDataBase lectureDB, Dictionary dict)
        {
            this.db = lectureDB;
            this.glossary = dict;
        }
        public void StartWork()
        {
            FileOperations fo = new FileOperations("");
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
                    _user = new User(this);
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
            this._normalUser = new NormalUser(this);
            Console.Clear();
            Console.WriteLine("Hello, " + this._normalUser.login + "! This is user Menu");
            int selectedMenu = -1;
            const int numberMenuItems = 5;
            while (selectedMenu != numberMenuItems)
            {
                Console.WriteLine("1 - to see your account settings");
                Console.WriteLine("2 - to see your learning progress");
                Console.WriteLine("3 - to see lectures");
                Console.WriteLine("4 - to see reference");
                Console.WriteLine("5 - to log out from account");
                Console.Write("Enter your choice: ");
                selectedMenu = SafeReadNumberInRange(1, numberMenuItems);
                if (selectedMenu == 1)
                    PrintAccountSettingsOfNormalUser();
                if (selectedMenu == 2)
                    PrintProgressOfNormalUser();
                if (selectedMenu == 3)
                    PrintLectures(this.db);
                if (selectedMenu == 4)
                    DictionaryMenu();
                Console.Clear();
            }
        }

        public void DictionaryMenu()
        {
            Console.WriteLine("This is dictionary menu");
            Console.WriteLine("1 - to find method in dictionary");
            Console.WriteLine("2 - to print dictionary");
            Console.WriteLine("3 - to quit from menu");
            int choice = SafeReadNumberInRange(1, 3);
            if (choice == 1)
            {
                Console.Write("Enter your search query: ");
                string query = Console.ReadLine();
                string result = this.glossary.findMethod(query);
                if (result == "")
                    Console.WriteLine("Not found");
                else
                {
                    Console.WriteLine(result);
                }
            }
            if (choice == 2)
            {
                glossary.printDictionary();
            }
            Console.ReadKey();
        }
        public void PrintLectures(LectureDataBase db)
        {
            int selectItem = -1;
            LectureOutputter lectureOutputter = new LectureOutputter();
            TestOutputter testOutputter = new TestOutputter();
            while (selectItem != 3)
            {
                Console.Clear();
                Console.WriteLine("This is lectures Menu");
                for (int i = 0; i < db.GetLectures().Count; i++)
                {
                    Console.WriteLine($"{i + 1} --> " + db.GetLectures()[i].title);
                }
                Console.WriteLine("1 - to select a lecture to view");
                Console.WriteLine("2 - to quit");
                int choice = SafeReadNumberInRange(1, 2);
                if (choice == 2)
                    break;
                Console.Write("To select a lecture, enter its number: ");
                int numberOfLecture = SafeReadNumberInRange(1, db.GetLectures().Count);
                lectureOutputter.OutputLecture(db.GetLectures()[numberOfLecture - 1]);
                _normalUser.readLection(numberOfLecture - 1);
                Console.Clear();
                Console.WriteLine("1 - to choose another lecture");
                Console.WriteLine("2 - to go to the test after this lecture");
                Console.WriteLine("3 - to quit from lectures");
                selectItem = SafeReadNumberInRange(1, 3);
                if (selectItem == 2)
                {
                    (bool wasInterrupted, int score) = testOutputter.RunTest(db.GetLectures()[numberOfLecture - 1].test);
                    if (!wasInterrupted)
                    {
                        this._normalUser.takeTest(numberOfLecture - 1, score);
                    }
                }
            }
        }

        public void PrintProgressOfNormalUser()
        {
            this._normalUser.loadProgress();
            for (int i = 0; i < 69;i++)
                Console.Write('-');
            Console.WriteLine();
            Console.Write('|');
            Console.Write("#".PadLeft(5));
            Console.Write('|');
            Console.Write("Lecture".PadLeft(50));
            Console.Write('|');
            Console.Write("Viewed".PadLeft(10));
            Console.WriteLine('|');
            for (int j = 0; j < 69; j++)
                Console.Write('-');
            Console.WriteLine();
            for (int i = 0; i < this.db.GetLectures().Count; i++)
            {
                Console.Write('|');
                Console.Write($"{i + 1}".PadLeft(5));
                Console.Write('|');
                Console.Write(db.GetLectures()[i].title.PadLeft(50));
                Console.Write('|');
                if (this._normalUser.getProgress()[i] == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("+".PadLeft(10));
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("-".PadLeft(10));
                    Console.ResetColor();
                }
                Console.WriteLine('|');
                for (int j = 0; j < 69;j++)
                    Console.Write('-');
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
            for (int j = 0; j < 69; j++)
                Console.Write('-');
            Console.WriteLine();
            Console.Write('|');
            Console.Write("#".PadLeft(5));
            Console.Write('|');
            Console.Write("Test".PadLeft(50));
            Console.Write('|');
            Console.Write("Result".PadLeft(10));
            Console.WriteLine('|');
            for (int i = 0; i < 69;i++)
                Console.Write('-');
            Console.WriteLine();
            for (int i = 0; i < this.db.GetLectures().Count; i++)
            {
                Console.Write('|');
                Console.Write($"{i + 1}".PadLeft(5));
                Console.Write('|');
                Console.Write(db.GetLectures()[i].test.title.PadLeft(50));
                Console.Write('|');
                Console.Write(this._normalUser.getResult()[i].ToString().PadLeft(10));
                Console.WriteLine('|');
                for (int j = 0; j < 69; j++)
                    Console.Write('-');
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        public void PrintAccountSettingsOfNormalUser()
        {
            int selectedItem = -1;
            while (selectedItem != 7)
            {
                Console.Clear();
                Console.WriteLine("Account data:");
                Console.WriteLine("Your login: " + this._normalUser.login);
                Console.WriteLine("Your password: " + this._normalUser.GetPassword());
                Console.WriteLine("Your name: " + this._normalUser.GetName());
                Console.WriteLine("Your surname: " + this._normalUser.GetSurname());
                Console.WriteLine("Your email: " + this._normalUser.GetEmail());
                Console.WriteLine("Your phone number: " + this._normalUser.GetPhone());
                Console.WriteLine("Your date of birth: " + this._normalUser.GetBirdthDate());
                Console.WriteLine("Your place of work: " + this._normalUser.GetPlaceOfWork());
                Console.WriteLine("1 - to change your email");
                Console.WriteLine("2 - to change your name");
                Console.WriteLine("3 - to change your phone number");
                Console.WriteLine("4 - to change your date of birth");
                Console.WriteLine("5 - to change your place of work");
                Console.WriteLine("6 - to change your password");
                Console.WriteLine("7 - to quit from menu");
                Console.Write("Enter your input: ");
                selectedItem = SafeReadNumberInRange(1, 7);
                if (selectedItem == 1)
                {
                    Console.Write("Enter new email: ");
                    string email = Console.ReadLine();
                    this._normalUser.ChangeEmail(email);
                    Console.WriteLine("Your email successfully changed!!\n");
                    Console.ReadKey();
                }
                if (selectedItem == 2)
                {
                    Console.Write("Enter new name: ");
                    string name = Console.ReadLine();
                    this._normalUser.ChangeName(name);
                    Console.WriteLine("Your name successfully changed!!\n");
                    Console.ReadKey();
                }
                if (selectedItem == 3)
                {
                    Console.Write("Enter new phone number: ");
                    string phoneNumber = Console.ReadLine();
                    this._normalUser.ChangeNumber(phoneNumber);
                    Console.WriteLine("Your phone number successfully changed!!\n");
                    Console.ReadKey();
                }
                if (selectedItem == 4)
                {
                    Console.Write("Enter new date of birth: ");
                    string dateOfBirth = Console.ReadLine();
                    this._normalUser.ChangeBirthDate(dateOfBirth);
                    Console.WriteLine("Your date of birth successfully changed!!\n");
                    Console.ReadKey();
                }
                if (selectedItem == 5)
                {
                    Console.Write("Enter new place of work: ");
                    string placeOfWork = Console.ReadLine();
                    this._normalUser.ChangePlaceOfWork(placeOfWork);
                    Console.WriteLine("Your place of work successfully changed!!\n");
                    Console.ReadKey();
                }
                if (selectedItem == 6)
                {
                    Console.Write("Enter new password: ");
                    string password = Console.ReadLine();
                    this._normalUser.ChangePassword(password);
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
            Admin admin = new Admin(this);
            _user = admin;
            Console.Clear();
            Console.WriteLine("Hello, " + _user.login + "! This is user Menu");
            int selectedMenu = -1;
            const int numberMenuItems = 5;
            while (selectedMenu != numberMenuItems)
            {
                Console.WriteLine("0 - to add new test");
                Console.WriteLine("1 - to add new lecture");
                Console.WriteLine("2 - to log out from account");
                Console.Write("Enter your choice: ");
                selectedMenu = SafeReadNumberInRange(0, numberMenuItems);
                if (selectedMenu == 0)
                {
                    Console.Clear();
                    new TestCreator().CreateTestInConsole();
                }
                if (selectedMenu == 1)
                {
                    Console.Clear();
                    Console.Write("Enter a title of the lecture:");
                    string title = Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Enter a data of the lecture (to end lecture filling use 'end'):");
                    List<string> data = new List<string>();
                    bool stop = false;
                    while (!stop)
                    {
                        string item = Console.ReadLine();
                        if (item.Equals("end"))
                        {
                            stop = true;
                            break;
                        }
                        data.Add(item);
                    }
                    admin.AddLecture(title, data);

                }

                if (selectedMenu == 2)
                {
                    Console.Clear();
                    Console.WriteLine($"See you soon, {admin.login}");
                    StartWork();
                }
            }
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
            Regex email_rgx = new Regex(@"^\w+@[a-z]+(.[a-z])*$");
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
