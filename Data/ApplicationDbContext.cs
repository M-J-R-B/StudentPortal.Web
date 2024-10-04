using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        //private DbSet<Student> students;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet <UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().HasData();
        }
    }
    
    

}
