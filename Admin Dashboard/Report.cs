using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmplManagementSystem.Interface;

namespace EmplManagementSystem.Admin_Dashboard
{
    internal class Report : IReport
    {
        public void GenerateReport()
        {
            Console.WriteLine("Select Type of Report");
            Console.WriteLine("1.DepartmentWise Employee List");
            Console.WriteLine("2.Employee Performance Report ");
            Console.WriteLine("3.Department Overview");
            Console.WriteLine("5.");
        }
    }
}
