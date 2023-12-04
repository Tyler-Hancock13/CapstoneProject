using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DAL;
using CapstoneProject.Model.Entities;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Repository
{
    /// <summary>
    /// The Employee Repository
    /// </summary>
    public class EmployeeRepo
    {
        /// <summary>
        /// Data Access Object
        /// </summary>
        private DataAccess db = new DataAccess();

        /// <summary>
        /// Inserts an Employee into the Database
        /// </summary>
        /// <param name="employee">The Employee to be inserted</param>
        /// <returns>The inserted Employee with it's newly generated ID</returns>
        public Tuple<Employee, User> Add(Employee employee, User user)
        {
            List<ParmStruct> parms = PackEmployeeAndUser(employee, user);

            if (db.ExecuteNonQuery("Employee_Insert", parms) > 0)
            {
                employee.ID = parms[0].Value.ToString();
                employee.Version = (byte[])parms[1].Value;
                return Tuple.Create(employee, user);
            }

            return null;
        }

        /// <summary>
        /// Searches for an Employee by full or partial last name
        /// </summary>
        /// <param name="lastName">The full or partial last name to search for</param>
        /// <returns>A List of Employees that matches the search term</returns>
        public List<Employee> Search(string lastName)
        {
            List<Employee> employees = new List<Employee>();
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@LastName", lastName, SqlDbType.NVarChar)
            };

            DataTable dt = db.Execute("Employee_SearchByLastName", CommandType.StoredProcedure, parms);

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(UnpackEmployee(row));
            }

            return employees;
        }

        /// <summary>
        /// Retrieves all Employees
        /// </summary>
        /// <returns>The Employees</returns>
        public List<Employee> Get()
        {
            List<Employee> employees = new List<Employee>();
            DataTable dt = db.Execute("Employee_GetAll");

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(UnpackEmployee(row));
            }

            return employees;
        }

        /// <summary>
        /// Retrieves an Employee by ID
        /// </summary>
        /// <param name="id">The ID of the Employee to be retrieved</param>
        /// <returns>The Employee with the matching ID</returns>
        public Employee Get(string id)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@ID", id, SqlDbType.VarChar)
            };

            DataTable dt = db.Execute("Employee_SearchByID", CommandType.StoredProcedure, parms);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return UnpackEmployee(dt.Rows[0]);
        }

        public Employee GetSupervisorById(string id)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@SupervisorId", id, SqlDbType.VarChar, ParameterDirection.Input, 8)
            };

            DataTable dt = db.Execute("Employee_GetSupervisorById", CommandType.StoredProcedure, parms);

            if(dt.Rows.Count == 0)
            {
                return null;
            }

            return UnpackEmployee(dt.Rows[0]);
        }

        /// <summary>
        /// Retrieves all Employees who are supervisors.
        /// </summary>
        /// <returns>A List of Employees who are Supervisors.</returns>
        public List<Employee> GetSupervisors()
        {
            List<Employee> employees = new List<Employee>();
            DataTable dt = db.Execute("Employee_GetSupervisors");

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(UnpackEmployee(row));
            }

            return employees;
        }

        /// <summary>
        /// Gets Employees by Department.
        /// </summary>
        /// <param name="departmentId">The ID of the Department who's Employees will be retrieved.</param>
        /// <returns>The Employees in the specified Department.</returns>
        public List<Employee> GetByDepartment(int departmentId)
        {
            List<Employee> employees = new List<Employee>();

            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@DepartmentID", departmentId, SqlDbType.Int)
            };

            DataTable dt = db.Execute("Employee_GetByDepartment", CommandType.StoredProcedure, parms);

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(UnpackEmployee(row));
            }

            return employees;
        }

        /// <summary>
        /// Gets all Employees who are either an HRSupervisor or an HREmployee.
        /// </summary>
        /// <returns>The HR Employees.</returns>
        public List<Employee> GetHRStaff()
        {
            List<Employee> employees = new List<Employee>();
            DataTable dt = db.Execute("Employee_GetHRStaff");

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(UnpackEmployee(row));
            }

            return employees;
        }

        /// <summary>
        /// Gets a List of Employees overseen by a particular Supervisor.
        /// </summary>
        /// <param name="superviorId">The ID of the Supervisor.</param>
        /// <returns>The Supervisors Employees.</returns>
        public List<Employee> GetBySupervisor(string supervisorId)
        {
            List<Employee> employees = new List<Employee>();
            List<ParmStruct> parms = new List<ParmStruct> 
            { 
                new ParmStruct("@SupervisorID", supervisorId, SqlDbType.VarChar)
            };

            DataTable dt = db.Execute("Employee_GetBySupervisor", CommandType.StoredProcedure, parms);

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(UnpackEmployee(row));
            }

            return employees;
        }

        /// <summary>
        /// Updates an Employee.
        /// </summary>
        /// <param name="employee">The Employee to be updated.</param>
        /// <returns>The Employee after the attempted update.</returns>
        public Employee Update(Employee employee)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@ID", employee.ID, SqlDbType.VarChar, ParameterDirection.Input, 8),
                new ParmStruct("@Version", employee.Version, SqlDbType.Timestamp, ParameterDirection.InputOutput, 99),
                new ParmStruct("@FirstName", employee.FirstName, SqlDbType.NVarChar),

                employee.MiddleInitial == "" || employee.MiddleInitial == null
                    ? new ParmStruct("@MiddleInitial", DBNull.Value, SqlDbType.NVarChar)
                    : new ParmStruct("@MiddleInitial", employee.MiddleInitial, SqlDbType.NVarChar),

                new ParmStruct("@LastName", employee.LastName, SqlDbType.NVarChar),
                new ParmStruct("@DateOfBirth", employee.DateOfBirth, SqlDbType.DateTime),
                new ParmStruct("@StreetAddress", employee.StreetAddress, SqlDbType.NVarChar),
                new ParmStruct("@City", employee.City, SqlDbType.NVarChar),
                new ParmStruct("@PostalCode", employee.PostalCode, SqlDbType.NVarChar),
                new ParmStruct("@SIN", employee.SIN, SqlDbType.NVarChar),
                new ParmStruct("@SeniorityDate", employee.SeniorityDate, SqlDbType.DateTime),
                new ParmStruct("@JobStartDate", employee.JobStartDate, SqlDbType.DateTime),

                employee.WorkPhone == null
                    ? new ParmStruct("@WorkPhone", DBNull.Value, SqlDbType.NVarChar)
                    : new ParmStruct("@WorkPhone", employee.WorkPhone, SqlDbType.NVarChar),

                employee.CellPhone == null
                    ? new ParmStruct("@CellPhone", DBNull.Value, SqlDbType.NVarChar)
                    : new ParmStruct("@CellPhone", employee.CellPhone, SqlDbType.NVarChar),

                new ParmStruct("@Email", employee.EmailAddress, SqlDbType.NVarChar),

                new ParmStruct("@OfficeAddress", employee.OfficeAddress, SqlDbType.NVarChar),
                new ParmStruct("@OfficeCity", employee.OfficeCity, SqlDbType.NVarChar),

                employee.OfficeUnit != 0
                    ? new ParmStruct("@OfficeUnit", employee.OfficeUnit, SqlDbType.Int)
                    : new ParmStruct("@OfficeUnit", DBNull.Value, SqlDbType.Int),

                employee.EndDate != DateTime.MinValue
                    ? new ParmStruct("@EndDate", employee.EndDate, SqlDbType.DateTime)
                    : new ParmStruct("@EndDate", DBNull.Value, SqlDbType.DateTime),

                new ParmStruct("@StatusID", (int) employee.Status, SqlDbType.Int),

                new ParmStruct("@JobID", employee.JobID, SqlDbType.Int),

                employee.SupervisorID == "" || employee.SupervisorID == null
                    ? new ParmStruct("@SupervisorID", DBNull.Value, SqlDbType.VarChar)
                    : new ParmStruct("@SupervisorID", employee.SupervisorID, SqlDbType.VarChar),

                new ParmStruct("@DepartmentID", employee.DepartmentID, SqlDbType.Int)
            };

            if (db.ExecuteNonQuery("Employee_Update", parms) > 0)
                employee.Version = (byte[]) parms[1].Value;

            return employee;
        }
        
        /// <summary>
        /// Updates an Employee and their User Account.
        /// </summary>
        /// <param name="employee">The Employee to be updated.</param>
        /// <param name="user">The User to be Updated.</param>
        /// <returns>A Tuple containing the updated employee and user.</returns>
        public Tuple<Employee, User> Update(Employee employee, User user)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@ID", employee.ID, SqlDbType.VarChar, ParameterDirection.Input, 8),
                new ParmStruct("@Version", employee.Version, SqlDbType.Timestamp, ParameterDirection.InputOutput, 99),
                new ParmStruct("@FirstName", employee.FirstName, SqlDbType.NVarChar),

                employee.MiddleInitial == "" || employee.MiddleInitial == null
                    ? new ParmStruct("@MiddleInitial", DBNull.Value, SqlDbType.NVarChar)
                    : new ParmStruct("@MiddleInitial", employee.MiddleInitial, SqlDbType.NVarChar),

                new ParmStruct("@LastName", employee.LastName, SqlDbType.NVarChar),
                new ParmStruct("@DateOfBirth", employee.DateOfBirth, SqlDbType.DateTime),
                new ParmStruct("@StreetAddress", employee.StreetAddress, SqlDbType.NVarChar),
                new ParmStruct("@City", employee.City, SqlDbType.NVarChar),
                new ParmStruct("@PostalCode", employee.PostalCode, SqlDbType.NVarChar),
                new ParmStruct("@SIN", employee.SIN, SqlDbType.NVarChar),
                new ParmStruct("@SeniorityDate", employee.SeniorityDate, SqlDbType.DateTime),
                new ParmStruct("@JobStartDate", employee.JobStartDate, SqlDbType.DateTime),

                employee.WorkPhone == null
                    ? new ParmStruct("@WorkPhone", DBNull.Value, SqlDbType.NVarChar)
                    : new ParmStruct("@WorkPhone", employee.WorkPhone, SqlDbType.NVarChar),

                employee.CellPhone == null
                    ? new ParmStruct("@CellPhone", DBNull.Value, SqlDbType.NVarChar)
                    : new ParmStruct("@CellPhone", employee.CellPhone, SqlDbType.NVarChar),

                new ParmStruct("@Email", employee.EmailAddress, SqlDbType.NVarChar),

                new ParmStruct("@OfficeAddress", employee.OfficeAddress, SqlDbType.NVarChar),
                new ParmStruct("@OfficeCity", employee.OfficeCity, SqlDbType.NVarChar),

                employee.OfficeUnit != 0
                    ? new ParmStruct("@OfficeUnit", employee.OfficeUnit, SqlDbType.Int)
                    : new ParmStruct("@OfficeUnit", DBNull.Value, SqlDbType.Int),

                employee.EndDate != DateTime.MinValue
                    ? new ParmStruct("@EndDate", employee.EndDate, SqlDbType.DateTime)
                    : new ParmStruct("@EndDate", DBNull.Value, SqlDbType.DateTime),

                new ParmStruct("@StatusID", (int) employee.Status, SqlDbType.Int),

                new ParmStruct("@JobID", employee.JobID, SqlDbType.Int),

                employee.SupervisorID == "" || employee.SupervisorID == null
                    ? new ParmStruct("@SupervisorID", DBNull.Value, SqlDbType.VarChar)
                    : new ParmStruct("@SupervisorID", employee.SupervisorID, SqlDbType.VarChar),

                new ParmStruct("@DepartmentID", employee.DepartmentID, SqlDbType.Int),
                new ParmStruct("@RoleID", (int) user.Role, SqlDbType.Int)
            };

            if (db.ExecuteNonQuery("Employee_User_Update", parms) > 0)
                employee.Version = (byte[]) parms[1].Value;

            return Tuple.Create(employee, user);
        }

        /// <summary>
        /// Populates an Employee Object with values from a DataRow.
        /// </summary>
        /// <param name="row">The DataRow to parse.</param>
        /// <returns>The Populated Employee Object.</returns>
        private Employee UnpackEmployee(DataRow row)
        {
            return new Employee
            {
                ID = row["ID"].ToString(),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                MiddleInitial = row["MiddleInitial"] != DBNull.Value ? row["MiddleInitial"].ToString() : null,
                DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
                StreetAddress = row["StreetAddress"].ToString(),
                City = row["City"].ToString(),
                PostalCode = row["PostalCode"].ToString(),
                SIN = row["SIN"].ToString(),
                SeniorityDate = Convert.ToDateTime(row["SeniorityDate"]),
                JobStartDate = Convert.ToDateTime(row["JobStartDate"]),
                WorkPhone = row["WorkPhone"] != DBNull.Value ? row["WorkPhone"].ToString() : null,
                CellPhone = row["CellPhone"] != DBNull.Value ? row["CellPhone"].ToString() : null,
                EmailAddress = row["Email"].ToString(),
                OfficeAddress = row["OfficeAddress"].ToString(),
                OfficeCity = row["OfficeCity"].ToString(),
                OfficeUnit = row["OfficeUnit"] != DBNull.Value ? Convert.ToInt32(row["OfficeUnit"]) : 0,
                EndDate = row["EndDate"] != DBNull.Value ? Convert.ToDateTime(row["EndDate"]) : DateTime.MinValue,
                Version = (byte[])row["Version"],
                JobID = Convert.ToInt32(row["JobID"]),
                SupervisorID = row["SupervisorID"] != DBNull.Value ? row["SupervisorID"].ToString() : null,
                DepartmentID = Convert.ToInt32(row["DepartmentID"]),
                Status = (EmployeeStatus) Enum.Parse(typeof(EmployeeStatus), row["StatusID"].ToString())
            };
        }

        /// <summary>
        /// Packs an Employee and User models into ParmStructs
        /// </summary>
        /// <param name="employee">The Employee to be packed.</param>
        /// <param name="user">The user to be packed.</param>
        /// <returns>A List of the ParmStructs.</returns>
        private List<ParmStruct> PackEmployeeAndUser(Employee employee, User user)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@ID", employee.ID, SqlDbType.VarChar, ParameterDirection.Output, 8),
                new ParmStruct("@Version", employee.Version, SqlDbType.Timestamp, ParameterDirection.Output, 99),
                new ParmStruct("@FirstName", employee.FirstName, SqlDbType.NVarChar),

                employee.MiddleInitial == ""
                    ? new ParmStruct("@MiddleInitial", DBNull.Value, SqlDbType.NVarChar)
                    : new ParmStruct("@MiddleInitial", employee.MiddleInitial, SqlDbType.NVarChar),

                new ParmStruct("@LastName", employee.LastName, SqlDbType.NVarChar),
                new ParmStruct("@DateOfBirth", employee.DateOfBirth, SqlDbType.DateTime),
                new ParmStruct("@StreetAddress", employee.StreetAddress, SqlDbType.NVarChar),
                new ParmStruct("@City", employee.City, SqlDbType.NVarChar),
                new ParmStruct("@PostalCode", employee.PostalCode, SqlDbType.NVarChar),
                new ParmStruct("@SIN", employee.SIN, SqlDbType.NVarChar),
                new ParmStruct("@SeniorityDate", employee.SeniorityDate, SqlDbType.DateTime),
                new ParmStruct("@JobStartDate", employee.JobStartDate, SqlDbType.DateTime),

                employee.WorkPhone == null
                    ? new ParmStruct("@WorkPhone", DBNull.Value, SqlDbType.NVarChar)
                    : new ParmStruct("@WorkPhone", employee.WorkPhone, SqlDbType.NVarChar),
                
                employee.CellPhone == null
                    ? new ParmStruct("@CellPhone", DBNull.Value, SqlDbType.NVarChar)
                    : new ParmStruct("@CellPhone", employee.CellPhone, SqlDbType.NVarChar),

                new ParmStruct("@Email", employee.EmailAddress, SqlDbType.NVarChar),
                new ParmStruct("@OfficeAddress", employee.OfficeAddress, SqlDbType.NVarChar),
                new ParmStruct("@OfficeCity", employee.OfficeCity, SqlDbType.NVarChar),
                
                employee.OfficeUnit != 0 
                    ? new ParmStruct("@OfficeUnit", employee.OfficeUnit, SqlDbType.Int)
                    : new ParmStruct("@OfficeUnit", DBNull.Value, SqlDbType.Int),

                employee.EndDate != DateTime.MinValue
                    ? new ParmStruct("@EndDate", employee.EndDate, SqlDbType.DateTime)
                    : new ParmStruct("@EndDate", DBNull.Value, SqlDbType.DateTime),


                new ParmStruct("@StatusID", (int) employee.Status, SqlDbType.Int),
                new ParmStruct("@JobID", employee.JobID, SqlDbType.Int),

                employee.SupervisorID == "" || employee.SupervisorID == null
                    ? new ParmStruct("@SupervisorID", DBNull.Value, SqlDbType.VarChar)
                    : new ParmStruct("@SupervisorID", employee.SupervisorID, SqlDbType.VarChar),

                new ParmStruct("@DepartmentID", employee.DepartmentID, SqlDbType.Int),
                new ParmStruct("@Password", user.Password, SqlDbType.NVarChar),
                new ParmStruct("@RoleID", (int) user.Role, SqlDbType.Int)
            };

            return parms;
        }
    }
}
