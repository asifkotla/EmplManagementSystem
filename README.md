## Employee Management System

### Slide 1: Introduction
**Title:** Employee Management System  
**Subtitle:** Efficient Employee Data Management

### Slide 2: Features
- Perform Create, Read, Update, Delete (CRUD) operations for employees.
- Integrates SMTP server for email notifications:
  - Password recovery functionality for users.
  - Notifications when HR adds a new employee.
- Designed for scalability and ease of use.

### Slide 3: Technologies Used
- **Programming Language:** C#
- **Framework:** Entity Framework Core
- **Database:** SQL Server
- **Email Integration:** SMTP Server

### Slide 4: Architecture
- **Core Components:**
  - C# for business logic and application development.
  - Entity Framework Core for Object-Relational Mapping (ORM).
  - SQL Server for data storage and management.
  - SMTP integration for email functionality.

### Slide 5: Key Components
#### **Model:**
```csharp
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
}
```

#### **Controller:**
```csharp
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeContext _context;

    public EmployeeController(EmployeeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetEmployees() => Ok(_context.Employees.ToList());

    [HttpPost]
    public IActionResult AddEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();

        // Send email notification
        EmailService.SendNotification("HR@company.com", "New Employee Added", $"Employee {employee.Name} has been added.");

        return CreatedAtAction(nameof(GetEmployees), new { id = employee.Id }, employee);
    }
}
```

#### **Email Service:**
```csharp
public static class EmailService
{
    public static void SendNotification(string to, string subject, string body)
    {
        var smtpClient = new SmtpClient("smtp.server.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("your_email", "your_password"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("noreply@company.com"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(to);

        smtpClient.Send(mailMessage);
    }
}
```

#### **Database Schema:**
```sql
CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Department NVARCHAR(50),
    Salary DECIMAL(18, 2)
);
```

### Slide 6: Future Enhancements
- Add role-based access control.
- Implement advanced analytics dashboards.
- Enhance email functionality for more triggers.

### Slide 7: Thank You
- Thank you for your attention!  
- Feel free to ask any questions about the project.

