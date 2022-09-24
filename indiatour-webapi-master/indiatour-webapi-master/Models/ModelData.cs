using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace indiatour_webapi_master.Models
{
    public partial class ModelData : DbContext
    {
        public ModelData()
            : base("name=ModelData")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<booking> bookings { get; set; }
        public virtual DbSet<cost> costs { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<itinerary> itineraries { get; set; }
        public virtual DbSet<passanger> passangers { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<sector> sectors { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tourpackage> tourpackages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<customer>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.passangers)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_cust_id);

            modelBuilder.Entity<itinerary>()
                .Property(e => e.Day)
                .IsUnicode(false);

            modelBuilder.Entity<itinerary>()
                .Property(e => e.Startlocation)
                .IsUnicode(false);

            modelBuilder.Entity<itinerary>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<passanger>()
                .Property(e => e.Firstname)
                .IsUnicode(false);

            modelBuilder.Entity<passanger>()
                .Property(e => e.Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<passanger>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<passanger>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<passanger>()
                .Property(e => e.Passport)
                .IsUnicode(false);

            modelBuilder.Entity<passanger>()
                .Property(e => e.Aadharcard)
                .IsUnicode(false);

            modelBuilder.Entity<passanger>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<passanger>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<passanger>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<passanger>()
                .Property(e => e.cost)
                .HasPrecision(10, 0);

            modelBuilder.Entity<role>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .HasMany(e => e.customers)
                .WithMany(e => e.roles)
                .Map(m => m.ToTable("user_roles").MapRightKey("user_id"));

            modelBuilder.Entity<sector>()
                .Property(e => e.Sector_Id)
                .IsUnicode(false);

            modelBuilder.Entity<sector>()
                .Property(e => e.Subsector_Id)
                .IsUnicode(false);

            modelBuilder.Entity<sector>()
                .Property(e => e.Sectorname)
                .IsUnicode(false);

            modelBuilder.Entity<sector>()
                .Property(e => e.Imgpath)
                .IsUnicode(false);

            modelBuilder.Entity<tourpackage>()
                .Property(e => e.Package_Name)
                .IsUnicode(false);
        }
    }
}
