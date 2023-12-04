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
    [RoutePrefix("api/departments")]
    public class DepartmentsController : CommonController
    {
        /// <summary>
        /// The Department Service.
        /// </summary>
        DepartmentService service = new DepartmentService();

        [Route("")]
        [HttpGet]
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

        [Route("update")]
        [HttpPost]
        public IHttpActionResult Update([FromBody] Department department)
        {
            try
            {
                return Ok(service.Update(department));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}

