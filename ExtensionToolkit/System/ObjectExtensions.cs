using System;
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
	}
}
