using LifeTrack.Core.Models;
using LifeTrack.Services.Data;
using LifeTrack.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeTrack.Services
{
    public class NoteService : IRepository<Note>
    {
        private readonly LifeTrackDbContext _dbContext;

        public NoteService(LifeTrackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Asenkron metotlar
        public async Task<bool> AddAsync(Note entity)
        {
            try
            {
                await _dbContext.Notes.AddAsync(entity);
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
                var note = await _dbContext.Notes.FindAsync(id);
                if (note == null) return false;
                _dbContext.Notes.Remove(note);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            return await _dbContext.Notes.FindAsync(id);
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            return await _dbContext.Notes.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Note entity)
        {
            try
            {
                _dbContext.Notes.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Senkron metotlar
        public void Add(Note entity)
        {
            _dbContext.Notes.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var note = _dbContext.Notes.Find(id);
            if (note != null)
            {
                _dbContext.Notes.Remove(note);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Note> GetAll()
        {
            return _dbContext.Notes.ToList();
        }

        public Note GetById(int id)
        {
            return _dbContext.Notes.Find(id);
        }

        public void Update(Note entity)
        {
            _dbContext.Notes.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}