using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset,
            DateTimeOffset? dateOfDeath)
        {
            var dateToCalculate = dateOfDeath != null ? dateOfDeath.Value.UtcDateTime : DateTime.UtcNow;

            int age = dateToCalculate.Year - dateTimeOffset.Year;

            if (dateToCalculate < dateTimeOffset.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}
