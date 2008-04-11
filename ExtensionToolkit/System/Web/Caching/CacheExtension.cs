using System;
using System.Web.Caching;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Summary description for System.Web.Caching
/// </summary>
static public class CacheExtensionMethod
{
    public static void Clear(this Cache x)
    {
        List<string> cacheKeys = new List<string>();

        IDictionaryEnumerator cacheEnum = x.GetEnumerator();

        while (cacheEnum.MoveNext())
        {
            cacheKeys.Add(cacheEnum.Key.ToString());
        }

        foreach (string cacheKey in cacheKeys)
        {
            x.Remove(cacheKey);
        }
    }

}
