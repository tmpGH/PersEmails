using Microsoft.AspNetCore.Mvc;
using PersEmails.IntegrationTests.FakeObjects;
using PersEmails.ViewModels;

namespace PersEmails.IntegrationTests.Controllers
{
    internal class BaseControllerTests
    {
        [Test]
        public void Error_WhenCalled_ReturnsViewWithErrorViewModel()
        {
            var controller = new FakeBaseController();
            
            //Act
            var view = controller.Error("My Error");
            var viewResult = view as ViewResult;
            
            //Assert
            Assert.IsNotNull(view);
            Assert.That(viewResult.Model, Is.TypeOf<ErrorViewModel>());
            Assert.That(viewResult.ViewName, Is.EqualTo("Error"));

            var model = viewResult.Model as ErrorViewModel;
            Assert.That(model.Error, Is.EqualTo("My Error"));
        }
    }
}
