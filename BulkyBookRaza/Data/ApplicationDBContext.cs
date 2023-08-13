using BulkyBookRaza.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookRaza.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Category> categories { get; set; }
    
    }
}
