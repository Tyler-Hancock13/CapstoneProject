using CapstoneProject.Model.Entities;
using CapstoneProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Service
{
    /// <summary>
    /// Department Service
    /// </summary>
    public class DepartmentService
    {
        /// <summary>
        /// The Department Repository
        /// </summary>
        private DepartmentRepo repo = new DepartmentRepo();

        /// <summary>
        /// Inserts a Department into the Database
        /// </summary>
        /// <param name="department">The Department to be inserted</param>
        /// <returns>The inserted Department with the newly created ID</returns>
        public Department Add(Department department)
        {
            if (Validate(department))
                return repo.Add(department);

            return department;
        }

        /// <summary>
        /// Gets all Departments
        /// </summary>
        /// <returns>A List of all Departments.</returns>
        public List<Department> Get()
        {
            return repo.Get();
        }

        /// <summary>
        /// Gets a Department by ID
        /// </summary>
        /// <param name="id">The ID of the Department to retrieve</param>
        /// <returns>The Department with the matching ID</returns>
        public Department Get(int id)
        {
            return repo.Get(id);
        }

        /// <summary>
        /// Updates a Department.
        /// </summary>
        /// <param name="department">The Department to be updated.</param>
        /// <returns>The updated Department.</returns>
        public Department Update(Department department)
        {
            byte[] previousVersion = department.Version;

            if (Validate(department))
            {
                department = repo.Update(department);

                if (department.Version == previousVersion)
                {
                    department.AddError(new Error("The Department has been modified, reload and try again.", ErrorType.Business));
                }
            }

            return department;
        }

        /// <summary>
        /// Deletes a department from the database.
        /// </summary>
        /// <param name="department">The department to delete.</param>
        /// <returns>The deleted department.</returns>
        public Department Delete(Department department)
        {
            try
            {
                return repo.Delete(department);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    department.AddError(new Error("A Department with Employees cannot be deleted.", ErrorType.Business));
                
                return department;
            }
            catch (Exception ex)
            {
                return department;
            }
        }

        /// <summary>
        /// Validates the Department
        /// </summary>
        /// <param name="department">The Department to be validated</param>
        /// <returns>A boolean value representing whether or not the Department is valid</returns>
        private bool Validate(Department department)
        {
            ValidationContext context = new ValidationContext(department);
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(department, context, results, true);

            foreach (ValidationResult result in results)
            {
                Error error = new Error(result.ErrorMessage, ErrorType.Model);
                department.AddError(error);
            }

            if (DateIsInPast(department.InvocationDate))
            {
                department.AddError(new Error("Invocation Date cannot be in the past.", ErrorType.Business));
            }

            return department.Errors.Count == 0;
        }

        /// <summary>
        /// Determines if a date is in the past.
        /// </summary>
        /// <param name="date">Th date to check.</param>
        /// <returns>A boolean value indicating whether or note the date was in the past.<returns>
        private bool DateIsInPast(DateTime date)
        {
            return date.Date < DateTime.Today;
        }
    }
}
