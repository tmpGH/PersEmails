using Microsoft.EntityFrameworkCore;
using Moq;
using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;

namespace Application.UnitTests.FakeObjects
{
    internal class FakeHelpers
    {
        public static IAppContext GetAppContext()
        {
            ICollection<Email> _emails = new List<Email> { new Email
            {
                EmailAddress = "e@mai.l",
                PersonId = 10
            }};
            var mockEmailsSet = new Mock<DbSet<Email>>();
            mockEmailsSet.As<IQueryable<Email>>().Setup(m => m.Provider)
                .Returns(_emails.AsQueryable().Provider);
            mockEmailsSet.As<IQueryable<Email>>().Setup(m => m.Expression)
                .Returns(_emails.AsQueryable().Expression);
            mockEmailsSet.As<IQueryable<Email>>().Setup(m => m.ElementType)
                .Returns(_emails.AsQueryable().ElementType);
            mockEmailsSet.As<IQueryable<Email>>().Setup(m => m.GetEnumerator())
                .Returns(_emails.AsQueryable().GetEnumerator());

            ICollection<Person> _persons = new List<Person> { new Person
            {
                Id = 10,
                Name = "Test",
                Surname = "Stest"
            }};
            var mockPersonsSet = new Mock<DbSet<Person>>();
            mockPersonsSet.As<IQueryable<Person>>().Setup(m => m.Provider)
                .Returns(_persons.AsQueryable().Provider);
            mockPersonsSet.As<IQueryable<Person>>().Setup(m => m.Expression)
                .Returns(_persons.AsQueryable().Expression);
            mockPersonsSet.As<IQueryable<Person>>().Setup(m => m.ElementType)
                .Returns(_persons.AsQueryable().ElementType);
            mockPersonsSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator())
                .Returns(_persons.AsQueryable().GetEnumerator());

            var mockContext = new Mock<IAppContext>();
            mockContext.Setup(ctx => ctx.Emails)
                .Returns(mockEmailsSet.Object);
            mockContext.Setup(ctx => ctx.Persons)
                .Returns(mockPersonsSet.Object);

            return mockContext.Object;
        }
    }
}
