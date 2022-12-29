using Microsoft.AspNetCore.Mvc;
using PersEmails.Application.Persons.Commands;
using PersEmails.Application.Emails.Queries;
using PersEmails.Application.Persons.Queries;
using PersEmails.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PersEmails.Controllers
{
    public class PersonsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var persons = await QueryService.ExecuteAsync(new GetPersonsQuery());

            return View(persons);
        }

        [HttpGet]
        public async Task<IActionResult> Person(int id)
        {
            var person = QueryService.Execute(new GetPersonQuery(id));
            var emails = await QueryService.ExecuteAsync(new GetPersonEmailsQuery(id));
            var viewModel = new PersonDataViewModel
            {
                Person = person,
                Emails = emails
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new EditPersonViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SavePerson(PersonDto person)
        {
            int result = 0;
            if (person.Id == 0)
            {
                var command = CommandService.GetCommand<AddPersonCommand>();
                command.Person = person;
                result = CommandService.Execute(command);
            }
            else
            {
                var command = CommandService.GetAsyncCommand<SavePersonCommand>();
                command.Person = person;
                result = await CommandService.ExecuteAsync(command);
            }

            if (result > 0)
            {
                return RedirectToAction("Index");
            }

            return Error("Person saving failed.");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var person = QueryService.Execute(new GetPersonQuery(id));
            if(person != null)
            {
                return View(new EditPersonViewModel {
                    Id = person.Id,
                    Name = person.Name,
                    Surname = person.Surname,
                    Description = person.Description
                });
            }

            return Error("Person not found.");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await CommandService.ExecuteAsync(new DeletePersonCommand(id));

            if (result > 0)
            {
                return RedirectToAction("Index");
            }

            return Error("Person deletion failed.");
        }
    }
}
