using Microsoft.AspNetCore.Mvc;
using PersEmails.Application.Emails.Queries;
using PersEmails.Application.Emails.Commands;
using PersEmails.Application.Persons.Queries;
using PersEmails.Application.Emails;
using PersEmails.ViewModels;
using PersEmails.Domain.Entities;

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

            return Error("Person not found.");
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmail(EmailDto email)
        {
            var command = CommandService.GetAsyncCommand<AddEmailToPersonCommand>();
            command.Email = email;
            var result = await CommandService.ExecuteAsync(command);
            if (result == 1)
            {
                return RedirectToAction("Person", "Persons", new { id = email.PersonId });
            }

            return Error("Email saving failed.");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = CommandService.Execute(new DeleteEmailCommand(id));
            if(result == 1)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }

            return Error("Email deletion failed.");
        }
    }
}
