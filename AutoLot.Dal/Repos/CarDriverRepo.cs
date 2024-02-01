using AutoLot.Dal.EfStructures;
using Microsoft.EntityFrameworkCore.Query;

namespace AutoLot.Dal.Repos;

public class CarDriverRepo : TemporalTableBaseRepo<CarDriver>, ICarDriverRepo
{
    // Used by ASP.NET Core and it's dependancy injection.
    public CarDriverRepo(ApplicationDbContext context) : base(context)
    {
    }

    // Used by WPF applications.
    internal CarDriverRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    internal IIncludableQueryable<CarDriver, Driver> BuildBaseQuery()
        => Table.Include(c => c.CarNavigation).Include(d => d.DriverNavigation);

    public override IEnumerable<CarDriver> GetAll() => BuildBaseQuery();

    public override IEnumerable<CarDriver> GetAllIgnoreQueryFilters() => BuildBaseQuery().IgnoreQueryFilters();

    public override CarDriver Find(int? id) => BuildBaseQuery().IgnoreQueryFilters().Where(cd => cd.Id == id).FirstOrDefault();
}
