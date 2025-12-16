using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<TEnity, TKey> : ISpecifications<TEnity, TKey> where TEnity : BaseEntity<TKey>
    {
        public Expression<Func<TEnity, bool>> Criteria { get; set; } = null; // P=>P.id==1
        public List<Expression<Func<TEnity, object>>> Includes { get; set; } = new List<Expression<Func<TEnity, object>>>(); // P=>P.brand 
        public Expression<Func<TEnity, object>> OrderBy { get; set; }
        public Expression<Func<TEnity, object>> OrderByDescending { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagingEnabled { get; set; }

        public BaseSpecifications(Expression<Func<TEnity, bool>> expression)
        {
            Criteria = expression;
        }
        public BaseSpecifications()
        {

        }
        public void AddOrderBy(Expression<Func<TEnity, object>> expression)
        {
            OrderBy = expression;
        }
        public void AddOrderByDesc(Expression<Func<TEnity, object>> expression)
        {
            OrderByDescending = expression;
        }
        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
