using Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Query.Auth
{
    public class LoginUser:IRequest<JsonWebToken>
    {
        public string PhoneNumber { get; set; }
        public int Code { get; set; }
    }
}
