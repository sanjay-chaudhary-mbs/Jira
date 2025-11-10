using Microsoft.EntityFrameworkCore;
using MiniJira.API.Models;

namespace MiniJira.API.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
    }
}
