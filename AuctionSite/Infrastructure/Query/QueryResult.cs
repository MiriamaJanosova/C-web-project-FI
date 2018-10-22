using System.Collections.Generic;
using System.Linq;
using DAL.Entities;

namespace Infrastructure.Query
{
    public class QueryResult<TEntity> where TEntity : IEntity
    {
        public QueryResult(IList<TEntity> items, long totalItemsCount, int pageSize = 10, int? requestedPageNumber = null)
        {
            TotalItemsCount = totalItemsCount;
            RequestedPageNumber = requestedPageNumber;
            PageSize = pageSize;
            Items = items;
        }
        
        public long TotalItemsCount { get; }
        public int? RequestedPageNumber { get; }
        public int PageSize { get; }
        public IList<TEntity> Items { get; }
        public bool PagingEnabled => RequestedPageNumber != null;

        protected bool Equals(QueryResult<TEntity> other)
        {
            return TotalItemsCount == other.TotalItemsCount &&
                   RequestedPageNumber == other.RequestedPageNumber &&
                   PageSize == other.PageSize &&
                   Items.All(entity => other.Items.Select(item => item.ID).Contains(entity.ID));
            
        }
    }
}