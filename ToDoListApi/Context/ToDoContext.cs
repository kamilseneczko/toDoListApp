using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ToDoListApi.Entities;

namespace ToDoListApi.Context
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }

        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<TaskGroup> TaskGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskGroup>()
                .HasMany(t => t.UserTasks)
                .WithOne(ut => ut.User)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
