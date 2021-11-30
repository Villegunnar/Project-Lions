using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Lions
{
    static class UserFactory
    {
        //HÄR LÄGGER VI EN ROLIG KOMMENTAR
        public static List<string> nums = new List<string>();
        public static List<string> uppercase = new List<string>();
        public static List<string> lowercase = new List<string>();
        public static List<string> notChar = new List<string>();
        static string[] createAccountMenu = { "Vänligen fyll i önskat användarnamn och lösenord:", " ", "ANVÄNDARNAMN: ", "LÖSENORD: ", "BEKRÄFTA LÖSENORD: " };
        static string nameNotVacant = "Det finns redan ett konto med det här användarnamnet.";
        static string passIsWeak = "Lösenordet är för svagt. Det måste innehålla stora och små bokstäver, minst en siffra och minst en specialsymbol.";
        static string passDontMatch = "Lösenordsfälten matchar inte. Försök igen.";
        static string passTooShort = "Lösenordet är för kort. Det måste vara minst 8 karaktärer långt.";
        static string passError;


        public static void Useless()
        {
            Console.WriteLine("öL");
        }
        static UserFactory()
        {
            uppercase.AddRange("A B C D E F G H I J K L M N O P Q R S T U V W X Y Z Å Ä Ö".Split(" "));
            lowercase.AddRange("A B C D E F G H I J K L M N O P Q R S T U V W X Y Z Å Ä Ö".ToLower().Split(" "));
            nums.AddRange("1 2 3 4 5 6 7 8 9 0".ToLower().Split(" "));
            notChar.AddRange(lowercase);
            notChar.AddRange(uppercase);
            notChar.AddRange(nums);
        }

        public static void CreateNewUser(System bank)
        {
            bool success = false;
            string usernameTry;
            string passwordTry;
            string passConfTry;
            bool fail = false;

            while (!success)
            {
                Console.Clear();
                foreach (string section in createAccountMenu)
                {
                    Console.WriteLine(section);
                }
                if (fail)
                {
                    Console.SetCursorPosition(0, 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(passError);
                    Console.ResetColor();
                }

                Console.SetCursorPosition(14, 2);
                usernameTry = Console.ReadLine().Trim();
                Console.SetCursorPosition(10, 3);
                passwordTry = Console.ReadLine();
                Console.SetCursorPosition(19, 4);
                passConfTry = Console.ReadLine();
                if (UsernameIsVacant(usernameTry, System.Allusers))
                {
                    if (passwordTry == passConfTry)
                    {
                        if (passwordTry.Length >= 8)
                        {
                            success = IsPassStrong(passwordTry);
                            if (!success)
                            {
                                fail = true;
                                passError = passIsWeak;
                            }
                            else
                            {
                                Console.Clear();
                                System.Allusers.Add(new User(usernameTry, passwordTry));
                                Console.WriteLine("Kontot {0} har skapats!", usernameTry);
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            fail = true;
                            passError = passTooShort;
                        }
                    }
                    else
                    {
                        fail = true;
                        passError = passDontMatch;
                    }
                }
                else
                {
                    fail = true;
                    passError = nameNotVacant;
                }
            }
        }
        public static bool IsPassStrong(string thisPassword)
        {
            bool upper = StringMatch(uppercase, thisPassword);
            bool lower = StringMatch(lowercase, thisPassword);
            bool num = StringMatch(nums, thisPassword);

            bool chara = false;
            bool match = false;
            foreach (char c in thisPassword)
            {
                string s = c.ToString();
                match = StringMatch(notChar, s);
                if (!match)
                {
                    chara = true;
                    break;
                }
                match = false;
            }

            if (upper && lower && num && chara)
            {
                return true;
            }
            return false;
        }
        public static bool StringMatch(List<string> s, string p)
        {
            foreach (string letter in s)
            {
                if (p.Contains(letter))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool UsernameIsVacant(string name, List<User> theseUsers)
        {
            foreach (User user in theseUsers)
            {
                if (user.Username.Equals(name))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
