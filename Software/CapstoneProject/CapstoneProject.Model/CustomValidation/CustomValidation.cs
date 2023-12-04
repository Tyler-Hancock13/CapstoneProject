using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Model.CustomValidation
{
    public class CustomValidation
    {
        //internal class DueDateAttribute : ValidationAttribute
        //{
        //    //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //    //{
        //    //    if ((DateTime)value == null)
        //    //    {
        //    //        return new ValidationResult("Due date is required");
        //    //    }

        //    //}
        //    private DateTime GetReturnDateAndTime()
        //    {
        //        DateTime returnDate = new DateTime();
        //        returnDate = AddBusinessDays(DateTime.Now, -2);
        //        return returnDate;
        //    }

        //    private DateTime AddBusinessDays(DateTime dt, int nDays)
        //    {
        //        int weeks = nDays / 5;
        //        nDays %= 5;
        //        while (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
        //            dt = dt.AddDays(1);

        //        while (nDays-- > 0)
        //        {
        //            dt = dt.AddDays(1);
        //            if (dt.DayOfWeek == DayOfWeek.Saturday)
        //                dt = dt.AddDays(2);
        //        }
        //        return dt.AddDays(weeks * 7);
        //    }
        //}
    }
}
