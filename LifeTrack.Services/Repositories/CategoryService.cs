using LifeTrack.Core.Models;
using LifeTrack.Services.Data;
using LifeTrack.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeTrack.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly LifeTrackDbContext _dbContext;

        public CategoryService(LifeTrackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(Category entity)
        {
            try
            {
                await _dbContext.Categories.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var category = await _dbContext.Categories.FindAsync(id);
                if (category == null) return false;
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Category entity)
        {
            try
            {
                _dbContext.Categories.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Senkron metotları da ekleyelim, böylece geçiş sürecinde eski kodları da destekler
        public IEnumerable<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _dbContext.Categories.Find(id);
        }

        public void Add(Category entity)
        {
            _dbContext.Categories.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Category entity)
        {
            _dbContext.Categories.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
            }
        }
    }
}