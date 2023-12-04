using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Model.Entities
{
    public abstract class Base
    {
        public List<Error> Errors { get; set; } = new List<Error>();

        public bool IsValid()
        {
            return Errors.Count == 0;
        }

        public void AddError(Error error)
        {
            Errors.Add(error);
        }
    }
}
