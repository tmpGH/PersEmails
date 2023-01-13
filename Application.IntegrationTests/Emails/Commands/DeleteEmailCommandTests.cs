using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using PersEmails.Application.Emails.Commands;
using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;

namespace Application.IntegrationTests.Emails.Commands
{
    internal class DeleteEmailCommandTests
    {
        [Test]
        public void Execute_WhenCalled_ReturnsZero()
        {
            var mockContext = new Mock<IAppContext>();
            mockContext.Setup(ctx => ctx.Emails.Find(It.IsAny<int>()))
                .Returns((Email)null);
            var command = new DeleteEmailCommand();

            //Act
            var result = command.Execute(mockContext.Object);

            //Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Execute_WhenCalled_ReturnsOne()
        {
            var mockContext = new Mock<IAppContext>();
            mockContext.Setup(ctx => ctx.Emails.Find(It.IsAny<int>()))
                .Returns(new Email());
            mockContext.Setup(ctx => ctx.Emails.Remove(It.IsAny<Email>()))
                .Returns((EntityEntry<Email>)null);
            mockContext.Setup(ctx => ctx.SaveChanges()).Returns(1);
            var command = new DeleteEmailCommand();

            //Act
            var result = command.Execute(mockContext.Object);

            //Assert
            Assert.That(result, Is.EqualTo(1));
        }
    }
}
