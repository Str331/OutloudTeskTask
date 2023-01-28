using Microsoft.EntityFrameworkCore;
using Outloud.Dal.ReposInterfaces;
using Outloud.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Dal.Repository
{
    public class SubRepos : ISubRepository
    {
        ApplicationDbContext _db;
        public SubRepos(ApplicationDbContext db) => _db = db;

        public async Task Create(Sub entity)
        {
            await _db.Subs.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Sub entity)
        {
             _db.Subs.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Sub> GetAll() => _db.Subs;

        public async Task<Sub> GetById(int id) =>
            await _db.Subs.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> isExist(string userLogin, int feedId)
        {
            if (await _db.Subs.FirstOrDefaultAsync(x => x.UserLogin == userLogin && x.FeedId == feedId) != null)
                return true;
            else
                return false;
        }

        public async Task<Sub> Update(Sub entity)
        {
            _db.Subs.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
