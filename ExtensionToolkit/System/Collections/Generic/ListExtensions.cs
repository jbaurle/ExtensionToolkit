using System;
using System.Data;

namespace System.Collections.Generic
{
	/// <summary>
	/// Represents a collection of useful extenions methods.
	/// </summary>
	public static class ListExtensions
	{
		/// <summary>
		/// Creates an empty DataTable instance and uses the list elements to define the
		/// table structure (columns of type string).
		/// </summary>
		public static DataTable ToDataTableStructure(this List<string> list)
		{
			if(list == null || list.Count == 0)
				throw new ArgumentNullException("list");

			DataTable dt = new DataTable();

			foreach(string columnName in list)
				dt.Columns.Add(columnName, typeof(string));

			return dt;
		}
	}
}
