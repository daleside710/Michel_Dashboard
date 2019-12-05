using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AdminApp.Models;

namespace AdminApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Customizations
            builder.Entity<User>(i =>
            {
                i.ToTable("usuarios_usu");
                i.HasKey(u => u.Id);
                i.Property(u => u.Email).HasColumnName("email_usu");
                i.Property(u => u.PasswordHash).HasColumnName("pass_usu");
                i.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique(false);
               
            });
            
            builder.Entity<Participation>()
                .HasOne(p => p.Regalo)
                .WithOne(r => r.Participation)
                .HasForeignKey<Regalo>(r => r.id_regalo)
                .HasPrincipalKey<Participation>(p=>p.premioSelFrnt_par);
        }

        public DbSet<AdminApp.Models.User> User { get; set; }

        public DbSet<AdminApp.Models.Workshop> Workshop { get; set; }

        public DbSet<AdminApp.Models.Participation> Participation { get; set; }

        public DbSet<AdminApp.Models.Regalo> Regalo { get; set; }

        public DbSet<AdminApp.Models.General> General { get; set; }

        public DbSet<AdminApp.Models.Regaloslimite> Regaloslimite { get; set; }
        public DbSet<AdminApp.Models.Regalosalert> Regalosalert { get; set; }
        public DbSet<AdminApp.Models.Llantas> Llantas { get; set; }
    }
}
