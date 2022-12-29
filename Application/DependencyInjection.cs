using Microsoft.Extensions.DependencyInjection;
using PersEmails.Application.Emails.Commands;
using PersEmails.Application.Persons.Commands;

namespace PersEmails.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<AddEmailToPersonCommand>();
            services.AddScoped<AddPersonCommand>();
            services.AddScoped<SavePersonCommand>();

            return services;
        }
    }
}
