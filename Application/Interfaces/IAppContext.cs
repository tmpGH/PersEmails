using PersEmails.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PersEmails.Application.Interfaces
{
    public interface IAppContext
    {
        DbSet<Person> Persons { get; set; }
        DbSet<Email> Emails { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
