using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contarct
{
    public interface IGenaricRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>>  GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsyncSpec(ISpecifications<TEntity,TKey> spec);
        Task<TEntity?> GetAsyncSpec(ISpecifications<TEntity, TKey> spec);
        Task<TEntity?> GetAsync(TKey id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
      
    }
}
