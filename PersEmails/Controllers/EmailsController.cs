using Microsoft.AspNetCore.Mvc;
using PersEmails.Application.Emails.Queries;
using PersEmails.Application.Emails.Commands;
using PersEmails.Application.Persons.Queries;
using PersEmails.Application.Emails;
using PersEmails.ViewModels;

namespace PersEmails.Controllers
{
    public class EmailsController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            var emails = QueryService.Execute(new GetAllEmailsQuery());

            return View(emails);
        }

        [HttpGet]
        public IActionResult Add(int personId)
        {
            var person = QueryService.Execute(new GetPersonQuery(personId));
            if(person != null)
            {

                return View( new EmailDataViewModel {
                        Email = new EmailDto { PersonId = person.Id },
                        Person = person
                });
            }

            return View("Error", new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier,
                Error = "Person not found."
            });
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmail(EmailDto email)
        {
            var result = await CommandService.ExecuteAsync(new AddEmailToPersonCommand(email));
            if (result == 1)
            {
                return RedirectToAction("Person", "Persons", new { id = email.PersonId });
            }

            return View("Error", new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier,
                Error = "Email saving failed."
            });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = CommandService.Execute(new DeleteEmailCommand(id));
            if(result == 1)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }

            return View("Error", new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier,
                Error = "Email deletion failed."
            });
        }
    }
}
