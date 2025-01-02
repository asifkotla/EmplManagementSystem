using EmplManagementSystem.HR_Dashboard;
using EmplManagementSystem.Interface;
using EmplManagementSystem.Model;
using EmplManagementSystem.Utility1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Task = EmplManagementSystem.Model.Task;


namespace EmplManagementSystem.ManagerDashboard
{
    public class Manager: IDashBoard
    {
          
        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();
        public void DisplayMenu()
        {
            Console.WriteLine(" * * * Manager Dashboard * * * ");
            Console.WriteLine("1. Assign Tasks");
            Console.WriteLine("2. View/Search Employees ");
            Console.WriteLine("3. Monitor Performance");
            Console.WriteLine("4. OverDue Tasks");
            Console.WriteLine("5. Salary Details");
            Console.WriteLine("6. Generate Reports");
            Console.WriteLine("7. For More...");
        }

        public void HandleMainmenu(string Username)
        {   
            Console.Clear();
            int choice;
            
            DisplayMenu();
            Console.Write("\nEnter Your Choice ");
            int n = int.Parse(Console.ReadLine());
            switch (n)
            {
                case 1:
                    Console.Write("Task Name: ");
                    string tasknm = Console.ReadLine();

                    Console.WriteLine("Task Description:");
                    string tdes = Console.ReadLine();

                    Console.WriteLine("Add Deadline To Task:");
                    DateTime date = DateTime.Parse(Console.ReadLine());

                    Console.WriteLine("Assign To EmpID:");
                    int empid = int.Parse(Console.ReadLine());

                    Task task = new Task()
                    {
                        taskName = tasknm,
                        Description = tdes,
                        deadLine = date,
                        empId = empid,
                        Status = "Pending"
                    };

                    try
                    {
                        dbo.Tasks.Add(task);
                        int q = dbo.SaveChanges();
                        if (q > 0)
                        {
                            Utility.DisplaySuccessMessage("Task Assigned Successfully");
                            HandleMainmenu(Username);
                        }
                        else
                        {
                            Utility.DisplayErrorMessage("Something Went Wrong, Please Try Again");
                            HandleMainmenu(Username);
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.DisplayErrorMessage($"Error: {ex.Message}");
                        HandleMainmenu(Username);
                    }

                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("1.Show All Employees\n2.Search\n3.Main Menu");
                    choice= int.Parse(Console.ReadLine());  
                    if(choice==1)
                    {
                        var obj1 = dbo.dtlForManagers.ToList();
                        if(obj1!=null)
                        {
                            Console.WriteLine("* * * * * Employee Details * * * * *");
                            foreach(var item in obj1)
                            {
                                Console.WriteLine($"Employee Name :{item.Employee_Name}\nDepartment :{item.Department}Email :{item.Email}\nContact :{item.Contact}");
                                Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - -");
                                
                            }
                            Console.ReadKey();
                            HandleMainmenu(Username);
                        }
                        else
                        {
                            Utility.DisplayErrorMessage("Something Gone Wrong Please Try Again ");
                            HandleMainmenu(Username);
                        }
                    }
                    else if(choice==2)
                    {
                        Console.Clear();
                        Console.WriteLine("1.Search By Id\n2.Search By Name\n3.Main Menu");
                        int choice1= int.Parse(Console.ReadLine());
                        if (choice1==1)
                        {
                            Searchmang search = new Searchmang();
                            Console.Write("Enter Employee Id : ");
                            int empid1=int.Parse(Console.ReadLine());
                            search.SearchbyId(empid1);
                            HandleMainmenu(Username);
                        }
                        else if (choice1==2)
                        {
                            Console.Write("Enter Employee Name :");
                            string name=Console.ReadLine();
                            Searchmang search1 = new Searchmang();
                            search1.SearchbyName(name);
                            HandleMainmenu(Username);
                        }
                        else if (choice1==3)
                        {
                            HandleMainmenu(Username);
                        }
                        else {
                            Utility.DisplayErrorMessage("Invalid Choice !!!");
                            HandleMainmenu(Username);
                        }

                    }
                    else if(choice==3)
                    {
                        HandleMainmenu(Username);
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("Invalid Choice !!!");
                        HandleMainmenu(Username);   
                    }
                    break;
                case 3:
                    
                    Console.WriteLine("Enter Department (Development || Marketing || HR || Testing || Tech Support):");
                    string deptnm = Console.ReadLine();
                    Console.WriteLine("Status : Pending | Completed | InProgress ");
                    string status = Console.ReadLine();
                    var obj2 = dbo.Pr_GetEmployeePerformanceReportByDepartment(deptnm, status).ToList();
                    if (obj2 != null)
                    {
                        foreach (var obj1 in obj2)
                        {
                            Console.WriteLine($"Employee Id :{obj1.empId}\nEmployee Name :{obj1.empName}\nTask {status} :{obj1.Tasks}");
                            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - -");
                        }
                        Console.ReadLine();
                        HandleMainmenu(Username);
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("Something Gone Wrong");
                        HandleMainmenu(Username);
                    }
                    break;
                case 4:
                    //DateTime filterDate = new DateTime(2025, 4, 7);
                    var obj4=dbo.Tasks.Where(x=>x.deadLine< DateTime.Now).ToList();
                    if(obj4.Any())
                    {
                        Console.WriteLine("\n* * * * * Overdue Tasks * * * * *\n");
                        foreach(var item in obj4)
                        {
                            var daysOverdue = (DateTime.Now - item.deadLine).Days;
                            Console.WriteLine($"task Id :{item.taskId}\nTask Name :{item.taskName}\nDeadline :{item.deadLine}\nTodays's Date :{DateTime.Now}\nDays Overdue :{daysOverdue}");
                            Console.WriteLine("------------------------------------------------------");
                            Console.ReadLine();
                            HandleMainmenu(Username);
                        }
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("SomeThing Went Wrong Please Try Again ");
                        HandleMainmenu(Username);
                    }
                    break;
                case 5:
                    var emp1 = dbo.UserInfoes.FirstOrDefault(x => x.userName == Username);
                    if (emp1 != null)
                    {


                        PayrollProcessing payrollProcessing = new PayrollProcessing();
                        var salary = emp1.Employee.empSalary;
                        var task1 = emp1.Employee.Tasks.Count(x => x.Status == "Completed");
                        payrollProcessing.CalculateNetSalary((decimal)salary, task1);
                        payrollProcessing.DisplaySalaryBreakdown((decimal)salary, task1);

                        Console.ReadKey();
                        HandleMainmenu(Username);
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("Something Went Wrong Please Try Again");
                        HandleMainmenu(Username);
                    }
                    break;
                   
                case 6:
                    Console.WriteLine("* * * Generate Reports * * *");
                    Console.WriteLine("1.EmployeeTask Report by Employee Id\n" +
                                      "2.Task Completion Status Report \n" +
                                      "3.Employee Performance Report\n" +
                                      "4.Pending Task\n" +
                                      "5.Top Performer\n" +
                                      "6.Back");
                    Console.Write("Enter Your Choice :");
                     choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Enter Employee Id :");
                            int empid1 = int.Parse(Console.ReadLine());
                            var headcount = dbo.Pr_GetEmployeeTaskReport(empid1).ToList();
                            if (headcount != null)
                            {
                                Console.WriteLine("\n* * * EmployeeTask Report * * *\n");
                                foreach (var item in headcount)
                                {
                                    Console.WriteLine($"Task Id :{item.taskId}\nTask Name :{item.taskName}\nDecription :{item.Description}\nStatus :{item.Status}\nDeadLine :{item.deadLine}");
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
                            Console.WriteLine("Department Name : Pending || InProgress || Completed");
                            string dept = Console.ReadLine();
                            var emplreport = dbo.Pr_TaskCompletionStatusReport(dept).ToList();
                            if (emplreport != null)
                            {
                                Console.WriteLine($"\n* * * Task  Completion  Status Report * * *\n");
                                foreach (var item in emplreport)
                                {
                                    Console.WriteLine($"Task Id :{item.taskId}\nTask Name :{item.taskName}\nEmployee Name :{item.AssignedTo}\nStatus :{item.Status}\nDeadLine :{item.deadLine}");
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
                            Console.WriteLine("Enter Department (Development || Marketing || HR || Testing || Tech Support):");
                            string dept1 = Console.ReadLine();
                            Console.WriteLine("Status : Pending | Completed | InProgress ");
                            string status1 = Console.ReadLine();
                            var saldis = dbo.Pr_GetEmployeePerformanceReportByDepartment(dept1,status1).ToList();
                            if (saldis != null)
                            {
                                Console.WriteLine("\n* * * Employee Performance Report * * *\n");
                                foreach (var item in saldis)
                                {
                                    Console.WriteLine($"Department Name :{item.empId}\nTotal Employee :{item.empName}\nTask Count :{item.Tasks}");
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
                            var pendtask = dbo.Pr_DepartmentPendingTasks().ToList();
                            if (pendtask != null)
                            {
                                Console.WriteLine("\n* * * Pending Task * * *\n");
                                foreach (var item in pendtask)
                                {
                                    Console.WriteLine($"Employee Name:{item.Employee_Name}\nDepartment :{item.Department}\nPending Task :{item.Total_Pending_Tasks}");
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
                        case 6:
                            HandleMainmenu(Username);
                            break;
                        default:
                            Utility.DisplayErrorMessage("Invalid Choice !!!");
                            HandleMainmenu(Username);
                            break;
                    }
                    break;
             
            }
        }
    }
}
