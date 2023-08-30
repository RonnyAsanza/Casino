using Casino.Domain.Interfaces.Repositories;
using Casino.Infrastructure.Persistence;
using Casino.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Casino.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CasinoDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("CasinoConn"),
                        b => b.MigrationsAssembly(typeof(CasinoDbContext).Assembly.FullName)), ServiceLifetime.Transient);

            #region Repositories
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserRepository, UserRepository>();

            #endregion

            return services;
        }
    }
}
