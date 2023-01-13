using Application.IntegrationTests.FakeObjects;
using PersEmails.Application.Interfaces;
using PersEmails.Application.Persons.Queries;

namespace Application.IntegrationTests.Persons.Queries
{
    internal class GetPersonsQueryTests
    {
        [Test]
        [Ignore("inner EF query imposible to test")]
        public async Task Execute_WhenCalled_ReturnsListOfPersonsWithEmails()
        {
            IAppContext context = FakeHelpers.GetAppContext();
            var query = new GetPersonsQuery { PageSize = 10 };

            //Act
            var result = await query.ExecuteAsync(context, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<IList<PersonWithEmailAddressDto>>());
            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}
