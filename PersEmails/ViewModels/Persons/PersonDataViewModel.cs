﻿using PersEmails.Application.Emails;
using PersEmails.Application.Persons;

namespace PersEmails.ViewModels.Persons
{
    public class PersonDataViewModel
    {
        public PersonDto Person { get; set; }
        public IList<EmailDto> Emails { get; set; }
    }
}