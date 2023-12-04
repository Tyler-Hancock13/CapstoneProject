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
    /// The User Repository
    /// </summary>
    public class UserRepo
    {
        /// <summary>
        /// The Data Access Object
        /// </summary>
        private DataAccess db = new DataAccess();

        /// <summary>
        /// Gets a User by EmployeeID.
        /// </summary>
        /// <param name="employeeId">The Employee ID of the Account to Retrieve.</param>
        /// <returns>The User Account with the Specified Employee ID.</returns>
        public User Get(string employeeId)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@ID", employeeId, SqlDbType.VarChar)
            };

            DataTable dt = db.Execute("User_GetByEmployeeID", CommandType.StoredProcedure, parms);

            if (dt.Rows.Count != 0)
            {
                return UnpackUser(dt.Rows[0]);
            }

            return null;
        }
        
        /// <summary>
        /// Gets a User by EmployeeID and Password.
        /// </summary>
        /// <param name="employeeId">The Employee ID of the Account to Retrieve.</param>
        /// <param name="password">The Password of the Account to Retrieve.</param>
        /// <returns>The User Account with the Specified Employee ID.</returns>
        public User Get(string employeeId, string password)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@ID", employeeId, SqlDbType.VarChar),
                new ParmStruct("@Password", password, SqlDbType.NVarChar)
            };

            DataTable dt = db.Execute("User_GetByPasswordAndEmployeeID", CommandType.StoredProcedure, parms);

            if (dt.Rows.Count != 0)
            {
                return UnpackUser(dt.Rows[0]);
            }

            return null;
        }

        /// <summary>
        /// Creates a User
        /// </summary>
        /// <param name="user">The user to be created.</param>
        /// <returns>The User if it is created successfully null if not.</returns>
        public User Add(User user)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@EmployeeID", user.EmployeeID, SqlDbType.VarChar),
                new ParmStruct("@Password", user.Password, SqlDbType.NVarChar),
                new ParmStruct("@RoleID", (int) user.Role, SqlDbType.Int)
            };

            bool success = db.ExecuteNonQuery("", parms) > 0;

            if (success)
                return user;

            return null;
        }

        /// <summary>
        /// Populates a User Object using a DataRow.
        /// </summary>
        /// <param name="row">The DataRow to be parsed.</param>
        /// <returns>The User Parsed from the DataRow.</returns>
        private User UnpackUser(DataRow row)
        {
            return new User()
            {
                ID = Convert.ToInt32(row["ID"]),
                EmployeeID = row["EmployeeID"].ToString(),
                Password = row["Password"].ToString(),
                Role = (Role)Enum.Parse(typeof(Role), row["RoleID"].ToString())
            };
        }
    }
}
