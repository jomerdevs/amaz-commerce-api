using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> Complete();
    }
}
