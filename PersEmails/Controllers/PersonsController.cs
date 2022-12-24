using Microsoft.AspNetCore.Mvc;
using PersEmails.Application.Persons.Commands;
using PersEmails.Application.Emails.Queries;
using PersEmails.Application.Persons.Queries;
using PersEmails.Models;
using PersEmails.Application.Persons;

namespace PersEmails.Controllers
{
    public class PersonsController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var persons = await QueryService.ExecuteAsync(new GetPersonsQuery());

            return View(persons);
        }

        public async Task<IActionResult> Person(int id)
        {
            var person = QueryService.Execute(new GetPersonQuery(id));
            var emails = await QueryService.ExecuteAsync(new GetPersonEmailsQuery(id));
            var viewModel = new PersonDataViewModel
            {
                Person = person,
                Emails = emails.Emails
            };

            return View(viewModel);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPerson(PersonDto person)
        {
            // TODO: AddPersonCommand

            return View();
        }

        public IActionResult Edit(int id)
        {
            var person = QueryService.Execute(new GetPersonQuery(id));
            return View(person);
        }

        public async Task<RedirectToActionResult> Delete(int id)
        {
            var result = await CommandService.ExecuteAsync(new DeletePersonCommand(id));

            if (result > 0)
            {
                return RedirectToAction("Index");
            }

            // TODO: obsluzyc errora
            return RedirectToAction("Error");
        }
    }
}
