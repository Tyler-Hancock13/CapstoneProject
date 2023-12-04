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
    [RoutePrefix("api/employees")]
    public class EmployeesController : CommonController
    {
        /// <summary>
        /// Employee Service
        /// </summary>
        private EmployeeService service = new EmployeeService();

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(service.Get());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetByID(string id)
        {
            try
            {
                Employee employee = service.Get(id);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
        
        [HttpGet]
        [Route("search/{lastName}")]
        public IHttpActionResult Search(string lastName)
        {
            try
            {
                return Ok(service.Search(lastName));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        [Route("department/{id}")]
        public IHttpActionResult GetByDepartment(int id)
        {
            try
            {
                return Ok(service.GetByDepartment(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        [Route("supervisor/{id}")]
        public IHttpActionResult GetBySupervisor(string id)
        {
            try
            {
                return Ok(service.GetBySupervisor(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [Route("update")]
        [HttpPost]
        public IHttpActionResult Update([FromBody] Employee employee)
        {
            try
            {
                return Ok(service.Update(employee));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}
