using LifeTrack.Core.Models;
using LifeTrack.Services.Data;
using LifeTrack.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeTrack.Services
{
    public class ExpenseService : IRepository<Expense>
    {
        private readonly LifeTrackDbContext _dbContext;

        public ExpenseService(LifeTrackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Asenkron metotlar
        public async Task<bool> AddAsync(Expense entity)
        {
            try
            {
                await _dbContext.Expenses.AddAsync(entity);
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
                var expense = await _dbContext.Expenses.FindAsync(id);
                if (expense == null) return false;
                _dbContext.Expenses.Remove(expense);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Expense> GetByIdAsync(int id)
        {
            return await _dbContext.Expenses
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _dbContext.Expenses
                .Include(e => e.Category)
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Expense entity)
        {
            try
            {
                _dbContext.Expenses.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Senkron metotlar
        public void Add(Expense entity)
        {
            _dbContext.Expenses.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var expense = _dbContext.Expenses.Find(id);
            if (expense != null)
            {
                _dbContext.Expenses.Remove(expense);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Expense> GetAll()
        {
            return _dbContext.Expenses
                .Include(e => e.Category)
                .OrderByDescending(e => e.Date)
                .ToList();
        }

        public Expense GetById(int id)
        {
            return _dbContext.Expenses
                .Include(e => e.Category)
                .FirstOrDefault(e => e.Id == id);
        }

        public void Update(Expense entity)
        {
            _dbContext.Expenses.Update(entity);
            _dbContext.SaveChanges();
        }

        // Ek yardımcı metotlar
        public IEnumerable<Expense> GetByDateRange(System.DateTime startDate, System.DateTime endDate)
        {
            return _dbContext.Expenses
                .Include(e => e.Category)
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .OrderByDescending(e => e.Date)
                .ToList();
        }

        public IEnumerable<Expense> GetByCategory(int categoryId)
        {
            return _dbContext.Expenses
                .Include(e => e.Category)
                .Where(e => e.CategoryId == categoryId)
                .OrderByDescending(e => e.Date)
                .ToList();
        }

        public decimal GetTotalByCategory(int categoryId)
        {
            return _dbContext.Expenses
                .Where(e => e.CategoryId == categoryId)
                .Sum(e => e.Amount);
        }

        public decimal GetTotalByDateRange(System.DateTime startDate, System.DateTime endDate)
        {
            return _dbContext.Expenses
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .Sum(e => e.Amount);
        }
    }
}