using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Common.Domain.Interface.Repositories
{
    public interface IRepository<TKey, T>
    {
        void Create(T entity);
        Task CreateAsync(T entity);        
        void Update(T entity);
        void UpdateNoTracked(T entity);
        void Delete(TKey id);
        Task DeleteAsync(TKey id);
        void DetachEntity(T entity);
        T Read(TKey id);
        Task<T> ReadAsync(TKey id);
        Task<IEnumerable<T>> ReadAllAsync();
        IEnumerable<T> ReadAll();        
        Task<int> SaveChangesAsync();
    }
}
