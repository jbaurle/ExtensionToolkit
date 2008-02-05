using System;
using System.Data;
using System.Reflection;

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

		/// <summary>
		/// Converts a given List of object into a List of targetValueType        
		/// </summary>       
		public static object ConvertTo(this List<object> list, Type targetValueType)
		{
			if(typeof(object).Equals(targetValueType))
			{
				return list;
			}
			else
			{
				Type targetGenericListType = typeof(List<>).MakeGenericType(targetValueType);
				object genericList = Activator.CreateInstance(targetGenericListType);

				MethodInfo method = targetGenericListType.GetMethod("Add", new Type[] { targetValueType });
				foreach(object item in list)
				{
					if(targetValueType.IsAssignableFrom(item.GetType()))
					{
						method.Invoke(genericList, new object[] { item });
					}
					else
					{
						throw new ArgumentException("List entry ist of type "
							+ item.GetType().Name + " but should be of type " + targetValueType.Name);
					}
				}
				return genericList;
			}
		}

		/// <summary>
		/// Translate a list into a string for display.
		/// </summary>
		public static string PrettyPrint<T>(this IList<T> list)
		{
			string listStr = "[";
			for(int i = 0; i < list.Count; i++)
			{
				listStr += list[i].ToString();
				if(i < list.Count - 1)
				{
					listStr += ", ";
				}
			}
			return listStr + "]";
		}
	}
}
