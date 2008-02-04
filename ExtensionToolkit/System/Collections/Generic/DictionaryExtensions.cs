using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace System.Collections.Generic
{
    /// <summary>
    /// Represents a collection of useful extenions methods.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Translate a dictionary into a string for display
        /// </summary>
        public static string PrettyPrint<K, V>(this IDictionary<K, V> dict)
        {
            if (dict == null)
                return "";
            string dictStr = "[";
            ICollection<K> keys = dict.Keys;
            int i = 0;
            foreach (K key in keys)
            {
                dictStr += key.ToString() + "=" + dict[key].ToString();
                if (i++ < keys.Count - 1)
                {
                    dictStr += ", ";
                }
            }
            return dictStr + "]";
        }
    }
}
