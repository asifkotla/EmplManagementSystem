using EmplManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmplManagementSystem.Interface;
using EmplManagementSystem.Utility1;
using EmplManagementSystem.Employee_Dashboard;
using EmplManagementSystem.HR_Dashboard;

namespace EmplManagementSystem.EmployeeLogin
{
    internal class EmployeeDashboard:IDashBoard
    {
        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();
        int choice;
        public void DisplayMenu()
        {
            Console.WriteLine(" * * * Employee Dashboard * * * ");
            Console.WriteLine("1. View Personal Details");
            Console.WriteLine("2. Edit Personal Details");
            Console.WriteLine("3. View/Search Employees");
            Console.WriteLine("4. Assigned Tasks");
            Console.WriteLine("5. Update Task Status");
            Console.WriteLine("6. Salary Details");
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
                    var obj=dbo.UserInfoes.FirstOrDefault(x=>x.userName==Username);
                    if (obj != null)
                    {
                        Utility.DisplaySuccessMessage("* - * - * PERSONAL DETAILS * - * - *");
                        Console.WriteLine($"UserId    :{obj.userId}\tEmployee Id:{obj.Employee.empId}");
                        Console.WriteLine($"User Name :{obj.userName}");
                        Console.WriteLine($"Name      :{obj.Employee.empName}\n" +
                                          $"Mobile    :{obj.Mobile}\n" +
                                          $"Email     :{obj.Email}\n" +
                                          $"Address   :{obj.address}");
                        Console.WriteLine("Press Enter");
                        Console.ReadKey();
                        Console.Clear();
                        HandleMainmenu(Username);
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("Something Went Wrong Pleade Try Again ");
                        HandleMainmenu(Username);
                    }       
                break;
                case 2:
                    EditPersonalInformation edit = new EditPersonalInformation();
                    edit.Edit(Username);
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("1.All Employees\n2.Search By Name\n3.Back");
                     choice=int.Parse(Console.ReadLine());
                    if(choice == 1)
                    {
                        var empl=dbo.dtlForEmpls.ToList();
                        Console.WriteLine(" * * * All Employees Details * * * ");
                       
                        foreach (var item in empl)
                        {
                            Console.WriteLine("- - - - - - - - - - - - - - - - - -");
                            Console.WriteLine($"Name :{item.Employee_Name}\nDepartment :{item.Department}\nEmail :{item.Email}");
                            Console.WriteLine("- - - - - - - - - - - - - - - - - -");
                            
                        }
                        Console.ReadLine();
                        HandleMainmenu(Username);

                    }
                    else if(choice==2)
                    {
                        
                        Console.WriteLine("1.Search By Id ");
                        Console.WriteLine("2.Search By Name");
                        Console.WriteLine("3.Back");
                        choice=int.Parse(Console.ReadLine());
                        if(choice==1)
                        {
                            Console.WriteLine("Enter Employee ID ");
                            int empid=int.Parse(Console.ReadLine());
                            SearchbyIdName searchbyIdName = new SearchbyIdName();
                            searchbyIdName.SearchbyId(empid);
                            Console.ReadLine() ;
                            HandleMainmenu(Username);
                        }
                        else if(choice==2)
                        {
                            Console.WriteLine("Enter Employee Name ");
                            string name = Console.ReadLine();
                            SearchbyIdName searchbyIdName = new SearchbyIdName();
                            searchbyIdName.SearchbyName(name);
                            Console.ReadLine();
                            HandleMainmenu(Username);
                        }
                        else if(choice==3)
                        {
                            HandleMainmenu(Username);

                        }
                        else
                        {
                            Utility.DisplayErrorMessage("Invalid Choice");
                            HandleMainmenu(Username);
                        }
                    }
                    else if (choice == 3)
                    {
                        HandleMainmenu(Username);
                    }
                    else 
                    {
                        Utility.DisplayErrorMessage("Invalid Choice");
                        HandleMainmenu(Username);
                    }
                    break;
                case 4:
                    Console.WriteLine("Task Assinged To You");
                    var tasks = dbo.UserInfoes.FirstOrDefault(x => x.userName == Username);
                    if (tasks != null)
                    {
                        var task = tasks.Employee.Tasks.ToList();
                        foreach(var items in task)
                        {
                            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                            Console.WriteLine($"Task Id :{items.taskId}\t\tTask Name :{items.taskName}\n\nDescription :{items.Description}\n\nDeadLine :{items.deadLine}\t\tStatus :{items.Status}");
                            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                        }
                        Console.ReadKey();
                        HandleMainmenu(Username);
                    }
                    else
                    {
                       Console.WriteLine("0 Task Found");
                        HandleMainmenu(Username);
                    }
                    break;
                case 5:
                    Console.WriteLine("Enter Task Id");
                    int tid = int.Parse(Console.ReadLine());
                    var tasks1 = dbo.UserInfoes.FirstOrDefault(x => x.userName == Username);
                    if (tasks1 != null)
                    {
                        var task1 = tasks1.Employee.Tasks.ToList();
                        foreach(var item in task1)
                        {
                            if(item.taskId==tid)
                            {
                                Console.Write("Enter Status As InProgess || Completed --> ");
                                string status = Console.ReadLine();
                                if(status=="InProgress"||status=="Completed")
                                {
                                    item.Status = status;
                                    int x = dbo.SaveChanges();
                                    if (x > 0)
                                    {
                                        Utility.DisplaySuccessMessage("Status Updated");
                                        HandleMainmenu(Username);
                                    }
                                    else
                                    {
                                        Utility.DisplayErrorMessage("Staus Not Upated Please Try Again !");
                                        HandleMainmenu(Username);
                                    }
                                }
                                else
                                {
                                    Utility.DisplayErrorMessage("You Can Enter Only InProgess || Completed");
                                    HandleMainmenu(Username);
                                }
                               
                            }
                        }
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("0 Task Found");
                        HandleMainmenu(Username);
                    }

                    break;
                case 6:
                    var emp1 = dbo.UserInfoes.FirstOrDefault(x => x.userName == Username);
                    if (emp1 != null)
                    {


                        PayrollProcessing payrollProcessing = new PayrollProcessing();
                        var salary = emp1.Employee.empSalary;
                        var task = emp1.Employee.Tasks.Count(x => x.Status == "Completed");
                        payrollProcessing.CalculateNetSalary((decimal)salary, task);
                        payrollProcessing.DisplaySalaryBreakdown((decimal)salary, task);

                        Console.ReadKey();
                        HandleMainmenu(Username);
                    }
                    break;
                
            }

        }
    }
}
