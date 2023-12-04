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
    /// The Job Repository
    /// </summary>
    public class JobRepo
    {
        /// <summary>
        /// Data Access Object
        /// </summary>
        private DataAccess db = new DataAccess();
        
        /// <summary>
        /// Retrieves all Jobs
        /// </summary>
        /// <returns></returns>
        public List<Job> Get()
        {
            List<Job> jobs = new List<Job>();
            DataTable dt = db.Execute("Jobs_GetAll", CommandType.StoredProcedure);

            foreach (DataRow row in dt.Rows)
            {
                jobs.Add(UnpackJob(row));
            }
            
            return jobs;
        }

        /// <summary>
        /// Gets a Job by ID
        /// </summary>
        /// <param name="id">The ID of the Job to be retrieved</param>
        /// <returns>The Job with the matching ID</returns>
        public Job Get(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@ID", id, SqlDbType.Int)
            };

            DataTable dt = db.Execute("Jobs_GetByID", CommandType.StoredProcedure, parms);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return UnpackJob(dt.Rows[0]);
        }

        /// <summary>
        /// Populates a Job object using a DataRow.
        /// </summary>
        /// <param name="row">The DataRow to be parsed.</param>
        /// <returns>The populated Job object.</returns>
        private Job UnpackJob(DataRow row)
        {
            return new Job()
            {
                ID = Convert.ToInt32(row["ID"]),
                Name = row["Name"].ToString()
            };
        }
    }
}
