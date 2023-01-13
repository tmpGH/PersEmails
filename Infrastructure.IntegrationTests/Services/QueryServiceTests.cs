using Infrastructure.IntegrationTests.FakeObjects;
using Moq;
using PersEmails.Application.Interfaces;
using PersEmails.Infrastructure.Services;

namespace Infrastructure.IntegrationTests.Services
{
    internal class QueryServiceTests
    {
        private static readonly object[] _sourceObjects = { null, new object() };

        [TestCaseSource("_sourceObjects")]
        public void Execute_QueryReturnsResult_ReturnsSameResult(object myObject)
        {
            var mockQuery = new Mock<IQuery<object>>();
            mockQuery.Setup(q => q.Execute(It.IsAny<IAppContext>()))
                .Returns(myObject);
            var service = new QueryService(new FakeServiceProvider());

            //Act
            var result = service.Execute(mockQuery.Object);

            //Assert
            Assert.That(result, Is.SameAs(myObject));
        }

        [TestCaseSource("_sourceObjects")]
        public async Task ExecuteAsync_QueryReturnsResult_ReturnsSameResult(object myObject)
        {
            var mockQuery = new Mock<IQueryAsync<object>>();
            mockQuery.Setup(q => q.ExecuteAsync(It.IsAny<IAppContext>(), It.IsAny<CancellationToken>()))
                .Returns(Task.Run(() => myObject));
            var service = new QueryService(new FakeServiceProvider());

            //Act
            var result = await service.ExecuteAsync(mockQuery.Object, CancellationToken.None);

            //Assert
            Assert.That(result, Is.SameAs(myObject));
        }
    }
}
