using EmplManagementSystem.Model;
using System;
using EmplManagementSystem.Utility1;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using EmplManagementSystem.EmployeeLogin;
using EmplManagementSystem.ManagerDashboard;
using EmplManagementSystem.HR_Dashboard;
using EmplManagementSystem.Admin_Dashboard;

namespace EmplManagementSystem.SignUp
{
    public class Signup
    {
        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();
        string name = "";
        
        public bool SignUp(int empid)
        {
            try
            {
                Utility.Heading1();

                var emp = dbo.UserInfoes.FirstOrDefault(x => x.empId == empid);
                Console.WriteLine("Enter New username : ");
                string name = Console.ReadLine();
                Console.WriteLine("Mobile Number :");
                string mob = Console.ReadLine();
                //Console.WriteLine("Email :");
                //string email = Console.ReadLine();

                //if (!Utility.IsValidEmail(email))
                //{
                  
                //    Utility.DisplayErrorMessage("Invalid email format.");
                   
                //    return false;
                //}

                Console.WriteLine(" Enter Address :");
                string add = Console.ReadLine();
                Console.WriteLine("Create New Password :");
                string pass = Console.ReadLine();

                if (!Utility.IsValidPassword(pass))
                {
                    
                    Utility.DisplayErrorMessage("Password must be at least 8 characters long and include one uppercase letter, one lowercase letter, one number, and one special character.");
                    
                    return false;
                }

                //Console.WriteLine("Select Your Role:\n1. Admin\n2. HR\n3. Manager\n4. Employee");
                //if (!int.TryParse(Console.ReadLine(), out int role) || role < 1 || role > 4)
                //{
                //    Utility.DisplayErrorMessage("Wrong Choice. Please select a valid role.");
                //    return false;
                //}
                Console.Clear();
                


                var existinguser = dbo.UserInfoes.FirstOrDefault(x => x.userName == name );
                if (existinguser != null)
                {
                    //if (existinguser.Email == email)
                    //{
                    //    Utility.DisplayErrorMessage("This Email is already in use. Please choose another email.");
                    //    SignUp(username);
                    //}
                    if (existinguser.userName == name)
                    {
                        Utility.DisplayErrorMessage("This Username is already taken. Please choose another username.");
                        SignUp(empid);
    
                    }
                   
                    return false;
                }

                byte[] hashedPassword = Utility.HashPassword(pass);

                //UserInfo lg = new UserInfo();
                //lg.userName = name;
                ////lg.empId = empid;
                //    lg.Mobile = mob;
                //    //lg.Email = email;
                //    lg.address = add;
                //    lg.password = hashedPassword;
                //    //lg.roleId = role;
                //dbo.UserInfoes.Add(lg);
                emp.userName = name;
                emp.password = hashedPassword;
                emp.Mobile=mob;
                emp.address = add;
                int n = dbo.SaveChanges();
                if (n > 0)
                {
                    Utility.DisplaySuccessMessage("Registration successfull.");
                    return true;
                }
                else
                {
                    Utility.DisplayErrorMessage("Registration Unsuccessfull.");
                    return false;
                }
            }
            catch (Exception ex)
            {

                Utility.DisplayErrorMessage($"Error during sign-up: {ex.Message}");
                return false;
            }
        }

        public bool IsLogin()
        {
            int count = 0;
            const int MaxCount = 3;
            try
            {
                while (count < MaxCount)
                {



                    Console.Write("Enter User Name : ");
                    string uname = Console.ReadLine();
                    name = uname;
                    Console.Write("Enter Password : ");
                    string pass = Console.ReadLine();
                    Console.Write("Enter Role id : ");
                    int r = int.Parse(Console.ReadLine());

                    // Fetch the user from the database
                    var user = dbo.UserInfoes.FirstOrDefault(x => x.userName == uname && x.roleId == r);

                    if (user != null)
                    {
                        byte[] hashedInputPassword = Utility.HashPassword(pass);

                        // Compare passwords in memory after loading the user from the database
                        if (user.password != null && hashedInputPassword.SequenceEqual(user.password))
                        {
                            Console.Clear();

                            Utility.DisplaySuccessMessage("- - Login Successfull - - ");
                            if (hashedInputPassword.SequenceEqual(Utility.HashPassword("EMSpass@1234")))
                            {
                                SignUp(user.empId);
                            }
                            else
                            {
                                switch (user.roleId)
                                {
                                    case 1:
                                        Console.WriteLine("\nWelcome" + " " + user.userName + "...\n");
                                        Admin admin = new Admin();
                                        name = user.userName;
                                        admin.HandleMainmenu(name);
                                        break;

                                    case 2:

                                        Console.WriteLine("\nWelcome" + " " + user.userName + "...\n");
                                        HR hr = new HR();
                                        name = user.userName;
                                        hr.HandleMainmenu(name);

                                        break;
                                    case 3:
                                        Console.WriteLine("\nWelcome" + " " + user.userName + "...\n");
                                        Manager manager = new Manager();
                                        name = user.userName;
                                        manager.HandleMainmenu(name);


                                        break;
                                    case 4:
                                        Console.WriteLine("\nWelcome" + " " + user.userName + "...\n");
                                        EmployeeDashboard employeeDashboard = new EmployeeDashboard();
                                        name = user.userName;
                                        employeeDashboard.HandleMainmenu(name);
                                        break;

                                    default:
                                        Utility.DisplayErrorMessage("Invalid role assigned to user.");
                                        break;
                                }
                                return true;
                            }
                        }
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("Invalid username or password or Role Id.");

                    }
                    count++;
                    Console.WriteLine($"Attempts remaining: {MaxCount - count}");
                    
                }
                Console.Clear();
                Console.WriteLine("\nMaximum login attempts reached.");
                Console.WriteLine("1. Forget Password\n2. Back");
                int choice=int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        var obj = dbo.UserInfoes.FirstOrDefault(x => x.userName == name);
                        if (obj != null)
                        {
                            forgetPassword forgetPassword=new forgetPassword();
                            forgetPassword.frgtpass(obj.userName);
                        }
                        else
                        {
                            Utility.DisplayErrorMessage("User not Found");
                            IsLogin();
                        }
                        break;
                    case 2:
                        IsLogin();
                        break;
                    default:
                        Utility.DisplayErrorMessage("Invalid Choice ");
                        IsLogin();
                        break;
                }

                
                return false;



            }
            catch (Exception ex)
            {

                Utility.DisplayErrorMessage($"Error during login: {ex.Message}");
 
                return false;
            }
        }
           
    }
}

