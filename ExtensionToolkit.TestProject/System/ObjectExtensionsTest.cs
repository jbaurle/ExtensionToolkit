using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionToolkit.TestProject
{
	[TestClass()]
	public class ObjectExtensionsTest
	{
		public TestContext TestContext { get; set; }

		[TestMethod()]
		public void HasCustomAttributeTest()
		{
			ObjectExtensionsTest tc = new ObjectExtensionsTest();
			Assert.AreEqual(true, tc.HasCustomAttribute(typeof(TestClassAttribute)));
			Assert.AreEqual(false, tc.HasCustomAttribute(typeof(TestMethodAttribute)));
		}

		[TestMethod()]
		public void IsInTest()
		{
			List<string> l = new List<string>();
			l.Add("ABC");
			l.Add("XYZ");
			l.Add("MIT");
			Assert.AreEqual(true, "MIT".IsIn(l));
			Assert.AreEqual(false, "MIT2".IsIn(l));
		}
	}
}
