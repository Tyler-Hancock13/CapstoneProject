using CapstoneProject.Model.Entities;
using CapstoneProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Service
{
    /// <summary>
    /// Employee Service
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// The Employee Repository
        /// </summary>
        private EmployeeRepo repo = new EmployeeRepo();

        /// <summary>
        /// Inserts an Employee into the Database
        /// </summary>
        /// <param name="employee">The Employee to be inserted</param>
        /// <returns>The inserted Employee with it's newly generated ID</returns>
        public Tuple<Employee, User> Add(Employee employee, User user)
        {
            if (ValidateEmployeeAndUser(employee, user))
            {
                user.Password = GetMd5Hash(user.Password);
                return repo.Add(employee, user);
            }

            return Tuple.Create(employee, user);
        }

        /// <summary>
        /// Searches for an Employee by full or partial last name
        /// </summary>
        /// <param name="lastName">The full or partial last name to search for</param>
        /// <returns>A List of Employees that matches the search term</returns>
        public List<Employee> Search(string lastName)
        {
            return repo.Search(lastName);
        }

        /// <summary>
        /// Retrieves all Employees
        /// </summary>
        /// <returns>The Employees</returns>
        public List<Employee> Get()
        {
            return repo.Get();
        }

        /// <summary>
        /// Retrieves an Employee by ID
        /// </summary>
        /// <param name="id">The ID of the Employee to be retrieved</param>
        /// <returns>The Employee with the matching ID</returns>
        public Employee Get(string id)
        {
            return repo.Get(id);
        }

        /// <summary>
        /// Retrieves all Employees who are supervisors.
        /// </summary>
        /// <returns>A List of Employees who are Supervisors.</returns>
        public List<Employee> GetSupervisors()
        {
            return repo.GetSupervisors();
        }

        /// <summary>
        /// Gets Employees by Department.
        /// </summary>
        /// <param name="departmentId">The ID of the Department who's Employees will be retrieved.</param>
        /// <returns>The Employees in the specified Department.</returns>
        public List<Employee> GetByDepartment(int departmentId)
        {
            return repo.GetByDepartment(departmentId);
        }

        /// <summary>
        /// Gets a List of Employees overseen by a particular Supervisor.
        /// </summary>
        /// <param name="superviorId">The ID of the Supervisor.</param>
        /// <returns>The Supervisors Employees.</returns>
        public List<Employee> GetBySupervisor(string supervisorId)
        {
            return repo.GetBySupervisor(supervisorId);
        }

        public Employee GetSupervisorById(string supervisorId)
        {
            return repo.GetSupervisorById(supervisorId);
        }

        /// <summary>
        /// Updates an Employee.
        /// </summary>
        /// <param name="employee">The Employee to be updated.</param>
        /// <returns>The Employee after the attempted update.</returns>
        public Employee Update(Employee employee)
        {
            byte[] previousVersion = employee.Version;

            if (Validate(employee))
            {
                employee = repo.Update(employee);

                if (employee.Version == previousVersion)
                {
                    employee.AddError(new Error("The Employee has been modified, reload and try again.", ErrorType.Business));
                }
            }

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
            byte[] previousVersion = employee.Version;
            Tuple<Employee, User> returnValues;

            if (ValidateEmployeeAndUser(employee, user))
            {
                returnValues = repo.Update(employee, user);
                employee = returnValues.Item1;

                if (employee.Version == previousVersion)
                {
                    employee.AddError(new Error("The Employee has been modified, reload and try again.", ErrorType.Business));
                }
            }
            else
            {
                returnValues = Tuple.Create(employee, user);
            }

            return returnValues;
        }

        /// <summary>
        /// Determines whether or not an Employee is a Supervisor
        /// </summary>
        /// <param name="employee">The Employee to check</param>
        /// <returns>A boolean value representing whether or not the Employee is a Supervisor</returns>
        public bool IsSupervisor(Employee employee)
        {
            return GetSupervisors().Contains(employee);
        }

        /// <summary>
        /// Gets all Employees who are either an HRSupervisor or an HREmployee.
        /// </summary>
        /// <returns>The HR Employees.</returns>
        public List<Employee> GetHRStaff()
        {
            return repo.GetHRStaff();
        }

        /// <summary>
        /// Validates the Employee
        /// </summary>
        /// <param name="employee">The Employee to be validated</param>
        /// <returns>A boolean value representing whether or not the Employee is valid</returns>
        private bool Validate(Employee employee)
        {
            ValidationContext context = new ValidationContext(employee);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(employee, context, results, true);

            foreach (ValidationResult result in results)
            {
                Error error = new Error(result.ErrorMessage, ErrorType.Model);
                employee.AddError(error);
            }

            if (DateIsInFuture(employee.DateOfBirth))
            {
                employee.AddError(new Error("Date of Birth cannot be in the future.", ErrorType.Business));
            }

            if ((employee.WorkPhone == null || employee.WorkPhone == "") && (employee.CellPhone == null || employee.CellPhone == ""))
            {
                employee.AddError(new Error("Employee must have either a Work Phone or Cell Phone.", ErrorType.Business));
            }

            if (employee.JobStartDate < employee.SeniorityDate)
            {
                employee.AddError(new Error("Job Start Date cannot be before Seniority Date.", ErrorType.Business));
            }

            if (SupervisorHasTooManyEmployees(employee.SupervisorID))
            {
                employee.AddError(new Error("Supervisor has too many Employees.", ErrorType.Business));
            }

            if (employee.Status == EmployeeStatus.Retired && CalculateAge(employee) < 55)
            {
                employee.AddError(new Error("Employees must be at least 55 years old to retire.", ErrorType.Business));
            }

            return employee.Errors.Count == 0;
        }

        /// <summary>
        /// Validates the Employee
        /// </summary>
        /// <param name="employee">The Employee to be validated</param>
        /// <returns>A boolean value representing whether or not the Employee is valid</returns>
        private bool ValidateEmployeeAndUser(Employee employee, User user)
        {
            Validate(employee);

            ValidationContext context = new ValidationContext(user);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(user, context, results, true);

            foreach (ValidationResult result in results)
            {
                Error error = new Error(result.ErrorMessage, ErrorType.Model);
                user.AddError(error);
            }

            return employee.Errors.Count == 0 && user.Errors.Count == 0;
        }

        /// <summary>
        /// Calculates an Employee's age.
        /// </summary>
        /// <param name="employee">The Employee who's age is to be calculated.</param>
        /// <returns>The Employee's age.</returns>
        private int CalculateAge(Employee employee)
        {
            DateTime dob = employee.DateOfBirth;
            int years = new DateTime(DateTime.Now.Subtract(dob).Ticks).Year - 1;
            return years;
        }

        /// <summary>
        /// Checks if a Supervisor with a specified ID has a maximum of 10 employees.
        /// </summary>
        /// <param name="supervisorID">The ID of the Supervisor to check.</param>
        /// <returns>A boolean value representing whether or not the Supervisor has too many employees.</returns>
        private bool SupervisorHasTooManyEmployees(string supervisorID)
        {
            List<Employee> supervisorsEmployees = Get().Where(e => e.SupervisorID == supervisorID).ToList();
            return supervisorsEmployees.Count >= 10;
        }

        /// <summary>
        /// Checks if a date is in the future.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns>True or false.</returns>
        private bool DateIsInFuture(DateTime date)
        {
            return date > DateTime.Now;
        }

        /// <summary>
        /// Hashes a string using MD5 Format.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <returns>The hash as a string.</returns>
        private string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}
