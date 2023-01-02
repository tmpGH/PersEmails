using Application.UnitTests.FakeObjects;
using PersEmails.Application.Interfaces;
using PersEmails.Application.Persons;
using PersEmails.Application.Persons.Queries;

namespace Application.UnitTests.Persons.Queries
{
    internal class GetPersonQueryTests
    {
        [Test]
        public void Execute_WhenCalled_ReturnsListOfPersons()
        {
            IAppContext context = FakeHelpers.GetAppContext();
            var query = new GetPersonQuery { Id = 10 };

            //Act
            var result = query.Execute(context);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<PersonDto>());
            Assert.That(result.Id, Is.EqualTo(10));
        }
    }
}
