using Lab3_WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3_WebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<FoundItem> FoundItems { get; set; } 
        public DbSet<LostItem> LostItems { get; set; }   
    }
}