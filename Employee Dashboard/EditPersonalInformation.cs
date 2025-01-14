using EmplManagementSystem.EmployeeLogin;
using EmplManagementSystem.Model;
using EmplManagementSystem.Utility1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace EmplManagementSystem.Employee_Dashboard
{
    
    internal class EditPersonalInformation

    {
        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();
        int q;
        public void Edit(string name)
        {
            Utility.Heading1();

            string name1 = name;
            
            var obj = dbo.UserInfoes.FirstOrDefault(x => x.userName == name);
            if (obj != null)
            {
                Console.Clear();
               Console.WriteLine("Choose What You Want To Edit ");
                Console.WriteLine("1.Name\n2.Email\n3.Mobile Number\n4.User Name\n5.Address\n6.Main Menu");
                int n=int.Parse(Console.ReadLine());
                switch (n)
                {
                    case 1:
                        Console.Write("Enter Name :");
                        string chngname = Console.ReadLine();
                        obj.Employee.empName = chngname;
                        q = dbo.SaveChanges();
                        if (q > 0)
                        {
                            Utility.DisplaySuccessMessage("Name Changed Successfully");
                            Edit(name);
                            
                        }
                        else
                        {
                            Utility.DisplayErrorMessage("Something Went Wrong Please Try Agian");
                            Edit(name);
                        }
                        break;

                    case 2:
                        Console.Write("Enter Email  :");
                        string email = Console.ReadLine();
                        if (!Utility.IsValidEmail(email))
                        {

                            Utility.DisplayErrorMessage("Invalid email format.");
                            Edit(name);

                        }
                        var existinguser = dbo.UserInfoes.FirstOrDefault(x => x.Email == email);
                        if (existinguser != null)
                        {

                            Utility.DisplayErrorMessage("This Email is already in use. Please choose another email.");
                            Edit(name);

                        }
                        else
                        {
                            obj.Email = email;
                            q = dbo.SaveChanges();
                            if (q > 0)
                            {
                                Utility.DisplaySuccessMessage("Email Changed Successfully");
                                Edit(name);
                            }
                            else
                            {
                                Utility.DisplayErrorMessage("Something Went Wrong Please Try Agian");
                                Edit(name);
                            }
                        }

                        break;
                    case 3:
                        Console.Write("Enter Mobile Number :");
                        string mblno = Console.ReadLine();
                        obj.Mobile = mblno;
                        q = dbo.SaveChanges();
                        if (q > 0)
                        {
                            Utility.DisplaySuccessMessage("Mobile Number Changed Changed Successfully");
                            Edit(name);
                        }
                        else
                        {
                            Utility.DisplayErrorMessage("Something Went Wrong Please Try Agian");
                            Edit(name);
                        }
                        break;

                    case 4:
                        Console.Write("Enter User Name :");
                        string usernm = Console.ReadLine();
                        var existinguser1 = dbo.UserInfoes.FirstOrDefault(x => x.userName == usernm);
                        if (existinguser1 != null)
                        {

                            Utility.DisplayErrorMessage("This UserName is already in use. Please choose another email.");
                            Edit(name);

                        }
                        else
                        {
                            obj.userName = usernm;
                            q = dbo.SaveChanges();
                            if (q > 0)
                            {
                                Utility.DisplaySuccessMessage("User Name Changed Successfully");
                                Edit(name);
                            }
                            else
                            {
                                Utility.DisplayErrorMessage("Something Went Wrong Please Try Agian");
                                Edit(name);
                            }
                        }
                        break;
                    case 5:
                        Console.Write("Enter Address :");
                        string address = Console.ReadLine();
                        obj.address = address;
                         q = dbo.SaveChanges();
                        if (q > 0)
                        {
                            Utility.DisplaySuccessMessage("Address Changed Successfully");
                            Edit(name);
                        }
                        else
                        {
                            Utility.DisplayErrorMessage("Something Went Wrong Please Try Agian");
                            Edit(name);
                        }
                        break;
                    case 6:
                        EmployeeDashboard empl=new EmployeeDashboard();
                        Console.Clear();
                        empl.HandleMainmenu(name1);
                        break;
                    //default:
                    //    Utility.DisplayErrorMessage("Invaild Choice");
                    //    Edit(name);
                    //break;
                }
            }
            else
            {
                Utility.DisplayErrorMessage("Something Went Wrong Please Try Again");
                Edit(name);
            }
        }
    }
}
