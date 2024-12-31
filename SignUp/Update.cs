using EmplManagementSystem.Model;
using System;
using System.Linq;
using EmplManagementSystem.Utility1;

namespace EmplManagementSystem.SignUp
{
    internal class Update : Signup
    {
        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();

        public bool ChangePassword()
        {
            try
            {
                Console.WriteLine("Enter User Name:");
                string uname = Console.ReadLine();
                Console.WriteLine("Enter Your Old Password:");
                string pass = Console.ReadLine();
                Console.WriteLine("Enter Role ID:");
                int r;
                while (!int.TryParse(Console.ReadLine(), out r))
                {
                    Utility.DisplayErrorMessage("Please enter a valid Role ID.");
                }
                var user = dbo.UserInfoes.FirstOrDefault(x => x.userName == uname && x.roleId == r);
                if (user != null)
                {
                    byte[] hashedInputPassword =Utility.HashPassword(pass);

                    if (user.password != null && hashedInputPassword.SequenceEqual(user.password))
                    {
                        Console.WriteLine("Enter New Password:");
                        string pass1 = Console.ReadLine();
                        Console.WriteLine("Re-Enter  Password :");
                        string pass2 = Console.ReadLine();

                        if (pass1 != pass2)
                        {
                            Utility.DisplayErrorMessage("Passwords do not match!");
                            ChangePassword();
                            return false;
                        }

                        byte[] hashed = Utility.HashPassword(pass2);
                        user.password = hashed;

                        int n = dbo.SaveChanges();
                        if (n > 0)
                        {
                            Console.Clear();
                            Utility.DisplaySuccessMessage("Password changed successfully.");
                            return true;
                        }
                        else
                        {
                            Console.Clear();
                            Utility.DisplayErrorMessage("An error occurred while saving the changes. Please try again.");
                            ChangePassword();
                            return false;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Utility.DisplayErrorMessage("Invalid current password.");
                        ChangePassword();
                        return false;
                    }
                }
                else
                {
                    Console.Clear();
                    Utility.DisplayErrorMessage("Invalid username or role ID.");
                    return false;
                }
            }
            catch (FormatException ex)
            {
                Utility.DisplayErrorMessage($"Input format is incorrect. Details: {ex.Message}");
                ChangePassword();
                return false;
            }
            catch (Exception ex)
            {
                Utility.DisplayErrorMessage($"An unexpected error occurred. Details: {ex.Message}");
                ChangePassword();
                return false;
            }
        }
    }
}
