using CapstoneProject.Model.Entities;
using CapstoneProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.API.Controllers
{
    [RoutePrefix("api/authentication")]
    public class AuthenticationController : CommonController
    {
        /// <summary>
        /// The User Service
        /// </summary>
        UserService service = new UserService();

        /// <summary>
        /// The Employee service
        /// </summary>
        EmployeeService employeeService = new EmployeeService();

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Authenticate([FromBody] User user)
        {
            try
            {
                // Hashing Input User Password
                user.Password = GetMd5Hash(user.Password);

                // Attempting to retrieve user with specified EmployeeID and Hashed Password
                User dbUser = service.Get(user.EmployeeID, user.Password);

                if (dbUser != null)
                {
                    Employee employee = employeeService.Get(dbUser.EmployeeID);

                    if (employee.Status != EmployeeStatus.Active)
                    {
                        dbUser.AddError(new Error("This Employee can no longer access information.", ErrorType.Business));
                    }

                    // Clear Password and return User
                    dbUser.Password = "";
                    return Ok(dbUser);
                }
                else
                {
                    // Return 401 - Unauthorized
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}
