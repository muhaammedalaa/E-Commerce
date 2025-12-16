using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Service.Contract
{
    public interface ICacheService
    {
        Task<string> GetCacheAsync(string key);
        Task SetCacheAsync(string key, object response, TimeSpan expireDate);
    }
}
