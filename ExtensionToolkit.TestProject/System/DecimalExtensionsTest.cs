using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionToolkit.TestProject.System
{
	[TestClass()]
	public class DecimalExtensionsTest
	{
		[TestMethod()]
		public void TruncatePrecision_Zero()
		{
			decimal myNumber = 1.1234567M;

			Assert.AreEqual(1.0M, myNumber.TruncatePrecision(0));
		}

		[TestMethod()]
		public void TruncatePrecision_One()
		{
			decimal myNumber = 1.1234567M;

			Assert.AreEqual(1.1M, myNumber.TruncatePrecision(1));
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void TruncatePrecision_Negative()
		{
			decimal myNumber = 1.1234567M;
			decimal temp = myNumber.TruncatePrecision(-1);

			Assert.Fail("This should have thrown an exception");
		}
	}
}
