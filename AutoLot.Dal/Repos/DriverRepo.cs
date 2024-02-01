using AutoLot.Dal.EfStructures;

namespace AutoLot.Dal.Repos;

public class DriverRepo : BaseRepo<Driver>, IDriverRepo
{
    // Used by ASP.NET Core and it's dependancy injection.
    public DriverRepo(ApplicationDbContext context) : base(context) { }

    // Used by WPF applications.
    internal DriverRepo(DbContextOptions<ApplicationDbContext> options) : this(new ApplicationDbContext(options)) { }

    internal IOrderedQueryable<Driver> BuildQuery()
    {
        return Table.OrderBy(d => d.PersonInformation.LastName).ThenBy(d => d.PersonInformation.FirstName);
    }

    public override IEnumerable<Driver> GetAll()
    {
        return BuildQuery();
    }

    public override IEnumerable<Driver> GetAllIgnoreQueryFilters()
    {
        return BuildQuery().IgnoreQueryFilters();
    }
}
