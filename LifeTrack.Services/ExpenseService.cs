using LifeTrack.Core;
using LifeTrack.Core.Models;
using LifeTrack.Services.Data;
using LifeTrack.Services.Repositories;
using System;
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

        public async Task<bool> AddAsync(Expense entity)
        {
            try
            {
                await _dbContext.Expenses.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
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
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Expense> GetByIdAsync(int id)
        {
            return await _dbContext.Expenses.FindAsync(id);
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return _dbContext.Expenses.ToList();
        }

        public async Task<IEnumerable<Expense>> GetByDateRangeAsync(DateTime start, DateTime end)
        {
            return _dbContext.Expenses
                .Where(e => e.Date >= start && e.Date <= end)
                .ToList();
        }

        public async Task<IEnumerable<Expense>> GetByCategoryAsync(int categoryId)
        {
            return _dbContext.Expenses
                .Where(e => e.CategoryId == categoryId)
                .ToList();
        }

        public async Task<decimal> GetTotalExpensesByDateRangeAsync(DateTime start, DateTime end)
        {
            return _dbContext.Expenses
                .Where(e => e.Date >= start && e.Date <= end)
                .Sum(e => e.Amount);
        }

        public async Task<bool> UpdateAsync(Expense entity)
        {
            try
            {
                _dbContext.Expenses.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}