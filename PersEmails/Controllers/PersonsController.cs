using Microsoft.AspNetCore.Mvc;
using PersEmails.Application.Persons.Commands;
using PersEmails.Application.Emails.Queries;
using PersEmails.Application.Persons.Queries;
using PersEmails.ViewModels.Persons;

namespace PersEmails.Controllers
{
    public class PersonsController : BaseController
    {
        private int _pageSize = 6;

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int? page)
        {
            int pageNumber = page ?? 1;
            var persons = await QueryService.ExecuteAsync(new GetPersonsQuery { PageNumber = pageNumber, PageSize = _pageSize });
            var viewModel = new PersonListViewModel
            {
                Persons = persons,
                PageSize = _pageSize,
                PageNumber = pageNumber,
                ItemsCount = persons.Count
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Person(int id)
        {
            var viewModel = new PersonDataViewModel
            {
                Person = QueryService.Execute(new GetPersonQuery { Id = id }),
                Emails = await QueryService.ExecuteAsync(new GetPersonEmailsQuery { Id = id })
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new EditPersonViewModel());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var person = QueryService.Execute(new GetPersonQuery { Id = id });
            if (person == null)
                return Error("Person not found.");

            return View(new EditPersonViewModel
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                Description = person.Description
            });
        }

        [HttpPost]
        public async Task<IActionResult> SavePerson(EditPersonViewModel person)
        {
            var result = person.Id == 0
                ? await Task.Run(() => CommandService.Execute(new AddPersonCommand
                    {
                        Name = person.Name,
                        Surname = person.Surname,
                        Description = person.Description
                    }))
                : await CommandService.ExecuteAsync(new SavePersonCommand
                    {
                        Id = person.Id,
                        Name = person.Name,
                        Surname = person.Surname,
                        Description = person.Description
                    })
            ;
            if (result == 1)
            {
                return RedirectToAction("Index");
            }

            return Error("Person saving failed.");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await CommandService.ExecuteAsync(new DeletePersonCommand { Id = id });
            if (result > 0)
            {
                return RedirectToAction("Index");
            }

            return Error("No person deleted.");
        }
    }
}
