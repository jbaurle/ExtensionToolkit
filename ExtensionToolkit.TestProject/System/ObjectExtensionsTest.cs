using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Specialized;

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

		[TestMethod()]
		public void ToValue()
		{
			string s = "Test string";
			Assert.AreEqual("Test string", ((object)s).ToValue());

			int i = 3999;
			Assert.AreEqual("3999", ((object)i).ToValue());

			DateTime d = new DateTime(2008, 2, 28, 23, 0, 0);
			Assert.AreEqual("2008-02-28 23:00:00", ((object)d).ToValue());

			object n = null;
			Assert.AreEqual("null", n.ToValue());
		}


		[TestMethod()]
		public void ToString_Test()
		{
			Hashtable ht = new Hashtable();
			ht["Key1"] = "Value1";
			ht["Key2"] = 12345;
			ht[12345] = "Key2";

			NameValueCollection nvc = new NameValueCollection();
			nvc["K1"] = "V1";
			nvc["K2"] = "V2";
			nvc["K3"] = "V3";
			nvc["K4"] = "V4";

			var obj = new
			{
				Field1 = "OK",
				Field2 = 123,
				Field2_1 = new int[] {
					1,2,3,4,5,6
				},
				Field3 = new char[] { 'a', 'b', 'c' },
				Field4 = new string[] { "123", "456", "789" },
				Field5 = new
				{
					Field5_1 = new char[] { 'a', 'b', 'c' },
					Field5_2 = new DateTime(2008, 2, 28, 15, 20, 14),
					Field5_3 = new
					{
						ID = "123",
						PW = "456",
						Profile = new
						{
							Name = "Will",
							Tel = "555-3384"
						},
						HT = ht,
						NVC = nvc

					},
				}
			};

			string strTest = "TEST";
			Assert.AreEqual("strTest = \"TEST\"", strTest.ToString("strTest"));

			Assert.AreEqual("obj.Field1 = \"OK\"\r\n" +
"obj.Field2 = 123\r\n" +
"obj.Field2_1 = 1,2,3,4,5,6\r\n" +
"obj.Field3 = 'a','b','c'\r\n" +
"obj.Field4 = \"123\",\"456\",\"789\"\r\n" +
"obj.Field5 = {\r\n" +
"obj.Field5.Field5_1 = 'a','b','c'\r\n" +
"	obj.Field5.Field5_2 = 2008-02-28 15:20:14\r\n" +
"	obj.Field5.Field5_3 = {\r\n" +
"	obj.Field5.Field5_3.ID = \"123\"\r\n" +
"		obj.Field5.Field5_3.PW = \"456\"\r\n" +
"		obj.Field5.Field5_3.Profile = {\r\n" +
"		obj.Field5.Field5_3.Profile.Name = \"Will\"\r\n" +
"			obj.Field5.Field5_3.Profile.Tel = \"555-3384\"\r\n" +
"		}\r\n" +
"		obj.Field5.Field5_3.HT = [\"Key2\"]=[12345],[12345]=[\"Key2\"],[\"Key1\"]=[\"Value1\"]\r\n" +
"		obj.Field5.Field5_3.NVC = \"K1\"=\"V1\",\"K2\"=\"V2\",\"K3\"=\"V3\",\"K4\"=\"V4\"\r\n" +
"	}\r\n" +
"}", obj.ToString("obj"));


			Assert.AreEqual("nvc[\"K1\"] = \"V1\"\r\n" +
"nvc[\"K2\"] = \"V2\"\r\n" +
"nvc[\"K3\"] = \"V3\"\r\n" +
"nvc[\"K4\"] = \"V4\"", ((object)nvc).ToString("nvc"));


			Assert.AreEqual("ht[\"Key2\"] = 12345\r\n" +
"ht[12345] = \"Key2\"\r\n" +
"ht[\"Key1\"] = \"Value1\"", ((object)ht).ToString("ht"));
		}


	}
}
