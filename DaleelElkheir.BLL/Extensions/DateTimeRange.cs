using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Extensions
{
    public static class DateTimeRange
    {
        public static IEnumerable<DateTime> Range(this DateTime startDate, DateTime endDate)
        {
            return Enumerable.Range(0, (endDate - startDate).Days + 1).Select(d => startDate.AddDays(d));
        }
    }
}
