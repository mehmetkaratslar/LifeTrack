using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeTrack.Services.Repositories
{
    public interface IRepository<T>
    {
        // Asenkron metotlar
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);

        // Senkron metotlar
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}