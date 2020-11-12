using Entities.Models;
using Microsoft.EntityFrameworkCore;
namespace Entities
{
    public class RepositoryContext : DbContext
    {
         public RepositoryContext(DbContextOptions options)
            :base(options){}

        public DbSet<Project> Project {get; set;}
    }
}