using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionToolkit.TestProject
{
	[TestClass()]
	public class DateTimeExtensionsTest
	{
		public TestContext TestContext { get; set; }

		[TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void DateDiffTest()
		{
            DateTime startdate = DateTime.Now;
            DateTime enddate;

            enddate = startdate.AddYears(3);
            Assert.AreEqual(3, startdate.DateDiff("year", enddate));
            Assert.AreEqual(3, startdate.DateDiff("yyyy", enddate));
            Assert.AreEqual(3, startdate.DateDiff("yy", enddate));

            enddate = startdate.AddMonths(3);
            Assert.AreEqual(1, startdate.DateDiff("quarter", enddate));
            Assert.AreEqual(1, startdate.DateDiff("qq", enddate));
            Assert.AreEqual(1, startdate.DateDiff("q", enddate));

            Assert.AreEqual(3, startdate.DateDiff("month", enddate));
            Assert.AreEqual(3, startdate.DateDiff("mm", enddate));
            Assert.AreEqual(3, startdate.DateDiff("m", enddate));

            enddate = startdate.AddDays(7);
            Assert.AreEqual(1, startdate.DateDiff("week", enddate));
            Assert.AreEqual(1, startdate.DateDiff("wk", enddate));
            Assert.AreEqual(1, startdate.DateDiff("ww", enddate));

            Assert.AreEqual(7, startdate.DateDiff("day", enddate));
            Assert.AreEqual(7, startdate.DateDiff("dd", enddate));
            Assert.AreEqual(7, startdate.DateDiff("d", enddate));

            enddate = startdate.AddHours(1);
            Assert.AreEqual(1, startdate.DateDiff("hour", enddate));
            Assert.AreEqual(1, startdate.DateDiff("hh", enddate));

            Assert.AreEqual(60, startdate.DateDiff("minute", enddate));
            Assert.AreEqual(60, startdate.DateDiff("mi", enddate));
            Assert.AreEqual(60, startdate.DateDiff("n", enddate));

            Assert.AreEqual(60 * 60, startdate.DateDiff("second", enddate));
            Assert.AreEqual(60 * 60, startdate.DateDiff("ss", enddate));
            Assert.AreEqual(60 * 60, startdate.DateDiff("s", enddate));

            Assert.AreEqual(60 * 60 * 1000, startdate.DateDiff("millisecond", enddate));
            Assert.AreEqual(60 * 60 * 1000, startdate.DateDiff("ms", enddate));

            // Throw ArgumentException if input wrong 'datepart' parameter.
            startdate.DateDiff("", enddate);
            startdate.DateDiff("abc", enddate);
        }
	}
}
