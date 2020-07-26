using ChoiceA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ChoiceA.Data
{
    public class ChoiceContext : IdentityDbContext
    {
        private readonly string _conStr = "Data Source =.\\SQLEXPRESS;Initial Catalog = ChoiceA; Integrated Security = True";
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public ChoiceContext(DbContextOptions<ChoiceContext> options) : base(options)
        {
        
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DisciplineStudent>()
                      .HasKey(t => new { t.DisciplineId, t.StudentId });

      
         //   modelBuilder
         //.Entity<ApplicationUser>()
         //.HasOne(u => u.Student)
         //.WithOne(p => p.ApplicationUser)
         //.HasForeignKey<Student>(p => p.Id);

         //   modelBuilder
         //  .Entity<ApplicationUser>()
         //  .HasOne(u => u.Teacher)
         //  .WithOne(p => p.ApplicationUser)
         //  .HasForeignKey<Teacher>(p => p.Id);


        }
        //Data Source =.\\SQLEXPRESS;Initial Catalog = ChoiceDbGl1; Integrated Security = True
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conStr);
        }
        public class DisciplineStudent
        {
            public string DisciplineId { get; set; }
            public Discipline Discipline { get; set; }

            public string StudentId { get; set; }
            public Student Student { get; set; }
        }

    }
}
