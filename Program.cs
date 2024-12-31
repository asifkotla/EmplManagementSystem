using EmplManagementSystem.Model;
using EmplManagementSystem.SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmplManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            bool isLogedIn = false;
            int ch;
            do
            {
                if (isLogedIn == false)
                {
                    Console.WriteLine("1:Login");
                    Console.WriteLine("2:SignUp");
                }
                else
                {
                    Console.WriteLine("8:Sign out");
                }
                Console.WriteLine("9:Change Password ");
                Console.WriteLine("10:Exit");
                Console.WriteLine("Enter Your choice");
                ch = int.Parse(Console.ReadLine());
                Console.Clear();
                Signup lg = new Signup();
                switch (ch)
                {
                    
                    case 1:
                        if (isLogedIn == true)
                        {
                           
                            Console.WriteLine("already logged in. please sign out..");
                        }
                        else
                        {

                            bool result = lg.IsLogin();
                            if (result)
                            {
                                isLogedIn = true;
                               
                            }
                        }
                        break;

                    case 2:
                        bool r = lg.SignUp();
                        break;

                    case 8:
                        if (isLogedIn == true)
                        {
                            isLogedIn = false;
                        }
                        else
                        {
                            Console.WriteLine("Not logged in yet!!!");
                        }
                        break;
                    case 9:
                        Update update=new Update();
                        update.ChangePassword();
                        break;
                    case 10:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }

            } while (true);
          
        }
    }
}
