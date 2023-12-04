using CapstoneProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Service
{
    /// <summary>
    /// Employee Service Interface
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Inserts an Employee into the Database
        /// </summary>
        /// <param name="employee">The Employee to be inserted</param>
        /// <returns>The inserted Employee with it's newly generated ID</returns>
        Tuple<Employee, User> Add(Employee employee, User user);

        /// <summary>
        /// Searches for an Employee by full or partial last name
        /// </summary>
        /// <param name="lastName">The full or partial last name to search for</param>
        /// <returns>A List of Employees that matches the search term</returns>
        List<Employee> Search(string lastName);

        /// <summary>
        /// Retrieves all Employees
        /// </summary>
        /// <returns>The Employees</returns>
        List<Employee> Get();

        /// <summary>
        /// Retrieves an Employee by ID
        /// </summary>
        /// <param name="id">The ID of the Employee to be retrieved</param>
        /// <returns>The Employee with the matching ID</returns>
        Employee Get(string id);

        /// <summary>
        /// Retrieves all Employees who are supervisors.
        /// </summary>
        /// <returns>A List of Employees who are Supervisors.</returns>
        List<Employee> GetSupervisors();

        /// <summary>
        /// Gets Employees by Department.
        /// </summary>
        /// <param name="departmentId">The ID of the Department who's Employees will be retrieved.</param>
        /// <returns>The Employees in the specified Department.</returns>
        List<Employee> GetByDepartment(int departmentId);

        /// <summary>
        /// Gets a List of Employees overseen by a particular Supervisor.
        /// </summary>
        /// <param name="superviorId">The ID of the Supervisor.</param>
        /// <returns>The Supervisors Employees.</returns>
        List<Employee> GetBySupervisor(string supervisorId);

        /// <summary>
        /// Updates an Employee.
        /// </summary>
        /// <param name="employee">The Employee to be updated.</param>
        /// <returns>The Employee after the attempted update.</returns>
        Employee Update(Employee employee);

        /// <summary>
        /// Updates an Employee and their User Account.
        /// </summary>
        /// <param name="employee">The Employee to be updated.</param>
        /// <param name="user">The User to be Updated.</param>
        /// <returns>A Tuple containing the updated employee and user.</returns>
        Tuple<Employee, User> Update(Employee employee, User user);

        /// <summary>
        /// Determines whether or not an Employee is a Supervisor
        /// </summary>
        /// <param name="employee">The Employee to check</param>
        /// <returns>A boolean value representing whether or not the Employee is a Supervisor</returns>
        bool IsSupervisor(Employee employee);

        /// <summary>
        /// Gets all Employees who are either an HRSupervisor or an HREmployee.
        /// </summary>
        /// <returns>The HR Employees.</returns>
        List<Employee> GetHRStaff();
    }
}
