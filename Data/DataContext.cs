using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEF.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace LearnEF.Data
{
    public class DataContext: IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = true;
        }

        public DbSet<Character> Characters { get; set; }

        public DbSet<Backpack> Backpacks { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Faction> Factions { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>(modelBuilder => {
                modelBuilder.ToTable("Companies");

                modelBuilder
                    .HasMany(company => company.Employees)
                    .WithOne()
                    .HasForeignKey(employee => employee.CompanyId)
                    .IsRequired();
                modelBuilder.HasData(
                    new Company
                    {
                        Id = 1, Name = "Awsome Company"
                    }
                );
            });

            modelBuilder.Entity<Employee>(modelBuilder => {
                modelBuilder.ToTable("Employees");

                var employee = Enumerable.Range(1, 1000)
                    .Select(id => new Employee{
                        Id = id,
                        Name = $"Employee #{id}",
                        Salary = 100.0m,
                        CompanyId = 1
                    }).ToList();
                    modelBuilder.HasData(employee);
            });

           
        }
    }
}