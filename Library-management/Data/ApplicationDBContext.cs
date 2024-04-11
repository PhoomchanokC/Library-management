using Library_management.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_management.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options):base(options) { }
        public DbSet<Book> books { get; set; }
    }
}
