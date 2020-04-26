using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BusinessLogic
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public ApiDbContext() { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }

       
        
    }
}
