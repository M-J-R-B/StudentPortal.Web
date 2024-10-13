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

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().HasData(
                new UserAccount { Id = 1, Username = "user1", Password = "password1" },
                new UserAccount { Id = 2, Username = "user2", Password = "password2" }
            );
        }
    }
    
}
