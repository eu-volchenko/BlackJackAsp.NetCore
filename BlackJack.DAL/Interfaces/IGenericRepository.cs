using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DAL.Entities;

namespace BlackJack.DAL.Interfaces
{
    public interface IGenericRepository<TEntity>:IDisposable where TEntity:BaseEntity
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);
        void Create(TEntity item);
        Task CreateAsync(TEntity item);
        void Remove(TEntity item);
        Task RemoveAsync(TEntity item);
        void Update(TEntity item);
        Task UpdateAsync(TEntity item);
    }
}
