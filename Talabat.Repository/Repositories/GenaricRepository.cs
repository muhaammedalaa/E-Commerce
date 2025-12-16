using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contarct;
using Talabat.Core.Specifications;
using Talabat.Repository.Data.Contexts;

namespace Talabat.Repository.Repositories
{
    public class GenaricRepository<TEntity, TKey> : IGenaricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreContext _dbContext;

        public GenaricRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsyncSpec(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySepcifications(spec).ToListAsync();
        }
        public async Task<TEntity?> GetAsyncSpec(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySepcifications(spec).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product))
            return (IEnumerable<TEntity>)  await _dbContext.products.Include(P => P.Brand).Include(P => P.Catgory).ToListAsync();
         return await _dbContext.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity?> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
                return await _dbContext.products.Include(P => P.Brand).Include(P => P.Catgory).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        private IQueryable<TEntity> ApplySepcifications(ISpecifications<TEntity,TKey> spec)
        {
          return  SpecificationsEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec);
        }
       
    }
}
