namespace Infrastructure.UnitTests.FakeObjects
{
    internal class FakeServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public FakeServiceProvider() { }

        public object? GetService(Type serviceType)
        {
            return _services.ContainsKey(serviceType) ? _services[serviceType] : null;
        }
    }
}
