using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PersEmails.UnitTests.FakeObjects
{
    internal class FakeServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public FakeServiceProvider()
        {
            services.Add(typeof(ITempDataDictionaryFactory), new FakeTempDataDictionaryFactory());
            services.Add(typeof(IUrlHelperFactory), new FakeUrlHelperFactory());
        }
        public object? GetService(Type serviceType)
        {
            if (services.ContainsKey(serviceType))
                return services[serviceType];

            return null;
        }

        public void AddService(Type serviceType, object service)
        {
            services.Add(serviceType, service);
        }
    }
}
