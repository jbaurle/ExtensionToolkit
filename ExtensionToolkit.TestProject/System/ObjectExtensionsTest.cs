using System;
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
	}
}
