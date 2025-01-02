# Employee Management System

## Overview
The Employee Management System is a comprehensive solution designed to streamline employee data management. It provides functionality for creating, reading, updating, and deleting (CRUD) employee records while incorporating additional features like email notifications and search capabilities. The system is scalable, secure, and user-friendly, making it an ideal choice for organizations of all sizes.

---

## Features

1. **CRUD Operations**
   - Add, update, view, and delete employee records.

2. **Email Notifications**
   - Password recovery for users.
   - Notifications for new hires.

3. **Employee Search Functionality**
   - Search employees by ID, Name, or Joining Date.

4. **Role-Based Dashboards**
   - Different views for Admin, Manager, HR, and Employees.

5. **Security Features**
   - Password validation and hashing.
   - Secure credential verification.

---

## Technologies Used

- **Programming Language:** C#
- **Framework:** .NET Core & Entity Framework Core
- **Database:** SQL Server
- **Email Service:** SMTP Server

---

## System Architecture

1. **Model Layer**
   - Handles data representation.

2. **Logic Layer**
   - Implements application logic and workflows.

3. **Data Access Layer**
   - Interfaces with SQL Server using Entity Framework Core.

4. **Email Integration Layer**
   - Handles notifications via SMTP.

---

## Key Components

### **Model Example**
```csharp
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime JoiningDate { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
}
```

### **Search Functionality**
```csharp
public IEnumerable<Employee> SearchEmployees(string searchType, string searchValue)
{
    switch (searchType.ToLower())
    {
        case "id":
            int id = int.Parse(searchValue);
            return dbo.Employees.Where(e => e.Id == id);
        case "name":
            return dbo.Employees.Where(e => e.Name.Contains(searchValue));
        case "joiningdate":
            DateTime date = DateTime.Parse(searchValue);
            return dbo.Employees.Where(e => e.JoiningDate == date);
        default:
            return Enumerable.Empty<Employee>();
    }
}
```

### **Email Service**
```csharp
public void SendEmail(SendMail sendMail)
{
    MailMessage message = new MailMessage
    {
        From = new MailAddress("employeeemanagementsystem@gmail.com"),
        Subject = sendMail.Subject,
        Body = sendMail.Body,
        IsBodyHtml = true
    };
    message.To.Add(new MailAddress(sendMail.ToEmail));

    var smtpClient = new SmtpClient("smtp.gmail.com")
    {
        Port = 587,
        Credentials = new NetworkCredential("employeeemanagementsystem@gmail.com", "password"),
        EnableSsl = true
    };
    smtpClient.Send(message);
}
```

### **Database Schema**
```sql
CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    JoiningDate DATE,
    Department NVARCHAR(50),
    Salary DECIMAL(18, 2)
);
```

---

## Future Enhancements

1. Role-based access control for improved security.
2. Integration with cloud-based services for scalability.
3. Advanced analytics and reporting dashboards.
4. Automated onboarding workflows.

---

## Getting Started

### Prerequisites

- Visual Studio 2022 or later.
- .NET Core SDK.
- SQL Server.
- SMTP server credentials.


## Acknowledgments

- Special thanks to the open-source community for their tools and libraries.

  Here’s a note you can include:  

---

**Note:**  
To enable the SMTP email functionality in this project, you need to replace the placeholders in the following lines of code with your own email address and app password:  

```csharp
string fromemail = "Your Email";  
string frompass = "Your App Password";  
```

### Steps:
1. **Email Address**: Use the email address you want to send emails from. Ensure this account is configured to allow SMTP access.  
2. **App Password**: Generate an app password for your email account instead of using your main password.  
   - For Gmail: Go to your Google Account > Security > App Passwords, and create an app password.  
   - For other providers: Check their documentation on enabling SMTP and generating app passwords.  

> **Caution:** Never hard-code sensitive credentials in production. Use environment variables or secure storage for better security.
