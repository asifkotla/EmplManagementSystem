using EmplManagementSystem.Admin_Dashboard;
using EmplManagementSystem.Model;
using EmplManagementSystem.SMTP;
using EmplManagementSystem.Utility1;
using System;
using System.Data.Entity;
using System.Linq; 
namespace EmplManagementSystem.HR_Dashboard
{
    internal class ADDEMPL
    {
        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();

        public void AddEmployee()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("Enter Employee Name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Enter Employee Salary IN LPA:");
                if (!float.TryParse(Console.ReadLine(), out float sal))
                {
                    Utility.DisplayErrorMessage("Invalid salary input. Please enter a numeric value.");
                    return;
                }
                decimal salary = (decimal)sal * 100000 / 12;

                Console.WriteLine("Enter Department ID:");
                if (!int.TryParse(Console.ReadLine(), out int dept))
                {
                    Utility.DisplayErrorMessage("Invalid department ID. Please enter a numeric value.");
                    return;
                }

                Console.WriteLine("Enter Manager ID :");
                int mang = int.Parse(Console.ReadLine());
                
                Console.WriteLine("Enter Employee Email Address:");
                string email;
                while (true)
                {
                    email = Console.ReadLine();
                    if (Utility.IsValidEmail(email))
                        break;

                    Utility.DisplayErrorMessage("Invalid email format. Please try again:");
                }

                Employee employee = new Employee
                {
                    empName = name,
                    empSalary = salary,
                    deptId = dept,
                    managerId = mang,
                    JoiningDate = DateTime.Now,
                    isActive = false
                };

                dbo.Employees.Add(employee);
                int n=dbo.SaveChanges();
                if (n > 0)
                {
                    Utility.DisplaySuccessMessage("Added Employee Successfully");

                    // Fetch the newly added employee along with related Department
                    var emp2 = dbo.Employees.FirstOrDefault(x => x.empName == name && x.isActive==false);
                    if (emp2 != null)
                    {
                        var obj=dbo.UserInfoes.FirstOrDefault(x=>x.Employee.empId==mang);
                        var obj1 = dbo.UserInfoes.FirstOrDefault(x => x.Employee.Department.deptId == dept);
                        SendMail sendMail = new SendMail()
                       
                        {
                            Subject = "Welcome to Our Organization - Complete Your Registration",
                            ToEmail = email,
                            Body =
                              $"<html><body>" +
                              $"<h1>Welcome, {emp2.empName}!</h1>" +
                              $"<p>We are delighted to have you join our organization.</p>" +
                              $"<p><strong>Your Employee ID:</strong> {emp2.empId}</p>" +
                              $"<p>Please complete your registration process using your Employee ID by the end of today to activate your account.</p>" +
                              $"<br><strong>Details of Your Employment:</strong><br>" +
                              $"1. <strong>Package:</strong> {sal} LPA<br>" +
                              $"2. <strong>Manager:</strong> {obj.Employee.empName} <br>" +
                              $"3. <strong>Manager Email:</strong> {obj.Email} <br>" +
                              $"4. <strong>Department:</strong> {obj1.Employee.Department.deptName} <br>" +
                              $"<p>If you have any questions or require assistance, feel free to reach out to the HR department.</p>" +
                              $"<p>Looking forward to working with you!</p>" +
                              $"<p>Best Regards,</p>" +
                              $"<p><strong>Employee Management System Team</strong></p>" +
                              $"</body></html>"
                        };
                        try
                        {
                            sendMail.SendEmail(sendMail);
                        }
                        catch (Exception ex)
                        {
                            Utility.DisplayErrorMessage($"Failed to send email Check Internet Connection: {ex.Message}");
                        }
                    }
                }
                else
                {
                    Utility.DisplayErrorMessage("Something went wrong. Please try again.");
                }
            }
            catch (FormatException fe)
            {
                Utility.DisplayErrorMessage($"Invalid input format: {fe.Message}");
            }
            catch (Exception ex)
            {
                Utility.DisplayErrorMessage($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                HR hR = new HR();
                hR.HandleMainmenu(null);
            }
        }
    }
}
