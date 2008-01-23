using System;

namespace System
{
	/// <summary>
	/// Represents a collection of useful extenions methods.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Indicates whether the specified String object is null or an Empty string.
		/// </summary>
		/// <returns>true if the value parameter is null or an empty string (""); otherwise, false.</returns>
		public static bool IsNullOrEmpty(this string s)
		{
			return string.IsNullOrEmpty(s);
		}
	}
}
