using Microsoft.AspNetCore.Mvc;
using PersEmails.Application.Emails.Queries;
using PersEmails.Application.Emails.Commands;
using PersEmails.Application.Persons.Queries;
using PersEmails.Application.Emails;

namespace PersEmails.Controllers
{
    public class EmailsController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var emails = await QueryService.ExecuteAsync(new GetAllEmailsQuery());

            return View(emails);
        }

        public async Task<IActionResult> Add(int personId)
        {
            var person = await QueryService.ExecuteAsync(new GetPersonQuery(personId));
            if(person != null)
            {
                return View(person);
            }

            // TODO: obsluzyc errora
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> AddEmail(EmailDto email)
        {
            var result = await CommandService.ExecuteAsync(new AddEmailCommand(email));
            if (result == 1)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }

            // TODO: obsluzyc errora
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await CommandService.ExecuteAsync(new DeleteEmailCommand(id));
            if(result == 1)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }

            // TODO: obsluzyc errora
            return RedirectToAction("Error");
        }
    }
}
