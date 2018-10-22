using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Query.Predicates;

namespace Infrastructure.Query
{
    public interface IQuery<TEntity> where TEntity : class, IEntity, new()
    {
        IQuery<TEntity> Where(IPredicate root);

        IQuery<TEntity> SortBy(string sortAccordingTo, bool ascendingOrder = true);

        IQuery<TEntity> Page(int pageToFetch, int pageSize = 10);

        Task<QueryResult<TEntity>> ExecuteAsync();
    }
}
