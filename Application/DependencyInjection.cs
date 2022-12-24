using Microsoft.Extensions.DependencyInjection;

namespace PersEmails.Application
{
    // TODO: usunac jezeli niepotrzebne
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
