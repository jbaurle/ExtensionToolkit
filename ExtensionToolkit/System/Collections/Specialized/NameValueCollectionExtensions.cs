﻿using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Collections.Specialized
{
	/// <summary>
	/// Represents a collection of useful extenions methods.
	/// </summary>
	public static class NameValueCollectionExtensons
	{
		// TODO:
		//  - Add the following methods: 
		//     -> Join (Key1=Value1|...)
		//     -> ...

		/// <summary>
		/// Determines if the collection contains a specific key.
		/// </summary>
		public static bool ContainsKey(this NameValueCollection collection, string keyToFind)
		{
			if(collection == null)
				throw new ArgumentNullException("collection");
			if(keyToFind == null)
				throw new ArgumentNullException("keyToFind");

			string k = keyToFind.ToLower().Trim();

			foreach(string key in collection)
			{
				if(key.ToLower().Trim().Equals(k))
					return true;
			}

			return false;
		}

		/// <summary>
		/// Joins the key/value pairs into a string of the follwing format:
		/// "Key1=Value1|Key2=Value2|...".
		/// </summary>
		public static string Join(this NameValueCollection collection)
		{
			return Join(collection, "|");
		}

		/// <summary>
		/// Joins the key/value pairs into a string of the follwing format:
		/// "Key1=Value1|Key2=Value2|...".
		/// </summary>
		public static string Join(this NameValueCollection collection, string separator)
		{
			if(string.IsNullOrEmpty(separator))
				throw new ArgumentNullException("separator");

			StringBuilder sb = new StringBuilder();
			foreach(string key in collection.AllKeys)
				sb.AppendFormat("{0}={1}{2}", key, (collection[key] ?? string.Empty).ToString(), separator);

			return sb.ToString().TrimEnd(separator.ToCharArray());
		}

		/// <summary>
		/// Creates a XML encoding of the collection object and its current state with
		/// a given root name.
		/// </summary>
		public static string ToXml(this NameValueCollection collection, string name)
		{
			return ToXml(collection, name, null);
		}

		/// <summary>
		/// Creates a XML encoding of the collection object and its current state with
		/// a given root name and attributes.
		/// </summary>
		public static string ToXml(this NameValueCollection collection, string name, object attributes)
		{
			if(string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			StringWriter sw = new StringWriter();
			XmlTextWriter w = new XmlTextWriter(sw);

			w.Formatting = Formatting.Indented;

			w.WriteStartElement(name);

			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(attributes);

			foreach(PropertyDescriptor pd in properties)
				w.WriteAttributeString(pd.Name, pd.GetValue(attributes).ToString());

			foreach(string key in collection.AllKeys)
				w.WriteElementString(key, collection[key]);

			w.WriteEndElement();

			return sw.ToString();
		}
	}
}
