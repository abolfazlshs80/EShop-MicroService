using IDP.Application.Command.User;
using IDP.Domain.IRepository;
using IDP.Domain.IRepository.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Handler.Command.User
{
    public class UserHandler() : IRequestHandler<CreateUser, bool>
    {
        public async Task<bool> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            //return await userRepository.Create(new Domain.Entities.User
            //{
            //    CodeNumber = request.CodeNumber,
            //    FullName = request.FullName,
            //    UserName = request.UserName,
            //    Password = request.Password,
            //    Salt = request.Salt
            //});

            return true;
        }
    }
}
