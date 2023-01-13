using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace PersEmails.IntegrationTests.FakeObjects
{
    internal class FakeUrlHelperFactory : IUrlHelperFactory
    {
        public IUrlHelper GetUrlHelper(ActionContext context)
        {
            return null;
        }
    }
}
