using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartLib.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDateOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDateOrMonth(this DateTime date)
        { 
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));// DaysInMonth => 그달의 날짜수
        }
    }
}
