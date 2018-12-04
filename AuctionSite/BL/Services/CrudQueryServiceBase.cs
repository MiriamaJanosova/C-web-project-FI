using System;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.Common;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure;
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

        public virtual async Task<TDto> GetAsync(int entityId, bool withIncludes = true)
        {
            TEntity entity;
            if (withIncludes)
            {
                entity = await GetWithIncludesAsync(entityId);
            }
            else
            {
                entity = await Repository.GetAsync(entityId);
            }
            return entity != null ? AutoMapper.Mapper.Map<TDto>(entity) : null;
        }

        protected abstract Task<TEntity> GetWithIncludesAsync(int entityId);

        public virtual int Create(TDto entityDto)
        {
            var entity = AutoMapper.Mapper.Map<TEntity>(entityDto);
            Repository.Create(entity);
            return entity.Id;
        }

        public virtual async Task Update(TDto entityDto)
        {
            var entity = await GetWithIncludesAsync(entityDto.Id);
            AutoMapper.Mapper.Map(entityDto, entity);
            Repository.Update(entity);
        }

        public virtual void Delete(int entityId)
        {
            Repository.Delete(entityId);
        }

        public virtual async Task<QueryResultDto<TDto, TFilterDto>> ListAllAsync()
        {
            return await Query.ExecuteQuery(new TFilterDto());
        }
    }
}