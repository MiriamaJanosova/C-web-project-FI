using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly IList<Action> afterCommitActions = new List<Action>();
        public DbContext Context { get; }

        public EntityFrameworkUnitOfWork(Func<DbContext> dbContextFactory)
        {
            this.Context = dbContextFactory?.Invoke() ?? throw new ArgumentException("Db context factory cant be null!");
        }

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
            foreach (var action in afterCommitActions)
            {
                action();
            }
            afterCommitActions.Clear();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void RegisterAction(Action action)
        {
            afterCommitActions.Add(action);                                 
        }
    }
}
