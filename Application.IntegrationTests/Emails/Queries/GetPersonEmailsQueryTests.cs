using Application.IntegrationTests.FakeObjects;
using PersEmails.Application.Emails.Queries;
using PersEmails.Application.Interfaces;

namespace Application.IntegrationTests.Emails.Queries
{
    internal class GetPersonEmailsQueryTests
    {
        [Test]
        [Ignore("inner EF query imposible to test")]
        public async Task Execute_WhenCalled_ReturnsListOfEmails()
        {

            IAppContext context = FakeHelpers.GetAppContext();
            var query = new GetPersonEmailsQuery { Id = 10 };

            //Act
            var result = await query.ExecuteAsync(context, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<IList<EmailWithNamesDto>>());
            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}
