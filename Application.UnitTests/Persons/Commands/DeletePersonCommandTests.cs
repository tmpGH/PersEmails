using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using PersEmails.Application.Interfaces;
using PersEmails.Application.Persons.Commands;
using PersEmails.Domain.Entities;

namespace Application.UnitTests.Persons.Commands
{
    internal class DeletePersonCommandTests
    {
        [Test]
        public async Task ExecuteAsync_WhenCalled_ReturnsZero()
        {
            var mockContext = new Mock<IAppContext>();
            mockContext.Setup(ctx => ctx.Persons.FindAsync(It.IsAny<int>()))
                .Returns(new ValueTask<Person>(Task<Person>.Run(() => (Person)null)));
            var command = new DeletePersonCommand();

            //Act
            var result = await command.ExecuteAsync(mockContext.Object, CancellationToken.None);

            //Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task ExecuteAsync_WhenCalled_ReturnsOne()
        {
            var mockContext = new Mock<IAppContext>();
            mockContext.Setup(ctx => ctx.Persons.FindAsync(It.IsAny<int>()))
                .Returns(new ValueTask<Person>(Task<Person>.Run(() => new Person())));
            mockContext.Setup(ctx => ctx.Persons.Remove(It.IsAny<Person>())).Returns((EntityEntry<Person>)null);
            mockContext.Setup(ctx => ctx.SaveChanges()).Returns(1);
            var command = new DeletePersonCommand();

            //Act
            var result = await command.ExecuteAsync(mockContext.Object, CancellationToken.None);

            //Assert
            Assert.That(result, Is.EqualTo(1));
        }
    }
}
