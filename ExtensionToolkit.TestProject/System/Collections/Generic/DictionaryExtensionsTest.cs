using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionToolkit.TestProject.System.Collections.Generic
{
	[TestClass()]
	public class DictionaryExtensionsTest
	{
		public TestContext TestContext { get; set; }

		[TestMethod()]
		public void PrettyPrintTest()
		{
			IDictionary<string, string> dict = new Dictionary<string, string>();
			dict.Add("key one", "val one");
			dict.Add("key two", "val two");
			string pretty = dict.PrettyPrint<string, string>();
			string expected = "[key one=val one, key two=val two]";
			Assert.AreEqual(expected, pretty);
		}
	}
}
