using Microsoft.Extensions.DependencyInjection;
using PersEmails.Application.Emails.Commands;
using PersEmails.Application.Persons.Commands;

namespace PersEmails.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            AddCommands(services);
            AddValidators(services);

            return services;
        }

        private static void AddCommands(IServiceCollection services)
        {
            services.AddScoped<AddEmailToPersonCommand>();
            services.AddScoped<AddPersonCommand>();
            services.AddScoped<SavePersonCommand>();
        }

        private static void AddValidators(IServiceCollection services)
        {
            services.AddScoped<AddEmailToPersonCommandValidator>();
            services.AddScoped<AddPersonCommandValidator>();
            services.AddScoped<SavePersonCommandValidator>();
        }
    }
}
