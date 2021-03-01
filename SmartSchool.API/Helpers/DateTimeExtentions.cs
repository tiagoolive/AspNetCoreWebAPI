using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Helpers
{
    public static class DateTimeExtentions
    {
        public static int GetCurrentAge(this DateTime dateTime)
        {
            var currentAge = DateTime.UtcNow;
            int age = currentAge.Year - dateTime.Year;

            if (currentAge < dateTime.AddYears(age))
                age--;

            return age;
        }
    }
}
