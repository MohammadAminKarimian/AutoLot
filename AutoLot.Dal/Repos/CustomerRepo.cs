using AutoLot.Dal.EfStructures;

namespace AutoLot.Dal.Repos;

public class CustomerRepo : BaseRepo<Customer>, ICustomerRepo
{
    // Used by ASP.NET Core and it's dependancy injection.
    public CustomerRepo(ApplicationDbContext context) : base(context) { }
    // Used by WPF applications.
    internal CustomerRepo(DbContextOptions<ApplicationDbContext> options) : this(new ApplicationDbContext(options)) { }

    public override IEnumerable<Customer> GetAll()
    {
        return Table.Include(c => c.Orders).OrderBy(c => c.PersonInformation.LastName);
        //return Table.Include(c => c.Orders).ThenInclude(o => o.CarNavigation).Include(c => c.CreditRisks).OrderBy(c => c.LastName);
    }
}
