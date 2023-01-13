using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using PersEmails.Application.Emails.Commands;
using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;

namespace Application.IntegrationTests.Emails.Commands
{
    internal class AddEmailToPersonCommandTests
    {
        [Test]
        public void Execute_WhenCalled_ReturnsZero()
        {
            var mockContext = new Mock<IAppContext>();
            mockContext.Setup(ctx => ctx.Emails.Add(It.IsAny<Email>()))
                .Returns((EntityEntry<Email>)null);
            mockContext.Setup(ctx => ctx.SaveChanges()).Returns(0);
            var command = new AddEmailToPersonCommand();

            //Act
            var result = command.Execute(mockContext.Object);

            //Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Execute_WhenCalled_ReturnsOne()
        {
            var mockContext = new Mock<IAppContext>();
            mockContext.Setup(ctx => ctx.Emails.Add(It.IsAny<Email>()))
                .Returns((EntityEntry<Email>)null);
            mockContext.Setup(ctx => ctx.SaveChanges()).Returns(1);
            var command = new AddEmailToPersonCommand();

            //Act
            var result = command.Execute(mockContext.Object);

            //Assert
            Assert.That(result, Is.EqualTo(1));
        }
    }
}
