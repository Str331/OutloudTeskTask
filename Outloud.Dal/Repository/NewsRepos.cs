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
    public class NewsRepos : INewsRepository
    {
        private readonly ApplicationDbContext _db;
        public NewsRepos(ApplicationDbContext db) => _db = db;

        public async Task Create(News entity)
        {
            await _db.News.AddAsync(entity);
            _db.SaveChangesAsync();
        }

        public async Task Delete(News entity)
        {
            _db.News.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<News> GetAll() => _db.News;

        public async Task<News> GetById(int id) =>
            await _db.News.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<News>> GetUnreadNews(DateTime startDate) =>
            await _db.News.Include(x => x.Feed).Where(x => x.DateofAdd.Day == startDate.Day && x.IsRead == false).ToListAsync();

        public async Task NewsAsRead(int id)
        {
                var news = await _db.News.FindAsync(id);
            if (news != null)
            {
                news.IsRead = true;
                await _db.SaveChangesAsync();
            }
            else
                news.IsRead = false;
            await _db.SaveChangesAsync();
        }

        public async Task<News> Update(News entity)
        {
            _db.News.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
