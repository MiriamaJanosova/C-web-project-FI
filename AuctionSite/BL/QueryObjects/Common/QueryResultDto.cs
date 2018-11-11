using System;
using System.Collections.Generic;
using BL.DTOs.Common;

namespace BL.QueryObjects.Common
{
    public class QueryResultDto<TDto, TFilter> where TFilter : FilterDtoBase
    {
        /// <summary>
        /// Total number of items for the queryŠtok
        /// </summary>
        public long TotalItemsCount { get; set; }

        /// <summary>
        /// Number of page (indexed from 1) which was requested
        /// </summary>
        public int? RequestedPageNumber { get; set; }

        /// <summary>
        /// Size of the page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The queryŠtok results page
        /// </summary>
        public IEnumerable<TDto> Items { get; set; }

        /// <summary>
        /// Applied filter for this queryŠtok
        /// </summary>
        public TFilter Filter { get; set; }

        public override string ToString()
        {
            return $"{TotalItemsCount} {typeof(TDto).Name}(s)" +
                   $"{(RequestedPageNumber != null ? $", page {RequestedPageNumber}/{Math.Ceiling(TotalItemsCount / (double)PageSize)}." : ".")}";
        }
    }
}
