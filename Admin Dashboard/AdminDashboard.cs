using EmplManagementSystem.Interface;
using EmplManagementSystem.Model;
using EmplManagementSystem.Utility1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using EmplManagementSystem.HR_Dashboard;

namespace EmplManagementSystem.Admin_Dashboard
{
    internal class Admin : IDashBoard
    {
        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();
        public void DisplayMenu()
        {
            Console.WriteLine(" * * * Admin Dashboard * * * ");
            Console.WriteLine("1. Update Employee");
            Console.WriteLine("2. View/Search Employees ");
            Console.WriteLine("3. Monitor Performance");
            Console.WriteLine("4. Salary Management");
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
                    UpdateEmpl add = new UpdateEmpl();
                    add.UpdateEmployee();
                    break;
                case 2:

                    Console.WriteLine("1.View All Employess \n2.Search By Id/Name");  
                    Console.WriteLine("Enter Choice ");
                    int q=int.Parse(Console.ReadLine());
                    if (q == 1)
                    {
                        var obj1 = dbo.dtlForAdmins.ToList();
                        if (obj1 != null && obj1.Count > 0)
                        {
                            Console.WriteLine("Employee Details:");
                            Console.WriteLine("--------------------------------------------------");
                            foreach (var admin in obj1)
                            {
                                Console.WriteLine($"Name    : {admin.EmpName}");
                                Console.WriteLine($"Role    : {admin.Role}");
                                Console.WriteLine($"Email   : {admin.Email}");
                                Console.WriteLine($"Contact : {admin.Contact}");
                                Console.WriteLine($"Address : {admin.Address}");
                                Console.WriteLine("--------------------------------------------------");
                            }
                        }
                    }
                    else if (q == 2)
                    {
                        Console.WriteLine("1.Search By Id\n2.Search By Name");
                        var w = int.Parse(Console.ReadLine());
                        if (w == 1)
                        {
                            Console.WriteLine("Enter Employee Id ");
                            int id = int.Parse(Console.ReadLine());
                            SearchbyIdName search = new SearchbyIdName();
                            search.SearchbyId(id);
                        }
                        else if (w == 2)
                        {
                            Console.WriteLine("Enter Employee Name");
                            string name = Console.ReadLine();
                            SearchbyIdName search = new SearchbyIdName();
                            search.SearchbyName(name);
                        }
                        else
                        {
                            Utility.DisplayErrorMessage("Invalid choice !!!");
                            HandleMainmenu(Username);
                        }
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("Invalid Choice !!");
                        HandleMainmenu(Username);

                    }
                    Console.WriteLine("Press 0.Back ");
                    int b = int.Parse(Console.ReadLine());
                    if(b==0)
                    {
                        Console.Clear();
                        HandleMainmenu(Username);
                    }
                break;
                case 3:
                    Console.WriteLine("Enter Department :");
                    string deptnm = Console.ReadLine();
                    Console.WriteLine("Status : Pending | Completed | InProgress ");
                    string status = Console.ReadLine();
                    var obj = dbo.GetEmployeePerformanceReportByDepartment(deptnm, status).ToList();
                    if (obj != null)
                    {
                        foreach (var obj1 in obj)
                        {
                            Console.WriteLine($"Employee Id :{obj1.empId}\nEmployee Name :{obj1.empName}\nTask {status} :{obj1.TasksCompleted}");
                            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - -");
                        } 
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("Something Gone Wrong");
                        HandleMainmenu(Username);
                    }
                break;
                case 4:
                    Console.WriteLine("1.Appraisals\n2.Back");
                    int a=int.Parse(Console.ReadLine());
                    if (a == 1)
                    {
                        Console.WriteLine("Enter Employee Id to Add Appraisal");
                        int empid1 = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Precentage Of Appraisal");
                        float percentage = float.Parse(Console.ReadLine());
                        var emp = dbo.Employees.FirstOrDefault(x => x.empId == empid1);
                        if (emp != null)
                        {


                            emp.empSalary = emp.empSalary + ((emp.empSalary * (decimal)percentage) / 100);
                            int final = dbo.SaveChanges();
                            if (final > 0)
                            {
                                Utility.DisplaySuccessMessage("Salary Updated Succssfully");
                                HandleMainmenu(Username);
                            }
                            else
                            {
                                Utility.DisplayErrorMessage("Something Went Wrong, Please Try Again!");
                                HandleMainmenu(Username);
                            }
                        }

                    }
                    else if (a == 0)
                    {
                        HandleMainmenu(Username);
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("Wrong Choise!!");
                        HandleMainmenu(Username);
                    }
                    break;
                case 5:
                    var emp1 = dbo.UserInfoes.FirstOrDefault(x => x.userName == Username);
                    if(emp1!=null)
                    {
                      

                        PayrollProcessing payrollProcessing = new PayrollProcessing();
                        var salary = emp1.Employee.empSalary;
                        var task = emp1.Employee.Tasks.Count(x=>x.Status=="Completed");
                        payrollProcessing.CalculateNetSalary((decimal)salary, task);
                        payrollProcessing.DisplaySalaryBreakdown((decimal)salary, task);

                        Console.ReadKey();
                        HandleMainmenu(Username);
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("Something Went Wrong please Try Again");
                        
                        HandleMainmenu(Username);
                    }
                break;
                case 6:
                    Console.WriteLine("* * * Generate Reports * * *");
                    Console.WriteLine("1.Department Headcount\n" +
                                      "2.Employee Report By Department\n" +
                                      "3.Department Salary Distribution\n" +
                                      "4.Salary Report\n" +
                                      "5.Employee Role Distribution\n" +
                                      "6.Back");
                    Console.Write("Enter Your Choice :");
                    int choice=int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            var headcount=dbo.Pr_DepartmentHeadCount1().ToList();
                            if (headcount!=null)
                            {
                                    Console.WriteLine("\n* * * Department Wise Headcount * * *\n");
                                foreach(var item in headcount)
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
                        case 2:
                            Console.WriteLine("Department Name : Development || Marketing || HR || Testing || Tech Support");
                            string dept=Console.ReadLine();
                            var emplreport = dbo.Pr_EmplDetailsDeptWise1(dept).ToList();
                            if (emplreport != null)
                            {
                                Console.WriteLine($"\n* * *{dept} Department Employee Details * * *\n");
                                foreach (var item in emplreport)
                                {
                                    Console.WriteLine($"Employee Name :{item.Employee_Name}\nE-mail :{item.Email}");
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
                        case 3:
                            var saldis = dbo.Pr_DepartmentSalaryDistribution1().ToList();
                            if (saldis != null)
                            {
                                Console.WriteLine("\n* * * Department Wise Salary Distribuition * * *\n");
                                foreach (var item in saldis)
                                {
                                    Console.WriteLine($"Department Name :{item.Department}\nTotal Employee :{item.Total_Employees}\nTotal Salary :{item.Total_Salary_Distribution}\nAverage Salary :{item.Average_Salary}");
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
                        case 4:
                            var salreport = dbo.Pr_GetSalaryReport().ToList();
                            if (salreport != null)
                            {
                                Console.WriteLine("\n* * * Salary Report * * *\n");
                                foreach (var item in salreport)
                                {
                                    Console.WriteLine($"Employee ID:{item.empId}\nEmployee Name :{item.Name}\nDepartment :{item.Department}\nSalaray :{item.Salary}");
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
                        case 5:
                            var role = dbo.Pr_EmployeeRoleDistribution().ToList();
                            if (role != null)
                            {
                                Console.WriteLine("* * * Role Distribution * * *\n");
                                foreach (var item in role)
                                {
                                    Console.WriteLine($"Role Name :{item.Role}\nDepartment Name :{item.Department}\nTotal Employees :{item.Total_Employees}");
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
                        default: HandleMainmenu(Username);
                            break;
                    }
                    break;
                    default:Utility.DisplayErrorMessage("Invalid Choise");
                    HandleMainmenu(Username);
                    break;
                 
            }

        }
    }
}
