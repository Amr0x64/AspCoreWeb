using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApplication3.Models;

#nullable disable

namespace WebApplication3
{
    public partial class RPRCContext : IdentityDbContext<User>
    {
        public RPRCContext()    
        {
        }

        public RPRCContext(DbContextOptions<RPRCContext> options)
            : base(options)
        {
        }

        /*public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }*/
        public virtual DbSet<CartLine> CartLines { get; set; }
        public virtual DbSet<FiasStatment> FiasStatments { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Socrbase> Socrbases { get; set; }
        public virtual DbSet<StreetNumber> Apartments { get; set; }
        public virtual DbSet<UserViewProduct> UserViewProducts { get; set; }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=RPRC;Username=postgres;Password=cronaldo789");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            /*modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.Property(e => e.Id)
                    .HasMaxLength(450)
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsFixedLength(true);

                entity.Property(e => e.NormalizedName)
                    .HasMaxLength(256)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasKey(k => k.Id);
                
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.RoleId)
                    .HasMaxLength(450)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .HasMaxLength(450)
                    .IsFixedLength(true);

                entity.Property(e => e.LockoutEnd)
                    .HasMaxLength(34)
                    .IsFixedLength(true);

                entity.Property(e => e.NormalizedEmail)
                    .HasMaxLength(256)
                    .IsFixedLength(true);

                entity.Property(e => e.NormalizedUserName)
                    .HasMaxLength(256)
                    .IsFixedLength(true);

                entity.Property(e => e.UserName)
                    .HasMaxLength(256)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
            

                entity.Property(e => e.LoginProvider)
                    .HasMaxLength(450)
                    .IsFixedLength(true);

                entity.Property(e => e.ProviderKey)
                    .HasMaxLength(450)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
               

                entity.Property(e => e.RoleId)
                    .HasMaxLength(450)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
               

                entity.Property(e => e.LoginProvider)
                    .HasMaxLength(450)
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .HasMaxLength(450)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .IsFixedLength(true);
            });*/

            /*modelBuilder.Entity<CartLine>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<FiasStatment>(entity =>
            {
                

                entity.Property(e => e.ActStatus).HasColumnName("act_status");

                entity.Property(e => e.AddUser).HasColumnName("add_user");

                entity.Property(e => e.AddressName)
                    .HasMaxLength(120)
                    .HasColumnName("address_name")
                    .IsFixedLength(true);

                entity.Property(e => e.CurrStatus).HasColumnName("curr_status");

                entity.Property(e => e.EndDate)
                    .HasPrecision(3)
                    .HasColumnName("end_date");

                entity.Property(e => e.FiasGuid).HasColumnName("fias_guid");

                entity.Property(e => e.FiasStatementsId).HasColumnName("fias_statements_id");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.NextId).HasColumnName("next_id");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.PrevId).HasColumnName("prev_id");

                entity.Property(e => e.ShortTypeName)
                    .HasMaxLength(10)
                    .HasColumnName("short_type_name")
                    .IsFixedLength(true);

                entity.Property(e => e.StartDate)
                    .HasPrecision(3)
                    .HasColumnName("start_date");

                entity.Property(e => e.TypeName).HasColumnName("type_name");
            });

            modelBuilder.Entity<Order>(entity =>
            {
        

                entity.Property(e => e.OrderDate)
                    .HasMaxLength(27)
                    .IsFixedLength(true);

                entity.Property(e => e.OrderId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Product>(entity =>
            {
              

                entity.Property(e => e.AddDate)
                    .HasMaxLength(27)
                    .IsFixedLength(true);

                entity.Property(e => e.ChangeDate)
                    .HasMaxLength(27)
                    .IsFixedLength(true);

                entity.Property(e => e.isRemoved).HasColumnName("isRemoved");

                entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Socrbase>(entity =>
            {
           

                entity.ToTable("SOCRBASE");

                entity.Property(e => e.KodTSt)
                    .HasMaxLength(4)
                    .HasColumnName("KOD_T_ST");

                entity.Property(e => e.Level).HasColumnName("LEVEL");

                entity.Property(e => e.Scname)
                    .HasMaxLength(10)
                    .HasColumnName("SCNAME");

                entity.Property(e => e.Socrname)
                    .HasMaxLength(50)
                    .HasColumnName("SOCRNAME");
            });

            modelBuilder.Entity<StreetNumber>(entity =>
            {
               

                entity.Property(e => e.EndDate)
                    .HasMaxLength(27)
                    .IsFixedLength(true);

                entity.Property(e => e.Startdate)
                    .HasMaxLength(27)
                    .IsFixedLength(true);

                entity.Property(e => e.StreetNumberId).HasColumnName("StreetNumberID");
            });

            modelBuilder.Entity<UserViewProduct>(entity =>
            {
                entity.Property(e => e.UserIP).HasColumnName("UserIP");

                entity.Property(e => e.UserViewProductId).ValueGeneratedOnAdd();

                entity.Property(e => e.ViewDate)
                    .HasMaxLength(27)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<AspNetRoleClaim>()
                .HasOne(c => c.AspNetRole)
                .WithMany(s => s.AspNetRoleClaims)
                .HasForeignKey(p => p.RoleId)
                .HasPrincipalKey(k => k.Id);

            modelBuilder.Entity<AspNetUserLogin>()
                .HasKey(k => new {k.LoginProvider, k.ProviderKey});

            modelBuilder.Entity<AspNetUserRole>()
                .HasKey(k => new {k.RoleId, k.UserId});

            modelBuilder.Entity<AspNetUserToken>()
                .HasKey(k => new {k.UserId, k.LoginProvider, k.Name});*/

            /*modelBuilder.Entity<StreetNumber>()
                .HasOne(f => f.FiasStatment)
                .WithMany(s => s.StreetNumbers)
                .HasForeignKey(fk => fk.FiasGuid)
                .HasPrincipalKey(k => k.FiasGuid);*/    
            
            OnModelCreatingPartial(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
