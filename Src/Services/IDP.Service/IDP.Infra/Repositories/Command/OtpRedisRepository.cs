using IDP.Domain.DTOs;
using IDP.Domain.IRepository.Command;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Infra.Repositories.Command
{
    public class OtpRedisRepository(IDistributedCache distributedCache, IConfiguration configuration) : IOtpRedisRepository
    {
  
        public async Task<Otp> Getdata(string mobile)
        {
            var data = distributedCache.GetString(mobile);
            if (data == null) return null;
            var otpobj = JsonConvert.DeserializeObject<Otp>(data);
            return otpobj;
        }
        public async Task<bool> Delete(Otp entity)
        {
           await distributedCache.RemoveAsync(entity.UserName.ToString());
            return true;
        }

        public Task<bool> Update(Otp entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Otp> Insert(Otp entity)
        {
            int time = Convert.ToInt32(configuration.GetSection("Otp:OtpTime").Value);
            var get = distributedCache.GetString(entity.UserName);
            distributedCache.SetString(entity.UserName.ToString(), JsonConvert.SerializeObject(entity),
                new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(time))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(time)));

            return entity;
        }
    }
}
