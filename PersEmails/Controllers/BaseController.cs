using Microsoft.AspNetCore.Mvc;
using PersEmails.Infrastructure.Interfaces;
using PersEmails.ViewModels;

namespace PersEmails.Controllers
{
    public abstract class BaseController : Controller
    {
        private IQueryService _queryService;
        protected IQueryService QueryService => _queryService ??= HttpContext.RequestServices.GetService<IQueryService>();

        private ICommandService _commandService;
        protected ICommandService CommandService => _commandService ??= HttpContext.RequestServices.GetService<ICommandService>();
    
        protected IActionResult Error(string errorMessage)
        {
            var viewModel = new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier,
                Error = errorMessage
            };

            return View("Error", viewModel);
        }
    }
}
