using Microsoft.EntityFrameworkCore;
using Outloud.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Dal
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) :
            base(contextOptions) => Database.EnsureCreated();

        public DbSet<News> News { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Sub> Subs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);

                builder.HasData(new User
                {
                    Id = 1,
                    Login = "StR",
                    Password =("63obutup"),
                });
                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Login).HasMaxLength(20).IsRequired();
            });
            modelBuilder.Entity<News>()
                .HasOne(x => x.Feed)
                .WithMany(y => y.News)
                .HasForeignKey(j => j.FeedId);
        }
    }
}
