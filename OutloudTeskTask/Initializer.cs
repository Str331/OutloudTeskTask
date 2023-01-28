using Outloud.Dal.ReposInterfaces;
using Outloud.Dal.Repository;
using Outloud.Domain.Entity;
using Outloud.Service.ServiceInterfaces;

namespace OutloudTeskTask
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, UserRepos>();
            services.AddScoped<IBaseRepository<Feed>, FeedRepos>();
            services.AddScoped<IBaseRepository<News>, NewsRepos>();
            services.AddScoped<IBaseRepository<Sub>, SubRepos>();
        }
    }
}
