using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersEmails.Application.Emails;
using PersEmails.Application.Interfaces;
using PersEmails.Application.Persons;
using PersEmails.Application.Persons.Queries;
using PersEmails.Controllers;
using PersEmails.Infrastructure.Interfaces;
using PersEmails.IntegrationTests.FakeObjects;
using PersEmails.ViewModels;
using PersEmails.ViewModels.Persons;

namespace PersEmails.IntegrationTests.Controllers
{
    internal class PersonsControllerTests
    {
        HttpContext _context;
        FakeServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            _context = new FakeHttpContext();
            _serviceProvider = _context.RequestServices as FakeServiceProvider;
        }

        [Test]
        public async Task Index_WhenCalled_ReturnsViewWithPersonListViewModel()
        {
            var controller = new PersonsController();
            controller.ControllerContext.HttpContext = _context;
            var personData = new PersonWithEmailAddressDto { Name = "Test", Surname = "STest" };
            var mockService = new Mock<IQueryService>();
            mockService.Setup(service => service.ExecuteAsync<IList<PersonWithEmailAddressDto>>(
                    It.IsAny<IQueryAsync<IList<PersonWithEmailAddressDto>>>(), It.IsAny<CancellationToken>()
                )).Returns(Task.Run(() => (IList<PersonWithEmailAddressDto>) new List<PersonWithEmailAddressDto> { personData }));
            _serviceProvider.AddService(typeof(IQueryService), mockService.Object);

            //Act
            var view = await controller.Index(null);

            //Assert
            Assert.IsNotNull(view);
            var viewResult = view as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<PersonListViewModel>());
            var model = viewResult.Model as PersonListViewModel;
            Assert.That(model.Persons.Count, Is.EqualTo(1));
            Assert.That(model.Persons[0].Name, Is.EqualTo("Test"));
            Assert.That(model.Persons[0].Surname, Is.EqualTo("STest"));
        }

        [Test]
        public async Task Person_WhenCalled_ReturnsViewWithPersonDataViewModel()
        {
            var controller = new PersonsController();
            controller.ControllerContext.HttpContext = _context;
            var emailData = new EmailDto { EmailAddress = "e@mai.l" };
            var mockService = new Mock<IQueryService>();
            mockService.Setup(service => service.Execute<PersonDto>(
                    It.IsAny<IQuery<PersonDto>>()
                )).Returns(new PersonDto { Name = "Test", Surname = "STest" });
            mockService.Setup(service => service.ExecuteAsync<IList<EmailDto>>(
                    It.IsAny<IQueryAsync<IList<EmailDto>>>(), It.IsAny<CancellationToken>()
                )).Returns(Task.Run(() => (IList<EmailDto>)new List<EmailDto> { emailData }));
            _serviceProvider.AddService(typeof(IQueryService), mockService.Object);

            //Act
            var view = await controller.Person(1);

            //Assert
            Assert.IsNotNull(view);
            var viewResult = view as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<PersonDataViewModel>());
            var model = viewResult.Model as PersonDataViewModel;
            Assert.That(model.Person.Name, Is.EqualTo("Test"));
            Assert.That(model.Person.Surname, Is.EqualTo("STest"));
            Assert.That(model.Emails.Count, Is.EqualTo(1));
            Assert.That(model.Emails[0].EmailAddress, Is.EqualTo("e@mai.l"));
        }

        [Test]
        public void Add_WhenCalled_ReturnsViewWithEditPersonViewModel()
        {
            var controller = new PersonsController();

            //Act
            var view = controller.Add();

            //Assert
            Assert.IsNotNull(view);
            var viewResult = view as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<EditPersonViewModel>());
        }

        [Test]
        public void Edit_GetsWrongId_ReturnsViewWithErrorViewModel()
        {
            var controller = new PersonsController();
            controller.ControllerContext.HttpContext = _context;
            var mockService = new Mock<IQueryService>();
            mockService.Setup(service => service.Execute<PersonDto>(It.IsAny<IQuery<PersonDto>>()))
                .Returns((PersonDto)null);
            _serviceProvider.AddService(typeof(IQueryService), mockService.Object);

            //Act
            var view = controller.Edit(-1);

            //Assert
            Assert.IsNotNull(view);
            var viewResult = view as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<ErrorViewModel>());
        }

        [Test]
        public void Edit_GetsRightId_ReturnsViewWithEditPersonViewModel()
        {
            var controller = new PersonsController();
            controller.ControllerContext.HttpContext = _context;
            var personId = 10;
            var mockService = new Mock<IQueryService>();
            mockService.Setup(service => service.Execute<PersonDto>(It.IsAny<IQuery<PersonDto>>()))
                .Returns(new PersonDto { Id = personId, Name = "Test" });
            _serviceProvider.AddService(typeof(IQueryService), mockService.Object);

            //Act
            var view = controller.Edit(personId);

            //Assert
            Assert.IsNotNull(view);
            var viewResult = view as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<EditPersonViewModel>());
            var model = viewResult.Model as EditPersonViewModel;
            Assert.That(model.Id, Is.EqualTo(personId));
            Assert.That(model.Name, Is.EqualTo("Test"));
        }

        [Test]
        public async Task SavePerson_GetsWrongData_ReturnsErrorViewModel()
        {
            var controller = new PersonsController();
            controller.ControllerContext.HttpContext = _context;
            FakeHelpers.MockICommandServiceWithWrongResult(_serviceProvider);
            var viewModel = new EditPersonViewModel();

            //Act
            var view = controller.SavePerson(viewModel);

            //Assert
            Assert.IsNotNull(view);
            var viewResult = await view as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<ErrorViewModel>());
        }

        [Test]
        public async Task SavePerson_GetsRightData_ReturnsRedirectToActionResult()
        {
            var controller = new PersonsController();
            controller.ControllerContext.HttpContext = _context;
            FakeHelpers.MockICommandServiceWithRightResult(_serviceProvider);
            var viewModel = new EditPersonViewModel();

            //Act
            var view = await controller.SavePerson(viewModel);

            //Assert
            Assert.IsNotNull(view);
            Assert.That(view, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public async Task Delete_GetsWrongId_ReturnsErrorViewModel()
        {
            var controller = new PersonsController();
            controller.ControllerContext.HttpContext = _context;
            FakeHelpers.MockICommandServiceWithWrongResult(_serviceProvider);

            //Act
            var view = controller.Delete(-1);

            //Assert
            Assert.IsNotNull(view);
            var viewResult = await view as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<ErrorViewModel>());
        }

        [Test]
        public async Task Delete_GetsRightId_ReturnsRedirectToActionResult()
        {
            var controller = new PersonsController();
            controller.ControllerContext.HttpContext = _context;
            FakeHelpers.MockICommandServiceWithRightResult(_serviceProvider);

            //Act
            var view = await controller.Delete(10);

            //Assert
            Assert.IsNotNull(view);
            Assert.That(view, Is.TypeOf<RedirectToActionResult>());
        }
    }
}
