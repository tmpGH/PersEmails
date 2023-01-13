using Microsoft.Extensions.Logging;
using Moq;
using PersEmails.Application.Emails.Commands;
using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;

namespace Application.IntegrationTests.Emails.Commands
{
    internal class AddEmailToPersonCommandValidatorTests
    {
        private ILogger<AddEmailToPersonCommandValidator> _logger;
        private Mock<IAppContext> _context;

        [SetUp]
        public void Setup()
        {
            var mockLogger = new Mock<ILogger<AddEmailToPersonCommandValidator>>();
            _logger = mockLogger.Object;
            _context = new Mock<IAppContext>();
        }

        [Test]
        public void IsValid_EmailAddressIsNull_ReturnsFalse()
        {
            var command = new AddEmailToPersonCommand();
            var validator = new AddEmailToPersonCommandValidator(_context.Object, _logger);

            //Act
            var result = validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValid_EmailAddressHasWrongFormat_ReturnsFalse()
        {
            var command = new AddEmailToPersonCommand { EmailAddress = "@email"};
            var validator = new AddEmailToPersonCommandValidator(_context.Object, _logger);

            //Act
            var result = validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValid_PersonDoesNotExist_ReturnsFalse()
        {
            _context.Setup(ctx => ctx.Persons.Find(It.IsAny<int>()))
                .Returns((Person)null);
            var command = new AddEmailToPersonCommand { EmailAddress = "e@mai.l" };
            var validator = new AddEmailToPersonCommandValidator(_context.Object, _logger);

            //Act
            var result = validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValid_GetsRightData_ReturnsTrue()
        {
            _context.Setup(ctx => ctx.Persons.Find(It.IsAny<int>()))
                .Returns(new Person());
            var command = new AddEmailToPersonCommand { EmailAddress = "e@mai.l" };
            var validator = new AddEmailToPersonCommandValidator(_context.Object, _logger);

            //Act
            var result = validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(true));
        }
    }
}
