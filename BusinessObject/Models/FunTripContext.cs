using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BusinessObject.Models
{
    public partial class FunTripContext : DbContext
    {
        public FunTripContext()
        {
        }

        public FunTripContext(DbContextOptions<FunTripContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<AreaGroup> AreaGroups { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:yume.database.windows.net,1433;Initial Catalog=FunTrip;Persist Security Info=False;User ID=yume;Password=PutinD@ide;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.Username, "IX_Account")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "IX_Account_1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Role");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.ApartmentName).HasMaxLength(50);

                entity.Property(e => e.DistrictId).HasColumnName("District_ID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Apartment_District");
            });

            modelBuilder.Entity<AreaGroup>(entity =>
            {
                entity.ToTable("Area_Group");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AreaId).HasColumnName("Area_ID");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.AreaGroups)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Area_Group_Area");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AreaGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Area_Group_Group");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.EndLocationId).HasColumnName("End_Location_ID");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Feedback).HasMaxLength(200);

                entity.Property(e => e.StartLocationId).HasColumnName("Start_Location_ID");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.VehicleId).HasColumnName("Vehicle_ID");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_Order_Driver");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Order_Employee");

                entity.HasOne(d => d.StartLocation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.StartLocationId)
                    .HasConstraintName("FK_Order_Area_Group");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Booking_Vehicles");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category1)
                    .HasMaxLength(50)
                    .HasColumnName("Category");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.District1)
                    .HasMaxLength(100)
                    .HasColumnName("District");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("Driver");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("Account_ID");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.CreditCard).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Gmail)
                    .HasMaxLength(150)
                    .HasColumnName("gmail");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.Img)
                    .HasMaxLength(1000)
                    .HasColumnName("img");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.VehicleId).HasColumnName("Vehicle_ID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Driver_Account");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Driver_Group");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("Account_ID");

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Gmail).HasMaxLength(150);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Employee_Account");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApartmentId).HasColumnName("Apartment_ID");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(50)
                    .HasColumnName("Group_Name");

                entity.Property(e => e.ManagerId).HasColumnName("ManagerID");

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Role1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Role");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.Img)
                    .HasMaxLength(1000)
                    .HasColumnName("img");

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(50)
                    .HasColumnName("manufacturer");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.VehicleName)
                    .HasMaxLength(50)
                    .HasColumnName("Vehicle_Name");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_Category");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_Vehicles_Driver");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
