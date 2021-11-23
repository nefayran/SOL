using System;
using DigestCreator.DigestCreator.Core.Entity;

namespace DigestCreator.DigestCreator.Core.Interfaces
{
    public interface IRepository<in T> : IDisposable where T : IEntity
    {
        int SaveChanges();
        void Add(T entity);
        void Update(T entity);
    }
}