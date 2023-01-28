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
    public class UserRepos : IUserRepository
    {
        ApplicationDbContext _db;
        public UserRepos(ApplicationDbContext db) => _db = db;

        public async Task Create(User entity)
        {
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<User> GetAll() =>
            _db.Users;

        public async Task<User> GetById(int id) =>
            await _db.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task GetByLogin(string login)=>
            await _db.Users.FirstOrDefaultAsync(x => x.Login == login);
        

        public async Task<User> Update(User entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
