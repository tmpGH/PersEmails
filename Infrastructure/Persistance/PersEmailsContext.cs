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

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // string connectionString = @"Data Source=PC\SQLEXPRESS;Initial Catalog=PersEmails;Integrated Security=True";
            // options.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
