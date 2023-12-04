using CapstoneProject.Model.Entities;
using CapstoneProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Service
{
    /// <summary>
    /// The Review Service
    /// </summary>
    public class ReviewService
    {
        /// <summary>
        /// The Review Repository
        /// </summary>
        ReviewRepo repo = new ReviewRepo();

        /// <summary>
        /// Service for sending emails.
        /// </summary>
        IEmailService emailService;

        /// <summary>
        /// Service for employees.
        /// </summary>
        IEmployeeService employeeService;

        /// <summary>
        /// Constructor for injecting Email Service dependency.
        /// </summary>
        public ReviewService(IEmailService emailService, IEmployeeService employeeService)
        {
            this.emailService = emailService;
            this.employeeService = employeeService;
        }

        /// <summary>
        /// Adds a Review to the Database.
        /// </summary>
        /// <param name="review">The Review to be Added.</param>
        /// <returns>The Added Review.</returns>
        public Review Add(Review review)
        {
            if (Validate(review))
                return repo.Add(review);

            return review;
        }

        /// <summary>
        /// Retrieves the Reviews for a specific Employee.
        /// </summary>
        /// <param name="employeeId">ID of the Employee to retrieve Reviews for.</param>
        /// <returns>A List of the Employees Reviews.</returns>
        public List<Review> GetByEmployee(string employeeId)
        {
            return repo.GetByEmployee(employeeId);
        }

        /// <summary>
        /// Gets the most recent reminder from the database.
        /// </summary>
        /// <returns>The most recent reminder or null if there are none.</returns>
        public Reminder GetMostRecentReminder()
        {
            return repo.GetMostRecentReminder();
        }

        /// <summary>
        /// Sends email reminders to supervisors who have pending reviews.
        /// </summary>
        /// <returns>A boolean value indicating if the reminders sent successfully.</returns>
        public bool SendReminders()
        {
            try
            {
                Reminder mostRecentReminder = GetMostRecentReminder();

                if (mostRecentReminder == null || mostRecentReminder.Date.Date < DateTime.Today)
                {
                    List<Email> emails = new List<Email>();
                    List<Employee> supervisors = employeeService.GetSupervisors();

                    foreach (Employee supervisor in supervisors)
                    {
                        List<Employee> pendingEmployees = GetPendingEmployees(supervisor.ID);

                        if (pendingEmployees.Count > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append($"Dear {supervisor.FullName},<br/><br/>This message is to inform you that you have pending reviews for the following employees:<ul style=\"margin-top:0;\">");

                            foreach (Employee employee in pendingEmployees)
                            {
                                sb.Append($"<li>{employee.FullName}</li>");
                            }

                            sb.Append("</ul>Please, ensure you complete these reviews in a timely manner.<br/><br/>Thank you,</br>Cryptech");

                            if (IsMoreThanThirtyDaysIntoQuarter())
                            {
                                List<Employee> hrStaff = employeeService.GetHRStaff();
                                List<string> hrEmails = new List<string>();

                                foreach (Employee employee in hrStaff)
                                {
                                    if (employee.ID != supervisor.ID)
                                        hrEmails.Add(employee.EmailAddress);
                                }

                                emails.Add
                                (
                                    new Email
                                    (
                                        supervisor.EmailAddress,
                                        "no-reply@cryptech.com",
                                        "You have Pending Reviews",
                                        sb.ToString(),
                                        hrEmails
                                    )
                                );
                            }
                            else
                            {
                                emails.Add(new Email(supervisor.EmailAddress, "no-reply@cryptech.com", "You have Pending Reviews", sb.ToString()));
                            }
                        }
                    }

                    foreach (Email email in emails)
                    {
                        emailService.Send(email);
                    }

                    AddReminder(new Reminder(DateTime.Now));
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a supervisors employees that are pending review.
        /// </summary>
        /// <param name="supervisorId">The id of the supervisor who's pending employees should be retrieved.</param>
        /// <returns>The supervisors employees that are pending review.</returns>
        public List<Employee> GetPendingEmployees(string supervisorId)
        {
            List<Employee> employees = employeeService.GetBySupervisor(supervisorId).Where(e => e.Status == EmployeeStatus.Active).ToList();
            List<Employee> pendingEmployees = new List<Employee>();

            foreach (Employee employee in employees)
            {
                if (IsPendingReview(employee.ID))
                {
                    pendingEmployees.Add(employee);
                }
            }

            return pendingEmployees;
        }

        /// <summary>
        /// Adds a reminder to the database.
        /// </summary>
        /// <param name="reminder">The reminder to be inserted.</param>
        /// <returns>The inserted reminder.</returns>
        private Reminder AddReminder(Reminder reminder)
        {
            return repo.AddReminder(reminder);
        }

        /// <summary>
        /// Validates a Review.
        /// </summary>
        /// <param name="review">The Review to be validated.</param>
        /// <returns>A boolean value indicating if the Review was valid.</returns>
        private bool Validate(Review review)
        {
            ValidationContext context = new ValidationContext(review);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(review, context, results, true);

            foreach (ValidationResult result in results)
            {
                Error error = new Error(result.ErrorMessage, ErrorType.Model);
                review.AddError(error);
            }

            if (DateIsInFuture(review.Date))
            {
                review.AddError(new Error("The Date field cannot be in the future.", ErrorType.Business));
            }

            if (!IsPendingReview(review.EmployeeID))
            {
                review.AddError(new Error("The Employee has already received a review this quarter.", ErrorType.Business));
            }

            return review.Errors.Count == 0;
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
        /// Determines if the current date is more than thirty days into the fiscal quarter.
        /// </summary>
        /// <returns>A boolean value indicating if the current date is more than thirty days into the fiscal quarter.</returns>
        private bool IsMoreThanThirtyDaysIntoQuarter()
        {
            DateTime now = DateTime.Now;
            return now >= new DateTime(now.Year, 1, 1).AddDays(29) && now < new DateTime(now.Year, 4, 1)
                || now >= new DateTime(now.Year, 4, 1).AddDays(29) && now < new DateTime(now.Year, 7, 1)
                || now >= new DateTime(now.Year, 7, 1).AddDays(29) && now < new DateTime(now.Year, 10, 1)
                || now >= new DateTime(now.Year, 10, 1).AddDays(29) && now < new DateTime(now.Year + 1, 1, 1);
        }

        /// <summary>
        /// Determines if a particular employee is overdue for review.
        /// </summary>
        /// <param name="employeeId">The id of the employee to check.</param>
        /// <returns>A boolean value indicating if the employee is overdue for review.</returns>
        private bool IsPendingReview(string employeeId)
        {
            List<Review> reviews = GetByEmployee(employeeId);
            bool isPendingReview = true;
            DateTime now = DateTime.Now;
            DateTime startOfQuarter;

            if (now >= new DateTime(now.Year, 1, 1) && now < new DateTime(now.Year, 4, 1))
            {
                startOfQuarter = new DateTime(now.Year, 1, 1);
            }
            else if (now >= new DateTime(now.Year, 4, 1).AddDays(29) && now < new DateTime(now.Year, 7, 1))
            {
                startOfQuarter = new DateTime(now.Year, 4, 1);
            }
            else if (now >= new DateTime(now.Year, 7, 1).AddDays(29) && now < new DateTime(now.Year, 10, 1))
            {
                startOfQuarter = new DateTime(now.Year, 7, 1);
            }
            else
            {
                startOfQuarter = new DateTime(now.Year, 10, 1);
            }

            foreach (Review review in reviews)
            {
                if (review.Date >= startOfQuarter)
                    isPendingReview = false;
            }

            return isPendingReview;
        }
    }
}
