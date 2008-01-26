using System;

namespace System
{
	/// <summary>
	/// Represents a collection of useful extenions methods.
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Calculate the DateTime different with the current DateTime object.
		/// datepart is referenced from T-SQL DATEDIFF function ( http://technet.microsoft.com/zh-tw/library/ms189794.aspx )
		/// </summary>
		public static double DateDiff(this DateTime startdate, string datepart, DateTime enddate)
		{
			double result = 0;

			TimeSpan tsDiff = new TimeSpan(enddate.Ticks - startdate.Ticks);

			switch(datepart.ToLower())
			{
				case "year":
				case "yyyy":
				case "yy":
					result = enddate.Year - startdate.Year;
					break;

				case "quarter":
				case "qq":
				case "q":
					const double AvgQuarterDays = 365 / 4;
					result = Math.Floor(tsDiff.TotalDays / AvgQuarterDays);
					break;

				case "month":
				case "mm":
				case "m":
					const double AvgMonthDays = 365 / 12;
					result = Math.Floor(tsDiff.TotalDays / AvgMonthDays);
					break;

				case "day":
				case "dd":
				case "d":
					result = tsDiff.TotalDays;
					break;

				case "week":
				case "wk":
				case "ww":
					result = Math.Floor(tsDiff.TotalDays / 7);
					break;

				case "hour":
				case "hh":
					result = tsDiff.TotalHours;
					break;

				case "minute":
				case "mi":
				case "n":
					result = tsDiff.TotalMinutes;
					break;

				case "second":
				case "ss":
				case "s":
					result = tsDiff.TotalSeconds;
					break;

				case "millisecond":
				case "ms":
					result = tsDiff.TotalMilliseconds;
					break;

				default:
					throw new ArgumentException("You input a invalid 'datepart' parameter.");
			}

			return result;
        }

        /// <summary>
        /// Convert the DateTime object to TaiwanCalendar DateTime format.
        /// For example: 2008/1/1 will become 97/1/1
        /// </summary>
        public static DateTime ToTaiwanDateTime(this DateTime d)
        {
            System.Globalization.TaiwanCalendar tc = new System.Globalization.TaiwanCalendar();

            DateTime result = new DateTime(tc.GetYear(d), tc.GetMonth(d), tc.GetDayOfMonth(d), d.Hour, d.Minute, d.Second, d.Millisecond);

            return result;
        }

        /// <summary>
        /// Convert the DateTime object to TaiwanLunisolarCalendar's DateTime format.
        /// For example: 2008/1/26 will become 96/12/19
        /// </summary>
        public static DateTime ToTaiwanLunisolarDateTime(this DateTime d)
        {
            System.Globalization.TaiwanLunisolarCalendar tlc = new System.Globalization.TaiwanLunisolarCalendar();

            DateTime result = new DateTime(tlc.GetYear(d), tlc.GetMonth(d), tlc.GetDayOfMonth(d), d.Hour, d.Minute, d.Second, d.Millisecond);

            return result;
        }
	}
}
