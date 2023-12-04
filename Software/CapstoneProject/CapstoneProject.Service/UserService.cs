using CapstoneProject.Model.Entities;
using CapstoneProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Service
{
    /// <summary>
    /// The User Service
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// The User Repository
        /// </summary>
        private UserRepo repo = new UserRepo();

        /// <summary>
        /// Gets a User by EmployeeID.
        /// </summary>
        /// <param name="employeeId">The Employee ID of the Account to Retrieve.</param>
        /// <returns>The User Account with the Specified Employee ID.</returns>
        public User Get(string employeeId)
        {
            return repo.Get(employeeId);
        }

        /// <summary>
        /// Gets a User by EmployeeID and Password.
        /// </summary>
        /// <param name="employeeId">The Employee ID of the Account to Retrieve.</param>
        /// <param name="password">The Password of the Account to Retrieve.</param>
        /// <returns>The User Account with the Specified Employee ID.</returns>
        public User Get(string employeeId, string password)
        {
            return repo.Get(employeeId, password);
        }

        /// <summary>
        /// Creates a User
        /// </summary>
        /// <param name="user">The user to be created.</param>
        /// <returns>The User if it is created successfully null if not.</returns>
        public User Add(User user)
        {
            if (Validate(user))
                return repo.Add(user);

            return user;
        }

        /// <summary>
        /// Validates the user model.
        /// </summary>
        /// <param name="user">The user to be validated.</param>
        /// <returns>A Boolean value representing whether or not the user is valid.</returns>
        private bool Validate(User user)
        {
            ValidationContext context = new ValidationContext(user);
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(user, context, results, true);

            foreach (ValidationResult result in results)
            {
                Error error = new Error(result.ErrorMessage, ErrorType.Model);
                user.AddError(error);
            }

            return user.Errors.Count == 0;
        }
    }
}
