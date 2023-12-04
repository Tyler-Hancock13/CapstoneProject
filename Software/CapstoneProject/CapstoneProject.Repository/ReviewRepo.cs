using CapstoneProject.DAL;
using CapstoneProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Repository
{
    /// <summary>
    /// The Review Repository
    /// </summary>
    public class ReviewRepo
    {
        /// <summary>
        /// Data Access Object
        /// </summary>
        DataAccess db = new DataAccess();

        public SqlDbType SqlDbType { get; private set; }

        /// <summary>
        /// Adds a Review to the Database.
        /// </summary>
        /// <param name="review">The Review to be Added.</param>
        /// <returns>The Added Review.</returns>
        public Review Add(Review review)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@Date", review.Date, SqlDbType.DateTime),
                new ParmStruct("@RatingID", (int) review.Rating, SqlDbType.Int),
                new ParmStruct("@Comment", review.Comment, SqlDbType.NVarChar),
                new ParmStruct("@EmployeeID", review.EmployeeID, SqlDbType.VarChar),
                new ParmStruct("@SupervisorID", review.SupervisorID, SqlDbType.VarChar)
            };

            db.ExecuteNonQuery("Reviews_Insert", parms);

            return review;
        }

        /// <summary>
        /// Retrieves the Reviews for a specific Employee.
        /// </summary>
        /// <param name="employeeId">ID of the Employee to retrieve Reviews for.</param>
        /// <returns>A List of the Employees Reviews.</returns>
        public List<Review> GetByEmployee(string employeeId)
        {
            List<Review> reviews = new List<Review>();

            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@EmployeeID", employeeId, SqlDbType.VarChar)
            };

            DataTable dt = db.Execute("Reviews_GetByEmployee", CommandType.StoredProcedure, parms);

            foreach (DataRow row in dt.Rows)
            {
                reviews.Add(UnpackReview(row));
            }

            return reviews;
        }

        /// <summary>
        /// Adds a reminder to the database.
        /// </summary>
        /// <param name="reminder">The reminder to be inserted.</param>
        /// <returns>The inserted reminder.</returns>
        public Reminder AddReminder(Reminder reminder)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@Date", reminder.Date, SqlDbType.DateTime)
            };

            db.ExecuteNonQuery("Reminders_Insert", parms);

            return reminder;
        }

        /// <summary>
        /// Gets the most recent reminder from the database.
        /// </summary>
        /// <returns>The most recent reminder or null if there are none.</returns>
        public Reminder GetMostRecentReminder()
        {
            DataTable dt = db.Execute("Reminders_GetMostRecent");
            return dt.Rows.Count > 0 ? UnpackReminder(dt.Rows[0]) : null;
        }

        /// <summary>
        /// Populates a reminder model from a datarow.
        /// </summary>
        /// <param name="row">The row to parse.</param>
        /// <returns>The populated reminder.</returns>
        private Reminder UnpackReminder(DataRow row)
        {
            return new Reminder
            {
                ID = Convert.ToInt32(row["ID"]),
                Date = Convert.ToDateTime(row["Date"])
            };
        }

        /// <summary>
        /// Parses a DataRow and populates a Review model.
        /// </summary>
        /// <param name="row">The DataRow to be parsed.</param>
        /// <returns>The populated Review model.</returns>
        private Review UnpackReview(DataRow row)
        {
            return new Review
            {
                ID = Convert.ToInt32(row["ID"]),
                Date = Convert.ToDateTime(row["Date"]),
                Rating = (Rating)Enum.Parse(typeof(Rating), row["RatingID"].ToString()),
                Comment = row["Comment"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                SupervisorID = row["SupervisorID"].ToString()
            };
        }
    }
}
