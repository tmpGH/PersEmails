using Application.UnitTests.FakeObjects;
using PersEmails.Application.Emails.Queries;
using PersEmails.Application.Interfaces;

namespace Application.UnitTests.Emails.Queries
{
    internal class GetEmailsQueryTests
    {
        [Test]
        public void Execute_WhenCalled_ReturnsListOfEmails()
        {
            IAppContext context = FakeHelpers.GetAppContext();
            var query = new GetEmailsQuery { PageSize = 10 };

            //Act
            var result = query.Execute(context);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<IList<EmailWithNamesDto>>());
            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}
