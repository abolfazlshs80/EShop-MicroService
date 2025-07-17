using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Domain.IRepository.Base
{
    public interface IBaseCommonRepository<in T> where T : class
    {
        Task<bool>Create(T entity);
        Task<bool>Update(T entity);
        Task<bool>Delete(T entity);
    }
}
