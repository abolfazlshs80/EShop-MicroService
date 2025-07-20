using Auth;
using IDP.Application.Query.Auth;
using IDP.Domain.IRepository.Command;
using IDP.Domain.IRepository.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Handler.Query.Auth
{
    public class LoginAuthHandler : IRequestHandler<LoginUser, JsonWebToken>
    {
        private readonly IJwtHandler _jwtHandler;
        private readonly IOtpRedisRepository _redisRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        public LoginAuthHandler(IJwtHandler jwtHandler, IOtpRedisRepository otpRedisRepository, IUserQueryRepository userQueryRepository)
        {
            _jwtHandler = jwtHandler;
            _redisRepository = otpRedisRepository;
            _userQueryRepository = userQueryRepository;
        }
        public async Task<JsonWebToken> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _redisRepository.Getdata(request.PhoneNumber);
                if (res == null) return null;
                if (res.OtpCode == request.Code)
                {
                    var user = await _userQueryRepository.GetUserAsync(request.PhoneNumber);
                    var token = _jwtHandler.Create(user.Id);
                    return token;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
