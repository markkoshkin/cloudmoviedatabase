using System;
using System.ComponentModel.DataAnnotations;

namespace CloudMovieDatabase.Models.ValidationAttributes
{
    public class NotFutureYear : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return DateTime.Now.Year >= Convert.ToDateTime(value).Year;
        }
    }
}
