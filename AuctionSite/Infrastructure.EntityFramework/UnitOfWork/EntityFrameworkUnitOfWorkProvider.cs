using System;
using System.Data.Entity;
using System.Threading;
using Infrastructure.UnitOfWork;

namespace Infrastructure.EntityFramework.UnitOfWork
{
    public class EntityFrameworkUnitOfWorkProvider : IUnitOfWorkProvider
    {

        protected readonly AsyncLocal<IUnitOfWork> UowLocalInstance
            = new AsyncLocal<IUnitOfWork>();

        private readonly Func<DbContext> dbContextFactory;

        public EntityFrameworkUnitOfWorkProvider(Func<DbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public IUnitOfWork Create()
        {
            UowLocalInstance.Value = new EntityFrameworkUnitOfWork(dbContextFactory);
            return UowLocalInstance.Value;
        }

        public void Dispose()
        {
            UowLocalInstance.Value?.Dispose();
            UowLocalInstance.Value = null;
        }

        public IUnitOfWork GetUnitOfWorkInstance()
        {
            return UowLocalInstance.Value != null ? UowLocalInstance.Value : throw new InvalidOperationException("UoW is null, thus cannot be returned instance.");
        }
    }
}
