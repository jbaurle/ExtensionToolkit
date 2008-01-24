using System;

namespace System.Collections.Specialized
{
	/// <summary>
	/// Represents a collection of useful extenions methods.
	/// </summary>
	public static class NameValueCollectionExtensons
	{
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
	}
}
