using Moq;
using PersEmails.Application.Interfaces;
using PersEmails.Infrastructure.Interfaces;

namespace PersEmails.UnitTests.FakeObjects
{
    internal class FakeHelpers
    {
        public static void MockICommandServiceWithWrongResult(FakeServiceProvider serviceProvider)
        {
            var mockService = new Mock<ICommandService>();
            mockService.Setup(service => service.Execute(It.IsAny<ICommand>()))
                .Returns(0);
            mockService.Setup(service => service.ExecuteAsync(It.IsAny<ICommandAsync>(), It.IsAny<CancellationToken>()))
                .Returns(Task<int>.Run(() => 0));
            serviceProvider.AddService(typeof(ICommandService), mockService.Object);
        }

        public static void MockICommandServiceWithRightResult(FakeServiceProvider serviceProvider)
        {
            var mockService = new Mock<ICommandService>();
            mockService.Setup(service => service.Execute(It.IsAny<ICommand>()))
                .Returns(1);
            mockService.Setup(service => service.ExecuteAsync(It.IsAny<ICommandAsync>(), It.IsAny<CancellationToken>()))
                .Returns(Task<int>.Run(() => 1));
            serviceProvider.AddService(typeof(ICommandService), mockService.Object);
        }

    }
}
