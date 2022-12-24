using PersEmails.Infrastructure.Interfaces;
using PersEmails.Infrastructure.Persistance;
using PersEmails.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using PersEmails.Application.Interfaces;

namespace PersEmails.Infrastructure
{
    public static class DependencyInjection
    {
        

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<PersEmailsContext>();
            services.AddScoped<IAppContext>(provider => provider.GetService<PersEmailsContext>());
            

            services.AddScoped(typeof(IQueryService), typeof(QueryService));
            services.AddScoped(typeof(ICommandService), typeof(CommandService));

            return services;
        }

    }
}
