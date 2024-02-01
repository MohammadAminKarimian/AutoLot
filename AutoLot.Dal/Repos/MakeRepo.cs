using AutoLot.Dal.EfStructures;

namespace AutoLot.Dal.Repos;

public class MakeRepo : TemporalTableBaseRepo<Make>, IMakeRepo
{
    // Used by ASP.NET Core and it's dependancy injection.
    public MakeRepo(ApplicationDbContext context) : base(context) { }

    // Used by WPF applications.
    internal MakeRepo(DbContextOptions<ApplicationDbContext> options) : this(new ApplicationDbContext(options)) { }

    internal IOrderedQueryable<Make> BuildQuery()
    {
        return Table.OrderBy(m => m.Name);
    }

    public override IEnumerable<Make> GetAll()
    {
        return BuildQuery();
    }

    public override IEnumerable<Make> GetAllIgnoreQueryFilters()
    {
        return BuildQuery().IgnoreQueryFilters();
    }
}
