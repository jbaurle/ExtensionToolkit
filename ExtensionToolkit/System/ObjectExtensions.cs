using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
			if (typeof(Attribute).IsAssignableFrom(attributeToFind) == false)
				throw new ArgumentException("attributeToFind");

			object[] attributes = o.GetType().GetCustomAttributes(attributeToFind, true);

			return (attributes != null && attributes.Length > 0);
		}

		/// <summary>
		/// Indicates whether the specified list contains the current object instance.
		/// </summary>
		public static bool IsIn(this object o, IEnumerable list)
		{
			foreach (object i in list)
			{
				if (i.Equals(o))
					return true;
			}

			return false;
		}

		/// <summary>
		/// Get the represent value of the object in System.String type.
		/// </summary>
		/// <param name="o">Extension object</param>
		/// <returns>string</returns>
		public static string ToValue(this object o)
		{
			if (o == null)
			{
				return "null";
			}
			else if (o is DateTime)
			{
				return ((DateTime)o).ToString("yyyy-MM-dd HH:mm:ss");
			}
			else if (o is ValueType || o is string)
			{
				if (o is char)
				{
					return "'" + o.ToString() + "'";
				}
				else if (o is string)
				{
					return "\"" + o.ToString() + "\"";
				}
				else
				{
					return o.ToString();
				}
			}
			else if (o is System.Collections.DictionaryEntry) // Hashtable
			{
				return ((System.Collections.DictionaryEntry)o).Key.ToValue() + "=" + ((System.Collections.DictionaryEntry)o).Value.ToValue();
			}
			else if (o is IEnumerable)
			{
				StringBuilder sb = new StringBuilder();

				foreach (var item in (IEnumerable)o)
				{
					if (o is Hashtable)
					{
						DictionaryEntry ht = (DictionaryEntry)item;

						sb.Append(
							"[" +
							ht.Key.ToValue() +
							"]=[" +
							ht.Value.ToValue() +
							"],");
					}
					else if (o is NameValueCollection)
					{
						sb.Append(
							"\"" + item.ToString() + "\"" +
							"=" +
							"\"" + ((NameValueCollection)o)[item.ToString()] + "\"" + ",");
					}
					else
					{
						sb.Append(item.ToValue() + ",");
					}
				}

				sb.Length = sb.Length - 1;

				return sb.ToString();
				//return "IEnumerable { ... }";
			}
			else
			{
				return "{ " + o.ToString() + "}";
			}
		}

		/// <summary>
		/// Get the all the values inside object in VERY detail.  Useful for debugging purpose.
		/// </summary>
		/// <param name="o">object</param>
		/// <param name="prefix">an object id or name</param>
		/// <returns>string</returns>
		public static string ToString(this object o, string prefix)
		{
			return o.ToString(prefix, 0);
		}

		/// <summary>
		/// Get the all the values inside object in VERY detail. Useful for debugging purpose.
		/// </summary>
		private static string ToString(this object o, string prefix, int depth)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			if (o == null || o is ValueType || o is string)
			{
				sb.Append(prefix);
				sb.Append(" = ");
				sb.Append(o.ToValue());
				sb.AppendLine();
			}
			else if (o is IEnumerable)
			{
				foreach (var item in (IEnumerable)o)
				{
					if (o is Hashtable)
					{
						DictionaryEntry ht = (DictionaryEntry)item;

						sb.AppendLine(
							prefix +
							"[" +
							ht.Key.ToValue() +
							"] = " +
							ht.Value.ToValue());
					}
					else if (o is NameValueCollection)
					{
						sb.AppendLine(
							prefix +
							"[\"" + item.ToString() + "\"]" +
							" = " +
							"\"" + ((NameValueCollection)o)[item.ToString()] + "\"");
					}
					else
					{
						sb.Append(item.ToValue());
					}
				}
			}
			else
			{
				MemberInfo[] members = o.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);

				foreach (MemberInfo mi in members)
				{
					FieldInfo fi = mi as FieldInfo;
					PropertyInfo pi = mi as PropertyInfo;
					if (fi != null || pi != null)
					{
						sb.Append(prefix + "." + mi.Name + " = ");

						Type t = (fi != null) ? fi.FieldType : pi.PropertyType;

						if (t.IsValueType || t == typeof(string))
						{
							if (fi != null)
							{
								sb.Append(fi.GetValue(o).ToValue());
							}
							else
							{
								sb.Append(pi.GetValue(o, null).ToValue());
							}
						}
						else
						{
							if (typeof(IEnumerable).IsAssignableFrom(t))
							{
								sb.Append(pi.GetValue(o, null).ToValue());
							}
							else
							{
								sb.AppendLine("{");
								sb.AppendLine(pi.GetValue(o, null).ToString(prefix + "." + pi.Name, depth + 1));
								sb.Append("}");
							}
						}

						sb.AppendLine();
					}
				}
			}

			if (depth > 0)
			{
				StringBuilder sb1 = new StringBuilder();
				foreach (string line in sb.ToString().Split(Environment.NewLine))
				{
					if (!String.IsNullOrEmpty(line))
					{
						sb1.AppendLine(line.PrependWithTabs(1));
					}
				}

				return sb1.ToString().Trim();
			}
			else
			{
				return sb.ToString().Trim();
			}
		}

		/// <summary>
		/// Creates an Enumerable containing this item as it's only memeber.
		/// </summary>
		public static IEnumerable<T> ToSingleItemEnumerable<T>(this T objectToTranslateToEnumerable)
		{
			yield return objectToTranslateToEnumerable;
		}

		/// <summary>
		/// Creates a List containing this item as it's only memeber.
		/// </summary>
		public static IList<T> ToSingleItemList<T>(this T objectToTranslateToEnumerable)
		{
			return new List<T> { objectToTranslateToEnumerable };
		}

		/// <summary>
		/// Creates an Array containing this item as it's only memeber.
		/// </summary>
		public static T[] ToSingleItemArray<T>(this T objectToTranslateToEnumerable)
		{
			return new T[] { objectToTranslateToEnumerable };
		}


		/// <summary>
		/// Creates a deep copy of a object.
		/// 
		/// Note: object must be serializable.
		/// 
		/// Note: this will not handle an object with events. 
		/// However a work around is described here
		/// http://www.lhotka.net/WeBlog/CommentView.aspx?guid=776f44e8-aaec-4845-b649-e0d840e6de2c
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objectToCopy"></param>
		/// <returns></returns>
		public static T SerializedDeepCopy<T>(this T objectToCopy)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();

			binaryFormatter.Serialize(memoryStream, objectToCopy);
			memoryStream.Seek(0, SeekOrigin.Begin);

			return (T)binaryFormatter.Deserialize(memoryStream);
		}

	}
}
