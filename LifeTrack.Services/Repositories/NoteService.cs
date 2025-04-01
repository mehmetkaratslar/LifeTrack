using LifeTrack.Core;
using LifeTrack.Services.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LifeTrack.Core.Models;

namespace LifeTrack.Services
{
    public class NoteService : IRepository<Note>
    {
        private readonly LifeTrackDbContext _dbContext;

        public NoteService(LifeTrackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
    }
}