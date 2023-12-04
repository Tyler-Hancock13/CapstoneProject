using CapstoneProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CapstoneProject.API.Controllers
{
    [RoutePrefix("api/jobs")]
    public class JobsController : CommonController
    {
        /// <summary>
        /// The Job Service
        /// </summary>
        JobService service = new JobService();
        
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
        
        [Route("{id}")]
        public IHttpActionResult GetByID(int id)
        {
            try
            {
                return Ok(service.Get(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}
