using Infrastructure.UnitTests.FakeObjects;
using Moq;
using PersEmails.Application.Interfaces;
using PersEmails.Infrastructure.Services;

namespace Infrastructure.UnitTests.Services
{
    internal class CommandServiceTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void Execute_CommandReturnsNumber_ReturnsSameNumber(int number)
        {
            var mockCommand = new Mock<ICommand>();
            mockCommand.Setup(c => c.Execute(It.IsAny<IAppContext>()))
                .Returns(number);
            var service = new CommandService(new FakeServiceProvider());

            //Act
            var result = service.Execute(mockCommand.Object);

            //Assert
            Assert.That(result, Is.EqualTo(number));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public async Task ExecuteAsync_CommandReturnsNumber_ReturnsSameNumber(int number)
        {
            var mockCommand = new Mock<ICommandAsync>();
            mockCommand.Setup(c => c.ExecuteAsync(It.IsAny<IAppContext>(), It.IsAny<CancellationToken>()))
                .Returns(Task.Run(() => number));
            var service = new CommandService(new FakeServiceProvider());

            //Act
            var result = await service.ExecuteAsync(mockCommand.Object, CancellationToken.None);

            //Assert
            Assert.That(result, Is.EqualTo(number));
        }
    }
}
