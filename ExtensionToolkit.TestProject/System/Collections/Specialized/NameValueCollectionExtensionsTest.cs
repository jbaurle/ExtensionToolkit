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

		[TestMethod()]
		public void JoinTest()
		{
			NameValueCollection c = new NameValueCollection();
			c.Add("ABC", "ABC-val");
			c.Add("XYZ", "XYZ-val");
			c.Add("XSA", null);
			c.Add("SSA", "");
			c.Add("MIT", "MIT-val");
			Assert.AreEqual(true, c.Join().StartsWith("ABC=ABC-val|"));
			Assert.AreEqual(true, c.Join("#").StartsWith("ABC=ABC-val#"));
		}

		[TestMethod()]
		public void ToXmlTest()
		{
			NameValueCollection c = new NameValueCollection();
			c.Add("Host", "codeplex.com");
			c.Add("Port", "80");
			string xml = c.ToXml("Server");
			Assert.AreEqual(true, xml.StartsWith("<Server"));
			xml = c.ToXml("Server", new { Active = "true" });
			Assert.AreEqual(true, xml.StartsWith("<Server"));
		}
	}
}
