using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmplManagementSystem.Interface;
using EmplManagementSystem.Model;
using EmplManagementSystem.Utility1;

namespace EmplManagementSystem.Admin_Dashboard
{

    internal class SearchbyIdName:Isearch
    {
        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();
        
        public void SearchbyId(int id)
        {
            
            var obj = dbo.dtlForAdmins.FirstOrDefault(x => x.empId == id );
           
            if (obj != null)
            {
                Console.WriteLine($"Employee Id      : {obj.empId}\n" +
                                    $"Employee Name  : {obj.EmpName}\n" +
                                    $"Department     : {obj.Department}\n" +
                                    $"Email          : {obj.Email}\n" +
                                    $"Contact        : {obj.Contact}\n" +
                                    $"Address        : {obj.Address}");
            }
            else
            {
                Utility.DisplayErrorMessage("Invalid Employee Id ");
                
            }
        }

        public void SearchbyName(string name)
        {
            var obj = dbo.dtlForAdmins.FirstOrDefault(x => x.EmpName == name );
            if (obj != null)
            {
                Console.WriteLine($"Employee Id    : {obj.empId}\n" +
                                  $"Employee Name  : {obj.EmpName}\n" +
                                  $"Department     : {obj.Department}\n" +
                                  $"Email          : {obj.Email}\n" +
                                  $"Contact        : {obj.Contact}\n" +
                                  $"Address        : {obj.Address}");
            }
            else
            {
                Utility.DisplayErrorMessage("Invalid Employee Name ");
            }
        }
    }
}
