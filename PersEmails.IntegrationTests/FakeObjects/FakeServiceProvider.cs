using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PersEmails.IntegrationTests.FakeObjects
{
    internal class FakeServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public FakeServiceProvider()
        {
            _services.Add(typeof(ITempDataDictionaryFactory), new FakeTempDataDictionaryFactory());
            _services.Add(typeof(IUrlHelperFactory), new FakeUrlHelperFactory());
        }

        public object? GetService(Type serviceType)
        {
            return _services.ContainsKey(serviceType) ? _services[serviceType] : null;
        }

        public void AddService(Type serviceType, object service) => _services.Add(serviceType, service);
    }
}
