using AutoLot.Dal.EfStructures;

namespace AutoLot.Dal.Repos;

public class CreditRiskRepo : BaseRepo<CreditRisk>, ICreditRiskRepo
{
    // Used by ASP.NET Core and it's dependancy injection.
    public CreditRiskRepo(ApplicationDbContext context) : base(context) { }

    // Used by WPF applications.
    internal CreditRiskRepo(DbContextOptions<ApplicationDbContext> options) : this(new ApplicationDbContext(options)) { }


}
