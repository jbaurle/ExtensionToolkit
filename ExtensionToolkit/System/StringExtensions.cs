using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
	/// <summary>
	/// Represents a collection of useful extenions methods.
	/// </summary>
	public static class StringExtensions
	{
		// TODO:
		//  - Add the following methods: 
		//     -> IsStrongPassword (http://regexlib.com/REDetails.aspx?regexp_id=2062)
		//     -> ToList, ToDictionary
		//     -> ...

		/// <summary>
		/// Capitalizes the current string object.
		/// </summary>
		public static string Capitalize(this string s)
		{
			return string.IsNullOrEmpty(s) ? s : s.Substring(0, 1).ToUpper() + (s.Length > 1 ? s.Substring(1) : string.Empty);
		}

		/// <summary>
		/// Returns an empty string if the current string object is null.
		/// </summary>
		public static string DefaultIfNull(this string s)
		{
			return s != null ? s : string.Empty;
		}

		/// <summary>
		/// Returns the specified default value if the current string object is null.
		/// </summary>
		public static string DefaultIfNull(this string s, string defaultValue)
		{
			return s != null ? s : (defaultValue ?? string.Empty);
		}

		/// <summary>
		/// Returns the specified default value if the current string object is null 
		/// or empty.
		/// </summary>
		public static string DefaultIfNullOrEmpty(this string s, string defaultValue)
		{
			if(string.IsNullOrEmpty(defaultValue))
				throw new ArgumentNullException("defaultValue");

			return string.IsNullOrEmpty(s) ? s : defaultValue;
		}

		/// <summary>
		/// Indicates whether the current string object is a valid emailaddresss.
		/// </summary>
		public static bool IsEmailAddress(this string s)
		{
			return (new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")).IsMatch(s);
		}

		/// <summary>
		/// Indicates whether the specified comma-separated value list contains the current 
		/// string object.
		/// </summary>
		public static bool IsIn(this string s, string value)
		{
			return s.IsIn(value, ",");
		}

		/// <summary>
		/// Indicates whether the specified value list contains the current string object.
		/// </summary>
		public static bool IsIn(this string s, string value, string separator)
		{
			if(string.IsNullOrEmpty(value))
				return string.IsNullOrEmpty(s);

			return s.IsIn(value.Split(separator.ToCharArray()));
		}

		/// <summary>
		/// Indicates whether the specified value list contains the current string object.
		/// </summary>
		public static bool IsIn(this string s, string[] value)
		{
			if(value == null || value.Length == 0)
				return string.IsNullOrEmpty(s);

			foreach(string v in value)
			{
				if(s.Equals(v.Trim()))
					return true;
			}

			return false;
		}

		/// <summary>
		/// Indicates whether the regular expression finds a match in the current string 
		/// object for the specified pattern.
		/// </summary>
		public static bool IsMatch(this string s, string pattern)
		{
			return (new Regex(pattern)).IsMatch(s);
		}

		/// <summary>
		/// Indicates whether the current string object is null or an empty string.
		/// </summary>
		public static bool IsNullOrEmpty(this string s)
		{
			return string.IsNullOrEmpty(s);
		}

		/// <summary>
		/// Retrieves a substring (left-side) of the specified length from the current 
		/// string object.
		/// </summary>
		public static string Left(this string s, int length)
		{
			if(length < 0)
				throw new ArgumentOutOfRangeException("length");

			if(length == 0 || string.IsNullOrEmpty(s))
				return string.Empty;

			return length > s.Length ? s : s.Substring(0, length);
		}

		/// <summary>
		/// Returns the current string with leading spaces.
		/// </summary>
		public static string Prepend(this string s, int count)
		{
			return Prepend(s, count, " ");
		}

		/// <summary>
		/// Returns the current string with the leading string.
		/// </summary>
		public static string Prepend(this string s, int count, string value)
		{
			if(string.IsNullOrEmpty(value))
				throw new ArgumentNullException("indentationString");

			StringBuilder sb = new StringBuilder();
			for(int i = 0; i < count; i++)
				sb.Append(value);
			sb.Append(s);

			return sb.ToString();
		}

		/// <summary>
		/// Returns the current string with leading HTML blank.
		/// </summary>
		public static string PrependWithNbsp(this string s, int count)
		{
			return Prepend(s, count, "&nbsp;");
		}

		/// <summary>
		/// Returns the current string with leading tabs.
		/// </summary>
		public static string PrependWithTabs(this string s, int count)
		{
			return Prepend(s, count, "\t");
		}

		/// <summary>
		/// Replaces variables (${PropertyName}) witin the current string object with
		/// the property values of the specified object.
		/// </summary>
		public static string Replace(this string s, object properties)
		{
			if(properties == null)
				return s;

			string rs = s;
			foreach(PropertyDescriptor pd in TypeDescriptor.GetProperties(properties))
				rs = rs.Replace("${" + pd.Name + "}", pd.GetValue(properties).ToString());

			return rs;
		}

		/// <summary>
		/// Replaces new line with BR sign.
		/// </summary>
		public static string ReplaceNewLineWithBR(this string s)
		{
			return s.Replace(Environment.NewLine, "<br />");
		}

		/// <summary>
		/// Retrieves a substring (right-side) of the specified length from the current 
		/// string object.
		/// </summary>
		public static string Right(this string s, int length)
		{
			if(length < 0)
				throw new ArgumentOutOfRangeException("length");

			if(length == 0 || string.IsNullOrEmpty(s))
				return string.Empty;

			return length > s.Length ? s : s.Substring(s.Length - length, length);
		}

		/// <summary>
		/// Returns a string array containing the substrings from the current string 
		/// object that are delimited by the given separator.
		/// </summary>
		public static string[] Split(this string s, string separator)
		{
			return s.Split(separator.ToCharArray());
		}

		/// <summary>
		/// Returns a Dictionary instance created from the current string 
		/// object if it contains a format like "firstkey=value1|second=Val2|...".
		/// </summary>
		public static Dictionary<string, string> ToDictionary(this string s)
		{
			return ToDictionary(s, "|");
		}

		/// <summary>
		/// Returns a Dictionary instance created from the current string 
		/// object if it contains a format like 
		/// "firstkey=value1[separator]second=Val2[separator]...".
		/// </summary>
		public static Dictionary<string, string> ToDictionary(this string s, string separator)
		{
			Dictionary<string, string> dic = new Dictionary<string, string>();

			NameValueCollection collection = ToNameValueCollection(s, separator);
			foreach(string key in collection.AllKeys)
				dic.Add(key, collection[key]);

			return dic;
		}

		/// <summary>
		/// Returns a MD5 representation of the current string object.
		/// </summary>
		public static string ToMD5(this string s)
		{
			byte[] bytes = (new MD5CryptoServiceProvider()).ComputeHash(Encoding.UTF8.GetBytes(s));

			StringBuilder sb = new StringBuilder();
			foreach(byte b in bytes)
				sb.Append(b.ToString("x2").ToLower());

			return sb.ToString();
		}

		/// <summary>
		/// Returns a NameValueCollection instance created from the current string 
		/// object if it contains a format like "firstkey=value1|second=Val2|...".
		/// </summary>
		public static NameValueCollection ToNameValueCollection(this string s)
		{
			return ToNameValueCollection(s, "|");
		}

		/// <summary>
		/// Returns a NameValueCollection instance created from the current string 
		/// object if it contains a format like 
		/// "firstkey=value1[separator]second=Val2[separator]...".
		/// </summary>
		public static NameValueCollection ToNameValueCollection(this string s, string separator)
		{
			if(string.IsNullOrEmpty(separator))
				throw new ArgumentNullException("separator");

			NameValueCollection collection = new NameValueCollection();

			string[] nameValuePairs = s.Split(separator.ToCharArray());

			foreach(string nvs in nameValuePairs)
			{
				string[] nvp = nvs.Split("=".ToCharArray());

				string name = nvp[0].Trim();
				string value = nvp.Length > 1 ? nvp[1].Trim() : string.Empty;

				if(name.Length > 0)
					collection.Add(name, value);
			}

			return collection;
		}

		/// <summary>
		/// Removes the specified string value from the current string object at the
		/// beginning and the end.
		/// </summary>
		public static string Trim(this string s, string trimString)
		{
			return s.Trim(trimString.ToCharArray());
		}

        /// <summary>
        /// Encode string into JavaScript string format that is encode \ and ' characters.
        /// </summary>
        public static string ToJsString(this string s)
        {
            if (s == null)
            {
                return "";
            }

            return s.Replace("'", "\\'").Replace("\r", "").Replace("\n", "\\r\\n");
        }

        /// <summary>
        /// Encode string into JavaScript string format that is encode \ and specified quote characters.
        /// </summary>
        public static string ToJsString(this string s, char quote)
        {
            if (s == null)
            {
                return "";
            }

            if (quote != '"' && quote != '\'')
            {
                throw new ArgumentOutOfRangeException("quote is limited to ' or \"");
            }

            return s.Replace(quote.ToString(), String.Concat("\\", quote.ToString())).Replace("\r", "").Replace("\n", "\\r\\n");
        }
	}
}
