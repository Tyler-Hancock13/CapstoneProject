using CapstoneProject.Model.Entities;
using CapstoneProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.API.Controllers
{
    public abstract class CommonController : ApiController
    {
        /// <summary>
        /// The User Service
        /// </summary>
        private UserService userService = new UserService();

        /// <summary>
        /// Hashes a string using MD5 Format.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <returns>The hash as a string.</returns>
        public string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        /// <summary>
        /// Checks if a User is Authenticated, used for protecting routes.
        /// </summary>
        /// <param name="request">The HTTP Request made to the server.</param>
        /// <param name="acceptedRoles">The User-Roles allowed to access the route.</param>
        /// <returns></returns>
        public bool UserIsAuthenticated(List<Role> acceptedRoles)
        {
            HttpRequestHeaders headers = Request.Headers;

            if (headers.Contains("EmployeeID") && headers.Contains("Password"))
            {
                string hashedPassword = GetMd5Hash(headers.GetValues("Password").First());
                string employeeID = headers.GetValues("EmpoyeeID").First();

                User user = userService.Get(employeeID, hashedPassword);

                if (user != null)
                {
                    foreach (Role role in acceptedRoles)
                    {
                        if (user.Role == role)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
