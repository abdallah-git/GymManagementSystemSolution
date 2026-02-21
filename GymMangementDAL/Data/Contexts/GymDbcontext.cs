using GymMangementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Data.Contexts
{
    public class GymDbcontext : IdentityDbContext<ApplicationUser> 
    {

        public GymDbcontext(DbContextOptions<GymDbcontext> options):base(options)
        {
            
            


        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = . ; Database = GymManagement; Trusted_Connection = true ; TrustServerCertificate = true "); 
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ApplicationUser>(ep =>
            {
                ep.Property(x => x.FirsName)
                .HasColumnType("varchar")
                .HasMaxLength(50);

                ep.Property(x => x.LastNamee)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            });
        }


        #region Dbsesets


        


        public DbSet<Member> Members { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<Healthrecord> Healthrecords { get; set; }

        public DbSet<Membersession> Membersessions { get; set; }

        public DbSet<Membership> Memberships { get; set; }






        #endregion



    }
}
