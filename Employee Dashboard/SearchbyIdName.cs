using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EmplManagementSystem.Interface;
using EmplManagementSystem.Model;
using EmplManagementSystem.Utility1;

namespace EmplManagementSystem.Employee_Dashboard
{
    internal class SearchbyIdName : Isearch
    {
        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();

        public void SearchbyId(int id)
        {
            Utility.Heading1();
            var obj = dbo.UserInfoes.FirstOrDefault(x => x.empId== id);

            if (obj != null)
            {
                Console.WriteLine($"Employee Id      : {obj.empId}\n" +
                                    $"Employee Name  : {obj.Employee.empName}\n" +
                                    $"Department     : {obj.Employee.Department.deptName}\n" +
                                    $"Email          : {obj.Email}\n");
            }
            else
            {
                Utility.DisplayErrorMessage("Invalid Employee Id ");

            }

        }

        public void SearchbyName(string id)
        {
            Utility.Heading1();
            var obj = dbo.UserInfoes.FirstOrDefault(x => x.Employee.empName == id);
            if (obj != null)
            {
                Console.WriteLine($"Employee Id      : {obj.empId}\n" +
                                    $"Employee Name  : {obj.Employee.empName}\n" +
                                    $"Department     : {obj.Employee.Department.deptName}\n" +
                                    $"Email          : {obj.Email}\n");
            }
            else
            {
                Utility.DisplayErrorMessage("Invalid Employee Name ");
            }
        }
    }
}
