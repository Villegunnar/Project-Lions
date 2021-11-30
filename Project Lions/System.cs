using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Lions
{
    public class System
    {

        public static List<User> Allusers = new List<User>();


        public static bool UserPassCheck(string username, string password)
        {
            if (Allusers.Any(item => username == item.Username && password == item.Password))
            {
                foreach (User item in Allusers)
                {
                    if (item.LockedOut)
                    {
                        Console.WriteLine("Användaren är utelåst!");
                        return false;
                    }
                    else
                    {

                        Console.WriteLine("Inloggning lyckades!");
                        return true;
                    }
                }
                if (Allusers.Any(item => username == item.Username && password != item.Password))
                {
                    foreach (User item in Allusers)
                    {
                        if (item.LockedOut)
                        {
                            Console.WriteLine("Du är utelåst!");
                            return false;
                        }
                        else if (item.Username == username)
                        {
                            item.LoginCounter++;
                            Console.WriteLine("Fel lösen men rätt användare");
                            return false;
                        }
                    }
                    return false;
                }
                Console.WriteLine("Fel Lösen och användare");
                return false;
            }
            return false;
        }
            public static void LogIn()
            {
                while (true)
                {
                    Console.WriteLine("Skriv in användarnamn: ");
                    string usernameInput = Console.ReadLine();
                    Console.WriteLine("Skriv in lösenord: ");
                    string passwordInput = Console.ReadLine();
                    if (UserPassCheck(usernameInput, passwordInput))
                    {
                        //User newUsers = new User(usernameInput, passwordInput, 0);
                        //System.Allusers.Add(newUsers);
                        foreach (var item in System.Allusers)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
            }
        }
    }
