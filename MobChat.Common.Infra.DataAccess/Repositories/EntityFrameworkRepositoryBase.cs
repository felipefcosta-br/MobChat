using Microsoft.EntityFrameworkCore;
using MobChat.Common.Domain.Entities;
using MobChat.Common.Domain.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Common.Infra.DataAccess.Repositories
{
    public class EntityFrameworkRepositoryBase<TKey, T> : IRepository<TKey, T> where T : TEntity<TKey>
    {
        protected DbContext dbContext;

        public EntityFrameworkRepositoryBase()
        {

        }

        public EntityFrameworkRepositoryBase(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);

        }

        public void Delete(TKey id)
        {
            dbContext.Set<T>().Remove(Read(id));
        }

        public async Task DeleteAsync(TKey id)
        {
            dbContext.Set<T>().Remove(await ReadAsync(id));
        }
        public void DetachEntity(T entity)
        {
            dbContext.Entry<T>(entity).State = EntityState.Detached;
        }
        public T Read(TKey id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> ReadAll()
        {
            return dbContext.Set<T>().AsNoTracking();
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> ReadAsync(TKey id)
        {
            T entity = await dbContext.Set<T>().FindAsync(id);
            dbContext.Entry<T>(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<int> SaveChangesAsync()
        {
            int result =  await dbContext.SaveChangesAsync();
            
            return result;
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }

        public void UpdateNoTracked(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }

    }
}