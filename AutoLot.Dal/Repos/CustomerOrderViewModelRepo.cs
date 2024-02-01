using AutoLot.Dal.EfStructures;

namespace AutoLot.Dal.Repos;

public class CustomerOrderViewModelRepo : BaseViewRepo<CustomerOrderViewModel>, ICustomerOrderViewModelRepo
{
    // Used by ASP.NET Core and it's dependancy injection.
    public CustomerOrderViewModelRepo(ApplicationDbContext context) : base(context) { }

    // Used by WPF applications.
    internal CustomerOrderViewModelRepo(DbContextOptions<ApplicationDbContext> options) : this(new ApplicationDbContext(options)) { }
}
