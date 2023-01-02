using Microsoft.AspNetCore.Mvc;
using PersEmails.Application.Emails.Queries;
using PersEmails.Application.Emails.Commands;
using PersEmails.Application.Persons.Queries;
using PersEmails.ViewModels.Emails;

namespace PersEmails.Controllers
{
    public class EmailsController : BaseController
    {
        private int _pageSize = 10;

        [HttpGet]
        public IActionResult Index([FromQuery] int? page)
        {
            int pageNumber = page ?? 1;
            var emails = QueryService.Execute(new GetEmailsQuery { PageNumber = pageNumber, PageSize = _pageSize });
            var viewModel = new EmailListViewModel
            {
                Emails = emails,
                PageSize = _pageSize,
                PageNumber = pageNumber,
                ItemsCount = emails.Count
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add(int personId) 
        {
            var person = QueryService.Execute(new GetPersonQuery { Id = personId });
            if(person == null)
                return Error("Person not found.");

            return View(new EmailDataViewModel
            {
                PersonId = person.Id,
                PersonName = person.Name,
                PersonSurname = person.Surname
            });
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmail(EmailDataViewModel email)
        {
            var result = CommandService.Execute(new AddEmailToPersonCommand
            {
                EmailAddress = email.EmailAddress,
                PersonId = email.PersonId
            });
            if (result == 1)
            {
                return RedirectToAction("Person", "Persons", new { id = email.PersonId });
            }

            return Error("Email saving failed.");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = CommandService.Execute(new DeleteEmailCommand { Id = id });
            if(result == 1)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }

            return Error("No email deleted.");
        }
    }
}
