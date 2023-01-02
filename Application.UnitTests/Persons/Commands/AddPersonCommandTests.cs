using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using PersEmails.Application.Interfaces;
using PersEmails.Application.Persons.Commands;
using PersEmails.Domain.Entities;

namespace Application.UnitTests.Persons.Commands
{
    internal class AddPersonCommandTests
    {
        [Test]
        public void Execute_WhenCalled_ReturnsZero()
        {
            var mockContext = new Mock<IAppContext>();
            mockContext.Setup(ctx => ctx.Persons.Add(It.IsAny<Person>()))
                .Returns((EntityEntry<Person>)null);
            mockContext.Setup(ctx => ctx.SaveChanges())
                .Returns(0);
            var command = new AddPersonCommand();

            //Act
            var result = command.Execute(mockContext.Object);

            //Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Execute_WhenCalled_ReturnsOne()
        {
            var mockContext = new Mock<IAppContext>();
            mockContext.Setup(ctx => ctx.Persons.Add(It.IsAny<Person>()))
                .Returns((EntityEntry<Person>)null);
            mockContext.Setup(ctx => ctx.SaveChanges())
                .Returns(1);
            var command = new AddPersonCommand();

            //Act
            var result = command.Execute(mockContext.Object);

            //Assert
            Assert.That(result, Is.EqualTo(1));
        }
    }
}
