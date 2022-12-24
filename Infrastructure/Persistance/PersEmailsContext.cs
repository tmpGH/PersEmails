using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PersEmails.Infrastructure.Persistance
{
    public class PersEmailsContext : DbContext, IAppContext
    {
        // TODO: move to the Settings
        private string connectionString = @"Data Source=PC\SQLEXPRESS;Initial Catalog=PersEmails;Integrated Security=True";

        public DbSet<Person> Persons { get; set; }
        public DbSet<Email> Emails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(connectionString);
            //options.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
