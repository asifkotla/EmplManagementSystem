-- 1. Employee Details Department Wise
CREATE PROCEDURE Pr_EmplDetailsDeptWise  
    @deptName VARCHAR(50)  
AS
BEGIN
    SELECT 
        e.empName AS Employee_Name, 
        u.email AS Email,
        d.DeptName AS Department_Name
    FROM 
        Employee e
    JOIN 
        Department d ON e.deptId = d.deptId
    JOIN 
        UserInfo u ON e.empId = u.empId
    WHERE 
        d.deptName = @deptName AND e.isActive = 0
    ORDER BY 
        e.empName;
END;

-- 2. Employee Overview
CREATE PROCEDURE Pr_EmployeeOverview 
AS
BEGIN
    SELECT 
        e.empId, 
        e.empName, 
        e.empSalary, 
        d.deptName AS Department,
        u.userName AS UserName,
        r.roleName AS Role
    FROM 
        Employee e
    JOIN 
        Department d ON e.deptId = d.deptId
    JOIN 
        UserInfo u ON e.empId = u.empId
    JOIN 
        Role r ON u.roleId = r.roleId
    WHERE 
        e.isActive = 0
    ORDER BY 
        e.empName;
END;

-- 3. Task Report for Employees
CREATE PROCEDURE Pr_GetEmployeeTaskReport
    @empId INT
AS
BEGIN
    SELECT 
        t.taskId, 
        t.taskName, 
        t.Description, 
        t.Status, 
        t.deadLine
    FROM 
        Task t
    WHERE 
        t.empId = @empId AND EXISTS (
            SELECT 1 FROM Employee e WHERE e.empId = @empId AND e.isActive = 0
        )
    ORDER BY 
        t.deadLine;
END;

-- 4. Department-Wise Employee Report
CREATE PROCEDURE Pr_DepartmentWiseEmployeeReport 
AS
BEGIN
    SELECT 
        d.deptName AS Department,
        e.empId, 
        e.empName, 
        e.empSalary
    FROM 
        Employee e
    JOIN 
        Department d ON e.deptId = d.deptId
    WHERE 
        e.isActive = 0
    ORDER BY 
        d.deptName, e.empName;
END;

-- 5. Salary Report
CREATE PROCEDURE Pr_GetSalaryReport
AS
BEGIN
    SELECT 
        e.empId, 
        e.empName AS Name, 
        e.empSalary AS Salary, 
        d.deptName AS Department
    FROM 
        Employee e
    JOIN 
        Department d ON e.deptId = d.deptId
    WHERE 
        e.isActive = 0
    ORDER BY 
        e.empSalary DESC;
END;

-- 6. Task Completion Status Report
CREATE PROCEDURE Pr_TaskCompletionStatusReport 
    @Status VARCHAR(50)
AS
BEGIN
    SELECT 
        t.taskId, 
        t.taskName, 
        t.Status, 
        e.empName AS AssignedTo, 
        t.deadLine
    FROM 
        Task t
    JOIN 
        Employee e ON t.empId = e.empId
    WHERE 
        t.Status = @Status AND e.isActive = 0
    ORDER BY 
        t.Status, t.deadLine;
END;

-- 7. Employee Performance Report by Department
CREATE PROCEDURE Pr_GetEmployeePerformanceReportByDepartment
    @deptName VARCHAR(50),
    @status VARCHAR(50)
AS
BEGIN
    SELECT 
        e.empId, 
        e.empName, 
        COUNT(t.taskId) AS Tasks
    FROM 
        Employee e
    JOIN 
        Department d ON e.deptId = d.deptId
    LEFT JOIN 
        Task t ON e.empId = t.empId AND t.Status = @status
    WHERE 
        d.deptName = @deptName AND e.isActive = 0
    GROUP BY 
        e.empId, e.empName
    ORDER BY 
        Tasks DESC;
END;

-- 8. Department-Wise Salary Distribution
CREATE PROCEDURE Pr_DepartmentSalaryDistribution
AS
BEGIN
    SELECT 
        d.deptName AS Department,
        COUNT(e.empId) AS Total_Employees,
        SUM(e.empSalary) AS Total_Salary_Distribution,
        AVG(e.empSalary) AS Average_Salary
    FROM 
        Department d
    LEFT JOIN 
        Employee e ON d.deptId = e.deptId AND e.isActive = 0
    GROUP BY 
        d.deptName
    ORDER BY 
        Total_Salary_Distribution DESC;
END;



-- 9 task pending
CREATE PROCEDURE Pr_DepartmentPendingTasks
AS
BEGIN
    SELECT 
        d.deptName AS Department,
        COUNT(t.taskId) AS Total_Pending_Tasks,
        e.empName AS Employee_Name
    FROM 
        Task t
    JOIN 
        Employee e ON t.empId = e.empId
    JOIN 
        Department d ON e.deptId = d.deptId
    WHERE 
        t.Status = 'Pending' AND e.isActive = 0
    GROUP BY 
        d.deptName, e.empName
    ORDER BY 
        Total_Pending_Tasks DESC;
END;

--10 top performer
CREATE PROCEDURE Pr_TopPerformers
AS
BEGIN
    SELECT 
        e.empId,
        e.empName AS Employee_Name,
        d.deptName AS Department,
        COUNT(t.taskId) AS Completed_Tasks
    FROM 
        Employee e
    LEFT JOIN 
        Task t ON e.empId = t.empId AND t.Status = 'Completed'
    JOIN 
        Department d ON e.deptId = d.deptId
    WHERE 
        e.isActive = 0
    GROUP BY 
        e.empId, e.empName, d.deptName
    ORDER BY 
        Completed_Tasks DESC;
END;
exec Pr_TopPerformers
--11 department salary budget
CREATE PROCEDURE Pr_DepartmentSalaryBudget
AS
BEGIN
    SELECT 
        d.deptName AS Department,
        COUNT(e.empId) AS Total_Employees,
        SUM(e.empSalary) AS Total_Salary,
        MAX(e.empSalary) AS Highest_Salary,
        MIN(e.empSalary) AS Lowest_Salary
    FROM 
        Department d
    LEFT JOIN 
        Employee e ON d.deptId = e.deptId AND e.isActive = 0
    GROUP BY 
        d.deptName
    ORDER BY 
        Total_Salary DESC;
END;

--12 emp role destribution
CREATE PROCEDURE Pr_EmployeeRoleDistribution
AS
BEGIN
    SELECT 
        r.roleName AS Role,
        d.deptName AS Department,
        COUNT(e.empId) AS Total_Employees
    FROM 
        Role r
    JOIN 
        UserInfo u ON r.roleId = u.roleId
    JOIN 
        Employee e ON u.empId = e.empId
    JOIN 
        Department d ON e.deptId = d.deptId
    WHERE 
        e.isActive = 0
    GROUP BY 
        r.roleName, d.deptName
    ORDER BY 
        Total_Employees DESC;
END;

--13 task effienciy
CREATE PROCEDURE Pr_TaskEfficiency
AS
BEGIN
    SELECT 
        e.empId,
        e.empName AS Employee_Name,
        COUNT(t.taskId) AS Total_Tasks,
        COUNT(CASE WHEN t.Status = 'Completed' AND t.Getdate() <= t.deadLine THEN 1 END) AS On_Time_Tasks,
        COUNT(CASE WHEN t.Status = 'Completed' AND t.CompletedDate > t.deadLine THEN 1 END) AS Late_Tasks
    FROM 
        Employee e
    LEFT JOIN 
        Task t ON e.empId = t.empId
    WHERE 
        e.isActive = 0
    GROUP BY 
        e.empId, e.empName
    ORDER BY 
        On_Time_Tasks DESC, Late_Tasks;
END;
exec Pr_TaskEfficiency
--14 resgined employee contribustion
CREATE PROCEDURE Pr_Resigned
AS
BEGIN
    SELECT 
        e.empId,
        e.empName AS Employee_Name,
        COUNT(t.taskId) AS Total_Tasks_Completed
    FROM 
        Employee e
    LEFT JOIN 
        Task t ON e.empId = t.empId AND t.Status = 'Completed'
    WHERE 
        e.isActive = 1
    GROUP BY 
        e.empId, e.empName
    ORDER BY 
        Total_Tasks_Completed DESC;
END;
 drop procedure  Pr_ResignedEmployeeContribution
 exec Pr_ResignedEmployeeContribution