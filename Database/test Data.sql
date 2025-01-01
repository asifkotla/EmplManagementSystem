
insert into Role (roleName) values 
('Admin'), 
('HR'), 
('Manager'), 
('Employee');

select * from Role;
--------------------------------------------------------------
insert into Department (deptName) values 
('Development'), 
('Marketing'), 
('HR'), 
('Testing'), 
('Tech Support');

select * from Department;
---------------------------------------------------------------
INSERT INTO UserInfo (empid,userName, Mobile, email, address, password, roleId)
VALUES
(115,'johnDoe', '9876543210', 'john.doe@gmail.com', 'Mumbai, India', HASHBYTES('SHA2_256', 'JohnDoe@123'), 4),
(116,'janeSmith', '7894561230', 'jane.smith@gmail.com', 'Pune, India', HASHBYTES('SHA2_256', 'JaneSmith@456'), 3),
(117,'aliceWong', '8529637410', 'alice.wong@gmail.com', 'Delhi, India', HASHBYTES('SHA2_256', 'AliceWong@789'), 2),
(118,'bobBrown', '9517538524', 'bob.brown@gmail.com', 'Chennai, India', HASHBYTES('SHA2_256', 'BobBrown@321'), 4),
(119,'mikeJones', '7418529630', 'mike.jones@gmail.com', 'Hyderabad, India', HASHBYTES('SHA2_256', 'MikeJones@654'), 1),
(120,'emmaWatson', '4561237890', 'emma.watson@gmail.com', 'Kolkata, India', HASHBYTES('SHA2_256', 'EmmaWatson@987'), 4),
(121,'chrisEvans', '3692581470', 'chris.evans@gmail.com', 'Bangalore, India', HASHBYTES('SHA2_256', 'ChrisEvans@111'), 3),
(122,'lindaBrown', '1237894560', 'linda.brown@gmail.com', 'Ahmedabad, India', HASHBYTES('SHA2_256', 'LindaBrown@222'), 2),
(123,'willSmith', '7896541230', 'will.smith@gmail.com', 'Jaipur, India', HASHBYTES('SHA2_256', 'WillSmith@333'), 1),
(124,'susanClark', '1472583690', 'susan.clark@gmail.com', 'Surat, India', HASHBYTES('SHA2_256', 'SusanClark@444'), 4);

insert into UserInfo(empId,userName, Mobile, email, address, password, roleId) values
(110,'asifK', '7276159569', 'asifkotla535@gmail.com', 'Sangli, Maharashtra',HASHBYTES('SHA2_256', 'Kotlaasif@07'), 4),
(111,'hrishiB', '1111111111', 'hrishib@gmail.com', 'Nashik, Maharashtra', HASHBYTES('SHA2_256', 'Hrishikesh@07'), 4),
(112,'nihalA', '1122334455', 'nihalatal@gmail.com', 'Sangli, Maharashtra', HASHBYTES('SHA2_256', 'NihalAtal@07'), 3),
(113,'tejasH', '1231231230', 'tejashinge@gmail.com', 'Buldhana, Maharashtra', HASHBYTES('SHA2_256', 'tejasHinge@07'), 2),
(114,'yashH', '1234512345', 'yashhinge@gmail.com', 'Buldhana, Maharashtra', HASHBYTES('SHA2_256', 'yashHinge@07'), 1);


INSERT INTO Report (ReportDate, ReportName, empId, taskId)
VALUES
('2025-01-16', 'Code Deployment Report', 110, 1),
('2025-02-21', 'Bug Fixes Summary', 111, 2),
('2025-03-11', 'Module Design Documentation', 112, 3),
('2025-04-06', 'Database Optimization Results', 113, 4),
('2025-05-13', 'UI Update Feedback', 114, 5),
('2025-06-19', 'Requirement Gathering Notes', 115, 6),
('2025-07-23', 'Documentation Review Summary', 116, 13),
('2025-08-31', 'Security Audit Findings', 117, 14),
('2025-09-16', 'Testing Enhancements Results', 118, 15),
('2025-10-26', 'Client Demo Summary', 119, 16);

select * from Employee

INSERT INTO Employee (empName, empSalary, deptId, managerId,joiningdate)
VALUES
('John Doe', 400000, 100,  110,GETDATE()),
('Jane Smith', 450000, 101,  111,GETDATE()),
('Alice Wong', 500000, 102,  112,GETDATE()),
('Bob Brown', 350000, 100,  113,GETDATE()),
('Mike Jones', 600000, 103,  111,GetDate()),
('Emma Watson', 420000, 104, 111,GETDATE()),
('Chris Evans', 470000, 101,  112,GETDATE()),
('Linda Brown', 380000, 104,  113,GETDATE()),
('Will Smith', 520000, 103, 111, GETDATE()),
('Susan Clark', 440000, 100,  110,GETDATE());
insert into Employee (empName, empSalary, deptId,managerId,joiningDate) 
values 
('Asif Kotla', 250000, 100, null,GETDATE()),  -- deptId 1 must exist in the Department table
('Hrishikesh Bhadane', 550000, 100,112,GETDATE()), 
('Tejas Hinge', 700000, 102,null,GETDATE()), 
('Yash Hinge', 430000, 101,111,GETDATE()), 
('Nihal Atal', 300000, 103,null,GETDATE()); -- Reports to Hrishikesh Bhadane (empId 111


select * from Department
select * from UserInfo






INSERT INTO Task (taskName, Description, Status, deadLine, empId)
VALUES
('Code Deployment', 'Deploy code to production server', 'Pending', '2025-01-15', 110),
('Bug Fixes', 'Resolve critical bugs in the system', 'InProgress', '2025-02-20', 111),
('Module Design', 'Design the architecture for the new module', 'Completed', '2025-03-10', 112),
('Database Optimization', 'Optimize database queries for performance', 'Pending', '2025-04-05', 113),
('UI Update', 'Revamp the user interface for the app', 'Pending', '2025-05-12', 114),
('Requirement Gathering', 'Meet with clients to gather requirements', 'InProgress', '2025-06-18', 115),
('Documentation Review', 'Review the project documentation', 'Completed', '2025-07-22', 111),
('Security Audit', 'Conduct a security audit for the system', 'Pending', '2025-08-30', 116),
('Testing Enhancements', 'Test new features before release', 'InProgress', '2025-09-15', 117),
('Client Demo', 'Provide a demo of the system to clients', 'Pending', '2025-10-25', 118);
insert into Task(taskName, Description, Status, deadLine, empId) values
('Complete Documentation', 'Make complete documentation on the final project', 'Pending', '2025-04-04', 110),
('Prepare Project Report', 'Create a detailed project report for client submission', 'Pending', '2025-05-15', 111),
('Code Review Session', 'Conduct a review session for newly implemented features', 'Pending', '2025-06-01', 112),
('Client Meeting', 'Organize and attend a meeting with the client for feedback', 'Pending', '2025-07-10', 113),
('Testing Final Module', 'Perform unit and integration testing on the final project module', 'Pending', '2025-08-20', 114),
('Update Project Plan', 'Update the project plan with current milestones and tasks', 'Pending', '2025-09-12', 114);

select * from Task
INSERT INTO Report (ReportDate, ReportName, empId, taskId)
VALUES
('2025-01-16', 'Code Deployment Report', 110, 1),
('2025-02-21', 'Bug Fixes Summary', 111, 2),
('2025-03-11', 'Module Design Documentation', 112, 3),
('2025-04-06', 'Database Optimization Results', 113, 4),
('2025-05-13', 'UI Update Feedback', 134, 5),
('2025-06-19', 'Requirement Gathering Notes', 111, 19),
('2025-07-23', 'Documentation Review Summary', 137, 20),
('2025-08-31', 'Security Audit Findings', 112, 21),
('2025-09-16', 'Testing Enhancements Results', 138, 23),
('2025-10-26', 'Client Demo Summary', 137, 24);
insert into Report( ReportDate,ReportName, empId, taskId) values
(GetDate(),'My Test Report', 111, 3);