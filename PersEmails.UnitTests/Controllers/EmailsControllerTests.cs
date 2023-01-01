using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersEmails.Application.Emails.Queries;
using PersEmails.Application.Interfaces;
using PersEmails.Application.Persons;
using PersEmails.Controllers;
using PersEmails.Infrastructure.Interfaces;
using PersEmails.UnitTests.FakeObjects;
using PersEmails.ViewModels;
using PersEmails.ViewModels.Emails;

namespace PersEmails.UnitTests.Controllers
{
    internal class EmailsControllerTests
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
        public void Index_WhenCalled_ReturnsViewWithEmailListViewModel()
        {
            var controller = new EmailsController();
            controller.ControllerContext.HttpContext = _context;
            var emailData = new EmailWithNamesDto { EmailAddress = "e@mai.l" };
            var mockService = new Mock<IQueryService>();
            mockService.Setup(service => service.Execute<IList<EmailWithNamesDto>>(It.IsAny<IQuery<IList<EmailWithNamesDto>>>()))
                .Returns(new List<EmailWithNamesDto> { emailData });
            _serviceProvider.AddService(typeof(IQueryService), mockService.Object);

            //Act
            var view = controller.Index(null);

            //Assert
            Assert.IsNotNull(view);
            var viewResult = view as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<EmailListViewModel>());
            var model = viewResult.Model as EmailListViewModel;
            Assert.That(model.Emails.Count, Is.EqualTo(1));
            Assert.That(model.Emails[0].EmailAddress, Is.EqualTo("e@mai.l"));
        }

        [Test]
        public void Add_GetsWrongId_ReturnsViewWithErrorViewModel()
        {
            var controller = new EmailsController();
            controller.ControllerContext.HttpContext = _context;
            var mockService = new Mock<IQueryService>();
            mockService.Setup(service => service.Execute<PersonDto>(It.IsAny<IQuery<PersonDto>>()))
                .Returns((PersonDto)null);
            _serviceProvider.AddService(typeof(IQueryService), mockService.Object);

            //Act
            var view = controller.Add(-1);

            //Assert
            Assert.IsNotNull(view);
            var viewResult = view as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<ErrorViewModel>());
        }

        [Test]
        public void Add_GetsRightId_ReturnsViewWithEmailDataViewModel()
        {
            var controller = new EmailsController();
            controller.ControllerContext.HttpContext = _context;
            var personId = 10;
            var mockService = new Mock<IQueryService>();
            mockService.Setup(service => service.Execute<PersonDto>(It.IsAny<IQuery<PersonDto>>()))
                .Returns(new PersonDto { Id = personId, Name = "Test" });
            _serviceProvider.AddService(typeof(IQueryService), mockService.Object);

            //Act
            var view = controller.Add(personId);

            //Assert
            Assert.IsNotNull(view);
            var viewResult = view as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<EmailDataViewModel>());
            var model = viewResult.Model as EmailDataViewModel;
            Assert.That(model.PersonId, Is.EqualTo(personId));
            Assert.That(model.PersonName, Is.EqualTo("Test"));
        }

        [Test]
        public async Task SaveEmail_GetsWrongData_ReturnsErrorViewModel()
        {
            var controller = new EmailsController();
            controller.ControllerContext.HttpContext = _context;
            FakeHelpers.MockICommandServiceWithWrongResult(_serviceProvider);
            var viewModel = new EmailDataViewModel();

            //Act
            var view = controller.SaveEmail(viewModel);

            //Assert
            Assert.IsNotNull(view);
            var viewResult = await view as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<ErrorViewModel>());
        }

        [Test]
        public async Task SaveEmail_GetsRightData_ReturnsRedirectToActionResult()
        {
            var controller = new EmailsController();
            controller.ControllerContext.HttpContext = _context;
            FakeHelpers.MockICommandServiceWithRightResult(_serviceProvider);
            var viewModel = new EmailDataViewModel();

            //Act
            var view = await controller.SaveEmail(viewModel);

            //Assert
            Assert.IsNotNull(view);
            Assert.That(view, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Delete_GetsWrongId_ReturnsErrorViewModel()
        {
            var controller = new EmailsController();
            controller.ControllerContext.HttpContext = _context;
            FakeHelpers.MockICommandServiceWithWrongResult(_serviceProvider);

            //Act
            var view = controller.Delete(-1);

            //Assert
            Assert.IsNotNull(view);
            var viewResult = view as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<ErrorViewModel>());
        }

        [Test]
        public void Delete_GetsRightId_ReturnsRedirectResult()
        {
            var controller = new EmailsController();
            controller.ControllerContext.HttpContext = _context;
            FakeHelpers.MockICommandServiceWithRightResult(_serviceProvider);

            //Act
            var view = controller.Delete(10);

            //Assert
            Assert.IsNotNull(view);
            Assert.That(view, Is.TypeOf<RedirectResult>());
        }
    }
}
