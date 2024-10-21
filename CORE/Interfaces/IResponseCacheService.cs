using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE.Interfaces
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cachekey, object response, TimeSpan timeToLive);
        Task<string> GetCachedResponseAsync(string cachekey);
    }
}