using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Query.Auth
{
    public class LoginUser:IRequest<bool>
    {
        public string UserName { get; set; }
        public string Pswword { get; set; }
    }
}
