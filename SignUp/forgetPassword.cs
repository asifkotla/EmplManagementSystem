using EmplManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmplManagementSystem.Utility1;
using EmplManagementSystem.SMTP;
using static System.Net.WebRequestMethods;

namespace EmplManagementSystem.SignUp
{
    internal class forgetPassword

    {
        Employee_Management_SystemEntities3 dbo = new Employee_Management_SystemEntities3();
        public void frgtpass(string name)
        {
            var obj = dbo.UserInfoes.FirstOrDefault(x=>x.userName==name);
            if (obj != null)
            {
                string otp1 = GenerateOtp();
                SendMail sendMail = new SendMail()
                {
                    ToEmail=obj.Email,
                    Subject="One Time Password for Reset Your PassWord",
                    Body = $@"<html>
                               <head>
                                <style>
                                body {{
                                font-family: Arial, sans-serif;
                                line-height: 1.6;
                                color: #333333;
                                }}
                               .container {{
                                max-width: 600px;
                                margin: 0 auto;
                                padding: 20px;
                                border: 1px solid #dddddd;
                                border-radius: 5px;
                                background-color: #f9f9f9;
                                }}
                               .otp {{
                               font-size: 20px;
                               font-weight: bold;
                               color: #d9534f;
                               }}
                             </style>
                           </head>
                        <body>
                            <div class='container'>
                              <p>Dear {obj.userName},</p>
                              <p>We received a request to reset your password. Please use the following One Time Password (OTP) to proceed:</p>
                              <p class='otp'>OTP: {otp1}</p>
                              <p>If you did not request this change, please ignore this email.</p>
                              <p>Best regards,<br>Employee Management System Team</p>
                            </div>
                       </body>
                    </html>"
                };
                sendMail.SendEmail(sendMail);
                obj.otp = otp1;
                int save=dbo.SaveChanges();
                if (save > 0)
                {
                    Utility.DisplaySuccessMessage("OTP Generated");
                }
                else
                {
                    Utility.DisplayErrorMessage("Something went wrong");
                }
                Console.WriteLine("Enter New Password");
                string pass1=Console.ReadLine();
                if (Utility.IsValidEmail(pass1))
                {
                    Utility.DisplayErrorMessage("Password must be at least 8 characters long and include one uppercase letter, one lowercase letter, one number, and one special character.");
                    frgtpass(name);
                }
                Console.WriteLine("Re-Enter Password");
                string pass2=Console.ReadLine();
                if (pass1 == pass2)
                {
                    Console.Write("Enter OTP :");
                    string otp = Console.ReadLine();
                    if (otp == obj.otp)
                    {

                        byte[] pass=Utility.HashPassword(pass1);
                        obj.password = pass;
                        int n=dbo.SaveChanges();
                        if (n > 0)
                        {
                            Utility.DisplaySuccessMessage("Password Changed Successfully. . .");
                            Signup signup = new Signup();
                            signup.IsLogin();

                        }
                        else
                        {
                            Utility.DisplayErrorMessage("Something Went Wrong Please Try");
                            frgtpass(name);
                        }
                    }
                    else
                    {
                        Utility.DisplayErrorMessage("Wrong OTP");
                        frgtpass(name);
                    }

                }
                else {
                    Utility.DisplayErrorMessage("Password Not Matched");
                    frgtpass(name);
                }
                
            }
            else
            {
                Utility.DisplayErrorMessage("User Not Found");
                Signup signup = new Signup();
                signup.IsLogin();
            }



        }
        public string GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 1000000); // Generates a number between 100000 and 999999
            return otp.ToString();
        }
    }
}
