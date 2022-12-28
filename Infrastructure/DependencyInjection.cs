using PersEmails.Infrastructure.Interfaces;
using PersEmails.Infrastructure.Persistance;
using PersEmails.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using PersEmails.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PersEmails.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PersEmailsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PersEmailsDatabase")));
            services.AddScoped<IAppContext>(provider => provider.GetService<PersEmailsContext>());

            services.AddScoped(typeof(IQueryService), typeof(QueryService));
            services.AddScoped(typeof(ICommandService), typeof(CommandService));

            return services;
        }
    }
}
