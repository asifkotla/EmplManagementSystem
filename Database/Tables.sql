-- Create the database
create database Employee_Management_System;
use Employee_Management_System;
--------------------------------------------------------------------------------------------------
-- Role Table
create table Role(
    roleId int primary key identity(1,1),
    roleName varchar(50) not null
);
---------------------------------------------------------------------------------------------------


-- User Table
CREATE TABLE UserInfo (
    userId INT PRIMARY KEY IDENTITY(10,1),
    empId INT UNIQUE NOT NULL, -- Links to Employee
    userName VARCHAR(50) UNIQUE NOT NULL,
    Mobile VARCHAR(15) NOT NULL,
    Email VARCHAR(30) UNIQUE NOT NULL,
    address VARCHAR(100),
    password VARBINARY(500) NOT NULL,
    roleId INT FOREIGN KEY REFERENCES Role(roleId) DEFAULT 0, -- Password Encryption
    CONSTRAINT FK_UserInfo_Employee FOREIGN KEY (empId) REFERENCES Employee(empId)
);

-----------------------------------------------------------------------------------------------------



-- Department Table
create table Department(
    deptId int primary key identity(100,1),
    deptName varchar(50) unique not null
);
 select * from Department
--------------------------------------------------------------------------------------------------

-- Employee Table
CREATE TABLE Employee (
    empId INT PRIMARY KEY IDENTITY(110,1),
    empName VARCHAR(50) NOT NULL,
    empSalary DECIMAL(10,2),
    deptId INT FOREIGN KEY REFERENCES Department(deptId),
    managerId INT NULL,-- Self-referential relationship
	JoiningDate datetime
    CONSTRAINT FK_Manager_Employee FOREIGN KEY (managerId) REFERENCES Employee(empId)
    ON DELETE NO ACTION
);

select * from Employee;

----------------------------------------------------------------------------------------------------
-- Task Table
create table Task(
    taskId int primary key identity(001,1),
    taskName varchar(50) not null,
    Description varchar(100) default null,
    Status varchar(20) not null,
    deadLine date not null,
    empId int foreign key references Employee(empId) on delete set null on update cascade
);
SELECT COLUMN_NAME, COLUMN_DEFAULT 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Employee';
--------------------------------------------------------------------------------------------------------



select * from Task;

-- Report Table
create table Report(
    reportId int primary key identity(0101,1),
    ReportDate date not null,
    ReportName varchar(100) not null,
    empId int foreign key references Employee(empId),
    taskId int foreign key references Task(taskId) on delete set null on update cascade
);

-------------------------------------------------------------------------------------------------------

select * from Report;


------all Tables-----------------

select * from Role;
select* from Employee;
select * from Department;
select * from UserInfo;
Select * from Task;
Select * from Report;


-----------------------------------------------------------------------------------
---Emoployee and there manager name
SELECT e.empId, e.empName, m.empName AS ManagerName
FROM Employee e
left JOIN Employee m ON e.managerId = m.empId;

--Employee Under Specific Mangaer
SELECT e.empId, e.empName
FROM Employee e
WHERE e.managerId = 112;

---not Manager
SELECT m.empId, m.empName
FROM Employee m
LEFT JOIN Employee e ON m.empId = e.managerId
WHERE e.empId IS NULL;

-------------------------------------------------------------------------------
DECLARE @InputPassword NVARCHAR(50) = 'Kotlaasif@07';

SELECT userName
FROM UserInfo
WHERE password = HASHBYTES('SHA2_256', @InputPassword);


-----------------Alter--------------------------------- 
ALTER TABLE Task
ADD CONSTRAINT CHK_Task_Status
CHECK (Status IN ('Pending', 'Completed', 'InProgress'));

ALTER TABLE Role ADD isActive BIT DEFAULT 0;
ALTER TABLE UserInfo ADD isActive BIT DEFAULT 0;
ALTER TABLE Department ADD isActive BIT DEFAULT 0;
ALTER TABLE Employee ADD isActive BIT DEFAULT 0;
ALTER TABLE Task ADD isActive BIT DEFAULT 0;
ALTER TABLE Report ADD isActive BIT DEFAULT 0;

-------------------------------------------------------
----------------------Triggers-------------------------
-------------------------------------------------------
CREATE TRIGGER TR_ondeletereport
ON report
INSTEAD OF DELETE
AS
BEGIN
    PRINT 'Deleted Record Successfully';

    -- Mark the records as inactive instead of deleting them
    UPDATE u
    SET isActive = 1
    FROM Report u
    INNER JOIN deleted d ON u.reportId = d.reportId;

    PRINT 'Trigger executed';
END;
---------------------------------------------------------------
--======trigger are created for all Tables As Shown Above======-
---------------------------------------------------------------

----------------------Views----------------------------
---------------For employee Details--------------------
----+++++++Restricted Views Based On Role++++++--------

create view employeeDetails As Select e.empName as Employee_Name , d.deptName As Department_Name,u.email as Email from Employee e  Join Department d on e.deptId=d.deptId join UserInfo u on e.empId=u.empId;

select * From employeeDetails

--1 Emp Details  for  HR
create View dtlForHR as Select e.EmpName As Employee_Name,d.deptNAme as Department,u.email As Email,u.Mobile As Contact,e.EmpSalary from Employee e  Join Department d on e.
deptId=d.deptId join  UserInfo u  on e.empid=u.empid WHERE  e.isActive = 0;

--2 Emp Details for Manager
create View dtlForManager as Select e.EmpName As Employee_Name,d.deptNAme as Department,u.email As Email,u.Mobile As Contact from Employee e Join Department d on e.
deptId=d.deptId join UserInfo u on e.empid=u.empid WHERE  e.isActive = 0;

--3  emp Details for Employee
create View dtlForEmpl as Select e.EmpName As Employee_Name,d.deptNAme as Department,u.email As Email from Employee e Join Department d on e.
deptId=d.deptId join UserInfo u on e.empid=u.empId WHERE  e.isActive = 0;

--4--Admin Emp Details
create View dtlForAdmin AS Select e.empId ,r.Rolename As Role,e.EmpName,u.Email As Email ,d.deptName as Department,u.Mobile As Contact,u.address As Address from Role r 
Join UserInfo u on r.roleId=u.roleId join employee e on u.empid=e.empid join Department d on d.deptId=e.deptId Where e.isActive=0;





---------------------------------------------------
SELECT 
    r.roleName AS Role,
    COUNT(e.empId) AS TotalEmployees
FROM 
    Role r
JOIN 
    UserInfo u ON r.roleId = u.roleId
JOIN 
    Employee e ON u.userid = e.userId
GROUP BY 
    r.roleName;

-------------------------------------------------------
----------------------Procedure------------------------
----------------------For Reports----------------------
--1 --employee Details Department Wise
CREATE PROCEDURE Pr_EmplDetailsDeptWise  
    (@deptName varchar(50))  
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
    d.deptName = @deptName
ORDER BY 
    e.empName;
END;
drop procedure EmployeeOverview   
---------------------------
EXEC Pr_EmplDetailsDeptWise @deptName = 'Marketing';

--2 Employee OverView
CREATE Procedure EmployeeOverview 
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
	End;

	exec  EmployeeOverview

--3-----Task Report For Employees----
drop procedure GetEmployeeTaskReport   

CREATE PROCEDURE GetEmployeeTaskReport
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
        t.empId=@empId
    ORDER BY 
        t.deadLine;
END;

----4 Department Wise Employee Report
drop procedure DepartmentWiseEmployeeReport
CREATE procedure DepartmentWiseEmployeeReport AS Begin
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
	end;


--5-----Salary Report
drop procedure GetSalaryReport
CREATE PROCEDURE GetSalaryReport
AS
BEGIN
    SELECT 
        e.empId, 
        e.empName as Name, 
        e.empSalary As Salary, 
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

-----6--Task Report--
drop procedure TaskCompletionStatusReport
select * from Task;
CREATE procedure TaskCompletionStatusReport 
(@Status varchar(50))
AS Begin
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
    t.Status IN (@Status)
ORDER BY 
    t.Status, t.deadLine;
	End;

exec TaskCompletionStatusReport @Status='Pending'

---7  Employee Performance Report by Department
drop procedure GetEmployeePerformanceReportByDepartment
CREATE PROCEDURE GetEmployeePerformanceReportByDepartment
    @deptName VARCHAR(50),@status varchar(50)
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
        d.deptName = @deptName
    GROUP BY 
        e.empId, e.empName
    ORDER BY 
        TasksCompleted DESC;
END;
EXEC GetEmployeePerformanceReportByDepartment @deptName='marketing',@status='completed'

--8 Resigned Employees
drop view ResignedEmpl
Create View ResignedEmpl As Select e.empName as Employee_Name,d.DeptName As Department,u.Email 
As Email From Employee e 
join
Department d on e.deptId=d.deptId 
JOin
UserInfo u on u.empId=e.empId
where
e.isActive=1;
---------------------------------------------------------------
--9 Specific Emp Task Complete Count
drop procedure TaskCompletionDetailsEmpl
CREATE PROCEDURE TaskCompletionDetailsEmpl
    @empName VARCHAR(50)
AS
BEGIN
    SELECT 
    e.empName,
    COUNT(t.taskId) AS TotalAssignedTasks,
    COUNT(CASE WHEN t.Status = 'Completed' THEN 1 END) AS CompletedTasks,
    COUNT(CASE WHEN t.Status = 'Pending' THEN 1 END) AS PendingTasks
FROM 
    Employee e
LEFT JOIN 
    Task t ON e.empId = t.empId
	where
	e.empName=@empName
GROUP BY 
    e.empName
ORDER BY
    e.empName;
END;


--10 department Head Count 

drop procedure  Pr_DepartmentHeadCount
CREATE PROCEDURE Pr_DepartmentHeadCount
AS
BEGIN
    SELECT 
        d.deptName AS Department_Name,
        COUNT(e.empId) AS Total_Employees
    FROM 
        Department d
    LEFT JOIN 
        Employee e ON d.deptId = e.deptId
    GROUP BY 
        d.deptName
    ORDER BY 
        d.deptName;
END;


---11--------------------------------
drop PROCEDURE Pr_SalaryReport
    @MinSalary DECIMAL(10, 2) = NULL,  -- Minimum salary (optional)
    @MaxSalary DECIMAL(10, 2) = NULL,  -- Maximum salary (optional)
    @FilterType NVARCHAR(20) = 'All'   -- Filter type: 'Above', 'Below', 'Range', or 'All'
AS
BEGIN
    -- Base query
    SELECT 
        e.empName AS Employee_Name,
        d.deptName AS Department,
        e.empSalary AS Salary,
        u.email AS Email,
        u.Mobile AS Contact
    FROM 
        Employee e
    JOIN 
        Department d ON e.deptId = d.deptId
    JOIN 
        UserInfo u ON e.empid = u.empid
    WHERE
        (@FilterType = 'All') OR
        (@FilterType = 'Above' AND e.empSalary > @MinSalary) OR
        (@FilterType = 'Below' AND e.empSalary < @MaxSalary) OR
        (@FilterType = 'Range' AND e.empSalary BETWEEN @MinSalary AND @MaxSalary)
    ORDER BY 
        e.empSalary DESC;
END

EXEC Pr_SalaryReport;
EXEC Pr_SalaryReport @MinSalary = 50000, @FilterType = 'Above';
EXEC Pr_SalaryReport @MaxSalary = 30000, @FilterType = 'Below';
EXEC Pr_SalaryReport @MinSalary = 40000, @MaxSalary = 60000, @FilterType = 'Range';
--------------------------------------------------------------------------------------------------------

----Pay Roll Report

drop PROCEDURE Pr_PayrollReport1
    @DepartmentId INT = NULL,      -- Optional: Filter by department
    @IncludeInactive BIT = 0  -- Optional: Include inactive employees (default is active only)
AS
BEGIN
    -- Payroll report with calculated fields
    SELECT 
        e.empName AS Employee_Name,
        d.deptName AS Department,
        e.empSalary AS Gross_Salary,
        CAST(e.empSalary * 0.10 AS DECIMAL(10, 2)) AS Tax_Deduction, -- Assuming 10% tax
        CAST(e.empSalary * 0.05 AS DECIMAL(10, 2)) AS Other_Deductions, -- Assuming 5% other deductions
        CAST(e.empSalary * 0.85 AS DECIMAL(10, 2)) AS Net_Salary, -- Gross - (Tax + Other Deductions)
        u.email AS Email,
        u.Mobile AS Contact,
        e.isActive AS Is_Active
    FROM 
        Employee e
    JOIN 
        Department d ON e.deptId = d.deptId
    JOIN 
        UserInfo u ON e.empid = u.empid
    WHERE 
        (@DepartmentId IS NULL OR e.deptId = @DepartmentId ) -- Filter by department if specified
        AND (e.isActive = 0 OR @IncludeInactive = 1)       -- Include active/inactive employees based on input
    ORDER BY 
        d.deptName, e.empName;
END;

EXEC Pr_PayrollReport1 ;
EXEC Pr_PayrollReport @DepartmentId = 100;


----Department Wise Salary Distribution

create PROCEDURE Pr_DepartmentSalaryDistribution
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
        Employee e ON d.deptId = e.deptId
    GROUP BY 
        d.deptName
    ORDER BY 
        Total_Salary_Distribution DESC; -- Departments with the highest salary distribution first
END;

	exec Pr_DepartmentSalaryDistribution 
