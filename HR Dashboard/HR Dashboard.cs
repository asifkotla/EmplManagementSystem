using EmplManagementSystem.Admin_Dashboard;
using EmplManagementSystem.Interface;
using EmplManagementSystem.Model;
using EmplManagementSystem.Utility1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using EmplManagementSystem.SMTP;
using System.Runtime.Serialization.Json;

namespace EmplManagementSystem.HR_Dashboard
{
    internal class HR : IDashBoard
    {
        int choice;
        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();
        public void DisplayMenu()
        {
            Console.WriteLine(" * * * HR Dashboard * * * ");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. View/Search Employees Filer By Joining Date ");
            Console.WriteLine("3. Monitor Performance");
            Console.WriteLine("4. Payroll Processing");
            Console.WriteLine("5. Salary Details");
            Console.WriteLine("6. Generate Reports");
            Console.WriteLine("7. For More...");
        }

        public void HandleMainmenu(string Username)
        {
            Console.Clear();
            DisplayMenu();
            Console.Write("\nEnter Your Choice ");
            int n = int.Parse(Console.ReadLine());
            switch (n)
            {
                case 1:
                    ADDEMPL add = new ADDEMPL();
                    add.AddEmployee();

                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("1.All Employees\n2.Search\n3.Main Menu");
                    choice = int.Parse(Console.ReadLine());
                    if (choice == 1)
                    {
                        var empl = dbo.dtlForHRs.ToList();
                        Console.WriteLine("     * - * - * Employee Details * - * - *         ");
                        foreach (var item in empl)
                        {
                            Console.WriteLine($"Employee Name :{item.Employee_Name}\nEmail :{item.Email}\nDepartment :{item.Department}\nContact No :{item.Contact}\nSalary :{item.EmpSalary}");
                            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");

                        }
                        Console.ReadLine();
                        HandleMainmenu(Username);
                    }
                    else if (choice == 2)
                    {
                        Console.Clear();
                        Search search = new Search();
                        Console.WriteLine("1.Search By Id\n2.Search By Name\n3.Filter By Joining Date");
                        choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine("Enter Employee Id");
                                int id = int.Parse(Console.ReadLine());
                                search.SearchbyId(id);
                                Console.ReadLine();
                                HandleMainmenu(Username);
                                break;
                            case 2:
                                Console.WriteLine("Enter Employee Name");
                                string nm = Console.ReadLine();
                                search.SearchbyName(nm);
                                Console.ReadLine();
                                HandleMainmenu(Username);
                                break;
                            case 3:
                                Console.WriteLine("Enter Date To Filter eg:-('YYYY-MM-DD') ");
                                string date = Console.ReadLine();
                                search.SearchByJoiningDate(DateTime.Parse(date));
                                Console.ReadLine();
                                HandleMainmenu(Username);
                                break;
                        }
                    }
                    else if (choice == 3)
                    {
                        HandleMainmenu(Username);
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("Invalid choice");
                        HandleMainmenu(Username);
                    }
                    break;
                case 3:
                    Console.WriteLine("* * * Employees Performance By department And Status * * *");
                    Console.WriteLine("Enter Department : Development || Marketing || HR || Testing || Tech Support");
                    string dept = Console.ReadLine();
                    Console.WriteLine("Enter Status : 'Pending' || 'Completed' || 'InProgress'");
                    string Status = Console.ReadLine();
                    var obj = dbo.Pr_GetEmployeePerformanceReportByDepartment(dept, Status).ToList();
                    foreach (var item in obj)
                    {
                        Console.WriteLine($"Employee Id :{item.empId}\nEmployee Name :{item.empName}\nTask {Status} :{item.Tasks}");
                        Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - -");
                    }
                    Console.ReadLine();
                    HandleMainmenu(Username);
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("1.Generate Payroll For Specific Employee\n2.Payroll For All Employees\n3.Main Menu");
                    choice = int.Parse(Console.ReadLine());
                    if (choice == 1)
                    {
                        Console.Write("Enter Employee Id : ");
                        int empid = int.Parse(Console.ReadLine());
                        var emp1 = dbo.UserInfoes.FirstOrDefault(x => x.Employee.empId == empid);
                        if (emp1 != null)
                        {

                            Console.WriteLine($"Employee Id : {emp1.empId}\nEmployee Name :{emp1.Employee.empName}");
                            PayrollProcessing payrollProcessing = new PayrollProcessing();
                            var salary = emp1.Employee.empSalary;
                            var task = emp1.Employee.Tasks.Count(x => x.Status == "Completed");
                            payrollProcessing.CalculateNetSalary((decimal)salary, task);
                            payrollProcessing.DisplaySalaryBreakdown((decimal)salary, task);

                            Console.ReadKey();
                            HandleMainmenu(Username);

                        }
                        else
                        {
                            Utility.DisplayErrorMessage("Employee Not Found");
                            HandleMainmenu(Username);
                        }
                    }
                    else if (choice == 2)
                    {
                        var allemp = dbo.UserInfoes.ToList();
                        if (allemp != null)
                        {


                            PayrollProcessing payrollProcessing = new PayrollProcessing();
                            Console.WriteLine("\n* * * * * Payroll For All Employees * * * * * \n ");

                            foreach (var emp1 in allemp)
                            {
                                Console.WriteLine($"Employee Id : {emp1.empId}\nEmployee Name :{emp1.Employee.empName}");
                                var salary = emp1.Employee.empSalary;
                                var task = emp1.Employee.Tasks.Count(x => x.Status == "Completed");
                                payrollProcessing.CalculateNetSalary((decimal)salary, task);
                                payrollProcessing.DisplaySalaryBreakdown((decimal)salary, task);
                                Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - ");
                            }
                            Console.ReadKey();
                            HandleMainmenu(Username);

                        }
                        else
                        {
                            Utility.DisplayErrorMessage("Employee Not Found");
                            HandleMainmenu(Username);
                        }
                    }
                    else if (choice == 3)
                    {
                        HandleMainmenu(Username);
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("Invalid Input Please Try Again");
                        HandleMainmenu(Username);
                    }
                    break;

                case 5:
                    var emp2 = dbo.UserInfoes.FirstOrDefault(x => x.userName == Username);
                    if (emp2 != null)
                    {


                        PayrollProcessing payrollProcessing = new PayrollProcessing();
                        var salary = emp2.Employee.empSalary;
                        var task = emp2.Employee.Tasks.Count(x => x.Status == "Completed");
                        payrollProcessing.CalculateNetSalary((decimal)salary, task);
                        payrollProcessing.DisplaySalaryBreakdown((decimal)salary, task);

                        Console.ReadKey();
                        HandleMainmenu(Username);
                    }
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("* * * Generate Reports * * *");
                    Console.WriteLine("1.Top Performer\n" +
                                      "2.Department Wise Budget \n" +
                                      "3.Deparment Wise Pending Task\n" +
                                      "4.Resgined Employees With There Contribution\n" +
                                      "5.Department Wise Headcount\n" +
                                      "6.Back");
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            var obj3 = dbo.Pr_TopPerformers().ToList();
                            Console.WriteLine("\n* * * * * Top Performer * * * * *\n");
                            if (obj3 != null)
                            {

                                foreach (var item in obj3)
                                {
                                    Console.WriteLine($"Employee Id :{item.empId}\nEmployee Name :{item.Employee_Name}\nDepartment :{item.Department}\nTask Completed :{item.Completed_Tasks}");
                                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - ");
                                }
                                Console.ReadLine();
                                HandleMainmenu(Username);
                            }
                            else
                            {
                                Utility.DisplayErrorMessage("Something Went Wrong Plase Try Again");
                                HandleMainmenu(Username);
                            }

                            break;
                        case 2:
                            var obj4 = dbo.Pr_DepartmentSalaryBudget().ToList();
                            Console.WriteLine("\n* * * * * Department Wise Budget * * * * *\n");
                            if (obj4 != null)
                            {

                                foreach (var item in obj4)
                                {
                                    Console.WriteLine($"Department :{item.Department}\nTotal Employees :{item.Total_Employees}\nTotal Salary :{item.Total_Salary}\nHighest Salary :{item.Highest_Salary}\nLowest Salary :{item.Lowest_Salary}");
                                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - ");
                                }
                                Console.ReadLine();
                                HandleMainmenu(Username);
                            }
                            else
                            {
                                Utility.DisplayErrorMessage("Something Went Wrong Plase Try Again");
                                HandleMainmenu(Username);
                            }
                            break;
                        case 3:
                            var obj5 = dbo.Pr_DepartmentPendingTasks().ToList();
                            Console.WriteLine("\n* * * * * Employee Pending Task * * * * *\n");
                            if (obj5 != null)
                            {

                                foreach (var item in obj5)
                                {
                                    Console.WriteLine($"Employee :{item.Employee_Name}\nTotal Pending Task :{item.Total_Pending_Tasks}\nDepartment :{item.Department}");
                                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - ");
                                }
                                Console.ReadLine();
                                HandleMainmenu(Username);
                            }
                            else
                            {
                                Utility.DisplayErrorMessage("Something Went Wrong Plase Try Again");
                                HandleMainmenu(Username);
                            }
                            break;
                        case 4:
                            var obj6 = dbo.Pr_Resigned().ToList();
                            Console.WriteLine("\n* * * * * Resigned Employees * * * * *\n");
                            if (obj6 != null)
                            {

                                foreach (var item in obj6)
                                {
                                    Console.WriteLine($"Employee Id :{item.empId}\nEmployee Name :{item.Employee_Name}\nTotal Pending Task :{item.Total_Tasks_Completed}");
                                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - ");
                                }
                                Console.ReadLine();
                                HandleMainmenu(Username);
                            }
                            else
                            {
                                Utility.DisplayErrorMessage("Something Went Wrong Plase Try Again");
                                HandleMainmenu(Username);
                            }
                            break;
                        case 5:
                            var headcount = dbo.Pr_DepartmentHeadCount1().ToList();
                            if (headcount != null)
                            {
                                Console.WriteLine("\n* * * Department Wise Headcount * * *\n");
                                foreach (var item in headcount)
                                {
                                    Console.WriteLine($"Department Name :{item.Department_Name}\nTotal Employee :{item.Total_Employees}");
                                    Console.WriteLine("======================================");
                                }
                                Console.ReadLine();
                                HandleMainmenu(Username);
                            }
                            else
                            {
                                Utility.DisplayErrorMessage("Something Gone Wrong Plaese Try Again");
                                HandleMainmenu(Username);
                            }
                            break;
                        case 6:
                            HandleMainmenu(Username);
                            break;
                        default:
                            Utility.DisplayErrorMessage("Invalid Choice");
                            HandleMainmenu(Username);
                        break;
                    }


                break;
                    //default:
                    //    Utility.DisplayErrorMessage("Invaild Choice");
                    //    HandleMainmenu(Username);
                    //    break;

            }

        }
    }
}
    

