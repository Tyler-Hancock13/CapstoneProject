using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Model
{
    /// <summary>
    /// A class containing custom types
    /// </summary>
    public class Types
    {
        public struct ParmStruct
        {
            public ParmStruct(string name, object value,
                SqlDbType dataType, ParameterDirection direction = ParameterDirection.Input, int size = 0)
            {
                Name = name;
                Value = value;
                Size = size;
                DataType = dataType;
                Direction = direction;
            }

            public string Name;
            public object Value;
            public int Size;
            public SqlDbType DataType;
            public ParameterDirection Direction;
        }

        /// <summary>
        /// Represents the type of an Error
        /// </summary>
        public enum ErrorType
        {
            Model,
            Business
        }

        /// <summary>
        /// Represents an Employees Role at the Company
        /// </summary>
        public enum Role
        {
            HRSupervisor,
            Supervisor,
            HREmployee,
            Employee
        }

        /// <summary>
        /// Represents an Employees employment status.
        /// </summary>
        public enum EmployeeStatus
        {
            Active,
            Retired,
            Terminated
        }

        /// <summary>
        /// Represents the rating for an Employee Review.
        /// </summary>
        public enum Rating
        {
            BelowExpectations,
            MeetsExpectations, 
            ExceedsExpectations
        }

        public enum ItemStatus
        {
            Pending = 1,
            Approved = 2,
            Denied = 3
        }

        public enum RequestStatus
        {
            Pending = 1,
            UnderReview = 2,
            Closed = 3
        }
    }
}
