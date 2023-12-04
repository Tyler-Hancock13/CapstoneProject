using CapstoneProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Service
{
    /// <summary>
    /// Email Service
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Method for sending an email.
        /// </summary>
        /// <param name="email">The email to be sent.</param>
        /// <returns>The email that was sent.</returns>
        public Email Send(Email email)
        {
            if (Validate(email))
            {
                SmtpClient smtp = new SmtpClient("localhost");
                MailMessage message = new MailMessage(email.From, email.To, email.Subject, email.Body);
                message.From = new MailAddress(email.From, "Cryptech");
                message.IsBodyHtml = true;

                foreach (string address in email.CC)
                {
                    message.CC.Add(new MailAddress(address));
                }

                smtp.Send(message);
            }

            return email;
        }

        /// <summary>
        /// Validates an email.
        /// </summary>
        /// <param name="email">The email to be validated.</param>
        /// <returns>A flag indicating if the email was valid.</returns>
        private bool Validate(Email email)
        {
            ValidationContext context = new ValidationContext(email);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(email, context, results, true);

            foreach (ValidationResult result in results)
            {
                Error error = new Error(result.ErrorMessage, ErrorType.Model);
                email.AddError(error);
            }

            return email.Errors.Count == 0;
        }
    }
}
