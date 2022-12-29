using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PersEmails.Infrastructure.Persistance
{
    public class PersEmailsContext : DbContext, IAppContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Email> Emails { get; set; }

        public PersEmailsContext(DbContextOptions<PersEmailsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
