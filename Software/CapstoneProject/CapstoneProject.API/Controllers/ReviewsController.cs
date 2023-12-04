using CapstoneProject.Model.Entities;
using CapstoneProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CapstoneProject.API.Controllers
{
    [RoutePrefix("api/reviews")]
    public class ReviewsController : CommonController
    {
        /// <summary>
        /// Review Service
        /// </summary>
        ReviewService service = new ReviewService(new EmailService(), new EmployeeService());

        [Route("create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] Review review)
        {
            try
            {
                return Ok(service.Add(review));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [Route("pending/{supervisorId}")]
        public IHttpActionResult GetPendingEmployees(string supervisorId)
        {
            try
            {
                return Ok(service.GetPendingEmployees(supervisorId));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [Route("{employeeId}")]
        public IHttpActionResult GetByEmployee(string employeeId)
        {
            try
            {
                return Ok(service.GetByEmployee(employeeId));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}
