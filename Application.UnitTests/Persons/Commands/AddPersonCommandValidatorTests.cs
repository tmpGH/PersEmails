using Microsoft.Extensions.Logging;
using Moq;
using PersEmails.Application.Persons.Commands;

namespace Application.UnitTests.Persons.Commands
{
    internal class AddPersonCommandValidatorTests
    {
        private ILogger<AddPersonCommandValidator> _logger;

        [SetUp]
        public void Setup()
        {
            var mockLogger = new Mock<ILogger<AddPersonCommandValidator>>();
            _logger = mockLogger.Object;
        }

        [Test]
        public void IsValid_NameIsNull_ReturnsFalse()
        {
            var command = new AddPersonCommand { Surname = "Test" };
            var validator = new AddPersonCommandValidator(_logger);

            //Act
            var result = validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValid_SurnameIsNull_ReturnsFalse()
        {
            var command = new AddPersonCommand { Name = "Test" };
            var validator = new AddPersonCommandValidator(_logger);

            //Act
            var result = validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValid_NameIsTooLong_ReturnsFalse()
        {
            var command = new AddPersonCommand
            {
                Name = "01245567890124556789012455678901245567890124556789_",
                Surname = "Stest"
            };
            var validator = new AddPersonCommandValidator(_logger);

            //Act
            var result = validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValid_SurnameIsTooLong_ReturnsFalse()
        {
            var command = new AddPersonCommand
            {
                Name = "Test",
                Surname = "01245567890124556789012455678901245567890124556789_"
            };
            var validator = new AddPersonCommandValidator(_logger);

            //Act
            var result = validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValid_GetsRightData_ReturnsTrue()
        {
            var command = new AddPersonCommand { Name = "Test", Surname = "Stest" };
            var validator = new AddPersonCommandValidator(_logger);

            //Act
            var result = validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(true));
        }
    }
}
