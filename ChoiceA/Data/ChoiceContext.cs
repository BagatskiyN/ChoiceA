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
    public class ChoiceContext : IdentityDbContext<IdentityUser>
    {
        private readonly string _conStr = "Data Source =.\\SQLEXPRESS;Initial Catalog = ChoiceA; Integrated Security = True";
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public ChoiceContext(DbContextOptions<ChoiceContext> options) : base(options)
        {

            Database.EnsureCreated();
       
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
     
            modelBuilder.Entity<DisciplineStudent>()
                      .HasKey(t => new { t.DisciplineId, t.StudentId });

            //   modelBuilder.Entity<Student>()
            //.HasKey(e => e.Name);
            modelBuilder
         .Entity<ApplicationUser>()
         .HasOne(u => u.Student)
         .WithOne(p => p.ApplicationUser)
         .HasForeignKey<Student>(p => p.Name)
         .HasPrincipalKey<ApplicationUser>(c => c.UserName);
            //      modelBuilder.Entity<Teacher>()
            //.HasKey(e => e.Name);
            modelBuilder
           .Entity<ApplicationUser>()
           .HasOne(u => u.Teacher)
           .WithOne(p => p.ApplicationUser)
           .HasForeignKey<Teacher>(p => p.Name)
           .HasPrincipalKey<ApplicationUser>(c => c.UserName);
            base.OnModelCreating(modelBuilder);

        }
        //Data Source =.\\SQLEXPRESS;Initial Catalog = ChoiceDbGl1; Integrated Security = True
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conStr);
        }
        public class DisciplineStudent
        {
            public int DisciplineId { get; set; }
            public Discipline Discipline { get; set; }

            public int StudentId { get; set; }
            public Student Student { get; set; }
        }

    }
}
