using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LesBlaguesDeFlavio.Models;

namespace LesBlaguesDeFlavio.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LesBlaguesDeFlavio.Models.Blague> Blague { get; set; }
    }
}