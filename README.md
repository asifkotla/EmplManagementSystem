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
Here's an updated section for the **README.md** file to include details about the **Task Module**:

---

### **Task Module**

The Task Module in the Employee Management System is designed to manage and monitor tasks assigned to employees. It provides features for tracking task progress, deadlines, and status updates.

#### **Key Features**
- **Task Assignment**: Assign tasks to specific employees based on their roles or departments.
- **Status Tracking**: Monitor the status of tasks with predefined statuses like `Pending`, `InProgress`, and `Completed`.
- **Deadline Management**: Set and track task deadlines to ensure timely completion.
- **Task Reports**: Generate reports to analyze task completion rates and identify overdue tasks.
- **Incentive Integration**: Automatically calculate and add incentives for employees upon task completion.

#### **Database Schema**
```sql
CREATE TABLE Task (
    taskId INT IDENTITY(1,1) PRIMARY KEY,
    taskName NVARCHAR(100) NOT NULL,
    description NVARCHAR(255),
    status NVARCHAR(20) CHECK (status IN ('Pending', 'InProgress', 'Completed')) NOT NULL,
    deadline DATE NOT NULL,
    empId INT FOREIGN KEY REFERENCES Employee(empId) ON DELETE SET NULL
);
```

#### **Key Methods**
1. **Assign Task**  
   Assign tasks to employees with necessary details such as description, status, and deadline.

2. **Update Task Status**  
   Allows HR and Managers to update the status of a task as it progresses.

3. **Overdue Task Detection**  
   Automatically detect tasks that are overdue based on the current date and provide alerts or notifications.

4. **Task Completion Report**  
   Generate detailed reports on completed and pending tasks for individual employees or departments.

#### **Example Usage**
```csharp
// Assigning a task to an employee
Task newTask = new Task {
    taskName = "Complete Documentation",
    description = "Prepare the project documentation for submission.",
    status = "Pending",
    deadline = DateTime.Parse("2025-04-15"),
    empId = 110 // Assigned to Employee ID 110
};
dbo.Tasks.Add(newTask);
dbo.SaveChanges();
```

Here's an updated section for the **README.md** file to include details about the **Report Generation Module**:

---

### **Report Generation Module**

The Report Generation Module is a key feature of the Employee Management System, enabling Admin, HR, and Managers to generate insightful reports for monitoring and decision-making. This module ensures transparency and provides a detailed view of employee and organizational performance.

#### **Key Features**
- **Department-Wise Reports**: View employee distribution, salaries, and performance metrics by department.
- **Employee Performance Reports**: Analyze task completion and efficiency for individual employees.
- **Salary Reports**: Generate salary distribution reports, including gross, deductions, and net salary details.
- **Task Status Reports**: Monitor pending, in-progress, and completed tasks organization-wide.
- **Role-Based Access**: Reports are customized based on user roles (Admin, HR, Manager).

#### **Database Integration**
The report module uses stored procedures for efficient data retrieval and analysis. Examples include:
```sql
-- Employee Details by Department
CREATE PROCEDURE Pr_EmplDetailsDeptWise (@deptName NVARCHAR(50))
AS
BEGIN
    SELECT 
        e.empName AS Employee_Name,
        u.email AS Email,
        d.deptName AS Department
    FROM 
        Employee e
    JOIN 
        Department d ON e.deptId = d.deptId
    JOIN 
        UserInfo u ON e.empId = u.empId
    WHERE 
        d.deptName = @deptName
    ORDER BY 
        e.empName;
END;
```

#### **Key Reports**
1. **Department Headcount Report**  
   Displays the number of employees in each department.

2. **Salary Distribution Report**  
   Summarizes salary data across departments, including total and average salaries.

3. **Task Completion Report**  
   Provides insights into task status for employees, departments, or the organization as a whole.

4. **Performance Report**  
   Tracks individual and departmental performance metrics based on task completion.

5. **Resigned Employees Report**  
   Lists details of employees who have resigned, along with pending tasks.

#### **Example Usage**
```csharp
// Generate a department-wise employee report
Console.WriteLine("Enter Department Name:");
string departmentName = Console.ReadLine();
var report = dbo.Pr_EmplDetailsDeptWise(departmentName).ToList();

foreach (var item in report)
{
    Console.WriteLine($"Employee: {item.Employee_Name}, Email: {item.Email}, Department: {item.Department}");
}
```

#### **Benefits**
- **Data-Driven Decisions**: Facilitates strategic planning and decision-making.
- **Time Efficiency**: Reduces manual effort in report creation with automated generation.
- **Role-Specific Insights**: Tailored reports ensure relevant data is accessible to the right stakeholders.

---

## Future Enhancements
1. Enhancements include role-based access control for security.
2. Cloud service integration.
3. Advanced analytics for better insights.
4. Migrate Console Application to Web Application using ASP.NET and MVC.
5. Use WEB API for Notifications on Contact Numbers.
6. Additional features like attendance tracking and leave management will be added.

## Usage
1. Replace the placeholders in the SMTP configuration with your email and app password.
2. Run the application to perform employee management operations.
3. Explore the dashboards and utilize features based on your role.

## Contributing
Contributions are welcome! Feel free to submit issues or pull requests for improvements or bug fixes.


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
