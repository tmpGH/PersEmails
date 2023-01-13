using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PersEmails.IntegrationTests.FakeObjects
{
    internal class FakeTempDataDictionaryFactory : ITempDataDictionaryFactory
    {
        public ITempDataDictionary GetTempData(HttpContext context)
        {
            return new FakeTempDataDictionary();
        }
    }
}
