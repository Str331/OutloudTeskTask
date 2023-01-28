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
    public class FeedRepos : IFeedRepository
    {
        private readonly ApplicationDbContext _db;
        public FeedRepos(ApplicationDbContext db) => _db = db;

        public async Task Create(Feed entity)
        {
            await _db.Feeds.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Feed entity)
        {
            _db.Feeds.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Feed> GetAll() =>
            _db.Feeds;
        

        public Task<Feed> GetById(int id)=>
            _db.Feeds.Include(x => x.News).FirstOrDefaultAsync(x => x.Id == id);
        

        public Task<Feed> GetByURLadress(string urlAdress) =>
            _db.Feeds.Include(x => x.News).Include(x => x.Subs).FirstOrDefaultAsync(x => x.URLadress == urlAdress);

        public async Task<Feed> Update(Feed entity)
        {
            _db.Feeds.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
