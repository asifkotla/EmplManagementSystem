using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmplManagementSystem.Interface;
using EmplManagementSystem.Model;
using EmplManagementSystem.Utility1;

namespace EmplManagementSystem.HR_Dashboard
{
    internal class Search : Isearch
    {
        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();
        public void SearchbyId(int id)
        {
            var obj = dbo.UserInfoes.FirstOrDefault(x=>x.empId==id);


            if (obj != null)
            {
                    Console.WriteLine("     * - * - * Employee Details * - * - *         ");
                    Console.WriteLine($"Employee Name   : {obj.Employee.empName}\n" +
                                      $"Department      : {obj.Employee.Department.deptName}\n" +
                                      $"Email           : {obj.Email}\n" +
                                      $"Employee Salary : {obj.Employee.empSalary}");
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
                
               
            }
            else
            {
                Utility.DisplayErrorMessage("Invalid Employee Id ");

            }
        }

        public void SearchbyName(string id)
        {
            var obj = dbo.UserInfoes.FirstOrDefault(x => x.Employee.empName == id);
            if (obj != null)
            {
                Console.WriteLine("     * - * - * Employee Details * - * - *         ");
                Console.WriteLine($"Employee Name   : {obj.Employee.empName}\n" +
                                  $"Department      : {obj.Employee.Department.deptName}\n" +
                                  $"Email           : {obj.Email}\n" +
                                  $"Employee Salary : {obj.Employee.empSalary}");
                Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
            }
            else
            {
                Utility.DisplayErrorMessage("Invalid Employee Name ");
            }
        }
        public void SearchByJoiningDate(DateTime date)
        {
            var obj = dbo.UserInfoes
     .Where(x => DbFunctions.TruncateTime(x.Employee.JoiningDate) == DbFunctions.TruncateTime(date))
     .ToList();

            if (obj.Any())
            {
                    Console.WriteLine("     * - * - * Employee Details * - * - *         ");
                foreach (var item in obj)
                {
                    Console.WriteLine($"Employee Id      : {item.empId}\n" +
                                      $"Employee Name    : {item.Employee.empName}\n" +
                                      $"Department       : {item.Employee.Department.deptName}\n" +
                                      $"Email            : {item.Email}\n" +
                                      $"Joining Date     :{item.Employee.JoiningDate}");
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");

                }
                
            }
            else
            {
                Utility.DisplayErrorMessage("Invalid Joining Date ");
            }
        }
    }
}
