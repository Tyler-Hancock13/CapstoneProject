USE master;
GO

-- Dropping Connections --
ALTER	DATABASE Cryptech
SET 	SINGLE_USER
WITH	ROLLBACK IMMEDIATE
GO

-- Deleting Old Database if One Exists --
DROP DATABASE IF EXISTS Cryptech;

-- Creating Database --
CREATE DATABASE Cryptech;
GO

USE Cryptech;
GO

-- Creating Departments Table --
CREATE TABLE Departments
(
	ID INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(20) NOT NULL,
	[Description] NVARCHAR(255) NOT NULL,
	InvocationDate DATETIME NOT NULL,
	[Version] ROWVERSION NOT NULL
);
GO

-- Creating Stored Procedures for Departments --
CREATE OR ALTER PROC Departments_GetAll
AS
BEGIN
	SELECT * FROM Departments;
END
GO

CREATE OR ALTER PROC Departments_GetByID
	@ID INT
AS
BEGIN
	SELECT * FROM Departments WHERE ID = @ID;
END
GO

CREATE OR ALTER PROC Departments_Insert
	@Version ROWVERSION OUT,
	@Name NVARCHAR(20),
	@Description NVARCHAR(255),
	@InvocationDate DATETIME
AS
BEGIN
	INSERT INTO
		Departments
	(
		[Name],
		[Description],
		InvocationDate
	)
	VALUES
	(
		@Name,
		@Description,
		@InvocationDate
	);

	SET @Version = (SELECT [Version] FROM Departments WHERE ID = @@IDENTITY);
END
GO

CREATE OR ALTER PROC Department_Update
	@ID	INT,
	@Version ROWVERSION OUT,
	@Name NVARCHAR(20),
	@Description NVARCHAR(255),
	@InvocationDate DATETIME
AS
BEGIN
	IF (SELECT [Version] FROM Departments WHERE ID = @ID) = @Version
	BEGIN
		UPDATE 
			Departments
		SET
			[Name] = @Name,
			[Description] = @Description,
			InvocationDate = @InvocationDate
		WHERE 
			ID = @ID;

		SET @Version = (SELECT [Version] FROM Departments WHERE ID = @ID);
	END
END
GO

CREATE OR ALTER PROC Department_Delete
	@ID INT
AS
BEGIN
	DELETE FROM Departments WHERE ID = @ID;
END
GO

-- Creating Test Data for Departments --
DECLARE @Version ROWVERSION;

EXEC Departments_Insert 
	@Version,
	'Human Resources', 
	'The department that takes care of hiring, administration, and training of personnel.', 
	'20200520 12:00:00 AM';
	
EXEC Departments_Insert 
	@Version,
	'Quality Assurance', 
	'Our QA team ensures that all of our software functions properly and reports any issues to the development team.', 
	'20200520 12:00:00 AM';
	
EXEC Departments_Insert 
	@Version,
	'IT', 
	'Our IT team ensures that all of our technology functions properly and is properly maintained.', 
	'20200520 12:00:00 AM';

-- Creating Jobs Table --
CREATE TABLE Jobs
(
	ID INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(20) NOT NULL
);
GO

-- Creating Stored Procedures for Jobs --
CREATE OR ALTER PROC Jobs_GetAll
AS
BEGIN
	SELECT * FROM Jobs;
END
GO

CREATE OR ALTER PROC Jobs_GetByID
	@ID INT
AS
BEGIN
	SELECT * FROM Jobs WHERE ID = @ID;
END
GO

CREATE OR ALTER PROC Jobs_Insert
	@Name NVARCHAR(20)
AS
BEGIN
	INSERT INTO
		Jobs
	VALUES
	(
		@Name
	);
END
GO

-- Creating Test Data for Jobs --
EXEC Jobs_Insert 'Hiring Manager';
EXEC Jobs_Insert 'Director';
EXEC Jobs_Insert 'Automation Tester';
EXEC Jobs_Insert 'Analyst';
EXEC Jobs_Insert 'Manager';

-- Creating Employees Table --
CREATE TABLE Employees
(
	ID VARCHAR(8) NOT NULL PRIMARY KEY,
	FirstName NVARCHAR(35) NOT NULL,
	LastName NVARCHAR(35) NOT NULL,
	MiddleInitial NVARCHAR(1) NULL,
	DateOfBirth DATETIME NOT NULL,
	StreetAddress NVARCHAR(50) NOT NULL,
	City NVARCHAR(25) NOT NULL,
	PostalCode NVARCHAR(6) NOT NULL,
	[SIN] NVARCHAR(11) NOT NULL,
	SeniorityDate DATETIME NOT NULL,
	JobStartDate DATETIME NOT NULL,
	WorkPhone NVARCHAR(17) NULL,
	CellPhone NVARCHAR(17) NULL,
	Email NVARCHAR(255) NOT NULL,
	OfficeAddress NVARCHAR(50) NOT NULL,
	OfficeCity NVARCHAR(25) NOT NULL,
	OfficeUnit INT NULL,
	EndDate DATETIME NULL,
	[Version] ROWVERSION NOT NULL,
	StatusID INT NOT NULL,
	JobID INT NOT NULL FOREIGN KEY REFERENCES Jobs(ID),
	SupervisorID VARCHAR(8) NULL FOREIGN KEY REFERENCES Employees(ID),
	DepartmentID INT NOT NULL FOREIGN KEY REFERENCES Departments(ID)
);
GO

-- Creating Function to Generate 8-Digit Employee ID --
CREATE FUNCTION Employee_GenerateID()
RETURNS VARCHAR(8)
AS
BEGIN
	IF (SELECT COUNT(*) FROM Employees) != 0
	BEGIN
		RETURN RIGHT
		('00000000' + 
			-- Subquery --
			(
				-- Casting INT to VARCHAR --
				SELECT CAST
				(
					-- Subquery --
					(
						-- Casting VARCHAR ID to INT and Incrementing It --
						SELECT CAST
						(
							-- Subquery --
							(
								(
									SELECT MAX
									(
										ID
									) 
									FROM 
										Employees
								)
							) 
							AS INT
						)			
						+ 1
					) 
					AS VARCHAR(8)
				)
			),
			8
		);
	END
	RETURN '00000001';
END
GO

-- Creating Users Table --
CREATE TABLE Users
(
	ID INT NOT NULL PRIMARY KEY IDENTITY,
	EmployeeID VARCHAR(8) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
	[Password] NVARCHAR(32) NOT NULL,
	RoleID INT NOT NULL
);
GO

-- Creating Stored Procedures for Users --
CREATE OR ALTER PROC User_GetByEmployeeID
	@ID VARCHAR(8)
AS
BEGIN
	SELECT * FROM Users WHERE EmployeeID = @ID;
END
GO

CREATE OR ALTER PROC User_GetByPasswordAndEmployeeID
	@ID VARCHAR(8),
	@Password NVARCHAR(32)
AS
BEGIN
	SELECT * FROM Users WHERE EmployeeID = @ID AND [Password] = @Password;
END
GO

CREATE OR ALTER PROC User_Insert
	@EmployeeID VARCHAR(8),
	@Password NVARCHAR(32),
	@RoleID INT
AS
BEGIN
	INSERT INTO	
		Users
	VALUES
	(
		@EmployeeID,
		@Password,
		@RoleID
	);
END
GO
GO

-- Creating Stored Procedures for Employees --
CREATE OR ALTER PROC Employee_Insert
	@ID VARCHAR(8) OUT,
	@Version ROWVERSION OUT,
	@FirstName NVARCHAR(35),
	@LastName NVARCHAR(35),
	@MiddleInitial NVARCHAR(1) NULL,
	@DateOfBirth DATETIME,
	@StreetAddress NVARCHAR(50),
	@City NVARCHAR(25),
	@PostalCode NVARCHAR(6),
	@SIN NVARCHAR(11),
	@SeniorityDate DATETIME,
	@JobStartDate DATETIME,
	@WorkPhone NVARCHAR(17),
	@CellPhone NVARCHAR(17),
	@Email NVARCHAR(255),
	@OfficeAddress NVARCHAR(50),
	@OfficeCity NVARCHAR(25),
	@OfficeUnit INT NULL,
	@EndDate DATETIME NULL,
	@StatusID INT,
	@JobID INT,
	@SupervisorID VARCHAR(8) NULL,
	@DepartmentID INT,
	@Password NVARCHAR(32),
	@RoleID INT
AS
BEGIN
	BEGIN TRANSACTION;
	BEGIN TRY
		SET @ID = dbo.Employee_GenerateID();
	
		INSERT INTO 
			Employees
		(
			ID,
			FirstName,
			LastName,
			MiddleInitial,
			DateOfBirth,
			StreetAddress,
			City,
			PostalCode,
			[SIN],
			SeniorityDate,
			JobStartDate,
			WorkPhone,
			CellPhone,
			Email,
			OfficeAddress,
			OfficeCity,
			OfficeUnit,
			EndDate,
			StatusID,
			JobID,
			SupervisorID,
			DepartmentID
		)
		VALUES 
		(
			@ID,
			@FirstName,
			@LastName,
			@MiddleInitial,
			@DateOfBirth,
			@StreetAddress,
			@City,
			@PostalCode,
			@SIN,
			@SeniorityDate,
			@JobStartDate,
			@WorkPhone,
			@CellPhone,
			@Email,
			@OfficeAddress,
			@OfficeCity,
			@OfficeUnit,
			@EndDate,
			@StatusID,
			@JobID,
			@SupervisorID,
			@DepartmentID
		);

		INSERT INTO 
			Users
		VALUES
		(
			@ID,
			@Password,
			@RoleID
		);

		SET @Version = (SELECT [Version] FROM Employees WHERE ID = @ID);

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
	END CATCH
END
GO

CREATE OR ALTER PROC Employee_Update
	@ID VARCHAR(8),
	@Version ROWVERSION OUT,
	@FirstName NVARCHAR(35),
	@LastName NVARCHAR(35),
	@MiddleInitial NVARCHAR(1) NULL,
	@DateOfBirth DATETIME,
	@StreetAddress NVARCHAR(50),
	@City NVARCHAR(25),
	@PostalCode NVARCHAR(6),
	@SIN NVARCHAR(11),
	@SeniorityDate DATETIME,
	@JobStartDate DATETIME,
	@WorkPhone NVARCHAR(17),
	@CellPhone NVARCHAR(17),
	@Email NVARCHAR(255),
	@OfficeAddress NVARCHAR(50),
	@OfficeCity NVARCHAR(25),
	@OfficeUnit INT NULL,
	@EndDate DATETIME NULL,
	@StatusID INT,
	@JobID INT,
	@SupervisorID VARCHAR(8) NULL,
	@DepartmentID INT
AS
BEGIN
	IF (SELECT [Version] FROM Employees WHERE ID = @ID) = @Version
	BEGIN
		UPDATE 
			Employees
		SET
			FirstName = @FirstName,
			LastName = @LastName,
			MiddleInitial = @MiddleInitial,
			DateOfBirth = @DateOfBirth,
			StreetAddress = @StreetAddress,
			City = @City,
			PostalCode = @PostalCode,
			[SIN] = @SIN,
			SeniorityDate = @SeniorityDate,
			JobStartDate = @JobStartDate,
			WorkPhone = @WorkPhone,
			CellPhone = @CellPhone,
			Email = @Email,
			OfficeAddress = @OfficeAddress,
			OfficeCity = @OfficeCity,
			OfficeUnit = @OfficeUnit,
			EndDate = @EndDate,
			StatusID = @StatusID,
			JobID = @JobID,
			SupervisorID = @SupervisorID,
			DepartmentID = @DepartmentID
		WHERE 
			ID = @ID;

		SET @Version = (SELECT [Version] FROM Employees WHERE ID = @ID);
	END
END
GO

CREATE OR ALTER PROC Employee_User_Update
	@ID VARCHAR(8),
	@Version ROWVERSION OUT,
	@FirstName NVARCHAR(35),
	@LastName NVARCHAR(35),
	@MiddleInitial NVARCHAR(1) NULL,
	@DateOfBirth DATETIME,
	@StreetAddress NVARCHAR(50),
	@City NVARCHAR(25),
	@PostalCode NVARCHAR(6),
	@SIN NVARCHAR(11),
	@SeniorityDate DATETIME,
	@JobStartDate DATETIME,
	@WorkPhone NVARCHAR(17),
	@CellPhone NVARCHAR(17),
	@Email NVARCHAR(255),
	@OfficeAddress NVARCHAR(50),
	@OfficeCity NVARCHAR(25),
	@OfficeUnit INT NULL,
	@EndDate DATETIME NULL,
	@StatusID INT,
	@JobID INT,
	@SupervisorID VARCHAR(8) NULL,
	@DepartmentID INT,
	@RoleID INT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

		IF (SELECT [Version] FROM Employees WHERE ID = @ID) = @Version
		BEGIN
			UPDATE 
				Employees
			SET
				FirstName = @FirstName,
				LastName = @LastName,
				MiddleInitial = @MiddleInitial,
				DateOfBirth = @DateOfBirth,
				StreetAddress = @StreetAddress,
				City = @City,
				PostalCode = @PostalCode,
				[SIN] = @SIN,
				SeniorityDate = @SeniorityDate,
				JobStartDate = @JobStartDate,
				WorkPhone = @WorkPhone,
				CellPhone = @CellPhone,
				Email = @Email,
				OfficeAddress = @OfficeAddress,
				OfficeCity = @OfficeCity,
				OfficeUnit = @OfficeUnit,
				EndDate = @EndDate,
				StatusID = @StatusID,
				JobID = @JobID,
				SupervisorID = @SupervisorID,
				DepartmentID = @DepartmentID
			WHERE 
				ID = @ID;

			IF (SELECT [Version] FROM Employees WHERE ID = @ID) != @Version
			BEGIN
				UPDATE 
					Users
				SET
					RoleID = @RoleID
				WHERE
					EmployeeID = @ID;
			END
		END

		SET @Version = (SELECT [Version] FROM Employees WHERE ID = @ID);
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
	END CATCH
END
GO

CREATE OR ALTER PROC Employee_SearchByID
	@ID	VARCHAR(8)
AS
BEGIN
	SELECT * FROM Employees WHERE ID = @ID;
END
GO

CREATE OR ALTER PROC Employee_SearchByLastName
	@LastName NVARCHAR(35)
AS
BEGIN
	SELECT * FROM Employees WHERE LastName LIKE CONCAT('%', @LastName, '%');
END
GO

CREATE OR ALTER PROC Employee_GetSupervisors
AS
BEGIN
	SELECT
		*
	FROM
		Employees
	WHERE
		ID
	IN
		(
			SELECT 
				EmployeeID
			FROM
				Users
			WHERE
				RoleID = 0 
			OR 
				RoleID = 1
		)
END
GO

CREATE OR ALTER PROC Employee_GetSupervisorById
@SupervisorId VARCHAR(8)
AS
BEGIN
	SELECT * FROM Employees WHERE ID = @SupervisorId
END
GO

CREATE OR ALTER PROC Employee_GetHRStaff
AS
BEGIN
	SELECT
		*
	FROM
		Employees
	WHERE
		ID
	IN
		(
			SELECT 
				EmployeeID
			FROM
				Users
			WHERE
				RoleID = 0 
			OR 
				RoleID = 2
		)
END
GO

CREATE OR ALTER PROC Employee_GetAll
AS
BEGIN
	SELECT * FROM Employees;
END
GO

CREATE OR ALTER PROC Employee_GetByDepartment
	@DepartmentID INT
AS
BEGIN
	SELECT * FROM Employees WHERE DepartmentID = @DepartmentID;
END
GO

CREATE OR ALTER PROC Employee_GetBySupervisor
	@SupervisorID VARCHAR(8)
AS
BEGIN
	SELECT * FROM Employees WHERE SupervisorID = @SupervisorID;
END
GO

-- Creating Test Data for Employees --
DECLARE @Now DATETIME;
SET @Now = GETDATE();

DECLARE @TwoDaysFromNow DATETIME;
SET @TwoDaysFromNow = DATEADD(DAY, 2, GETDATE());

DECLARE @ID VARCHAR(8);
DECLARE @Version ROWVERSION;
DECLARE @Password NVARCHAR(32);
SET @Password = CONVERT(NVARCHAR(32), HashBytes('MD5', 'password'), 2);

EXEC Employee_Insert 
	@ID,
	@Version,
	@FirstName = 'John', 
	@LastName = 'Doe', 
	@MiddleInitial = NULL, 
	@DateOfBirth = '19980618 12:00:00 AM', 
	@StreetAddress = '243 Main Street', 
	@City = 'Moncton', 
	@PostalCode = 'E1C2J2',
	@SIN = '252-876-334',
	@SeniorityDate = @Now,
	@JobStartDate = @TwoDaysFromNow,
	@WorkPhone = '1-506-522-8254',
	@CellPhone = '1-506-237-4227',
	@Email = 'johndoe@gmail.com',
	@OfficeAddress = '1234 Mountain Rd',
	@OfficeCity = 'Moncton',
	@OfficeUnit = NULL,
	@EndDate = NULL,
	@StatusID = 0,
	@JobID = 1,
	@SupervisorID = NULL,
	@DepartmentID = 1,
	@Password = @Password,
	@RoleID = 0;

EXEC Employee_Insert 
	@ID,
	@Version,
	@FirstName = 'Jane', 
	@LastName = 'Doe', 
	@MiddleInitial = 'H', 
	@DateOfBirth = '19920324 12:00:00 AM', 
	@StreetAddress = '243 Main Street', 
	@City = 'Moncton', 
	@PostalCode = 'E1C2J2',
	@SIN = '866-293-736',
	@SeniorityDate = @Now,
	@JobStartDate = @TwoDaysFromNow,
	@WorkPhone = '1-506-237-7223',
	@CellPhone = '1-506-507-1275',
	@Email = 'jane.doe@gmail.com',
	@OfficeAddress = '1864 Main St',
	@OfficeCity = 'Moncton',
	@OfficeUnit = 12,
	@EndDate = NULL,
	@StatusID = 0,
	@JobID = 2,
	@SupervisorID = NULL,
	@DepartmentID = 2,
	@Password = @Password,
	@RoleID = 1;

EXEC Employee_Insert 
	@ID,
	@Version,
	@FirstName = 'Bob', 
	@LastName = 'Doe', 
	@MiddleInitial = NULL, 
	@DateOfBirth = '19780113 12:00:00 AM', 
	@StreetAddress = '873 St.George Blvd', 
	@City = 'Moncton', 
	@PostalCode = 'E1C3E6',
	@SIN = '645-194-756',
	@SeniorityDate = @Now,
	@JobStartDate = @TwoDaysFromNow,
	@WorkPhone = '1-506-873-2547',
	@CellPhone = '1-506-996-7564',
	@Email = 'bob_doe@hotmail.com',
	@OfficeAddress = '524 Lutz St',
	@OfficeCity = 'Moncton',
	@OfficeUnit = 2,
	@EndDate = NULL,
	@StatusID = 1,
	@JobID = 2,
	@SupervisorID = '00000002',
	@DepartmentID = 2,
	@Password = @Password,
	@RoleID = 2;

EXEC Employee_Insert 
	@ID,
	@Version,
	@FirstName = 'Jeff', 
	@LastName = 'Aryah', 
	@MiddleInitial = NULL, 
	@DateOfBirth = '19830716 12:00:00 AM', 
	@StreetAddress = '46 Killam Drive', 
	@City = 'Moncton', 
	@PostalCode = 'E1C2K7',
	@SIN = '654-837-746',
	@SeniorityDate = @Now,
	@JobStartDate = @TwoDaysFromNow,
	@WorkPhone = '1-506-665-6354',
	@CellPhone = '1-506-442-3287',
	@Email = 'jeffdoe83@yahoo.com',
	@OfficeAddress = '1522 Acadie Ave',
	@OfficeCity = 'Dieppe',
	@OfficeUnit = 125,
	@EndDate = NULL,
	@StatusID = 0,
	@JobID = 2,
	@SupervisorID = '00000001',
	@DepartmentID = 1,
	@Password = @Password,
	@RoleID = 3;

EXEC Employee_Insert 
	@ID,
	@Version,
	@FirstName = 'Bill', 
	@LastName = 'Lapointe', 
	@MiddleInitial = 'N', 
	@DateOfBirth = '19860928 12:00:00 AM', 
	@StreetAddress = '334 Morton Ave', 
	@City = 'Moncton', 
	@PostalCode = 'E1C5G9',
	@SIN = '534-205-937',
	@SeniorityDate = @Now,
	@JobStartDate = @TwoDaysFromNow,
	@WorkPhone = '1-506-939-3474',
	@CellPhone = '1-506-298-9253',
	@Email = 'billydoe@gmail.com',
	@OfficeAddress = '925 Champlain St',
	@OfficeCity = 'Dieppe',
	@OfficeUnit = 12,
	@EndDate = NULL,
	@StatusID = 0,
	@JobID = 2,
	@SupervisorID = '00000001',
	@DepartmentID = 1,
	@Password = @Password,
	@RoleID = 2;

EXEC Employee_Insert 
	@ID,
	@Version,
	@FirstName = 'Jerry', 
	@LastName = 'Doe', 
	@MiddleInitial = NULL, 
	@DateOfBirth = '19630624 12:00:00 AM', 
	@StreetAddress = '113 Oakland Ave', 
	@City = 'Moncton', 
	@PostalCode = 'E1C6L2',
	@SIN = '837-284-980',
	@SeniorityDate = '19890514 12:00:00 AM',
	@JobStartDate = @TwoDaysFromNow,
	@WorkPhone = '1-506-643-2727',
	@CellPhone = NULL,
	@Email = 'billydoe@gmail.com',
	@OfficeAddress = '925 Champlain St',
	@OfficeCity = 'Dieppe',
	@OfficeUnit = 17,
	@EndDate = NULL,
	@StatusID = 0,
	@JobID = 2,
	@SupervisorID = '00000001',
	@DepartmentID = 1,
	@Password = @Password,
	@RoleID = 2;

EXEC Employee_Insert 
	@ID,
	@Version,
	@FirstName = 'Steve', 
	@LastName = 'Jobs', 
	@MiddleInitial = NULL, 
	@DateOfBirth = '19770420 12:00:00 AM', 
	@StreetAddress = '877 Whitney Ave', 
	@City = 'Moncton', 
	@PostalCode = 'E1C5K9',
	@SIN = '736-984-225',
	@SeniorityDate = @Now,
	@JobStartDate = @TwoDaysFromNow,
	@WorkPhone = '1-506-726-7826',
	@CellPhone = '1-506-783-9263',
	@Email = 'steve.jobs@hotmail.com',
	@OfficeAddress = '728 Coverdale Rd',
	@OfficeCity = 'Riverview',
	@OfficeUnit = 5,
	@EndDate = NULL,
	@StatusID = 0,
	@JobID = 1,
	@SupervisorID = NULL,
	@DepartmentID = 2,
	@Password = @Password,
	@RoleID = 1;

EXEC Employee_Insert 
	@ID,
	@Version,
	@FirstName = 'Eddie', 
	@LastName = 'Vedder', 
	@MiddleInitial = NULL, 
	@DateOfBirth = '19811116 12:00:00 AM', 
	@StreetAddress = '225 Joyce Ave', 
	@City = 'Moncton', 
	@PostalCode = 'E1C7E6',
	@SIN = '736-924-935',
	@SeniorityDate = @Now,
	@JobStartDate = @TwoDaysFromNow,
	@WorkPhone = '1-506-726-8347',
	@CellPhone = '1-506-783-2398',
	@Email = 'eddiie.vedder@outlook.com',
	@OfficeAddress = '728 Coverdale Rd',
	@OfficeCity = 'Riverview',
	@OfficeUnit = 11,
	@EndDate = NULL,
	@StatusID = 0,
	@JobID = 1,
	@SupervisorID = '00000007',
	@DepartmentID = 2,
	@Password = @Password,
	@RoleID = 1;
GO

-- Creating Reviews Table --
CREATE TABLE Reviews 
(
	ID INT NOT NULL PRIMARY KEY IDENTITY,
	[Date] DATETIME NOT NULL,
	RatingID INT NOT NULL,
	Comment NVARCHAR(255) NOT NULL,
	EmployeeID VARCHAR(8) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
	SupervisorID VARCHAR(8) NOT NULL FOREIGN KEY REFERENCES Employees(ID)
)
GO

-- Creating Stored Procedures for Reviews --
CREATE OR ALTER PROC Reviews_Insert
	@Date DATETIME,
	@RatingID INT,
	@Comment NVARCHAR(255),
	@EmployeeID VARCHAR(8),
	@SupervisorID VARCHAR(8)
AS
BEGIN
	INSERT INTO
		Reviews
	VALUES
	(
		@Date,
		@RatingID,
		@Comment,
		@EmployeeID,
		@SupervisorID
	);
END
GO

CREATE OR ALTER PROC Reviews_GetByEmployee
	@EmployeeID VARCHAR(8)
AS
BEGIN
	SELECT * FROM Reviews WHERE EmployeeID = @EmployeeID;
END
GO

-- Creatin Test Data for Reviews --
EXEC Reviews_Insert
	@Date = '20190105 12:00:00 AM',
	@RatingID = 2,
	@Comment = 'Fantastic Job!',
	@EmployeeID = '00000004',
	@SupervisorID = '00000001';
	
EXEC Reviews_Insert
	@Date = '20190413 12:00:00 AM',
	@RatingID = 1,
	@Comment = 'Sufficient.',
	@EmployeeID = '00000004',
	@SupervisorID = '00000001';

EXEC Reviews_Insert
	@Date = '20190725 12:00:00 AM',
	@RatingID = 0,
	@Comment = 'You can do better!',
	@EmployeeID = '00000004',
	@SupervisorID = '00000001';

EXEC Reviews_Insert
	@Date = '20190718 12:00:00 AM',
	@RatingID = 2,
	@Comment = 'Great work!',
	@EmployeeID = '00000008',
	@SupervisorID = '00000007';
GO

-- Creating Reminders Table --
CREATE TABLE Reminders
(
	ID INT NOT NULL PRIMARY KEY IDENTITY,
	[Date] DATETIME NOT NULL
);
GO

-- Creating Stored Procedures for Reminders --
CREATE OR ALTER PROC Reminders_Insert
	@Date DATETIME
AS
BEGIN
	INSERT INTO
		Reminders
	VALUES (
		@Date
	);
END
GO

CREATE OR ALTER PROC Reminders_GetMostRecent
AS
BEGIN
	SELECT TOP 1 * FROM Reminders ORDER BY [Date] DESC;
END
GO

--Create table for Status--

CREATE TABLE Status (
	Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(15) NOT NULL
)
GO

--Create data for Status
INSERT INTO Status(Name)VALUES('Pending')
INSERT INTO Status(Name)VALUES('Approved')
INSERT INTO Status(Name)VALUES('Denied')
INSERT INTO Status(Name)VALUES('Under Review')
INSERT INTO Status(Name)VALUES('Closed')

--Create table for PurchaseOrders--

CREATE TABLE PurchaseOrders(
	PONumber INT NOT NULL PRIMARY KEY IDENTITY(00001, 1),
	[ItemsSubtotal] MONEY NOT NULL,
	SalesTax MONEY NOT NULL,
	Total MONEY NOT NULL,
	DateCreated DATETIME NOT NULL,
	EmployeeId VARCHAR(8) NOT NULL FOREIGN KEY REFERENCES Employees(ID),
	SupervisorId VARCHAR(8) NULL,
	DepartmentsId INT NOT NULL FOREIGN KEY REFERENCES Departments(ID),
	Status INT NOT NULL FOREIGN KEY REFERENCES Status(Id)
)
GO

--Create stored procedures for PurchaseOrder--

CREATE OR ALTER PROC PurchaseOrder_InsertItemAndRequest
@Timestamp TIMESTAMP OUTPUT,
@ItemId INT OUTPUT,
@Id INT OUTPUT,
@ItemsSubtotal MONEY,
@SalesTax MONEY,
@Total MONEY,
@DateCreated DATETIME,
@EmployeeId VARCHAR(8),
@DepartmentsId INT,
@Status INT,
@Name VARCHAR(20),
@Description VARCHAR(50),
@Quantity INT,
@Price MONEY,
@Justification VARCHAR(50),
@Location VARCHAR(20),
@Subtotal MONEY,
@ItemStatus INT
AS
BEGIN TRANSACTION
BEGIN TRY
	INSERT INTO PurchaseOrders(ItemsSubtotal, SalesTax, Total, DateCreated, EmployeeId, DepartmentsId, Status) VALUES(@ItemsSubtotal, @SalesTax, @Total, @DateCreated, @EmployeeId, @DepartmentsId, @Status)

	SET @Id = SCOPE_IDENTITY();
	INSERT INTO PurchaseOrderItems(Name, Description, Quantity, Price, Justification, Location, Subtotal, PONumber, Status) VALUES(@Name, @Description, @Quantity, @Price, @Justification, @Location, @Subtotal, @Id, @ItemStatus)
	
	SET @ItemId = SCOPE_IDENTITY();
	SET @Timestamp = (SELECT Timestamp FROM PurchaseOrderItems WHERE Id = SCOPE_IDENTITY())

COMMIT TRANSACTION
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
	PRINT 'Rolling back transaction';
END CATCH
GO

CREATE OR ALTER PROC PurchaseOrder_Insert
	@Id INT OUT,
	@ItemsSubtotal MONEY,
	@SalesTax MONEY,
	@Total MONEY,
	@DateCreated DATETIME,
	@EmployeeId VARCHAR(8),
	@DepartmentsId INT,
	@Status INT
AS
BEGIN
	INSERT INTO PurchaseOrders(ItemsSubtotal, SalesTax, Total, DateCreated, EmployeeId, DepartmentsId, Status)
	VALUES(
		@ItemsSubtotal,
		@SalesTax,
		@Total,
		@DateCreated,
		@EmployeeId,
		@DepartmentsId,
		@Status
	);
END
GO

CREATE OR ALTER PROC PurchaseOrder_Update
@PONumber INT OUTPUT,
@ItemsSubtotal MONEY,
@SalesTax MONEY,
@Total MONEY,
@Status INT
AS
BEGIN
	UPDATE PurchaseOrders SET ItemsSubtotal = @ItemsSubtotal, SalesTax = @SalesTax, Total = @Total, Status = @Status WHERE PONumber = @PONumber
END
GO

CREATE OR ALTER PROC PurchaseOrder_SelectById
@EmployeeId VARCHAR(8)
AS
BEGIN
	SELECT PONumber, ItemsSubtotal, SalesTax, Total, DateCreated, PurchaseOrders.EmployeeId, PurchaseOrders.DepartmentsId, PurchaseOrders.Status, Employees.FirstName + ' ' + Employees.LastName AS EmployeeName, Departments.Name AS Name
	 FROM PurchaseOrders INNER JOIN Employees ON PurchaseOrders.EmployeeId = Employees.ID INNER JOIN Departments ON Employees.DepartmentID = Departments.ID WHERE EmployeeId = @EmployeeId ORDER BY DateCreated DESC
END
GO

CREATE OR ALTER PROC PurchaseOrder_GetByOldestDate
@EmployeeId VARCHAR(8)
AS
BEGIN
		SELECT PONumber, ItemsSubtotal, SalesTax, Total, DateCreated, PurchaseOrders.EmployeeId, PurchaseOrders.DepartmentsId, PurchaseOrders.Status, Employees.FirstName + ' ' + Employees.LastName AS EmployeeName, Departments.Name AS Name
	 FROM PurchaseOrders INNER JOIN Employees ON PurchaseOrders.EmployeeId = Employees.ID INNER JOIN Departments ON Employees.DepartmentID = Departments.ID WHERE EmployeeId = @EmployeeId ORDER BY DateCreated ASC
END
GO

CREATE OR ALTER PROC PurchaseOrder_GetByPOID
@PONumber INT
AS
BEGIN
	SELECT * FROM PurchaseOrders WHERE PONumber = @PONumber
END
GO

CREATE OR ALTER PROC PurchaseOrder_SelectAll
AS
BEGIN
	SELECT * FROM PurchaseOrders
END
GO

CREATE OR ALTER PROC PurchaseOrder_SearchByPOID
@PONumber INT,
@EmployeeId VARCHAR(8)
AS
BEGIN
	SELECT PONumber, ItemsSubtotal, SalesTax, Total, DateCreated, PurchaseOrders.EmployeeId, PurchaseOrders.DepartmentsId, PurchaseOrders.Status, Employees.FirstName + ' ' + Employees.LastName AS EmployeeName, Departments.Name AS Name
	FROM PurchaseOrders INNER JOIN Employees ON PurchaseOrders.EmployeeId = Employees.ID INNER JOIN Departments ON Employees.DepartmentID = Departments.ID WHERE PONumber = @PONumber AND EmployeeId = @EmployeeId
END
GO

CREATE OR ALTER PROC PurchaseOrder_SearchByDate
@StartDate DATETIME,
@EndDate DATETIME,
@EmployeeId VARCHAR(8)
AS
BEGIN
	SELECT PONumber, ItemsSubtotal, SalesTax, Total, DateCreated, PurchaseOrders.EmployeeId, PurchaseOrders.DepartmentsId, PurchaseOrders.Status, Employees.FirstName + ' ' + Employees.LastName AS EmployeeName, Departments.Name AS Name
	FROM PurchaseOrders INNER JOIN Employees ON PurchaseOrders.EmployeeId = Employees.ID INNER JOIN Departments ON Employees.DepartmentID = Departments.ID WHERE DateCreated > @StartDate AND DateCreated < @EndDate AND EmployeeId = @EmployeeId
END
GO

--Create test data for PurchaseOrder--
DECLARE @ID INT
EXEC PurchaseOrder_Insert

@ID,
@ItemsSubtotal = 3.99,
@SalesTax = 1.50,
@Total = 6.50,
@DateCreated = '2020-04-24 11:04:14',
@EmployeeId = '00000001',
@DepartmentsId = 1,
@Status = 1
GO

DECLARE @ID INT
EXEC PurchaseOrder_Insert

@ID,
@ItemsSubtotal = 14.50,
@SalesTax = 1.89,
@Total = 17.02,
@DateCreated = '2020-04-30 02:46:49',
@EmployeeId = '00000005',
@DepartmentsId = 1,
@Status = 1
GO

DECLARE @ID INT
EXEC PurchaseOrder_Insert

@ID,
@ItemsSubtotal = 97.11,
@SalesTax = 14.57,
@Total = 111.68,
@DateCreated = '2020-04-29 10:21:38',
@EmployeeId = '00000004',
@DepartmentsId = 1,
@Status = 1
GO

DECLARE @ID INT
EXEC PurchaseOrder_Insert

@ID,
@ItemsSubtotal = 15.68,
@SalesTax = 2.35,
@Total = 18.03,
@DateCreated = '2020-04-29 10:25:38',
@EmployeeId = '00000004',
@DepartmentsId = 1,
@Status = 1
GO
--Create table for PurchaseOrderItems--

CREATE TABLE PurchaseOrderItems(
	Id INT NOT NULL PRIMARY KEY IDENTITY(00001, 1),
	[Name] VARCHAR(20) NOT NULL,
	Description VARCHAR(50) NOT NULL,
	Quantity INT NOT NULL,
	Price MONEY NOT NULL,
	Justification VARCHAR(50) NOT NULL,
	Location VARCHAR(20) NOT NULL,
	Subtotal MONEY NOT NULL,
	PONumber INT NOT NULL FOREIGN KEY REFERENCES PurchaseOrders(PONumber),
	Status INT NOT NULL FOREIGN KEY REFERENCES Status(Id),
	Reason VARCHAR(30) NULL,
	Timestamp TIMESTAMP NULL
)
GO

--Create stored procedures for PurchaseOrderItems

CREATE OR ALTER PROC PurchaseOrderItem_Insert
	@Timestamp TIMESTAMP OUTPUT,
	@Id INT OUTPUT,
	@Name VARCHAR(20),
	@Description VARCHAR(50),
	@Quantity INT,
	@Price MONEY,
	@Justification VARCHAR(50),
	@Location VARCHAR(20),
	@Subtotal MONEY,
	@PONumber INT,
	@Status INT
AS
BEGIN TRY
	INSERT INTO PurchaseOrderItems(Name, Description, Quantity, Price, Justification, Location, Subtotal, PONumber, Status)
	VALUES(
		@Name,
		@Description,
		@Quantity,
		@Price,
		@Justification,
		@Location,
		@Subtotal,
		@PONumber,
		@Status)

		SET @Timestamp = (SELECT Timestamp FROM PurchaseOrderItems WHERE Id = SCOPE_IDENTITY())
		SET @Id = SCOPE_IDENTITY();
END TRY
BEGIN CATCH
	;THROW
END CATCH
GO

CREATE OR ALTER PROC PurchaseOrderItem_Update
	@Timestamp TIMESTAMP OUTPUT,
	@ItemId INT OUT,
	@Name VARCHAR(20),
	@Description VARCHAR(50),
	@Quantity INT,
	@Price MONEY,
	@Justification VARCHAR(50),
	@Location VARCHAR(20),
	@Subtotal MONEY,
	@PONumber INT,
	@Status INT
AS
BEGIN TRY

	IF (SELECT [Timestamp] FROM PurchaseOrderItems WHERE Id = @ItemId) <> @Timestamp
	THROW 51005, 'The record has been updated by someone since you last retrieved it. 
	Retrieve the record again before you make updates.', 1;

	UPDATE PurchaseOrderItems SET Name = @Name, Description = @Description, Quantity = @Quantity, Price = @Price,
	Justification = @Justification, Location = @Location, Subtotal = @Subtotal, PONumber = @PONumber, Status = @Status
	WHERE Id = @ItemId

	SET @Timestamp = (SELECT [TimeStamp] FROM PurchaseOrderItems WHERE Id = @ItemId)
	
END TRY
BEGIN CATCH
	;THROW
END CATCH
GO

CREATE OR ALTER PROC PurchaseOrderItem_Process
	@Timestamp TIMESTAMP OUTPUT,
	@Id INT OUT,
	@Location VARCHAR(20),
	@Quantity INT,
	@Price MONEY,
	@Subtotal MONEY,
	@Reason VARCHAR(20),
	@Status INT
AS
BEGIN TRY
		IF (SELECT [Timestamp] FROM PurchaseOrderItems WHERE Id = @Id) <> @Timestamp
	THROW 51005, 'The record has been updated by someone since you last retrieved it. 
	Retrieve the record again before you make updates.', 1;

	IF @Reason IS NULL
		UPDATE PurchaseOrderItems SET Location = @Location, Quantity = @Quantity, Price = @Price, Subtotal = @Subtotal, Reason = 'N/A', Status = @Status WHERE Id = @Id
	ELSE
		UPDATE PurchaseOrderItems SET Location = @Location, Quantity = @Quantity, Price = @Price, Subtotal = @Subtotal, Reason = @Reason, Status = @Status WHERE Id = @Id

	SET @Timestamp = (SELECT [TimeStamp] FROM PurchaseOrderItems WHERE Id = @Id)
END TRY
BEGIN CATCH
	;THROW
END CATCH
GO

CREATE OR ALTER PROC PurchaseOrderItem_GetById
@Id INT
AS
BEGIN
	SELECT * FROM PurchaseOrderItems WHERE Id = @Id
END
GO

CREATE OR ALTER PROC PurchaseOrderItem_Delete
@Id INT
AS
BEGIN
	DELETE FROM PurchaseOrderItems WHERE Id = @Id
END
GO

CREATE OR ALTER PROC PurchaseOrderItem_SelectItemByPOId
@PONumber INT
AS
BEGIN
	SELECT * FROM PurchaseOrderItems WHERE PONumber = @PONumber
END
GO

CREATE OR ALTER PROC PurchaseOrderItem_GetAll
AS
BEGIN
	SELECT * FROM PurchaseOrderItems
END
GO

CREATE OR ALTER PROC PurchaseOrderItem_CheckIfDuplicateItem
@Name VARCHAR(30)
AS
BEGIN
	SELECT Name FROM PurchaseOrderItems WHERE Name = @Name
END
GO

--Create test data for PurchaseOrderItems--
DECLARE @Id INT DECLARE @Timestamp TIMESTAMP
EXEC PurchaseOrderItem_Insert
@Id,
@Timestamp,
@Name = 'Pencils',
@Description = 'Special art pencils',
@Quantity = 3,
@Price = 3.99,
@Justification = 'Need more',
@Location = 'Staples',
@Subtotal = 11.97,
@PONumber = 3,
@Status = 1
GO

DECLARE @Id INT DECLARE @Timestamp TIMESTAMP
EXEC PurchaseOrderItem_Insert
@Id,
@Timestamp,
@Name = 'File Sorteers',
@Description = 'Value desk sorter',
@Quantity = 1,
@Price = 4.99,
@Justification = 'Replacing broken one',
@Location = 'Staples',
@Subtotal = 4.99,
@PONumber = 3,
@Status = 1
GO

DECLARE @Id INT DECLARE @Timestamp TIMESTAMP
EXEC PurchaseOrderItem_Insert
@Id,
@Timestamp,
@Name = 'Pencil Holder',
@Description = 'Black mesh',
@Quantity = 1,
@Price = 5.99,
@Justification = 'Need one',
@Location = 'Staples',
@Subtotal = 5.99,
@PONumber = 3,
@Status = 1
GO

DECLARE @Id INT DECLARE @Timestamp TIMESTAMP
EXEC PurchaseOrderItem_Insert
@Id,
@Timestamp,
@Name = 'Desk Organize',
@Description = 'Oak',
@Quantity = 1,
@Price = 74.16,
@Justification = 'To organize',
@Location = 'Staples',
@Subtotal = 74.16,
@PONumber = 3,
@Status = 1
GO

DECLARE @Id INT DECLARE @Timestamp TIMESTAMP
EXEC PurchaseOrderItem_Insert
@Id,
@Timestamp,
@Name = 'USB Key',
@Description = '16GB USB',
@Quantity = 1,
@Price = 11.99,
@Justification = 'Ran out of disk space',
@Location = 'Best Buy',
@Subtotal = 11.99,
@PONumber = 2,
@Status = 1
GO

DECLARE @Id INT DECLARE @Timestamp TIMESTAMP
EXEC PurchaseOrderItem_Insert
@Id,
@Timestamp,
@Name = 'Big box of Business Cards',
@Description = 'Re-order same as last set',
@Quantity = 1,
@Price = 1.99,
@Justification = 'Running low',
@Location = 'Staples',
@Subtotal = 1.99,
@PONumber = 2,
@Status = 1
GO

DECLARE @Id INT DECLARE @Timestamp TIMESTAMP
EXEC PurchaseOrderItem_Insert
@Id,
@Timestamp,
@Name = 'Large box of Letter Head',
@Description = 'Letterhead for official correspondance',
@Quantity = 4,
@Price = 3.99,
@Justification = 'Ran out',
@Location = 'Best Buy',
@Subtotal = 15.96,
@PONumber = 1,
@Status = 1
GO

DECLARE @Id INT DECLARE @Timestamp TIMESTAMP
EXEC PurchaseOrderItem_Insert
@Id,
@Timestamp,
@Name = 'Business Card Holder',
@Description = 'For displaying cards',
@Quantity = 1,
@Price = 1.96,
@Justification = 'Get rid of clutter',
@Location = 'Best Buy',
@Subtotal = 1.96,
@PONumber = 4,
@Status = 1
GO

DECLARE @Id INT DECLARE @Timestamp TIMESTAMP
EXEC PurchaseOrderItem_Insert
@Id,
@Timestamp,
@Name = 'Letter Tray Set',
@Description = 'For holding incoming mail',
@Quantity = 1,
@Price = 5.76,
@Justification = 'Other one is broken',
@Location = 'Staples',
@Subtotal = 5.76,
@PONumber = 4,
@Status = 1
GO

DECLARE @Id INT DECLARE @Timestamp TIMESTAMP
EXEC PurchaseOrderItem_Insert
@Id,
@Timestamp,
@Name = 'Erasable Markers',
@Description = 'Box: Multi-colored',
@Quantity = 2,
@Price = 3.98,
@Justification = 'Ran out',
@Location = 'Best Buy',
@Subtotal = 7.96,
@PONumber = 4,
@Status = 1
GO

--E2 STORED PROCEDURES
CREATE OR ALTER PROC PurchaseOrder_SearchByStatus
@Status INT
AS
BEGIN
	SELECT * FROM PurchaseOrders WHERE Status = @Status
END
GO

CREATE OR ALTER PROC PurchaseOrder_LookupStatus
AS
BEGIN
	SELECT * FROM Status
END
GO

CREATE OR ALTER PROC PurchaseOrder_GetPOBySupervisorAndDepartment
@DepartmentId INT
AS
BEGIN
	SELECT po.PONumber, po.EmployeeId, po.DepartmentsId, po.SupervisorId, po.ItemsSubtotal, po.SalesTax, po.Total, po.Status, po.DateCreated, Employees.FirstName + ' ' + Employees.LastName AS EmployeeName, Departments.Name FROM PurchaseOrders AS po INNER JOIN Employees ON po.EmployeeId = Employees.ID INNER JOIN Departments ON po.DepartmentsId = Departments.ID WHERE po.DepartmentsId = @DepartmentId ORDER BY po.DateCreated ASC
END
GO