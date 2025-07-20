using IDP.Domain.DTOs;
using IDP.Domain.IRepository.Base;

namespace IDP.Domain.IRepository.Command
{
    public interface IOtpRedisRepository : IBaseCommonRepository<Otp>
    {
        Task<Otp> Getdata(string mobile);
    }
}
