using Directory.Models;
using Microsoft.EntityFrameworkCore;

namespace Directory.Data
{
    public class ContactDbContext: DbContext
    {
        public ContactDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set;}
    }
}