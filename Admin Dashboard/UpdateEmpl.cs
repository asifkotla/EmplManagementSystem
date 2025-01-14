using EmplManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmplManagementSystem.Utility1;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EmplManagementSystem.Admin_Dashboard
{
    internal class UpdateEmpl
    {
        string name1 = "";
        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();
        public void UpdateEmployee()
        {
            Utility.Heading1();

            try
            {
                Console.Clear();
                Console.WriteLine("Enter Employee Id For Update: ");
                int empid = int.Parse(Console.ReadLine());

                var emp = dbo.Employees.FirstOrDefault(x => x.empId == empid);
                name1=emp.empName;
                Console.WriteLine("\n1.Update Name\n2.Update Salary Package\n3.Change Department\n4.AssingManager\n5.Back");
                Console.WriteLine("Enter Your Choice");
                int s = int.Parse(Console.ReadLine());

                switch (s)
                {
                    case 1:
                        Console.WriteLine("Enter Name To Be Update");
                        string nm = Console.ReadLine();
                        emp.empName = nm;
                        int c = dbo.SaveChanges();
                        if (c > 0)
                        {
                            
                            Utility.DisplaySuccessMessage(" ++ Updated ++ ");
                        }
                        else
                        {
                            
                            Utility.DisplayErrorMessage("Something Went Wrong Please try Again");
                            UpdateEmployee();
                        }
                        break;

                    case 2:
                        Console.WriteLine("Enter Employee Salary IN LPA To Update :");
                        float sal = float.Parse(Console.ReadLine());
                        decimal salary = (decimal)sal * 100000 / 12;

                        emp.empSalary = salary;
                        int q = dbo.SaveChanges();
                        if (q > 0)
                        {
                            Utility.DisplaySuccessMessage(" ++ Updated ++ ");
                        }
                        else
                        {
                            Utility.DisplayErrorMessage("Something Went Wrong Please try Again");
                            UpdateEmployee();
                        }

                        break;

                    case 3:
                        Console.WriteLine("Enter Department ID To be Updated:");
                        int dept = int.Parse(Console.ReadLine());
                        emp.deptId = dept;
                        int e = dbo.SaveChanges();
                        if (e > 0)
                        {
                            Utility.DisplaySuccessMessage(" ++ Updated ++ ");
                        }
                        else
                        {
                            Utility.DisplayErrorMessage("Something Went Wrong Please try Again");
                            UpdateEmployee();
                        }

                        break;

                    case 4:
                        Console.WriteLine("Enter Manager ID To be Updated");
                        int? mang;
                        string managerInput = Console.ReadLine();

                        // Handle nullable manager ID
                        if (string.IsNullOrWhiteSpace(managerInput))
                        {
                            mang = null;
                        }
                        else
                        {
                            mang = int.Parse(managerInput);
                        }
                        emp.managerId = mang;
                        int r = dbo.SaveChanges();

                        if (r > 0)
                        {
                            Utility.DisplaySuccessMessage(" ++ Updated ++ ");
                        }
                        else
                        {
                            Utility.DisplayErrorMessage("Something Went Wrong Please try Again");
                            UpdateEmployee();
                        }
                        break;

                    case 5:
                        Admin admin = new Admin();
                        admin.HandleMainmenu(name1);
                        break;

                    default:
                        Utility.DisplayErrorMessage("Wrong Choice !!");
                        UpdateEmployee();
                        break;
                }
            }
            catch (FormatException fe)
            {

                Utility.DisplayErrorMessage($"Invalid input format: {fe.Message}");
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Utility.DisplayErrorMessage($"An unexpected error occurred: {ex.Message}");
                Console.ReadKey();

            }
            finally
            {
                Admin admin = new Admin();
                admin.HandleMainmenu(name1); // Navigate back to the main menu
            }
        }
    }
}
