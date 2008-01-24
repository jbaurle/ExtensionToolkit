using System;
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
		//     -> IsEmailaddress
		//     -> IsStrongPassword (http://regexlib.com/REDetails.aspx?regexp_id=2062)
		//     -> ToList, ToNameValueCollection, ToDictionary
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
		/// Removes the specified string value from the current string object at the
		/// beginning and the end.
		/// </summary>
		public static string Trim(this string s, string trimString)
		{
			return s.Trim(trimString.ToCharArray());
		}
	}
}
