using IDP.Domain.Entities;
using IDP.Domain.IRepository.Command;
using IDP.Infra.Data;
using IDP.Infra.Repository.Command.Base;

namespace IDP.Infra.Repository.Command
{
    public class UserCommandRepository : CommandRepository<User>, IUserCommandRepository
    {
        private readonly ShopCommandDbContext shopCommandDbContext;
       
        public UserCommandRepository(ShopCommandDbContext context) : base(context)
        {
            shopCommandDbContext = context; 
           
        }
      
    }
}
