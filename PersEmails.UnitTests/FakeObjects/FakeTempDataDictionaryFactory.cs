using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PersEmails.UnitTests.FakeObjects
{
    internal class FakeTempDataDictionaryFactory : ITempDataDictionaryFactory
    {
        public ITempDataDictionary GetTempData(HttpContext context)
        {
            return new FakeTempDataDictionary();
        }
    }
}
