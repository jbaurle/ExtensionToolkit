using System;
using System.Collections;
using System.Reflection;

namespace System
{
	/// <summary>
	/// Represents a collection of useful extenions methods.
	/// </summary>
	public static class ObjectExtensions
	{
		/// <summary>
		/// Indicates whether the specified attribute type is defined for the current object 
		/// instance.
		/// </summary>
		public static bool HasCustomAttribute(this object o, Type attributeToFind)
		{
			if(typeof(Attribute).IsAssignableFrom(attributeToFind) == false)
				throw new ArgumentException("attributeToFind");

			object[] attributes = o.GetType().GetCustomAttributes(attributeToFind, true);

			return (attributes != null && attributes.Length > 0);
		}

		/// <summary>
		/// Indicates whether the specified list contains the current object instance.
		/// </summary>
		public static bool IsIn(this object o, IEnumerable list)
		{
			foreach(object i in list)
			{
				if(i.Equals(o))
					return true;
			}

			return false;
		}
	}
}
