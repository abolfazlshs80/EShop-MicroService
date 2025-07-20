using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Command.Auth
{
    public class LoginMobileUser:IRequest<bool>
    {
        public required string MobileNumber { get; set; }
    }
}
