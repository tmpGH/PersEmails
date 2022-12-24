namespace PersEmails.Application.Persons
{
    public class PersonListDto
    {
        public PersonListDto(IList<PersonDto> persons)
        {
            Persons = persons ?? new List<PersonDto>();
        }

        public IList<PersonDto> Persons { get; private set; }
    }
}
