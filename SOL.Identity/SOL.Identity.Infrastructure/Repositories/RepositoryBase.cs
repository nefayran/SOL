using System;
using SOL.Core.Entity;
using SOL.Core.Interfaces;
using SOL.Identity.SOL.Identity.Infrastructure.Data.Context;

namespace SOL.Identity.SOL.Identity.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : IEntity
    {
        protected readonly IdentityContext _identityContext;

        public RepositoryBase(IdentityContext sampleLibraryContext)
        {
            _identityContext = sampleLibraryContext;
        }

        public int SaveChanges() => _identityContext.SaveChangesAsync().Result;

        public void Add(T entity) => _identityContext.Add(entity);
        public void Update(T entity) => _identityContext.Update(entity);

        public void Dispose()
        {
            _identityContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}