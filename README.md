Here’s the updated presentation with the added feature:

---

### **Slide 1: Title Slide**  
**Title:** Employee Management System  
**Subtitle:** Streamlining Employee Data Management  

---

### **Slide 2: Introduction**  
**Why Employee Management System?**  
- Centralized solution for managing employee information.  
- Automates key tasks like notifications and validations.  
- Scalable design for organizations of all sizes.  

---

### **Slide 3: Features**  
- **CRUD Operations:** Add, update, view, and delete employee records.  
- **Email Notifications:**  
  - Password recovery.  
  - Notifications for new hires.  
- **Employee Search Functionality:**  
  - Search by ID, Name, and Joining Date.  
- **Role-Based Dashboards:**  
  - Separate views for Admin, Manager, HR, and Employees.  
- **Security Features:**  
  - Validate and hash passwords.  
  - Verify credentials securely.  

---

### **Slide 4: Technologies Used**  
- **Programming Language:** C#  
- **Framework:** .NET Core & Entity Framework Core  
- **Database:** SQL Server  
- **Email Service:** SMTP Server  

---

### **Slide 5: System Architecture**  
**Core Layers:**  
1. **Model Layer:** Handles data representation.  
2. **Business Logic Layer:** Implements application logic and workflows.  
3. **Data Access Layer:** Interfaces with SQL Server using Entity Framework Core.  
4. **Email Integration Layer:** Handles notifications via SMTP.  

---

### **Slide 6: Key Components**  

#### **Model Example:**  
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

#### **Search Functionality:**  
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

#### **Email Service:**  
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

#### **Database Schema:**  
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

### **Slide 7: Future Enhancements**  
- Role-based access control for improved security.  
- Integration with cloud-based services for scalability.  
- Advanced analytics and reporting dashboards.  
- Automated onboarding workflows.  

---

### **Slide 8: Thank You!**  
**Q&A:**  
- We appreciate your time and look forward to your feedback.  
- Feel free to ask questions about the design or implementation.  

---

Let me know if you'd like further refinements!
