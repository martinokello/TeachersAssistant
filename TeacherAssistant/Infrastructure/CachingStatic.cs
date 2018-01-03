using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace TeacherAssistant.Infrastructure.ExtensionMethods
{
    public static class CachingStatic
    {
        public static void Cache<T>(this System.Web.Caching.Cache cache, string key, T data, long timeInSeconds)
        {
            cache.Add(key, data, null, DateTime.Now.AddSeconds(timeInSeconds), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }
    }
}