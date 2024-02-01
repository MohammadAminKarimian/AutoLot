using AutoLot.Dal.EfStructures;

namespace AutoLot.Dal.Repos;

public class OrderRepo : TemporalTableBaseRepo<Order>, IOrderRepo
{
    // Used by ASP.NET Core and it's dependancy injection.
    public OrderRepo(ApplicationDbContext context) : base(context) { }

    // Used by WPF applications.
    internal OrderRepo(DbContextOptions<ApplicationDbContext> options) : this(new ApplicationDbContext(options)) { }
}
