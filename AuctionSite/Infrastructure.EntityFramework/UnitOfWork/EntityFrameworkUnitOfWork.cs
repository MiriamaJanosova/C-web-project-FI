using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.UnitOfWork;

namespace Infrastructure.EntityFramework.UnitOfWork
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
            var changeInfo = Context.ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Modified)
                .Select (t => new {
                    Original = t.OriginalValues.PropertyNames.ToDictionary (pn => pn, pn => t.OriginalValues[pn]),
                    Current = t.CurrentValues.PropertyNames.ToDictionary (pn => pn, pn => t.CurrentValues[pn]),
                });
            var changeInfo2 = Context.ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Added)
                .Select (t => new {
                    Original = t.OriginalValues.PropertyNames.ToDictionary (pn => pn, pn => t.OriginalValues[pn]),
                    Current = t.CurrentValues.PropertyNames.ToDictionary (pn => pn, pn => t.CurrentValues[pn]),
                });
            
            var changeInfo3 = Context.ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Unchanged)
                .Select (t => new {
                    Original = t.OriginalValues.PropertyNames.ToDictionary (pn => pn, pn => t.OriginalValues[pn]),
                    Current = t.CurrentValues.PropertyNames.ToDictionary (pn => pn, pn => t.CurrentValues[pn]),
                });
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
