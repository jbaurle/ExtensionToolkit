using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionToolkit.TestProject
{
	[TestClass()]
	public class StringExtensionsTest
	{
		public TestContext TestContext { get; set; }

		[TestMethod()]
		public void CapitalizeTest()
		{
			string s = null;
			Assert.AreEqual(null, s.Capitalize());
			s = "";
			Assert.AreEqual("", s.Capitalize());
			Assert.AreEqual("A", "a".Capitalize());
			Assert.AreEqual("A", "A".Capitalize());
			Assert.AreEqual("Abc", "abc".Capitalize());
		}

		[TestMethod()]
		public void DefaultIfNullTest()
		{
			string s = null;
			Assert.AreEqual("", s.DefaultIfNull());
			Assert.AreEqual("", "".DefaultIfNull());
			Assert.AreEqual("ABC", "ABC".DefaultIfNull());
			Assert.AreEqual("", s.DefaultIfNull(null));
			Assert.AreEqual("", s.DefaultIfNull(""));
			Assert.AreEqual("ABC", s.DefaultIfNull("ABC"));
			Assert.AreEqual("", "".DefaultIfNull("ABC"));
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DefaultIfNullOrEmptyTest()
		{
			"".DefaultIfNullOrEmpty(null);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DefaultIfNullOrEmptyTest2()
		{
			"".DefaultIfNullOrEmpty("");
		}

		[TestMethod()]
		public void DefaultIfNullOrEmptyTest3()
		{
			string s = null;
			Assert.AreEqual("ABC", s.DefaultIfNull("ABC"));
			Assert.AreEqual("", "".DefaultIfNull("ABC"));
		}

		[TestMethod()]
		public void IsInTest()
		{
			string l = " abc   , ABC ; XYZ";
			Assert.AreEqual(false, "ABC".IsIn(l));
			Assert.AreEqual(false, "ABC2".IsIn(l));
			Assert.AreEqual(true, "XYZ".IsIn(l, ";"));
			Assert.AreEqual(true, "XYZ".IsIn(new string[] { "ABC ", "ABC2", "XYZ" }));
		}

		[TestMethod()]
		public void IsMatchTest()
		{
			string e = @"^-?\d+(\.\d{2})?$";
			Assert.AreEqual(true, "19.99".IsMatch(e));
			Assert.AreEqual(false, "1,052.21".IsMatch(e));
		}

		[TestMethod()]
		public void IsNullOrEmptyTest()
		{
			string s = null;
			Assert.AreEqual(true, s.IsNullOrEmpty());
			Assert.AreEqual(true, "".IsNullOrEmpty());
			Assert.AreEqual(false, "CodePlex".IsNullOrEmpty());
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void LeftTest()
		{
			string s = null;
			s.Left(-1);
		}

		[TestMethod()]
		public void LeftTest2()
		{
			string s = null;
			Assert.AreEqual("", s.Left(0));
			Assert.AreEqual("", s.Left(1));
			s = "";
			Assert.AreEqual("", s.Left(0));
			Assert.AreEqual("", s.Left(1));
			s = "CodePlex";
			Assert.AreEqual("Code", s.Left(4));
		}

		[TestMethod()]
		public void PrependTest()
		{
			Assert.AreEqual(true, "ABC".Prepend(2).StartsWith("  "));
			Assert.AreEqual(true, "ABC".Prepend(2, "#").StartsWith("##"));
			Assert.AreEqual(true, "ABC".PrependWithNbsp(2).StartsWith("&nbsp;&nbsp;"));
			Assert.AreEqual(true, "ABC".PrependWithTabs(2).StartsWith("\t\t"));
		}

		[TestMethod()]
		public void ReplaceTest()
		{
			string s = "Dear ${Name}, how are you? .... Date ${SignedOn}";
			Assert.AreEqual(-1, s.Replace(new { Name = "Billy", SignedOn = DateTime.Now }).IndexOf("${"));
		}

		[TestMethod()]
		public void ReplaceNewLineWithBRTest()
		{
			string s = " \r\n \r\n \r\n    -- \r\n\r\n   ";
			Assert.AreEqual(false, s.ReplaceNewLineWithBR().Contains(Environment.NewLine));
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void RightTest()
		{
			string s = null;
			s.Right(-1);
		}

		[TestMethod()]
		public void RightTest2()
		{
			string s = null;
			Assert.AreEqual("", s.Right(0));
			Assert.AreEqual("", s.Right(1));
			s = "";
			Assert.AreEqual("", s.Right(0));
			Assert.AreEqual("", s.Right(1));
			s = "CodePlex";
			Assert.AreEqual("Plex", s.Right(4));
		}

		[TestMethod()]
		public void IsSplitTest()
		{
			string l = "ABC,XYZ,MIT";
			string[] a = l.Split(",");
			Assert.AreEqual(3, a.Length);
		}

		[TestMethod()]
		public void ToDictionaryTest()
		{
			Dictionary<string, string> d = "first=ABC|second = XYZ| third = 3".ToDictionary();
			Assert.AreEqual(3, d.Count);
		}

		[TestMethod()]
		public void ToMD5Test()
		{
			string s = "ABCXYZ";
			Assert.AreNotEqual(s.Length, s.ToMD5());
		}

		[TestMethod()]
		public void ToNameValueCollectionTest()
		{
			Assert.AreEqual(3, "first=ABC|second = XYZ| third = 3".ToNameValueCollection().Count);
			Assert.AreEqual(3, "first=ABC|second = XYZ| third = 3|".ToNameValueCollection().Count);
			Assert.AreEqual(3, "first=ABC|second = XYZ| third = 3|=4".ToNameValueCollection().Count);
		}

		[TestMethod()]
		public void TrimTest()
		{
			string s = "__ABC__";
			Assert.AreEqual("ABC", s.Trim("__"));
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ToJsStringTest()
		{
			string s1 = "This is Tim's test";
			Assert.AreEqual(@"This is Tim\'s test", s1.ToJsString());
			Assert.AreEqual(@"This is Tim\'s test", s1.ToJsString('\''));
			Assert.AreEqual(@"This is Tim's test", s1.ToJsString('"'));

			string s2 = "This is Tim\"s test";
			Assert.AreEqual(@"This is Tim""s test", s2.ToJsString());
			Assert.AreEqual(@"This is Tim""s test", s2.ToJsString('\''));
			Assert.AreEqual(@"This is Tim\""s test", s2.ToJsString('"'));


			Assert.AreEqual(@"This is Tim\""s test", s2.ToJsString('$'));

			string s3 = null;
			Assert.AreEqual(@"", s3.ToJsString());
			Assert.AreEqual(@"", s3.ToJsString('\''));
			Assert.AreEqual(@"", s3.ToJsString('"'));
		}
	}
}
