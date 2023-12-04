using CapstoneProject.Model.Entities;
using CapstoneProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Service
{
    /// <summary>
    /// Job Service
    /// </summary>
    public class JobService
    {
        /// <summary>
        /// Job Repo
        /// </summary>
        private JobRepo repo = new JobRepo();

        /// <summary>
        /// Retrieves all Jobs
        /// </summary>
        /// <returns></returns>
        public List<Job> Get()
        {
            return repo.Get();
        }

        /// <summary>
        /// Gets a Job by ID
        /// </summary>
        /// <param name="id">The ID of the Job to be retrieved</param>
        /// <returns>The Job with the matching ID</returns>
        public Job Get(int id)
        {
            return repo.Get(id);
        }

        /// <summary>
        /// Validates the Job
        /// </summary>
        /// <param name="job">The Job to be validated</param>
        /// <returns>A boolean value representing whether or not the Job is valid</returns>
        private bool Validate(Job job)
        {
            return job.Errors.Count == 0;
        }
    }
}
