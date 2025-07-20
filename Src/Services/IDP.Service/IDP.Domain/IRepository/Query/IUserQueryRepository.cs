using IDP.Domain.Entities;

namespace IDP.Domain.IRepository.Query
{
    public interface IUserQueryRepository
    {
        Task<User> GetUserAsync(string mobilenumber);    
    }
}
