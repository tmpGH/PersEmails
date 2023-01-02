using Microsoft.Extensions.Logging;
using Moq;
using PersEmails.Application.Interfaces;
using PersEmails.Application.Persons.Commands;
using PersEmails.Domain.Entities;

namespace Application.UnitTests.Persons.Commands
{
    internal class SavePersonCommandValidatorTests
    {
        private ILogger<SavePersonCommandValidator> _logger;
        private Mock<IAppContext> _context;

        [SetUp]
        public void Setup()
        {
            var mockLogger = new Mock<ILogger<SavePersonCommandValidator>>();
            _logger = mockLogger.Object;
            _context = new Mock<IAppContext>();
        }

        [Test]
        public async Task IsValid_NameIsNull_ReturnsFalse()
        {
            var command = new SavePersonCommand { Surname = "Test" };
            var validator = new SavePersonCommandValidator(_context.Object, _logger);

            //Act
            var result = await validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public async Task IsValid_SurnameIsNull_ReturnsFalse()
        {
            var command = new SavePersonCommand { Name = "Test" };
            var validator = new SavePersonCommandValidator(_context.Object, _logger);

            //Act
            var result = await validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public async Task IsValid_NameIsTooLong_ReturnsFalse()
        {
            var command = new SavePersonCommand
            {
                Name = "01245567890124556789012455678901245567890124556789_",
                Surname = "Stest"
            };
            var validator = new SavePersonCommandValidator(_context.Object, _logger);

            //Act
            var result = await validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public async Task IsValid_SurnameIsTooLong_ReturnsFalse()
        {
            var command = new SavePersonCommand
            {
                Name = "Test",
                Surname = "01245567890124556789012455678901245567890124556789_"
            };
            var validator = new SavePersonCommandValidator(_context.Object, _logger);

            //Act
            var result = await validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public async Task IsValid_PersonDoesNotExist_ReturnsTrue()
        {
            _context.Setup(ctx => ctx.Persons.FindAsync(It.IsAny<int>()))
                .Returns(new ValueTask<Person>(Task<Person>.Run(() => (Person)null)));
            var command = new SavePersonCommand { Name = "Test", Surname = "Stest", Id = -1 };
            var validator = new SavePersonCommandValidator(_context.Object, _logger);

            //Act
            var result = await validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public async Task IsValid_GetsRightData_ReturnsTrue()
        {
            _context.Setup(ctx => ctx.Persons.FindAsync(It.IsAny<int>()))
                .Returns(new ValueTask<Person>(Task<Person>.Run(() => new Person())));
            var command = new SavePersonCommand { Name = "Test", Surname = "Stest", Id = 1 };
            var validator = new SavePersonCommandValidator(_context.Object, _logger);

            //Act
            var result = await validator.IsValid(command);

            //Assert
            Assert.That(result, Is.EqualTo(true));
        }
    }
}
