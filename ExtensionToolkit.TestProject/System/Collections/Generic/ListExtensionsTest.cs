using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionToolkit.TestProject
{
	[TestClass()]
	public class ListExtensionsTest
	{
		public TestContext TestContext { get; set; }

		[TestMethod()]
		public void ToDataTableStructureTest()
		{
			List<string> columns = new List<string>();
			columns.Add("ID");
			columns.Add("Name");
			DataTable dt = columns.ToDataTableStructure();
			Assert.AreEqual("ID", dt.Columns[0].ColumnName);
			Assert.AreEqual("Name", dt.Columns[1].ColumnName);
		}
	}
}
