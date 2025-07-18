using Auth;
using IDP.Application.Query.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Handler.Query.Auth
{
    public class LoginAuthHandler(IJwtHandler jwtHandler) : IRequestHandler<LoginUser,bool>
    {
        public async Task<bool> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var token = jwtHandler.Create(22);
            return true;
        }
    }
}
