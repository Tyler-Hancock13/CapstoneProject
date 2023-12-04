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
    /// Department Repository
    /// </summary>
    public class DepartmentRepo
    {
        /// <summary>
        /// Data Access Object
        /// </summary>
        private DataAccess db = new DataAccess();

        /// <summary>
        /// Inserts a Department into the Database
        /// </summary>
        /// <param name="department">The Department to be inserted</param>
        /// <returns>The inserted Department with the newly created ID</returns>
        public Department Add(Department department)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@Version", department.Version, SqlDbType.Timestamp, ParameterDirection.Output),
                new ParmStruct("@Name", department.Name, SqlDbType.NVarChar),
                new ParmStruct("@Description", department.Description, SqlDbType.NVarChar),
                new ParmStruct("@InvocationDate", department.InvocationDate, SqlDbType.DateTime)
            };

            if (db.ExecuteNonQuery("Departments_Insert", parms) > 0)
            {
                department.Version = (byte[])parms[0].Value;
                return department;
            }

            return null;
        }

        /// <summary>
        /// Gets all Departments
        /// </summary>
        /// <returns>A List of all Departments.</returns>
        public List<Department> Get()
        {
            List<Department> departments = new List<Department>();
            DataTable dt = db.Execute("Departments_GetAll", CommandType.StoredProcedure);

            foreach (DataRow row in dt.Rows)
            {
                departments.Add(UnpackDepartment(row));
            }

            return departments;
        }

        /// <summary>
        /// Gets a Department by ID
        /// </summary>
        /// <param name="id">The ID of the Department to retrieve</param>
        /// <returns>The Department with the matching ID</returns>
        public Department Get(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@ID", id, SqlDbType.Int)
            };

            DataTable dt = db.Execute("Departments_GetByID", CommandType.StoredProcedure, parms);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return UnpackDepartment(dt.Rows[0]);
        }

        /// <summary>
        /// Updates a Department.
        /// </summary>
        /// <param name="department">The Department to be updated.</param>
        /// <returns>The updated Department.</returns>
        public Department Update(Department department)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@ID", department.ID, SqlDbType.Int),
                new ParmStruct("@Version", department.Version, SqlDbType.Timestamp, ParameterDirection.InputOutput),
                new ParmStruct("@Name", department.Name, SqlDbType.NVarChar),
                new ParmStruct("@Description", department.Description, SqlDbType.NVarChar),
                new ParmStruct("@InvocationDate", department.InvocationDate, SqlDbType.DateTime)
            };

            if (db.ExecuteNonQuery("Department_Update", parms) > 0)
                department.Version = (byte[])parms[1].Value;

            return department;
        }

        /// <summary>
        /// Deletes a department from the database.
        /// </summary>
        /// <param name="department">The department to delete.</param>
        /// <returns>The deleted department.</returns>
        public Department Delete(Department department)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@ID", department.ID, SqlDbType.Int)
            };

            db.ExecuteNonQuery("Department_Delete", parms);

            return department;
        }

        /// <summary>
        /// Populates a Department object with values from a DataRow.
        /// </summary>
        /// <param name="row">The DataRow to be parsed.</param>
        /// <returns>The populated Department object.</returns>
        private Department UnpackDepartment(DataRow row)
        {
            return new Department()
            {
                ID = Convert.ToInt32(row["ID"]),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                InvocationDate = Convert.ToDateTime(row["InvocationDate"]),
                Version = (byte[]) row["Version"]
            };
        }
    }
}
