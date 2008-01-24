using System;
using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionToolkit.TestProject
{
	[TestClass()]
	public class NameValueCollectionExtensionsTest
	{
		public TestContext TestContext { get; set; }

		[TestMethod()]
		public void ContainsKeyTest()
		{
			NameValueCollection c = new NameValueCollection();
			c.Add("ABC", "ABC-val");
			c.Add("XYZ", "XYZ-val");
			c.Add("MIT", "MIT-val");
			Assert.AreEqual(true, c.ContainsKey("MIT"));
			Assert.AreEqual(true, c.ContainsKey("xyz"));
			Assert.AreEqual(false, c.ContainsKey("BCZ"));
		}
	}
}
