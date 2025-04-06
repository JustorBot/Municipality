using Microsoft.EntityFrameworkCore;
using EvironmentalMunicipality.Models;

namespace EvironmentalMunicipality.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Citizen> Citizen { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure table names explicitly
            modelBuilder.Entity<ServiceRequest>().ToTable("ServiceRequests");
            modelBuilder.Entity<Citizen>().ToTable("Citizen");
            modelBuilder.Entity<Staff>().ToTable("Staff");

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Citizen)
                .WithMany(c => c.ServiceRequests)
                .HasForeignKey(sr => sr.CitizenID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}