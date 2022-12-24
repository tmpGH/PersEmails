using Microsoft.AspNetCore.Mvc;
using PersEmails.Infrastructure.Interfaces;
using PersEmails.Models;
using System.Diagnostics;

namespace PersEmails.Controllers
{
    public abstract class BaseController : Controller
    {
        private IQueryService _queryService;
        protected IQueryService QueryService => _queryService ??= HttpContext.RequestServices.GetService<IQueryService>();

        private ICommandService _commandService;
        protected ICommandService CommandService => _commandService ??= HttpContext.RequestServices.GetService<ICommandService>();

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
