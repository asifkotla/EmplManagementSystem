using EmplManagementSystem.Utility1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EmplManagementSystem.HR_Dashboard
{
    public class PayrollProcessing
    {

        private const decimal LowTaxRate = 0.10m;
        private const decimal HighTaxRate = 0.20m;
        private const decimal TaxThreshold = 50000m;
        private const decimal TaskIncentive = 500m;

        public decimal CalculateNetSalary(decimal grossSalary, int completedTasks)
        {

            decimal tax = grossSalary > TaxThreshold
                ? grossSalary * HighTaxRate
                : grossSalary * LowTaxRate;


            decimal totalIncentives = completedTasks * TaskIncentive;


            decimal netSalary = grossSalary - tax + totalIncentives;

            return netSalary;
        }

        public void DisplaySalaryBreakdown(decimal grossSalary, int completedTasks)
        {
            decimal tax = grossSalary > TaxThreshold
                ? grossSalary * HighTaxRate
                : grossSalary * LowTaxRate;

            decimal totalIncentives = completedTasks * TaskIncentive;
            decimal netSalary = CalculateNetSalary(grossSalary, completedTasks);
            
            Console.WriteLine("----- Salary Breakdown -----");
            Console.WriteLine($"Gross Salary     : {grossSalary}");
            Console.WriteLine($"Tax Deducted     : {tax}");
            Console.WriteLine($"Incentives Earned: {totalIncentives}");
            Console.WriteLine($"Net Salary       : {netSalary}");
            Console.WriteLine("-----------------------------");
        }
    }
}
