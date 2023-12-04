using CapstoneProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Service
{
    /// <summary>
    /// Interface for the Email Service.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Method for sending an email.
        /// </summary>
        /// <param name="email">The email to be sent.</param>
        /// <returns>The email that was sent.</returns>
        Email Send(Email email);
    }
}
