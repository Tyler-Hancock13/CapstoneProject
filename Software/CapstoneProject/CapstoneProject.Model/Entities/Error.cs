using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Model.Entities
{
    public class Error
    {
        public int Number { get; set; }

        public ErrorType Type { get; set; }

        public string Message { get; set; }

        public Error(int number, string message, ErrorType type)
        {
            this.Number = number;
            this.Type = type;
            this.Message = message;
        }

        public Error(string message, ErrorType type)
        {
            this.Type = type;
            this.Message = message;
        }
    }
}
