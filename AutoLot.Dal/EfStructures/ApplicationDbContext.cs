namespace AutoLot.Dal.EfStructures;

public partial class ApplicationDbContext : DbContext
{
    public virtual DbSet<CreditRisk> CreditRisks { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<CustomerOrderViewModel> CustomerOrderViewModels { get; set; }
    public virtual DbSet<Car> Cars { get; set; }
    public virtual DbSet<Make> Makes { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Driver> Drivers { get; set; }
    public virtual DbSet<CarDriver> CarsToDrivers { get; set; }
    public virtual DbSet<Radio> Radios { get; set; }
    public virtual DbSet<SeriLogEntry> SeriLogEntries { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        SavingChanges += (sender, args) =>
        {
            string cs = ((ApplicationDbContext)sender)!.Database!.GetConnectionString();
            Console.WriteLine($"Saving changes for {cs}");
        };
        SavedChanges += (sender, args) =>
        {
            string cs = ((ApplicationDbContext)sender)!.Database!.GetConnectionString();
            Console.WriteLine($"Saved {args!.EntitiesSavedCount} changes for {cs}");
        };
        SaveChangesFailed += (sender, args) =>
        {
            Console.WriteLine($"An exception occurred! {args.Exception.Message} entities");
            Console.WriteLine("Inner Exception ==> " + args.Exception.InnerException);
        };

        ChangeTracker.Tracked += (sender, args) =>
        {
            var source = (args.FromQuery) ? "Database" : "Code";
            if (args.Entry.Entity is Car car)
            {
                Console.WriteLine($"Car entry {car.PetName} was added from {source}");
            }
        };

        ChangeTracker.StateChanged += (sender, args) =>
        {
            if (args.Entry.Entity is not Car c)
            {
                return;
            }
            var action = string.Empty;
            Console.WriteLine($"Car {c.PetName} was {args.OldState} before the state changed to {args.NewState}");
            switch (args.NewState)
            {
                case EntityState.Unchanged:
                    action = args.OldState switch
                    {
                        EntityState.Added => "Added",
                        EntityState.Modified => "Edited",
                        _ => action
                    };
                    Console.WriteLine($"The object was {action}");
                    break;
            }
        };
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        new CarConfiguration().Configure(modelBuilder.Entity<Car>());
        new DriverConfiguration().Configure(modelBuilder.Entity<Driver>());
        new CarDriverConfiguration().Configure(modelBuilder.Entity<CarDriver>());
        new RadioConfiguration().Configure(modelBuilder.Entity<Radio>());
        new CustomerConfiguration().Configure(modelBuilder.Entity<Customer>());
        new MakeConfiguration().Configure(modelBuilder.Entity<Make>());
        new OrderConfiguration().Configure(modelBuilder.Entity<Order>());
        new CreditRiskConfiguration().Configure(modelBuilder.Entity<CreditRisk>());
        new CustomerOrderViewModelConfiguration().Configure(modelBuilder.Entity<CustomerOrderViewModel>());
        new SeriLogEntryConfiguration().Configure(modelBuilder.Entity<SeriLogEntry>());
        OnModelCreatingPartial(modelBuilder);
    }

    // exception encapsulation in one place.
    public override int SaveChanges()
    {
        try
        {
            return base.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            //A concurrency error occurred
            //Should log and handle intelligently
            throw new CustomConcurrencyException("A concurrency error happened.", ex);
        }
        catch (RetryLimitExceededException ex)
        {
            //DbResiliency retry limit exceeded
            //Should log and handle intelligently
            throw new CustomRetryLimitExceededException("There is a problem with SQL Server.", ex);
        }
        catch (DbUpdateException ex)
        {
            //Should log and handle intelligently
            throw new CustomDbUpdateException("An error occurred updating the database", ex);
        }
        catch (Exception ex)
        {
            //Should log and handle intelligently
            throw new CustomException("An error occurred updating the database", ex);
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(50);
        configurationBuilder.IgnoreAny<INonPersisted>();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    [DbFunction("udf_CountOfMakes", Schema = "dbo")]
    public static int InventoryCountFor(int makeId) => throw new NotSupportedException();

    [DbFunction("udtf_GetCarsForMake", Schema = "dbo")]
    public IQueryable<Car> GetCarsFor(int makeId) => FromExpression(() => GetCarsFor(makeId));
}
