using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Security.Domain.Models;
using Greenhouse.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Greenhouse.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Crop> Crops { get; set; }
    public DbSet<Phase> Phases { get; set; }
    public DbSet<Formula> Formulas { get; set; }
    public DbSet<PreparationArea> PreparationAreas { get; set; }
    public DbSet<Bunker> Bunkers { get; set; }
    public DbSet<Tunnel> Tunnels { get; set; }
    public DbSet<GrowRoomRecord> GrowRoomRecords { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Company> Companies { get; set; }
    
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Constraints
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.Username).IsRequired().HasMaxLength(30);
        builder.Entity<User>().Property(p => p.FirstName).IsRequired();
        builder.Entity<User>().Property(p => p.LastName).IsRequired();
        
        // Phases
        
        builder.Entity<Phase>().ToTable("Phases");
        builder.Entity<Phase>().HasKey(p => p.Id);
        builder.Entity<Phase>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Phase>().Property(p => p.Name).IsRequired().HasMaxLength(250);
        // Relationships
        builder.Entity<Phase>()
            .HasMany(p => p.Crops)
            .WithOne(p => p.Phase)
            .HasForeignKey(p => p.PhaseId);
        
        // Crops
        
        builder.Entity<Crop>().ToTable("Crops");
        builder.Entity<Crop>().HasKey(p => p.Id);
        builder.Entity<Crop>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Crop>().Property(p => p.StartDate).IsRequired();
        builder.Entity<Crop>().Property(p => p.EndDate).IsRequired();
        builder.Entity<Crop>().Property(p => p.State).IsRequired();
        // Relationships
        builder.Entity<Crop>()
            .HasMany(p => p.Formulas)
            .WithOne(p => p.Crop)
            .HasForeignKey(p => p.CropId);
        builder.Entity<Crop>()
            .HasMany(p => p.PreparationAreas)
            .WithOne(p => p.Crop)
            .HasForeignKey(p => p.CropId);
        builder.Entity<Crop>()
            .HasMany(p => p.Bunkers)
            .WithOne(p => p.Crop)
            .HasForeignKey(p => p.CropId);
        builder.Entity<Crop>()
            .HasMany(p => p.Tunnels)
            .WithOne(p => p.Crop)
            .HasForeignKey(p => p.CropId);
        builder.Entity<Crop>()
            .HasMany(p => p.GrowRoomRecords)
            .WithOne(p => p.Crop)
            .HasForeignKey(p => p.CropId);
        
        // Formulas
        
        builder.Entity<Formula>().ToTable("Formulas");
        builder.Entity<Formula>().HasKey(p => p.Id);
        builder.Entity<Formula>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Formula>().Property(p => p.Date).IsRequired();
        builder.Entity<Formula>().Property(p => p.Hay).IsRequired();
        builder.Entity<Formula>().Property(p => p.Corn).IsRequired();
        builder.Entity<Formula>().Property(p => p.Guano).IsRequired();
        builder.Entity<Formula>().Property(p => p.CottonSeedCake).IsRequired();
        builder.Entity<Formula>().Property(p => p.SoybeanMeal).IsRequired();
        builder.Entity<Formula>().Property(p => p.Gypsum).IsRequired();
        builder.Entity<Formula>().Property(p => p.Urea).IsRequired();
        builder.Entity<Formula>().Property(p => p.AmmoniumSulphate).IsRequired();
        
        // Preparation Areas
        
        builder.Entity<PreparationArea>().ToTable("PreparationAreas");
        builder.Entity<PreparationArea>().HasKey(p => p.Id);
        builder.Entity<PreparationArea>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<PreparationArea>().Property(p => p.Date).IsRequired();
        builder.Entity<PreparationArea>().Property(p => p.Activities).IsRequired();
        builder.Entity<PreparationArea>().Property(p => p.Temperature).IsRequired();
        builder.Entity<PreparationArea>().Property(p => p.Comment);
        
        // Bunkers
        
        builder.Entity<Bunker>().ToTable("Bunkers");
        builder.Entity<Bunker>().HasKey(p => p.Id);
        builder.Entity<Bunker>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Bunker>().Property(p => p.Date).IsRequired();
        builder.Entity<Bunker>().Property(p => p.ThermocoupleOne).IsRequired();
        builder.Entity<Bunker>().Property(p => p.ThermocoupleTwo).IsRequired();
        builder.Entity<Bunker>().Property(p => p.ThermocoupleThree).IsRequired();
        builder.Entity<Bunker>().Property(p => p.AverageThermocouple).IsRequired();
        builder.Entity<Bunker>().Property(p => p.MotorFrequency).IsRequired();
        builder.Entity<Bunker>().Property(p => p.Comment);
        
        // Tunnels
        
        builder.Entity<Tunnel>().ToTable("Tunnels");
        builder.Entity<Tunnel>().HasKey(p => p.Id);
        builder.Entity<Tunnel>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Tunnel>().Property(p => p.Date).IsRequired();
        builder.Entity<Tunnel>().Property(p => p.ThermocoupleOne).IsRequired();
        builder.Entity<Tunnel>().Property(p => p.ThermocoupleTwo).IsRequired();
        builder.Entity<Tunnel>().Property(p => p.ThermocoupleThree).IsRequired();
        builder.Entity<Tunnel>().Property(p => p.AverageThermocouple).IsRequired();
        builder.Entity<Tunnel>().Property(p => p.MotorFrequency).IsRequired();
        builder.Entity<Tunnel>().Property(p => p.FreshAir).IsRequired();
        builder.Entity<Tunnel>().Property(p => p.Recirculation).IsRequired();
        builder.Entity<Tunnel>().Property(p => p.Comment);
        
        // Grow Room Records
        
        builder.Entity<GrowRoomRecord>().ToTable("GrowRoomRecords");
        builder.Entity<GrowRoomRecord>().HasKey(p => p.Id);
        builder.Entity<GrowRoomRecord>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<GrowRoomRecord>().Property(p => p.Date).IsRequired();
        builder.Entity<GrowRoomRecord>().Property(p => p.GrowRoom).IsRequired();
        builder.Entity<GrowRoomRecord>().Property(p => p.AirTemperature).IsRequired();
        builder.Entity<GrowRoomRecord>().Property(p => p.CompostTemperature).IsRequired();
        builder.Entity<GrowRoomRecord>().Property(p => p.CarbonDioxide).IsRequired();
        builder.Entity<GrowRoomRecord>().Property(p => p.AirHumidity).IsRequired();
        builder.Entity<GrowRoomRecord>().Property(p => p.Setting).IsRequired();
        builder.Entity<GrowRoomRecord>().Property(p => p.ProcessType).IsRequired();
        builder.Entity<GrowRoomRecord>().Property(p => p.Comment);
        
        // Companies
        
        builder.Entity<Company>().ToTable("Companies");
        builder.Entity<Company>().HasKey(p => p.Id);
        builder.Entity<Company>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Company>().Property(p => p.CompanyName).IsRequired().HasMaxLength(250);
        builder.Entity<Company>().Property(p => p.Tin).IsRequired().HasMaxLength(11);
        // Relationships
        builder.Entity<Company>()
            .HasMany(p => p.Employees)
            .WithOne(p => p.Company)
            .HasForeignKey(p => p.CompanyId);
        
        // Employees
        
        builder.Entity<Employee>().ToTable("Employees");
        builder.Entity<Employee>().HasKey(p => p.Id);
        builder.Entity<Employee>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Employee>().Property(p => p.FirstName).IsRequired().HasMaxLength(250);
        builder.Entity<Employee>().Property(p => p.LastName).IsRequired().HasMaxLength(250);
        builder.Entity<Employee>().Property(p => p.Email).IsRequired().HasMaxLength(250);
        builder.Entity<Employee>().Property(p => p.Password).IsRequired().HasMaxLength(250);
        // Relationships
        builder.Entity<Employee>()
            .HasMany(p => p.Formulas)
            .WithOne(p => p.Employee)
            .HasForeignKey(p => p.EmployeeId);
        builder.Entity<Employee>()
            .HasMany(p => p.PreparationAreas)
            .WithOne(p => p.Employee)
            .HasForeignKey(p => p.CropId);
        builder.Entity<Employee>()
            .HasMany(p => p.Bunkers)
            .WithOne(p => p.Employee)
            .HasForeignKey(p => p.EmployeeId);
        builder.Entity<Employee>()
            .HasMany(p => p.Tunnels)
            .WithOne(p => p.Employee)
            .HasForeignKey(p => p.EmployeeId);
        builder.Entity<Employee>()
            .HasMany(p => p.GrowRoomRecords)
            .WithOne(p => p.Employee)
            .HasForeignKey(p => p.EmployeeId);
        
        
        // Apply Snake Case Naming Convention
        
        builder.UseSnakeCaseNamingConvention();
    }
}