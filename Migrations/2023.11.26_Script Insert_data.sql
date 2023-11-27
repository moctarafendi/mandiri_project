-- INSERT DATA BUSINESS AREA MASTER
INSERT INTO BusinessAreaMaster(AreaCode,AreaName,CreatedBy,ModifiedOn,ModifiedBy) VALUES('JKT-0001','JKT-0001','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO BusinessAreaMaster(AreaCode,AreaName,CreatedBy,ModifiedOn,ModifiedBy) VALUES('JKT-0002','JKT-0002','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO BusinessAreaMaster(AreaCode,AreaName,CreatedBy,ModifiedOn,ModifiedBy) VALUES('JKT-0003','JKT-0003','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO BusinessAreaMaster(AreaCode,AreaName,CreatedBy,ModifiedOn,ModifiedBy) VALUES('ALL','ALL','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')

-- INSERT DATA USER
INSERT INTO ApplicationUser(Email,Name,UserId,Password,Phone,Role,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ,Token,TokenExpireDate) VALUES('moctarafendi@gmail.com','ADMINISTRATOR','ADMINISTRATOR','/R+jR992c7LN7W72pQKRpAauskpe++qyvfa0h8f0/YWbSyqUfzjxIpWlHBcBB1Tr','08568056801','ADMINISTRATOR','ALL','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR','',GETDATE())
INSERT INTO ApplicationUser(Email,Name,UserId,Password,Phone,Role,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ,Token,TokenExpireDate) VALUES('moctarafendi1@gmail.com','Reyhan','ADMIN-0001','NXx0fjQqAIsjEtVx4TazfHt+LvHMvyp48uWpGwDf59wRjDaTplbjaq9utwPre3fg','087634568345','ADMIN','JKT-0001','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR','',GETDATE())
INSERT INTO ApplicationUser(Email,Name,UserId,Password,Phone,Role,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ,Token,TokenExpireDate) VALUES('moctarafendi1@gmail.com','Bob','ADMIN-0002','t/K04ZKQpU4mEkL1+8p1SR0BV3IFXQ+Rs1LLEnlosNcZwmcHOwt1XSYrTPWJOOyW','08763324324','ADMIN','JKT-0002','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR','',GETDATE())
INSERT INTO ApplicationUser(Email,Name,UserId,Password,Phone,Role,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ,Token,TokenExpireDate) VALUES('moctarafendi1@gmail.com','Sarah','ADMIN-0003','zeQRO+/Q3l9/MsCo81/kTPiWYNS1Z05qrrYQwaPWGXpfaC223dNwIbypS7uSIDu6','08763456569','ADMIN','JKT-0003','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR','',GETDATE())


--INSERT DATA EMPLOYEE
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000001','John','Doe','2022-01-01',NULL,5000000,'JKT-0001','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000002','Ramon','Carla','2022-02-02',NULL,5000000,'JKT-0002','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000003','Agnes','Sand','2022-03-03',NULL,5000000,'JKT-0001','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000004','Joy','Mitra','2022-04-04',NULL,5000000,'JKT-0003','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000005','Jona','Jan','2022-05-05',NULL,5000000,'JKT-0003','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000006','Joni','Indo','2022-06-06','2023-06-06',5000000,'JKT-0002','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000007','Agung','Hercules','2023-01-07',NULL,4000000,'JKT-0001','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000008','Herman','Li','2023-01-08',NULL,4000000,'JKT-0001','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000009','Rudi','Salim','2023-01-09',NULL,4000000,'JKT-0002','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000010','Budi','Santoso','2023-01-10',NULL,4000000,'JKT-0003','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000011','Santoso','Ahmad','2023-01-11',NULL,4000000,'JKT-0003','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000012','Silvia','Fitria','2021-01-12','2022-01-12',7000000,'JKT-0001','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000013','Riki','Ahmad','2021-01-13',NULL,7000000,'JKT-0001','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000014','Rahman','Liko','2021-01-14',NULL,7000000,'JKT-0002','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000015','Arman','Maulana','2021-01-15',NULL,7000000,'JKT-0003','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000016','July','Juleha','2017-01-16',NULL,10000000,'JKT-0001','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000017','Carlos','Santana','2017-01-17',NULL,10000000,'JKT-0001','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000018','Juan','Salim','2017-01-18',NULL,10000000,'JKT-0002','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')
INSERT INTO Employee(EmployeeNo,FirstName ,LastName ,HireDate ,TerminationDate,Salary ,BusinessAreaCode,CreatedBy ,ModifiedOn ,ModifiedBy ) VALUES('100020003000019','Santi','Salim','2017-01-19',NULL,10000000,'JKT-0003','ADMINISTRATOR',GETDATE(),'ADMINISTRATOR')


--INSERT DATA ANNUAL REVIEW
INSERT INTO AnnualReview(ID ,EmployeeNo,ReviewDate ,CreatedBy ,ModifiedOn ,ModifiedBy ,IsDelete ) VALUES(LOWER(NEWID()),'100020003000001','2023-12-20','ADMIN-0001',GETDATE(),'ADMIN-0001',0)
INSERT INTO AnnualReview(ID ,EmployeeNo,ReviewDate ,CreatedBy ,ModifiedOn ,ModifiedBy ,IsDelete ) VALUES(LOWER(NEWID()),'100020003000003','2023-12-21','ADMIN-0001',GETDATE(),'ADMIN-0001',0)
INSERT INTO AnnualReview(ID ,EmployeeNo,ReviewDate ,CreatedBy ,ModifiedOn ,ModifiedBy ,IsDelete ) VALUES(LOWER(NEWID()),'100020003000007','2023-12-22','ADMIN-0001',GETDATE(),'ADMIN-0001',0)
INSERT INTO AnnualReview(ID ,EmployeeNo,ReviewDate ,CreatedBy ,ModifiedOn ,ModifiedBy ,IsDelete ) VALUES(LOWER(NEWID()),'100020003000006','2023-12-20','ADMIN-0002',GETDATE(),'ADMIN-0002',0)
INSERT INTO AnnualReview(ID ,EmployeeNo,ReviewDate ,CreatedBy ,ModifiedOn ,ModifiedBy ,IsDelete ) VALUES(LOWER(NEWID()),'100020003000014','2023-12-21','ADMIN-0002',GETDATE(),'ADMIN-0002',0)
INSERT INTO AnnualReview(ID ,EmployeeNo,ReviewDate ,CreatedBy ,ModifiedOn ,ModifiedBy ,IsDelete ) VALUES(LOWER(NEWID()),'100020003000018','2023-12-22','ADMIN-0002',GETDATE(),'ADMIN-0002',0)
INSERT INTO AnnualReview(ID ,EmployeeNo,ReviewDate ,CreatedBy ,ModifiedOn ,ModifiedBy ,IsDelete ) VALUES(LOWER(NEWID()),'100020003000004','2023-12-20','ADMIN-0003',GETDATE(),'ADMIN-0003',0)
INSERT INTO AnnualReview(ID ,EmployeeNo,ReviewDate ,CreatedBy ,ModifiedOn ,ModifiedBy ,IsDelete ) VALUES(LOWER(NEWID()),'100020003000005','2023-12-21','ADMIN-0003',GETDATE(),'ADMIN-0003',0)
INSERT INTO AnnualReview(ID ,EmployeeNo,ReviewDate ,CreatedBy ,ModifiedOn ,ModifiedBy ,IsDelete ) VALUES(LOWER(NEWID()),'100020003000010','2023-12-22','ADMIN-0003',GETDATE(),'ADMIN-0003',0)
