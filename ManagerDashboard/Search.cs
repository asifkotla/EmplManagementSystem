using EmplManagementSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmplManagementSystem.Interface;
using EmplManagementSystem.Utility1;
using System.Xml.Linq;
using EmplManagementSystem.Model;

namespace EmplManagementSystem.ManagerDashboard
{
    internal class Searchmang : Isearch
    {

        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();
        public void SearchbyId(int id)
        {
            var obj = dbo.UserInfoes.FirstOrDefault(x => x.empId == id);

            if (obj != null)
            {
                Console.WriteLine($"Employee Id      : {obj.empId}\n" +
                                    $"Employee Name  : {obj.Employee.empName}\n" +
                                    $"Department     : {obj.Employee.Department.deptName}\n" +
                                    $"Email          : {obj.Email}\n" +
                                    $"Contact        : {obj.Mobile}\n");
                Console.WriteLine("------------------------------------------------------------");
                Console.ReadLine();

                
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
                Console.WriteLine($"Employee Id      : {obj.empId}\n" +
                                    $"Employee Name  : {obj.Employee.empName}\n" +
                                    $"Department     : {obj.Employee.Department.deptName}\n" +
                                    $"Email          : {obj.Email}\n" +
                                    $"Contact        : {obj.Mobile}\n");
                Console.WriteLine("------------------------------------------------------------");
                Console.ReadLine();
            }
            else
            {
                Utility.DisplayErrorMessage("Invalid Employee Name ");
            }
        }
    }
}
