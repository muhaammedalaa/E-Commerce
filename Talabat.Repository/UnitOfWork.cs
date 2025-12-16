using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contarct;
using Talabat.Repository.Data.Contexts;
using Talabat.Repository.Repositories;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private Hashtable _repository;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repository = new Hashtable();
        }
        public async Task<int> CompleteAsync() =>  await _dbContext.SaveChangesAsync();
        public IGenaricRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var Key = typeof(TEntity).Name;
            if (!_repository.ContainsKey(Key))
            {
                var repository = new GenaricRepository<TEntity, TKey>(_dbContext);
                _repository.Add(Key, repository);
            }
            return _repository[Key] as IGenaricRepository<TEntity, TKey>;

        }
    }
}
