using LifeTrack.Core;
using LifeTrack.Services.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LifeTrack.Core.Models;

namespace LifeTrack.Services
{
    public class CategoryService : IRepository<Category>
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
    }
}