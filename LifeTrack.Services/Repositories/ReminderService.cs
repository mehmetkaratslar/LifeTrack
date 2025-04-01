using LifeTrack.Core.Models;
using LifeTrack.Services.Data;
using LifeTrack.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeTrack.Services
{
    public class ReminderService : IRepository<Reminder>
    {
        private readonly LifeTrackDbContext _dbContext;

        public ReminderService(LifeTrackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Asenkron metotlar
        public async Task<bool> AddAsync(Reminder entity)
        {
            try
            {
                await _dbContext.Reminders.AddAsync(entity);
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
                var reminder = await _dbContext.Reminders.FindAsync(id);
                if (reminder == null) return false;
                _dbContext.Reminders.Remove(reminder);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Reminder> GetByIdAsync(int id)
        {
            return await _dbContext.Reminders.FindAsync(id);
        }

        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await _dbContext.Reminders.ToListAsync();
        }

        public async Task<IEnumerable<Reminder>> GetActiveRemindersAsync()
        {
            return await _dbContext.Reminders
                .Where(r => !r.IsCompleted && r.DueDate >= DateTime.Now)
                .OrderBy(r => r.DueDate)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Reminder entity)
        {
            try
            {
                _dbContext.Reminders.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Senkron metotlar
        public void Add(Reminder entity)
        {
            _dbContext.Reminders.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var reminder = _dbContext.Reminders.Find(id);
            if (reminder != null)
            {
                _dbContext.Reminders.Remove(reminder);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Reminder> GetAll()
        {
            return _dbContext.Reminders.ToList();
        }

        public Reminder GetById(int id)
        {
            return _dbContext.Reminders.Find(id);
        }

        public IEnumerable<Reminder> GetActiveReminders()
        {
            return _dbContext.Reminders
                .Where(r => !r.IsCompleted && r.DueDate >= DateTime.Now)
                .OrderBy(r => r.DueDate)
                .ToList();
        }

        public void Update(Reminder entity)
        {
            _dbContext.Reminders.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}