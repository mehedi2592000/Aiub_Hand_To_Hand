using Microsoft.EntityFrameworkCore;

namespace Aiub_Hand_To_Hand_MVC.Models.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Login> Logins { get; set; }    

        public DbSet<Repository> Repositories { get; set; }

        public DbSet<Job> Jobs { get; set; }
    }
}
