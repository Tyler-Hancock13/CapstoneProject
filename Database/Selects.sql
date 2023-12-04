SELECT * FROM Employees
SELECT * FROM Users
SELECT * FROM Departments
SELECT * FROM Jobs
SELECT * FROM PurchaseOrders
SELECT * FROM PurchaseOrderItems
SELECT * FROM Reviews
SELECT * FROM Reminders;

DELETE FROM Users WHERE EmployeeID IN (SELECT ID FROM Employees WHERE DepartmentID = 1);
DELETE FROM PurchaseOrderItems WHERE PONumber IN (SELECT PONumber FROM PurchaseOrders WHERE EmployeeID IN (SELECT ID FROM Employees WHERE DepartmentID = 1));
DELETE FROM PurchaseOrders WHERE EmployeeID IN (SELECT ID FROM Employees WHERE DepartmentID = 1);
DELETE FROM Employees WHERE DepartmentID = 1;
EXEC Department_Delete 1;