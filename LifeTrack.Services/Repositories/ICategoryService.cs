using LifeTrack.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeTrack.Services.Repositories
{
    public interface ICategoryService
    {
        // Asenkron metotlar
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<bool> AddAsync(Category entity);
        Task<bool> UpdateAsync(Category entity);
        Task<bool> DeleteAsync(int id);

        // Senkron metotlar
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Add(Category entity);
        void Update(Category entity);
        void Delete(int id);
    }
}