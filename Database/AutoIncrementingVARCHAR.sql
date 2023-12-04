DELETE FROM Employees

CREATE OR ALTER PROCEDURE Employee_GenerateID
AS
BEGIN
	IF (SELECT COUNT(*) FROM Employees) != 0
	BEGIN
		INSERT INTO 
			Employees
		VALUES
		(
			RIGHT
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
			)
		)
	END
	ELSE
	BEGIN
		INSERT INTO
			Employees
		VALUES ('00000001');
	END
END
GO

EXEC Employee_Insert;
SELECT * FROM Employees
GO


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
END;
SELECT dbo.Employee_GenerateID();