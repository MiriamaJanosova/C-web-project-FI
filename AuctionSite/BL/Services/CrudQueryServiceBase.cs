using AutoMapper;
using BL.DTOs.Common;
using DAL.Entities;
using DAL.Repository;
using Infrastructure.Query;

namespace BL.Services
{
    public abstract class CrudQueryServiceBase<TEntity, TDto, TFilterDto> : ServiceBase
        where TFilterDto : FilterDtoBase, new()
        where TEntity : class, IEntity, new()
        where TDto : DtoBase
    
    {
        protected readonly IRepository<TEntity> Repository;

        protected readonly QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>> Query;

        protected CrudQueryServiceBase(IMapper mapper, IRepository<TEntity> repository, QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>> query) : base(mapper)
        {
            this.Query = query;
            this.Repository = repository;
        }
    }
}