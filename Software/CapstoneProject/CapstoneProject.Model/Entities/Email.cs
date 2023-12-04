using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Model.Entities
{
    /// <summary>
    /// Email Model
    /// </summary>
    public class Email : Base
    {
        /// <summary>
        /// The email address to send the email to.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// The email address to send the email from.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// The subject of the email.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The body of the email.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// A list of email addresses to cc in the email.
        /// </summary>
        public List<string> CC { get; set; } = new List<string>();

        /// <summary>
        /// Constructor for setting all properties except CC.
        /// </summary>
        /// <param name="to">The address to send the email to.</param>
        /// <param name="from">The address to send the email from.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body of the email.</param>
        public Email(string to, string from, string subject, string body)
        {
            To = to;
            From = from;
            Subject = subject;
            Body = body;
        }

        /// <summary>
        /// Constructor for setting all properties.
        /// </summary>
        /// <param name="to">The address to send the email to.</param>
        /// <param name="from">The address to send the email from.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <param name="cc">A list of addresses to cc on the email.</param>
        public Email(string to, string from, string subject, string body, List<string> cc)
        {
            To = to;
            From = from;
            Subject = subject;
            Body = body;
            CC = cc;
        }
    }
}
